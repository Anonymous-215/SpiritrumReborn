using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic; 

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Democracy : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.moveSpeed -= 0.05f;
            player.endurance += 0.05f;
            player.lifeRegen += 2;
            player.GetCritChance(DamageClass.Generic) -= 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

