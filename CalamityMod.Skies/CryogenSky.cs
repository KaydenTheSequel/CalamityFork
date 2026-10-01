using System;
using CalamityMod.NPCs.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class CryogenSky : CustomSky
{
	public struct CryogenAurora
	{
		public float Depth;

		public float ColorHueOffset;

		public float CenterOffsetRatio;

		public Vector2 Center;

		public SpriteEffects DirectionEffect;
	}

	public int CryogenIndex = -1;

	public float FadeInCountdown;

	public float FadeoutTimer;

	public CryogenAurora[] Auroras;

	public static bool ShouldDrawRegularly;

	public static void UpdateDrawEligibility()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		bool useEffect = (NPC.AnyNPCs(ModContent.NPCType<Cryogen>()) || ShouldDrawRegularly || Main.LocalPlayer.Calamity().monolithCryogenShader > 0) && !Main.gameMenu;
		if (SkyManager.Instance["CalamityMod:Cryogen"] != null && useEffect != SkyManager.Instance["CalamityMod:Cryogen"].IsActive())
		{
			if (useEffect)
			{
				SkyManager.Instance.Activate("CalamityMod:Cryogen", default(Vector2));
			}
			else
			{
				SkyManager.Instance.Deactivate("CalamityMod:Cryogen");
			}
		}
		if (ShouldDrawRegularly)
		{
			ShouldDrawRegularly = false;
		}
	}

	public override void Update(GameTime gameTime)
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		if (!ShouldDrawRegularly && Main.LocalPlayer.Calamity().monolithCryogenShader <= 0)
		{
			if (FadeInCountdown > 0f)
			{
				FadeInCountdown--;
			}
		}
		else
		{
			FadeInCountdown = 1f;
		}
		if (CryogenIndex == -1 && !ShouldDrawRegularly && Main.LocalPlayer.Calamity().monolithCryogenShader <= 0)
		{
			UpdateCryogenIndex();
			if (FadeoutTimer == 0f)
			{
				FadeoutTimer = 45f;
			}
			FadeoutTimer--;
			if (FadeoutTimer <= 0f)
			{
				Deactivate();
			}
		}
		float auroraStrength = CalculateAuroraStrength();
		float width = (float)Main.screenWidth * 0.5f;
		float height = (float)Main.screenHeight * 0.1f;
		Vector2 centerOffset = Vector2.UnitX * (float)Main.screenWidth * 0.5f;
		float time = Main.GlobalTimeWrappedHourly * 1.2f;
		float centerOffsetRatioIncrement = 0.00055555557f * MathHelper.Lerp(0.2f, 1f, auroraStrength) * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.9f);
		Vector2 centerRange = default(Vector2);
		for (int i = 0; i < Auroras.Length; i++)
		{
			((Vector2)(ref centerRange))._002Ector(width, height / Auroras[i].Depth);
			Auroras[i].Center = centerRange * (Auroras[i].CenterOffsetRatio * ((float)Math.PI * 2f)).ToRotationVector2() + centerOffset;
			Auroras[i].Center.Y -= 180f + (float)Math.Cos(time + Auroras[i].CenterOffsetRatio * (float)Math.PI) * 50f;
			Auroras[i].CenterOffsetRatio += centerOffsetRatioIncrement;
		}
	}

	public float GetCryogenLifeRatio()
	{
		if (UpdateCryogenIndex())
		{
			float lifeRatio = 0f;
			if (CryogenIndex != -1)
			{
				lifeRatio = (float)Main.npc[CryogenIndex].life / (float)Main.npc[CryogenIndex].lifeMax;
			}
			return lifeRatio;
		}
		return 0f;
	}

	public float CalculateAuroraStrength()
	{
		if (ShouldDrawRegularly || Main.LocalPlayer.Calamity().monolithCryogenShader > 0)
		{
			return 1f;
		}
		return 1f - GetCryogenLifeRatio();
	}

	public bool UpdateCryogenIndex()
	{
		int cryogenType = ModContent.NPCType<Cryogen>();
		if (CryogenIndex >= 0 && Main.npc[CryogenIndex].active && Main.npc[CryogenIndex].type == cryogenType)
		{
			return true;
		}
		CryogenIndex = NPC.FindFirstNPC(cryogenType);
		return CryogenIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		if (!(maxDepth >= float.MaxValue) || !(minDepth < float.MaxValue))
		{
			return;
		}
		Texture2D auroraTexture = CalamityUtils.AuroraTexture;
		float auroraStrength = CalculateAuroraStrength();
		float hueOffset = 0.5f * MathHelper.Lerp(0.15f, 0.95f, auroraStrength);
		float time = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 1.2f) * 0.25f;
		float time2 = Main.GlobalTimeWrappedHourly * 0.7f;
		float auroraColorLerp = MathHelper.Lerp(0.3f, 1f, auroraStrength);
		float fadeInLerp = Utils.GetLerpValue(45f, 0f, FadeInCountdown);
		float fadeOutLerp = Utils.GetLerpValue(0f, 45f, FadeoutTimer);
		float brightnessLerp = MathHelper.Lerp(0.6f, 1.1f, (float)Math.Sin(Main.GlobalTimeWrappedHourly / 1.8f) * 0.5f + 0.5f);
		Vector2 origin = auroraTexture.Size() * 0.5f;
		for (int i = 0; i < Auroras.Length; i++)
		{
			float hue = (0.5f + Auroras[i].ColorHueOffset * hueOffset + time) % 1f;
			float scale = 1.4f / Auroras[i].Depth;
			scale += (float)Math.Cos(time2 + Auroras[i].CenterOffsetRatio * ((float)Math.PI * 2f)) * 0.2f;
			Color auroraColor = Main.hslToRgb(hue, 1f, 0.825f) * 0.85f;
			auroraColor *= auroraColorLerp;
			if (FadeInCountdown > 0f)
			{
				auroraColor *= fadeInLerp;
			}
			if (FadeoutTimer > 0f)
			{
				auroraColor *= fadeOutLerp;
			}
			if (Main.IsItDay())
			{
				auroraColor *= 0.4f;
			}
			float yBrightness = MathHelper.Lerp(1.5f, 0.5f, 1f - MathHelper.Clamp((Auroras[i].Center.Y + 300f) / 200f, 0f, 1f)) * 1.3f;
			yBrightness *= brightnessLerp;
			if (yBrightness > 1.3f)
			{
				yBrightness = 1.3f;
			}
			auroraColor *= yBrightness;
			spriteBatch.Draw(auroraTexture, Auroras[i].Center, (Rectangle?)null, auroraColor * 1.1f, (float)Math.PI / 2f, origin, scale, Auroras[i].DirectionEffect, 0f);
		}
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		FadeInCountdown = 45f;
		Auroras = new CryogenAurora[150];
		float randomOffsetMax = 3f / (float)Auroras.Length;
		for (int i = 0; i < Auroras.Length; i++)
		{
			Auroras[i].Depth = Main.rand.NextFloat(1f, 2.2f);
			Auroras[i].ColorHueOffset = Main.rand.NextFloat(-1f, 1f);
			Auroras[i].DirectionEffect = Utils.SelectRandom(Main.rand, (SpriteEffects[])(object)new SpriteEffects[2]
			{
				default(SpriteEffects),
				(SpriteEffects)1
			});
			Auroras[i].CenterOffsetRatio = (float)i / (float)Auroras.Length + Main.rand.NextFloat(randomOffsetMax);
		}
	}

	public override void Reset()
	{
	}

	public override void Deactivate(params object[] args)
	{
	}

	public override bool IsActive()
	{
		if (CryogenIndex != -1 || FadeoutTimer > 0f || FadeInCountdown > 0f)
		{
			return !Main.gameMenu;
		}
		return false;
	}
}
