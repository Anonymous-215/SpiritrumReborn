using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class VoidPendant : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 24);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            {
                player.maxMinions += 3;
                player.maxTurrets += 3;
                player.GetDamage(DamageClass.Summon) += 0.20f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WitchLocket>())
                .AddIngredient(ModContent.ItemType<StrategyWiki>())
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
