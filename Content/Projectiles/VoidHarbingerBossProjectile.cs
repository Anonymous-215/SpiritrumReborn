using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
    public class VoidHarbingerBossProjectile : ModProjectile
    {        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4; 
            
            ProjectileID.Sets.TrailCacheLength[Type] = 10; 
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            Projectile.alpha = 100; 
            Projectile.light = 0.5f; 
            Projectile.extraUpdates = 1; 
        }
    private static Texture2D cachedTexture;
        
        public override Color? GetAlpha(Color lightColor)
        {
            switch ((int)Projectile.ai[0])
            {
                case 0: 
                    return new Color(200, 50, 255, 200);
                    
                case 1: 
                    return new Color(100, 50, 255, 200);
                    
                case 2: 
                    return new Color(150, 20, 220, 200);
                    
                case 3: 
                    float pulseRate = 0.05f;
                    float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * pulseRate * 20f) * 0.2f + 0.8f;
                    return new Color(100 * pulse, 10 * pulse, 150 * pulse, 200);
                    
                default:
                    return new Color(180, 50, 255, 200);
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            if ((int)Projectile.ai[0] > 0) 
            {
                if (cachedTexture == null)
                {
                    cachedTexture = ModContent.Request<Texture2D>("SpiritrumReborn/Content/Projectiles/VoidHarbingerBossProjectile").Value;
                }

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

                Vector2 drawOrigin = new Vector2(cachedTexture.Width / 2, cachedTexture.Height / Main.projFrames[Projectile.type] / 2);
                int frameHeight = cachedTexture.Height / Main.projFrames[Projectile.type];

                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    if (Projectile.oldPos[i] == Vector2.Zero)
                        continue;

                    float progress = 1f - (float)i / Projectile.oldPos.Length;
                    Color trailColor = GetAlpha(lightColor).Value * progress * 0.5f;
                    trailColor.A = 0;

                    float scale = Projectile.scale * (0.6f + 0.4f * progress);

                    Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                    Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, cachedTexture.Width, frameHeight);

                    Main.spriteBatch.Draw(
                        cachedTexture,
                        drawPos,
                        sourceRect,
                        trailColor,
                        Projectile.oldRot[i],
                        drawOrigin,
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            }
            
            return true;
        }
        
        public override void PostDraw(Color lightColor)
        {
            if ((int)Projectile.ai[0] > 0)
            {
                if (cachedTexture == null)
                {
                    cachedTexture = ModContent.Request<Texture2D>("SpiritrumReborn/Content/Projectiles/VoidHarbingerBossProjectile").Value;
                }

                int frameHeight = cachedTexture.Height / Main.projFrames[Projectile.type];
                Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, cachedTexture.Width, frameHeight);
                Vector2 drawOrigin = new Vector2(cachedTexture.Width / 2, frameHeight / 2);
                
                Color glowColor = Color.White;
                switch ((int)Projectile.ai[0])
                {
                    case 1: 
                        glowColor = new Color(100, 100, 255, 0) * 0.5f;
                        break;
                        
                    case 2: 
                        glowColor = new Color(200, 50, 255, 0) * 0.5f;
                        break;
                        
                    case 3: 
                        float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.3f + 0.7f;
                        glowColor = new Color(100, 10, 150, 0) * (0.5f * pulse);
                        break;
                }
                
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
                
                Main.spriteBatch.Draw(
                    cachedTexture,
                    Projectile.Center - Main.screenPosition,
                    sourceRect,
                    glowColor,
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale * 1.2f,
                    SpriteEffects.None,
                    0f
                );
                
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            }
        }public override void AI()
        {
            switch ((int)Projectile.ai[0])
            {
                case 0: 
                    DefaultProjectileBehavior();
                    break;
                
                case 1: 
                    OrbitingProjectileBehavior();
                    break;
                
                case 2: 
                    HomingProjectileBehavior();
                    break;
                
                case 3: 
                    ImplosionProjectileBehavior();
                    break;
                
                default:
                    DefaultProjectileBehavior();
                    break;
            }
            
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        
        private void DefaultProjectileBehavior()
        {
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }

            Lighting.AddLight(Projectile.Center, 0.4f, 0.1f, 0.6f);
            
            Projectile.rotation += 0.2f;
        }
        
        private void OrbitingProjectileBehavior()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                Projectile.localAI[0] = Projectile.Center.X;
                Projectile.localAI[1] = Projectile.Center.Y;
            }
            
            Vector2 orbitCenter = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            Vector2 toCenter = orbitCenter - Projectile.Center;
            float distance = toCenter.Length();
            toCenter.Normalize();
            
            float orbitSpeed = 0.05f;
            Vector2 orbitalVelocity = toCenter.RotatedBy(MathHelper.PiOver2) * distance * orbitSpeed;
            
            Projectile.velocity = orbitalVelocity + toCenter * 0.05f;
            
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
            }
            
            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.8f);
            
            Projectile.rotation += 0.3f;
        }
          private void HomingProjectileBehavior()
        {
            
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 0, default, 1.3f);
                dust.noGravity = true;
            }
            
            float pulse = (float)Math.Sin(Projectile.timeLeft * 0.1f) * 0.2f + 0.6f;
            Lighting.AddLight(Projectile.Center, 0.2f, 0.1f, pulse);
            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        
        private void ImplosionProjectileBehavior()
        {
            if (Projectile.timeLeft < 60)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                    return;
                }
                
                Projectile.velocity *= 0.97f;
            }
            
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity = -Projectile.velocity * 0.5f;
            }
            
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Shadowflame,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }
            
            Lighting.AddLight(Projectile.Center, 0.7f, 0.1f, 0.9f);
            
            Projectile.rotation += 0.4f;
        }        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            switch ((int)Projectile.ai[0])
            {
                case 0: 
                    target.AddBuff(BuffID.Darkness, 240); 
                    target.AddBuff(BuffID.Slow, 120); 
                    break;
                    
                case 1: 
                    target.AddBuff(BuffID.Confused, 180); 
                    target.AddBuff(BuffID.Slow, 120); 
                    break;
                    
                case 2: 
                    target.AddBuff(BuffID.Darkness, 300); 
                    target.AddBuff(BuffID.Weak, 600); 
                    break;
                    
                case 3: 
                    target.AddBuff(BuffID.Darkness, 300); 
                    target.AddBuff(BuffID.Slow, 300); 
                    target.AddBuff(BuffID.Blackout, 180); 
                    break;
                    
                default:
                    target.AddBuff(BuffID.Darkness, 240); 
                    break;
            }
            
            for (int i = 0; i < 15; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                dust.noGravity = true;
            }
            
            if (Projectile.ai[0] == 2 || Projectile.ai[0] == 3)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                    Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Shadowflame, velocity.X, velocity.Y, 0, default, 1.8f);
                    dust.noGravity = true;
                }
                
                SoundEngine.PlaySound(SoundID.Item73, target.position);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item101, target.position);
            }
        }        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool destroyProjectile = true;
            
            switch ((int)Projectile.ai[0])
            {
                case 0: 
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                case 1: 
                    if (Math.Abs(oldVelocity.X - Projectile.velocity.X) > 0.1f)
                    {
                        Projectile.velocity.X = -oldVelocity.X * 0.8f;
                    }
                    
                    if (Math.Abs(oldVelocity.Y - Projectile.velocity.Y) > 0.1f)
                    {
                        Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
                    }
                    
                    SoundEngine.PlaySound(SoundID.Item56, Projectile.position);
                    
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(3f, 3f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.2f);
                        dust.noGravity = true;
                    }
                    
                    destroyProjectile = false;
                    
                    Projectile.timeLeft = Math.Min(Projectile.timeLeft, 60);
                    break;
                    
                case 2: 
                    SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                    
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.8f);
                        dust.noGravity = true;
                    }
                    
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                case 3: 
                    SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
                    
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Vector2 dustPos = Projectile.Center + Main.rand.NextVector2Circular(20f, 20f);
                        Vector2 dustVel = (Projectile.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleCrystalShard, dustVel, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                default:
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
            }
            
            return destroyProjectile;
        }
    }
}


