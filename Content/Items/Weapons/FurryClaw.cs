using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class FurryClaw : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 8;
			Item.DamageType = DamageClass.Melee;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1.5f;
			Item.scale = 1.1f;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Leather, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.LeadBar, 5);
            recipe2.AddIngredient(ItemID.Leather, 2);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}


