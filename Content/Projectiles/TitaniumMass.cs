using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class TitaniumMass : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255; 
            Projectile.hide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            int updatesPerSecond = 60 * (1 + Projectile.extraUpdates);
            bool homingActive = Projectile.ai[0] >= 2 * updatesPerSecond;

            if (homingActive)
            {
                NPC target = FindTarget();
                if (target != null)
                {
                    float speed = Math.Max(6f, Projectile.velocity.Length());
                    Vector2 desired = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * speed;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desired, 0.1f);
                }
            }

            Projectile.localAI[0]++;
            if (((int)Projectile.localAI[0] % 4) == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 offset = -Projectile.velocity * (0.25f * i);
                    Vector2 spawnPos = Projectile.Center + offset;
                    int type = DustID.Titanium;
                    int d = Dust.NewDust(spawnPos - new Vector2(4f, 4f), 8, 8, type, 0f, 0f, 150, default, 1.1f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.1f;
                }
            }
        }

        private NPC FindTarget()
        {
            float range = 2480f;
            float sq = range * range;
            NPC best = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (!n.CanBeChasedBy())
                    continue;

                var g = n.GetGlobalNPC<BaseGameReworks.TitaniumGlobalNPC>();
                if (g != null && g.titaniumMassTime > 0)
                    continue;

                float d = Vector2.DistanceSquared(n.Center, Projectile.Center);
                if (d < sq)
                {
                    sq = d;
                    best = n;
                }
            }
            return best;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dustType = (i % 2 == 0) ? DustID.Titanium : DustID.Electric;
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity += Projectile.velocity * 0.3f + Main.rand.NextVector2Circular(1.5f, 1.5f);
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            int updatesPerSecond = 60 * (1 + Projectile.extraUpdates);
            bool damageActive = Projectile.ai[0] >= 2 * updatesPerSecond;
            return damageActive ? (bool?)null : false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= 0.5f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}


