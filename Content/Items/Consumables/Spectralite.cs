using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Consumables
{
	public class Spectralite : ModItem
	{
		public override bool CanUseItem(Player player) //This detects if the player didn't consume Spectralite
		{
			return player.statManaMax == 200 && player.GetModPlayer<SpectralitePlayer>().spectraliteConsumed == 0; 
		}

		public override bool? UseItem(Player player) //This is simply the variables
		{
			player.GetModPlayer<SpectralitePlayer>().spectraliteMana = 100; 
			player.GetModPlayer<SpectralitePlayer>().spectraliteConsumed = 1; 


			return true; 
		}

		public override void SetDefaults() //Basic info
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 9999; 
			Item.value = Item.buyPrice(silver: 10); 
			Item.rare = ItemRarityID.Pink; 
			Item.useStyle = ItemUseStyleID.HoldUp; 
			Item.useAnimation = 20; 
			Item.useTime = 20; 
			Item.consumable = true; 
		}

		public override void AddRecipes() //Recipe
		{
			Recipe recipe = CreateRecipe(3); //Crafts multiple because it can also be used in some recipes
			recipe.AddIngredient(ItemID.Ectoplasm, 1); 
			recipe.AddIngredient(ItemID.HallowedBar, 1); 
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.Register();
		}
	}

	public class SpectralitePlayer : ModPlayer// Here is the player to make sure the player can have 300 base max mana and max 500 mana.
	// Yes it bypasses the 400 limit
	{
		public int spectraliteMana = 0; 
		public int spectraliteConsumed = 0; 

		public override void ResetEffects()
		{
			Player.statManaMax2 += spectraliteMana; 
		}


	}
}


