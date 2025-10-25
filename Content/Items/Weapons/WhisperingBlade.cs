using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic; 
using Terraria.Localization; 
using Terraria.DataStructures;

namespace SpiritrumReborn.Content.Items.Weapons 
{
    public class WhisperingBlade : ModItem
    {
        public override void SetDefaults()
        {
            // For now it doesn't have any projectile (the projectile also needs to be remade)
            // Resprite is planned
            Item.damage = 124;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
            Item.crit = 8;
			Item.scale = 1.6f;
			Item.value = Item.buyPrice(gold: 20);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
    }
}



