using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons 
{
    public class GoogBlaster : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 47;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7f;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.scale = 1f;
            Item.shoot = ModContent.ProjectileType<GoogBlast_Projectile>();
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GoogBlast>())
                .AddIngredient(ModContent.ItemType<CopiumBar>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
