using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class EchoingLancer : ModItem
    {
        private const int BurstCount = 3;
        private const float AmmoSaveChance = 0.6f;

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 34;
            Item.height = 60;
            Item.useTime = 15;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(gold: 60);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EchoingLancerArrow>();
            Item.shootSpeed = 22f;
            Item.useAmmo = AmmoID.Arrow;
            Item.reuseDelay = 14;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int projectileType = ModContent.ProjectileType<EchoingLancerArrow>();
            for (int i = 0; i < BurstCount; i++)
            {
                int proj = Projectile.NewProjectile(source, position, velocity, projectileType, damage, knockback, player.whoAmI);
                if (proj >= 0)
                {
                    Main.projectile[proj].timeLeft -= i * 2;
                }
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= AmmoSaveChance;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-4f, 0f);
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "EchoingLancerTip1", "Fires a burst of 3 spectral arrows per shot"));
            tooltips.Add(new TooltipLine(Mod, "EchoingLancerTip2", "60% chance to not consume arrows"));
        }
    }
}


