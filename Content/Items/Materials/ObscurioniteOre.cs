using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content;

namespace SpiritrumReborn.Content.Items.Materials
{
	public class ObscurioniteOre : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
			
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Content.Tiles.ObscurioniteOre>());
			Item.width = 12;
			Item.height = 12;
			Item.value = 3000;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Green;
		}
	}
}