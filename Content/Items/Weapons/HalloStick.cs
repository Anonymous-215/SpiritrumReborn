using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class HalloStick : ModItem
	{
		public override void SetStaticDefaults() { }
        

		public override void SetDefaults()
        {
			Item.damage = 13;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.knockBack = 1.5f;
            Item.scale = 1f;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item21;
            Item.autoReuse = true;
            Item.mana = 9;
            Item.shoot = ModContent.ProjectileType<Projectiles.HalloBall>(); 
            Item.noMelee = true;
            Item.shootSpeed = 40f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProjectiles = Main.rand.Next(4, 7); // this is the amount of projectiles per shot (4 to 7 here)
			for (int i = 0; i < numProjectiles; i++)
			{
				Vector2 perturbedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(180)); // This is the degrees of spread (max of 180 degrees here)
				Projectile.NewProjectile(source, position, perturbedVelocity, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddIngredient(ItemID.LeafWand);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
