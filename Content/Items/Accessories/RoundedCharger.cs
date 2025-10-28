using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Accessories
{
        public class RoundedCharger : ModItem
        {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 20);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Item heldItem = player.HeldItem; //detects the held weapon's damage type
            if (heldItem != null && heldItem.damage > 0 && heldItem.DamageType == DamageClass.Ranged)
            { //if true, it should than contine to detect the ammo the weapon uses
                int ammoType = heldItem.useAmmo;
                if (ammoType == AmmoID.Bullet)
                {
                    player.GetAttackSpeed(DamageClass.Ranged) += 0.08f; //+8% ranged attack speed if the condition is true
                    player.ammoBox = true; //+20% chance to not consume ammo (basically infinite ammo box)
                }
            }
        }
    }
}
