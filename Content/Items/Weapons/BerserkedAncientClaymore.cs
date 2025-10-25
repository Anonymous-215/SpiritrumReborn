﻿using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class BerserkedAncientClaymore : ModItem //Previously Ancient Katana
	{

		public override void SetDefaults()
		{
			// Has been reworked to be a very slow, but really hard hitting and big weapon
			Item.damage = 105; // more damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 10;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10; // more knockback
			Item.scale = 2.0f; //even bigger
			Item.value = Item.buyPrice(gold: 15);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
		}
		
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AncientClaymore>(), 1);
			// will have a special item here
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}

