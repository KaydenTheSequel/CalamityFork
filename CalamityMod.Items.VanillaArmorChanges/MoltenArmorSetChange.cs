using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MoltenArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 231;

	public override int? BodyPieceID => 232;

	public override int? LegPieceID => 233;

	public override string ArmorSetName => "Molten";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.GetDamage<MeleeDamageClass>() -= 0.03f;
		player.fireWalk = true;
		player.lavaMax += 300;
		player.GetDamage<TrueMeleeDamageClass>() += 0.1f;
	}
}
