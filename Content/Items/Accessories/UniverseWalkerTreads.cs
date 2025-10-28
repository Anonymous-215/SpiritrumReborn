using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class UniverseWalkerTreads : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 1.25f; 
            player.jumpSpeedBoost += 1.0f; 
            player.maxRunSpeed += 0.25f;
            player.waterWalk = true;
            player.lavaImmune = true;
            player.iceSkate = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RealmWalkerBoots>());
            recipe.AddIngredient(ItemID.LunarBar, 10); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}


