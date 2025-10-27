using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Ammo
{
    public class Egg : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = Item.sellPrice(copper: 5);
            Item.rare = ItemRarityID.White;
            Item.ammo = ModContent.ItemType<Egg>();
            Item.shoot = ModContent.ProjectileType<SpiritrumReborn.Content.Projectiles.Egged>();
            Item.shootSpeed = 10f;
            Item.damage = 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddIngredient(ItemID.Hay, 1);
                .Register();
        }
    }
}


