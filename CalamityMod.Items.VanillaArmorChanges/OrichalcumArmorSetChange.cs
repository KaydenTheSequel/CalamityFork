using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class OrichalcumArmorSetChange : VanillaArmorChange
{
	public const int ChestplateCritChanceBoost = 4;

	public override int? HeadPieceID => 1211;

	public override int? BodyPieceID => 1213;

	public override int? LegPieceID => 1214;

	public override int[] AlternativeHeadPieceIDs => new int[2] { 1212, 1210 };

	public override string ArmorSetName => "Orichalcum";

	public override void ApplyBodyPieceEffect(Player player)
	{
		player.GetCritChance<GenericDamageClass>() += 4f;
	}
}
