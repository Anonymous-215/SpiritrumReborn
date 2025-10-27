//IMPORANT: Resprite needed

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class UniversalShell : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) //Basically gives the full effects of nighttime celestial shell and with extra bonuses
        {

            player.statLifeMax2 += 60; 
            player.statManaMax2 += 80; 
            player.GetDamage(DamageClass.Generic) += 0.16f; 
            player.GetAttackSpeed(DamageClass.Melee) += 0.18f; 
            player.GetCritChance(DamageClass.Generic) += 6; 
            player.lifeRegen += 2; 
            player.statDefense += 8; 
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CelestialShell)
                .AddIngredient(ItemID.FragmentNebula, 8)
                .AddIngredient(ItemID.FragmentSolar, 8)
                .AddIngredient(ItemID.FragmentVortex, 8)
                .AddIngredient(ItemID.FragmentStardust, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


