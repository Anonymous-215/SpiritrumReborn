using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
	public class TitaniumRework : GlobalItem
	{
		public override bool InstancePerEntity => true;

		public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (item.type == ItemID.TitaniumSword && target.CanBeChasedBy())
			{
				var g = target.GetGlobalNPC<TitaniumGlobalNPC>();
				g.titaniumMassTime = 4 * 60;
				g.titaniumMassOwner = player.whoAmI;
			}

			base.OnHitNPC(item, player, target, hit, damageDone);
		}

		public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (item.type != ItemID.TitaniumRepeater)
			{
				return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
			}

			if (Main.myPlayer == player.whoAmI)
			{
				int projType = ModContent.ProjectileType<Projectiles.TitaniumArrowProj>();
				Projectile.NewProjectile(source, position, velocity, projType, damage, knockback, player.whoAmI);
			}

			return false;
		}
	}

	public class TitaniumGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public int titaniumMassTime;
		public int titaniumMassOwner = -1;
		private bool titaniumWasActive;
		private bool titaniumColorStored;
		private Color titaniumOriginalColor;

		public override void ResetEffects(NPC npc)
		{
			if (titaniumMassTime <= 0)
			{
				titaniumMassOwner = -1;
			}
		}

		public override void AI(NPC npc)
		{
			bool active = titaniumMassTime > 0;
			if (active && !titaniumWasActive)
			{
				titaniumOriginalColor = npc.color;
				titaniumColorStored = true;
			}

			if (active)
			{
				titaniumMassTime--;
				npc.color = Color.Gray;

				if (Main.rand.NextBool(10))
				{
					int owner = (titaniumMassOwner >= 0 && titaniumMassOwner < Main.maxPlayers) ? titaniumMassOwner : 255;
					var src = npc.GetSource_FromAI();
					int proj = ModContent.ProjectileType<Projectiles.TitaniumMass>();
					Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
					int dmg = 20;
					if (owner >= 0 && owner < Main.maxPlayers)
					{
						Player p = Main.player[owner];
						if (p != null && p.active && !p.dead && p.HeldItem != null)
						{
							dmg = Math.Max(10, p.GetWeaponDamage(p.HeldItem) / 2);
						}
					}
					Projectile.NewProjectile(src, npc.Center, vel, proj, dmg, 1f, owner);
				}
			}
			else
			{
				if (titaniumWasActive && titaniumColorStored)
				{
					npc.color = titaniumOriginalColor;
					titaniumColorStored = false;
				}
			}

			titaniumWasActive = active;
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (titaniumMassTime > 0)
			{
				modifiers.FinalDamage *= 0.8f;
			}
		}
	}

	public class TitaniumGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => false;

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (projectile.type == ProjectileID.TitaniumTrident)
			{
				projectile.usesLocalNPCImmunity = true;
				projectile.localNPCHitCooldown = 7;
			}
		}

		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (projectile.type == ProjectileID.TitaniumTrident)
			{
				var g = target.GetGlobalNPC<TitaniumGlobalNPC>();
				if (g.titaniumMassTime > 0)
				{
					int owner = projectile.owner;
					int projType = ModContent.ProjectileType<Projectiles.TitaniumMass>();
					var src = projectile.GetSource_OnHit(target);
					for (int i = 0; i < 8; i++)
					{
						Vector2 vel = Main.rand.NextVector2Circular(4f, 4f);
						Projectile.NewProjectile(src, target.Center, vel, projType, Math.Max(10, damageDone / 3), 1f, owner);
					}
					g.titaniumMassTime = 0;
					g.titaniumMassOwner = -1;
				}
			}

			base.OnHitNPC(projectile, target, hit, damageDone);
		}
	}
}


