using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class AdamantiteArmorSetChange : VanillaArmorChange
{
	public const int MaxManaBoost = 20;

	public const int DefenseBoostMax = 10;

	public const int TimeUntilDecayBeginsAfterAttacking = 60;

	public const int TimeUntilBoostCompletelyDecays = 210;

	public override int? HeadPieceID => 401;

	public override int? BodyPieceID => 403;

	public override int? LegPieceID => 404;

	public override int[] AlternativeHeadPieceIDs => new int[2] { 400, 402 };

	public override string ArmorSetName => "Adamantite";

	public override void ApplyHeadPieceEffect(Player player)
	{
		if (player.armor[0].type == 400)
		{
			player.statManaMax2 += 20;
		}
	}

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		if (Main.LocalPlayer.armor[0].type == 401)
		{
			setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName + ".Melee");
		}
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(10);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		int critBoost = (int)(MathHelper.Clamp(player.endurance, 0f, 1f) * 50f);
		switch (player.armor[0].type)
		{
		case 400:
			player.GetCritChance<MagicDamageClass>() += critBoost;
			break;
		case 401:
			player.GetCritChance<MeleeDamageClass>() += critBoost;
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.05f;
			break;
		case 402:
			player.GetCritChance<RangedDamageClass>() += critBoost;
			break;
		}
		player.Calamity().AdamantiteSet = true;
	}
}
