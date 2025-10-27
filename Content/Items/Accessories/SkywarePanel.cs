using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class SkywarePanel : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true; 		
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic)  += 0.05f;
            player.statManaMax2 += 40;
            player.manaCost -= 0.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SunplateBlock, 35);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils); 
            recipe.Register();
        }
    }
}

