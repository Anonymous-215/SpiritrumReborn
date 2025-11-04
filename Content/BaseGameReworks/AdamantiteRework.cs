using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
	public class AdamantiteRework : GlobalItem
	{
		public override bool InstancePerEntity => true;

		public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!target.active)
			{
				base.OnHitNPC(item, player, target, hit, damageDone);
				return;
			}

			int itemType = item.type;

			if (IsAdamantitePolearm(itemType))
			{
				if (!target.boss)
				{
					var global = target.GetGlobalNPC<AdamantiteGlobalNPC>();
					if (global.adamantiteSlowStacks < AdamantiteGlobalNPC.MaxSlowStacks)
					{
						global.adamantiteSlowStacks++;
					}
					global.adamantiteSlowTimer = AdamantiteGlobalNPC.SlowDuration;
				}

				int[] debuffs =
				{
					BuffID.OnFire3,      
					BuffID.Frostburn2,   
					BuffID.Poisoned,
					BuffID.CursedInferno,
					BuffID.Ichor,
					BuffID.Bleeding
				};

				int chosen = Main.rand.Next(debuffs.Length);
				target.AddBuff(debuffs[chosen], 5 * 60);
			}

			base.OnHitNPC(item, player, target, hit, damageDone);
		}

		public override float UseSpeedMultiplier(Item item, Player player)
		{
			if (IsAdamantiteRepeater(item))
			{
				return 1.2f;
			}

			return base.UseSpeedMultiplier(item, player);
		}

		internal static bool IsAdamantiteSword(Item item)
		{
			if (item == null || item.IsAir)
				return false;

			return item.type == ItemID.AdamantiteSword;
		}

		internal static bool IsAdamantiteRepeater(Item item)
		{
			if (item == null || item.IsAir)
				return false;

			return item.type == ItemID.AdamantiteRepeater;
		}

		private bool IsAdamantitePolearm(int itemType)
		{
			return itemType == ItemID.AdamantiteGlaive;
		}
	}

	public class AdamantiteGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public const int MaxSlowStacks = 4;
		public const int SlowDuration = 5 * 60;

		public int adamantiteSlowStacks = 0;
		public int adamantiteSlowTimer = 0;

		public override void ResetEffects(NPC npc)
		{
		}

		public override void AI(NPC npc)
		{
			if (adamantiteSlowTimer > 0)
			{
				adamantiteSlowTimer--;
				if (adamantiteSlowTimer <= 0)
				{
					adamantiteSlowStacks = 0;
				}
			}
		}

		public override void PostAI(NPC npc)
		{
			if (adamantiteSlowStacks <= 0 || npc.boss)
				return;

			float multiplier = 1f - 0.1f * adamantiteSlowStacks;
			multiplier = MathHelper.Clamp(multiplier, 0.6f, 1f);

			npc.velocity *= multiplier;
		}
	}

	public class AdamantiteGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		private bool fromAdamantiteRepeater = false;

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (source is EntitySource_ItemUse_WithAmmo withAmmo && AdamantiteRework.IsAdamantiteRepeater(withAmmo.Item))
			{
				fromAdamantiteRepeater = true;
			}
		}

		public override void AI(Projectile projectile)
		{
			if (fromAdamantiteRepeater && projectile.arrow)
			{
				projectile.velocity.Y += 0.15f;
			}
		}

		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (!fromAdamantiteRepeater)
				return;

			if (IsAirborne(target))
			{
				modifiers.FinalDamage *= 1.5f;
			}
		}

		private bool IsAirborne(NPC target)
		{
			if (!target.collideY)
				return true;

			if (target.noGravity || target.noTileCollide)
				return true;

			return target.velocity.Y != 0f;
		}
	}
}


