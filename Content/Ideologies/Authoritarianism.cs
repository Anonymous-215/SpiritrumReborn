using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Authoritarianism : ModItem
    {
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
            player.statDefense += 12; 
            player.endurance += 0.10f; 
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.fireWalk = true;
            player.noKnockback = true;

            player.maxMinions += 2;

            player.GetDamage(DamageClass.Melee) -= 0.30f;
            player.GetDamage(DamageClass.Ranged) -= 0.30f;
            player.GetDamage(DamageClass.Magic) -= 0.30f;

            player.moveSpeed -= 0.20f; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddIngredient(ItemID.TitanGlove);
            recipe.AddIngredient(ItemID.ObsidianShield);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}


