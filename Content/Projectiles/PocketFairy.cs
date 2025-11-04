using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.DataStructures;



namespace SpiritrumReborn.Content.Projectiles
{
    public class PocketFairy : ModProjectile
    {
        public override void SetStaticDefaults() 
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; 
            Projectile.extraUpdates = 1; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.localNPCHitCooldown = 1;
        }
        public override void AI() 
        {
            { 
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            Projectile.frameCounter++; 
            if (Projectile.frameCounter >= 6) 
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 20); 
		}
    }
}




