using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MonkT2ArmorSetChange : VanillaArmorChange
{
	public static float SetBonusRogueStealth = 0.9f;

	public override int? HeadPieceID => 3806;

	public override int? BodyPieceID => 3807;

	public override int? LegPieceID => 3808;

	public override string ArmorSetName => "MonkTier2";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(SetBonusRogueStealth.ToStealth()) + "\n" + setBonusText;
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		player.GetAttackSpeed<MeleeDamageClass>() -= 0.2f;
		player.Calamity().rogueVelocity += 0.2f;
	}

	public override void ApplyBodyPieceEffect(Player player)
	{
		player.GetDamage<MeleeDamageClass>() -= 0.2f;
		player.GetDamage<RogueDamageClass>() += 0.2f;
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		player.GetCritChance<MeleeDamageClass>() -= 15f;
		player.GetCritChance<RogueDamageClass>() += 15f;
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().rogueStealthMax += SetBonusRogueStealth;
		player.Calamity().wearingRogueArmor = true;
	}
}
