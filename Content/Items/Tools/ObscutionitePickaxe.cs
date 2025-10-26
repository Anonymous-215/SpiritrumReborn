using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; 
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Tools 
{
	public class ObscurionitePickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 10; 
            Item.width = 46; 
			Item.height = 44; 
			Item.rare = ItemRarityID.Orange; 
			Item.value = Item.sellPrice(gold: 1); 
			Item.useStyle = ItemUseStyleID.Swing; 
			Item.useTime = 14; 
			Item.useAnimation = 16; 
			Item.autoReuse = true; 
			Item.UseSound = SoundID.Item1; 
			Item.DamageType = DamageClass.Melee; 
			Item.knockBack = 3; 
			Item.pick = 105; 
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<ObscurioniteAlloy>(15); 
			recipe.AddIngredient(ItemID.MoltenPickaxe, 1);
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemID.BonePickaxe);
			recipe.AddTile(TileID.Anvils); 
			recipe.Register();
		}
	}
}


