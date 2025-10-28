using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class Conservatism : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow; 
			Item.value = Item.buyPrice(gold: 15);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 5; 
			player.endurance -= 0.10f; 
			player.lifeRegen += 3; 
			player.GetDamage(DamageClass.Generic) += 0.1f; 
			player.moveSpeed += 0.05f; 
			player.GetCritChance(DamageClass.Generic) += 5; 
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofMight, 10); 
			recipe.AddIngredient(ItemID.CobaltBar, 5); 
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.Register();

			Recipe palladiumRecipe = CreateRecipe();
			palladiumRecipe.AddIngredient(ItemID.SoulofMight, 10); 
			palladiumRecipe.AddIngredient(ItemID.PalladiumBar, 5); 
			palladiumRecipe.AddTile(TileID.MythrilAnvil); 
			palladiumRecipe.Register();
		}
	}
}

