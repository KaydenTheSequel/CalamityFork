using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Empyrean;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "XerocCuisses" })]
public class EmpyreanCuisses : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.08f;

	public static int RogueCritBoost = 8;

	public static float MoveSpeedBoost = 0.2f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.defense = 20;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(15).AddIngredient(3467, 12).AddTile(412)
			.Register();
	}
}
