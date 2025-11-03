// IMPORTANT: NEEDS A RESPRITE AND NOT A FULLY BLACK (0, 0, 0) GUIDE


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using SpiritrumReborn.Content.Items.Accessories; 
using SpiritrumReborn.Content.Items.Weapons;
using SpiritrumReborn.Content.Items.Materials; 
using SpiritrumReborn.Content.Projectiles;  
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Entities.Friendly
{
    [AutoloadHead]
    public class NamelessTraveller : ModNPC
    {
        public override void SetStaticDefaults()
        {
           Main.npcFrameCount[NPC.type] = 26;
        }

        public override void SetDefaults()
        {
           NPC.townNPC = true;
           NPC.friendly = true;
           NPC.width = 18;
           NPC.height = 40;
           NPC.aiStyle = 7; 
           AnimationType = NPCID.Guide;
           NPC.lifeMax = 250000;
           NPC.defense = 15000;
           NPC.damage = 10;
           NPC.knockBackResist= 0f;
           NPC.HitSound = SoundID.NPCHit1;
           NPC.DeathSound = SoundID.NPCDeath1;
        }


        public override void AddShops()
        {
            var shop = new NPCShop(Type, "Shop"); 

            shop.Add<NamelessEmblem>(
                new Condition("Conditions.DownedAnyMechBoss", () => NPC.downedMechBossAny)
            );

            shop.Add<WhisperingBlade>(
                new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss)
            );

            shop.Add<BrokenBlaster>(
                new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss)
            );

            shop.Register();
        }

        public override string GetChat()
        {
            return Main.rand.Next(20) switch // Most lines are references to other mods, since the Nameless Traveller is never and always in every world.
            {
                0 => "I wonder if my father will arrive some day. He is the [UNKNOWN DATA]",
                1 => "There is this weird story that happened before my arrival. I do wonder what was it.",
                2 => "You don't know about the KAS council? They are the 3 creators. Konetrum, Anlatrum and Spiritrum.",
                3 => "Wrath of the Gods? Do you mean Wrath of the Creators?", //Wrath of the Gods
                4 => "Some may say, my father is a nameless deity, but why would that be the case?", //Wrath of the Gods
                5 => "Did you know, that before you came to this world, it was a total calamity.", //Calamity
                6 => "Shimmer is a very powerful tool that I need access for some research.",
                7 => "I've travelled multiple viking worlds, like that ancient one called Thorium", //Thorium
                8 => "There are some secrets that can be found in the shadows.", //Secrets of the Shadows
                9 => "I can feel that you are having a redemption arc.", //Redemption
                10 => "Ph... sorry, I can't say much. The creators will smite me right now if I say another letter.", //Ignore that part, I'm just keeping stuff for the lore
                11 => "The world of Calamity is a part of the 2nd dimension. Over there it can be either a ragnarok, or an infernal eclipse.", //Infernal Eclipse of Ragnarok
                12 => "There is this mineral that is very obscure and deep, it should make you able to awake any element.", //Elements awoken
                13 => "Have fun on your journey to home.", //Homeward Journey
                14 => "No I am not the type of guy to be Spooky.", //Spooky
                15 => "You shall never lack any soul!", //Fargo's Souls and Bereft Souls (Reminder: Bereft = Lack of)
                16 => "I wonder why this part of this dimension is called Spiritrum. Is it because it is a Spirit that drank rum?", //Spirit and Spiritrum
                17 => "Abaddon? Is that some sort of joke to me? The creators banished him in the shadow realm.", //Shadows of Abaddon
                18 => "There are 2 type of gods, old type gods and creator type gods.", //Hunt of the old gods
                19 => "Sorry, but I do not own a console.", //Consolaria
                _ => "This dimension is the best out of all 11 as most say. I know it, but I can't confirm it."
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); 
        }


        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs) => true;
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] { 
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                new FlavorTextBestiaryInfoElement("He is at the same time, eveverywhere and nowhere")
            });
        }
    }
}

