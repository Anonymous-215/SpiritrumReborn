using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; 
using SpiritrumReborn.Content.Tiles; 
using System.Collections.Generic; 

namespace SpiritrumReborn.Content.Items.Materials
{
    public class FrozoniteOre : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100; 
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58; 
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.FrozoniteOre>());
            Item.width = 12; 
            Item.height = 12; 
            Item.value = 3000; 
            Item.rare = ItemRarityID.Cyan; 
            Item.createTile = ModContent.TileType<Tiles.FrozoniteOre>();
        }
    }
}


