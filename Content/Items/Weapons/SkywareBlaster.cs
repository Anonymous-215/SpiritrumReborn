using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class SkywareBlaster : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Magic;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(gold: 8);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item91;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Content.Projectiles.SunLaser>();
			Item.shootSpeed = 22f;
			Item.mana = 7;

		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SunplateBlock, 20);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

	}
}


