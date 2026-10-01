using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class BloodflareCuisses : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.1f;

	public static int CritBoost = 7;

	public static float MoveSpeedBoost = 0.17f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 28;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(13).AddIngredient<RuinousSoul>(3).AddTile(134)
			.Register();
	}
}
