using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
    public class MythrilRework : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (item.type == ItemID.MythrilHalberd)
            {
                float extraCritChancePercent = target.defense * 0.5f;
                if (Main.rand.NextFloat(0f, 100f) < extraCritChancePercent)
                {
                    modifiers.SetCrit();
                }
            }

            base.ModifyHitNPC(item, player, target, ref modifiers);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (item.type == ItemID.MythrilSword)
            {
                int projectileType = ModContent.ProjectileType<Projectiles.MythrilRicochet>();
                for (int i = 0; i < 3; i++)
                {
                    Vector2 direction = (target.Center - player.Center).SafeNormalize(Vector2.Zero);
                    Vector2 randomOffset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0f, 3f);
                    Vector2 velocity = direction * (6f + Main.rand.NextFloat(0f, 4f)) + randomOffset;
                    Projectile.NewProjectile(player.GetSource_OnHit(target), player.Center, velocity, projectileType, Math.Max(1, (int)(damageDone * 0.7f)), 0f, player.whoAmI);
                }
            }

            if (item.type == ItemID.MythrilHalberd && hit.Crit)
            {
                MythrilReworkGlobalNPC npcState = target.GetGlobalNPC<MythrilReworkGlobalNPC>();
                if (npcState.sunderStacks < 10)
                {
                    npcState.sunderStacks++;
                }

                npcState.sunderTimer = 600;
            }

            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type != ItemID.MythrilRepeater)
            {
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
            }

            if (Main.myPlayer == player.whoAmI)
            {
                int projectileIndex = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                if (projectileIndex >= 0 && projectileIndex < Main.maxProjectiles)
                {
                    Main.projectile[projectileIndex].GetGlobalProjectile<MythrilReworkProjectile>().FromRepeater = true;
                }
            }

            return false;
        }
    }

    public class MythrilReworkGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int sunderStacks;
        public int sunderTimer;
        public bool mythrilCurse;

        public override void ResetEffects(NPC npc)
        {
            mythrilCurse = false;
        }

        public override void AI(NPC npc)
        {
            if (sunderTimer > 0)
            {
                sunderTimer--;
                if (sunderTimer <= 0)
                {
                    sunderStacks = 0;
                }
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (mythrilCurse)
            {
                modifiers.Defense -= (int)(npc.defense * 0.5f);
                modifiers.FinalDamage *= 1.1f;
            }

            if (sunderStacks > 0)
            {
                modifiers.FinalDamage *= 1f + 0.01f * sunderStacks;
            }
        }
    }

    public class MythrilReworkProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool FromRepeater;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (FromRepeater && projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
            {
                target.AddBuff(ModContent.BuffType<Debuffs.MythrilCurse>(), 480);
            }

            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
