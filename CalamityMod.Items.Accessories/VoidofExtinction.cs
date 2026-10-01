using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class VoidofExtinction : ModItem, ILocalizedModType, IModType
{
	public static int CritBoost = 13;

	public static int VoidExploDamage = 40;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBoost, Abaddon.BrimstoneFlamesReduction.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Abaddon>().AddIngredient<ScoriaBar>(3).AddIngredient<CoreofCalamity>()
			.AddTile(134)
			.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.voidOfExtinction = true;
		calamityPlayer.abaddon = true;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}
}
