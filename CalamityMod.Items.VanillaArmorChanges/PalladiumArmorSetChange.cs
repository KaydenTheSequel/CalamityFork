using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class PalladiumArmorSetChange : VanillaArmorChange
{
	public const int ChestplateDamagePercentageBoost = 2;

	public const int LeggingsDamagePercentageBoost = 3;

	public override int? HeadPieceID => 1206;

	public override int? BodyPieceID => 1208;

	public override int? LegPieceID => 1209;

	public override int[] AlternativeHeadPieceIDs => new int[2] { 1207, 1205 };

	public override string ArmorSetName => "Palladium";

	public override void ApplyBodyPieceEffect(Player player)
	{
		player.GetDamage<GenericDamageClass>() += 0.02f;
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		player.GetDamage<GenericDamageClass>() += 0.03f;
	}

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}
}
