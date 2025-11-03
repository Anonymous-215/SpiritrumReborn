using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
public class CopiumTrident : ModProjectile
{
    private static readonly Vector2 TipOffset = new Vector2(48f, 0f); 
        public override void SetDefaults()
        {
            Projectile.width = (int)(31f * 1.5f);
            Projectile.height = (int)(18f * 1.5f);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.aiStyle = 19; 
            AIType = ProjectileID.Spear;
        }

        protected virtual float HoldoutRangeMin => 4f; //this is the range of the spear
        protected virtual float HoldoutRangeMax => 24f;
    }
}

