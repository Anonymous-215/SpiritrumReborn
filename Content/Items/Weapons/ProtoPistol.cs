using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; 
using System.Collections.Generic; 
using static Terraria.ModLoader.ModContent;
using System; 

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ProtoPistol : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Magic;
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.scale = 1f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item33;
            Item.autoReuse = true;
            Item.mana = 6;
            Item.shoot = ProjectileID.GreenLaser;
            Item.noMelee = true;
            Item.shootSpeed = 8f;

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0); 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(ItemID.GoldBar, 6);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(ItemID.SilverBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


