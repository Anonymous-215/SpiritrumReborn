using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SpiritrumReborn.Content.Items.Armor.Goog;

namespace SpiritrumReborn.Content.Items.Armor.GalacticGoog
{
    [AutoloadEquip(EquipType.Head)]
    public class GalacticGoogMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Cyan; 
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 8f; 
            player.statManaMax2 += 20; 
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GalacticGoogBody>() && legs.type == ModContent.ItemType<GalacticGoogPaws>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+3 summon slots, +2 sentry slots";
            player.maxMinions += 3;
            player.maxTurrets += 2;
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GoogMask>());
            recipe.AddIngredient(ItemID.LunarBar, 10); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}


