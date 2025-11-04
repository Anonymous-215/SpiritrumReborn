using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace SpiritrumReborn.Players
{
    public class MythrilSetPlayer : ModPlayer
    {
        public bool mythrilSetEquipped = false;

        private float drTotal = 0f;
        private float nextIncrement = 0.15f;
        private int drTimer = 0;

        public override void ResetEffects()
        {
            mythrilSetEquipped = false;
        }

        public override void UpdateEquips()
        {
            bool hasSet = true;
            for (int i = 0; i < 3; i++)
            {
                var item = Player.armor[i];
                if (item == null || item.IsAir)
                {
                    hasSet = false;
                    break;
                }
                string name = Lang.GetItemNameValue(item.type);
                if (!name.Contains("Mythril"))
                {
                    hasSet = false;
                    break;
                }
            }

            mythrilSetEquipped = hasSet;

            if (!mythrilSetEquipped)
            {
                drTotal = 0f;
                nextIncrement = 0.15f;
                drTimer = 0;
                return;
            }

            int debuffCount = 0;
            for (int i = 0; i < Player.buffType.Length; i++)
            {
                int b = Player.buffType[i];
                if (b > 0 && Main.debuff[b])
                    debuffCount++;
            }

            if (debuffCount > 0)
            {
                Player.GetDamage(DamageClass.Generic) += 0.10f * debuffCount;
            }

            if (drTimer > 0 && drTotal > 0f)
            {
                Player.endurance += drTotal;
            }
        }

        public override void PreUpdate()
        {
            if (drTimer > 0)
            {
                drTimer--;
                if (drTimer <= 0)
                {
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        Player.immune = true;
                        Player.immuneTime = Math.Max(Player.immuneTime, 3 * 60);
                    }

                    drTotal = 0f;
                    nextIncrement = 0.15f;
                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!mythrilSetEquipped) return;

            drTotal += nextIncrement;
            nextIncrement *= 0.5f;
            drTimer = 5 * 60; 
        }

    }
}


