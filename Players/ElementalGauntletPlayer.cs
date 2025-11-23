using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Players
{
    public class ElementalGauntletPlayer : ModPlayer
    {
        public bool elementalGauntletEquipped;
        public bool eternalGauntletEquipped;
        public bool mechanicalSoulVesselEquipped;

        public override void ResetEffects()
        {
            elementalGauntletEquipped = false;
            eternalGauntletEquipped = false;
            mechanicalSoulVesselEquipped = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (elementalGauntletEquipped && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120);
            }

            if (eternalGauntletEquipped && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120);
                target.AddBuff(BuffID.Venom, 120);
                target.AddBuff(BuffID.Confused, 120);
                target.AddBuff(BuffID.Ichor, 120);
                target.AddBuff(BuffID.CursedInferno, 120);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (elementalGauntletEquipped && proj.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120);
            }

            if (eternalGauntletEquipped && proj.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120);
                target.AddBuff(BuffID.Venom, 120);
                target.AddBuff(BuffID.Confused, 120);
                target.AddBuff(BuffID.Ichor, 120);
                target.AddBuff(BuffID.CursedInferno, 120);
            }
        }
    }
}
