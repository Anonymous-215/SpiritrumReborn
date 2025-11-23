using System;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class NoDeerClaw : ModItem
    {
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "NoDeerClawTooltip", "A vicious claw that spawns a piercing slash on hit."));
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = false;
            Item.noMelee = false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Bone);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 spawnPos = player.Center + new Vector2(player.direction * 20, 0);
            Vector2 vel = new Vector2(player.direction * 10f, 0);
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), spawnPos, vel, ModContent.ProjectileType<NoDeerClawProj>(), Math.Max(1, damageDone / 2), 2f, player.whoAmI);
        }
    }
}
