using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; 

namespace SpiritrumReborn.Content.Items.Materials
{
    public class FrozoniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25; 
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 25); 
            Item.rare = ItemRarityID.Cyan; 
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrozoniteOre>(), 4); 
            recipe.AddTile(TileID.Furnaces); 
            recipe.Register();
        }
    }
}


