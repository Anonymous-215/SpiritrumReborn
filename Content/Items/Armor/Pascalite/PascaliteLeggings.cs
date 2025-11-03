using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Pascalite
{
    [AutoloadEquip(EquipType.Legs)]
    public class PascaliteLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 25);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 13;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.PascaliteBar>(), 12);
            recipe.AddIngredient(ItemID.FlowerBoots, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
