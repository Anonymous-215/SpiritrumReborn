using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
	public class LocalisationOverride : GlobalItem
	{
		private static Dictionary<int, LocalizedText[]> tooltipCache;
		internal static LocalizedText CobaltSetBonusText;
		internal static LocalizedText MythrilSetBonusText;
		internal static LocalizedText AdamantiteSetBonusText;

		public override bool InstancePerEntity => false;

		public override void Load()
		{
			tooltipCache = new Dictionary<int, LocalizedText[]>();

			void AddTooltip(int itemId, params (string key, string defaultText)[] lines)
			{
				var localizedLines = new List<LocalizedText>(lines.Length);
				foreach ((string key, string value) in lines)
				{
					localizedLines.Add(Language.GetOrRegister(Mod.GetLocalizationKey(key), () => value));
				}

				tooltipCache[itemId] = localizedLines.ToArray();
			}

			AddTooltip(ItemID.CobaltSword,
				("Tooltips.CobaltSword.Line1", "Hits have 20% chance to increase melee speed by 33% for 5 seconds."),
				("Tooltips.CobaltSword.Line2", "Every fifth hit releases a lightning sphrere."));

			AddTooltip(ItemID.CobaltNaginata,
				("Tooltips.CobaltNaginata.Line1", "Hits are now able to strike multiple times."));

			AddTooltip(ItemID.CobaltRepeater,
				("Tooltips.CobaltRepeater.Line1", "Fires in rapid three-round bursts."));

			AddTooltip(ItemID.PalladiumSword,
				("Tooltips.PalladiumSword.Line1", "Melee hits restore 2 hp."));

			AddTooltip(ItemID.PalladiumPike,
				("Tooltips.PalladiumPike.Line2", "Hits cause shockwaves."));

			AddTooltip(ItemID.PalladiumRepeater,
				("Tooltips.PalladiumRepeater.Line1", "Dealing damage from up close heals you for a portion of the damage dealt."));

			AddTooltip(ItemID.MythrilSword,
				("Tooltips.MythrilSword.Line1", "Hits release ricocheting dust."));

            AddTooltip(ItemID.MythrilHalberd,
                ("Tooltips.MythrilHalberd.Line1", "Crit chance scales with enemy defense."));

			AddTooltip(ItemID.MythrilRepeater,
				("Tooltips.MythrilRepeater.Line1", "Arrows inflict Mythril Curse, giving -20 defense and -10% damage reduction."));

			AddTooltip(ItemID.OrichalcumSword,
				("Tooltips.OrichalcumSword.Line1", "True melee hits do 3 weaker hits"));

			AddTooltip(ItemID.OrichalcumHalberd,
                ("Tooltips.OrichalcumHalberd.Line1", "Hits builds oricalchum curse (max 10)."),
                ("Tooltips.OrichalcumHalberd.Line2", "Orichalcum curse increases damage taken by 5% per stack from orichalcum weapons."),
                ("Tooltips.OrichalcumHalberd.Line3", "At max stacks the next hit crits and adds 3 extra hits."));

			AddTooltip(ItemID.OrichalcumRepeater,
				("Tooltips.OrichalcumRepeater.Line1", "Fires 2 arrows"));

            AddTooltip(ItemID.AdamantiteSword,
                ("Tooltips.AdamantiteSword.Line1", "Swinging summons orbiting orbs."),
                ("Tooltips.AdamantiteSword.Line2", "Orbs seek out enemies when released."));

			AddTooltip(ItemID.AdamantiteGlaive,
				("Tooltips.AdamantiteGlaive.Line1", "Strikes slow enemies (up to 40%) and applies a random debuff."));

			AddTooltip(ItemID.AdamantiteRepeater,
				("Tooltips.AdamantiteRepeater.Line1", "Arrows fall faster but deal 50% more damage to airborne foes."));

			AddTooltip(ItemID.TitaniumSword,
				("Tooltips.TitaniumSword.Line1", "Marks foes with titanium mass, causing them to shed seeking shards."),
				("Tooltips.TitaniumSword.Line2", "Marked enemies deal 20% less contact damage."));

			AddTooltip(ItemID.TitaniumTrident,
				("Tooltips.TitaniumTrident.Line1", "Pierces rapidly with reduced immunity frames."),
				("Tooltips.TitaniumTrident.Line2", "Ends titanium mass with a burst."));

			AddTooltip(ItemID.TitaniumRepeater,
				("Tooltips.TitaniumRepeater.Line1", "Replaces arrows with piercing bolts that deal extra damage the faster they go."));

            CobaltSetBonusText = Language.GetOrRegister(Mod.GetLocalizationKey("SetBonus.Cobalt"),
                () => "Hitting enemies shoots out a lightning bolt (0.5s cooldown).");

			MythrilSetBonusText = Language.GetOrRegister(Mod.GetLocalizationKey("SetBonus.Mythril"),
				() => "Gain +10% damage per active debuff.\nTaking damage grants 15% damage reduction for 5 seconds.\nFurther hits while it is active add half as much.\nAfter not getting hit for 5 seconds, gain 3 seconds of invulnerability.");
            

			AdamantiteSetBonusText = Language.GetOrRegister(Mod.GetLocalizationKey("SetBonus.Adamantite"),
				() => "Nearby foes grants +2 defense and +5% knockback (up to 8 times).");
		}

		public override void Unload()
		{
			tooltipCache = null;
			CobaltSetBonusText = null;
			MythrilSetBonusText = null;
			AdamantiteSetBonusText = null;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (tooltipCache == null || !tooltipCache.TryGetValue(item.type, out LocalizedText[] lines))
			{
				return;
			}

			foreach (LocalizedText line in lines)
			{
				tooltips.Add(new TooltipLine(Mod, "SpiritrumReborn.OreRework", line.Value)
				{
					OverrideColor = new Color(126, 200, 255)
				});
			}
		}
	}

	public class LocalisationOverridePlayer : ModPlayer
	{
		public override void PostUpdateEquips()
		{
			if (LocalisationOverride.CobaltSetBonusText != null && CobaltRework.HasFullCobaltSet(Player))
			{
				Player.setBonus = LocalisationOverride.CobaltSetBonusText.Value;
			}
			else if (LocalisationOverride.MythrilSetBonusText != null && HasFullMythrilSet())
			{
				Player.setBonus = LocalisationOverride.MythrilSetBonusText.Value;
			}
			else if (LocalisationOverride.AdamantiteSetBonusText != null && HasFullAdamantiteSet())
			{
				Player.setBonus = LocalisationOverride.AdamantiteSetBonusText.Value;
			}
		}

		private bool HasFullMythrilSet()
		{
			Item head = Player.armor[0];
			Item body = Player.armor[1];
			Item legs = Player.armor[2];

			if (body.type != ItemID.MythrilChainmail || legs.type != ItemID.MythrilGreaves)
			{
				return false;
			}

			return head.type == ItemID.MythrilHelmet || head.type == ItemID.MythrilHat || head.type == ItemID.MythrilHood;
		}

		private bool HasFullAdamantiteSet()
		{
			Item head = Player.armor[0];
			Item body = Player.armor[1];
			Item legs = Player.armor[2];

			if (body.type != ItemID.AdamantiteBreastplate || legs.type != ItemID.AdamantiteLeggings)
			{
				return false;
			}

			return head.type == ItemID.AdamantiteHeadgear || head.type == ItemID.AdamantiteHelmet || head.type == ItemID.AdamantiteMask;
		}
	}
}


