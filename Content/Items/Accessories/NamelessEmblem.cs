using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class NamelessEmblem : ModItem
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
            player.GetDamage(DamageClass.Generic) += 0.05f; 
            player.GetCritChance(DamageClass.Generic) += 5;  
            player.maxMinions += 1; 			
            player.maxTurrets += 1; 			
            player.statLifeMax2 += 10; 			
            player.lifeRegen += 1; 			
            player.statManaMax2 += 20; 			
            player.manaCost -= 0.05f; 		
        }
    }
}



