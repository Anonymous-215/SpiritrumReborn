using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Weapons
{
	public class LMG : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 65; 
			Item.crit = 5;
			Item.DamageType = DamageClass.Ranged; 
			Item.width = 30; 
			Item.height = 15; 
			Item.useTime = 5; 
			Item.useAnimation = 5; 
			Item.useStyle = ItemUseStyleID.Shoot; 
			Item.noMelee = true; 
			Item.knockBack = 2; 
			Item.value = Item.buyPrice(gold: 20); 
			Item.rare = ItemRarityID.Cyan; 
			Item.UseSound = SoundID.Item11; 
			Item.autoReuse = true; 
			Item.shoot = ProjectileID.Bullet; 
			Item.shootSpeed = 15f; 
			Item.useAmmo = AmmoID.Bullet; 
			Item.scale = 1f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, 3); 
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "LMGTipAmmo", "75% chance to not consume ammo"));
			tooltips.Add(new TooltipLine(Mod, "LMGTipCost", "It costs 210 copper coins to fire this weapon for 12 seconds")); 

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Megashark, 1); 
			recipe.AddIngredient(ItemID.FragmentVortex, 10); 
			recipe.AddIngredient(ModContent.ItemType<BrokenBlaster>(), 1);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.Register();
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= 0.75f; 
		}
	}
}

