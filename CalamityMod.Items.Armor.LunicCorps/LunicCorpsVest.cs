using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.LunicCorps;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class LunicCorpsVest : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.15f;

	public static int RangedCritBoost = 15;

	public static float AmmoReduction = 0.75f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.defense = 24;
		base.Item.rare = 9;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RoverDrive>().AddIngredient<AstralBar>(11).AddIngredient(1006, 11)
			.AddTile(412)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<LunicCorpsBoots>())
			.Register();
	}
}
