using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class ShadowArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 102;

	public override int? BodyPieceID => 101;

	public override int? LegPieceID => 100;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 956 };

	public override int[] AlternativeBodyPieceIDs => new int[1] { 957 };

	public override int[] AlternativeLegPieceIDs => new int[1] { 958 };

	public override string ArmorSetName => "Shadow";

	private static void ApplyAnyPieceEffect(Player player)
	{
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		ApplyAnyPieceEffect(player);
	}

	public override void ApplyBodyPieceEffect(Player player)
	{
		ApplyAnyPieceEffect(player);
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		ApplyAnyPieceEffect(player);
	}
}
