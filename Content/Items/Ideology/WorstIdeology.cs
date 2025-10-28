using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SpiritrumReborn.Content.Items.Ideology
{
    public class WorstIdeology : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red; 
            Item.value = Item.buyPrice(platinum: 1); 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            player.statDefense -= 5; 
            player.endurance -= 0.05f; 
            player.GetDamage(DamageClass.Generic) -= 0.2f; 
            player.moveSpeed -= 0.1f; 
            player.GetCritChance(DamageClass.Generic) -= 15; 
            player.GetAttackSpeed(DamageClass.Generic) -= 0.3f; 
            
            
            player.GetDamage(DamageClass.Generic) -= 0.10f; 
            player.moveSpeed -= 0.10f;
            
            player.coinLuck -= 1.0f; 
            
            player.GetAttackSpeed(DamageClass.Generic) -= 0.15f;
            player.GetKnockback(DamageClass.Generic) -= 0.2f; 
            
            player.manaCost += 0.15f; 
            player.statManaMax2 -= 20; 
            
            player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
            player.moveSpeed -= 0.05f;
            
            player.GetKnockback(DamageClass.Generic) -= 0.25f;
            player.pickSpeed += 0.2f; 
            
            player.GetDamage(DamageClass.Melee) -= 0.1f; 
            player.statDefense -= 5; 
            
            player.statLifeMax2 -= 20; 
            player.lifeRegen -= 1; 
            
            player.moveSpeed -= 0.05f; 
            player.GetDamage(DamageClass.Generic) -= 0.05f;
            
            player.GetDamage(DamageClass.Generic) -= 0.05f; 
            
            player.statDefense -= 8; 
            player.lifeRegen -= 2; 
            
            player.statDefense -= 10; 
            player.manaCost += 0.25f; 
            
            player.moveSpeed -= 0.05f; 
            player.GetAttackSpeed(DamageClass.Generic) -= 0.05f;
            
            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.statDefense -= 3; 
                    ally.GetDamage(DamageClass.Generic) -= 0.05f; 
                    ally.moveSpeed -= 0.05f; 
                }
            }
            
            if (Main.rand.NextBool(600)) 
            {
                player.AddBuff(BuffID.Confused, 300); 
            }
            
            player.buffImmune[BuffID.Poisoned] = false;
            player.buffImmune[BuffID.OnFire] = false;
            player.buffImmune[BuffID.Confused] = false;
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
            recipe.AddIngredient(ItemType<Communism>(), 1);
            recipe.AddIngredient(ItemType<Conservatism>(), 1);
            recipe.AddIngredient(ItemType<Corporatism>(), 1);
            recipe.AddIngredient(ItemType<Democracy>(), 1);
            recipe.AddIngredient(ItemType<Environmentalism>(), 1);
            recipe.AddIngredient(ItemType<Fascism>(), 1);
            recipe.AddIngredient(ItemType<Liberalism>(), 1);
            recipe.AddIngredient(ItemType<Libertarianism>(), 1);
            recipe.AddIngredient(ItemType<SocialDemocracy>(), 1);
            recipe.AddIngredient(ItemType<Socialism>(), 1);
            recipe.AddIngredient(ItemType<Technocracy>(), 1);
            recipe.AddIngredient(ItemType<Transhumanism>(), 1);
            
            recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 25);
            
            recipe.AddTile(TileID.DemonAltar); 
            
            recipe.Register();
        }
    }
}


