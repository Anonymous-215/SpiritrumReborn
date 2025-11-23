using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class CryoglyphAmuletPlayer : ModPlayer
    {
        public bool cryoglyphAmuletEquipped;

        public override void ResetEffects()
        {
            cryoglyphAmuletEquipped = false;
        }
    }
}
