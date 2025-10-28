using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Buffs
{
    public class PascaliteSunflowerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = false;
        }
    }
}


