using System.Collections.Generic;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BlunderBooster : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.accessory = true;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		return !player.Calamity().hasJetpack;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().hasJetpack = true;
		player.GetDamage<ThrowingDamageClass>() += 0.12f;
		player.Calamity().rogueVelocity += 0.15f;
		player.Calamity().blunderBooster = true;
		player.Calamity().blunderBoosterVisibility = !hideVisual;
		player.Calamity().stealthGenStandstill += 0.1f;
		player.Calamity().stealthGenMoving += 0.1f;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.BoosterDashHotKey);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlaguedFuelPack>().AddIngredient<EffulgentFeather>(8).AddTile(134)
			.Register();
	}
}
