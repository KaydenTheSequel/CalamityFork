using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class PlatinumArmorSetChange : VanillaArmorChange
{
	public const float SetBonusDR = 0.1f;

	public override int? HeadPieceID => 696;

	public override int? BodyPieceID => 697;

	public override int? LegPieceID => 698;

	public override string ArmorSetName => "Platinum";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(0.1f.ToPercent());
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.endurance += 0.1f;
	}
}
