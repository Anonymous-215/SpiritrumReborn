using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.BaseGameReworks;

namespace SpiritrumReborn.Content.Debuffs
{
    public class MythrilCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MythrilReworkGlobalNPC>().mythrilCurse = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}


