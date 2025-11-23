using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace SpiritrumReborn.Systems
{
    public class PascaliteGenerationSystem : ModSystem
    {

    public void GeneratePascaliteOre()
        {
            int amount = (int)((Main.maxTilesX * Main.maxTilesY) * 0.00008f); 
            for (int i = 0; i < amount; i++)
            {
                bool placed = false;
                for (int attempt = 0; attempt < 50; attempt++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 400); 

                    bool nearJungle = false;
                    int searchRadius = 60; 
                    for (int dy = -searchRadius; dy <= searchRadius; dy++)
                    {
                        int yy = y + dy;
                        if (yy < 10 || yy >= Main.maxTilesY - 10) continue;
                        Tile tile = Main.tile[x, yy];
                        if (tile != null && tile.HasTile && tile.TileType == TileID.JungleGrass)
                        {
                            nearJungle = true;
                            break;
                        }
                    }

                    if (nearJungle)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(1, 5), WorldGen.genRand.Next(1, 4), ModContent.TileType<Content.Tiles.PascaliteOre>());
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 400);
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(1, 5), WorldGen.genRand.Next(1, 4), ModContent.TileType<Content.Tiles.PascaliteOre>());
                }
            }
        }
    }
}


