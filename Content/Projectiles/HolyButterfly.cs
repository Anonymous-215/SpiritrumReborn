using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class HolyButterfly : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.98f;
            if (Projectile.velocity.LengthSquared() < 0.01f)
                Projectile.velocity += new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f));

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, default, 0.9f);
            }

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame, 0f, 0f, 0, default, 1.2f);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }

            float homingRange = 300f;
            int target = -1;
            float closest = homingRange;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(Projectile))
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < closest)
                    {
                        closest = dist;
                        target = i;
                    }
                }
            }

            if (target != -1)
            {
                Vector2 desired = (Main.npc[target].Center - Projectile.Center);
                desired.Normalize();
                desired *= 6f;
                Projectile.velocity = (Projectile.velocity * 20f + desired) / 21f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 120); 
        }
    }
}


