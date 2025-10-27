using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class RealmWalkerBoots : ModItem
    {

    public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.12f; 
            player.jumpSpeedBoost += 1.0f; 
            player.accRunSpeed += 0.10f; 
            player.waterWalk = true;
            player.lavaImmune = true;
            player.iceSkate = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TerrasparkBoots); 
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddIngredient(ItemID.SoulofNight);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}


// In case some mods are enabled, there will be a recipe override