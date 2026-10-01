using System;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyAura : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.aiStyle = -1;
		base.AIType = -1;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 210;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		int provIndex = CalamityGlobalNPC.holyBoss;
		if (provIndex >= 0 && provIndex < Main.maxNPCs && Main.npc[provIndex].active)
		{
			base.Projectile.Center = Main.npc[provIndex].Center;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() / 2f;
		float time = Main.GlobalTimeWrappedHourly % 10f / 10f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		int drawnAmt = 45;
		float[] posX = new float[drawnAmt];
		float[] posY = new float[drawnAmt];
		float[] hue = new float[drawnAmt];
		float[] size = new float[drawnAmt];
		int totalTime = 210;
		Utils.GetLerpValue(0f, 60f, base.Projectile.timeLeft, clamped: true);
		Utils.GetLerpValue(totalTime, totalTime - 60, base.Projectile.timeLeft, clamped: true);
		float colorChangeAmt2 = Utils.GetLerpValue(0f, 60f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(totalTime, 90f, base.Projectile.timeLeft, clamped: true);
		colorChangeAmt2 = Utils.GetLerpValue(0.2f, 0.5f, colorChangeAmt2, clamped: true);
		float sizeScale = 0.8f;
		float sizeScalar = (1f - sizeScale) / (float)drawnAmt;
		float yPosOffset = 60f;
		float xPosOffset = 400f;
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(12f, 12f);
		float amount2 = CalamityUtils.SineBumpEasing((float)base.Projectile.timeLeft / (float)totalTime, 1);
		for (int i = 0; i < drawnAmt; i++)
		{
			float timeScalar = (float)Math.Sin(time * ((float)Math.PI * 2f) + (float)Math.PI / 2f + (float)i / 2f);
			posX[i] = timeScalar * (xPosOffset - (float)i * 3f) * amount2;
			posY[i] = (float)Math.Sin(time * ((float)Math.PI * 2f) * 4f + (float)Math.PI / 3f + (float)i) * yPosOffset * 2f;
			posY[i] -= (float)i * 3f;
			hue[i] = (float)i / (float)drawnAmt * 2f + time;
			hue[i] = (timeScalar * 0.5f + 0.5f) * 0.6f + time;
			size[i] = sizeScale + (float)(i + 1) * sizeScalar;
			size[i] *= 0.3f;
			float a = (float)Math.Sin(amount2 / 20f) + 1f;
			Color color = Color.Lerp(ProvUtils.GetProjectileColor(0, Outline: true), ProvUtils.GetProjectileColor(0), a);
			bool underworld = base.Projectile.ai[0] == 2f;
			if (!Main.zenithWorld)
			{
				if (ProvUtils.StandardAI())
				{
					((Color)(ref color)).R = byte.MaxValue;
					if (underworld)
					{
						((Color)(ref color)).B = 0;
					}
				}
				else
				{
					byte blueValue = (byte)MathHelper.Clamp(MathHelper.Lerp(0f, 255f, Main.npc[CalamityGlobalNPC.holyBoss].Calamity().newAI[3] / 120f), 0f, 255f);
					if (blueValue > byte.MaxValue)
					{
						blueValue = byte.MaxValue;
					}
					((Color)(ref color)).B = blueValue;
					if (underworld)
					{
						((Color)(ref color)).G = (byte)(255 - blueValue);
					}
					else
					{
						((Color)(ref color)).R = (byte)(255 - blueValue);
					}
				}
			}
			((Color)(ref color)).A = 0;
			if (((Color)(ref color)).R > 0)
			{
				((Color)(ref color)).R = (byte)MathHelper.Lerp(0f, (float)(int)((Color)(ref color)).R, amount2);
			}
			if (((Color)(ref color)).G > 0)
			{
				((Color)(ref color)).G = (byte)MathHelper.Lerp(0f, (float)(int)((Color)(ref color)).G, amount2);
			}
			if (((Color)(ref color)).B > 0)
			{
				((Color)(ref color)).B = (byte)MathHelper.Lerp(0f, (float)(int)((Color)(ref color)).B, amount2);
			}
			((Color)(ref color)).A = (byte)MathHelper.Lerp(0f, (float)(int)((Color)(ref color)).A, amount2);
			float rotation = (float)Math.PI / 2f + timeScalar * ((float)Math.PI / 4f) * -0.3f;
			Main.EntitySpriteDraw(texture, drawPosition + new Vector2(posX[i], posY[i]), null, color, rotation, origin, new Vector2(size[i], size[i]) * scale, (SpriteEffects)0);
		}
		return false;
	}
}
