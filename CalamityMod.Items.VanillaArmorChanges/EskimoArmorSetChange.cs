using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class EskimoArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 803;

	public override int? BodyPieceID => 804;

	public override int? LegPieceID => 805;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 978 };

	public override int[] AlternativeBodyPieceIDs => new int[1] { 979 };

	public override int[] AlternativeLegPieceIDs => new int[1] { 980 };

	public override string ArmorSetName => "Eskimo";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().eskimoSet = true;
		player.Calamity().ColdDebuffMultiplier += 0.25f;
		player.buffImmune[44] = true;
	}
}
