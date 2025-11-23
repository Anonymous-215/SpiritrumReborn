using SpiritrumReborn.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ButterflyWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 8);
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 30;
            Item.mana = 10;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<HolyButterfly>();
            Item.shootSpeed = 10f;
            Item.knockBack = 2f;
            Item.useTurn = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofLight, 12)
                .AddIngredient(ItemID.MonarchButterfly, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset() => new Vector2(6f, -4f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int amount = Main.rand.Next(3, 5);
            float speed = velocity.Length();
            for (int i = 0; i < amount; i++)
            {
                Vector2 perturbedDirection = velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(MathHelper.ToRadians(22.5f));
                float speedScale = Main.rand.NextFloat(0.85f, 1.2f);
                Vector2 perturbed = perturbedDirection * speed * speedScale;
                Projectile.NewProjectile(source, position, perturbed, ModContent.ProjectileType<HolyButterfly>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
