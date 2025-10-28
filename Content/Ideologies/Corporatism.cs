using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Corporatism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 8;
            player.maxMinions -= 1; 
            player.lifeRegen -= 1; 
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.WarriorEmblem);
            recipe1.AddIngredient(ItemID.SoulofFright, 10);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.RangerEmblem);
            recipe2.AddIngredient(ItemID.SoulofFright, 10);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.Register();
            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.SorcererEmblem);
            recipe3.AddIngredient(ItemID.SoulofFright, 10);
            recipe3.AddTile(TileID.MythrilAnvil);
            recipe3.Register();
            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ItemID.SummonerEmblem);
            recipe4.AddIngredient(ItemID.SoulofFright, 10);
            recipe4.AddTile(TileID.MythrilAnvil);
            recipe4.Register();
        }
    }
}


