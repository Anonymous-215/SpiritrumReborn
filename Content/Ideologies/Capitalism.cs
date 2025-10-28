using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Capitalism : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.discountAvailable = true; 
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 4;
            player.moveSpeed -= 0.10f; 
            player.lifeRegen -= 2; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldCoin, 25);
            recipe.AddIngredient(ItemID.Diamond, 2);
            recipe.AddIngredient(ItemID.GreedyRing);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}


