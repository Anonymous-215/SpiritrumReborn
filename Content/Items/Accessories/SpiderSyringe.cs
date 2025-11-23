using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SpiritrumReborn.Content.Items.Accessories
{
    public class SpiderSyringe : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(gold: 8);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.GetModPlayer<global::SpiritrumReborn.Content.Items.Accessories.SpiderSyringe.SpiderSyringePlayer>().spiderSyringe = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SpiderSyringeEffect1", "Grants immunity to poison and venom"));
            tooltips.Add(new TooltipLine(Mod, "SpiderSyringeEffect2", "Spreads toxic clouds that damage enemies"));
        }

        public class SpiderSyringePlayer : ModPlayer
        {
            public bool spiderSyringe;
            private int toxicCloudTimer;

            public override void ResetEffects()
            {
                spiderSyringe = false;
            }

            public override void PostUpdate()
            {
                if (spiderSyringe)
                {
                    toxicCloudTimer++;
                    if (toxicCloudTimer >= 120)
                    {
                        toxicCloudTimer = 0;
                        bool enemiesNearby = false;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && !npc.friendly && npc.Distance(Player.Center) < 400f)
                            {
                                enemiesNearby = true;
                                break;
                            }
                        }
                        if (enemiesNearby && Main.myPlayer == Player.whoAmI)
                        {
                            int cloudCount = Main.rand.Next(1, 4);
                            for (int i = 0; i < cloudCount; i++)
                            {
                                Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-150, 151), Main.rand.Next(-100, 101));
                                int cloudType = Main.rand.Next(3) switch
                                {
                                    0 => ProjectileID.ToxicCloud,
                                    1 => ProjectileID.ToxicCloud2,
                                    _ => ProjectileID.ToxicCloud3
                                };
                                Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), spawnPos, Vector2.Zero, cloudType, 25, 0f, Player.whoAmI);
                            }
                        }
                    }
                }
            }

            public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
            {
                if ((proj.type == ProjectileID.ToxicCloud || proj.type == ProjectileID.ToxicCloud2 || proj.type == ProjectileID.ToxicCloud3) && spiderSyringe)
                {
                    target.AddBuff(BuffID.Venom, 60);
                }
            }
        }
    }
}
