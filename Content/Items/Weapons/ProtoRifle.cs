using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ProtoRifle : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.scale = 0.9f;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item33;
            Item.autoReuse = true;
            Item.mana = 5;
            Item.shoot = ProjectileID.GreenLaser;
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 2); 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LaserRifle, 1); 
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ModContent.ItemType<ProtoPistol>()); 
            recipe.AddTile(TileID.MythrilAnvil); 
            recipe.Register();
        }
    }
}


