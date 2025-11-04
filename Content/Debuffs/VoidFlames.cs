using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpiritrumReborn.Content.Debuffs
{
    public class VoidFlames : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<VoidFlamesGlobalNPC>().voidFlames = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<VoidFlamesPlayer>().voidFlames = true;
        }
    }

    public class VoidFlamesGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool voidFlames;

        public override void ResetEffects(NPC npc)
        {
            voidFlames = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (voidFlames)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                
                npc.lifeRegen -= 100; 
                
                if (damage < 50)
                    damage = 50;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (voidFlames)
            {
                modifiers.Defense -= 5;
            }
        }
    }

    public class VoidFlamesPlayer : ModPlayer
    {
        public bool voidFlames;

        public override void ResetEffects()
        {
            voidFlames = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (voidFlames)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                
                Player.lifeRegen -= 100; 
                
                if (Player.lifeRegenTime > 0)
                    Player.lifeRegenTime = 0;
            }
        }



        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (voidFlames)
            {
                modifiers.FinalDamage *= 1.1f;
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (voidFlames)
            {
                modifiers.FinalDamage *= 1.1f;
            }
        }
    }
}




