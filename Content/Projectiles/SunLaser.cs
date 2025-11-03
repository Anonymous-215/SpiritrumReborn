using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SpiritrumReborn.Content.Projectiles
{
    public class SunLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1; 
            Projectile.extraUpdates = 2; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1.2f, 1.1f, 0.5f); //Makes light
            for (int i = 0; i < 4; i++) //Dust effect
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.01f, Projectile.velocity.Y * 0.01f, 10, default, 0.3f);
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.velocity.Length() > 0.01f)
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 40); //Makes the projectile inflict on fire for 40 ticks
		}
    }
}


