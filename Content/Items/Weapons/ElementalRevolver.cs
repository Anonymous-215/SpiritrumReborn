using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ElementalRevolver : ModItem
    {
        private enum Element
        {
            Fire,
            Ice,
            Lightning
        }

        private static readonly string[] ElementNames = Enum.GetNames(typeof(Element));
        private static readonly Dictionary<Element, string> ElementDescriptions = new()
        {
            [Element.Fire] = "Fires a Flamelash",
            [Element.Ice] = "Fires a Frozo Flake",
            [Element.Lightning] = "Fires a Lightning Bolt"
        };

        private Element _currentElement;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverElement", $"Current Element: {ElementNames[(int)_currentElement]} (Right click to cycle)"));
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverEffect", ElementDescriptions[_currentElement]));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name.StartsWith("ElementalRevolver"))
                {
                    line.OverrideColor = new Color(255, 210, 120);
                }
            }
            tooltips.Add(new TooltipLine(Mod, "Tip1", "Fire mode is good against big crowds"));
            tooltips.Add(new TooltipLine(Mod, "Tip2", "Ice mode is good against small crowds"));
            tooltips.Add(new TooltipLine(Mod, "Tip3", "Lightning mode is only good in single target"));
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.scale = 0.5f;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 73;
            Item.knockBack = 3f;
            Item.crit = 8;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 14;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item41;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                _currentElement = (Element)(((int)_currentElement + 1) % ElementNames.Length);
                if (Main.myPlayer == player.whoAmI)
                {
                    Main.NewText($"Elemental Revolver: {ElementNames[(int)_currentElement]} mode", 255, 210, 120);
                }
                return true;
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            Vector2 direction = SafeDirection(velocity);
            Vector2 muzzlePosition = position + AdjustedMuzzleOffset(direction, 30f);
            float speed = velocity.Length();

            switch (_currentElement)
            {
                case Element.Fire:
                    FireFlamelash(source, player, muzzlePosition, speed, damage, knockback);
                    break;
                case Element.Ice:
                    FireFrozoFlake(source, player, muzzlePosition, direction * speed, damage, knockback);
                    break;
                case Element.Lightning:
                    FireHeatRay(source, player, muzzlePosition, direction * speed, damage, knockback);
                    break;
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver, 1)
                .AddIngredient(ItemID.HeatRay, 1)
                .AddIngredient(ItemID.Flamelash, 1)
                .AddIngredient(ModContent.ItemType<global::SpiritrumReborn.Content.Items.Materials.PascaliteBar>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 2);
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

        private static Vector2 AdjustedMuzzleOffset(Vector2 direction, float distance)
        {
            Vector2 offset = direction * distance;
            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                offset.Y *= 0.5f;
            }
            else
            {
                offset.X *= 0.5f;
            }

            return offset;
        }

        private static void FireFlamelash(EntitySource_ItemUse_WithAmmo source, Player player, Vector2 position, float speed, int damage, float knockback)
        {
            Vector2 targetDirection = SafeDirection(Main.MouseWorld - position) * speed;
            int projectileDamage = (int)(damage * 0.8f);
            Projectile.NewProjectile(source, position, targetDirection, ProjectileID.Flamelash, projectileDamage, knockback, player.whoAmI);
        }

        private static void FireFrozoFlake(EntitySource_ItemUse_WithAmmo source, Player player, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            int projectileDamage = (int)(damage * 1.2f);
            int projectileType = ModContent.ProjectileType<FrozoFlake>();
            int projectileIndex = Projectile.NewProjectile(source, position, velocity, projectileType, projectileDamage, knockback, player.whoAmI);
            if (projectileIndex >= 0)
            {
                Projectile projectile = Main.projectile[projectileIndex];
                projectile.friendly = true;
                projectile.hostile = false;
            }
        }

        private static void FireHeatRay(EntitySource_ItemUse_WithAmmo source, Player player, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            int projectileDamage = (int)(damage * 2.3f);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.HeatRay, projectileDamage, knockback, player.whoAmI);
        }
    }
}
