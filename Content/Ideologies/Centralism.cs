using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Centralism : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Cyan; 
            Item.value = Item.buyPrice(gold: 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 10;
            player.endurance += 0.10f; 
            player.lifeRegen += 3;
            player.GetDamage(DamageClass.Generic) += 0.10f; 
            player.GetCritChance(DamageClass.Generic) += 6; 

            if (player.velocity.Length() > 0.1f)
            {
                player.moveSpeed -= 0.15f; 
            }

            float allyRadius = 30f * 16f;
            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < allyRadius)
                {
                    ally.statDefense += 2;
                    ally.lifeRegen += 1;
                    ally.GetDamage(DamageClass.Generic) += 0.03f; 
                    ally.GetCritChance(DamageClass.Generic) += 3; 
                    ally.moveSpeed += 0.03f; 
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Socialism>(), 1); 
            recipe.AddIngredient(ModContent.ItemType<Conservatism>(), 1); 
            recipe.AddIngredient(ModContent.ItemType<Liberalism>(), 1); 
            recipe.AddTile(TileID.LunarCraftingStation); 
            recipe.Register();
        }
    }
}


