using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Buffs;
using SpiritrumReborn.Content.Projectiles.Misc;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Projectiles.Minions
{
    public class NamelessParasiteMinion : ModProjectile
    {
        private const float ORBIT_DISTANCE = 32f; 
        private const float ATTACK_INTERVAL = 78f; 
        private const float MAX_ATTACK_RANGE = 500f; 
        private const float IDLE_ROTATION_SPEED = 0.03f; 
        private const float HOMING_STRENGTH = 0.08f; 
        
        private float attackCooldown;
        private float orbitAngle;
        private NPC targetNPC;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; 
            Main.projFrames[Projectile.type] = 4; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false; 
            Projectile.friendly = true; 
            Projectile.DamageType = DamageClass.Summon; 
            Projectile.minionSlots = 1f; 
            Projectile.originalDamage = Projectile.damage; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 2; 
            Projectile.netImportant = true; 
            
            attackCooldown = 0f;
            orbitAngle = Main.rand.NextFloat() * MathHelper.TwoPi; 
        }

        public override bool? CanCutTiles()
        {
            return false; 
        }

        public override bool MinionContactDamage()
        {
            return false; 
        }        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!owner.active || owner.dead || !owner.HasBuff(ModContent.BuffType<NamelessParasiteBuff>()))
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;
            // MinionCounterPlayer was a dependency from the donor mod; skip increment here.
            // If a proper counter exists in this mod, it should increment it instead.

            Animate();

            FindTarget();

            attackCooldown -= 1f;

            if (targetNPC != null && targetNPC.active && !targetNPC.dontTakeDamage && attackCooldown <= 0)
            {
                Attack();
                attackCooldown = ATTACK_INTERVAL;
            }

            UpdatePosition(owner);
        }

        private void Animate()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10) 
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }
            
            Projectile.rotation = Projectile.velocity.X * 0.05f;
        }

        private void FindTarget()
        {
            if (targetNPC != null && (!targetNPC.active || targetNPC.friendly || targetNPC.dontTakeDamage))
            {
                targetNPC = null;
            }

            if (targetNPC == null)
            {
                float closestDistance = MAX_ATTACK_RANGE;
                
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    
                    if (!npc.active || npc.friendly || npc.dontTakeDamage || npc.lifeMax <= 5)
                        continue;
                        
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetNPC = npc;
                    }
                }
            }
        }

        private void Attack()
        {
            if (targetNPC == null || !targetNPC.active || Main.myPlayer != Projectile.owner)
                return;
                
            Vector2 direction = targetNPC.Center - Projectile.Center;
            direction.Normalize();
            direction *= 8f; 
            int projectileID = Projectile.NewProjectile(
                Projectile.GetSource_FromAI(),
                Projectile.Center,
                direction,
                ModContent.ProjectileType<CrimsonDart>(), 
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner,
                ai0: targetNPC.whoAmI, 
                ai1: HOMING_STRENGTH 
            );
                
            if (Main.projectile.IndexInRange(projectileID))
            {
                Main.projectile[projectileID].friendly = true;
                Main.projectile[projectileID].hostile = false;
                Main.projectile[projectileID].tileCollide = true;
                Main.projectile[projectileID].timeLeft = 180; 
                Main.projectile[projectileID].netUpdate = true;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
        }

        private void UpdatePosition(Player owner)
        {
            orbitAngle += IDLE_ROTATION_SPEED;
            if (orbitAngle > MathHelper.TwoPi)
                orbitAngle -= MathHelper.TwoPi;

            Vector2 orbitPosition = owner.Center + new Vector2(
                (float)Math.Cos(orbitAngle) * ORBIT_DISTANCE,
                (float)Math.Sin(orbitAngle) * ORBIT_DISTANCE
            );
            
            Vector2 direction = orbitPosition - Projectile.Center;
            float speed = 8f; 
            
            if (direction.Length() > speed)
            {
                direction.Normalize();
                Projectile.velocity = direction * speed;
            }
            else
            {
                Projectile.velocity = direction; 
            }
            
            if (Main.rand.NextBool(20))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0, 0, 0, default, 0.8f);
            }
        }
    }
}


