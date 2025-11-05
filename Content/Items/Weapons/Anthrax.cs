using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SpiritrumReborn.Content.Items.Ammo;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class Anthrax : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 18;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item61;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AnthraxProjectile>();
            Item.shootSpeed = 12f;
            Item.useAmmo = ModContent.ItemType<Ammo.AnthraxGasGrenade>();
            Item.scale = 0.8f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-12, 0);
    }
}


