using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Fearmonger;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class FearmongerPlateMail : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.1f;

	public static int CritBoost = 5;

	public static int MaxLifeBoost = 100;

	public static float DamageReductionBoost = 0.08f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, MaxLifeBoost, DamageReductionBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 48;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.statLifeMax2 += MaxLifeBoost;
		player.endurance += DamageReductionBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1833).AddIngredient<CosmiliteBar>(12).AddIngredient<AscendantSpiritEssence>(3)
			.AddIngredient(547, 12)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<FearmongerGreaves>())
			.Register();
	}
}
