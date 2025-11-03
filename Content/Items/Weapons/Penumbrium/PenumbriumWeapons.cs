using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons.Penumbrium
{

    public class PenumbralRailgun : ModItem
    {
        public override string Texture => "SpiritrumReborn/Content/Items/Weapons/Penumbrium/PenumbralRailgun";
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 54;
            Item.height = 18;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.EnergySphere>();
            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Content.Projectiles.EnergySphere>(), damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset() => new(-10, 0);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class PenumbralSpreader : ModItem
    {
        public override string Texture => "SpiritrumReborn/Content/Items/Weapons/Penumbrium/PenumbralSpreader";
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.width = 54;
            Item.height = 18;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.ShadowFlame;
            Item.shootSpeed = 11f;
            Item.mana = 11;
        }
        public override Vector2? HoldoutOffset() => new(-10, 0);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class NocturnalLance : ModItem
    {
        public override string Texture => "SpiritrumReborn/Content/Items/Weapons/Penumbrium/NocturnalLance";
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 2);
            Item.damage = 115;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 10f;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 55f;
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.NocturnalLance>();
            Item.noMelee = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity *= 3f;
            var proj = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Content.Projectiles.NocturnalLance>(), damage, knockback, player.whoAmI);
            if (proj != null) proj.scale = 2f;
            return false;
        }
        public override Vector2? HoldoutOffset() => new(-2, 0);
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SkullBow", "Shoots high power arrows") { OverrideColor = new Color(255, 255, 255) });
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}