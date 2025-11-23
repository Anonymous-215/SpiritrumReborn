using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Entities.Hostile.Bosses
{
    [AutoloadBossHead]
    public class FrostEmperor : ModNPC
    {
        public static int ActiveAttacker = 0; 
        public static bool EmperorAtHalf; 
        public static bool EmpressAtHalf; 
        public static bool MergeTriggered; 

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4; 
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 50; 
            NPC.defense = 60; 
            int baseLife = 4000;
            NPC.lifeMax = baseLife;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 25); 
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1; 
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        private Player Target => Main.player[NPC.target];
        private int localTimer;
        private float orbitAngle; 
    private const float BASE_ORBIT_RADIUS = 140f; 
        private bool isAttacking;
        private int attackStep; 
        private int currentAttackIndex; 
        private Vector2 sharedCenter; 

        public override void AI()
        {
            int empressIndex = NPC.FindFirstNPC(NPCType<FrostEmpress>());
            NPC empressNPC = empressIndex != -1 ? Main.npc[empressIndex] : null;

            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
            {
                NPC.TargetClosest();
            }

            if (Target.dead)
            {
                NPC.velocity.Y -= 0.04f;
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
                return;
            }
            HandleHalfHP(empressNPC);
            if (MergeTriggered)
                return; 

            if (EmperorAtHalf)
            {
                NPC.dontTakeDamage = true;
                NPC.velocity *= 0.95f;
                return;
            }

            if (empressNPC != null && empressNPC.active)
                sharedCenter = empressNPC.Center;
            else
                sharedCenter = Target.Center;

            DoOrbitMovement(empressNPC);
            DoAttackCoordinator(empressNPC);
            localTimer++;
        }
        private void DoOrbitMovement(NPC empressNPC)
        {
            float radius = BASE_ORBIT_RADIUS;
            if (empressNPC != null && empressNPC.active)
            {
                orbitAngle += 0.025f;
                Vector2 targetPos = empressNPC.Center + new Vector2((float)Math.Cos(orbitAngle), (float)Math.Sin(orbitAngle)) * radius;
                Vector2 move = targetPos - NPC.Center;
                float speed = MathHelper.Clamp(move.Length() * 0.12f, 6f, 18f);
                NPC.velocity = move.SafeNormalize(Vector2.Zero) * speed;
            }
            else
            {
                orbitAngle += 0.02f;
                Vector2 targetPos = sharedCenter + new Vector2((float)Math.Cos(orbitAngle), (float)Math.Sin(orbitAngle)) * (radius * 0.8f);
                Vector2 move = targetPos - NPC.Center;
                float speed = MathHelper.Clamp(move.Length() * 0.1f, 4f, 14f);
                NPC.velocity = move.SafeNormalize(Vector2.Zero) * speed;
            }
        }

        private void DoAttackCoordinator(NPC empressNPC)
        {
            if (ActiveAttacker != 1)
            {
                isAttacking = false;
                NPC.dontTakeDamage = false; 
                if (ActiveAttacker == 0 && localTimer % 240 == 0) 
                {
                    ActiveAttacker = 1; 
                    currentAttackIndex = (currentAttackIndex + 1) % 3;
                    attackStep = 0;
                }
                return;
            }

            isAttacking = true;
            NPC.dontTakeDamage = true;
            attackStep++;
            switch (currentAttackIndex)
            {
                case 0: Attack_SnipingLances(); break;
                case 1: Attack_SpiralShards(); break;
                case 2: Attack_IceRainFocus(); break;
            }
            if (attackStep > 120) 
            {
                ActiveAttacker = 2; 
                attackStep = 0;
            }
        }

        private void Attack_SnipingLances()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (attackStep % 30 == 10)
            {
                Vector2 dir = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                Vector2 vel = dir * 18f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostBeam, NPC.damage / 2, 0f, Main.myPlayer);
            }
        }

        private void Attack_SpiralShards()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (attackStep % 12 == 0)
            {
                float baseAngle = attackStep * 0.25f;
                int count = 6;
                for (int i = 0; i < count; i++)
                {
                    Vector2 vel = new Vector2(9f, 0).RotatedBy(baseAngle + MathHelper.TwoPi / count * i);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostShard, NPC.damage / 3, 0f, Main.myPlayer);
                }
            }
        }

        private void Attack_IceRainFocus()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (attackStep % 15 == 0)
            {
                Vector2 spawn = Target.Center + new Vector2(Main.rand.Next(-200, 200), -500);
                Vector2 vel = new Vector2(0, 14f);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), spawn, vel, ProjectileID.IceSpike, NPC.damage / 3, 0f, Main.myPlayer);
            }
        }

        private void HandleHalfHP(NPC empressNPC)
        {
            if (!EmperorAtHalf && NPC.life <= NPC.lifeMax / 2)
            {
                EmperorAtHalf = true;
                NPC.life = NPC.lifeMax / 2; 
            }
            if (empressNPC != null && empressNPC.active && EmpressAtHalf && EmperorAtHalf && !MergeTriggered)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    MergeTriggered = true;
                    Vector2 mergePos = (NPC.Center + empressNPC.Center) * 0.5f;
                    int id = NPC.NewNPC(NPC.GetSource_FromAI(), (int)mergePos.X, (int)mergePos.Y, NPCType<FrostEmpire>());
                    if (id != Main.maxNPCs)
                    {
                        Main.npc[id].life = Main.npc[id].lifeMax;
                    }
                    NPC.active = false;
                    empressNPC.active = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[Type])
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool CheckDead()
        {
            if (!EmperorAtHalf)
            {
                NPC.life = NPC.lifeMax / 2 + 1;
                return false;
            }
            return true;
        }
    }
}


