using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class CommanderManual : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width     = 28;
            Item.height    = 28;
            Item.accessory = true; 		
            Item.value     = Item.sellPrice(gold: 1);
            Item.rare      = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxTurrets += 2;
            player.GetDamage(DamageClass.Summon) += 0.08f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DefenderMedal, 50);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.Anvils); 
            recipe.Register();
        }
    }
}

