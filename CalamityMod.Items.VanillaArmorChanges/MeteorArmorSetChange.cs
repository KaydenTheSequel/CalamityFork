using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MeteorArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 123;

	public override int? BodyPieceID => 124;

	public override int? LegPieceID => 125;

	public override string ArmorSetName => "Meteor";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().meteorSet = true;
	}
}
