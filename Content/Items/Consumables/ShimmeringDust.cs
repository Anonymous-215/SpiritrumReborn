using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Buffs;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Consumables
{
    public class ShimmeringDust : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true; 
            Item.useStyle = ItemUseStyleID.DrinkLiquid; 
            Item.useAnimation = 17; 
            Item.useTime = 17; 
            Item.maxStack = 9999; 
            Item.width = 16; 
            Item.height = 16; 
            Item.rare = ItemRarityID.LightRed; 
            Item.value = Item.sellPrice(silver: 20); 
            Item.UseSound = SoundID.Item3; 

            Item.buffType = ModContent.BuffType<ShimmeredDust>(); 
            Item.buffTime = 60 * 60; 
        }

        public override bool? UseItem(Player player)
        {

            return true;
        }

    }
}

