using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class CalamityRevolver : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 424;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 30;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<CalamityLaser>();
            Item.shootSpeed = 26f;
            Item.scale = 0.88f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-2f, 0f);

        // Allow right-click alternate use
        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<CalamityRevolverPlayer>();
            if (player.altFunctionUse == 2)
            {
                if (modPlayer.spinCooldown <= 0 && modPlayer.spinCharges == 0)
                {
                    Item.useTime = 20;
                    Item.useAnimation = 20;
                    Item.UseSound = SoundID.Item1;
                    return true;
                }

                return false;
            }
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.UseSound = SoundID.Item91;
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var modPlayer = player.GetModPlayer<CalamityRevolverPlayer>();

            // Right-click activation: spawn spin projectile, set charges and cooldown
            if (player.altFunctionUse == 2)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    int projType = ModContent.ProjectileType<CalamityRevolverSpin>();
                    Projectile.NewProjectile(source, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
                }

                modPlayer.spinCharges = 6;
                modPlayer.spinCooldown = 30 * 60; // 30 seconds
                return false; // don't fire normal projectile
            }

            if (modPlayer.spinCharges > 0)
            {
                modPlayer.spinCharges--;
                int empoweredDamage = 600;
                int proj = Projectile.NewProjectile(source, position, velocity, type, empoweredDamage, knockback, player.whoAmI);
                if (proj >= 0 && proj < Main.maxProjectiles)
                {
                    Projectile p = Main.projectile[proj];
                    // Make sure projectile is friendly magic and behaves as expected
                    p.DamageType = DamageClass.Magic;
                }

                // enforce fast use time for this attack by reducing itemTime/animation (client-side immediate effect)
                if (player.whoAmI == Main.myPlayer)
                {
                    player.itemTime = 20;
                    player.itemAnimation = 20;
                }

                return false; // we've manually spawned the projectile
            }

            // Default behavior: shoot normally with configured projectile
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
        }
    }
}
