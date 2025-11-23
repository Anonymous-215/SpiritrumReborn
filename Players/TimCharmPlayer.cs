using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class TimCharmPlayer : ModPlayer
    {
        public bool timCharm;

        public override void ResetEffects()
        {
            timCharm = false;
        }
    }
}
