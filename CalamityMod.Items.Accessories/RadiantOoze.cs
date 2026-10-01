using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RadiantOoze : ModItem, ILocalizedModType, IModType
{
	public static int MinRegenBoost = 4;

	public static int MaxRegenBoost = 10;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinRegenBoost.ToRegenPerSecond(), MaxRegenBoost.ToRegenPerSecond());

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.rOoze = true;
		if (!modPlayer.aAmpoule && !modPlayer.purity && !hideVisual)
		{
			Lighting.AddLight(player.Center, new Vector3(1f, 1f, 0.6f));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		if (player != null)
		{
			list.FindAndReplace("[REGEN]", player.Calamity().radiantOozeRegen.ToString("0.##"));
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlightedGel>(45).AddIngredient<PurifiedGel>(15).AddTile(16)
			.Register();
	}
}
