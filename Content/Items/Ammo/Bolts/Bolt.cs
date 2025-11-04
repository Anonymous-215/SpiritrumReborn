using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace SpiritrumReborn.Content.Items.Ammo.Bolts
{
    public class Bolt : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.consumable = true; 
            Item.value = Item.buyPrice(copper: 10);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ProjectileID.ExplosiveBullet; 
            Item.shootSpeed = 5f; 
            Item.ammo = ModContent.ItemType<Bolt>(); 
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 25; 
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25); 
            recipe.AddIngredient(ItemID.EmptyBullet, 25);
            recipe.AddIngredient(ItemID.IronBar, 1); 
            recipe.AddIngredient(ItemID.ExplosivePowder, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe(25);
            recipe2.AddIngredient(ItemID.EmptyBullet, 25);
            recipe2.AddIngredient(ItemID.LeadBar, 1); 
            recipe2.AddIngredient(ItemID.ExplosivePowder, 1); 
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}


