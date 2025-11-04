using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class AdamantiteEnergyOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = false; 
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.alpha = 40;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!owner.active || owner.dead)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.ai[1] == 0f)
            {
                OrbitOwner(owner);
            }
            else
            {
                ReleaseBehavior();
            }
        }

        private void OrbitOwner(Player owner)
        {
            Projectile.friendly = false;
            Projectile.tileCollide = false;

            float radius = 60f;
            Projectile.ai[0] += 0.05f;
            Vector2 offset = radius * new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0]));
            Projectile.Center = owner.Center + offset;
            Projectile.velocity = Vector2.Zero;
            Projectile.timeLeft = 600;
        }

        private void ReleaseBehavior()
        {
            Projectile.friendly = true;
            Projectile.tileCollide = false;

            if (Projectile.velocity.LengthSquared() < 0.01f)
            {
                Projectile.Kill();
                return;
            }

            ApplyLimitedHoming();
            SpawnTravelDust();

            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        private void ApplyLimitedHoming()
        {
            const float homingRange = 200f;
            const float homingLerp = 0.8f;

            NPC closest = null;
            float sqRange = homingRange * homingRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.CanBeChasedBy())
                {
                    continue;
                }

                float distanceSq = Vector2.DistanceSquared(npc.Center, Projectile.Center);
                if (distanceSq <= sqRange)
                {
                    sqRange = distanceSq;
                    closest = npc;
                }
            }

            if (closest == null)
            {
                return;
            }

            Vector2 currentVelocity = Projectile.velocity;
            float currentSpeed = currentVelocity.Length();
            if (currentSpeed <= 0f)
            {
                currentSpeed = 6f;
            }

            Vector2 currentDir = currentVelocity.SafeNormalize(Vector2.UnitX);
            Vector2 desiredDirection = (closest.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
            Vector2 steeredDir = Vector2.Lerp(currentDir, desiredDirection, homingLerp).SafeNormalize(Vector2.UnitX);
            Projectile.velocity = steeredDir * currentSpeed;
        }

        private void SpawnTravelDust()
        {
            Vector2 dustPosition = Projectile.Center - new Vector2(Projectile.width, Projectile.height) * 0.5f;
            int dust = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Adamantite, 0f, 0f, 150, default, 1.1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Projectile.velocity * 0.2f;

            int electricDust = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 120, default, 1f);
            Main.dust[electricDust].noGravity = true;
            Main.dust[electricDust].velocity = Projectile.velocity * 0.15f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                int dustType = (i % 2 == 0) ? DustID.Adamantite : DustID.Electric;
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 150, default, 1.2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity += Projectile.velocity * 0.4f + Main.rand.NextVector2Circular(2f, 2f);
            }
        }
    }
}


