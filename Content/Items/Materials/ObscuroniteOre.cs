using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content;

namespace SpiritrumReborn.Content.Items.Materials
{
	public class ObscuroniteOre : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
			Item.rare = ItemRarityID.Green;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Content.Tiles.ObscuroniteOre>());
			Item.width = 12;
			Item.height = 12;
			Item.value = 3000;
		}
	}
}