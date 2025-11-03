using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class NinjaTextbook : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Magic;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 13;
			Item.useAnimation = 13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(gold: 6);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<SlimyKunai>();
			Item.shootSpeed = 18f;
			Item.mana = 6;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProjectiles = 1;
			for (int i = 0; i < numProjectiles; i++)
			{
				Vector2 perturbedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(source, position, perturbedVelocity, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
	}
}


