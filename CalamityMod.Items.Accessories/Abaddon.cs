using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Face })]
public class Abaddon : ModItem, ILocalizedModType, IModType
{
	public static int CritBoost = 8;

	public static float BrimstoneFlamesReduction = 0.5f;

	public static int AbaddonExploDamage = 25;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBoost, BrimstoneFlamesReduction.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().abaddon = true;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}
}
