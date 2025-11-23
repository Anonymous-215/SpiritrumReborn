using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles.Misc
{
    public class CrimsonDart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void SetDefaults()
        {            Projectile.width = 10; 
            Projectile.height = 10; 
            Projectile.alpha = 120; 
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Summon; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 180; 
            Projectile.light = 0.5f; 
            Projectile.ignoreWater = true; 
            Projectile.tileCollide = true; 
            Projectile.extraUpdates = 1; 

            Projectile.aiStyle = -1; 
        }        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position); 
            }
            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(2)) 
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height,
                    DustID.Blood, 
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }

            int targetIndex = (int)Projectile.ai[0];
            float homingStrength = Projectile.ai[1];
            
            if (targetIndex >= 0 && targetIndex < Main.maxNPCs)
            {
                NPC target = Main.npc[targetIndex];
                
                if (target != null && target.active && !target.friendly && !target.dontTakeDamage)
                {
                    Vector2 toTarget = target.Center - Projectile.Center;
                    float distanceToTarget = toTarget.Length();
                    
                    if (distanceToTarget < 500f)
                    {
                        toTarget.Normalize();
                        
                        float homingFactor = MathHelper.Lerp(0.08f, 0.2f, Math.Min(1f, homingStrength));
                        
                        float speed = Projectile.velocity.Length();
                        Projectile.velocity = Vector2.Normalize(Projectile.velocity + toTarget * homingFactor) * speed;
                    }
                }
            }
            
            Projectile.velocity += new Vector2(
                Main.rand.NextFloat(-0.05f, 0.05f),
                Main.rand.NextFloat(-0.05f, 0.05f));
        }        [System.Obsolete]
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath13, Projectile.position);

            for (int i = 0; i < 15; i++) 
            {
                Vector2 velocity = Main.rand.NextVector2Circular(3f, 3f); 
                
                Dust dust = Dust.NewDustDirect(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height,
                    DustID.Blood, 
                    velocity.X, 
                    velocity.Y, 
                    0, 
                    default, 
                    Main.rand.NextFloat(1.0f, 1.8f)); 
                
                dust.noGravity = i % 3 != 0; 
            }
            
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(Projectile.GetSource_Death(), 
                    Projectile.Center, 
                    Projectile.velocity * 0.5f, 
                    Main.rand.Next(61, 64)); 
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 50, 50, 255) * ((255 - Projectile.alpha) / 255f);
        }

        private static Texture2D cachedTexture;
        private static Vector2 cachedOrigin;
        public override bool PreDraw(ref Color lightColor)
        {
            if (cachedTexture == null)
            {
                Main.instance.LoadProjectile(Projectile.type);
                cachedTexture = TextureAssets.Projectile[Projectile.type].Value;
                cachedOrigin = new Vector2(cachedTexture.Width * 0.5f, Projectile.height * 0.5f);
            }

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + cachedOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length);
                Main.EntitySpriteDraw(cachedTexture, drawPos, null, color, Projectile.rotation, cachedOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.NPCHit13, target.position);
            
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    target.position,
                    target.width,
                    target.height,
                    DustID.Blood,
                    Projectile.velocity.X * 0.5f,
                    Projectile.velocity.Y * 0.5f,
                    0,
                    default,
                    1.2f);
                    
                dust.velocity *= 0.8f;
                dust.noGravity = true;
            }
            if (Main.rand.NextBool(3)) 
            {
                target.AddBuff(BuffID.Bleeding, 180); 
            }
        }
    }
}


