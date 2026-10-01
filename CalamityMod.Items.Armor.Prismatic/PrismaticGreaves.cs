using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Prismatic;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class PrismaticGreaves : ModItem, ILocalizedModType, IModType
{
	public static float MagicDamageBoost = 0.1f;

	public static int MagicCritBoost = 12;

	public static float NonMagicDamageDecrease = 0.2f;

	public static float FlightTimeBoost = 0.1f;

	public static float JumpSpeedBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBoost.ToPercent(), MagicCritBoost, NonMagicDamageDecrease.ToPercent(), FlightTimeBoost.ToPercent(), JumpSpeedBoost.ToJumpSpeedPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 21;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().prismaticGreaves = true;
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
		player.jumpSpeedBoost += JumpSpeedBoost;
		player.GetDamage<GenericDamageClass>() -= NonMagicDamageDecrease;
		player.GetDamage<MagicDamageClass>() += NonMagicDamageDecrease;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArmoredShell>(3).AddIngredient<ExodiumCluster>(5).AddIngredient<DivineGeode>(6)
			.AddIngredient(1346, 300)
			.AddTile(134)
			.Register();
	}
}
