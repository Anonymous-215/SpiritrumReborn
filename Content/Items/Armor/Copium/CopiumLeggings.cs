using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Copium
{
    [AutoloadEquip(EquipType.Legs)]
    public class CopiumLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 25);
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.CopiumBar>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
