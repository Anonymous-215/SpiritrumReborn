using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class AntlerRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 31;
            Item.crit = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 18;
			Item.height = 54;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
            Item.scale = 1.3f;
			Item.value = Item.buyPrice(gold: 4);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet; 
            Item.shoot = ProjectileID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 9f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 2); 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Musket, 1);
            recipe.AddIngredient(ItemID.AntlionMandible, 6); 
            recipe.AddIngredient(ItemID.Sandstone, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


