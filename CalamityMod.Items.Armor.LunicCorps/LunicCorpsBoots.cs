using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.LunicCorps;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class LunicCorpsBoots : ModItem, ILocalizedModType, IModType
{
	public static int RangedCritBoost = 7;

	public static float MoveSpeedAccelerationBoost = 0.15f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedCritBoost, MoveSpeedAccelerationBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.defense = 18;
		base.Item.rare = 9;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().lunicCorpsLegs = true;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
		player.moveSpeed += MoveSpeedAccelerationBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(8).AddIngredient(1006, 8).AddTile(412)
			.Register();
	}
}
