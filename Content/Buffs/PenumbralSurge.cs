using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Buffs
{
    public class PenumbralSurge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 0.12f; 
        }
    }
}


