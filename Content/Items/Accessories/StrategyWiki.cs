using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Accessories;

namespace SpiritrumReborn.Content.Items.Accessories
{
	public class StrategyWiki : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Pink;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxTurrets += 3;
			player.GetDamage(DamageClass.Summon) += 0.05f; 
			player.GetAttackSpeed(DamageClass.Summon) += 0.15f; 
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ApprenticeScarf);
			recipe.AddIngredient(ItemID.SquireShield);
			recipe.AddIngredient(ItemID.MonkBelt);
			recipe.AddIngredient(ItemID.HuntressBuckler);
			recipe.AddIngredient(ModContent.ItemType<StrategyWiki>(), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}


