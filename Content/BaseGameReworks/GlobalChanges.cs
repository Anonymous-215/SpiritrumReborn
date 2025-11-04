using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
    public class GlobalChanges : GlobalItem
    {
        private static readonly HashSet<int> SwordIds = new() { ItemID.CobaltSword, ItemID.PalladiumSword, ItemID.MythrilSword, ItemID.OrichalcumSword, ItemID.AdamantiteSword, ItemID.TitaniumSword };
        private static readonly HashSet<int> SpearIds = new() { ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident };

        public override bool InstancePerEntity => true;

        public bool isReworkedSword;
        public bool isReworkedSpear;

        public override void SetDefaults(Item item)
        {
            if (SwordIds.Contains(item.type))
            {
                isReworkedSword = true;
                item.damage = Math.Max(1, (int)Math.Round(item.damage * 1.2f));
                item.scale *= 1.35f;
            }

            if (SpearIds.Contains(item.type))
            {
                isReworkedSpear = true;
            }
        }
    }

    public class GlobalSpearProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.aiStyle != ProjAIStyleID.Spear)
            {
                return;
            }

            if (source is not EntitySource_ItemUse itemSource)
            {
                return;
            }

            Item origin = itemSource.Item;
            if (origin == null)
            {
                return;
            }

            GlobalChanges global = origin.GetGlobalItem<GlobalChanges>();
            if (!global.isReworkedSpear)
            {
                return;
            }

            const float scaleMultiplier = 1.4f;
            projectile.scale *= scaleMultiplier;

            int newWidth = (int)Math.Round(projectile.width * scaleMultiplier);
            int newHeight = (int)Math.Round(projectile.height * scaleMultiplier);
            if (newWidth > 0)
            {
                projectile.width = newWidth;
            }

            if (newHeight > 0)
            {
                projectile.height = newHeight;
            }

            projectile.velocity *= 1.55f;
        }
    }

    public class OreReworkPlayer : ModPlayer
    {
        public int cobaltHitCounter;
        public int cobaltLightningCooldown;
        public int cobaltMeleeSpeedTimer;

        public override void ResetEffects()
        {
            if (cobaltMeleeSpeedTimer > 0)
            {
                Player.GetAttackSpeed(DamageClass.Melee) += 0.33f;
                cobaltMeleeSpeedTimer--;
            }

            if (cobaltLightningCooldown > 0)
            {
                cobaltLightningCooldown--;
            }
        }
    }
}