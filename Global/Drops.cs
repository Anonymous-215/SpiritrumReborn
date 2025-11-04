using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace SpiritrumReborn.Global
{
    public class Drops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.BurningFleshChunk>(), 4));
            }
            if (npc.type == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.GelaticCrown>(), 33));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.StealthNodachi>(), 3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.NinjaTextbook>(), 3));
            }
            
            if (npc.type == NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.BoneCaster>(), 10));
            }
            if (npc.type == NPCID.NutcrackerSpinning)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.NutCracker>(), 20));
            }
            if (npc.type == NPCID.Deerclops)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.BreezeBreath>(), 5));
            }
            if (npc.type == NPCID.Nurse)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.WarmShot>(), 1));
            }
        }
    }
}


