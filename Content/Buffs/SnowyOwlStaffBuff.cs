using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Buffs
{
    public class SnowyOwlStaffBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }
}
