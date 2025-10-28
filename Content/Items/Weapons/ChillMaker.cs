using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class ChillMaker : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 93;
            Item.crit = 26;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 18;
			Item.height = 54;
			Item.useTime = 10;
			Item.useAnimation = 11;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4;
            Item.scale = 1f;
			Item.value = Item.buyPrice(gold: 22);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet; 
            Item.shoot = ProjectileID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 20f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 2); 
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 300);
        }
    }
}


