using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CoralShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)//points towards where they are going
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }

        public override void OnKill(int timeLeft) //Spawn shrapnels either when the projectile is gone or on a hit
        {
            SpawnShrapnel();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SpawnShrapnel();
        }

        private void SpawnShrapnel()
        {
            int count = Main.rand.Next(2, 4); //spawns 2-4 shrapnel in random directions
            for (int i = 0; i < count; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(3f, 3f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, vel, ModContent.ProjectileType<CoralShotShrapnel>(), Projectile.damage / 2, 1f, Projectile.owner);
            }
        }
    }
}


