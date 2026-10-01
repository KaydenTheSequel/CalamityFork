using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts.WaterSplash;

public abstract class LavaSplashDust : ModDust
{
	public abstract Vector3 LightColor { get; }

	public override void OnSpawn(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		dust.velocity *= 0.1f;
		dust.velocity.Y = -0.5f;
	}

	public override bool Update(Dust dust)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		if (dust.scale > 10f)
		{
			dust.active = false;
		}
		Dust.lavaBubbles++;
		dust.position += dust.velocity;
		if (!dust.noGravity)
		{
			dust.velocity.Y += 0.1f;
		}
		if (dust.noGravity)
		{
			dust.scale += 0.03f;
			if (dust.scale < 1f)
			{
				dust.velocity.Y += 0.075f;
			}
			dust.velocity.X *= 1.08f;
			dust.rotation += ((dust.velocity.X > 0f) ? 0.01f : (-0.01f));
			float intensity = Math.Min(dust.scale * 0.6f, 1f);
			int i = (int)(dust.position.X / 16f);
			int tileY = (int)(dust.position.Y / 16f + 1f);
			Lighting.AddLight(i, tileY, intensity * LightColor.X, intensity * LightColor.Y, intensity * LightColor.Z);
		}
		else
		{
			if (!Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
			{
				dust.scale = 0f;
			}
			else
			{
				dust.alpha += Main.rand.Next(2);
				if (dust.alpha > 255)
				{
					dust.scale = 0f;
				}
				dust.velocity.Y = -0.5f;
				dust.alpha++;
				dust.scale -= 0.01f;
				dust.velocity.Y = -0.2f;
				dust.velocity.X += (float)Main.rand.Next(-10, 10) * 0.002f;
				if (dust.velocity.X < -0.25f)
				{
					dust.velocity.X = -0.25f;
				}
				if (dust.velocity.X > 0.25f)
				{
					dust.velocity.X = 0.25f;
				}
			}
			float intensity2 = dust.scale * 0.3f + 0.4f;
			if (intensity2 > 1f)
			{
				intensity2 = 1f;
			}
			int i2 = (int)(dust.position.X / 16f);
			int tileY2 = (int)(dust.position.Y / 16f);
			Lighting.AddLight(i2, tileY2, intensity2 * LightColor.X, intensity2 * LightColor.Y, intensity2 * LightColor.Z);
		}
		dust.rotation += dust.velocity.X * 0.5f;
		if (dust.fadeIn > 0f && dust.fadeIn < 100f)
		{
			dust.scale += 0.03f;
			if (dust.scale > dust.fadeIn)
			{
				dust.fadeIn = 0f;
			}
		}
		dust.scale -= 0.01f;
		if (dust.noGravity)
		{
			dust.velocity *= 0.92f;
			if (dust.fadeIn == 0f)
			{
				dust.scale -= 0.04f;
			}
		}
		if (dust.position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
		{
			dust.active = false;
		}
		Dust dust2;
		float scale = (dust2 = dust).scale;
		float dCount = Dust.dCount;
		float num = ((dCount == 0.5f) ? 0.001f : ((dCount == 0.6f) ? 0.0025f : ((dCount == 0.7f) ? 0.005f : ((dCount == 0.8f) ? 0.01f : ((dCount != 0.9f) ? 0f : 0.02f)))));
		dust2.scale = scale - num;
		num = Dust.dCount;
		dCount = ((num == 0.5f) ? 0.11f : ((num == 0.6f) ? 0.13f : ((num == 0.7f) ? 0.16f : ((num == 0.8f) ? 0.22f : ((num != 0.9f) ? 0.1f : 0.25f)))));
		float despawnScaleThreshold = dCount;
		if (dust.scale < despawnScaleThreshold)
		{
			dust.active = false;
		}
		return false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		float colorIntensity = (float)(255 - dust.alpha) / 255f;
		colorIntensity = (colorIntensity + 3f) / 4f;
		int num = (int)((float)(int)((Color)(ref lightColor)).R * colorIntensity);
		int G = (int)((float)(int)((Color)(ref lightColor)).G * colorIntensity);
		int B = (int)((float)(int)((Color)(ref lightColor)).B * colorIntensity);
		int alpha = ((Color)(ref lightColor)).A - dust.alpha;
		return new Color(num, G, B, alpha);
	}
}
