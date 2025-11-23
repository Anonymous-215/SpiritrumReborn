using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Projectiles
{
    public class FrozoniteAuraTravel : ModProjectile
    {
    private bool damageBoosted = false;
    public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.hide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60; 
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.20f; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
        if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            if (!damageBoosted)
            {
                Projectile.damage = (int)(Projectile.damage * 2.2f);
                damageBoosted = true;
            }

            int orbitIndex = (int)Projectile.ai[0]; 
            int orbitCount = 5;
            float orbitRadius = 180f;
            float orbitSpeed = 0.05f; 
            float angle = Main.GameUpdateCount * orbitSpeed + MathHelper.TwoPi * orbitIndex / (float)orbitCount;
            Vector2 offset = orbitRadius * new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle));
            Projectile.Center = player.Center + offset;

            Projectile.rotation += 0.05f;


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 180);
        }



    }
}


