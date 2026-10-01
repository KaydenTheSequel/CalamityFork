using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Astral;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class AstralBreastplate : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 80;

	public static float AmmoReduction = 0.75f;

	public static float DamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, DamageBoost.ToPercent(), (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.defense = 25;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.statManaMax2 += MaxManaBoost;
		player.GetDamage<GenericDamageClass>() += DamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(12).AddIngredient(117, 9).AddTile(412)
			.Register();
	}
}
