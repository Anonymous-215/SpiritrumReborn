using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
// IMPORTANT: This is a testing weapon
namespace SpiritrumReborn.Content.Items.Weapons
{ 
	public class AdminBlade : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 20000;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.scale = 6f;
			Item.value = Item.buyPrice(copper: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	}
}


