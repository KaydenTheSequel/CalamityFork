using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class MomentumCapacitor : ModItem, ILocalizedModType, IModType
{
	internal const float MomentumChargePerFrame = 0.02f;

	internal const float MaxMomentumCharge = 5.8f;

	internal const int TotalFadeTime = 16;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = Item.buyPrice(0, 50);
		base.Item.rare = 5;
		base.Item.useStyle = 4;
		base.Item.useAnimation = (base.Item.useTime = 2);
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override bool? UseItem(Player player)
	{
		if (!CalamityPlayer.areThereAnyDamnBosses)
		{
			CalamityPlayer modPlayer = player.Calamity();
			modPlayer.momentumCapacitorTime = 16;
			modPlayer.momentumCapacitorBoost += Main.rand.NextFloat(0.5f, 3.5f) * 0.02f;
			if (modPlayer.momentumCapacitorBoost >= 5.8f && !Main.zenithWorld)
			{
				modPlayer.momentumCapacitorBoost = 5.8f;
			}
		}
		return null;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}
}
