using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class TimCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.discountAvailable = true;
            player.GetDamage(DamageClass.Magic) += 0.08f;
            player.manaCost -= 0.05f;
            player.GetModPlayer<global::SpiritrumReborn.Players.TimCharmPlayer>().timCharm = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Charm1", "-5% mana cost"));
            tooltips.Add(new TooltipLine(Mod, "Charm2", "+8% magic damage"));
            tooltips.Add(new TooltipLine(Mod, "Charm3", "Attacks have a chance to fire a water bolt"));
            tooltips.Add(new TooltipLine(Mod, "Charm4", "Water bolts can also fire more water bolts"));
        }
    }
}
