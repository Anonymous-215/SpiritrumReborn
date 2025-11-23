using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class LightningUmbrellaCloud : ModProjectile
    {
        private const int CloudCount = 3;
        private int timer;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 3600;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!owner.active || owner.dead)
            {
                Projectile.Kill();
                return;
            }
            Projectile.Center = owner.Center;
            timer++;
            for (int i = 0; i < CloudCount; i++)
            {
                float angle = (MathHelper.TwoPi / CloudCount) * i + timer / 60f;
                Vector2 offset = new Vector2(40f, 0f).RotatedBy(angle);
                Vector2 cloudPos = Projectile.Center + offset;
                if (Main.rand.NextBool(120)) 
                {
                    Vector2 target = cloudPos + new Vector2(Main.rand.NextFloat(-100, 100), Main.rand.NextFloat(-200, 0));
                    Vector2 direction = target - cloudPos;
                    if (direction == Vector2.Zero)
                        direction = new Vector2(0f, 1f);
                    direction.Normalize();
                    Vector2 velocity = direction * 12f;

                    int bolt = Projectile.NewProjectile(Projectile.GetSource_FromThis(), cloudPos, velocity, ProjectileID.DD2LightningAuraT1, 20, 2f, Projectile.owner);
                    if (bolt >= 0 && bolt < Main.maxProjectiles)
                    {
                        Main.projectile[bolt].timeLeft = 30;
                        Main.projectile[bolt].friendly = true;
                        Main.projectile[bolt].hostile = false;
                    }
                }
                if (Main.rand.NextBool(8))
                {
                    Dust.NewDust(cloudPos - new Vector2(4, 4), 8, 8, DustID.Electric);
                }
            }
        }
    }
}


