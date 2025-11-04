using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using SpiritrumReborn.Players;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Global
{
    public class GlobalArmor : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool isMeleeHit = (hit.DamageType == DamageClass.Melee) || (item.DamageType == DamageClass.Melee);
            if (isMeleeHit && player.GetModPlayer<Players.PenumbralPlayer>().penumbralSet)
            {
                if (Main.rand.NextFloat() < 0.20f)
                {
                    int manaRestore = 6;
                    player.statMana += manaRestore;
                    player.ManaEffect(manaRestore);
                    player.AddBuff(ModContent.BuffType<Content.Buffs.PenumbralSurge>(), 500);
                }
        }
    }
}
}
