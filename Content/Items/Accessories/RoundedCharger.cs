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
            Item heldItem = player.HeldItem; 
            if (heldItem != null && heldItem.damage > 0 && heldItem.DamageType == DamageClass.Ranged)
            { 
                int ammoType = heldItem.useAmmo;
                if (ammoType == AmmoID.Bullet)
                {
                    player.GetAttackSpeed(DamageClass.Ranged) += 0.08f; 
                    player.ammoBox = true; 
                }
            }
        }
    }
}


