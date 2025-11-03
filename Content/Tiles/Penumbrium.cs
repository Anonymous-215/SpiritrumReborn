using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace SpiritrumReborn.Content.Tiles
{
    public class Penumbrium : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileOreFinderPriority[Type] = 400;
            TileID.Sets.Ore[Type] = true;
            AddMapEntry(new Color(120, 40, 180), Language.GetText("Penumbrium"));
            DustType = DustID.PurpleTorch;
            RegisterItemDrop(ModContent.ItemType<Items.Materials.Penumbrium>());
            MinPick = 160;
        }
    }
}
