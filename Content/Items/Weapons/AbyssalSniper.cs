using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class AbyssalSniper : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 400;
            Item.crit = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 18;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3;
            Item.scale = 1f;
			Item.value = Item.buyPrice(gold: 20);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AbyssalBallProjectile>();
            Item.noMelee = true;
            Item.shootSpeed = 55f;
        

        }

        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Content.Items.Materials.VoidEssence>(), 12);
            recipe.AddIngredient(ItemID.SniperRifle, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}


