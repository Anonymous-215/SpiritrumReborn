using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class CopiumSpelltome : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "CopiumSpelltomeTooltip", "Casts a spread of Copium energy bolts that swirl into sparks while alive."));
        }

        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CopiumEnergy>();
            Item.shootSpeed = 10f;
            Item.ArmorPenetration = 999999;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int boltCount = 4;
            if (boltCount <= 0)
            {
                return false;
            }
            Vector2 baseDirection = velocity.SafeNormalize(Vector2.UnitY);
            float spacing = boltCount == 1 ? 0f : 16f / (boltCount - 1);
            for (int i = 0; i < boltCount; i++)
            {
                float offsetDegrees = -8f + spacing * i;
                Vector2 perturbed = baseDirection.RotatedBy(MathHelper.ToRadians(offsetDegrees)) * velocity.Length() * 0.9f;
                Projectile.NewProjectile(source, position, perturbed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CopiumBar>(), 6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
