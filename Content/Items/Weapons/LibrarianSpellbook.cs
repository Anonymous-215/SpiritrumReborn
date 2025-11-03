using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class LibrarianSpellbook : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 34;
            Item.damage = 42;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.MagicWordProjectile>();
            Item.shootSpeed = 10f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.FallenStar, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
