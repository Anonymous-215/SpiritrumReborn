using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Technocracy : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            
            player.GetDamage(DamageClass.Magic) += 0.08f;
            
            player.manaCost += 0.08f;
            
            player.pickSpeed -= 0.05f;
            
            if (!hideVisual && Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(
                    player.position,
                    player.width,
                    player.height,
                    DustID.Electric,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddIngredient(ItemID.Cog, 5);
            recipe.AddIngredient(ItemID.Ruby, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


