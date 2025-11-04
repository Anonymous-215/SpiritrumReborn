using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.BaseGameReworks
{
    public class PalladiumRework : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type == ItemID.PalladiumRepeater)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    int projectileIndex = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                    if (projectileIndex >= 0 && projectileIndex < Main.maxProjectiles)
                    {
                        Main.projectile[projectileIndex].GetGlobalProjectile<PalladiumReworkProjectile>().FromRepeater = true;
                    }
                }

                return false;
            }

            if (item.type == ItemID.PalladiumPike)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    int projectileIndex = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                    if (projectileIndex >= 0 && projectileIndex < Main.maxProjectiles)
                    {
                        Main.projectile[projectileIndex].GetGlobalProjectile<PalladiumReworkProjectile>().FromPalladiumSpear = true;
                    }
                }

                return false;
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (item.type == ItemID.PalladiumSword)
            {
                int heal = 2;
                player.statLife = Math.Min(player.statLife + heal, player.statLifeMax2);
                player.HealEffect(heal, true);
            }

            base.OnHitNPC(item, player, target, hit, damageDone);
        }
    }

    public class PalladiumReworkProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool FromRepeater;
        public bool FromPalladiumSpear;

        private float previousMovementFactor;
        private bool spawnedFollowThrough;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == ProjectileID.PalladiumPike)
            {
                FromPalladiumSpear = true;
            }

            if (!FromPalladiumSpear && source is EntitySource_ItemUse itemSource && itemSource.Item.type == ItemID.PalladiumPike)
            {
                FromPalladiumSpear = true;
            }
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.aiStyle == ProjAIStyleID.Spear && !FromPalladiumSpear)
            {
                if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
                {
                    Item held = Main.player[projectile.owner].HeldItem;
                    if (held.type == ItemID.PalladiumPike)
                    {
                        FromPalladiumSpear = true;
                    }
                }
            }

            if (projectile.aiStyle == ProjAIStyleID.Spear && FromPalladiumSpear)
            {
                float movementFactor = projectile.ai[0];
                if (!spawnedFollowThrough && previousMovementFactor != 0f && movementFactor < previousMovementFactor)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Player owner = Main.player[projectile.owner];
                        if (owner?.active == true)
                        {
                            int type = ModContent.ProjectileType<Content.Projectiles.PalladiumShockwave>();
                            int damage = Math.Max(1, (int)(projectile.damage * 0.5f));
                            Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem), owner.Center, Vector2.Zero, type, damage, 4f, owner.whoAmI);
                        }
                    }

                    spawnedFollowThrough = true;
                }

                previousMovementFactor = movementFactor;
            }

            base.AI(projectile);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.aiStyle == ProjAIStyleID.Spear && FromPalladiumSpear && Main.myPlayer == projectile.owner)
            {
                int type = ModContent.ProjectileType<Content.Projectiles.PalladiumShockwave>();
                int damage = Math.Max(1, (int)(damageDone * 0.2f));
                Projectile.NewProjectile(projectile.GetSource_OnHit(target), target.Center, Vector2.Zero, type, damage, projectile.knockBack * 0.5f, projectile.owner);
            }

            if (FromRepeater && projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
            {
                Player owner = Main.player[projectile.owner];
                if (owner?.active == true)
                {
                    const float maxDistance = 8f * 16f;
                    if (Vector2.Distance(owner.Center, target.Center) <= maxDistance)
                    {
                        int heal = Math.Max(1, (int)(damageDone * 0.075f));
                        owner.statLife = Math.Min(owner.statLife + heal, owner.statLifeMax2);
                        owner.HealEffect(heal, true);
                    }
                }
            }

            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
