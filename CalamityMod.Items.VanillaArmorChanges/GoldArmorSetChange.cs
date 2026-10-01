using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class GoldArmorSetChange : VanillaArmorChange
{
	public const float GoldDropChanceFromEnemies = 0.02f;

	public const int GoldFromBosses = 3;

	public const float SetBonusCritPerGoldCoin = 0.2f;

	public const float MaximumCritBonus = 5f;

	public override int? HeadPieceID => 92;

	public override int? BodyPieceID => 83;

	public override int? LegPieceID => 79;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 955 };

	public override string ArmorSetName => "Gold";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(0.02f.ToPercent(), 3, 0.2f, 5f);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().goldArmorGoldDrops = true;
		float critFromGold = ((!player.InventoryHas(74)) ? Math.Min((float)player.CountItem(73, 90) * 0.2f, 5f) : 5f);
		player.GetCritChance<GenericDamageClass>() += critFromGold;
	}
}
