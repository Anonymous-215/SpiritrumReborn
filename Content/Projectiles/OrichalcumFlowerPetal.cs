using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class OrichalcumFlowerPetal : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.FlowerPetal}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FlowerPetal);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 40;
            Projectile.penetrate = 1;
        }
    }
}


