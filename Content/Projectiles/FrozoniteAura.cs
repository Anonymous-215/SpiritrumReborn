using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Projectiles
{
    public class FrozoniteAura : ModProjectile
    {
    public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.hide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60; 
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            int orbitIndex = (int)Projectile.ai[0]; 
            float orbitRadius = 180f;
            float orbitSpeed = 0.10f; 
            float angle = Main.GameUpdateCount * orbitSpeed + MathHelper.TwoPi * orbitIndex / 3f;
            Vector2 offset = orbitRadius * new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle));
            Projectile.Center = player.Center + offset;

            Projectile.rotation += 0.5f;


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 180);
        }



    }
}


