using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
    public class OrichalcumRework : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type != ItemID.OrichalcumRepeater)
            {
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
            }

            if (Main.myPlayer == player.whoAmI)
            {
                float spread = MathHelper.ToRadians(4f);
                for (int i = 0; i < 2; i++)
                {
                    float factor = i / 1f;
                    float rotation = MathHelper.Lerp(-spread, spread, factor);
                    Vector2 shotVelocity = velocity.RotatedBy(rotation);
                    Projectile.NewProjectile(source, position, shotVelocity, type, damage, knockback, player.whoAmI);
                }
            }

            return false;
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!IsOrichalcumItem(item.type))
            {
                base.ModifyHitNPC(item, player, target, ref modifiers);
                return;
            }

            OrichalcumGlobalNPC state = target.GetGlobalNPC<OrichalcumGlobalNPC>();
            if (state.vulnStacks > 0)
            {
                modifiers.FinalDamage *= 1f + 0.05f * state.vulnStacks;
            }

            base.ModifyHitNPC(item, player, target, ref modifiers);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (item.type == ItemID.OrichalcumSword && item.DamageType == DamageClass.Melee)
            {
                int projectileType = ModContent.ProjectileType<Projectiles.OrichalcumFlowerPetal>();
                for (int i = 0; i < 3; i++)
                {
                    Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(24f, 24f);
                    Vector2 spawnPosition = target.Center + spawnOffset;
                    Vector2 direction = (target.Center - spawnPosition).SafeNormalize(Vector2.Zero);
                    float speed = 3.5f + Main.rand.NextFloat(0f, 2f);
                    Projectile.NewProjectile(player.GetSource_OnHit(target), spawnPosition, direction * speed, projectileType, Math.Max(1, (int)(damageDone * 0.2f)), 0f, player.whoAmI);
                }
            }

            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        internal static bool IsOrichalcumItem(int type)
        {
            return type == ItemID.OrichalcumSword || type == ItemID.OrichalcumHalberd || type == ItemID.OrichalcumRepeater;
        }
    }

    public class OrichalcumGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int vulnStacks;

        public override void SetDefaults(NPC npc)
        {
            vulnStacks = 0;
        }

        public override void OnKill(NPC npc)
        {
            vulnStacks = 0;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (vulnStacks <= 0)
            {
                return;
            }

            try
            {
                Vector2 worldPosition = npc.Center + new Vector2(0f, -npc.height * 1.1f - 8f);
                Vector2 screenPosition = worldPosition - screenPos;
                const int iconSize = 28;
                Rectangle iconRect = new Rectangle((int)screenPosition.X - iconSize / 2, (int)screenPosition.Y - iconSize / 2, iconSize, iconSize);

                Texture2D iconTexture = null;
                try
                {
                    iconTexture = ModContent.Request<Texture2D>("SpiritrumReborn/Content/BaseGameReworks/OricalchumCurse").Value;
                }
                catch
                {
                }

                if (iconTexture != null)
                {
                    spriteBatch.Draw(iconTexture, iconRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(TextureAssets.MagicPixel.Value, iconRect, Color.MediumPurple * 0.95f);
                }

                string text = vulnStacks.ToString();
                Vector2 textPosition = new Vector2(iconRect.Right + 6, iconRect.Y + (iconSize - FontAssets.MouseText.Value.LineSpacing) / 2f);
                Utils.DrawBorderString(spriteBatch, text, textPosition, Color.White);
            }
            catch
            {
            }
        }
    }

    public class OrichalcumGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool FromOrichalcumWeapon;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == ProjectileID.OrichalcumHalberd)
            {
                FromOrichalcumWeapon = true;
                return;
            }

            if (projectile.type == ModContent.ProjectileType<Projectiles.OrichalcumFlowerPetal>())
            {
                FromOrichalcumWeapon = true;
                return;
            }

            if (source is EntitySource_ItemUse itemSource)
            {
                FromOrichalcumWeapon = OrichalcumRework.IsOrichalcumItem(itemSource.Item.type);
            }
            else if (source is EntitySource_ItemUse_WithAmmo ammoSource)
            {
                FromOrichalcumWeapon = OrichalcumRework.IsOrichalcumItem(ammoSource.Item.type);
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!FromOrichalcumWeapon)
            {
                return;
            }

            OrichalcumGlobalNPC state = target.GetGlobalNPC<OrichalcumGlobalNPC>();
            if (state.vulnStacks > 0)
            {
                modifiers.FinalDamage *= 1f + 0.05f * state.vulnStacks;
            }

            if (projectile.type == ProjectileID.OrichalcumHalberd && state.vulnStacks >= 10)
            {
                modifiers.SetCrit();
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type != ProjectileID.OrichalcumHalberd)
            {
                return;
            }

            OrichalcumGlobalNPC state = target.GetGlobalNPC<OrichalcumGlobalNPC>();
            if (state.vulnStacks >= 10)
            {
                int projectileType = ModContent.ProjectileType<Projectiles.OrichalcumFlowerPetal>();
                for (int i = 0; i < 3; i++)
                {
                    Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(24f, 24f);
                    Vector2 spawnPosition = target.Center + spawnOffset;
                    Vector2 direction = (target.Center - spawnPosition).SafeNormalize(Vector2.Zero);
                    float speed = 3.5f + Main.rand.NextFloat(0f, 2f);
                    Projectile.NewProjectile(projectile.GetSource_OnHit(target), spawnPosition, direction * speed, projectileType, Math.Max(1, (int)(damageDone * 0.5f)), 0f, projectile.owner);
                }

                state.vulnStacks = 0;
            }
            else
            {
                state.vulnStacks = Math.Min(10, state.vulnStacks + 1);
            }
        }
    }
}
    
