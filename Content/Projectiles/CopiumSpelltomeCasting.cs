using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CopiumSpelltomeCasting : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 40;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
            }
            if (Main.rand.NextBool(12))
            {
                Vector2 dir = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1.5f, 3f);
                int spark = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, dir, ModContent.ProjectileType<CopiumSpark>(), (int)(Projectile.damage * 0.4f), 0f, Projectile.owner);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 v = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 4f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, v, ModContent.ProjectileType<CopiumSpark>(), (int)(Projectile.damage * 0.5f), 0f, Projectile.owner);
            }
        }
    }
}


