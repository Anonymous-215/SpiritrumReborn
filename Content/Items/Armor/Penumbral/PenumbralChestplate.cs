using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Armor.Penumbral
{
    [AutoloadEquip(EquipType.Body)]
    public class PenumbralChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.06f;
            player.GetDamage(DamageClass.Magic) += 0.06f;
            player.statManaMax2 += 20;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 20)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}


