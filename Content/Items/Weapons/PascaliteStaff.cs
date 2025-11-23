using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Materials;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class PascaliteStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.LightRed;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 32;
            Item.mana = 10;
            Item.useTime = 5;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item20;
            Item.knockBack = 3f;
            Item.shoot = ModContent.ProjectileType<PascaliteOrb>();
            Item.shootSpeed = 8f;
            Item.autoReuse = true;
            Item.reuseDelay = 30;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int count = Main.rand.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                Vector2 perturbed = velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.7f, 1.05f);
                Projectile.NewProjectile(source, position, perturbed, type, damage, knockback, player.whoAmI);
            }
            return false; 
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "PascaliteStaff", "Fires a burst of homing Pascalite Orbs."));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PascaliteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
