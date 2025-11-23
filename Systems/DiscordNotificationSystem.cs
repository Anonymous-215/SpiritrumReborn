using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Systems
{
    public class DiscordNotificationSystem : ModSystem
    {
        private int notificationTimer = 0;
        private bool shouldShowNotification = false;

        public override void OnWorldLoad()
        {
            shouldShowNotification = true;
            notificationTimer = 0;
        }

        public override void PostUpdateTime()
        {
            if (shouldShowNotification)
            {
                notificationTimer++;
                if (notificationTimer >= 240)
                {
                    Main.NewText("Spiritrum Reborn is Spiritrum... but better", Color.Cyan);
                    shouldShowNotification = false;
                    notificationTimer = 0;
                }
            }
        }
    }
}


