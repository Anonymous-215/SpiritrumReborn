using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class EternalGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "EternalGauntletTooltip", "Grants powerful melee bonuses and makes attacks inflict venom, electrified, confused, ichor and cursed flames."));
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(gold: 20);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f; 
            player.GetDamage(DamageClass.Melee) += 0.15f; 
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            player.GetModPlayer<ElementalGauntletPlayer>().elementalGauntletEquipped = true;
            player.GetModPlayer<ElementalGauntletPlayer>().eternalGauntletEquipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ElementalGauntlet>())
                .AddIngredient(ItemID.SpiderFang, 5)
                .AddIngredient(ItemID.MartianConduitPlating, 12)
                .AddIngredient(ItemID.Nanites, 20)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        
}
}


