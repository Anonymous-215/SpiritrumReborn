using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; 
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Materials
{
	public class PascaliteBar : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999; 
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Yellow;
			Item.useStyle = ItemUseStyleID.Swing; 
			Item.useTurn = true; 
			Item.useAnimation = 15; 
			Item.useTime = 10; 
			Item.autoReuse = true; 
			Item.consumable = true; 
			Item.createTile = TileType<Tiles.PascaliteBar>(); 
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<PascaliteOre>(), 4); 
            recipe.AddTile(TileID.Hellforge); 
			recipe.Register();
		}
	}
}


