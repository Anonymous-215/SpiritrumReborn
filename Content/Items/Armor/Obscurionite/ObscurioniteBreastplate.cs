using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Obscurionite
{
	[AutoloadEquip(EquipType.Body)]
	public class ObscurioniteBreastplate : ModItem
	{
		public static readonly int MaxManaIncrease = 60;
		public static readonly int MaxMinionIncrease = 2;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MaxMinionIncrease);

		public override void SetDefaults() {
			Item.width = 18; 
			Item.height = 18; 
			Item.value = Item.sellPrice(gold: 1); 
			Item.rare = ItemRarityID.Green; 
			Item.defense = 10; 
		}

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.OnFire] = true; 
			player.statManaMax2 += MaxManaIncrease; 
			player.maxMinions += MaxMinionIncrease; 
		}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<ObscurioniteAlloy>(25);
			recipe.AddIngredient(ItemID.BeeBreastplate, 1);
        	recipe.AddIngredient(ItemID.NecroBreastplate, 1);
        	recipe.AddIngredient(ItemID.MoltenBreastplate, 1);
        	recipe.AddIngredient(ItemID.JungleShirt, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}

