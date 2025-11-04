using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PalladiumShockwave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            int radius = 40; 
            Projectile.width = radius * 2;
            Projectile.height = radius * 2;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1; 
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 8; 
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.ignoreWater = true;
            Projectile.alpha = 150;
        }

        public override void AI()
        {
            if (Projectile.owner >= 0 && Projectile.owner < Main.maxPlayers)
            {
                Player owner = Main.player[Projectile.owner];
                if (owner == null || !owner.active)
                {
                    Projectile.Kill();
                    return;
                }

                Projectile.Center = owner.Center;
            }

            if (!Projectile.localAI[0].Equals(1f))
            {
                if (Projectile.owner >= 0 && Projectile.owner < Main.maxPlayers && Main.myPlayer == Projectile.owner)
                {
                    int volcanoDamage = Projectile.damage;
                    int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.Volcano, volcanoDamage, Projectile.knockBack, Projectile.owner);
                    if (id >= 0 && id < Main.maxProjectiles)
                    {
                        Projectile vol = Main.projectile[id];
                        vol.scale *= 2f;
                        vol.width = (int)(vol.width * 2f);
                        vol.height = (int)(vol.height * 2f);
                        vol.position = Projectile.Center - new Vector2(vol.width / 2, vol.height / 2);
                        vol.netUpdate = true;
                    }
                }
                Projectile.localAI[0] = 1f; 
            }

            float t = Projectile.timeLeft / 8f;
            Projectile.scale = 1f + (1f - t) * 0.25f;
            Projectile.alpha = (int)(150 * t);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
    }
}


