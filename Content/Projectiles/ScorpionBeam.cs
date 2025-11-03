using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class ScorpionBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.6f, 0.2f); // light production
            if (Projectile.velocity != Microsoft.Xna.Framework.Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            if (Main.rand.NextBool(3)) // dust production
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[d].velocity *= 0.2f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 20); //Inflicts daybreak for 1/3 of a second
        }
    }
}


