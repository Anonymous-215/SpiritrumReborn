using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class SocialDemocracy : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 10);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.lifeRegen -= 1;
            player.GetDamage(DamageClass.Generic) += 0.06f;
            player.endurance -= 0.02f;

            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.lifeRegen += 1; 
                    ally.endurance += 0.02f; 
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddIngredient(ItemID.CrossNecklace);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}


