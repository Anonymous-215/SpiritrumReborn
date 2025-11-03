using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class PascaliteShellshocker : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 28;
			Item.value = Item.sellPrice(silver: 70);
			Item.rare = ItemRarityID.Orange;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 35;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item11;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.shoot = ModContent.ProjectileType<Content.Projectiles.CoralShot>();
			Item.shootSpeed = 18f;
			Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Content.Items.Materials.PascaliteBar>(), 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}

