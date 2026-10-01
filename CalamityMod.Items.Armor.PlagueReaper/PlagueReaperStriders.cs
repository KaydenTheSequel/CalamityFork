using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.PlagueReaper;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class PlagueReaperStriders : ModItem, ILocalizedModType, IModType
{
	public static int RangedCritBoost = 8;

	public static float MoveSpeedBoost = 0.15f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 14;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(153).AddIngredient<PlagueCellCanister>(21).AddIngredient(1346, 17)
			.AddTile(134)
			.Register();
	}
}
