using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class CalamityRevolverSpin : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            // Make projectile fully invisible by default; used as a countdown indicator
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            var modPlayer = player.GetModPlayer<Players.CalamityRevolverPlayer>();

            // Position the spin in front of the player and slightly above their feet
            float forward = player.direction >= 0 ? 1f : -1f;
            Vector2 desired = player.MountedCenter + new Vector2(forward * 36f, -8f);
            Projectile.Center = desired;

            // Rotate while following
            Projectile.rotation += 0.6f * forward;

            // Keep the projectile alive while the player still has spin charges
            if (modPlayer.spinCharges > 0)
            {
                Projectile.timeLeft = 2; // persist
            }
            else
            {
                // allow a short fade-out if charges are gone
                if (Projectile.timeLeft > 10)
                    Projectile.timeLeft = 10;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;

            // Draw the spin sprite at the projectile center
            var tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = tex.Size() * 0.5f;
            // Only draw the visual while not fully transparent
            if (Projectile.alpha < 255)
                spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White * ((255 - Projectile.alpha) / 255f), Projectile.rotation, origin, 1f, SpriteEffects.None, 0f);

            // Draw cooldown countdown under the owning player (seconds, red)
            Player player = Main.player[Projectile.owner];
            var modPlayer = player.GetModPlayer<Players.CalamityRevolverPlayer>();
            if (modPlayer.spinCooldown > 0 && player.whoAmI == Main.myPlayer)
            {
                int seconds = (modPlayer.spinCooldown + 59) / 60; // round up
                string text = seconds.ToString();
                var font = FontAssets.MouseText.Value;
                Vector2 textSize = font.MeasureString(text);
                Vector2 textPos = player.Bottom - Main.screenPosition + new Vector2(0f, 10f);
                // center the text under the player
                Vector2 drawPos = textPos - new Vector2(textSize.X * 0.5f, 0f);
                Utils.DrawBorderString(spriteBatch, text, drawPos, Color.Red);
            }

            return false; // we've handled drawing
        }
    }
}
