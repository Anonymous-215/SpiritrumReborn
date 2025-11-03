using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritrumReborn.Content.Projectiles;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class NutCracker : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 9f;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 1.8f; 
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) //On a true melee hit
        {
            int numShrapnel = Main.rand.Next(4, 7); //Shoots out 4 to 7 shrapnels
            for (int i = 0; i < numShrapnel; i++)
            {
                float speed = Main.rand.NextFloat(4f, 6f);
                float angle = MathHelper.ToRadians(Main.rand.Next(-15, 18)); //in a bit spread
                Vector2 velocity = Vector2.UnitX.RotatedBy(angle) * speed * player.direction;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, velocity, ModContent.ProjectileType<NutShrapnel>(), Item.damage / 2, 2f, player.whoAmI); //Deals 50% damage
            }
        }
    }
}


