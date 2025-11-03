using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Tiles;

namespace SpiritrumReborn.Content.Items.Materials
{
    public class CopiumBar : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.CopiumBar>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CopiumOre>(), 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}