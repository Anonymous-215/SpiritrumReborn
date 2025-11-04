using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;
using System;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class PocketFairies : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Magic;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 5;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(gold: 6);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<PocketFairy>();
			Item.shootSpeed = 12f;
			Item.mana = 8;
            Item.scale = 0f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProjectiles = 2; 
			for (int i = 0; i < numProjectiles; i++)
			{
				Vector2 perturbedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10)); 
				Projectile.NewProjectile(source, position, perturbedVelocity, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PixieDust, 40)
                .AddTile(TileID.Anvils)
                .Register();
        }
	}
}




