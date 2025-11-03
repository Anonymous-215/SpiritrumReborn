using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; 
using SpiritrumReborn.Content.Tiles; 
using System.Collections.Generic; 

namespace SpiritrumReborn.Content.Items.Materials
{
    public class PascaliteOre : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100; 
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58; 
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PascaliteOre>());
            Item.width = 12; 
            Item.height = 12; 
            Item.value = 3000; 
            Item.rare = ItemRarityID.Yellow; 
            Item.createTile = ModContent.TileType<Tiles.PascaliteOre>();
        }
    }
}


