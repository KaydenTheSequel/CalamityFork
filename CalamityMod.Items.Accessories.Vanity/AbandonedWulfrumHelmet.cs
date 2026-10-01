using CalamityMod.Items.BaseItems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "WulfrumMask" })]
[LegacyName(new string[] { "WulfrumHeadRogue" })]
[LegacyName(new string[] { "WulfrumHeadgear" })]
[LegacyName(new string[] { "WulfrumHeadRanged" })]
[LegacyName(new string[] { "WulfrumHelm" })]
[LegacyName(new string[] { "WulfrumHeadMelee" })]
[LegacyName(new string[] { "WulfrumHood" })]
[LegacyName(new string[] { "WulfrumHeadMagic" })]
public class AbandonedWulfrumHelmet : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "AbandonedWulfrumHelmetTrans", "WulfrumOldSetHead"),
		(EquipType.Body, "AbandonedWulfrumHelmet", null),
		(EquipType.Legs, "AbandonedWulfrumHelmet", null),
		(EquipType.Face, null, null)
	};

	public override bool ShouldHideAccessories => true;

	public override (SoundStyle sound, int delay)? HurtSound(Player p)
	{
		return (SoundID.NPCHit4, 10);
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 30;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 2;
		base.Item.vanity = true;
	}

	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<WulfrumTransformationPlayer>().vanityEquipped = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			player.GetModPlayer<WulfrumTransformationPlayer>().vanityEquipped = true;
		}
	}
}
