using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class LeadArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 690;

	public override int? BodyPieceID => 691;

	public override int? LegPieceID => 692;

	public override string ArmorSetName => "Lead";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.noKnockback = true;
	}
}
