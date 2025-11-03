using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace SpiritrumReborn.Content.Tiles
{
    public class CopiumBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileShine[Type] = 400;
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200), Terraria.Localization.Language.GetText("Copium Bar"));
            DustType = DustID.Platinum;
            RegisterItemDrop(ModContent.ItemType<Items.Materials.CopiumBar>());
        }
    }
}
