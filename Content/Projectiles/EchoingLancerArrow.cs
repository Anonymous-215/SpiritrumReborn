using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Projectiles
{
    public class EchoingLancerArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            var g = target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>();
            var hitTimes = g.GetOrCreate(owner.whoAmI);
            hitTimes.Add((int)Main.GameUpdateCount);
            hitTimes.RemoveAll(t => Main.GameUpdateCount - t > 30);
            if (hitTimes.Count == 3)
            {
                int echo = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * 0.1f, Projectile.type, (int)(Projectile.damage * 0.5f), Projectile.knockBack, owner.whoAmI);
                Main.projectile[echo].hostile = false;
                Main.projectile[echo].friendly = true;
                Main.projectile[echo].DamageType = DamageClass.Ranged;
                g.ClearPlayerList(owner.whoAmI);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player owner = Main.player[Projectile.owner];
            var g = target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>();
            if (g.TryGetList(owner.whoAmI, out var hitTimes) && hitTimes.Count >= 2 && Main.GameUpdateCount - hitTimes[hitTimes.Count - 2] <= 30)
            {
                modifiers.SourceDamage += 0.15f;
            }
        }
    }
    public class EchoingLancerArrowGlobalNPC : GlobalNPC
    {
        public Dictionary<int, List<int>> comboHits = new();
        private static readonly Stack<List<int>> listPool = new Stack<List<int>>();

        public override bool InstancePerEntity => true;

        private List<int> RentList()
        {
            if (listPool.Count > 0) return listPool.Pop();
            return new List<int>(4);
        }

        private void ReturnList(List<int> list)
        {
            list.Clear();
            listPool.Push(list);
        }

        public override void ResetEffects(NPC npc)
        {
            if (comboHits.Count > 0)
            {
                foreach (var kv in comboHits)
                {
                    ReturnList(kv.Value);
                }
                comboHits.Clear();
            }
        }

        public List<int> GetOrCreate(int playerId)
        {
            if (!comboHits.TryGetValue(playerId, out var list))
            {
                list = RentList();
                comboHits[playerId] = list;
            }
            return list;
        }

        public bool TryGetList(int playerId, out List<int> list)
        {
            return comboHits.TryGetValue(playerId, out list);
        }

        public void ClearPlayerList(int playerId)
        {
            if (comboHits.TryGetValue(playerId, out var list))
            {
                ReturnList(list);
                comboHits.Remove(playerId);
            }
        }
    }
}
