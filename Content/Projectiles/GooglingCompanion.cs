using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
// dependency on donor 'GoogEnchantmentPlayer' removed; no external using required

namespace SpiritrumReborn.Content.Projectiles
{

    public class GooglingCompanion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.tileCollide = false; 
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10; 
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner == null || !owner.active)
            {
                Projectile.Kill();
                return;
            }

            // The original mod used a separate GoogEnchantmentPlayer type from the
            // donor mod. That dependency is not present here, so allow the companion
            // by default and use conservative defaults for optional flags.
            bool galacticGoog = false;
            bool forceOfTravels = false;

            Vector2 idlePosition = owner.Center + new Vector2(0, -40);
            float speed = 6f;
            if (galacticGoog) speed = 6f * 1.60f;
            if (forceOfTravels) speed = 6f * 2.00f;

            Vector2 toIdle = idlePosition - Projectile.Center;
            if (toIdle.Length() > 8f)
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Normalize(toIdle) * speed, 0.12f);
            else
                Projectile.velocity *= 0.98f;

            int target = -1;
            float bestDist = 300f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    float dist = Vector2.Distance(npc.Center, Projectile.Center);
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        target = i;
                    }
                }
            }

            if (target != -1)
            {
                Vector2 dash = Main.npc[target].Center - Projectile.Center;
                dash.Normalize();
                float dashSpeed = 10f;
                if (galacticGoog) dashSpeed = 10f * 1.60f;
                if (forceOfTravels) dashSpeed = 10f * 2.00f;
                dash *= dashSpeed;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, dash, 0.12f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool galacticGoog2 = false;
            bool forceOfTravels2 = false;
            if (galacticGoog2)
            {
                target.StrikeNPC(new NPC.HitInfo() { Damage = 0, Knockback = 2f, HitDirection = hit.HitDirection });
            }
            if (forceOfTravels2)
            {
                target.StrikeNPC(new NPC.HitInfo() { Damage = 0, Knockback = 6f, HitDirection = hit.HitDirection });
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
        }
    }
}


