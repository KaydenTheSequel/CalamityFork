using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class ProfanedSoulShield : CooldownHandler
{
	private CalamityPlayer CalPlayer => instance.player.Calamity();

	private int CorrectMaxDurability
	{
		get
		{
			if (!CalPlayer.profanedCrystalBuffs)
			{
				return ProfanedSoulArtifact.ShieldDurabilityMax;
			}
			return ProfanedSoulCrystal.ShieldDurabilityMax;
		}
	}

	private float AdjustedCompletion => (float)instance.timeLeft / (float)CorrectMaxDurability;

	public new static string ID => "ProfanedSoulShieldDurability";

	public override bool CanTickDown
	{
		get
		{
			if (CalPlayer.pSoulArtifact)
			{
				return instance.timeLeft <= 0;
			}
			return true;
		}
	}

	public override bool ShouldDisplay
	{
		get
		{
			if (!CalPlayer.pSoulArtifact || CalPlayer.profanedCrystal)
			{
				return CalPlayer.profanedCrystalBuffs;
			}
			return true;
		}
	}

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/ProfanedSoulShieldActive";

	public override string OutlineTexture => "CalamityMod/Cooldowns/ProfanedSoulShieldOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/ProfanedSoulShieldOverlay";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 191, 73);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(GetColor(start: true), GetColor(start: false), instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(GetColor(start: true), GetColor(start: false), instance.Completion);
		}
	}

	public override bool SavedWithPlayer => false;

	public override bool PersistsThroughDeath => false;

	private Color GetColor(bool start)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (start)
		{
			return CalPlayer.profanedCrystalBuffs ? new Color(247, 172, 131) : new Color(235, 178, 96);
		}
		return CalPlayer.profanedCrystalBuffs ? new Color(255, 194, 161) : new Color(219, 179, 121);
	}

	public override void ApplyBarShaders(float opacity)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:CircularBarShader"].UseOpacity(opacity);
		GameShaders.Misc["CalamityMod:CircularBarShader"].UseSaturation(AdjustedCompletion);
		GameShaders.Misc["CalamityMod:CircularBarShader"].UseColor(CooldownStartColor);
		GameShaders.Misc["CalamityMod:CircularBarShader"].UseSecondaryColor(CooldownEndColor);
		GameShaders.Misc["CalamityMod:CircularBarShader"].Apply();
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		float Xoffset = ((instance.timeLeft > 9) ? (-10f) : (-5f));
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, instance.timeLeft.ToString(), position + new Vector2(Xoffset, 4f) * scale, Color.Lerp(GetColor(start: true), Color.OrangeRed, 1f - instance.Completion), Color.Black, scale);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D outline = ModContent.Request<Texture2D>(OutlineTexture, (AssetRequestMode)2).Value;
		Texture2D overlay = ModContent.Request<Texture2D>(OverlayTexture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(outline, position, (Rectangle?)null, OutlineColor * opacity, 0f, outline.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sprite, position, (Rectangle?)null, Color.White * opacity, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		int lostHeight = (int)Math.Ceiling((float)overlay.Height * AdjustedCompletion);
		Rectangle crop = default(Rectangle);
		((Rectangle)(ref crop))._002Ector(0, lostHeight, overlay.Width, overlay.Height - lostHeight);
		spriteBatch.Draw(overlay, position + Vector2.UnitY * (float)lostHeight * scale, (Rectangle?)crop, OutlineColor * opacity * 0.9f, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		float Xoffset = ((instance.timeLeft > 9) ? (-10f) : (-5f));
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, instance.timeLeft.ToString(), position + new Vector2(Xoffset, 4f) * scale, Color.Lerp(GetColor(start: true), Color.OrangeRed, 1f - instance.Completion), Color.Black, scale);
	}
}
