using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class ClawBladeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3; 
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 30;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            float angular = Projectile.ai[0];
            Projectile.rotation += angular;

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(angular * 0.5f);
                Projectile.velocity *= 0.995f;
            }

            if (Main.rand.NextBool(3))
                Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Projectile.velocity * -0.1f, 0, default, 0.6f).noGravity = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.defense = Math.Max(0, target.defense - 6);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Vector2.Zero, 0, default, 0.8f).noGravity = true;
            return true;
        }
    }
}


