using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class VoidRiftProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            Player target = Main.player[(int)Projectile.ai[0]];
            if (!target.active || target.dead)
            {
                Projectile.Kill();
                return;
            }

            Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget * 8f, 0.05f);

            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Main.rand.NextVector2Circular(2, 2), 150, default, 1.5f);
            }
        }
    }

    public class ShadowCloneProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 96;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 150;
        }

        public override void AI()
        {
            Player target = Main.player[(int)Projectile.ai[0]];
            if (!target.active || target.dead)
            {
                Projectile.Kill();
                return;
            }

            Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget * 6f, 0.08f);

            if (Projectile.timeLeft % 60 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 fireDir = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, fireDir * 10f, 
                    ModContent.ProjectileType<VoidBoltProjectile>(), Projectile.damage, 0f, Main.myPlayer);
            }

            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Main.rand.NextVector2Circular(3, 3), 150, default, 1.2f);
            }
        }
    }

    public class VoidBoltProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            
            if (Main.rand.NextBool(4))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Vector2.Zero, 150, default, 1f);
            }
        }
    }

    public class DimensionalTearProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 480;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 50;
        }

        public override void AI()
        {
            Player target = Main.player[(int)Projectile.ai[0]];
            
            Projectile.velocity = Vector2.Zero;
            
            if (Projectile.timeLeft % 45 == 0 && Main.netMode != NetmodeID.MultiplayerClient && target.active)
            {
                Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, toTarget * 12f,
                    ModContent.ProjectileType<VoidBoltProjectile>(), Projectile.damage, 0f, Main.myPlayer);
            }

            if (Main.rand.NextBool(2))
            {
                Vector2 dustPos = Projectile.Center + Main.rand.NextVector2Circular(24, 24);
                Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, (Projectile.Center - dustPos) * 0.1f, 150, default, 2f);
            }
        }
    }

    public class VoidStormProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player target = Main.player[(int)Projectile.ai[0]];
            
            if (target.active && Projectile.timeLeft < 180)
            {
                Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget * 10f, 0.03f);
            }

            Projectile.rotation += 0.2f;
            
            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Main.rand.NextVector2Circular(2, 2), 150, default, 1.3f);
            }
        }
    }

    public class RealityBreakProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft % 30 == 0)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.5f, 0.5f));
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            
            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch, Main.rand.NextVector2Circular(3, 3), 150, default, 1.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity * -0.8f;
            return false;
        }
    }

    public class VoidNovaProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.hostile = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.3f;
            
            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.RedTorch, Main.rand.NextVector2Circular(4, 4), 150, default, 2f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(8, 8);
                Dust.NewDustPerfect(Projectile.Center, DustID.RedTorch, velocity, 150, default, 2.5f);
            }
        }
    }
}


