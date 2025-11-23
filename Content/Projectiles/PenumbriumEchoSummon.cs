using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PenumbriumEchoSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = false;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240; 
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 0;
        }

        private int orbitIndex = -1;
    private float orbitRadius = 40f;
    private float orbitSpeed = 0.05f;
    private int fireTimer = 0;
    private const int FireInterval = 40;

    private static Texture2D cachedTexture;
    private static Vector2 cachedOrigin;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            // PenumbriumEnchantPlayer not present in this mod port; allow summon by default.

            if (orbitIndex == -1)
            {
                orbitIndex = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == Projectile.owner && p.type == Projectile.type && p.whoAmI < Projectile.whoAmI)
                        orbitIndex++;
                }
            }

            float speedMult = 1f;
            int fireInterval = FireInterval;
            int maxEchoes = 3; // fallback default

            float globalAngle = Main.GameUpdateCount * orbitSpeed * speedMult;
            float angle = globalAngle + (MathHelper.TwoPi / maxEchoes) * orbitIndex;
            Vector2 target = player.Center + angle.ToRotationVector2() * orbitRadius;

            Projectile.velocity = (target - Projectile.Center) * 0.15f;
            Projectile.Center += Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation();

            fireTimer++;
            if (fireTimer >= fireInterval)
            {
                fireTimer = 0;
                int targetNPC = -1;
                float bestDist = 600f;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(this) && !npc.friendly)
                    {
                        float dist = Vector2.Distance(npc.Center, Projectile.Center);
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            targetNPC = i;
                        }
                    }
                }

                if (targetNPC != -1)
                {
                    Vector2 dir = Main.npc[targetNPC].Center - Projectile.Center;
                    dir.Normalize();
                    dir *= 8f;
                    int boltDamage = 20;
                    if (player.GetModPlayer<ForceOfTravelsPlayer>().forceOfTravels)
                        boltDamage = (int)(boltDamage * 3.5f); 
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, dir,
                        ModContent.ProjectileType<PenumbriumEchoBolt>(), boltDamage, 1f, Projectile.owner, targetNPC);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (cachedTexture == null)
            {
                cachedTexture = ModContent.Request<Texture2D>("SpiritrumReborn/Content/Projectiles/PenumbriumEchoSummon").Value;
                cachedOrigin = new Vector2(cachedTexture.Width * 0.5f, cachedTexture.Height * 0.5f);
            }

            Main.spriteBatch.Draw(cachedTexture, Projectile.Center - Main.screenPosition, null, Color.White * 0.9f, Projectile.rotation, cachedOrigin, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}


