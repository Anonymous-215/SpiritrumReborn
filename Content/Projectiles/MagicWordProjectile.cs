using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class MagicWordProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1; //I want this projectile sprite sheet to have all 26 letters and randomized frames
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.2f;
            if (Main.rand.NextBool(4))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Main.dust[d].velocity *= 0.2f;
            }
        }
    }
}
