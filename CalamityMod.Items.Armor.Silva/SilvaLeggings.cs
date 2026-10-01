using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Silva;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class SilvaLeggings : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.11f;

	public static int CritBoost = 11;

	public static float MoveSpeedBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 28;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlantyMush>(9).AddIngredient<EffulgentFeather>(7).AddIngredient<AscendantSpiritEssence>(2)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
