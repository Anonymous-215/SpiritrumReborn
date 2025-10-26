using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Accessories
{

    public class DragonheartInsignia : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 20);
        }
        public override void UpdateEquip(Player player)
        {
            player.wingTime = float.MaxValue;
            player.moveSpeed += 1.5f;
            player.runAcceleration *= 2f;
            player.runSlowdown *= 1f;
            player.jumpSpeedBoost += 1.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EmpressFlightBooster);
            recipe.AddIngredient(ItemID.BetsyWings);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}