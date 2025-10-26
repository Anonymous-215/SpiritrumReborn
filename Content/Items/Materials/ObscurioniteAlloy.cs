using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Materials
{
	public class ObscurioniteAlloy : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 25;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 750; 
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Green;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<ObscurioniteOre>(4)
				.AddTile(TileID.Hellforge)
				.Register();
		}
	}
}