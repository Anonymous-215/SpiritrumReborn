using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class RocketBackpack : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {                       //(int)(...) is required for it to work, since float cannot turn to int
            player.wingTimeMax = (int)(player.wingTimeMax + 60);  //increases flight time by 60 ticks (1 second)

            Item heldItem = player.HeldItem;
            if (heldItem != null && heldItem.damage > 0 && heldItem.DamageType == DamageClass.Ranged) //detects if weapon is ranged
            { //Detects if uses rockets if it is ranged
                int ammoType = heldItem.useAmmo;
                if (ammoType == AmmoID.Rocket)
                {
                    player.GetDamage(DamageClass.Ranged) += 0.12f; //if true, +12% ranged damage
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RocketBoots);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}


