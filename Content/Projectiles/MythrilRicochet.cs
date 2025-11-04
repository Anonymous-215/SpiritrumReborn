using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class MythrilRicochet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
        }

        public override bool PreAI()
        {
            if (Projectile.ai[0] == 0f)
                Projectile.ai[0] = 3f; 

            return true;
        }

        public override void AI()
        {
            if (!Projectile.localAI[0].Equals(1f))
            {
                int[] dustIds = new int[] { 131, 157, 128 };
                Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.Zero);
                if (dir == Vector2.Zero)
                    dir = Main.rand.NextVector2Unit();
                float baseSpeed = Math.Max(0.6f, Projectile.velocity.Length() * 0.25f);
                for (int i = 0; i < 3; i++)
                {
                    int id = dustIds[Main.rand.Next(dustIds.Length)];
                    var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, id, 0f, 0f, 100, default, 1.05f);
                    d.noGravity = true;
                    d.scale = 1.25f;
                    d.velocity = dir * (baseSpeed * Main.rand.NextFloat(0.9f, 1.1f));
                }
            }
        }

        public override bool PreDraw(ref Microsoft.Xna.Framework.Color lightColor)
        {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0]--;
            if (Projectile.ai[0] <= 0f)
            {
                SpawnRicochetDusts(6);
                return true; 
            }

            if (oldVelocity.X != Projectile.velocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.75f;
            }
            if (oldVelocity.Y != Projectile.velocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.75f;
            }

            SpawnRicochetDusts(4);
            Projectile.netUpdate = true;
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= (1f / 3f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0]--;
            if (Projectile.ai[0] <= 0f)
            {
                Projectile.Kill();
                return;
            }

            const float searchRadius = 600f; 
            List<int> candidates = new List<int>();

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n == null || !n.active) continue;
                if (i == target.whoAmI) continue;
                if (n.friendly) continue;
                if (n.lifeMax <= 5) continue;
                if (n.dontTakeDamage) continue;

                if (Vector2.Distance(Projectile.Center, n.Center) <= searchRadius)
                    candidates.Add(i);
            }

            if (candidates.Count > 0)
            {
                int pick = Main.rand.Next(candidates.Count);
                NPC next = Main.npc[candidates[pick]];
                Vector2 dir = (next.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                float speed = Projectile.velocity.Length();
                if (speed < 6f) speed = 10f;
                Projectile.velocity = dir * speed;
            }
            else
            {
                Projectile.velocity = Main.rand.NextVector2Unit() * 8f;
            }

            Projectile.netUpdate = true;
        }

        private void SpawnRicochetDusts(int count)
        {
            int[] dustIds = new int[] { 131, 157, 128 };
            Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.Zero);
            if (dir == Vector2.Zero) dir = Main.rand.NextVector2Unit();
            float baseSpeed = Math.Max(0.6f, Projectile.velocity.Length() * 0.25f);
            for (int i = 0; i < count; i++)
            {
                int id = dustIds[Main.rand.Next(dustIds.Length)];
                Vector2 pos = Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
                var d = Dust.NewDustDirect(new Microsoft.Xna.Framework.Vector2(pos.X, pos.Y), 2, 2, id, 0f, 0f, 100, default, 1.0f);
                if (d != null)
                {
                    d.noGravity = true;
                    d.velocity = dir * (baseSpeed * Main.rand.NextFloat(0.8f, 1.25f));
                }
            }
        }
    }
}


