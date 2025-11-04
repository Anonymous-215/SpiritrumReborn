using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Obscurionite
{
	[AutoloadEquip(EquipType.Head)]
	public class ObscurioniteHelmet : ModItem
	{
		public const int MeleeSpeedBonus = 15; 
		public const int MeleeDamageBonus = 10;  

		public const int SetBonusCritChance = 15; 

		public static LocalizedText SetBonusText { get; private set; }

		public override void SetStaticDefaults() {

			SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(SetBonusCritChance);
			ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
		}

		public override void SetDefaults() {
			Item.width = 18; 
			Item.height = 18; 
			Item.value = Item.sellPrice(gold: 1); 
			Item.rare = ItemRarityID.Green; 
			Item.defense = 9; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
			player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<ObscurioniteBreastplate>() && legs.type == ModContent.ItemType<ObscurioniteLeggings>();
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = SetBonusText.Value;
			player.GetCritChance(DamageClass.Generic) += SetBonusCritChance;
		}

		public override void AddRecipes() {
				Recipe recipe =CreateRecipe();
				recipe.AddIngredient<ObscurioniteAlloy>(15);
				recipe.AddIngredient(ItemID.BeeHeadgear, 1);
            	recipe.AddIngredient(ItemID.NecroHelmet, 1);
            	recipe.AddIngredient(ItemID.MoltenHelmet, 1);
            	recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddTile(TileID.Anvils);
				recipe.Register();
		}
	}
}

