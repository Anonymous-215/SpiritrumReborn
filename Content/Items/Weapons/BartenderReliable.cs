using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class BartenderReliable : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 54;
            Item.height = 20;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BeerStream>();
            Item.shootSpeed = 18f;
            Item.useAmmo = ItemID.Ale;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-8f, 0f);

        public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= 0.2f;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 baseDirection = SafeDirection(velocity);
            Vector2 muzzlePosition = position + baseDirection * 24f;
            if (Collision.CanHit(position, 0, 0, muzzlePosition, 0, 0))
            {
                position = muzzlePosition;
            }

            float speed = velocity.Length();
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedDirection = baseDirection.RotatedByRandom(MathHelper.ToRadians(3f));
                float speedScale = Main.rand.NextFloat(0.9f, 1.1f);
                Vector2 perturbedVelocity = perturbedDirection * speed * speedScale;
                Projectile.NewProjectile(source, position, perturbedVelocity, ModContent.ProjectileType<BeerStream>(), damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.AddRange(new[]
            {
                new TooltipLine(Mod, "Tip1", "A pistol powered by beer"),
                new TooltipLine(Mod, "Tip2", "Uses Ale as ammo and inflicts Tipsy"),
                new TooltipLine(Mod, "Tip3", "Tipsy now increases DoT from poisoned and Venom"),
                new TooltipLine(Mod, "Tip4", "Has a chance to give a penalty to health regen")
            });
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IllegalGunParts)
                .AddIngredient(ItemID.AleThrowingGlove)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        private static Vector2 SafeDirection(Vector2 velocity)
        {
            if (velocity.LengthSquared() < 0.0001f)
            {
                return Vector2.UnitX;
            }

            velocity.Normalize();
            return velocity;
        }
    }
}