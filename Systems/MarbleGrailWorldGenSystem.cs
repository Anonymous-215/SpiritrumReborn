using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Weapons;

namespace SpiritrumReborn.Systems
{
    public class MarbleGrailWorldGenSystem : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null || chest.item == null)
                    continue;

                Tile tile = Main.tile[chest.x, chest.y];
                if (tile != null && tile.TileType == TileID.Containers)
                {
                    int style = tile.TileFrameX / 36;
                    if (style == 51)
                    {
                        bool inMarbleCave = false;
                        int radius = 30; 
                        for (int x = chest.x - radius; x <= chest.x + radius && !inMarbleCave; x++)
                        {
                            for (int y = chest.y - radius; y <= chest.y + radius && !inMarbleCave; y++)
                            {
                                if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
                                {
                                    Tile nearbyTile = Main.tile[x, y];
                                    if (nearbyTile != null && nearbyTile.TileType == TileID.MarbleBlock)
                                    {
                                        inMarbleCave = true;
                                    }
                                }
                            }
                        }
                        if (inMarbleCave)
                        {
                            if (Main.rand.NextFloat() < 0.20f)
                            {
                                for (int slot = 0; slot < Chest.maxItems; slot++)
                                {
                                    if (chest.item[slot].type == ItemID.None)
                                    {
                                        chest.item[slot].SetDefaults(ModContent.ItemType<MarbleGrail>());
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


