using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PenumbriumRing : ModProjectile
    {
        private int pierceCount = 0;
        private bool returning = false;
        private const int maxPierce = 3;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 300;
            Projectile.light = 0.8f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100, default, 0.8f);
            }
            Projectile.rotation += 0.35f * (Projectile.direction == 0 ? 1 : Projectile.direction);

            if (!returning)
            {
            }
            else
            {
                Player owner = Main.player[Projectile.owner];
                Vector2 toPlayer = owner.Center - Projectile.Center;
                float speed = 14f;
                Projectile.velocity = Vector2.Normalize(toPlayer) * speed;

                if (toPlayer.Length() < 24f)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Shadowflame, 0f, 0f, 100, default, 1.2f);
            }
            pierceCount++;
            if (pierceCount >= maxPierce && !returning)
            {
                returning = true;
                Projectile.tileCollide = false;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            if (!returning)
            {
                returning = true;
                Projectile.tileCollide = false;
                return false; 
            }
            return true; 
        }
    }
}


