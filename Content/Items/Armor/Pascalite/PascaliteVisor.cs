using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Materials;

namespace SpiritrumReborn.Content.Items.Armor.Pascalite
{
    [AutoloadEquip(EquipType.Head)]
    public class PascaliteVisor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.12f;
            player.GetCritChance(DamageClass.Ranged) += 10f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PascaliteChestplate>() && legs.type == ModContent.ItemType<PascaliteLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Immunity to Poisoned, Acid Venom and Feral Bite\n+2 Summon slots and +1 Sentry slot";
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.maxMinions += 2;
            player.maxTurrets += 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.PascaliteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
