using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class WitheredBloom : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 24);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.10f;
            player.manaFlower = true;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.starCloakItem = Item; 
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MagnetFlower)
                .AddIngredient(ItemID.AvengerEmblem)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.ManaFlower)
                .AddIngredient(ItemID.CelestialEmblem)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}


