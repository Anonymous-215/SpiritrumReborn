using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpiritrumReborn.Content.Buffs
{
    public class ElementalBlades : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }
}
