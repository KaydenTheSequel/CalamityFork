using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.TitanHeart;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class TitanHeartBoots : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.07f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 10;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(14).AddIngredient<global::CalamityMod.Items.Materials.TitanHeart>().AddTile(16)
			.Register();
	}
}
