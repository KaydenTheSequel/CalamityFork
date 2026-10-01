using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Sulphurous;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "SulfurLeggings" })]
public class SulphurousLeggings : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.04f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 16;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.defense = 5;
		base.Item.rare = 2;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient<SulphuricScale>(15).AddTile(16)
			.Register();
	}
}
