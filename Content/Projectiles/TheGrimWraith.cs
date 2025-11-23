using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace SpiritrumReborn.Content.Projectiles
{
    public class TheGrimWraith : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 36000;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (!target.friendly && target.active && !target.dontTakeDamage)
                return true;
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 cursorWorld = Main.MouseWorld;

            if (player.HeldItem.type != ModContent.ItemType<global::SpiritrumReborn.Content.Items.Weapons.TheGrimWraith>())
            {
                Projectile.Kill();
                return;
            }

            float orbitRadius = 32f;
            float spinSpeed = 0.25f; 
            Projectile.ai[0] += spinSpeed;
            float angle = Projectile.ai[0];
            Vector2 offset = new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle)) * orbitRadius;
            Projectile.Center = cursorWorld + offset;

            Projectile.rotation += 0.4f;

            if (player.channel && !player.dead && !player.CCed)
                Projectile.timeLeft = 2;
            else
                Projectile.Kill();
            }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() < 0.20f) 
            {
                player.statLife += 2;
                player.HealEffect(2, true);
            }
        }
        }
    }


