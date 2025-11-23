using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class ElementalSerum : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 12);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.GetModPlayer<global::SpiritrumReborn.Players.ElementalSerumPlayer>().elementalSerumEquipped = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ElementalSerumEffect", "Spawns Toxic Clouds periodically"));
            tooltips.Add(new TooltipLine(Mod, "ElementalSerumEffect2", "Spawns Fiery Sparks when hit"));
            tooltips.Add(new TooltipLine(Mod, "ElementalSerumEffect3", "Grants multiple immunities"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<global::SpiritrumReborn.Content.Items.Accessories.VolcanicSyringe>())
                .AddIngredient(ModContent.ItemType<global::SpiritrumReborn.Content.Items.Accessories.SpiderSyringe>())
                .AddIngredient(ItemID.FrostCore, 2)
                .AddIngredient(ItemID.ChlorophyteBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
