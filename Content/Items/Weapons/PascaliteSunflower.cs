using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;
using SpiritrumReborn.Content.Buffs;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class PascaliteSunflower : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PascaliteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
            Item.damage = 40;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item44;
            Item.buffType = ModContent.BuffType<PascaliteSunflowerBuff>();
            Item.shoot = ModContent.ProjectileType<PascaliteSunflowerSummon>();
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 3600);
            Projectile.NewProjectile(source, player.Center, Microsoft.Xna.Framework.Vector2.Zero, type, Item.damage, Item.knockBack, player.whoAmI);
            return false;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "PascaliteSunflower", "Summons a Pascalite Sunflower that orbits and fires homing orbs.") );
        }
    }
}
