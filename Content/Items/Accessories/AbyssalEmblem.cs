using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class AbyssalEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true; 		
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) 
        {
            player.GetDamage(DamageClass.Generic) += 0.1f; 
            player.GetCritChance(DamageClass.Generic) += 10;  
            player.maxMinions += 2; 			
            player.maxTurrets += 2; 			
            player.statLifeMax2 += 20; 			
            player.lifeRegen += 2; 			
            player.statManaMax2 += 40; 			
            player.manaCost -= 0.1f; 		
        }
    }
}



