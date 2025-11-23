using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpiritrumReborn.Players
{
    public class IdeologySlotPlayer : ModPlayer
    {
        private readonly HashSet<int> tempEquippedIdeologies = new HashSet<int>();

        public Item ideologySlotItem = new Item();
        public bool ideologyUnlocked = false;
        public bool extraMoneyDrop;
        public bool corpoBuff;
        public bool teamBuff;
        public bool natureRegen;
        public bool shareBuff;
        public bool inflictMidas;
        public float rangedVelocity;
        public int futurismOverheatTimer;
        public int futurismOverheatCooldown;
        public bool futurismOverheated;
        public const int FuturismOverheatThreshold = 300;
        public const int FuturismOverheatCooldownDuration = 300;

        public override void Initialize()
        {
            ideologySlotItem = new Item();
            ideologySlotItem.TurnToAir();
            futurismOverheatTimer = 0;
            futurismOverheatCooldown = 0;
            futurismOverheated = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["ideologySlotItem"] = ideologySlotItem.Clone();
            tag["ideologyUnlocked"] = ideologyUnlocked;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("ideologySlotItem"))
                ideologySlotItem = tag.Get<Item>("ideologySlotItem");
            ideologyUnlocked = tag.ContainsKey("ideologyUnlocked") && tag.GetBool("ideologyUnlocked");
        }

        public override void UpdateEquips()
        {
            if (!ideologySlotItem.IsAir && ideologySlotItem.accessory && IsIdeologyItem(ideologySlotItem) && ideologyUnlocked)
            {
                if (ideologySlotItem.ModItem != null)
                {
                    ideologySlotItem.ModItem.UpdateAccessory(Player, false);
                }
            }
        }

        public override void ResetEffects()
        {
            extraMoneyDrop = false;
            corpoBuff = false;
            teamBuff = false;
            natureRegen = false;
            shareBuff = false;
            inflictMidas = false;
            rangedVelocity = 0f;
            futurismOverheated = futurismOverheated && futurismOverheatCooldown > 0;
        }

        private void CheckForDuplicateIdeologyAndApplyConfusion()
        {
            if (!ideologyUnlocked) return;

            tempEquippedIdeologies.Clear();

            if (ideologySlotItem != null && !ideologySlotItem.IsAir && IsIdeologyItem(ideologySlotItem))
            {
                tempEquippedIdeologies.Add(ideologySlotItem.type);
            }

            for (int i = 3; i < Player.armor.Length; i++)
            {
                Item invItem = Player.armor[i];
                if (invItem != null && !invItem.IsAir && invItem.accessory && IsIdeologyItem(invItem))
                {
                    tempEquippedIdeologies.Add(invItem.type);
                }
            }

            if (tempEquippedIdeologies.Count >= 2)
            {
                Player.AddBuff(Terraria.ID.BuffID.Confused, 2, true);
            }
        }

        public bool CanEquipIdeologyAccessory(Item item, Player player)
        {
            if (!ideologyUnlocked) return false;

            for (int i = 3; i < player.armor.Length; i++)
            {
                Item invItem = player.armor[i];
                if (!invItem.IsAir && invItem.accessory && IsIdeologyItem(invItem) && invItem.type == item.type) return false;
            }

            if (ideologySlotItem != null && !ideologySlotItem.IsAir && ideologySlotItem.type == item.type) return false;

            return true;
        }

        public override void PostUpdate()
        {
            if (futurismOverheated)
            {
                futurismOverheatCooldown--;
                if (futurismOverheatCooldown <= 0)
                {
                    futurismOverheated = false;
                    futurismOverheatCooldown = 0;
                    futurismOverheatTimer = 0;
                }
            }
            else
            {
                if (futurismOverheatTimer > 0) futurismOverheatTimer = Math.Max(0, futurismOverheatTimer - 1);
            }

            CheckForDuplicateIdeologyAndApplyConfusion();
        }

        public void IncrementFuturismOverheat(int amount = 1)
        {
            if (futurismOverheated) return;
            futurismOverheatTimer += amount;
            if (futurismOverheatTimer >= FuturismOverheatThreshold)
            {
                futurismOverheated = true;
                futurismOverheatCooldown = FuturismOverheatCooldownDuration;
                futurismOverheatTimer = 0;
            }
        }

        public void InflictMidasOnHit(NPC target)
        {
            if (inflictMidas) target.AddBuff(Terraria.ID.BuffID.Midas, 180);
        }

        private bool IsIdeologyItem(Item item)
        {
            if (item.ModItem != null)
            {
                string modItemFullName = item.ModItem.GetType().FullName;
                return modItemFullName != null && modItemFullName.Contains(".Items.Ideology.");
            }
            return false;
        }
    }
}
