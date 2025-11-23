using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class FrozoFlake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 17;
            Projectile.height = 17;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2; 
            Projectile.timeLeft = 600; 
            Projectile.alpha = 100; 
            Projectile.light = 0.5f; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4; 
        }

        public override void AI()
        {
            Projectile.rotation += 0.4f;
            if (Main.rand.NextBool(3)) 
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceTorch, 0f, 0f, 100, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
            float wobbleSpeed = 0.2f; 
            float wobbleAmplitude = 1.5f; 
            Projectile.ai[0]++;
            float wobbleOffset = (float)Math.Sin(Projectile.ai[0] * wobbleSpeed) * wobbleAmplitude;
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X).SafeNormalize(Vector2.Zero);
            Projectile.position += perpendicular * wobbleOffset;
            if (Projectile.ai[0] > 30) 
            {
                Projectile.velocity *= 0.995f; 
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 180);
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(4f, 4f);
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceRod, speed.X, speed.Y, 0, default, Main.rand.NextFloat(1f, 1.5f));
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 8; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2f, 2f);
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceRod, speed.X, speed.Y, 0, default, Main.rand.NextFloat(0.8f, 1.2f));
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            return true;
        }
        private static Texture2D cachedTexture;
        private static Rectangle cachedSourceRectangle;
        private static Vector2 cachedOrigin;
        public override bool PreDraw(ref Color lightColor)
        {
            if (cachedTexture == null)
            {
                cachedTexture = TextureAssets.Projectile[Projectile.type].Value;
                cachedSourceRectangle = new Rectangle(0, 0, cachedTexture.Width, cachedTexture.Height);
                cachedOrigin = cachedSourceRectangle.Size() / 2f;
            }
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Color trailColor = Color.Cyan * 0.5f;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPosTrail = Projectile.oldPos[k] + Projectile.Size / 2f - Main.screenPosition;
                float trailFade = (float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length;
                Main.spriteBatch.Draw(cachedTexture, drawPosTrail, cachedSourceRectangle, trailColor * trailFade, 
                    Projectile.rotation, cachedOrigin, Projectile.scale * (0.8f + 0.2f * trailFade), SpriteEffects.None, 0);
            }
            Color color = Projectile.GetAlpha(lightColor); 
            Main.spriteBatch.Draw(cachedTexture, drawPos, cachedSourceRectangle, color, 
                Projectile.rotation, cachedOrigin, Projectile.scale, SpriteEffects.None, 0);
            return false; 
        }
    }
}


