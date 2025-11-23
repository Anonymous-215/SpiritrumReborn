using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Ammo.Bolts;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class Bolter : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 18;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.ExplosiveBullet;
            Item.shootSpeed = 18f;
            Item.useAmmo = ModContent.ItemType<Bolt>();
            Item.scale = 1f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8f, 0f);
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 1.2f, type, (int)(damage * 1.2f), knockback * 1.2f, player.whoAmI);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "BolterTip1", "Consumes Bolts as ammunition"));
            tooltips.Add(new TooltipLine(Mod, "BolterTip2", "Very effective against armored targets and crowds"));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentVortex, 12)
                .AddIngredient(ItemID.MeteoriteBar, 10)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.MythrilBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.FragmentVortex, 12)
                .AddIngredient(ItemID.MeteoriteBar, 10)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.OrichalcumBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
