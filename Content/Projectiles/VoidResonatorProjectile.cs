using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;

namespace SpiritrumReborn.Content.Projectiles
{
    public class VoidResonatorProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3; 
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180; 
            Projectile.light = 0.5f; 
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6; 
            
            Projectile.alpha = 100; 
            Projectile.extraUpdates = 1; 
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
                dust.velocity *= 0.3f;
            }

            Lighting.AddLight(Projectile.Center, 0.5f, 0.1f, 0.7f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    target.position,
                    target.width,
                    target.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
            }
            {
            DoExplosion();
            }
            
            if (Main.rand.NextBool(3)) 
            {
                target.AddBuff(BuffID.ShadowFlame, 180); 
            }
            
            SoundEngine.PlaySound(SoundID.Item10, target.position);
            
            Projectile.penetrate--;
            
            if (Projectile.penetrate <= 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(
                        Projectile.Center - new Vector2(20, 20),
                        40, 40,
                        DustID.PurpleTorch,
                        0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2Circular(8f, 8f);
                }
                
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate > 1)
            {
                Projectile.penetrate--;
                
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X * 0.6f;
                }
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y * 0.6f;
                }
                
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(
                        Projectile.position,
                        Projectile.width,
                        Projectile.height,
                        DustID.PurpleCrystalShard,
                        0f, 0f, 0, default, 1.2f);
                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
                }
                
                return false; 
            }
            
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
            }
            
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            
            return true; 
        }
        
        

        private void DoExplosion()
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 150, default, 1.5f);
            }
            for (int i = 0; i < 10; i++)
            {
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity * 0.2f, Main.rand.Next(61, 64));
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int splashRadius = 64; 
            int splashDamage = (int)(Projectile.damage * 0.75f); 
            Player owner = Main.player[Projectile.owner];
            bool isCrit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Magic) / 100f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < splashRadius)
                {
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = splashDamage,
                        Knockback = 4f,
                        HitDirection = Projectile.direction,
                        Crit = isCrit,
                        DamageType = DamageClass.Magic,
                    };
                    bool oldImmune = npc.immune[Projectile.owner] > 0;
                    int oldImmuneTime = npc.immune[Projectile.owner];
                    npc.immune[Projectile.owner] = 0;                    npc.StrikeNPC(hitInfo);
                    npc.immune[Projectile.owner] = oldImmune ? oldImmuneTime : 0;
                }
            }
        }

        [System.Obsolete]
        public override void OnKill(int timeLeft)
        {
            DoExplosion();
        }

        }
    }

