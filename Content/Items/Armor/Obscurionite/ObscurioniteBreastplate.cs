using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Obscurionite
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class ObscurioniteBreastplate : ModItem
	{
		public static readonly int MaxManaIncrease = 60;
		public static readonly int MaxMinionIncrease = 2;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MaxMinionIncrease);

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 10; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
			player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have by 20
			player.maxMinions += MaxMinionIncrease; // Increase how many minions the player can have by one
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe().AddIngredient<ObscurioniteAlloy>(10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}