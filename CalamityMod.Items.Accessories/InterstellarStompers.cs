using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "GravistarSabaton" })]
public class InterstellarStompers : ModItem, ILocalizedModType, IModType
{
	public static readonly int PassthroughDamage = 150;

	public static readonly int SlamDamage = 300;

	public static readonly int PassthroughIFrames = 5;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 7;
		base.Item.expert = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.GravistarSabatonHotkey);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.noFallDmg = true;
		player.moveSpeed += 0.06f;
		player.jumpSpeedBoost++;
		player.Calamity().gSabaton = true;
	}
}
