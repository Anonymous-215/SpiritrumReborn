using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CosmicOrbit : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f;
            if (Main.rand.NextBool(6))
            {
                Vector2 vel = Main.rand.NextVector2Circular(2f, 2f) + Projectile.velocity * 0.2f;
                Projectile.NewProjectile(Projectile.GetSource_Misc("CosmicShot"), Projectile.Center, vel, ProjectileID.MagicMissile, Projectile.damage / 2, 0f, Projectile.owner);
            }
        }
    }
}


