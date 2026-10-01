using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AmbrosialAmpoule : ModItem, ILocalizedModType, IModType
{
	public static int MaxLifeBoost = 50;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxLifeBoost, RadiantOoze.MinRegenBoost.ToRegenPerSecond(), RadiantOoze.MaxRegenBoost.ToRegenPerSecond());

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		player.statLifeMax2 += MaxLifeBoost;
		if (!player.HasBuff(48))
		{
			player.AddBuff(48, 2);
		}
		modPlayer.aAmpoule = true;
		modPlayer.honeyDewHalveDebuffs = true;
		modPlayer.livingDewHalveDebuffs = true;
		if (!modPlayer.rOoze && !modPlayer.purity && !hideVisual)
		{
			Lighting.AddLight(player.Center, new Vector3(1.2f, 1.2f, 0.72f));
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
		CreateRecipe().AddIngredient<LivingDew>().AddIngredient<RadiantOoze>().AddIngredient<LifeAlloy>(3)
			.AddIngredient(3458, 6)
			.AddTile(412)
			.Register();
	}
}
