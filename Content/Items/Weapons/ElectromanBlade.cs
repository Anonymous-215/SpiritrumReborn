using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ElectromanBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30; 
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 18; 
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f; 
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow; 
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; 
            Item.noUseGraphic = false; 
            Item.noMelee = false; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

