using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SpiritrumReborn.Content.Projectiles
{
    public class SunBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; 
            Projectile.extraUpdates = 600; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Lava, Projectile.velocity.X * 0.01f, Projectile.velocity.Y * 0.01f, 10, default, 0.3f);
            Main.dust[dust].noGravity = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 8);
		}
    }
}


