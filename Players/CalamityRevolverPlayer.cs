using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;

namespace SpiritrumReborn.Players
{
    public class CalamityRevolverPlayer : ModPlayer
    {
        public int spinCooldown = 0;
        public int spinCharges = 0;

        private int previousCooldown = 0;

        public override void ResetEffects()
        {
        }

        public override void PostUpdate()
        {
            if (spinCooldown > 0)
            {
                spinCooldown--;
            }

            // Play ready sound when cooldown just finished
            if (previousCooldown > 0 && spinCooldown == 0 && Player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Item35, Player.position);
            }

            // Ensure a cooldown/spin indicator projectile exists while cooldown is active
            if (spinCooldown > 0 && Player.whoAmI == Main.myPlayer)
            {
                bool hasProj = false;
                int projType = ModContent.ProjectileType<Content.Projectiles.CalamityRevolverSpin>();
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == Player.whoAmI && p.type == projType)
                    {
                        hasProj = true;
                        break;
                    }
                }

                if (!hasProj)
                {
                    Projectile.NewProjectile(Player.GetSource_Misc("CalamityRevolverCooldown"), Player.Center, Vector2.Zero, projType, 0, 0f, Player.whoAmI);
                }
            }

            previousCooldown = spinCooldown;
        }
    }
}
