using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.VoidHunter
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidHunterLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 14);
            Item.rare = ItemRarityID.Red;
            Item.defense = 22;
        }
        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterLeggingsInfo", "14% increased movement speed and 5% increased damage"));
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.14f;
            player.GetDamage(DamageClass.Generic) += 0.05f;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<VoidEssence>(), 22).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
