using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class CopiumBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.crit = 0;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
            Item.scale = 0.75f;
			Item.value = Item.buyPrice(silver: 40);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet; 
            Item.shoot = ProjectileID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CopiumBar>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}


