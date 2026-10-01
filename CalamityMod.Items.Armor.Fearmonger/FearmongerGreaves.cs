using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Fearmonger;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class FearmongerGreaves : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.1f;

	public static float MoveSpeedBoost = 0.2f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 42;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1834).AddIngredient<CosmiliteBar>(10).AddIngredient<AscendantSpiritEssence>(2)
			.AddIngredient(547, 10)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
