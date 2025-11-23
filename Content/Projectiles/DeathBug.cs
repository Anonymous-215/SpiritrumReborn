using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Projectiles
{
    public class DeathBug : ModProjectile
    {

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.12f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Main.rand.NextBool(4))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, default, 0.8f);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0f, 0f, 0, default, 1f);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.position);
            }
        }

    [System.Obsolete]
    public override void Kill(int timeLeft)
        {
            int shards = Main.rand.Next(3, 6);
            for (int i = 0; i < shards; i++)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, -1f));
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<DecayShard>(), Projectile.damage / 2, 0, Projectile.owner);
            }
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(3))
                target.AddBuff(BuffID.Bleeding, 180);
        }
    }
}


