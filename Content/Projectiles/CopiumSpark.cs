using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CopiumSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 20;
            Projectile.ignoreWater = true;
        }

    public override void AI()
        {
            Projectile.rotation += 0.4f;
            Projectile.velocity *= 0.98f;
            NPC target = null;
            float closest = 300f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(this) && Vector2.Distance(n.Center, Projectile.Center) < closest)
                {
                    closest = Vector2.Distance(n.Center, Projectile.Center);
                    target = n;
                }
            }
            if (target != null)
            {
                Vector2 toTarget = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget * 6f, 0.08f);
            }
            Lighting.AddLight(Projectile.Center, 0.4f, 0.1f, 0.6f);
        }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 4; i++)
            {
                var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
                d.velocity *= 0.6f;
                d.scale = 0.8f;
                d.noGravity = true;
            }
        }
    }
}


