using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class SkinwalkerClawBarrierProjectile : ModProjectile
    {
        private const int ClawCount = 5;
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.Center = owner.Center;
            if (Projectile.localAI[0] == 0f)
            {
                for (int i = 0; i < ClawCount; i++)
                {
                    float angle = (MathHelper.TwoPi / ClawCount) * i + ((float)Main.time / 30f);
                    Vector2 ofs = new Vector2(40f, 0).RotatedBy(angle);
                    float spreadRadians = MathHelper.ToRadians(45f);
                    float randomOffset = Main.rand.NextFloat(-spreadRadians, spreadRadians);
                    ofs = ofs.RotatedBy(randomOffset);
                    Vector2 spawnPos = Projectile.Center + ofs;
                    Vector2 direction = (spawnPos - Projectile.Center);
                    if (direction == Vector2.Zero)
                        direction = new Vector2(1f, 0f);
                    direction.Normalize();
                    float speed = 3.5f; 
                    Vector2 velocity = direction * speed;
                    int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, velocity, ModContent.ProjectileType<ClawBladeProjectile>(), Projectile.damage, 2f, Projectile.owner);
                    Main.projectile[p].ai[0] = 0.08f * (i == 0 ? 1f : -1f); 
                    Main.projectile[p].timeLeft = 30; 
                }
                Projectile.localAI[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (!owner.active || owner.dead)
            {
                Projectile.Kill();
            }
        }
    }
}


