using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class TarragonLeggings : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.08f;

	public static int CritBoost = 8;

	public static float MoveSpeedBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 24;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(11).AddIngredient<DivineGeode>(12).AddTile(134)
			.Register();
	}
}
