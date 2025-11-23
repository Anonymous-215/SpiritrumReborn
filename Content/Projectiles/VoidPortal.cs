using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

using SpiritrumReborn.Content.Projectiles;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
    public class VoidPortal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            var vh = player.GetModPlayer<global::SpiritrumReborn.Players.VoidHunterEnchPlayer>();
            var fot = player.GetModPlayer<global::SpiritrumReborn.Players.ForceOfTravelsPlayer>();
            if (vh == null || (!vh.voidHunterEnchanted && (fot == null || !fot.forceOfTravels)))
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center + new Vector2(0, -64);

            Projectile.rotation += 0.32f;
            if (++Projectile.ai[0] >= 30)
            {
                Projectile.ai[0] = 0;
                NPC target = FindTarget(player, 1000f);
                if (target != null && Main.myPlayer == Projectile.owner)
                {
                    Vector2 toTarget = target.Center - Projectile.Center;
                    toTarget.Normalize();
                    toTarget *= 12f;
                    int bolt = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, toTarget, ModContent.ProjectileType<VoidBolts>(), Projectile.damage, 3f, Projectile.owner);
                    if (bolt >= 0)
                    {
                        Main.projectile[bolt].friendly = true;
                        Main.projectile[bolt].hostile = false;
                        Main.projectile[bolt].DamageType = DamageClass.Summon;
                    }
                }
            }
        }

    private static Microsoft.Xna.Framework.Graphics.Texture2D cachedTexture;
    private static Vector2 cachedOrigin;
    public bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            if (cachedTexture == null)
            {
                cachedTexture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
                cachedOrigin = new Vector2(cachedTexture.Width / 2, cachedTexture.Height / 2);
            }
            float pulse = 1f + 0.15f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4f + Projectile.whoAmI);
            Color glowColor = Color.MediumPurple * 0.9f;
            spriteBatch.Draw(
                cachedTexture,
                Projectile.Center - Main.screenPosition,
                null,
                glowColor,
                Projectile.rotation,
                cachedOrigin,
                pulse * 1.35f,
                Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                cachedTexture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                cachedOrigin,
                pulse,
                Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
                0f
            );
            return false;
        }

        private NPC FindTarget(Player player, float maxDetect)
        {
            NPC target = null;
            float closest = maxDetect;
            for (int n = 0; n < Main.maxNPCs; n++)
            {
                NPC npc = Main.npc[n];
                if (npc.CanBeChasedBy() && !npc.friendly)
                {
                    float dist = Vector2.Distance(player.Center, npc.Center);
                    if (dist < closest && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
                    {
                        closest = dist;
                        target = npc;
                    }
                }
            }
            return target;
        }
    }
}


