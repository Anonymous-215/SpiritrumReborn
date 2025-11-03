using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace SpiritrumReborn.Content.Tiles
{
    public class PascaliteOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileOreFinderPriority[Type] = 200;
            TileID.Sets.Ore[Type] = true;
            AddMapEntry(new Color(78, 204, 95), Language.GetText("Pascalite Ore"));
            DustType = DustID.Silver;
            RegisterItemDrop(ModContent.ItemType<Items.Materials.PascaliteOre>());
            MinPick = 160;
        }
    }
}
