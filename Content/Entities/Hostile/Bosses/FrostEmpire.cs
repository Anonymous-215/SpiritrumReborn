using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;

namespace SpiritrumReborn.Content.Entities.Hostile.Bosses
{
    [AutoloadBossHead]
    public class FrostEmpire : ModNPC
    {
        private int baseLife;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4; 
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("The Frost Emperor and the Frost Empress are ancient rulers of a long-lost frozen kingdom. Their love is eternal — cold and majestic, like their realm itself. They bound together even in darkness. Their daughter, the Frost Queen, is just as cold and merciless. She chose the path of destructive power, and her parents proudly stood by her side. Together, they see good not as salvation, but as a threat to the order they seek to restore to the world.")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 120;
            NPC.damage = 60;
            NPC.defense = 60;
            int baseLife = 20000;
            if (Main.masterMode)
                baseLife -= 5000;
            else if (Main.expertMode)
                baseLife -= 7500;
            NPC.lifeMax = baseLife; 
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 25f;
            NPC.aiStyle = -1; 
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        private Player Target => Main.player[NPC.target];
    private int attackTimer;
    private bool pauseWindow; 
    private Vector2 hoverPosition;
    private int patternIndex;
    private int patternStep;

        public override void AI()
        {
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

            hoverPosition = Target.Center + new Vector2(0, -250);

            attackTimer++;
            if (attackTimer < 240)
            {
                pauseWindow = false;
                NPC.dontTakeDamage = true;
                DoAttackPattern();
            }
            else if (attackTimer < 390)
            {
                if (!pauseWindow)
                {
                    patternIndex = (patternIndex + 1) % 4;
                    patternStep = 0;
                    pauseWindow = true;
                }
                NPC.velocity *= 0.90f;
                NPC.dontTakeDamage = false;
            }
            else
            {
                attackTimer = 0;
            }
        }
        private void DoAttackPattern()
        {
            Vector2 moveDirection = (hoverPosition - NPC.Center).SafeNormalize(Vector2.Zero);
            NPC.velocity = Vector2.Lerp(NPC.velocity, moveDirection * 5f, 0.08f);
            patternStep++;
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            switch (patternIndex)
            {
                case 0: Pattern_DualSpirals(); break;
                case 1: Pattern_CrossBeams(); break;
                case 2: Pattern_BlizzardRain(); break;
                case 3: Pattern_IceNovaBursts(); break;
            }
        }

        private void Pattern_DualSpirals()
        {
            if (patternStep % 8 == 0)
            {
                float baseAngle = patternStep * 0.15f;
                for (int i = 0; i < 2; i++)
                {
                    float ang = baseAngle + MathHelper.Pi * i;
                    Vector2 vel = new Vector2(10f, 0).RotatedBy(ang);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostShard, NPC.damage / 4, 0f, Main.myPlayer);
                }
            }
        }

        private void Pattern_CrossBeams()
        {
            if (patternStep % 65 == 5)
            {
                Vector2 toPlayer = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                float baseSpread = MathHelper.ToRadians(32 + Main.rand.NextFloat(-8f, 8f)); 
                for (int i = -2; i <= 2; i++)
                {
                    float spread = baseSpread * i;
                    Vector2 vel = toPlayer.RotatedBy(spread) * 20f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.FrostBeam, (int)(NPC.damage * 0.85f / 2), 0f, Main.myPlayer);
                }
            }
        }

        private void Pattern_BlizzardRain()
        {
            if (patternStep % 12 == 0)
            {
                Vector2 spawn = Target.Center + new Vector2(Main.rand.Next(-300, 300), -500);
                Vector2 vel = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(10f, 16f));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), spawn, vel, ProjectileID.IcewaterSpit, NPC.damage / 3, 0f, Main.myPlayer);
            }
        }

        private void Pattern_IceNovaBursts()
        {
            if (patternStep % 50 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Vector2 vel = new Vector2(12f, 0).RotatedBy(MathHelper.TwoPi / 16 * i);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.IceSpike, NPC.damage / 3, 0f, Main.myPlayer);
                }
            }
        }


        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 4)
                    NPC.frame.Y = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Materials.FrozoniteOre>(), 1, 80, 126));
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 offset)
        {
            if (!pauseWindow)
                scale = 1.1f;
            else
                scale = 1.3f + (float)Math.Sin(Main.GameUpdateCount * 0.4f) * 0.05f;
            return null;
        }
    }
}


