using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Players
{
    public class VoidHunterEnchPlayer : ModPlayer
    {
        public bool voidHunterEnchanted = false;
        private int voidRiftTimer = 0;

        public override void ResetEffects()
        {
            voidHunterEnchanted = false;
        }

        public override void PostUpdate()
        {
            if (!voidHunterEnchanted)
                return;

            if (Main.myPlayer == Player.whoAmI)
            {
                bool portalExists = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<global::SpiritrumReborn.Content.Projectiles.VoidPortal>())
                    {
                        portalExists = true;
                        proj.timeLeft = 10;
                        break;
                    }
                }
                if (!portalExists)
                {
                    Vector2 portalPos = Player.Center + new Vector2(0, -64);
                    int baseDamage = 80;
                    float kb = 3f;
                    int portalCount = 1;
                    if (Player.GetModPlayer<ForceOfTravelsPlayer>().forceOfTravels)
                    {
                        baseDamage = 80 * 3;
                        kb = 3f;
                        portalCount = 2;
                    }
                    for (int pi = 0; pi < portalCount; pi++)
                    {
                        Vector2 pos = portalPos + new Vector2(40 * (pi - (portalCount - 1) / 2f), 0);
                        Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), pos, Vector2.Zero, ModContent.ProjectileType<global::SpiritrumReborn.Content.Projectiles.VoidPortal>(), baseDamage, kb, Player.whoAmI);
                    }
                }
            }
        }
    }
}
