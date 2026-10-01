using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class JungleArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 228;

	public override int? BodyPieceID => 229;

	public override int? LegPieceID => 230;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 960 };

	public override int[] AlternativeBodyPieceIDs => new int[1] { 961 };

	public override int[] AlternativeLegPieceIDs => new int[1] { 962 };

	public override string ArmorSetName => "Jungle";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		player.statManaMax2 -= 20;
		player.GetCritChance<MagicDamageClass>() -= 3f;
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		player.GetCritChance<MagicDamageClass>() -= 3f;
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.manaCost += 0.06f;
	}
}
