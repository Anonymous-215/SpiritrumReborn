using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Projectiles
{
	public class ElementalFlailBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.IsAWhip[Projectile.type] = false;
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = 15; 
			AIType = ProjectileID.Sunfury;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1500;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
		}

		public override bool PreAI()
		{
			int mode = (int)Projectile.ai[0];
			if (Main.rand.NextBool(2)) //This makes it so that the ball produces dusst every 2 ticks.
			{
				int dustType = DustID.Smoke;
				if (mode == 0) dustType = DustID.Torch; //spin
				else if (mode == 1 || mode == 2) dustType = DustID.Frost; //Throw + return
				var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);
				d.velocity *= 1.5f;
				d.noGravity = false;
			}
			return true; 
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			int mode = (int)Projectile.ai[0];
			if (mode == 0) //This is while spinning
			{
				target.AddBuff(BuffID.OnFire3, 360);
			}
			else if (mode == 1) //This is when thrown
			{
				target.AddBuff(BuffID.Frostburn2, 240);
			}
			else if (mode == 2) //This is so that the ball can return to the player
			{
				int nextTarget = -1;
				float bestDist = 400f;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC n = Main.npc[i];
					if (n.active && !n.friendly && n.whoAmI != target.whoAmI)
					{
						float d = Vector2.Distance(n.Center, Projectile.Center);
						if (d < bestDist)
						{
							bestDist = d;
							nextTarget = i;
						}
					}
				}
			}
		}
	}
}



