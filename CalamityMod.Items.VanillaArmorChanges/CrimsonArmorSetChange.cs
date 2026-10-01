using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class CrimsonArmorSetChange : VanillaArmorChange
{
	public const float ArmorPieceDamage = 0.06f;

	public const int ArmorPieceLifeRegen = 1;

	public override int? HeadPieceID => 792;

	public override int? BodyPieceID => 793;

	public override int? LegPieceID => 794;

	public override string ArmorSetName => "Crimson";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	private static void ApplyAnyPieceEffect(Player player)
	{
		player.GetDamage<GenericDamageClass>() += 0.03f;
		player.lifeRegen++;
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
