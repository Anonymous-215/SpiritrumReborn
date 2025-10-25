using SpiritrumReborn.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{

	public class FireScytheSlash : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 18; // The width of projectile hitbox
			Projectile.height = 18; // The height of projectile hitbox
			AIType = ProjectileID.Bullet;

			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
			Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 30; // Each update timeLeft is decreased by 1. Once timeLeft hits 0, the Projectile will naturally despawn. (60 ticks = 1 second)
            Projectile.localNPCHitCooldown = -1;
			Projectile.penetrate = 3;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) //idk if I will keep that
        {
        }
        private float spinOffset;

        public override void AI()
        {
            // simple spinning code for fun
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

            // This line of code makes the projectile face the direction it is moving
            Projectile.spriteDirection = Projectile.velocity.X >= 0f ? 1 : -1;
        }
        //This code is just for some simple dust effect
        public override bool PreAI()
        {
            // This code is for travel dust
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
            // Spawn dust when the projectile ends
            int count = Main.rand.Next(6, 10);
            for (int i = 0; i < count; i++)
            {
                var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 158);
                d.velocity = new Microsoft.Xna.Framework.Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
                d.scale = 1.25f;
                d.noGravity = false;
            }
            // Here what it does, it spawns the Volcano explosion, but deal 140% of the original damage.
            // I put that for an optimal range (note that while in true melee, you have more damage)
            int volcanoDamage = (int)(Projectile.damage * 1.4f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.Volcano, volcanoDamage, Projectile.knockBack, Projectile.owner);
        }
	}
}