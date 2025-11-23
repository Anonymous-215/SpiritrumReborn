using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Buffs;

namespace SpiritrumReborn.Content.Projectiles
{
    public class PascaliteSunflowerSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 2; 
            Projectile.minion = true; 
            Projectile.minionSlots = 0.5f;
            Projectile.netImportant = true;
            Projectile.originalDamage = Projectile.damage;
        }

        public override void AI()
        {
            if (!Main.projectile.IndexInRange(Projectile.whoAmI))
                return;

            if (Projectile.owner < 0 || Projectile.owner >= Main.maxPlayers)
            {
                Projectile.Kill();
                return;
            }

            Player player = Main.player[Projectile.owner];
            if (player == null || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;
            int buffType = ModContent.BuffType<global::SpiritrumReborn.Content.Buffs.PascaliteSunflowerBuff>();
            if (buffType > 0)
                player.AddBuff(buffType, 180);

            if (Projectile.ai[0] == 0f)
            {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item44, Projectile.position);
                Projectile.ai[0] = 1f; 
            }

            int count = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p != null && p.active && p.owner == Projectile.owner && p.type == Projectile.type)
                    count++;
            }

            int index = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p == null) continue;
                if (p.whoAmI == Projectile.whoAmI)
                    break;
                if (p.active && p.owner == Projectile.owner && p.type == Projectile.type)
                    index++;
            }

            float radius = 48f + count * 6f;
            float angle = MathHelper.TwoPi * index / System.Math.Max(1, count) + Main.GlobalTimeWrappedHourly * 0.9f;
            Vector2 offset = new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle)) * radius;
            Vector2 desired = player.Center + offset;
            Projectile.Center = Vector2.Lerp(Projectile.Center, desired, 0.28f);

            if (Main.rand.NextBool(30))
            {
                int orbType = ModContent.ProjectileType<global::SpiritrumReborn.Content.Projectiles.PascaliteOrb>();
                Vector2 vel = (player.Center - Projectile.Center).SafeNormalize(Vector2.UnitY) * 6.5f;
                int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, orbType, Projectile.damage * 3 / 4, Projectile.knockBack, Projectile.owner);
                if (Main.projectile.IndexInRange(id))
                {
                    Main.projectile[id].netUpdate = true;
                }
            }
        }
    }
}


