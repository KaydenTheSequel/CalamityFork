using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "Regenator" })]
public class Regenerator : ModItem, ILocalizedModType, IModType
{
	public static float HealthRatioCap = 0.5f;

	public static int FramesPerHeal = 8;

	public static int RegenTimeBoost = 4;

	public static float RegenToDamageRatio = 0.015f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(HealthRatioCap.ToPercent(), (60f / (float)FramesPerHeal).Round());

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 56;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().regenerator = true;
		player.longInvince = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (Main.LocalPlayer != null)
		{
			list.FindAndReplace("[DAMAGE]", Main.LocalPlayer.Calamity().regeneratorDamage.ToPercent());
		}
	}
}
