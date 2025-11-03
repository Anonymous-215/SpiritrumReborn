using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Copium 
{
    [AutoloadEquip(EquipType.Body)]
    public class CopiumChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 40);
            Item.rare = ItemRarityID.White;
            Item.defense = 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.CopiumBar>(), 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


