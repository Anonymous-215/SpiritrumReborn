using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class Communism : ModItem
	{


		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(gold: 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 1; 
			player.endurance += 0.03f; 
			player.lifeRegen += 1; 
			player.GetDamage(DamageClass.Generic) -= 0.1f; 

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 60f)
				{
					ally.statDefense += 1; 
					ally.lifeRegen += 1; 
				}
			}
		}


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
	}
}
}

