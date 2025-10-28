using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Environmentalism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 6);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 2f;
            player.manaCost -= 0.08f; 

            player.GetCritChance(DamageClass.Generic) -= 10;

            int tileX = (int)(player.Center.X / 16f);
            int tileY = (int)((player.position.Y + player.height + 8f) / 16f);
            if (WorldGen.InWorld(tileX, tileY))
            {
                ushort tileType = Main.tile[tileX, tileY].TileType;
                if (tileType == TileID.Grass || tileType == TileID.JungleGrass || tileType == TileID.MushroomGrass)
                {
                    player.lifeRegen += 2;
                }
            }

            if (player.ZoneUnderworldHeight)
            {
                player.lifeRegen -= 3; 
                player.GetDamage(DamageClass.Generic) -= 0.20f; 
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DontHurtNatureBook);  
            recipe.AddIngredient(ItemID.NaturesGift);
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


