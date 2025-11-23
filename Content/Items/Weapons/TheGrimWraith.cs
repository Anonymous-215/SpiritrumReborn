using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class TheGrimWraith : ModItem
    {
        private bool _reverseSwing;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 15);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 150;
            Item.knockBack = 7f;
            Item.crit = 14;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ModContent.ProjectileType<global::SpiritrumReborn.Content.Projectiles.TheGrimWraith>();
            Item.shootSpeed = 0f; // handled by projectile
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<global::SpiritrumReborn.Content.Projectiles.TheGrimWraith>()] == 0;
        }

        public override void UseItemFrame(Player player)
        {
            if (player.itemAnimation <= 0)
                return;

            float progress = 1f - player.itemAnimation / (float)player.itemAnimationMax;
            float swingDirection = _reverseSwing ? -1f : 1f;

            if (player.direction == -1)
                swingDirection *= -1f;

            float startRadians = MathHelper.ToRadians(-110f);
            float rotation = startRadians + swingDirection * MathHelper.Pi * progress;
            player.itemRotation = rotation;

            float reach = MathHelper.Lerp(14f, 20f, progress);
            float verticalOffset = MathHelper.Lerp(-8f, 6f, progress);
            player.itemLocation = player.MountedCenter + new Vector2(player.direction * reach, verticalOffset);

            CreateSwingEffects(player, progress);

            if (player.itemAnimation == 1)
                _reverseSwing = !_reverseSwing;
        }

        public override void HoldItem(Player player)
        {
            if (Main.rand.NextBool(24))
            {
                int dustType = DustID.PurpleTorch;
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, dustType, 0f, -0.4f, 0, default, 1.1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (!Main.rand.NextBool(3))
                return;

            int dustType = DustID.PurpleTorch;
            Vector2 position = new Vector2(hitbox.X + Main.rand.Next(hitbox.Width), hitbox.Y + Main.rand.Next(hitbox.Height));
            Dust dust = Dust.NewDustDirect(position, 0, 0, dustType, 0f, 0f, 0, default, 1f);
            dust.noGravity = true;
            dust.velocity = player.DirectionTo(position) * 3f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int debuffTime = 180;
            target.AddBuff(BuffID.ShadowFlame, debuffTime);

            SpawnHitDust(target);
            SoundEngine.PlaySound(SoundID.Item103, target.position);
            TriggerVoidBurst(player, target, target.Center);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var lore = new TooltipLine(Mod, "TheGrimWraithLore", "A scythe forged from void-tinged metal; its strikes ignite voidfire upon the battlefield.")
            {
                OverrideColor = new Color(180, 100, 255)
            };
            tooltips.Add(lore);

            var effect = new TooltipLine(Mod, "TheGrimWraithEffect", "Inflicts Shadowflame and releases void shards that spread the blaze.")
            {
                OverrideColor = new Color(200, 130, 255)
            };
            tooltips.Add(effect);
        }

        private void CreateSwingEffects(Player player, float progress)
        {
            int dustType = DustID.PurpleTorch;
            int particleCount = 1 + (Main.rand.NextBool() ? 1 : 0);

            for (int i = 0; i < particleCount; i++)
            {
                Vector2 offsetDirection = Vector2.UnitX.RotatedBy(player.itemRotation);
                Vector2 offset = offsetDirection * Main.rand.NextFloat(6f, 16f);
                Vector2 spawnPosition = player.MountedCenter + offset + new Vector2(0f, Main.rand.NextFloat(-6f, 6f));

                Dust dust = Dust.NewDustDirect(spawnPosition, 2, 2, dustType, 0f, 0f, 100, default, MathHelper.Lerp(0.8f, 1.4f, progress));
                dust.noGravity = true;
                dust.velocity = offsetDirection * Main.rand.NextFloat(0.5f, 1.2f);
            }

            Lighting.AddLight(player.itemLocation, 0.35f * progress, 0f, 0.55f * progress);
        }

        private void SpawnHitDust(NPC target)
        {
            int dustType = DustID.PurpleTorch;

            for (int i = 0; i < 20; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(6f, 6f);
                Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType, velocity.X, velocity.Y, 0, default, 1.5f);
                dust.noGravity = true;
            }
        }

        private void TriggerVoidBurst(Player player, NPC source, Vector2 center)
        {
            if (Main.myPlayer != player.whoAmI)
                return;

            int projectileCount = 6;
            float burstSpeed = 9f;
            float damageMultiplier = 0.7f;
            int damage = (int)(player.GetWeaponDamage(Item) * damageMultiplier);

            for (int i = 0; i < projectileCount; i++)
            {
                float angle = MathHelper.TwoPi * i / projectileCount;
                Vector2 velocity = Vector2.UnitX.RotatedBy(angle) * burstSpeed;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), center, velocity, ModContent.ProjectileType<VoidExplosionProjectile>(), damage, Item.knockBack, player.whoAmI);
            }

            float debuffRadius = 140f;
            float radiusSquared = debuffRadius * debuffRadius;
            int debuffTime = 120;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || npc.friendly || npc.life <= 0 || npc.whoAmI == source.whoAmI)
                    continue;

                if (Vector2.DistanceSquared(npc.Center, center) <= radiusSquared)
                    npc.AddBuff(BuffID.ShadowFlame, debuffTime);
            }
        }
    }
}
