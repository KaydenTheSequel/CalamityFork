using CalamityMod.CalPlayer;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public abstract class InvasionProgressUI : ModType
{
	public virtual int SecondaryDigitPrecision { get; }

	public abstract bool IsActive { get; }

	public abstract float CompletionRatio { get; }

	public abstract string InvasionName { get; }

	public abstract Color InvasionBarColor { get; }

	public abstract Texture2D IconTexture { get; }

	protected sealed override void Register()
	{
		ModTypeLookup<InvasionProgressUI>.Register(this);
		InvasionProgressUIManager.gUIs.Add(this);
	}

	public virtual void DrawBlueBar(SpriteBatch spriteBatch, Vector2 barDrawPosition, int barOffsetY)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		int barWidth = 200;
		int barHeight = 45;
		barDrawPosition.Y += barOffsetY;
		Rectangle screenCoordsRectangle = default(Rectangle);
		((Rectangle)(ref screenCoordsRectangle))._002Ector((int)barDrawPosition.X - barWidth / 2, (int)barDrawPosition.Y - barHeight / 2, barWidth, barHeight);
		Texture2D barTexture = TextureAssets.ColorBar.Value;
		Utils.DrawInvBG(spriteBatch, screenCoordsRectangle, new Color(63, 65, 151, 255) * 0.785f);
		spriteBatch.Draw(barTexture, barDrawPosition, (Rectangle?)null, Color.White, 0f, new Vector2((float)(barTexture.Width / 2), 0f), 1f, (SpriteEffects)0, 0f);
	}

	public virtual void DrawProgressText(SpriteBatch spriteBatch, float yScale, Vector2 baseBarDrawPosition, int barOffsetY, out Vector2 newBarPosition)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		string progressText = (100f * CompletionRatio).ToString($"N{SecondaryDigitPrecision}") + "%";
		progressText = Language.GetTextValue("Game.WaveCleared", progressText);
		Vector2 textSize = FontAssets.MouseText.Value.MeasureString(progressText);
		float progressTextScale = 1f;
		if (textSize.Y > 22f)
		{
			progressTextScale *= 22f / textSize.Y;
		}
		newBarPosition = baseBarDrawPosition + Vector2.UnitY * (yScale + (float)barOffsetY);
		Utils.DrawBorderString(spriteBatch, progressText, newBarPosition - Vector2.UnitY * 4f, Color.White, progressTextScale, 0.5f, 1f);
	}

	public virtual void DrawBackground(SpriteBatch spriteBatch, float yScale, Vector2 baseBarDrawPosition, int barOffsetY)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		float barDrawOffsetX = 169f;
		Vector2 barDrawPosition = baseBarDrawPosition + Vector2.UnitX * (CompletionRatio - 0.5f) * barDrawOffsetX;
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, barDrawPosition, (Rectangle?)new Rectangle(0, 0, 1, 1), new Color(255, 241, 51), 0f, new Vector2(1f, 0.5f), new Vector2(barDrawOffsetX * CompletionRatio, yScale), (SpriteEffects)0, 0f);
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, barDrawPosition, (Rectangle?)new Rectangle(0, 0, 1, 1), new Color(255, 165, 0, 127), 0f, new Vector2(1f, 0.5f), new Vector2(2f, yScale), (SpriteEffects)0, 0f);
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, barDrawPosition, (Rectangle?)new Rectangle(0, 0, 1, 1), Color.Black, 0f, Vector2.UnitY * 0.5f, new Vector2(barDrawOffsetX * (1f - CompletionRatio), yScale), (SpriteEffects)0, 0f);
	}

	public virtual void DrawProgressTextAndIcons(SpriteBatch spriteBatch, int barOffsetY)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 textMeasurement = FontAssets.MouseText.Value.MeasureString(InvasionName);
		float x = 120f;
		if (textMeasurement.X > 200f)
		{
			x += textMeasurement.X - 200f;
		}
		Rectangle iconRectangle = Utils.CenteredRectangle(new Vector2((float)Main.screenWidth - x, (float)(Main.screenHeight - 80 + barOffsetY)), textMeasurement + new Vector2((float)(IconTexture.Width + 12), 6f));
		Utils.DrawInvBG(spriteBatch, iconRectangle, InvasionBarColor * 0.5f);
		spriteBatch.Draw(IconTexture, iconRectangle.Left() + Vector2.UnitX * 8f, (Rectangle?)null, Color.White, 0f, Vector2.UnitY * (float)IconTexture.Height / 2f, 0.8f, (SpriteEffects)0, 0f);
		Utils.DrawBorderString(spriteBatch, InvasionName, iconRectangle.Right() + Vector2.UnitX * -16f, Color.White, 0.9f, 1f, 0.4f);
	}

	public virtual void Draw(SpriteBatch spriteBatch)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (IsActive && Main.invasionProgressMode != 0)
		{
			int barOffsetY = 0;
			int totalBars = 0;
			if (Main.invasionProgressNearInvasion || Main.invasionProgressAlpha > 0f)
			{
				totalBars++;
			}
			if (CalamityPlayer.areThereAnyDamnBosses || BossRushEvent.BossRushActive)
			{
				totalBars++;
			}
			totalBars += InvasionProgressUIManager.TotalGUIsActive;
			barOffsetY -= 85 * (totalBars - 1);
			Vector2 barDrawPosition = default(Vector2);
			((Vector2)(ref barDrawPosition))._002Ector((float)(Main.screenWidth - 120), (float)(Main.screenHeight - 40));
			DrawBlueBar(spriteBatch, barDrawPosition, barOffsetY);
			float yScale = 8f;
			DrawProgressText(spriteBatch, yScale, barDrawPosition, barOffsetY, out var newBarPosition);
			DrawBackground(spriteBatch, yScale, newBarPosition, barOffsetY);
			DrawProgressTextAndIcons(spriteBatch, barOffsetY);
		}
	}
}
