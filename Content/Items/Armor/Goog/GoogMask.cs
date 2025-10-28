using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Goog
{
    [AutoloadEquip(EquipType.Head)]
    public class GoogMask : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.04f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GoogBody>() && legs.type == ModContent.ItemType<GoogPaws>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+1 summon and sentry slot";
            player.maxMinions += 1;
            player.maxTurrets += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Googling>(), 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
