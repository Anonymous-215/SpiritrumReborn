using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CopiumEnergy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
                Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.ai[0] = 0f;
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f;
                    dir = dir.RotatedByRandom(MathHelper.ToRadians(20));
                    int spark = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, dir, ModContent.ProjectileType<CopiumSpark>(), (int)(Projectile.damage * 0.35f), 0f, Projectile.owner);
                    if (Main.projectile.IndexInRange(spark))
                    {
                        Main.projectile[spark].timeLeft = 40;
                    }
                }
            }

            if (Main.rand.NextBool(3))
            {
                var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
                d.velocity *= 0.2f;
                d.scale = 0.8f;
                d.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 6; i++)
            {
                Vector2 dir = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 5f);
                int spark = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, dir, ModContent.ProjectileType<CopiumSpark>(), (int)(Projectile.damage * 0.45f), 0f, Projectile.owner);
                if (Main.projectile.IndexInRange(spark))
                    Main.projectile[spark].timeLeft = 30;
            }
        }
    }
}


