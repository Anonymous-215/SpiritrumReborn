

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class WhisperingBladeProjectile : ModProjectile
    {

        public override void SetDefaults() 
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;                          
            AIType = ProjectileID.Bullet;        
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 4;                         
            Projectile.timeLeft = 300;                        
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;                          
        }

        public override void AI() 
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Shadowflame,      
                    Projectile.velocity.X * 0.2f,
                    Projectile.velocity.Y * 0.2f
                );
            }
        }

        private float spinOffset;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) 
        {
            target.AddBuff(BuffID.ShadowFlame, 120);               

        }
    }
}




