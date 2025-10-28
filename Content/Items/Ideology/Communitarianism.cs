using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Communitarianism : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 30;
            player.lifeRegen += 2;
            player.GetDamage(DamageClass.Generic) -= 0.15f; 

            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 60f)
                {
                    ally.statDefense += 2; 
                    ally.lifeRegen += 1;   
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddIngredient(ItemID.PanicNecklace);
            recipe.AddIngredient(ItemID.HoneyComb);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}


