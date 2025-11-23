using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SpiritrumReborn.Content.Buffs;

namespace SpiritrumReborn.Content.Projectiles
{
    public class ElementalBladeProjectile : ModProjectile
    {
        private enum AIState
        {
            Idle = 0,
            Dashing = 1,
            Stabbing = 2,
            Spinning = 3,
            Returning = 4
        }

        private AIState currentState = AIState.Idle;
        private int targetNPC = -1;
        private int stateTimer = 0;
        private Vector2 dashStartPos;
        private Vector2 idleOffset;
        private bool hasStabbed = false;
        private const float MAX_DETECT_RANGE = 1000f; 
        private const float DASH_SPEED = 25f;
        private const float IDLE_SPEED = 8f;
        private const float RETURN_SPEED = 16f;
        private const int STAB_DURATION = 8;
        private const float WING_OFFSET_DISTANCE = 60f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            
            Main.projPet[Projectile.type] = true; 
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;         
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;           
            Projectile.timeLeft = 18000;         
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.light = 0.5f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12; 
        }

    private static Texture2D cachedTexture;
    private static Vector2 cachedOrigin;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.HasBuff(ModContent.BuffType<ElementalBlades>()))
            {
                Projectile.Kill();
                return;
            }
            player.AddBuff(ModContent.BuffType<ElementalBlades>(), 2);
            if (Projectile.ai[0] == 0f)
            {
                Vector2 playerPos = player.Center;
                Vector2 behindPlayer = playerPos + new Vector2(-60f * player.direction, -20f);
                Projectile.Center = behindPlayer;
                Projectile.ai[0] = 1f; 
                currentState = AIState.Idle;
                CalculateIdlePosition(player);
                Projectile.netUpdate = true;
            }

            stateTimer++;

            switch (currentState)
            {
                case AIState.Idle:
                    HandleIdleState(player);
                    break;
                case AIState.Dashing:
                    HandleDashingState(player);
                    break;
                case AIState.Stabbing:
                    HandleStabbingState(player);
                    break;
                case AIState.Spinning:
                    HandleSpinningState(player);
                    break;
                case AIState.Returning:
                    HandleReturningState(player);
                    break;
            }

            CreateDustEffects();
        }

        private void HandleIdleState(Player player)
        {
            Projectile.ai[1] = -1;
            
            NPC target = FindNearestTarget(player);
            if (target != null)
            {
                targetNPC = target.whoAmI;
                Projectile.ai[1] = targetNPC; 
                currentState = AIState.Dashing;
                stateTimer = 0;
                dashStartPos = Projectile.Center;
                hasStabbed = false;
                Projectile.netUpdate = true;
                return;
            }

            Vector2 targetPos = player.Center + idleOffset;
            Vector2 direction = targetPos - Projectile.Center;
            
            if (direction.Length() > 200f)
            {
                Projectile.Center = targetPos;
            }
            else
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * 0.1f, 0.2f);
                if (direction.Length() < 10f)
                {
                    Projectile.velocity *= 0.9f;
                }
            }

            Projectile.rotation = MathHelper.ToRadians(player.direction == 1 ? 45f : 135f);
        }

        private void HandleDashingState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            if (Vector2.Distance(target.Center, player.Center) > MAX_DETECT_RANGE)
            {
                ReturnToIdle(player);
                return;
            }

            Projectile.ai[1] = targetNPC;

            Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = direction * DASH_SPEED;
            Projectile.rotation = direction.ToRotation();

            if (Vector2.Distance(Projectile.Center, target.Center) < 50f)
            {
                currentState = AIState.Stabbing;
                stateTimer = 0;
                Projectile.ResetLocalNPCHitImmunity(); 
                Projectile.netUpdate = true;
            }
        }

        private void HandleStabbingState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            Projectile.ai[1] = targetNPC;
            
            Projectile.velocity *= 0.8f; 
            
            if (stateTimer >= STAB_DURATION)
            {
                currentState = AIState.Spinning;
                stateTimer = 0;
                hasStabbed = true;
                Projectile.netUpdate = true;
            }
        }

        private void HandleSpinningState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            if (Vector2.Distance(target.Center, player.Center) > MAX_DETECT_RANGE)
            {
                ReturnToIdle(player);
                return;
            }

            Projectile.ai[1] = targetNPC;

            Vector2 targetCenter = target.Center;
            float orbitRadius = 40f;
            float orbitSpeed = 0.15f;
            float angle = stateTimer * orbitSpeed;
            
            Vector2 orbitPos = targetCenter + new Vector2(
                (float)Math.Cos(angle) * orbitRadius,
                (float)Math.Sin(angle) * orbitRadius
            );
            
            Vector2 direction = (orbitPos - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = direction * 12f;
            
            Projectile.rotation += 0.3f;
        }

        private void HandleReturningState(Player player)
        {
            Projectile.ai[1] = -1;
            
            Vector2 targetPos = player.Center + idleOffset;
            Vector2 direction = targetPos - Projectile.Center;
            
            if (direction.Length() < 30f)
            {
                currentState = AIState.Idle;
                stateTimer = 0;
                Projectile.netUpdate = true;
                return;
            }
            
            Projectile.velocity = direction.SafeNormalize(Vector2.Zero) * RETURN_SPEED;
            Projectile.rotation = direction.ToRotation();
        }

        private NPC FindNearestTarget(Player player)
        {
            var occupiedTargets = GetOccupiedTargets();
            
            NPC closestNPC = null;
            float closestDistance = MAX_DETECT_RANGE;
            
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy(this))
                {
                    float distanceToPlayer = Vector2.Distance(npc.Center, player.Center);
                    float distanceToProjectile = Vector2.Distance(npc.Center, Projectile.Center);
                    
                    if (distanceToPlayer < MAX_DETECT_RANGE && distanceToProjectile < closestDistance)
                    {
                        if (!occupiedTargets.Contains(i))
                        {
                            closestDistance = distanceToProjectile;
                            closestNPC = npc;
                        }
                    }
                }
            }
            
            if (closestNPC == null)
            {
                closestDistance = MAX_DETECT_RANGE;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.CanBeChasedBy(this))
                    {
                        float distanceToPlayer = Vector2.Distance(npc.Center, player.Center);
                        float distanceToProjectile = Vector2.Distance(npc.Center, Projectile.Center);
                        
                        if (distanceToPlayer < MAX_DETECT_RANGE && distanceToProjectile < closestDistance)
                        {
                            closestDistance = distanceToProjectile;
                            closestNPC = npc;
                        }
                    }
                }
            }
            
            return closestNPC;
        }

        private System.Collections.Generic.HashSet<int> GetOccupiedTargets()
        {
            var occupiedTargets = new System.Collections.Generic.HashSet<int>();
            
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && 
                    proj.type == Projectile.type && 
                    proj.owner == Projectile.owner && 
                    proj.identity != Projectile.identity) 
                {
                    int otherTarget = (int)proj.ai[1];
                    if (otherTarget >= 0 && otherTarget < Main.maxNPCs && Main.npc[otherTarget].active)
                    {
                        occupiedTargets.Add(otherTarget);
                    }
                }
            }
            
            return occupiedTargets;
        }

        private void ReturnToIdle(Player player)
        {
            currentState = AIState.Returning;
            stateTimer = 0;
            targetNPC = -1;
            Projectile.ai[1] = -1; 
            CalculateIdlePosition(player);
            Projectile.netUpdate = true;
        }

        private void CalculateIdlePosition(Player player)
        {
            int swordIndex = 0;
            int totalSwords = 0;
            
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == Projectile.type && proj.owner == Projectile.owner)
                {
                    totalSwords++;
                    if (proj.identity < Projectile.identity)
                        swordIndex++;
                }
            }
            
            float wingSpread = WING_OFFSET_DISTANCE;
            float verticalOffset = -30f;
            
            if (totalSwords == 1)
            {
                idleOffset = new Vector2(-wingSpread * player.direction, verticalOffset);
            }
            else
            {
                float angleStep = MathHelper.PiOver4 / Math.Max(1, totalSwords - 1);
                float angle = -MathHelper.PiOver4 / 2f + angleStep * swordIndex;
                
                idleOffset = new Vector2(
                    (float)Math.Cos(angle) * wingSpread * -player.direction,
                    verticalOffset + (float)Math.Sin(angle) * 20f
                );
            }
        }

        private void CreateDustEffects()
        {
            if (Main.rand.NextBool(currentState == AIState.Spinning ? 2 : 4))
            {
                int dustType = Main.rand.NextBool() ? DustID.Torch : DustID.IceTorch;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 
                    dustType, 0f, 0f, 100, default, 0.8f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (cachedTexture == null)
            {
                cachedTexture = ModContent.Request<Texture2D>(Texture).Value;
                cachedOrigin = new Vector2(cachedTexture.Width * 0.5f, cachedTexture.Height * 0.5f);
            }

            Vector2 drawOrigin = cachedOrigin;
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin;
            int trailLen = ProjectileID.Sets.TrailCacheLength[Projectile.type];

            for (int i = 0; i < trailLen; i++)
            {
                float trailOpacity = 0.8f * ((float)(trailLen - i) / trailLen);
                Vector2 trailPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                Color trailColor = Projectile.GetAlpha(lightColor) * trailOpacity;

                Main.spriteBatch.Draw(cachedTexture, trailPos, null, trailColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            Color elementColor = Color.Lerp(Color.OrangeRed, Color.SkyBlue, (float)Math.Sin(Main.GameUpdateCount * 0.05f) * 0.5f + 0.5f);
            Main.spriteBatch.Draw(cachedTexture, drawPos, null, Projectile.GetAlpha(elementColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire3, 180); 
            }
            else
            {
                target.AddBuff(BuffID.Frostburn2, 180); 
            }
        }
    }
}


