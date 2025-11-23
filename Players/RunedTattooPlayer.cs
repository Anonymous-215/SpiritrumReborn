using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class RunedTattooPlayer : ModPlayer
    {
        public bool runedTattooEquipped;
        private int greenProj = -1;
        private int orangeProj = -1;
        private float runeRotation = 0f;
        private int shootCooldown = 0;

        public override void ResetEffects()
        {
            runedTattooEquipped = false;
        }

        public override void PostUpdate()
        {
            if (Player.whoAmI != Main.myPlayer) return;

            if (runedTattooEquipped)
            {
                Vector2 center = Player.Center;
                const float radius = 125f;
                const float rotationsPerSecond = 1.5f;
                float angleIncrement = MathHelper.TwoPi * rotationsPerSecond / 60f;
                runeRotation += angleIncrement;
                if (runeRotation > MathHelper.TwoPi) runeRotation -= MathHelper.TwoPi;

                if (shootCooldown > 0) shootCooldown--;

                if (greenProj < 0 || greenProj >= Main.maxProjectiles || !Main.projectile[greenProj].active || Main.projectile[greenProj].owner != Player.whoAmI)
                {
                    int id = Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), center, Vector2.Zero, ProjectileID.RuneBlast, 40, 0f, Player.whoAmI);
                    greenProj = id;
                    if (greenProj >= 0 && greenProj < Main.maxProjectiles)
                    {
                        Main.projectile[greenProj].friendly = true;
                        Main.projectile[greenProj].hostile = false;
                        Main.projectile[greenProj].tileCollide = false;
                        Main.projectile[greenProj].timeLeft = 9999;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, greenProj);
                    }
                }
                else
                {
                    if (greenProj >= 0 && greenProj < Main.maxProjectiles && Main.projectile[greenProj].active)
                    {
                        Vector2 offset = new Vector2(radius, 0f).RotatedBy(runeRotation);
                        Main.projectile[greenProj].Center = center + offset;
                        Main.projectile[greenProj].velocity = Vector2.Zero;
                        Main.projectile[greenProj].timeLeft = 2;
                        Main.projectile[greenProj].rotation = (runeRotation + MathHelper.PiOver2);
                    }
                }

                if (orangeProj < 0 || orangeProj >= Main.maxProjectiles || !Main.projectile[orangeProj].active || Main.projectile[orangeProj].owner != Player.whoAmI)
                {
                    int id = Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), center, Vector2.Zero, ProjectileID.RuneBlast, 40, 0f, Player.whoAmI);
                    orangeProj = id;
                    if (orangeProj >= 0 && orangeProj < Main.maxProjectiles)
                    {
                        Main.projectile[orangeProj].friendly = true;
                        Main.projectile[orangeProj].hostile = false;
                        Main.projectile[orangeProj].tileCollide = false;
                        Main.projectile[orangeProj].timeLeft = 9999;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, orangeProj);
                    }
                }
                else
                {
                    if (orangeProj >= 0 && orangeProj < Main.maxProjectiles && Main.projectile[orangeProj].active)
                    {
                        Vector2 offset = new Vector2(radius, 0f).RotatedBy(runeRotation + MathHelper.Pi);
                        Main.projectile[orangeProj].Center = center + offset;
                        Main.projectile[orangeProj].velocity = Vector2.Zero;
                        Main.projectile[orangeProj].timeLeft = 2;
                        Main.projectile[orangeProj].rotation = (runeRotation + MathHelper.PiOver2 + MathHelper.Pi);
                    }
                }

                if (shootCooldown <= 0)
                {
                    NPC target = null;
                    float bestDist = 600f;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc == null || !npc.active || npc.friendly || npc.life <= 5 || npc.dontTakeDamage) continue;
                        float dist = Vector2.Distance(npc.Center, center);
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            target = npc;
                        }
                    }

                    if (target != null)
                    {
                        Vector2 dir = Vector2.Normalize(target.Center - center);
                        float speed = 14f;
                        Vector2 vel = dir * speed;
                        int scaledDamage = (int)Player.GetDamage(DamageClass.Magic).ApplyTo(25f);
                        int shot = Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), center, vel, ProjectileID.RuneBlast, scaledDamage, 2f, Player.whoAmI);
                        if (shot >= 0 && shot < Main.maxProjectiles)
                        {
                            Main.projectile[shot].friendly = true;
                            Main.projectile[shot].hostile = false;
                            Main.projectile[shot].timeLeft = 60;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, shot);
                        }

                        shootCooldown = 120;
                    }
                }
            }
            else
            {
                if (greenProj >= 0 && greenProj < Main.maxProjectiles && Main.projectile[greenProj].active && Main.projectile[greenProj].owner == Player.whoAmI)
                {
                    Main.projectile[greenProj].Kill();
                    greenProj = -1;
                }
                if (orangeProj >= 0 && orangeProj < Main.maxProjectiles && Main.projectile[orangeProj].active && Main.projectile[orangeProj].owner == Player.whoAmI)
                {
                    Main.projectile[orangeProj].Kill();
                    orangeProj = -1;
                }
            }
        }
    }
}
