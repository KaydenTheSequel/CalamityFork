using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Brimflame;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class BrimflameBoots : ModItem, ILocalizedModType, IModType
{
	public static float MagicDamageBoost = 0.05f;

	public static float MoveSpeedBoost = 0.05f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

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
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.moveSpeed += MoveSpeedBoost;
		player.fireWalk = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AshesofCalamity>(5).AddIngredient<UnholyCore>(3).AddTile(134)
			.Register();
	}
}
