using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Players
{
    public class TipsyPlayer : ModPlayer
    {
        public bool hasPortableHoneycomb;
        private int portableHoneycombCooldown;

        public override void ResetEffects()
        {
            hasPortableHoneycomb = false;
            if (portableHoneycombCooldown > 0)
                portableHoneycombCooldown--;
        }

        public override void UpdateBadLifeRegen()
        {
            bool tipsy = Player.HasBuff(BuffID.Tipsy);
            bool poisoned = Player.HasBuff(BuffID.Poisoned);
            bool venom = Player.HasBuff(BuffID.Venom);

            foreach (var npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    if (npc.HasBuff(BuffID.Tipsy)) tipsy = true;
                    if (npc.HasBuff(BuffID.Poisoned)) poisoned = true;
                    if (npc.HasBuff(BuffID.Venom)) venom = true;
                }
            }

            if (tipsy)
            {
                if (poisoned)
                {
                    Player.lifeRegen -= 4;
                }
                if (venom)
                {
                    Player.lifeRegen -= 15;
                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!hasPortableHoneycomb || info.Damage <= 0) return;
            if (portableHoneycombCooldown > 0) return;

            portableHoneycombCooldown = 30;

            if (Main.myPlayer != Player.whoAmI) return;

            int stingers = Main.rand.Next(2, 5);
            for (int i = 0; i < stingers; i++)
            {
                float speed = Main.rand.NextFloat(3f, 6f);
                Vector2 vel = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1.5f, -0.2f));
                vel = Vector2.Normalize(vel) * speed;
                int projType = ProjectileID.QueenBeeStinger;
                int damage = 8;
                int p = Projectile.NewProjectile(Player.GetSource_Misc("PortableHoneycomb"), Player.Center, vel, projType, damage, 1f, Player.whoAmI);
                if (p >= 0 && p < Main.maxProjectiles)
                {
                    Projectile pr = Main.projectile[p];
                    pr.friendly = true;
                    pr.hostile = false;
                    pr.timeLeft = 60;
                }
            }
        }
    }
}
