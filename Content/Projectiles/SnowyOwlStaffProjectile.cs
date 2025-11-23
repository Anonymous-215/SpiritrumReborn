using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Buffs;

namespace SpiritrumReborn.Content.Projectiles
{
    public class SnowyOwlStaffProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 5; 
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6) 
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.HasBuff(ModContent.BuffType<SnowyOwlStaffBuff>()))
            {
                Projectile.Kill();
                return;
            }
            if (player.HasBuff(ModContent.BuffType<SnowyOwlStaffBuff>()))
            {
                player.AddBuff(ModContent.BuffType<SnowyOwlStaffBuff>(), 3600000);
            }

            float detectRadius = 500f;
            NPC target = null;
            float minDist = detectRadius;
            int currentTarget = -1;
            if (Projectile.ai[0] >= 0 && Projectile.ai[0] < Main.maxNPCs && Main.npc[(int)Projectile.ai[0]].CanBeChasedBy(this))
            {
                currentTarget = (int)Projectile.ai[0];
                target = Main.npc[currentTarget];
                minDist = Vector2.Distance(Projectile.Center, target.Center);
            }
            if (Main.rand.NextBool(90))
            {
                float farDist = 128f;
                NPC swapTarget = null;
                float swapDist = farDist;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(this))
                    {
                        float dist = Vector2.Distance(Projectile.Center, npc.Center);
                        if (dist > farDist && dist < detectRadius)
                        {
                            if (swapTarget == null || dist < swapDist)
                            {
                                swapTarget = npc;
                                swapDist = dist;
                                currentTarget = i;
                            }
                        }
                    }
                }
                if (swapTarget != null)
                {
                    target = swapTarget;
                    Projectile.ai[0] = currentTarget;
                }
            }
            if (target == null || !target.active || !target.CanBeChasedBy(this))
            {
                minDist = detectRadius;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(this))
                    {
                        float dist = Vector2.Distance(Projectile.Center, npc.Center);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            target = npc;
                            currentTarget = i;
                        }
                    }
                }
                if (target != null)
                    Projectile.ai[0] = currentTarget;
            }

            if (target != null)
            {
                Vector2 toTarget = target.Center - Projectile.Center;
                float speed = 8f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget.SafeNormalize(Vector2.Zero) * speed, 0.1f);
            }
            else
            {
                Vector2 idlePos = player.Center + new Vector2(-40 * player.direction, -60);
                Vector2 toIdle = idlePos - Projectile.Center;
                float idleSpeed = 6f;
                if (toIdle.Length() > 200f)
                    Projectile.velocity = toIdle.SafeNormalize(Vector2.Zero) * idleSpeed;
                else
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, toIdle.SafeNormalize(Vector2.Zero) * idleSpeed, 0.05f);
            }

            if (Projectile.velocity.X != 0)
                Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0) ? 1 : -1;
        }
    }
}


