using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Sulphurous;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
[LegacyName(new string[] { "SulfurBreastplate" })]
public class SulphurousBreastplate : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.06f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.defense = 6;
		base.Item.rare = 2;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(20).AddIngredient<SulphuricScale>(20).AddTile(16)
			.Register();
	}
}
