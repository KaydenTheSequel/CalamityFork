using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Crags;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "Gehenna" })]
public class SlagsplitterPauldron : ModItem, ILocalizedModType, IModType
{
	public static int PauldronSlamDamage = 330;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 56;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.sPauldron = true;
		calamityPlayer.sPauldronVisual = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScorchedBone>(12).AddIngredient<AncientBoneDust>(4).AddIngredient<EssenceofHavoc>(8)
			.AddTile(16)
			.Register();
	}
}
