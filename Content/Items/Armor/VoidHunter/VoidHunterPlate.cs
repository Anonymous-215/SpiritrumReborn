using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.VoidHunter
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidHunterPlate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 16);
            Item.rare = ItemRarityID.Red;
            Item.defense = 34;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.08f;
            player.statLifeMax2 += 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<VoidEssence>(), 28).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
