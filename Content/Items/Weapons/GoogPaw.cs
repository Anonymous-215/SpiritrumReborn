using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class GoogPaw : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 14;
			Item.DamageType = DamageClass.Melee;
			Item.width = 5;
			Item.height = 5;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.scale = 0.55f;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
    }
}


