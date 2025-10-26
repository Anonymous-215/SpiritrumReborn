using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{ 
	public class PainterPride : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 22; 
			Item.DamageType = DamageClass.Melee;
			Item.width = 20; 
			Item.height = 20; 
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3; 
			Item.value = Item.buyPrice(gold: 4); 
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Paintbrush, 1);
            recipe.AddIngredient(ItemID.RedPaint, 5);
            recipe.AddIngredient(ItemID.OrangePaint, 5);
            recipe.AddIngredient(ItemID.YellowPaint, 5);
            recipe.AddIngredient(ItemID.LimePaint, 5);
            recipe.AddIngredient(ItemID.GreenPaint, 5);
            recipe.AddIngredient(ItemID.TealPaint, 5);
            recipe.AddIngredient(ItemID.CyanPaint, 5);
            recipe.AddIngredient(ItemID.SkyBluePaint, 5);
            recipe.AddIngredient(ItemID.BluePaint, 5);
            recipe.AddIngredient(ItemID.PurplePaint, 5);
            recipe.AddIngredient(ItemID.VioletPaint, 5);
            recipe.AddIngredient(ItemID.PinkPaint, 5);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.Register();
		}
	}
}


