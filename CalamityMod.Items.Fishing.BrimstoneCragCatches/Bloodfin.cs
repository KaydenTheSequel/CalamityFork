using CalamityMod.Buffs.Potions;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.BrimstoneCragCatches;

public class Bloodfin : ModItem, ILocalizedModType, IModType
{
	public static int RegenBoost = 4;

	public static int RegenTimeBoost = 4;

	public static int DebuffedRegenBoost = 10;

	public static int DebuffedRegenTimeFloor = 900;

	public static double ExtraRegenHealthThreshold = 0.75;

	public static int FramesForExtraRegen = 30;

	public static int BuffType = ModContent.BuffType<BloodfinBoost>();

	public static int BuffDuration = 10;

	public new string LocalizationCategory => "Items.Fishing";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RegenBoost.ToRegenPerSecond(), DebuffedRegenBoost.ToRegenPerSecond(), FramesForExtraRegen.FramesToSeconds(), ExtraRegenHealthThreshold.ToPercent(), BuffDuration);

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 30;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToHealingPotion(38, 36, 240);
		base.Item.useStyle = 2;
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void OnConsumeItem(Player player)
	{
		player.AddBuff(BuffType, CalamityUtils.SecondsToFrames(BuffDuration));
	}
}
