using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
    public class VoidBolts : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override void AI()
        {
            float homingRange = 300f;
            float homingStrength = 0.12f;
            NPC target = null;
            float closest = homingRange;
            for (int n = 0; n < Main.maxNPCs; n++)
            {
                NPC npc = Main.npc[n];
                if (npc.CanBeChasedBy() && !npc.friendly)
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < closest && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
                    {
                        closest = dist;
                        target = npc;
                    }
                }
            }
            if (target != null)
            {
                Vector2 toTarget = target.Center - Projectile.Center;
                toTarget.Normalize();
                toTarget *= 12f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget, homingStrength);
            }
            Projectile.rotation += 0.3f;
        }
    }
}


