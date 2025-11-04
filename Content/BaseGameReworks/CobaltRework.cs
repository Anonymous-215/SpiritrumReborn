using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
    public class CobaltRework : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type != ItemID.CobaltRepeater)
            {
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
            }

            item.useAnimation = 18;
            item.useTime = 6;
            item.reuseDelay = 20;

            if (Main.myPlayer == player.whoAmI)
            {
                float spread = MathHelper.ToRadians(6f);
                for (int i = 0; i < 1; i++) 
                {
                    float factor = i / 2f;
                    float rotation = MathHelper.Lerp(-spread, spread, factor);
                    Vector2 shotVelocity = velocity.RotatedBy(rotation);
                    Projectile.NewProjectile(source, position, shotVelocity, type, damage, knockback, player.whoAmI);
                }
            }

            return false;
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OreReworkPlayer orePlayer = player.GetModPlayer<OreReworkPlayer>();

            if (item.type == ItemID.CobaltSword)
            {
                if (Main.rand.NextFloat() < 0.2f)
                {
                    orePlayer.cobaltMeleeSpeedTimer = 300;
                }

                orePlayer.cobaltHitCounter++;
                if (orePlayer.cobaltHitCounter >= 5)
                {
                    orePlayer.cobaltHitCounter = 0;
                    if (Main.myPlayer == player.whoAmI)
                    {
                        Projectile.NewProjectile(player.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.MonkStaffT3_AltShot, Math.Max(1, damageDone / 4), 0f, player.whoAmI);
                    }
                }
            }

            TryTriggerCobaltLightning(player, target, damageDone);

            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        internal static bool HasFullCobaltSet(Player player)
        {
            Item head = player.armor[0];
            Item body = player.armor[1];
            Item legs = player.armor[2];

            if (body.type != ItemID.CobaltBreastplate || legs.type != ItemID.CobaltLeggings)
            {
                return false;
            }

            return head.type == ItemID.CobaltHelmet || head.type == ItemID.CobaltMask || head.type == ItemID.CobaltHat;
        }

        internal static void TryTriggerCobaltLightning(Player player, NPC target, int damageDone)
        {
            if (!HasFullCobaltSet(player))
            {
                return;
            }

            OreReworkPlayer orePlayer = player.GetModPlayer<OreReworkPlayer>();
            if (orePlayer.cobaltLightningCooldown > 0)
            {
                return;
            }

            orePlayer.cobaltLightningCooldown = 30;
            if (Main.myPlayer != player.whoAmI)
            {
                return;
            }

            Vector2 direction = target.Center - player.Center;
            if (direction.LengthSquared() <= float.Epsilon)
            {
                direction = new Vector2(0f, -8f);
            }

            Vector2 velocity = Vector2.Normalize(direction) * 10f;
            Projectile.NewProjectile(player.GetSource_OnHit(target), player.Center, velocity, ProjectileID.ThunderStaffShot, Math.Max(1, damageDone * 2), 2f, player.whoAmI);
        }
    }

    public class CobaltReworkProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => false;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileID.CobaltNaginata && Main.myPlayer == projectile.owner)
            {
                Projectile.NewProjectile(projectile.GetSource_OnHit(target), projectile.Center, projectile.velocity * 0.8f, ProjectileID.CobaltNaginata, Math.Max(1, (int)(damageDone * 0.9f)), 0f, projectile.owner);
            }

            if (projectile.friendly && projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
            {
                Player owner = Main.player[projectile.owner];
                CobaltRework.TryTriggerCobaltLightning(owner, target, damageDone);
            }

            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}


