using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Pascalite
{
    [AutoloadEquip(EquipType.Body)]
    public class PascaliteChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 40);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.12f;
            player.endurance += 0.08f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.PascaliteBar>(), 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
