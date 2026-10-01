using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class FrostArmorSetChange : VanillaArmorChange
{
	public const float ProximityBoost = 0.2f;

	public const float MinDistance = 160f;

	public const float MaxDistance = 800f;

	public override int? HeadPieceID => 684;

	public override int? BodyPieceID => 685;

	public override int? LegPieceID => 686;

	public override string ArmorSetName => "Frost";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		int PercentBoost = (int)Math.Round(20.0);
		setBonusText = CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(PercentBoost) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().frostSet = true;
		player.GetDamage<MeleeDamageClass>() -= 0.1f;
		player.GetDamage<RangedDamageClass>() -= 0.1f;
	}
}
