using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class BasicPolitics : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime; 
			Item.value = Item.buyPrice(gold: 20);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 5;
			player.moveSpeed += 0.15f;
			player.endurance += 0.1f; 
			player.lifeRegen += 1;
			player.GetCritChance(DamageClass.Generic) += 5;
			player.GetDamage(DamageClass.Generic) -= 0.12f;

			player.manaCost += 0.15f; 

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 3; 
					ally.lifeRegen += 2; 
					ally.GetDamage(DamageClass.Generic) += 0.05f; 
				}
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<Democracy>()); 
			recipe.AddIngredient(ItemType<Communism>()); 
			recipe.AddIngredient(ItemType<Fascism>()); 
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}

