using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class PenumbralPlayer : ModPlayer
    {
        public bool penumbralSet;

        public override void ResetEffects()
        {
            penumbralSet = false;
        }
    }
}
