using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CalamityMod.CalPlayer;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;

namespace CalamityMod.UI.ResourceSets;

public class CalamityResourceOverlay : ModResourceOverlay
{
	private Dictionary<string, Asset<Texture2D>> vanillaAssetCache = new Dictionary<string, Asset<Texture2D>>();

	private const string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";

	private const string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

	[CompilerGenerated]
	private Asset<Texture2D> _003CFancyHeartFill_003Ek__BackingField;

	[CompilerGenerated]
	private Asset<Texture2D> _003CFancyHeartFillB_003Ek__BackingField;

	[CompilerGenerated]
	private Asset<Texture2D> _003CFancyStarFill_003Ek__BackingField;

	[CompilerGenerated]
	private Asset<Texture2D> _003CBarMPFill_003Ek__BackingField;

	[CompilerGenerated]
	private Asset<Texture2D> _003CBarHPFill_003Ek__BackingField;

	[CompilerGenerated]
	private Asset<Texture2D> _003CBarHPFillHoney_003Ek__BackingField;

	private Asset<Texture2D> FancyHeartFill => _003CFancyHeartFill_003Ek__BackingField ?? (_003CFancyHeartFill_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/FancyClassic/Heart_Fill", (AssetRequestMode)2));

	private Asset<Texture2D> FancyHeartFillB => _003CFancyHeartFillB_003Ek__BackingField ?? (_003CFancyHeartFillB_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/FancyClassic/Heart_Fill_B", (AssetRequestMode)2));

	private Asset<Texture2D> FancyStarFill => _003CFancyStarFill_003Ek__BackingField ?? (_003CFancyStarFill_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/FancyClassic/Star_Fill", (AssetRequestMode)2));

	private Asset<Texture2D> BarMPFill => _003CBarMPFill_003Ek__BackingField ?? (_003CBarMPFill_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/HorizontalBars/MP_Fill", (AssetRequestMode)2));

	private Asset<Texture2D> BarHPFill => _003CBarHPFill_003Ek__BackingField ?? (_003CBarHPFill_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/HorizontalBars/HP_Fill", (AssetRequestMode)2));

	private Asset<Texture2D> BarHPFillHoney => _003CBarHPFillHoney_003Ek__BackingField ?? (_003CBarHPFillHoney_003Ek__BackingField = Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/HorizontalBars/HP_Fill_Honey", (AssetRequestMode)2));

	public static CalamityUIResourceSet GetLifeTextureSet()
	{
		CalamityPlayer modPlayer = Main.LocalPlayer.Calamity();
		if (modPlayer.chaliceHeartStyle)
		{
			return CalamityUIResourceSets.HPChalice;
		}
		if (modPlayer.sStrawberry)
		{
			return CalamityUIResourceSets.HPSacredStrawberry;
		}
		if (modPlayer.tCloudberry)
		{
			return CalamityUIResourceSets.HPTaintedCloudberry;
		}
		if (modPlayer.mFruit)
		{
			return CalamityUIResourceSets.HPMiracleFruit;
		}
		if (modPlayer.sTangerine)
		{
			return CalamityUIResourceSets.HPSanguineTangerine;
		}
		return null;
	}

	public static CalamityUIResourceSet GetManaTextureSet()
	{
		CalamityPlayer modPlayer = Main.LocalPlayer.Calamity();
		if (Main.LocalPlayer.statMana < 0 && Main.LocalPlayer.Calamity().ChaosStone)
		{
			return CalamityUIResourceSets.MPManaBurn;
		}
		if (modPlayer.pHeart)
		{
			return CalamityUIResourceSets.MPPhantomHeart;
		}
		if (modPlayer.eCore)
		{
			return CalamityUIResourceSets.MPEtherealCore;
		}
		if (modPlayer.cShard)
		{
			return CalamityUIResourceSets.MPCometShard;
		}
		return null;
	}

	public override void PostDrawResource(ResourceOverlayDrawContext context)
	{
		Asset<Texture2D> asset = context.texture;
		CalamityUIResourceSet manaTextureSet = GetManaTextureSet();
		if (manaTextureSet != null)
		{
			if (asset == TextureAssets.Mana || asset == FancyStarFill)
			{
				context.texture = manaTextureSet.Star;
				context.Draw();
			}
			else if (asset == BarMPFill)
			{
				context.texture = manaTextureSet.Bar;
				context.Draw();
			}
		}
		CalamityUIResourceSet lifeTextureSet = GetLifeTextureSet();
		if (lifeTextureSet != null)
		{
			if (asset == TextureAssets.Heart || asset == TextureAssets.Heart2 || asset == FancyHeartFill || asset == FancyHeartFillB)
			{
				context.texture = lifeTextureSet.Heart;
				context.Draw();
			}
			else if (asset == BarHPFill || asset == BarHPFillHoney)
			{
				context.texture = lifeTextureSet.Bar;
				context.Draw();
			}
		}
	}

	public override void PostDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, Color textColor, bool drawText)
	{
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		Player Player = Main.LocalPlayer;
		CalamityPlayer CalPlayer = Player.Calamity();
		if (drawingLife)
		{
			if (!CalPlayer.chaliceHeartStyle)
			{
				return;
			}
			double bleed = CalPlayer.chaliceBleedoutBuffer;
			int hearts = snapshot.AmountOfLifeHearts;
			int drawType = -1;
			Vector2 position = default(Vector2);
			((Vector2)(ref position))._002Ector((float)(Main.screenWidth - 60), 28f);
			switch (displaySet.NameKey)
			{
			case "HorizontalBarsWithText":
				drawType = 0;
				break;
			case "HorizontalBarsWithFullText":
				position.Y -= 2f;
				drawType = 0;
				break;
			case "HorizontalBars":
				position.Y -= 4f;
				drawType = 0;
				break;
			case "Default":
				drawType = 1;
				((Vector2)(ref position))._002Ector((float)(Main.screenWidth - 289), 43f);
				break;
			case "New":
				drawType = 2;
				((Vector2)(ref position))._002Ector((float)(Main.screenWidth - 281), 30f);
				break;
			case "NewWithText":
				drawType = 2;
				((Vector2)(ref position))._002Ector((float)(Main.screenWidth - 281), 36f);
				break;
			}
			switch (drawType)
			{
			case 0:
			{
				int width = snapshot.AmountOfLifeHearts * 12;
				float pixelsPerLife = 12f / snapshot.LifePerSegment;
				int deadPixels = (int)MathF.Floor((float)(snapshot.LifeMax - snapshot.Life) * pixelsPerLife);
				int bleedPixels = (int)MathF.Ceiling((float)bleed * pixelsPerLife);
				bleedPixels = Math.Min(bleedPixels, width - deadPixels);
				if (bleedPixels < 0)
				{
					break;
				}
				int drawX = (int)position.X - width + deadPixels;
				using (Main.spriteBatch.Scope())
				{
					Vector2 bleedDrawPos = default(Vector2);
					((Vector2)(ref bleedDrawPos))._002Ector((float)drawX, position.Y);
					Rectangle bleedDrawRect = default(Rectangle);
					((Rectangle)(ref bleedDrawRect))._002Ector(deadPixels % 12, 0, bleedPixels, 12);
					Vector2 bleedDrawOrigin = default(Vector2);
					((Vector2)(ref bleedDrawOrigin))._002Ector(0f, 0f);
					Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Main.UIScaleMatrix);
					Main.spriteBatch.Draw(CalamityUIResourceSets.HPChaliceBleed.Bar.Value, bleedDrawPos, (Rectangle?)bleedDrawRect, Color.White, 0f, bleedDrawOrigin, 1f, (SpriteEffects)0, 1f);
					Main.spriteBatch.End();
					break;
				}
			}
			case 1:
			{
				Texture2D heartTexture2 = CalamityUIResourceSets.HPChaliceBleed.Heart.Value;
				Vector2 PosOffset2 = default(Vector2);
				for (int j = 0; j < hearts; j++)
				{
					((Vector2)(ref PosOffset2))._002Ector((float)(((j >= 10) ? (j - 10) : j) * 26), Math.Min(MathF.Floor(j / 10), 1f) * 26f);
					float opacity2 = Math.Clamp((0f - ((float)j * snapshot.LifePerSegment - (float)snapshot.Life)) / snapshot.LifePerSegment, 0f, 1f);
					if ((float)j * snapshot.LifePerSegment > (float)snapshot.Life)
					{
						opacity2 = 0f;
					}
					opacity2 = Math.Clamp(0f - ((float)j * snapshot.LifePerSegment - (float)bleed) / snapshot.LifePerSegment, 0f, 1f);
					Main.spriteBatch.Draw(heartTexture2, position + PosOffset2, (Rectangle?)null, Color.White, 0f, heartTexture2.Size() / 2f, 1f * opacity2, (SpriteEffects)0, 1f);
				}
				break;
			}
			case 2:
			{
				Texture2D heartTexture = CalamityUIResourceSets.HPChaliceBleed.Heart.Value;
				Vector2 PosOffset = default(Vector2);
				for (int i = 0; i < hearts; i++)
				{
					((Vector2)(ref PosOffset))._002Ector((float)(((i >= 10) ? (i - 10) : i) * 24), Math.Min(MathF.Floor(i / 10), 1f) * 28f);
					float opacity = Math.Clamp((0f - ((float)i * snapshot.LifePerSegment - (float)snapshot.Life)) / snapshot.LifePerSegment, 0f, 1f);
					if ((float)i * snapshot.LifePerSegment > (float)snapshot.Life)
					{
						opacity = 0f;
					}
					opacity = Math.Clamp(0f - ((float)i * snapshot.LifePerSegment - (float)bleed) / snapshot.LifePerSegment, 0f, 1f);
					Main.spriteBatch.Draw(heartTexture, position + PosOffset, (Rectangle?)null, Color.White, 0f, heartTexture.Size() / 2f, 1f * opacity, (SpriteEffects)0, 1f);
				}
				break;
			}
			}
		}
		else
		{
			if (Player.statMana >= 0 || !Player.Calamity().ChaosStone)
			{
				return;
			}
			CalamityUIResourceSet manaSet = GetManaTextureSet();
			int mana = -Player.statMana;
			int stars = snapshot.AmountOfManaStars;
			int drawType2 = -1;
			Vector2 position2 = default(Vector2);
			((Vector2)(ref position2))._002Ector((float)(Main.screenWidth - 70), 52f);
			switch (displaySet.NameKey)
			{
			case "HorizontalBarsWithText":
				drawType2 = 0;
				break;
			case "HorizontalBarsWithFullText":
				position2.Y -= 2f;
				drawType2 = 0;
				break;
			case "HorizontalBars":
				position2.Y -= 4f;
				drawType2 = 0;
				break;
			case "Default":
				drawType2 = 1;
				((Vector2)(ref position2))._002Ector((float)(Main.screenWidth - 25), 43f);
				break;
			case "New":
				drawType2 = 2;
				((Vector2)(ref position2))._002Ector((float)(Main.screenWidth - 25), 38f);
				break;
			case "NewWithText":
				drawType2 = 2;
				((Vector2)(ref position2))._002Ector((float)(Main.screenWidth - 25), 38f);
				break;
			}
			switch (drawType2)
			{
			case 0:
			{
				float pixelsPerStar = 12f / snapshot.ManaPerSegment;
				int bleedPixels2 = (int)MathF.Ceiling((float)mana * pixelsPerStar);
				if (bleedPixels2 < 0)
				{
					break;
				}
				using (Main.spriteBatch.Scope())
				{
					Vector2 bleedDrawPos2 = default(Vector2);
					((Vector2)(ref bleedDrawPos2))._002Ector(position2.X, position2.Y);
					Rectangle bleedDrawRect2 = default(Rectangle);
					((Rectangle)(ref bleedDrawRect2))._002Ector(-(bleedPixels2 % 12), 0, bleedPixels2, 12);
					Vector2 bleedDrawOrigin2 = default(Vector2);
					((Vector2)(ref bleedDrawOrigin2))._002Ector((float)bleedPixels2, 0f);
					Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Main.UIScaleMatrix);
					Main.spriteBatch.Draw(CalamityUIResourceSets.MPManaBurn.Bar.Value, bleedDrawPos2, (Rectangle?)bleedDrawRect2, Color.White, 0f, bleedDrawOrigin2, 1f, (SpriteEffects)0, 1f);
					Main.spriteBatch.End();
					break;
				}
			}
			case 1:
			{
				Texture2D heartTexture4 = manaSet.Star.Value;
				Vector2 PosOffset4 = default(Vector2);
				for (int l = 0; l < stars; l++)
				{
					((Vector2)(ref PosOffset4))._002Ector(0f, (float)(28 * l));
					float opacity4 = Math.Clamp((0f - ((float)l * snapshot.ManaPerSegment - (float)snapshot.Mana)) / snapshot.ManaPerSegment, -100f, 100f);
					if ((float)l * snapshot.ManaPerSegment > (float)(-snapshot.Mana))
					{
						opacity4 = 0f;
					}
					opacity4 = Math.Clamp(0f - ((float)l * snapshot.ManaPerSegment - (float)mana) / snapshot.ManaPerSegment, 0f, 1f);
					Main.spriteBatch.Draw(heartTexture4, position2 + PosOffset4, (Rectangle?)null, Color.White, 0f, heartTexture4.Size() / 2f, 1f * opacity4, (SpriteEffects)0, 1f);
				}
				break;
			}
			case 2:
			{
				Texture2D heartTexture3 = manaSet.Star.Value;
				Vector2 PosOffset3 = default(Vector2);
				for (int k = 0; k < stars; k++)
				{
					((Vector2)(ref PosOffset3))._002Ector(0f, (float)(22 * k));
					float opacity3 = Math.Clamp((0f - ((float)k * snapshot.ManaPerSegment - (float)snapshot.Mana)) / snapshot.ManaPerSegment, -100f, 100f);
					if ((float)k * snapshot.ManaPerSegment > (float)(-snapshot.Mana))
					{
						opacity3 = 0f;
					}
					opacity3 = Math.Clamp(0f - ((float)k * snapshot.ManaPerSegment - (float)mana) / snapshot.ManaPerSegment, 0f, 1f);
					Main.spriteBatch.Draw(heartTexture3, position2 + PosOffset3, (Rectangle?)null, Color.White, 0f, heartTexture3.Size() / 2f, 1f * opacity3, (SpriteEffects)0, 1f);
				}
				break;
			}
			}
		}
	}
}
