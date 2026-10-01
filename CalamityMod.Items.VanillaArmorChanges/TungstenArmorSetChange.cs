using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class TungstenArmorSetChange : VanillaArmorChange
{
	public const float HookBoost = 0.5f;

	public override int? HeadPieceID => 693;

	public override int? BodyPieceID => 694;

	public override int? LegPieceID => 695;

	public override string ArmorSetName => "Tungsten";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(0.5f.ToPercent());
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().tungstenArmorHookBoost = true;
	}
}
