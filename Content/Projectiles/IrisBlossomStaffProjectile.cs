using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class IrisBlossomStaffProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.light = 0.5f;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.05f; //Gets slower over distance traveled
            Projectile.rotation += 0.1f * (float)Projectile.direction;
            if (Main.rand.NextBool(6))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Grass, 0f, 0f, 150, default, 0.8f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 120);
        }
    }
}


