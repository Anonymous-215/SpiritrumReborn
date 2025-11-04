using SpiritrumReborn.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{

	public class FireScytheSlash : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 18; 
			Projectile.height = 18; 
			AIType = ProjectileID.Bullet;

			Projectile.friendly = true; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.ignoreWater = false; 
			Projectile.tileCollide = false; 
			Projectile.timeLeft = 30; 
            Projectile.localNPCHitCooldown = -1;
			Projectile.penetrate = 3;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) 
        {
        }
        private float spinOffset;

        public override void AI()
        {
            spinOffset += 0.8f;

            if (Projectile.velocity.LengthSquared() > 0.001f)
            {
                float dir = (float)System.Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
                Projectile.rotation = dir + spinOffset;
            }
            else
            {
                Projectile.rotation += 0.8f;
            }

            Projectile.spriteDirection = Projectile.velocity.X >= 0f ? 1 : -1;
        }
        public override bool PreAI()
        {
            if (Main.rand.NextBool(5))
            {
            var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 158);
            d.velocity *= 0.25f;
            d.scale = 0.9f;
            d.noGravity = false;
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {
            int count = Main.rand.Next(6, 10);
            for (int i = 0; i < count; i++)
            {
                var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 158);
                d.velocity = new Microsoft.Xna.Framework.Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
                d.scale = 1.25f;
                d.noGravity = false;
            }
            int volcanoDamage = (int)(Projectile.damage * 1.4f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.Volcano, volcanoDamage, Projectile.knockBack, Projectile.owner);
        }
	}
}

