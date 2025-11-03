using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Projectiles
{
	public class SlimyKunai : ModProjectile
	{
		private int pierceLeft = 5;

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5; 
            Projectile.timeLeft = 360;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.scale = 0.6f;
		}

		public override void AI()
		{
			Projectile.velocity.Y += 0.25f;
			if (Projectile.velocity.Length() > 0.01f)
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			pierceLeft--; //loses 1 pierce on bounce
			if (pierceLeft <= 0)
				return true; 

			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = -oldVelocity.X * 0.7f;
			if (Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = -oldVelocity.Y * 0.7f;

			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			return false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			pierceLeft--; //if on bounce, pierce goes to 0, the projectile is gone
			if (pierceLeft <= 0)
				Projectile.Kill();
		}
	}
}


