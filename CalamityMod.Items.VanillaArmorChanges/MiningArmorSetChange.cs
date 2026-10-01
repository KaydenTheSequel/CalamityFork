using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MiningArmorSetChange : VanillaArmorChange
{
	public const int BonusOreChance = 4;

	public const int CooldownMin = 180;

	public const int CooldownMax = 360;

	public override int? HeadPieceID => 88;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 4008 };

	public override int? BodyPieceID => 410;

	public override int? LegPieceID => 411;

	public override string ArmorSetName => "Mining";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().miningSet = true;
	}
}
