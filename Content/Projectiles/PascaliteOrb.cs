using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PascaliteOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            NPC target = null;
            float closest = 500f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    float d = Vector2.Distance(Projectile.Center, npc.Center);
                    if (d < closest)
                    {
                        closest = d;
                        target = npc;
                    }
                }
            }

            if (target != null)
            {
                Vector2 dir = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, dir * 10f, 0.1f);
            }
            Projectile.rotation += 0.4f;
        }
    }
}


