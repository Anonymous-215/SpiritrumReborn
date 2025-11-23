using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class CryoglyphAmulet : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "CryoglyphAmulet1", "+8 armor penetration"));
            tooltips.Add(new TooltipLine(Mod, "CryoglyphAmulet2", "Your attacks inflict Frostburn for 2 seconds"));
            tooltips.Add(new TooltipLine(Mod, "CryoglyphAmulet3", "+10% damage against chilled, frozen, or frostbitten enemies"));
            tooltips.Add(new TooltipLine(Mod, "CryoglyphAmulet4", "Standing still grants a brief ice barrier (10s cooldown)"));
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 8;
            player.GetModPlayer<global::SpiritrumReborn.Players.CryoglyphAmuletPlayer>().cryoglyphAmuletEquipped = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkToothNecklace);
            recipe.AddIngredient(ItemID.FrostCore, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        
    }
}
