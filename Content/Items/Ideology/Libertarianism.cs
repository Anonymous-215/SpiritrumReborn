using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Libertarianism : ModItem
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
            player.moveSpeed += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f; 
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.statDefense -= 6;
            player.GetDamage(DamageClass.Melee) -= 0.05f; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FeralClaws);
            recipe.AddIngredient(ItemID.Aglet);
            recipe.AddIngredient(ItemID.HermesBoots);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}


