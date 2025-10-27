using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class EggRepeater : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
            Item.scale = 1f;
			Item.value = Item.buyPrice(silver: 5);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.useAmmo = ModContent.ItemType<Content.Items.Ammo.Egg>();
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.Egged>();
            Item.noMelee = true;
            Item.shootSpeed = 9f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 12)
                .AddIngredient(ItemID.Cobweb, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbed = velocity.RotatedByRandom(MathHelper.ToRadians(6));
            Projectile.NewProjectile(source, position, perturbed, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}


