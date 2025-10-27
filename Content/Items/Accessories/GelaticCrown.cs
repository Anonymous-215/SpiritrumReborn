using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Accessories
{
	public class GelaticCrown : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28; 
			Item.height = 28; 
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink; 
			Item.value = Item.buyPrice(gold: 5); 
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += 0.1f; 
			player.jumpSpeedBoost += 1.5f; 
			player.noFallDmg = true; // I want this to be 40% fall damage taken in the end
		}
		public override void AddRecipes() // I added a temporary recipe for now
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Gel, 100); 
			recipe.AddIngredient(ItemID.GoldCrown);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
			Recipe recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.Gel, 100);
			recipe2.AddIngredient(ItemID.PlatinumCrown);
			recipe2.AddTile(TileID.Anvils);
			recipe2.Register();
		}
	}
}

