using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class TitaniumArrowProj : ModProjectile
    {
        private bool bonusConsumed;
        private float baseSpeed;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.penetrate = 3;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.extraUpdates = 0;
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            baseSpeed = Projectile.velocity.Length();
        }

        public override void AI()
        {
            if (Projectile.velocity.Y > 0f)
            {
                Projectile.velocity.Y += 0.12f; 
            }

            if (Projectile.velocity.LengthSquared() > 0.01f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            float speed = Projectile.velocity.Length();
            float ratio = speed / Math.Max(1f, baseSpeed);
            float bonus = MathHelper.Clamp(ratio - 1f, 0f, 0.5f); 
            if (!bonusConsumed && bonus > 0f)
            {
                modifiers.SourceDamage *= 1f * (1f + 2f * bonus);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!bonusConsumed)
            {
                bonusConsumed = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }
    }
}


