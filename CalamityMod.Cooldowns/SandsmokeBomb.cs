using System;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Particles;
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

public class SandsmokeBomb : CooldownHandler
{
	public bool PowerActive => instance.timeLeft > DesertProwlerHat.SmokeCooldown;

	public float PowerPercent => (float)(instance.timeLeft - DesertProwlerHat.SmokeCooldown) / (float)DesertProwlerHat.SmokeDuration;

	public new static string ID => "SandsmokeBomb";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns.SandsmokeBomb" + (PowerActive ? "Active" : "Cooldown"));

	public override string Texture => "CalamityMod/Cooldowns/SandsmokeBomb";

	public override string OutlineTexture => "CalamityMod/Cooldowns/SandsmokeBombOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/SandsmokeBombOverlay";

	public override Color OutlineColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 299, 156);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!PowerActive)
			{
				return new Color(204, 181, 72);
			}
			return Color.Lerp(new Color(204, 181, 72), new Color(169, 142, 16), PowerPercent);
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
				return new Color(169, 142, 16);
			}
			return Color.Lerp(new Color(204, 181, 72), new Color(169, 142, 16), PowerPercent);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DesertProwlerSmokeBombReload");

	private float AdjustedCompletion
	{
		get
		{
			if (!CooldownRackUI.DebugFullDisplay)
			{
				if (!PowerActive)
				{
					return 1f - (float)instance.timeLeft / (float)DesertProwlerHat.SmokeCooldown;
				}
				return PowerPercent;
			}
			return CooldownRackUI.DebugForceCompletion;
		}
	}

	public override void OnCompleted()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 playerVelocity = instance.player.velocity / 8f;
		Vector2 particleGravity = Vector2.UnitY * 0.03f;
		for (int i = 0; i < 16; i++)
		{
			Vector2 dustDisplace = Main.rand.NextVector2Circular(80f, 50f);
			Vector2 position = instance.player.MountedCenter + dustDisplace;
			Vector2 dustSpeed = Main.rand.NextVector2Circular(0.5f, 0.5f) + playerVelocity - Vector2.UnitY.RotatedByRandom(0.7853981852531433) * 0.06f;
			dustSpeed.X += 1.4f * (float)Math.Sin((dustDisplace.X + 80f) / 160f * (float)Math.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			GeneralParticleHandler.SpawnParticle(new SandyDustParticle(position, dustSpeed, Color.White, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(20, 50), 0.03f, particleGravity));
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
