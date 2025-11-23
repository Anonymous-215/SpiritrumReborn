using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using SpiritrumReborn.Content.Items.Weapons;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Entities.Hostile.Bosses
{
    public class VoidHarbinger : ModNPC
    {
        private const int MaxLife = 75000;
        private const int PhaseTransition = 37500; 
        
        private enum BossPhase { Awakening, PhaseOne, Transition, PhaseTwo, Enrage }
        private enum AttackType { VoidRift, ShadowClones, DimensionalTear, VoidStorm, RealityBreak, VoidNova }
        
        private BossPhase currentPhase = BossPhase.Awakening;
        private AttackType currentAttack = AttackType.VoidRift;
        private int phaseTimer = 0;
        private int attackTimer = 0;
        private int patternIndex = 0;
        private bool isChanneling = false;
        private Vector2 anchorPoint = Vector2.Zero;
    private List<int> activeMinions = new List<int>(8);
        private float voidPower = 0f; 
        private int consecutiveHits = 0;
        private bool inVulnerableState = false;
        
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = 1,
                Scale = 1.1f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 140;
            NPC.damage = 80;
            NPC.defense = 50;
            NPC.lifeMax = MaxLife;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = Item.buyPrice(0, 30, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.npcSlots = 15f;
            NPC.lavaImmune = true;
            NPC.alpha = 50;
            
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                NPC.buffImmune[i] = true;
            }
            
            NPC.buffImmune[BuffID.Ichor] = false;
            NPC.buffImmune[BuffID.CursedInferno] = false;
            
            Banner = 0;
            BannerItem = 0;
            if (!Main.dedServ)
            {
                Music = MusicID.Boss5;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("A cosmic entity from the void between realities. This harbinger of destruction tears through dimensions, wielding powers that bend space and time. Its presence alone warps the fabric of existence.")
            });
        }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; 
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            
            if (!target.active || target.dead)
            {
                NPC.TargetClosest(false);
                if (!target.active || target.dead)
                {
                    NPC.velocity.Y -= 0.1f;
                    if (NPC.timeLeft > 20)
                        NPC.timeLeft = 20;
                    return;
                }
            }

            UpdatePhase();
            
            CreateVisualEffects();
            
            switch (currentPhase)
            {
                case BossPhase.Awakening:
                    AwakeningPhase(target);
                    break;
                case BossPhase.PhaseOne:
                    PhaseOneAI(target);
                    break;
                case BossPhase.Transition:
                    TransitionPhase(target);
                    break;
                case BossPhase.PhaseTwo:
                    PhaseTwoAI(target);
                    break;
                case BossPhase.Enrage:
                    EnragePhase(target);
                    break;
            }

            phaseTimer++;
            attackTimer++;
            
            voidPower = Math.Min(voidPower + 0.02f, 100f);
            
            NPC.direction = NPC.spriteDirection = NPC.Center.X < target.Center.X ? 1 : -1;
        }

        private void UpdatePhase()
        {
            float healthPercent = (float)NPC.life / NPC.lifeMax;
            
            if (currentPhase == BossPhase.Awakening && phaseTimer > 120)
            {
                currentPhase = BossPhase.PhaseOne;
                phaseTimer = 0;
            }
            else if (currentPhase == BossPhase.PhaseOne && healthPercent <= 0.5f)
            {
                currentPhase = BossPhase.Transition;
                phaseTimer = 0;
                isChanneling = true;
            }
            else if (currentPhase == BossPhase.Transition && phaseTimer > 300)
            {
                currentPhase = BossPhase.PhaseTwo;
                phaseTimer = 0;
                isChanneling = false;
            }
            else if (currentPhase == BossPhase.PhaseTwo && healthPercent <= 0.15f)
            {
                currentPhase = BossPhase.Enrage;
                phaseTimer = 0;
            }
        }

        private void CreateVisualEffects()
        {
            int dustRate = currentPhase >= BossPhase.PhaseTwo ? 2 : 4;
            if (Main.rand.NextBool(dustRate))
            {
                int dustType = currentPhase == BossPhase.Enrage ? DustID.RedTorch : DustID.PurpleTorch;
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType, 0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }

            float intensity = currentPhase >= BossPhase.PhaseTwo ? 0.8f : 0.6f;
            Color lightColor = currentPhase == BossPhase.Enrage ? Color.Red : Color.Purple;
            Lighting.AddLight(NPC.Center, lightColor.R / 255f * intensity, lightColor.G / 255f * intensity, lightColor.B / 255f * intensity);
        }

        private void AwakeningPhase(Player target)
        {
            NPC.velocity *= 0.95f;
            
            if (phaseTimer % 20 == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(60, 60);
                    Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, Vector2.Zero, 150, default, 2f);
                }
                SoundEngine.PlaySound(SoundID.Item8 with { Volume = 0.3f }, NPC.Center);
            }
        }

        private void PhaseOneAI(Player target)
        {
            if (attackTimer >= GetAttackDuration())
            {
                NextAttack();
                attackTimer = 0;
            }

            switch (currentAttack)
            {
                case AttackType.VoidRift:
                    VoidRiftAttack(target);
                    break;
                case AttackType.ShadowClones:
                    ShadowClonesAttack(target);
                    break;
                case AttackType.DimensionalTear:
                    DimensionalTearAttack(target);
                    break;
                default:
                    VoidRiftAttack(target);
                    break;
            }
        }

        private void TransitionPhase(Player target)
        {
            NPC.dontTakeDamage = true;
            NPC.velocity *= 0.98f;
            
            if (phaseTimer % 10 == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(80, 80);
                    Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, (NPC.Center - dustPos) * 0.1f, 150, default, 2.5f);
                }
            }
            
            if (phaseTimer == 150)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                
                for (int i = 0; i < 50; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(10, 10);
                    Dust.NewDustPerfect(NPC.Center, DustID.PurpleTorch, velocity, 150, default, 3f);
                }
            }
        }

        private void PhaseTwoAI(Player target)
        {
            NPC.dontTakeDamage = false;
            
            if (attackTimer >= GetAttackDuration() * 0.7f) 
            {
                NextAttack();
                attackTimer = 0;
            }

            switch (currentAttack)
            {
                case AttackType.VoidStorm:
                    VoidStormAttack(target);
                    break;
                case AttackType.RealityBreak:
                    RealityBreakAttack(target);
                    break;
                case AttackType.DimensionalTear:
                    DimensionalTearAttack(target);
                    break;
                case AttackType.ShadowClones:
                    ShadowClonesAttack(target);
                    break;
                default:
                    VoidStormAttack(target);
                    break;
            }
        }

        private void EnragePhase(Player target)
        {
            if (attackTimer >= GetAttackDuration() * 0.5f) 
            {
                NextAttack();
                attackTimer = 0;
                
                if (Main.rand.NextBool(3))
                {
                    VoidNovaAttack(target);
                }
            }

            NPC.damage = (int)(NPC.damage * 1.5f);
            
            switch (currentAttack)
            {
                case AttackType.VoidNova:
                    VoidNovaAttack(target);
                    break;
                case AttackType.RealityBreak:
                    RealityBreakAttack(target);
                    break;
                case AttackType.VoidStorm:
                    VoidStormAttack(target);
                    break;
                default:
                    VoidNovaAttack(target);
                    break;
            }
        }

        private int GetAttackDuration()
        {
            return currentPhase switch
            {
                BossPhase.PhaseOne => 240,
                BossPhase.PhaseTwo => 180,
                BossPhase.Enrage => 120,
                _ => 300
            };
        }

        private void NextAttack()
        {
            patternIndex++;
            
            if (currentPhase == BossPhase.PhaseOne)
            {
                currentAttack = (AttackType)(patternIndex % 3); 
            }
            else if (currentPhase == BossPhase.PhaseTwo)
            {
                currentAttack = (AttackType)((patternIndex % 4) + 2); 
            }
            else if (currentPhase == BossPhase.Enrage)
            {
                currentAttack = (AttackType)(patternIndex % 6); 
            }
        }

        private void VoidRiftAttack(Player target)
        {
            if (attackTimer == 60)
            {
                anchorPoint = target.Center + new Vector2(0, -200);
                Vector2 moveDir = (anchorPoint - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = moveDir * 8f;
            }
            else if (attackTimer > 60 && Vector2.Distance(NPC.Center, anchorPoint) < 50f)
            {
                NPC.velocity *= 0.9f;
                
                if (attackTimer % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projType = ModContent.ProjectileType<VoidRiftProjectile>();
                    Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(100, 100);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPos, Vector2.Zero, projType, NPC.damage / 4, 0f, Main.myPlayer, target.whoAmI);
                }
            }
        }

        private void ShadowClonesAttack(Player target)
        {
            if (attackTimer == 30)
            {
                Vector2 teleportPos = target.Center + Main.rand.NextVector2Circular(300, 300);
                NPC.Center = teleportPos;
                NPC.velocity = Vector2.Zero;
                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 clonePos = target.Center + Main.rand.NextVector2Circular(250, 250);
                        int projType = ModContent.ProjectileType<ShadowCloneProjectile>();
                        int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), clonePos, Vector2.Zero, projType, NPC.damage / 3, 0f, Main.myPlayer, target.whoAmI, i);
                        activeMinions.Add(proj);
                    }
                }
                
                SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
            }
            else if (attackTimer > 30)
            {
                Vector2 moveDir = (target.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = Vector2.Lerp(NPC.velocity, moveDir * 4f, 0.05f);
                
                if (attackTimer % 45 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = (target.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                    int projType = ModContent.ProjectileType<VoidBoltProjectile>();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, toPlayer * 12f, projType, NPC.damage / 5, 0f, Main.myPlayer);
                }
            }
        }

        private void DimensionalTearAttack(Player target)
        {
            if (attackTimer <= 90)
            {
                NPC.velocity *= 0.95f;
                
                if (attackTimer % 15 == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(40, 40);
                        Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, (NPC.Center - dustPos) * 0.2f, 150, default, 2f);
                    }
                }
            }
            else if (attackTimer == 90)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 tearPos = target.Center + new Vector2(300 * (i % 2 == 0 ? -1 : 1), 300 * (i < 2 ? -1 : 1));
                        int projType = ModContent.ProjectileType<DimensionalTearProjectile>();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), tearPos, Vector2.Zero, projType, NPC.damage / 3, 0f, Main.myPlayer, target.whoAmI);
                    }
                }
                SoundEngine.PlaySound(SoundID.Item94, NPC.Center);
            }
            else
            {
                if (attackTimer % 60 == 0)
                {
                    Vector2 newTarget = target.Center + Main.rand.NextVector2Circular(200, 200);
                    Vector2 moveDir = (newTarget - NPC.Center).SafeNormalize(Vector2.Zero);
                    NPC.velocity = moveDir * 10f;
                }
                NPC.velocity *= 0.98f;
            }
        }

        private void VoidStormAttack(Player target)
        {
            if (attackTimer <= 60)
            {
                Vector2 stormCenter = target.Center + new Vector2(0, -250);
                Vector2 moveDir = (stormCenter - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = Vector2.Lerp(NPC.velocity, moveDir * 8f, 0.1f);
            }
            else
            {
                NPC.velocity *= 0.95f;
                
                if (attackTimer % 8 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(150, 150);
                    Vector2 velocity = Main.rand.NextVector2Circular(8, 8);
                    int projType = ModContent.ProjectileType<VoidStormProjectile>();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPos, velocity, projType, NPC.damage / 6, 0f, Main.myPlayer, target.whoAmI);
                }
            }
        }

        private void RealityBreakAttack(Player target)
        {
            if (attackTimer == 30)
            {
                Vector2 behindPlayer = target.Center + new Vector2(target.direction * -200, 0);
                NPC.Center = behindPlayer;
                NPC.velocity = Vector2.Zero;
                
                SoundEngine.PlaySound(SoundID.Item15, NPC.Center);
                
                for (int i = 0; i < 30; i++)
                {
                    Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(80, 80);
                    Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, Main.rand.NextVector2Circular(5, 5), 150, default, 2.5f);
                }
            }
            else if (attackTimer > 30 && attackTimer % 15 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int projCount = 8;
                for (int i = 0; i < projCount; i++)
                {
                    float angle = MathHelper.TwoPi / projCount * i;
                    Vector2 velocity = new Vector2(10f, 0).RotatedBy(angle);
                    int projType = ModContent.ProjectileType<RealityBreakProjectile>();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projType, NPC.damage / 4, 0f, Main.myPlayer);
                }
            }
        }

        private void VoidNovaAttack(Player target)
        {
            if (attackTimer <= 90)
            {
                NPC.velocity *= 0.9f;
                
                if (attackTimer % 10 == 0)
                {
                    float radius = 100f + (attackTimer * 2f);
                    for (int i = 0; i < 12; i++)
                    {
                        float angle = MathHelper.TwoPi / 12 * i;
                        Vector2 dustPos = NPC.Center + new Vector2(radius, 0).RotatedBy(angle);
                        Dust.NewDustPerfect(dustPos, DustID.RedTorch, (NPC.Center - dustPos) * 0.1f, 150, default, 3f);
                    }
                }
            }
            else if (attackTimer == 90)
            {
                SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projCount = 24;
                    for (int i = 0; i < projCount; i++)
                    {
                        float angle = MathHelper.TwoPi / projCount * i;
                        Vector2 velocity = new Vector2(15f, 0).RotatedBy(angle);
                        int projType = ModContent.ProjectileType<VoidNovaProjectile>();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projType, NPC.damage / 3, 0f, Main.myPlayer);
                    }
                }
                
                for (int i = 0; i < 100; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(20, 20);
                    Dust.NewDustPerfect(NPC.Center, DustID.RedTorch, velocity, 150, default, 4f);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int frameSpeed = currentPhase >= BossPhase.PhaseTwo ? 3 : 5;
            
            NPC.frameCounter++;
            if (NPC.frameCounter >= frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                
                int maxFrames = currentPhase == BossPhase.Enrage ? 6 : 4;
                if (NPC.frame.Y >= frameHeight * maxFrames)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.VoidEssence>(), 1, 40, 60));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofSight, 1, 20, 35));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 20, 35));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 20, 35));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofFright, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofMight, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.HallowedBar, 2, 20, 35));
            npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 1, 5, 10));

            IItemDropRule weaponPool = new OneFromOptionsNotScaledWithLuckDropRule(1, 1,
                ModContent.ItemType<Content.Items.Weapons.TheGrimWraith>()
            );
            npcLoot.Add(weaponPool);

            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 5, 5, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentSolar, 5, 5, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentStardust, 5, 5, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentVortex, 5, 5, 12));

            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterManaPotion, 1, 15, 25));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 15; i++)
            {
                int dustType = currentPhase == BossPhase.Enrage ? DustID.RedTorch : DustID.PurpleTorch;
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, hit.HitDirection * 2f, -2f, 150, default, 1.5f);
            }

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 80; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(15, 15);
                    Dust.NewDustPerfect(NPC.Center, DustID.PurpleTorch, velocity, 150, default, 3f);
                }
                
                SoundEngine.PlaySound(SoundID.NPCDeath52, NPC.Center);
            }
        }
    }
}


