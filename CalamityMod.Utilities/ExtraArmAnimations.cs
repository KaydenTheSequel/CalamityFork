using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Utilities;

public class ExtraArmAnimations
{
	public static void ThrowArmAnimationSlow(Player player, Item item)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		Vector2 mW = player.Calamity().mouseWorld;
		float value1 = player.direction * -90;
		float value2 = player.direction * 180;
		float value3 = player.direction * -240;
		float g1 = item.useAnimation;
		float gg = (float)player.itemAnimation / g1;
		player.direction = Math.Sign(mW.X - player.Center.X);
		if (player.direction == 0)
		{
			player.direction = 1;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.AngleTo(mW) + ((player.direction == 1) ? MathHelper.ToRadians(180f) : 0f) + MathHelper.ToRadians(MathHelper.Lerp(value2, value1, CalamityUtils.SineInOutEasing(gg, 1))));
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, player.AngleTo(mW) + ((player.direction == 1) ? MathHelper.ToRadians(180f) : 0f) + MathHelper.ToRadians(MathHelper.Lerp(value1, value3, CalamityUtils.SineInOutEasing(gg, 1))));
	}

	public static void ThrowArmAnimationFast(Player player, Item item)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		Vector2 mW = player.Calamity().mouseWorld;
		float value1 = player.direction * -90;
		float value2 = player.direction * 180;
		float value3 = player.direction * -240;
		float g1 = item.useAnimation;
		float gg = (float)player.itemAnimation / g1;
		player.direction = Math.Sign(mW.X - player.Center.X);
		if (player.direction == 0)
		{
			player.direction = 1;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.AngleTo(mW) + ((player.direction == 1) ? MathHelper.ToRadians(180f) : 0f) + MathHelper.ToRadians(MathHelper.Lerp(value2, value1, CalamityUtils.CircInEasing(gg, 1))));
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, player.AngleTo(mW) + ((player.direction == 1) ? MathHelper.ToRadians(180f) : 0f) + MathHelper.ToRadians(MathHelper.Lerp(value1, value3, CalamityUtils.CircInEasing(gg, 1))));
	}
}
