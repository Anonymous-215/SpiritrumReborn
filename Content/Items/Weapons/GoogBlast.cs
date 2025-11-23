using SpiritrumReborn.Content.Items.Materials;
using SpiritrumReborn.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class GoogBlast : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GoogBlast_Projectile>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.None;
            Item.UseSound = SoundID.Item11;
            Item.scale = 1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Googling>(), 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
