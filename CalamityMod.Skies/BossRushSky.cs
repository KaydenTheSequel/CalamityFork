using System;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class BossRushSky : CustomSky
{
	public bool CurrentlyActive;

	public float Intensity;

	public static float IdleTimer;

	public static float CurrentInterest;

	public static float IncrementalInterest;

	public static float CurrentInterestMin;

	public static bool ShouldDrawRegularly;

	public static Color GeneralColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.LightGray, Color.Black, BossRushEvent.WhiteDimness) * 0.2f;
		}
	}

	public static bool DetermineDrawEligibility()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		int num;
		if ((!BossRushEvent.BossRushActive || BossRushEvent.StartTimer <= 100) && !ShouldDrawRegularly)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer == null || !(localPlayer.Calamity()?.monolithBossRushShader > 0))
			{
				num = 0;
				goto IL_0063;
			}
		}
		num = ((!Main.gameMenu) ? 1 : 0);
		goto IL_0063;
		IL_0063:
		bool useEffect = (byte)num != 0;
		if (SkyManager.Instance["CalamityMod:BossRush"] != null && useEffect != SkyManager.Instance["CalamityMod:BossRush"].IsActive())
		{
			if (useEffect)
			{
				SkyManager.Instance.Activate("CalamityMod:BossRush", default(Vector2));
			}
			else
			{
				SkyManager.Instance.Deactivate("CalamityMod:BossRush");
			}
		}
		return useEffect;
	}

	public override void Update(GameTime gameTime)
	{
		if (CurrentlyActive)
		{
			if (Intensity < 1f)
			{
				Intensity += 0.03f;
			}
			CurrentInterest = MathHelper.Clamp(CurrentInterest - 0.005f, CurrentInterestMin, 1f);
			IncrementalInterest = MathHelper.Lerp(IncrementalInterest, CurrentInterest, 0.085f);
			IdleTimer += MathHelper.Lerp(0.04f, 0.1f, IncrementalInterest);
		}
		else if (!CurrentlyActive && Intensity > 0f)
		{
			Intensity -= 0.03f;
			CurrentInterest = 0f;
			IncrementalInterest = 0f;
			IdleTimer = 0f;
		}
	}

	private float GetIntensity()
	{
		if (!ShouldDrawRegularly)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer == null || localPlayer.Calamity().monolithBossRushShader <= 0)
			{
				float fadeRatio = (float)BossRushEvent.StartTimer / 120f;
				return Utils.GetLerpValue(0.57f, 1f, fadeRatio, clamped: true);
			}
		}
		return 0.57f;
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Color generalColor = GeneralColor;
		return new Color(Vector4.Lerp(((Color)(ref generalColor)).ToVector4() * 0.5f, ((Color)(ref inColor)).ToVector4(), 1f - GetIntensity()));
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		if (GetIntensity() == 0f)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer != null && localPlayer.Calamity()?.monolithBossRushShader <= 0)
			{
				return;
			}
		}
		if (maxDepth >= 0f && minDepth < 0f && GetIntensity() > 0f)
		{
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), GeneralColor * GetIntensity() * 0.5f);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive);
		if ((float)BossRushEvent.EndTimer >= 100f)
		{
			Texture2D whiteTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/XerocLight", (AssetRequestMode)2).Value;
			Vector2 screenCenter = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			float fadeToWhite = Utils.GetLerpValue(110f, 140f, BossRushEvent.EndTimer, clamped: true);
			screenCenter += new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * (Main.GameViewMatrix.Zoom - Vector2.One) * 0.5f;
			fadeToWhite *= Utils.GetLerpValue(335f, 315f, BossRushEvent.EndTimer, clamped: true);
			float backScale = MathHelper.Lerp(0.01f, 8f, fadeToWhite);
			Color backFadeColor = Color.White * fadeToWhite * 0.64f;
			spriteBatch.Draw(whiteTexture, screenCenter, (Rectangle?)null, backFadeColor, 0f, whiteTexture.Size() * 0.5f, backScale, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin();
		Color baseXerocColor = default(Color);
		((Color)(ref baseXerocColor))._002Ector(209, 183, 50);
		Color dimXerocColor = default(Color);
		((Color)(ref dimXerocColor))._002Ector(181, 164, 81);
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue)
		{
			if (!((float)BossRushEvent.EndTimer < 300f) && !ShouldDrawRegularly)
			{
				Player localPlayer2 = Main.LocalPlayer;
				if (localPlayer2 == null || !(localPlayer2.Calamity()?.monolithBossRushShader > 0))
				{
					goto IL_0483;
				}
			}
			Vector2 screenCenter2 = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			screenCenter2 += new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * (Main.GameViewMatrix.Zoom - Vector2.One) * 0.5f;
			float scale = MathHelper.Lerp(0.8f, 0.9f, IncrementalInterest) + (float)Math.Sin(IdleTimer) * 0.01f;
			Vector2 drawPosition = (new Vector2(Main.LocalPlayer.Center.X, 1120f) - screenCenter2) * 0.097f + screenCenter2 - Main.screenPosition - Vector2.UnitY * 100f;
			Texture2D eyeTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/XerocEye", (AssetRequestMode)2).Value;
			Color baseColorDraw = Color.Lerp(baseXerocColor, Color.DimGray, IncrementalInterest);
			Vector2 origin = eyeTexture.Size() * 0.5f;
			spriteBatch.Draw(eyeTexture, drawPosition, (Rectangle?)null, baseColorDraw, 0f, origin, scale, (SpriteEffects)0, 0f);
			Color fadedColor = Color.Lerp(baseColorDraw, dimXerocColor, 0.3f) * MathHelper.Lerp(0.18f, 0.3f, IncrementalInterest);
			((Color)(ref fadedColor)).A = 0;
			float backEyeOutwardness = MathHelper.Lerp(8f, 4f, IncrementalInterest);
			int backInstances = (int)MathHelper.Lerp(6f, 24f, IncrementalInterest);
			float fourPi = (float)Math.PI * 4f;
			float time = Main.GlobalTimeWrappedHourly * 2.1f;
			for (int i = 0; i < backInstances; i++)
			{
				Vector2 drawOffset = (fourPi * (float)i / (float)backInstances + time).ToRotationVector2() * backEyeOutwardness;
				spriteBatch.Draw(eyeTexture, drawPosition + drawOffset, (Rectangle?)null, fadedColor, 0f, origin, scale, (SpriteEffects)0, 0f);
			}
		}
		goto IL_0483;
		IL_0483:
		if (ShouldDrawRegularly)
		{
			ShouldDrawRegularly = false;
		}
	}

	public override float GetCloudAlpha()
	{
		return 1f - GetIntensity();
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		CurrentlyActive = true;
	}

	public override void Deactivate(params object[] args)
	{
		CurrentlyActive = false;
	}

	public override void Reset()
	{
		CurrentlyActive = false;
	}

	public override bool IsActive()
	{
		if (!CurrentlyActive)
		{
			return Intensity > 0f;
		}
		return true;
	}
}
