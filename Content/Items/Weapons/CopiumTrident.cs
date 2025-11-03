using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader.IO;
using System.IO;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class CopiumTrident : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Item.type] = true; 
        }

        public override void SetDefaults()
        {
            Item.damage = 12; 
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 30; 
            Item.useAnimation = 30; 
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.knockBack = 5f; 
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.White; 
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true; 
            Item.noUseGraphic = true; 
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.CopiumTrident>();
            Item.shootSpeed = 5f; 
            Item.channel = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.CopiumBar>(), 9)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}


