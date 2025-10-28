using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Materials
{
    public class VoidEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.ItemIconPulse[Item.type] = true; 
            ItemID.Sets.ItemNoGravity[Item.type] = true; 
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 125); 
            Item.rare = ItemRarityID.Purple;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, 0.3f, 0.1f, 0.4f); 
            
            if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustDirect(
                    Item.position,
                    Item.width,
                    Item.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
        }
    }
}


