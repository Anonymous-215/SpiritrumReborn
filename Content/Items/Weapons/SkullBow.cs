using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class SkullBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 16;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.BoneGloveProj;
                Item.shootSpeed = 11f; 
                Item.useAmmo = AmmoID.Arrow; 
            Item.knockBack = 5f; 
            Item.value = Item.sellPrice(gold: 2); 
            Item.rare = ItemRarityID.Orange; 
            Item.UseSound = SoundID.Item5;             
            Item.autoReuse = true;
            Item.noMelee = true; 
            Item.consumable = false; 
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.BoneGloveProj, damage, knockback, player.whoAmI);
            
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 25); 
            recipe.AddTile(TileID.Anvils); 
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}




