using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Copium
{
    [AutoloadEquip(EquipType.Head)]
    public class CopiumMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CopiumChestplate>() && legs.type == ModContent.ItemType<CopiumLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Immunity to Poisoned and Feral Bite.";
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Rabies] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.CopiumBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
