using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
	public class EnergySphere : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240; 
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 3; 
		}

		public override void AI()
		{
			Projectile.rotation += 0.8f * (float)Math.PI; 
			Projectile.velocity *= 1.01f; 
		}


	}
}




