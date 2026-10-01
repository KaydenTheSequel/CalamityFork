using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class CopperArmorSetChange : VanillaArmorChange
{
	public const float SetBonusFlatDamage = 2f;

	public override int? HeadPieceID => 89;

	public override int? BodyPieceID => 80;

	public override int? LegPieceID => 76;

	public override string ArmorSetName => "Copper";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(2f.ToString("N0"));
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.GetDamage<GenericDamageClass>().Flat += 2f;
	}
}
