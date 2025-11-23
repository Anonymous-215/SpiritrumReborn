using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Projectiles.SandstoneLauncherProjectiles
{
    public class Sandstone : ModProjectile
    {
    public override string Texture => "SpiritrumReborn/Content/Projectiles/Sandstone";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1; 
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.knockBack = 2f;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }

    public class EbonSandstone : ModProjectile
    {
    public override string Texture => "SpiritrumReborn/Content/Projectiles/EbonSandstone";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.knockBack = 2f;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.damage = (int)(Projectile.damage * 1.10f);
        }
    }

    public class CrimSandstone : ModProjectile
    {
    public override string Texture => "SpiritrumReborn/Content/Projectiles/CrimSandstone";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.knockBack = 2f;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextFloat() < 0.10f)
            {
                int critDamage = (int)(damageDone * 1.5f);
                Player owner = Main.player[Projectile.owner];
                bool oldImmune = target.immune[Projectile.owner] > 0;
                int oldImmuneTime = target.immune[Projectile.owner];
                target.immune[Projectile.owner] = 0;
                NPC.HitInfo hitInfo = new NPC.HitInfo()
                {
                    Damage = critDamage,
                    Knockback = Projectile.knockBack,
                    HitDirection = Projectile.direction,
                    Crit = true,
                    DamageType = DamageClass.Ranged,
                };
                target.StrikeNPC(hitInfo);
                target.immune[Projectile.owner] = oldImmune ? oldImmuneTime : 0;
            }
        }
    }

    public class PearlSandstone : ModProjectile
    {
    public override string Texture => "SpiritrumReborn/Content/Projectiles/PearlSandstone";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.knockBack = 2f;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 1.10f; 
            Projectile.penetrate += 1; 
        }
    }
}


