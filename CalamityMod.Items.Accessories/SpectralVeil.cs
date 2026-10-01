using System.Collections.Generic;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SpectralVeil : ModItem, ILocalizedModType, IModType
{
	public const float TeleportRange = 845f;

	internal static readonly int VeilIFrames = 80;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.SpectralVeilHotKey);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().spectralVeil = true;
		player.Calamity().stealthGenMoving += 0.15f;
		player.Calamity().stealthGenStandstill += 0.15f;
	}
}
