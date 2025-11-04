using SpiritrumReborn.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Armor.Obscurionite
{
	[AutoloadEquip(EquipType.Legs)]
	public class ObscurioniteLeggings : ModItem
	{
		public static readonly float MoveSpeedBonus = 0.20f; 
		public static readonly float RangedDamageBonus = 0.15f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(MoveSpeedBonus * 100));

		public override void SetDefaults() {
			Item.width = 18; 
			Item.height = 18; 
			Item.value = Item.sellPrice(gold: 1); 
			Item.rare = ItemRarityID.Green; 
			Item.defense = 9; 
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += MoveSpeedBonus; 
			player.GetDamage(DamageClass.Ranged) += RangedDamageBonus;
		}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ObscurioniteAlloy>(20);
            recipe.AddIngredient(ItemID.BeeGreaves, 1);
            recipe.AddIngredient(ItemID.NecroGreaves, 1);
            recipe.AddIngredient(ItemID.MoltenGreaves, 1);
            recipe.AddIngredient(ItemID.JunglePants, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}

