using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class FrostBreaker : ModItem
    {
        private const int ShardCount = 3;
        private const float SpreadDegrees = 12f;
        private const float ShardSpeed = 12f;

        public override void SetDefaults()
        {
            Item.damage = 128;
            Item.DamageType = DamageClass.Melee;
            Item.width = 75;
            Item.height = 75;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 2f;
            Item.shoot = ModContent.ProjectileType<FrostBreakerShard>();
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-10f, 8f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 baseDirection = velocity.SafeNormalize(Vector2.UnitX);
            for (int i = 0; i < 3; i++)
            {
                float rotation = MathHelper.ToRadians(-12 + (12 * 2f / (3 - 1)) * i);
                Vector2 perturbed = baseDirection.RotatedBy(rotation) * 12f;
                Projectile.NewProjectile(source, position, perturbed, type, (int)(damage * 0.75f), knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlade)
                .AddIngredient(ItemID.FrostCore)
                .AddIngredient(ItemID.IceBlock, 50)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


