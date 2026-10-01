using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MonkT3ArmorSetChange : VanillaArmorChange
{
	public static float SetBonusRogueStealth = 1.1f;

	public override int? HeadPieceID => 3880;

	public override int? BodyPieceID => 3881;

	public override int? LegPieceID => 3882;

	public override string ArmorSetName => "MonkTier3";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(SetBonusRogueStealth.ToStealth()) + "\n" + setBonusText;
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		player.GetDamage<MeleeDamageClass>() -= 0.2f;
		player.GetDamage<RogueDamageClass>() += 0.2f;
	}

	public override void ApplyBodyPieceEffect(Player player)
	{
		player.GetAttackSpeed<MeleeDamageClass>() -= 0.2f;
		player.Calamity().rogueVelocity += 0.2f;
		player.GetCritChance<MeleeDamageClass>() -= 5f;
		player.GetCritChance<RogueDamageClass>() += 5f;
	}

	public override void ApplyLegPieceEffect(Player player)
	{
		player.GetCritChance<MeleeDamageClass>() -= 20f;
		player.GetCritChance<RogueDamageClass>() += 20f;
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().rogueStealthMax += SetBonusRogueStealth;
		player.Calamity().wearingRogueArmor = true;
	}
}
