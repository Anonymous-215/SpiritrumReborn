using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Tiles
{
    public class FrozoniteOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;            
            var name = CreateMapEntryName();
            AddMapEntry(new Color(0, 144, 255), name);

            DustType = DustID.Stone;
            MinPick = 200;

        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail)
            {
                base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
            }
        }
    }
}


