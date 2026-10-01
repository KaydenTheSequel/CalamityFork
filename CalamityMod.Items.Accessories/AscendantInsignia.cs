using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "YharimsInsignia" })]
public class AscendantInsignia : ModItem, ILocalizedModType, IModType
{
	public static double FlightTimeBoost = 0.5;

	public static float MoveSpeedBoost = 0.15f;

	public static float JumpSpeedBoost = 0.75f;

	public static float AccelerationBoost = 0.5f;

	public static int AbilityDuration = CalamityUtils.SecondsToFrames(4);

	public static int AbilityCooldown = CalamityUtils.SecondsToFrames(40);

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlightTimeBoost.ToPercent(), MoveSpeedBoost.ToPercent(), (1f + AccelerationBoost).Round(), AbilityDuration.FramesToSeconds(), AbilityCooldown.FramesToSeconds());

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.AscendantInsigniaHotKey);
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().ascendantInsignia = true;
		player.empressBrooch = true;
		player.moveSpeed += MoveSpeedBoost;
		player.jumpSpeedBoost += JumpSpeedBoost - 0.5f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4989).AddIngredient<EffulgentFeather>(5).AddIngredient<DivineGeode>(5)
			.AddIngredient(575, 10)
			.AddTile(134)
			.Register();
	}
}
