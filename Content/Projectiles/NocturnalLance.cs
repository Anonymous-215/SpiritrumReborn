using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SpiritrumReborn.Content.Projectiles
{
    public class NocturnalLance : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 6; 
            Projectile.extraUpdates = 600; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 150, default, 1.2f);
            Main.dust[dust].noGravity = true;
        }
    }
}


