using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures; 

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class Transhumanism : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 20);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 30; 
            player.statManaMax2 += 30; 
            player.GetDamage(DamageClass.Generic) += 0.15f; 
            player.GetCritChance(DamageClass.Generic) += 8; 

            player.GetAttackSpeed(DamageClass.Melee) += 0.10f; 
            player.moveSpeed += 0.10f; 

            player.nightVision = true;
            player.accMerman = true; 
            player.accDivingHelm = true;
            player.lavaImmune = true;
            player.fireWalk = true;

            player.endurance -= 0.12f; 
            player.statDefense -= 10; 
            player.lifeRegen -= 6; 

            player.pickSpeed += 0.10f; 

            if (!hideVisual && Main.rand.NextBool(25))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.GemSapphire);
                d.noGravity = true;
                d.scale = 1.0f;
                d.velocity *= 0.2f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(ItemID.Nanites, 25);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}


