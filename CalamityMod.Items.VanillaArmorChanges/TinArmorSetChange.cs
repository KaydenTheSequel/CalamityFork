using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class TinArmorSetChange : VanillaArmorChange
{
	public const float SetBonusCrit = 10f;

	public override int? HeadPieceID => 687;

	public override int? BodyPieceID => 688;

	public override int? LegPieceID => 689;

	public override string ArmorSetName => "Tin";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(10f);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.GetCritChance<GenericDamageClass>() += 10f;
	}
}
