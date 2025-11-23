using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CopiumBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            Projectile.velocity *= 0.999f;
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int owner = Projectile.owner;
            Player player = Main.player[owner];
            int aoeRadius = 30;

            for (int i = 0; i < 20; i++)
            {
                Vector2 spawn = Projectile.Center + Main.rand.NextVector2Circular(aoeRadius, aoeRadius);
                Dust.NewDustDirect(spawn - Vector2.One * 4f, 8, 8, DustID.Smoke).velocity *= 1.5f;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) <= aoeRadius)
                {
                    int damage = (int)(damageDone); 
                    bool crit = hit.Crit;
                    npc.SimpleStrikeNPC(damage, Projectile.direction, crit, 0, DamageClass.Ranged, true, player.luck);
                }
            }

            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}


