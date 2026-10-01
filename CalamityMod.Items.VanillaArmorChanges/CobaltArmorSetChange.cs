using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class CobaltArmorSetChange : VanillaArmorChange
{
	public const int SpeedBoostSetBonusPercentage = 10;

	public const int MaxManaBoost = 20;

	public const int MovementSpeedBoostPercentageMax = 10;

	public const int MovementSpeedBoostMphThreshold = 80;

	public override int? HeadPieceID => 372;

	public override int? BodyPieceID => 374;

	public override int? LegPieceID => 375;

	public override int[] AlternativeHeadPieceIDs => new int[2] { 371, 373 };

	public override string ArmorSetName => "Cobalt";

	public override void ApplyHeadPieceEffect(Player player)
	{
		if (player.armor[0].type == 371)
		{
			player.statManaMax2 += 20;
		}
	}

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		if (Main.LocalPlayer.armor[0].type == 372)
		{
			setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName + ".Melee");
		}
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(10);
	}

	public static float CalculateMovementSpeedInterpolant(Player player)
	{
		float milesPerHour = ((Vector2)(ref player.velocity)).Length() * 225f / 44f;
		return (float)Math.Pow(Utils.GetLerpValue(0f, 80f, milesPerHour, clamped: true), 1.6666666666666667);
	}

	public static void ApplyMovementSpeedBonuses(Player player)
	{
		float movementSpeedInterpolant = CalculateMovementSpeedInterpolant(player);
		player.GetDamage<GenericDamageClass>() += 10f * movementSpeedInterpolant * 0.01f;
		float critBonus = 10f * movementSpeedInterpolant;
		player.GetCritChance<GenericDamageClass>() += critBonus;
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.GetAttackSpeed<MeleeDamageClass>() -= 0.05f;
		player.Calamity().CobaltSet = true;
	}
}
