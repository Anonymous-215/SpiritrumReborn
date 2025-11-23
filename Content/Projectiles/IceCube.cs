using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
	public class IceCube : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 3; 
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 0;
		}

		public override void AI()
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 150, default, 1.1f);
			}
			Projectile.rotation += Projectile.velocity.X * 0.08f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.penetrate > 1)
			{
				if (Projectile.velocity.X != oldVelocity.X)
					Projectile.velocity.X = -oldVelocity.X * 0.8f;
				if (Projectile.velocity.Y != oldVelocity.Y)
					Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
				Projectile.penetrate--;
				return false;
			}
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.NextBool(3))
			{
				int extra = (int)(hit.Damage * 0.5f);
				target.StrikeNPC(new NPC.HitInfo() { Damage = extra, Knockback = 0f, HitDirection = hit.HitDirection });
				target.AddBuff(BuffID.Frostburn2, 120);
			}
			else
			{
				target.AddBuff(BuffID.Chilled, 60);
			}
		}
	}
}


