using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class SilverArmorSetChange : VanillaArmorChange
{
	public const double SetBonusMinimumDamageToHeal = 20.0;

	public const int SetBonusHealTime = 120;

	public const int SetBonusHealAmount = 10;

	public override int? HeadPieceID => 91;

	public override int? BodyPieceID => 82;

	public override int? LegPieceID => 78;

	public override string ArmorSetName => "Silver";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(120.FramesToSeconds(), 20.0.ToString("N0"), 10);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().silverMedkit = true;
	}

	internal static void OnHealEffects(Entity entity)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dustCenter = entity.Center;
		int numDust = 36;
		for (int i = 0; i < numDust; i++)
		{
			float theta = (float)Math.PI * 2f * ((float)i / 36f);
			Vector2 dustVel = 3.5f * Vector2.One.RotatedBy(theta);
			Dust dust = Dust.NewDustPerfect(dustCenter, 245, dustVel, 0, default(Color), 1.4f);
			dust.noGravity = true;
			dust.noLight = false;
		}
	}
}
