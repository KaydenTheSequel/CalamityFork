using System.Collections.Generic;
using CalamityMod.Items.BaseItems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "SirensHeart" })]
public class AquaticHeart : TransformationAccessory, ILocalizedModType, IModType
{
	public static float WaterSpeedBoost = 0.15f;

	public static float IceShieldDamageReductionBoost = 0.2f;

	public static int IceShieldCooldown = CalamityUtils.SecondsToFrames(30);

	public new string LocalizationCategory => "Items.Accessories";

	public static LocalizedText FullTooltip => CalamityUtils.GetText("Items.Accessories.AquaticHeart.FullTooltip").WithFormatArgs(WaterSpeedBoost.ToPercent(), IceShieldDamageReductionBoost.ToPercent(), IceShieldCooldown.FramesToSeconds());

	public override string AssetPath => "CalamityMod/Items/Accessories/";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "AquaticTrans", null),
		(EquipType.Body, "AquaticTrans", null),
		(EquipType.Legs, "AquaticTrans", null),
		(EquipType.Face, null, null)
	};

	public override (SoundStyle sound, int delay)? HurtSound(Player p)
	{
		return (SoundID.FemaleHit, 10);
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().aquaticHeart = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string statusTooltip = (NPC.downedBoss3 ? FullTooltip.ToString() : this.GetLocalizedValue("LockedTooltip"));
		tooltips.FindAndReplace("[STATUS]", statusTooltip);
	}
}
