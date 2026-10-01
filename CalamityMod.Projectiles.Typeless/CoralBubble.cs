using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class CoralBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 1;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] > 2f)
		{
			base.Projectile.alpha -= 5;
			if (base.Projectile.alpha < 100)
			{
				base.Projectile.alpha = 100;
			}
		}
		else
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[1] > 30f)
		{
			if (base.Projectile.velocity.Y > -1.5f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.05f;
			}
		}
		else
		{
			base.Projectile.ai[1]++;
		}
		if (base.Projectile.wet)
		{
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y * 0.98f;
			}
			if (base.Projectile.velocity.Y > -1f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.2f;
			}
		}
		int closestPlayer = Player.FindClosest(base.Projectile.Center, 1, 1);
		if (base.Projectile.Distance(Main.player[closestPlayer].Center) < 14f)
		{
			SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.Center);
			Main.player[closestPlayer].AddBuff(4, 90);
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Vector3 lightCol = default(Vector3);
		((Vector3)(ref lightCol))._002Ector(1f / 3f, 0.5921569f, 0.76862746f);
		float brightness = 0.7f;
		float declareThisHereToPreventRunningTheSameCalculationMultipleTimes = (float)Main.GameUpdateCount * 0.01f;
		brightness *= MathF.Sin(8f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness += 0.4f;
		brightness = MathHelper.Clamp(brightness, 0.1f, 0.5f);
		lightCol *= brightness;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		Lighting.AddLight(base.Projectile.position, lightCol);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			int size = 12;
			int dustIndex = Dust.NewDust(base.Projectile.Center - Vector2.One * (float)size, size * 2, size * 2, 212);
			Dust dust = Main.dust[dustIndex];
			Vector2 value14 = Vector2.Normalize(dust.position - base.Projectile.Center);
			dust.position = base.Projectile.Center + value14 * (float)size;
			dust.velocity = value14 * ((Vector2)(ref dust.velocity)).Length();
			dust.color = Main.hslToRgb((float)(0.4000000059604645 + Main.rand.NextDouble() * 0.20000000298023224), 1f, 0.7f);
			dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
			dust.noGravity = true;
			dust.scale = 0.7f;
		}
	}
}
