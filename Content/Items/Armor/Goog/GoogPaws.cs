using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpiritrumReborn.Content.Items.Armor.Goog
{
    [AutoloadEquip(EquipType.Legs)]
    public class GoogPaws : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.04f;
        }
    }
}
