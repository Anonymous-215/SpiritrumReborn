using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using SpiritrumReborn.Content.BaseGameReworks;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Players
{
    public class AdamantiteSetPlayer : ModPlayer
    {
        private const float DetectionRadiusPixels = 40f * 16f;
        private const int MaxNearbyNPCs = 8;
        private const int MaxEnergyOrbs = 5;

        public bool adamantiteSetEquipped = false;

        private bool wasUsingSword = false;

        public override void ResetEffects()
        {
            adamantiteSetEquipped = false;
        }

        public override void UpdateEquips()
        {
            bool hasSet = true;
            for (int i = 0; i < 3; i++)
            {
                var item = Player.armor[i];
                if (item == null || item.IsAir)
                {
                    hasSet = false;
                    break;
                }

                string name = Lang.GetItemNameValue(item.type);
                if (!name.Contains("Adamantite"))
                {
                    hasSet = false;
                    break;
                }
            }

            adamantiteSetEquipped = hasSet;

            if (!adamantiteSetEquipped)
            {
                return;
            }

            float radiusSq = DetectionRadiusPixels * DetectionRadiusPixels;
            int nearbyCount = 0;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || npc.friendly || npc.townNPC)
                {
                    continue;
                }

                if (npc.SpawnedFromStatue)
                {
                    continue;
                }

                if (!npc.CanBeChasedBy(Player, false))
                {
                    continue;
                }

                float distanceSq = Vector2.DistanceSquared(npc.Center, Player.Center);
                if (distanceSq <= radiusSq)
                {
                    nearbyCount++;
                }
            }

            int clampedNearby = Math.Min(nearbyCount, MaxNearbyNPCs);

            if (clampedNearby > 0)
            {
                Player.statDefense += 2 * clampedNearby;
                Player.GetKnockback(DamageClass.Generic) += 0.05f * clampedNearby;
            }
        }

        public override void PreUpdate()
        {
            bool usingSword = Player.itemAnimation > 0 && AdamantiteRework.IsAdamantiteSword(Player.HeldItem);
            bool previouslyUsingSword = wasUsingSword;

            if (usingSword)
            {
                if (Player.ItemAnimationJustStarted && Main.myPlayer == Player.whoAmI)
                {
                    SpawnEnergyOrb(Player.HeldItem);
                }
            }
            else if (previouslyUsingSword && Main.myPlayer == Player.whoAmI)
            {
                ReleaseEnergyOrbs();
            }

            wasUsingSword = usingSword;
        }

        private void SpawnEnergyOrb(Item sourceItem)
        {
            if (sourceItem == null || sourceItem.IsAir)
            {
                return;
            }

            var source = Player.GetSource_ItemUse(sourceItem);
            int projType = ModContent.ProjectileType<AdamantiteEnergyOrb>();

            int activeOrbs = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == Player.whoAmI && proj.type == projType && proj.ai[1] == 0f)
                {
                    activeOrbs++;
                    if (activeOrbs >= MaxEnergyOrbs)
                    {
                        return;
                    }
                }
            }
            int damage = Player.GetWeaponDamage(sourceItem);
            float knockback = Player.GetWeaponKnockback(sourceItem, sourceItem.knockBack);
            float angle = Main.rand.NextFloat(MathHelper.TwoPi);

            Projectile.NewProjectile(source, Player.Center, Vector2.Zero, projType, damage, knockback, Player.whoAmI, angle);
        }

        private void ReleaseEnergyOrbs()
        {
            Vector2 target = Main.MouseWorld;
            float speed = 18f;
            int projType = ModContent.ProjectileType<AdamantiteEnergyOrb>();

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (!proj.active || proj.owner != Player.whoAmI || proj.type != projType)
                {
                    continue;
                }

                if (proj.ai[1] != 0f)
                {
                    continue;
                }

                Vector2 direction = (target - proj.Center).SafeNormalize(Vector2.UnitX);
                proj.ai[1] = 1f;
                proj.velocity = direction * speed;
                proj.tileCollide = false;
                proj.friendly = true;
                proj.hostile = false;
                if (proj.damage <= 0)
                {
                    proj.damage = Math.Max(1, proj.originalDamage);
                }
                proj.netUpdate = true;
            }
        }
    }
}
