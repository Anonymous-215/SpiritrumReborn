using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PenumbriumEchoBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
        }

    private float speed = 10f;

        public override void AI()
        {
            Projectile.velocity *= 0.99f;
            Projectile.rotation = Projectile.velocity.ToRotation();

            int targetIndex = (int)Projectile.ai[0];
            if (targetIndex >= 0 && targetIndex < Main.maxNPCs)
            {
                NPC npc = Main.npc[targetIndex];
                if (npc.active && !npc.friendly)
                {
                    Vector2 dir = npc.Center - Projectile.Center;
                    dir.Normalize();
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, dir * speed, 0.08f);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int nextTarget = -1;
            float bestDist = 400f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.whoAmI != target.whoAmI)
                {
                    float d = Vector2.Distance(npc.Center, Projectile.Center);
                    if (d < bestDist)
                    {
                        bestDist = d;
                        nextTarget = i;
                    }
                }
            }

            int remainingBounces = (int)Projectile.ai[1];

            int aoeRadius = 40; 
            int aoeDamage = System.Math.Max(1, damageDone / 3);
            if (Main.myPlayer == Projectile.owner)
            {
                Player ownerPlayer = Main.player[Projectile.owner];
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && npc.whoAmI != target.whoAmI)
                    {
                        float d = Vector2.Distance(npc.Center, Projectile.Center);
                        if (d <= aoeRadius)
                        {
                            bool crit = hit.Crit;
                            npc.SimpleStrikeNPC(aoeDamage, Projectile.direction, crit, 0, Projectile.DamageType, true, ownerPlayer.luck);
                        }
                    }
                }

                for (int d = 0; d < 12; d++)
                {
                    var dust = Dust.NewDustDirect(Projectile.position - new Microsoft.Xna.Framework.Vector2(aoeRadius), Projectile.width + aoeRadius * 2, Projectile.height + aoeRadius * 2, DustID.Electric, 0f, 0f, 150, default, 1.1f);
                    dust.velocity *= 0.6f;
                    dust.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
            }

            if (nextTarget != -1 && remainingBounces > 0)
            {
                remainingBounces--;
                Vector2 dir = Main.npc[nextTarget].Center - Projectile.Center;
                dir.Normalize();
                dir *= speed;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, dir, Projectile.type, damageDone / 2, 0f, Projectile.owner, nextTarget, remainingBounces);
            }
        }
    }
}


