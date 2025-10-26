using SpiritrumReborn.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Armor.Obscurionite
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class ObscurioniteLeggings : ModItem
	{
		public static readonly float MoveSpeedBonus = 0.20f; 
		public static readonly float RangedDamageBonus = 0.15f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(MoveSpeedBonus * 100));

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 9; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += MoveSpeedBonus; // Increase the movement speed of the player
			player.GetDamage(DamageClass.Ranged) += RangedDamageBonus;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ObscurioniteAlloy>(20);
            recipe.AddIngredient(ItemID.BeeGreaves, 1);
            recipe.AddIngredient(ItemID.NecroGreaves, 1);
            recipe.AddIngredient(ItemID.MoltenGreaves, 1);
            recipe.AddIngredient(ItemID.JunglePants, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}