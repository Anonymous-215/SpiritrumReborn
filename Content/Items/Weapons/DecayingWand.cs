using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class DecayingWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 58;
            Item.mana = 12;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<DeathBug>();
            Item.shootSpeed = 9f;
            Item.knockBack = 3f;
            Item.useTurn = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofNight, 12)
                .AddIngredient(ItemID.Stinkbug, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset() => new Vector2(8f, -6f);
    }
}
