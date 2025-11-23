using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Projectiles
{
    public class NoDeerClawProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 30;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[1] = Main.rand.NextFloat(0f, MathHelper.TwoPi);
                Projectile.localAI[0] = 1f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + Projectile.localAI[1];
            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone);
        }
    }
}


