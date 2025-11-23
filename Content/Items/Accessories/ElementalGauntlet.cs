using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class ElementalGauntlet : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ElementalGauntletTooltip", "Grants minor melee bonuses and makes attacks inflict chilled, shadowflame and venom."));
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            player.GetModPlayer<ElementalGauntletPlayer>().elementalGauntletEquipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
             .AddIngredient(ItemID.FireGauntlet)
             .AddIngredient(ItemID.FrostCore, 2)
             .AddIngredient(ItemID.JungleSpores, 8)
             .AddIngredient(ItemID.AncientBattleArmorMaterial, 2)
             .AddTile(TileID.MythrilAnvil)
             .Register();
        }
        
}
}
