using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class PlaguedFuelPack : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		return !player.Calamity().hasJetpack;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().hasJetpack = true;
		player.GetDamage<ThrowingDamageClass>() += 0.08f;
		player.Calamity().rogueVelocity += 0.15f;
		player.Calamity().plaguedFuelPack = true;
		player.Calamity().stealthGenStandstill += 0.1f;
		player.Calamity().stealthGenMoving += 0.1f;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.BoosterDashHotKey);
	}
}
