using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Ammo
{
    public class AnthraxGasGrenade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.crit = 15;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AnthraxProjectile>();
            Item.shootSpeed = 15f;
            Item.ammo = Item.type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20); 
            recipe.AddIngredient(ItemID.Grenade, 4);
            recipe.AddIngredient(ItemID.VialofVenom, 1);
            recipe.AddIngredient(ItemID.Ichor, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}


