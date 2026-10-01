using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class PlagueBlackout : CooldownHandler
{
	public new static string ID => "PlagueBlackout";

	public override bool ShouldDisplay => instance.timeLeft <= 1500;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/PlagueBlackout";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(174, 237, 122);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.DarkSlateGray;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.DarkSlateGray;
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/PlagueReaperRecharge");

	private float AdjustedCompletion
	{
		get
		{
			if (instance.timeLeft <= 1500)
			{
				return 1f - (float)instance.timeLeft / 1500f;
			}
			return 0f;
		}
	}

	public override void OnCompleted()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pos = instance.player.position;
		int w = instance.player.width;
		int h = instance.player.height;
		for (int i = 0; i < 66; i++)
		{
			Dust dust = Dust.NewDustDirect(pos, w, h, 89, 0f, 0f, 100, default(Color), 1.5f);
			dust.noGravity = true;
			dust.velocity *= 6.6f;
		}
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
		Texture2D sprite = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D outline = ModContent.Request<Texture2D>(OutlineTexture, (AssetRequestMode)2).Value;
		Texture2D overlay = ModContent.Request<Texture2D>(OverlayTexture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(outline, position, (Rectangle?)null, OutlineColor * opacity, 0f, outline.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sprite, position, (Rectangle?)null, Color.White * opacity, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		int lostHeight = (int)Math.Ceiling((float)overlay.Height * AdjustedCompletion);
		Rectangle crop = default(Rectangle);
		((Rectangle)(ref crop))._002Ector(0, lostHeight, overlay.Width, overlay.Height - lostHeight);
		spriteBatch.Draw(overlay, position + Vector2.UnitY * (float)lostHeight * scale, (Rectangle?)crop, OutlineColor * opacity * 0.9f, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
