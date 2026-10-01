using System.Collections.Generic;
using CalamityMod.Balancing;
using CalamityMod.CalPlayer;
using CalamityMod.Rarities;
using CalamityMod.World;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DraedonsHeart : ModItem, ILocalizedModType, IModType
{
	private const double ContactDamageReduction = 0.15;

	internal static readonly int NanomachinesDuration = 120;

	internal static readonly int NanomachinesHealPerFrame = 3;

	internal static readonly int NanomachinePauseAfterDamage = 60;

	internal static readonly int NanomachinePauseAfterShieldDamage = 30;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(15.0.ToString("N0"), NanomachinesHealPerFrame * (NanomachinesDuration / 2), NanomachinesDuration / 60);

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 11));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 68;
		base.Item.accessory = true;
		base.Item.defense = 20;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.hadNanomachinesLastFrame)
		{
			modPlayer.adrenaline = 0f;
		}
		modPlayer.draedonsHeart = true;
		player.noKnockback = true;
		modPlayer.hadNanomachinesLastFrame = true;
		modPlayer.AdrenalineDuration = NanomachinesDuration;
		modPlayer.contactDamageReduction += 0.15;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.AdrenalineHotKey);
		string desc = this.GetLocalization(CalamityWorld.revenge ? "NanomachinesReplace" : "NanomachinesAdd").Format(NanomachinePauseAfterDamage / 60);
		list.FindAndReplace("[NANODESC]", desc);
		string fullAdrenDRString = (100f * BalancingConstants.FullAdrenalineDR).ToString("N0");
		list.FindAndReplace("[DR]", fullAdrenDRString);
	}
}
