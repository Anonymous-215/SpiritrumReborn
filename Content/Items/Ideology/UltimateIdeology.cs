using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Items.Ideology;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class UltimateIdeology : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple; 
            Item.value = Item.buyPrice(platinum: 1); 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 8;
            
            player.discountAvailable = true; 
            
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 2f;
            player.manaCost -= 0.08f;
            
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
            
            player.statLifeMax2 += 40;
            player.statManaMax2 += 40;
            player.GetDamage(DamageClass.Generic) += 0.10f; 
            player.GetCritChance(DamageClass.Generic) += 5; 
            player.statDefense += 4;
            player.endurance += 0.10f;
            player.lifeRegen += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            
            player.statDefense += 8; 
            player.endurance += 0.08f; 
            
            player.statLifeMax2 += 20; 
            player.lifeRegen += 1; 
            
            player.lifeRegen += 1;
            
            player.noKnockback = true; 
            
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.moveSpeed += 0.05f;
            player.statDefense += 5;
            
            player.GetDamage(DamageClass.Summon) += 0.15f; 
            player.maxTurrets += 1; 
            
            player.GetAttackSpeed(DamageClass.Generic) += 0.12f; 
            
            player.GetDamage(DamageClass.Generic) += 0.20f; 
            player.GetCritChance(DamageClass.Generic) += 5; 
            player.GetAttackSpeed(DamageClass.Generic) += 0.15f; 
            player.statDefense += 3;
            player.GetDamage(DamageClass.Generic) += 0.03f;
            
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Anarchy>(), 1);
            recipe.AddIngredient(ItemType<Authoritarianism>(), 1);
            recipe.AddIngredient(ItemType<BasicPolitics>(), 1);
            recipe.AddIngredient(ItemType<Capitalism>(), 1);
            recipe.AddIngredient(ItemType<Centralism>(), 1);
            recipe.AddIngredient(ItemType<Communitarianism>(), 1);
            recipe.AddIngredient(ItemType<Conservatism>(), 1);
            recipe.AddIngredient(ItemType<Corporatism>(), 1);
            recipe.AddIngredient(ItemType<Environmentalism>(), 1);
            recipe.AddIngredient(ItemType<SocialDemocracy>(), 1);
            recipe.AddIngredient(ItemType<Technocracy>(), 1);
            recipe.AddIngredient(ItemType<Transhumanism>(), 1);
            
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            
            recipe.AddTile(TileID.LunarCraftingStation);
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity != null)
            {
                if (calamity.TryFind("ShadowspecBar", out ModItem shadowBar) && shadowBar != null)
                {
                    recipe.AddIngredient(shadowBar.Type, 15);
                }
            }

            Mod fargo = null;
            if (ModLoader.TryGetMod("FargowiltasSouls", out fargo) || ModLoader.TryGetMod("Fargowiltas", out fargo))
            {
                if (fargo != null && fargo.TryFind("EternalEnergy", out ModItem eternal) && eternal != null)
                {
                    recipe.AddIngredient(eternal.Type, 15);
                }
            }

            recipe.Register();
        }
    }
}


