using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class RossRifle : ModItem
    {
        public override void SetDefaults()
        {
           Item.damage = 40;
            Item.crit = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 18;
			Item.height = 54;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6.25f;
            Item.scale = 1.1f;
			Item.value = Item.buyPrice(gold: 6);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet; 
            Item.shoot = ProjectileID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 11f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-21, 2); 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AntlerRifle>(), 1);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemType<AntlerRifle>(), 1);
            recipe2.AddIngredient(ItemID.TissueSample, 10);
            recipe2.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}


