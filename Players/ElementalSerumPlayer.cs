using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Players
{
    public class ElementalSerumPlayer : ModPlayer
    {
        public bool elementalSerumEquipped;
        private int cloudTimer;

        public override void ResetEffects()
        {
            elementalSerumEquipped = false;
        }

        public override void PostUpdate()
        {
            if (!elementalSerumEquipped) return;

            cloudTimer++;
            if (cloudTimer >= 60)
            {
                cloudTimer = 0;
                if (Main.myPlayer == Player.whoAmI)
                {
                    bool enemiesNearby = false;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.Distance(Player.Center) < 500f)
                        {
                            enemiesNearby = true;
                            break;
                        }
                    }
                    if (enemiesNearby)
                    {
                        int cloudCount = Main.rand.Next(2, 5);
                        for (int i = 0; i < cloudCount; i++)
                        {
                            Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-200, 201), Main.rand.Next(-120, 121));
                            int cloudType = Main.rand.Next(3) switch
                            {
                                0 => ProjectileID.ToxicCloud,
                                1 => ProjectileID.ToxicCloud2,
                                _ => ProjectileID.ToxicCloud3
                            };
                            int damage = 40;
                            Projectile.NewProjectile(Player.GetSource_Misc("ElementalSerumCloud"), spawnPos, Vector2.Zero, cloudType, damage, 0f, Player.whoAmI);
                        }
                    }
                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!elementalSerumEquipped || info.Damage <= 0) return;
            int sparks = 14;
            for (int i = 0; i < sparks; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-7f, -3f));
                int proj = Projectile.NewProjectile(Player.GetSource_Misc("ElementalSerumFlare"), Player.Center, velocity, ProjectileID.GreekFire2, 60, 7f, Player.whoAmI);
                if (proj >= 0 && proj < Main.maxProjectiles)
                {
                    Projectile p = Main.projectile[proj];
                    p.friendly = true;
                    p.hostile = false;
                    p.timeLeft = 90;
                }
            }
        }
    }
}
