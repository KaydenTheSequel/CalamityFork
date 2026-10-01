using System;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class WulfrumBastion : CooldownHandler
{
	public bool PowerActive => instance.timeLeft > WulfrumHat.BastionCooldown;

	public float DurabilityPercent => (float)(instance.timeLeft - WulfrumHat.BastionCooldown) / (float)WulfrumHat.BastionTime;

	public new static string ID => "WulfrumBastion";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns.WulfrumBastion" + (PowerActive ? "Active" : "Cooldown"));

	public override string Texture
	{
		get
		{
			if (!PowerActive)
			{
				return "CalamityMod/Cooldowns/WulfrumBastion";
			}
			return "CalamityMod/Cooldowns/WulfrumBastionActive";
		}
	}

	public override string OutlineTexture => "CalamityMod/Cooldowns/WulfrumBastionOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/WulfrumBastionOverlay";

	public override Color OutlineColor
	{
		get
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			if (!PowerActive)
			{
				return new Color(206, 201, 170);
			}
			return new Color(194, 255, 67);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			if (!PowerActive)
			{
				return new Color(92, 187, 99);
			}
			return Color.Lerp(new Color(112, 244, 244), new Color(54, 177, 221), DurabilityPercent);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!PowerActive)
			{
				return new Color(160, 232, 77);
			}
			return Color.Lerp(new Color(112, 244, 244), new Color(54, 177, 221), DurabilityPercent);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionRecharge");

	private float AdjustedCompletion
	{
		get
		{
			if (!CooldownRackUI.DebugFullDisplay)
			{
				if (!PowerActive)
				{
					return 1f - (float)instance.timeLeft / (float)WulfrumHat.BastionCooldown;
				}
				return (float)(instance.timeLeft - WulfrumHat.BastionCooldown) / (float)WulfrumHat.BastionTime;
			}
			return CooldownRackUI.DebugForceCompletion;
		}
	}

	public override void OnCompleted()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Vector2 dustDirection = Main.rand.NextVector2CircularEdge(1f, 1f);
			Dust dust = Dust.NewDustPerfect(instance.player.Center + dustDirection * Main.rand.NextFloat(0.4f, 10f), 226, dustDirection * Main.rand.NextFloat(1f, 4f), 100, Color.Transparent, Main.rand.NextFloat(0.8f, 1.2f));
			dust.noGravity = true;
			dust.noLight = true;
			dust.fadeIn = 1f;
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
