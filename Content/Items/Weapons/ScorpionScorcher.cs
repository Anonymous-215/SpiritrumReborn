// This weapon needs a total rework


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ScorpionScorcher : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 4;
            Item.useAnimation = 16;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ScorpionBeam>();
            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 10);
            Item.reuseDelay = 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
