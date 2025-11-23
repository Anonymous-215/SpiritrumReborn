using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Weapons
{
    public class MarbleGrail : ModItem
    {
        private const int BoltCount = 3;
        private const float SpreadDegrees = 80f;
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 2;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0.2f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WaterBolt;
            Item.shootSpeed = 17f;
            Item.mana = 4;
            Item.scale = 0.75f;
            Item.reuseDelay = 30;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < BoltCount; i++)
            {
                Vector2 perturbed = velocity.RotatedByRandom(MathHelper.ToRadians(SpreadDegrees));
                Projectile.NewProjectile(source, position, perturbed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}


