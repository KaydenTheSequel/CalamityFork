using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.FathomSwarmer;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class FathomSwarmerBoots : ModItem, ILocalizedModType, IModType
{
	public static float SummonDamageBoost = 0.08f;

	public static float SubmergedMoveSpeedBoost = 0.4f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBoost.ToPercent(), SubmergedMoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 13;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		if (player.Calamity().countsAsAnyWet)
		{
			player.moveSpeed += SubmergedMoveSpeedBoost;
		}
		player.ignoreWater = true;
		if (player.wingTime <= 0f)
		{
			player.accFlipper = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(9).AddIngredient<PlantyMush>(8).AddIngredient<DepthCells>(4)
			.AddTile(134)
			.Register();
	}
}
