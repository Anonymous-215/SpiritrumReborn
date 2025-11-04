using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Buffs
{
    public class ShimmeredDust : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false; 
            Main.debuff[Type]     = false;   
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.20f;
            player.jumpSpeedBoost += 1.00f;
            if (Main.rand.NextBool(3)) 
            {
                int d = Dust.NewDust(
                    player.position,
                    player.width,
                    player.height,
                    DustID.GoldFlame
                );
                Main.dust[d].velocity *= 0.3f;
            }
        }
    }
}

