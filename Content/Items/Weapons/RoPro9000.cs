using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using System;


namespace SpiritrumReborn.Content.Items.Weapons
{
    public class RoPro9000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 64;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 5);

            Item.damage = 225;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 36;
            Item.knockBack = 14f;
            Item.crit = -1000;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ProjectileID.SandnadoFriendly;
            Item.shootSpeed = 0.1f;
            Item.noMelee = true;
            Item.scale = 0.25f;
            Item.useTurn = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 spawnPos = Main.MouseWorld;
            int projType = ModContent.ProjectileType<RoPro9000_Telegraph>();
            int index = Projectile.NewProjectile(source, spawnPos, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            Projectile tele = Main.projectile[index];
            tele.ai[0] = damage;
            tele.ai[1] = knockback;
            tele.timeLeft = Main.rand.Next(10, 21);
            tele.netUpdate = true;
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }
    }

    public class RoPro9000_Telegraph : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 15;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Vector2 center = Projectile.Center;
            float explosionRadius = 96f;
            int ringCount = 6;
            double time = Main.GlobalTimeWrappedHourly * 2.0;
            for (int i = 0; i < ringCount; i++)
            {
                float angle = (float)(time + i * (MathHelper.TwoPi / ringCount));
                Vector2 pos = center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * explosionRadius;
                Dust d = Dust.NewDustPerfect(pos, DustID.Electric, Vector2.Zero, 0, new Color(255, 50, 50, 150), 0.6f);
                d.noGravity = true;
                d.noLight = false;
            }

            if (Main.rand.NextBool(6))
            {
                float offsetAngle = Main.rand.NextFloat(MathHelper.TwoPi);
                Vector2 pos2 = center + new Vector2((float)Math.Cos(offsetAngle), (float)Math.Sin(offsetAngle)) * (explosionRadius * Main.rand.NextFloat(0.9f, 1f));
                Dust d2 = Dust.NewDustPerfect(pos2, DustID.Smoke, Vector2.Zero, 0, Color.White, Main.rand.NextFloat(0.9f, 1.2f));
                d2.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            Vector2 spawnPos = Projectile.Center;
            float explosionRadius = 96f;

            for (int i = 0; i < 120; i++)
            {
                float dustDistance = Main.rand.NextFloat() * explosionRadius;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );

                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(2f, 8f);

                Dust dust = Dust.NewDustPerfect(
                    dustPosition,
                    DustID.WhiteTorch,
                    speed,
                    0,
                    Color.White,
                    Main.rand.NextFloat(1.5f, 2.5f)
                );
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
            }

            for (int i = 0; i < 60; i++)
            {
                float dustDistance = Main.rand.NextFloat() * explosionRadius * 0.8f;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );

                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(3f, 10f);

                Dust dust = Dust.NewDustPerfect(
                    dustPosition,
                    DustID.Smoke,
                    speed * 0.8f,
                    0,
                    Color.White,
                    Main.rand.NextFloat(2f, 3f)
                );
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
            }

            for (int i = 0; i < 40; i++)
            {
                float dustDistance = Main.rand.NextFloat() * explosionRadius * 0.7f;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );

                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(2f, 6f);

                Dust dust = Dust.NewDustPerfect(
                    dustPosition,
                    DustID.SparkForLightDisc,
                    speed,
                    0,
                    Color.White,
                    Main.rand.NextFloat(1f, 1.5f)
                );
                dust.noGravity = true;
                dust.fadeIn = 0.8f;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && Projectile.owner != Main.myPlayer)
                return;

            Player owner = Main.player[Projectile.owner];
            if (!owner.active)
                return;

            int baseDamage = (int)Projectile.ai[0];
            float baseKnock = Projectile.ai[1];
            int targets = 0;

            for (int i = 0; i < Main.maxNPCs && targets < 10; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || npc.friendly || npc.townNPC || npc.dontTakeDamage)
                    continue;

                float dist = Vector2.Distance(npc.Center, spawnPos);
                if (dist > explosionRadius)
                    continue;

                float multiplier = 1f - 0.9f * (dist / explosionRadius);
                if (multiplier < 0.1f)
                    multiplier = 0.1f;

                int adjusted = Math.Max(1, (int)(baseDamage * multiplier));
                bool crit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Magic) * 0.01f;
                owner.ApplyDamageToNPC(npc, adjusted, baseKnock, owner.direction, crit: crit);

                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.WhiteTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 0, default, 1.5f);
                }

                targets++;
            }

            SoundEngine.PlaySound(SoundID.Item14, spawnPos);
        }
    }
}
