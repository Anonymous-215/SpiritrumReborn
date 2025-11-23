using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class MechanicalSoulVessel : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(gold: 15);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "MechanicalSoulVesselTooltip", "Melee hits and your projectiles spread Ichor, Cursed Flames and Electrified."));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MechanicalSoulVesselPlayer>().MechanicalSoulVesselEquipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public class MechanicalSoulVesselPlayer : ModPlayer
    {
        public bool MechanicalSoulVesselEquipped;

        public override void ResetEffects()
        {
            MechanicalSoulVesselEquipped = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (MechanicalSoulVesselEquipped && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120); 
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (MechanicalSoulVesselEquipped && proj.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Electrified, 120); 
            }
        }
    }
}
}