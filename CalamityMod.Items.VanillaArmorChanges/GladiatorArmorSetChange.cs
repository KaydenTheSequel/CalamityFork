using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class GladiatorArmorSetChange : VanillaArmorChange
{
	public const int HelmetRogueDamageBoostPercent = 3;

	public const int ChestplateRogueCritBoostPercent = 3;

	public const int LeggingRogueVelocityBoostPercent = 3;

	public override int? HeadPieceID => 3187;

	public override int? BodyPieceID => 3188;

	public override int? LegPieceID => 3189;

	public override string ArmorSetName => "Gladiator";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName);
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += 0.03f;
	}

	public override void ApplyBodyPieceEffect(Player player)
	{
		player.GetCritChance<ThrowingDamageClass>() += 3f;
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		player.Calamity().rogueVelocity += 0.03f;
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().rogueStealthMax += 0.6f;
		player.Calamity().wearingRogueArmor = true;
		player.GetDamage<ThrowingDamageClass>() += 0.05f;
		player.Calamity().rogueVelocity += 0.1f;
	}
}
