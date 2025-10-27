using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Entities.Hostile
{
    public class EvilGoog : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 10; 
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;
            NPC.damage = 25;
            NPC.defense = 12;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 25f;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            NPC.buffImmune[BuffID.Confused] = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneOverworldHeight || Main.bloodMoon && !Main.dayTime && !spawnInfo.PlayerInTown && !Main.eclipse && !Main.pumpkinMoon && !Main.snowMoon)
            {
                return 0.25f; 
            }
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
                            // (ID.Item, drop chance=1/x, min amount, max amount)
            npcLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 2, 5, 20)); 
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Just a cat that was infected by a zombie. There is no way to save it (Sadly).")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, default, 0.7f);
                }
            }
        }
    }
}


