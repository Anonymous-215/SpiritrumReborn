using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Armor.Penumbral
{
    [AutoloadEquip(EquipType.Legs)]
    public class PenumbralLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 70);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f;
            player.jumpSpeedBoost += 0.08f;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
