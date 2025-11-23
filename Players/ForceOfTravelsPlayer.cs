using Terraria;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class ForceOfTravelsPlayer : ModPlayer
    {
        public bool forceOfTravels = false;

        public override void ResetEffects()
        {
            forceOfTravels = false;
        }
    }
}
