using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class IronArmorSetChange : VanillaArmorChange
{
	public const float KnockbackMultiplier = 1.5f;

	public override int? HeadPieceID => 90;

	public override int? BodyPieceID => 81;

	public override int? LegPieceID => 77;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 954 };

	public override string ArmorSetName => "Iron";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(1.5f);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.GetKnockback<GenericDamageClass>() *= 1.5f;
	}
}
