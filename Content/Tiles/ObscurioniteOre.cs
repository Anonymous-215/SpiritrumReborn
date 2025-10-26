using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace SpiritrumReborn.Content.Tiles
{
	public class ObscurioniteOre : ModTile
	{
		public override void SetStaticDefaults() {
			TileID.Sets.Ore[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(45, 36, 127, 255), name);

			DustType = DustID.Platinum;
			VanillaFallbackOnModDeletion = TileID.Silver;
			HitSound = SoundID.Tink;
			MineResist = 3f;
			MinPick = 65;
		}

		
		public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor) {
			sightColor = Color.Blue;
			return true;
		}
	}
	public class ObscuroniteOreSystem : ModSystem
	{
		public static LocalizedText ObscuroniteOrePassMessage { get; private set; }
		public static LocalizedText BlessedWithObscuroniteOreMessage { get; private set; }

		public override void SetStaticDefaults() {
			ObscuroniteOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(ObscuroniteOrePassMessage)}");
			BlessedWithObscuroniteOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithObscuroniteOreMessage)}");
		}
		public void BlessWorldWithObscuroniteOre() {
			return;
		}
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (ShiniesIndex != -1) {
				tasks.Insert(ShiniesIndex + 1, new ObscuroniteOrePass("Obscuronite Mod Ores", 237.4298f));
			}
		}
	}

	public class ObscuroniteOrePass : GenPass
	{
		public ObscuroniteOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			progress.Message = ObscuroniteOreSystem.ObscuroniteOrePassMessage.Value;
			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.0006); k++) {
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 7), WorldGen.genRand.Next(4, 7), ModContent.TileType<ObscurioniteOre>());
			}
		}
	}
}