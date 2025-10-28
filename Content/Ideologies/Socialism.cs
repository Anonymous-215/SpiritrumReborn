using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class Socialism : ModItem
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
			player.statDefense -= 4; 
			player.endurance += 0.1f; 
			player.lifeRegen -= 2; 
			player.moveSpeed += 0.1f; 
			player.GetDamage(DamageClass.Generic) -= 0.05f; 


			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 4; 
					ally.lifeRegen += 2; 
					ally.GetDamage(DamageClass.Generic) += 0.05f; 
				}
			}
		}


		public override void AddRecipes()
		{
			Recipe adamantiteRecipe = CreateRecipe();
			adamantiteRecipe.AddIngredient(ItemID.SoulofFright, 10); 
			adamantiteRecipe.AddIngredient(ItemID.AdamantiteBar, 5); 
			adamantiteRecipe.AddTile(TileID.MythrilAnvil); 
			adamantiteRecipe.Register();

			Recipe titaniumRecipe = CreateRecipe();
			titaniumRecipe.AddIngredient(ItemID.SoulofFright, 10); 
			titaniumRecipe.AddIngredient(ItemID.TitaniumBar, 5); 
			titaniumRecipe.AddTile(TileID.MythrilAnvil); 
			titaniumRecipe.Register();
		}

	}
}

