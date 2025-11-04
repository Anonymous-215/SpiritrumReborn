using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class AncientClaymore : ModItem 
	{

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 10;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.scale = 1.8f;
			Item.value = Item.buyPrice(gold: 10);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
		}
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<ObscurioniteAlloy>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}


