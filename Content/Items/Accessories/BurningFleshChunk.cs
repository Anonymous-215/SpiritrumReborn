

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class BurningFleshChunk : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 3; 
            player.manaRegen += 2; 
            player.GetDamage(DamageClass.Generic) += 0.08f;
        }
    }
}








