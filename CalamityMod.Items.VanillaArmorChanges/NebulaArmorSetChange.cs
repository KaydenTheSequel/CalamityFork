namespace CalamityMod.Items.VanillaArmorChanges;

public class NebulaArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 2760;

	public override int? BodyPieceID => 2761;

	public override int? LegPieceID => 2762;

	public override string ArmorSetName => "Nebula";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName);
	}
}
