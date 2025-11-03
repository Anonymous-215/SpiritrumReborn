using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CoralShotShrapnel : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 1;
            Projectile.OriginalArmorPenetration = 15; 
            Projectile.localNPCHitCooldown = 40;
        }
        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero) //points towards where they are going
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }
    }
}


