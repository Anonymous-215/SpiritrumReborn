using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Players;

namespace SpiritrumReborn.Content.Items.Armor.Penumbral
{
    [AutoloadEquip(EquipType.Head)]
    public class PenumbralHelm : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 8f;
            player.GetCritChance(DamageClass.Melee) += 8f;
            player.manaCost -= 0.15f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PenumbralChestplate>() && legs.type == ModContent.ItemType<PenumbralLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% chance on melee hit to restore 6 mana\nand grant Penumbral Surge (+12% Magic Damage for 5s).";
            player.GetModPlayer<PenumbralPlayer>().penumbralSet = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Content.Items.Materials.Penumbrium>(), 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}


