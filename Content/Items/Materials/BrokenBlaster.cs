using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic; 
using Terraria.Localization; 

namespace SpiritrumReborn.Content.Items.Materials
{
	public class BrokenBlaster : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999; 
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Yellow;
		}

	}
}

