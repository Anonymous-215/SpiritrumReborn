using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace SpiritrumReborn.Content.Systems
{
    public class EvilGunSwapSystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe musketToUndertaker = Recipe.Create(ItemID.TheUndertaker)
                .AddIngredient(ItemID.Musket)
                .AddTile(TileID.DemonAltar)
                .Register();

            Recipe undertakerToMusket = Recipe.Create(ItemID.Musket)
                .AddIngredient(ItemID.TheUndertaker)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}


