using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Materials
{
    public class Penumbrium : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Content.Tiles.Penumbrium>();
        }
    }
}
