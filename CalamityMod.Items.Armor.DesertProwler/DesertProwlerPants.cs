using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.DesertProwler;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class DesertProwlerPants : ModItem, ILocalizedModType, IModType
{
	public static int RogueCritBoost = 4;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 3;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.buffImmune[194] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormlionMandible>().AddIngredient(225, 5).AddTile(86)
			.Register();
	}
}
