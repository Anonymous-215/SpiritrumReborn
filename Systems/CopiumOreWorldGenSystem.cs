using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Systems
{
    public class CopiumOreWorldGenSystem : ModSystem
    {
        public override void PostWorldGen()
        {
            int amount = (int)((Main.maxTilesX * Main.maxTilesY) * 0.0005f); 
            for (int i = 0; i < amount; i++)
            {
                int attempts = 0;
                bool placed = false;
                while (attempts < 8 && !placed)
                {
                    attempts++;
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200); 

                    bool validHost = false;
                    int checkRadius = 3;
                    int hostX = x;
                    int hostY = y;
                    for (int ox = -checkRadius; ox <= checkRadius && !validHost; ox++)
                    {
                        for (int oy = -checkRadius; oy <= checkRadius; oy++)
                        {
                            int tx = x + ox;
                            int ty = y + oy;
                            if (tx < 0 || tx >= Main.maxTilesX || ty < 0 || ty >= Main.maxTilesY) continue;
                            ushort t = Main.tile[tx, ty].TileType;
                            if (t == TileID.Dirt || t == TileID.Stone || t == TileID.ClayBlock || t == TileID.Mud)
                            {
                                validHost = true;
                                hostX = tx;
                                hostY = ty;
                                break;
                            }
                        }
                    }

                    if (!validHost) continue;

                    int oreType = ModContent.TileType<Content.Tiles.CopiumOre>();
                    WorldGen.TileRunner(hostX, hostY, WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 6), oreType);

                    int cleanupRadius = 8;
                    for (int cx = hostX - cleanupRadius; cx <= hostX + cleanupRadius; cx++)
                    {
                        for (int cy = hostY - cleanupRadius; cy <= hostY + cleanupRadius; cy++)
                        {
                            if (cx < 0 || cx >= Main.maxTilesX || cy < 0 || cy >= Main.maxTilesY) continue;
                            Tile tile = Main.tile[cx, cy];
                            if (tile == null) continue;
                            if (!tile.HasTile || tile.TileType != oreType) continue;

                            bool adjacentValid = false;
                            int[] nx = new int[] { cx - 1, cx + 1, cx, cx };
                            int[] ny = new int[] { cy, cy, cy - 1, cy + 1 };
                            for (int n = 0; n < 4; n++)
                            {
                                int tx = nx[n];
                                int ty = ny[n];
                                if (tx < 0 || tx >= Main.maxTilesX || ty < 0 || ty >= Main.maxTilesY) continue;
                                Tile nt = Main.tile[tx, ty];
                                if (nt == null || !nt.HasTile) continue;
                                ushort ttype = nt.TileType;
                                if (ttype == TileID.Dirt || ttype == TileID.Stone || ttype == TileID.ClayBlock || ttype == TileID.Mud)
                                {
                                    adjacentValid = true;
                                    break;
                                }
                            }

                            if (!adjacentValid)
                            {
                                tile.ClearTile();
                            }
                        }
                    }

                    WorldGen.SquareTileFrame(hostX - cleanupRadius, hostY - cleanupRadius, true);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, hostX, hostY, cleanupRadius * 2 + 1);
                    }

                    placed = true;
                }
            }
        }
    }
}


