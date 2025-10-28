using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;
using SpiritrumReborn.Content.Items.Consumables;

namespace SpiritrumReborn.Content.Items.Ideology
{
	public class Anarchy : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.buyPrice(gold: 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 5; 
			player.endurance -= 0.05f; 
			player.GetDamage(DamageClass.Generic) += Main.rand.NextFloat(-0.2f, 0.5f); 
			player.moveSpeed += Main.rand.NextFloat(-0.1f, 0.3f); 
			player.GetCritChance(DamageClass.Generic) += Main.rand.Next(-15, 15); 
			player.GetAttackSpeed(DamageClass.Generic) += Main.rand.NextFloat(-0.3f, 0.4f); 

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense -= 3; 
					ally.GetDamage(DamageClass.Generic) += Main.rand.NextFloat(-0.05f, 0.15f); 
					ally.moveSpeed += Main.rand.NextFloat(-0.05f, 0.1f); 
				}
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<Spectralite>(), 20); 
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}

