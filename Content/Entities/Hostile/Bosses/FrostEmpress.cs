using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Entities.Hostile.Bosses
{
    [AutoloadBossHead]
    public class FrostEmpress : ModNPC
    {
        public override void OnSpawn(IEntitySource source)
        {
            if (!Content.Entities.Hostile.Bosses.FrostEmperor.MergeTriggered)
            {
                FrostEmperor.ActiveAttacker = 0;
            }
        }
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
            NPC.damage = 60; 
            NPC.defense = 55; 
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
    private float orbitAngle = MathHelper.Pi; 
    private const float BASE_ORBIT_RADIUS = 140f; 
    private bool isAttacking;
    private int attackStep;
    private int currentAttackIndex;
    private Vector2 sharedCenter;
    private Vector2 rushDirection;

        public override void AI()
        {
            int emperorIndex = NPC.FindFirstNPC(NPCType<FrostEmperor>());
            NPC emperorNPC = emperorIndex != -1 ? Main.npc[emperorIndex] : null;

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

            HandleHalfHP(emperorNPC);
            if (FrostEmperor.MergeTriggered) return;
            if (FrostEmperor.EmpressAtHalf)
            {
                NPC.dontTakeDamage = true;
                NPC.velocity *= 0.95f;
                return;
            }

            sharedCenter = Target.Center;

            DoOrbitMovement(emperorNPC);
            DoAttackCoordinator(emperorNPC);
            localTimer++;
        }
        private void DoOrbitMovement(NPC emperorNPC)
        {
            Vector2 targetPos = sharedCenter;
            float speed = 8f;
            Vector2 move = targetPos - NPC.Center;
            if (move.Length() > 10f)
                NPC.velocity = move.SafeNormalize(Vector2.Zero) * speed;
            else
                NPC.velocity *= 0.9f;
        }

        private void DoAttackCoordinator(NPC emperorNPC)
        {
            if (FrostEmperor.ActiveAttacker != 2)
            {
                isAttacking = false;
                NPC.dontTakeDamage = false;
                if (FrostEmperor.ActiveAttacker == 0 && localTimer % 240 == 120)
                {
                    FrostEmperor.ActiveAttacker = 2; 
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
                case 0: Attack_DashCombo(); break;
                case 1: Attack_FanWaves(); break;
                case 2: Attack_ShatteringRush(); break;
            }
            if (attackStep > 120)
            {
                FrostEmperor.ActiveAttacker = 0; 
                attackStep = 0;
            }
        }

        private void Attack_DashCombo()
        {
            if (attackStep % 40 == 5)
            {
                rushDirection = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                SoundEngine.PlaySound(SoundID.Item60, NPC.Center);
            }
            if (attackStep % 40 > 10 && attackStep % 40 < 30)
            {
                NPC.velocity = rushDirection * 28f;
                if (Main.netMode != NetmodeID.MultiplayerClient && attackStep % 8 == 0)
                {
                    Vector2 side = new Vector2(-rushDirection.Y, rushDirection.X) * Main.rand.NextFloat(-3f, 3f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, rushDirection * 4f + side, ProjectileID.FrostWave, NPC.damage / 3, 0f, Main.myPlayer);
                }
            }
        }

        private void Attack_FanWaves()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (attackStep % 12 == 0) 
            {
                Vector2 dir = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                int count = 5; 
                float spread = MathHelper.ToRadians(60);
                for (int i = 0; i < count; i++)
                {
                    float t = i / (float)(count - 1) - 0.5f;
                    Vector2 vel = dir.RotatedBy(spread * t) * 10f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostWave, NPC.damage / 3, 0f, Main.myPlayer);
                }
            }
        }

        private void Attack_ShatteringRush()
        {
            if (attackStep < 25)
            {
                Vector2 toPlayer = (Target.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = Vector2.Lerp(NPC.velocity, toPlayer * 10f, 0.15f);
                if (attackStep == 10)
                    SoundEngine.PlaySound(SoundID.Item71, NPC.Center);
            }
            else if (attackStep == 25)
            {
                rushDirection = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            }
            else if (attackStep < 65)
            {
                NPC.velocity = rushDirection * 36f;
                if (Main.netMode != NetmodeID.MultiplayerClient && attackStep % 6 == 0)
                {
                    Vector2 side = new Vector2(-rushDirection.Y, rushDirection.X) * Main.rand.NextFloat(-4f, 4f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, rushDirection * 6f + side, ProjectileID.FrostWave, NPC.damage / 4, 0f, Main.myPlayer);
                }
            }
            else if (attackStep == 65)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 18; i++)
                    {
                        Vector2 vel = new Vector2(12f, 0).RotatedBy(MathHelper.TwoPi / 18 * i);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostShard, NPC.damage / 4, 0f, Main.myPlayer);
                    }
                }
            }
        }

        private void HandleHalfHP(NPC emperorNPC)
        {
            if (!FrostEmperor.EmpressAtHalf && NPC.life <= NPC.lifeMax / 2)
            {
                FrostEmperor.EmpressAtHalf = true;
                NPC.life = NPC.lifeMax / 2;
            }
            if (emperorNPC != null && emperorNPC.active && FrostEmperor.EmpressAtHalf && FrostEmperor.EmperorAtHalf && !FrostEmperor.MergeTriggered)
            {
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (isAttacking)
            {
                target.AddBuff(BuffID.Chilled, 180);
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
            if (!FrostEmperor.EmpressAtHalf)
            {
                NPC.life = NPC.lifeMax / 2 + 1;
                return false;
            }
            return true;
        }
    }
}


