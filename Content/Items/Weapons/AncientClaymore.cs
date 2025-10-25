using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class AncientClaymore : ModItem //Previously Ancient Katana
	{

		public override void SetDefaults()
		{
			// Has been reworked to be a very slow, but really hard hitting and big weapon
			Item.damage = 80;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 10;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.scale = 1.8f;
			Item.value = Item.buyPrice(gold: 10);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
		}
	}
}

// Ancient weapons are weapons that have upgrades through out the progression
// Currently only this one exists
// There will be, an ancient bow, an ancient tome and an ancient whip.