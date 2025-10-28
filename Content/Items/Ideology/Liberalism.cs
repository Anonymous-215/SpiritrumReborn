using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class Liberalism : ModItem
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
			player.statDefense += 4; 
			player.moveSpeed += 0.1f; 
			player.GetDamage(DamageClass.Generic) -= 0.05f; 
			player.endurance += 0.05f; 
			player.lifeRegen += 1; 
			player.GetCritChance(DamageClass.Generic) -= 4; 

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 1; 
					ally.moveSpeed += 0.05f; 
					ally.GetDamage(DamageClass.Generic) += 0.02f; 
					ally.GetCritChance(DamageClass.Generic) += 1; 
				}
			}
		}

		public override void AddRecipes()
		{
			Recipe mythrilRecipe = CreateRecipe();
			mythrilRecipe.AddIngredient(ItemID.SoulofSight, 10); 
			mythrilRecipe.AddIngredient(ItemID.MythrilBar, 5); 
			mythrilRecipe.AddTile(TileID.MythrilAnvil); 
			mythrilRecipe.Register();

			Recipe orichalcumRecipe = CreateRecipe();
			orichalcumRecipe.AddIngredient(ItemID.SoulofSight, 10); 
			orichalcumRecipe.AddIngredient(ItemID.OrichalcumBar, 5); 
			orichalcumRecipe.AddTile(TileID.MythrilAnvil); 
			orichalcumRecipe.Register();
		}
	}
}

