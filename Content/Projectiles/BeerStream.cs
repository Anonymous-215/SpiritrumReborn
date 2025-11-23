using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class BeerStream : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 90;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.alpha = 60;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.12f;
            Lighting.AddLight(Projectile.Center, 0.6f, 0.45f, 0.2f);

            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 350, default, 1.6f);
                Main.dust[dust].noGravity = true;
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Tipsy, 480);

            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(target.position, target.width, target.height, DustID.IchorTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 150, default, 1.4f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Tipsy, 480);
        }
    }
}