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

public abstract class CooldownHandler : ModType
{
	public CooldownInstance instance;

	internal static string DefaultChargeBarTexture = "CalamityMod/Cooldowns/BarBase";

	public static string ID => null;

	public virtual bool CanTickDown => true;

	public virtual bool PersistsThroughDeath => false;

	public virtual bool SavedWithPlayer => true;

	public virtual SoundStyle? EndSound => null;

	public virtual bool ShouldPlayEndSound => true;

	public virtual LocalizedText DisplayName => LocalizedText.Empty;

	public virtual bool ShouldDisplay => true;

	public virtual string Texture => "";

	public virtual string OverlayTexture => Texture + "Overlay";

	public virtual string OutlineTexture => Texture + "Outline";

	public virtual string ChargeBarTexture => DefaultChargeBarTexture;

	public virtual string ChargeBarBackTexture => DefaultChargeBarTexture;

	public virtual Color OutlineColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public virtual Color CooldownStartColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Gray;
		}
	}

	public virtual Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	protected sealed override void Register()
	{
		ModTypeLookup<CooldownHandler>.Register(this);
	}

	public virtual void Tick()
	{
	}

	public virtual void OnCompleted()
	{
	}

	public virtual void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D outline = ModContent.Request<Texture2D>(OutlineTexture, (AssetRequestMode)2).Value;
		Texture2D barBase = ModContent.Request<Texture2D>(ChargeBarTexture, (AssetRequestMode)2).Value;
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		ApplyBarShaders(opacity);
		spriteBatch.Draw(barBase, position, (Rectangle?)null, Color.White * opacity, 0f, barBase.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(outline, position, (Rectangle?)null, OutlineColor * opacity, 0f, outline.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sprite, position, (Rectangle?)null, Color.White * opacity, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}

	public virtual void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D outline = ModContent.Request<Texture2D>(OutlineTexture, (AssetRequestMode)2).Value;
		Texture2D overlay = ModContent.Request<Texture2D>(OverlayTexture, (AssetRequestMode)2).Value;
		Color outlineColor = OutlineColor;
		spriteBatch.Draw(outline, position, (Rectangle?)null, outlineColor * opacity, 0f, outline.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sprite, position, (Rectangle?)null, Color.White * opacity, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		int lostHeight = (int)Math.Ceiling((float)overlay.Height * (1f - instance.Completion));
		Rectangle crop = default(Rectangle);
		((Rectangle)(ref crop))._002Ector(0, lostHeight, overlay.Width, overlay.Height - lostHeight);
		spriteBatch.Draw(overlay, position + Vector2.UnitY * (float)lostHeight * scale, (Rectangle?)crop, outlineColor * opacity * 0.9f, 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}

	public virtual void ApplyBarShaders(float opacity)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (ChargeBarTexture == DefaultChargeBarTexture)
		{
			GameShaders.Misc["CalamityMod:CircularBarShader"].UseOpacity(opacity);
			GameShaders.Misc["CalamityMod:CircularBarShader"].UseSaturation(1f - instance.Completion);
			GameShaders.Misc["CalamityMod:CircularBarShader"].UseColor(CooldownStartColor);
			GameShaders.Misc["CalamityMod:CircularBarShader"].UseSecondaryColor(CooldownEndColor);
			GameShaders.Misc["CalamityMod:CircularBarShader"].Apply();
		}
		else
		{
			GameShaders.Misc["CalamityMod:CircularBarSpriteShader"].SetShaderTexture(ModContent.Request<Texture2D>(ChargeBarBackTexture, (AssetRequestMode)2));
			GameShaders.Misc["CalamityMod:CircularBarSpriteShader"].UseOpacity(opacity);
			GameShaders.Misc["CalamityMod:CircularBarSpriteShader"].UseSaturation(1f - instance.Completion);
			GameShaders.Misc["CalamityMod:CircularBarSpriteShader"].Apply();
		}
	}
}
