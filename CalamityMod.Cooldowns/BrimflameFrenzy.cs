using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class BrimflameFrenzy : CooldownHandler
{
	public new static string ID => "BrimflameFrenzy";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/BrimflameFrenzy";

	public override Color OutlineColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(211, 124, 93);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(107, 6, 6), new Color(228, 78, 78), 1f - instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(107, 6, 6), new Color(228, 78, 78), 1f - instance.Completion);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/BrimflameRecharge");

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		Texture2D sprite = ModContent.Request<Texture2D>(Texture + "Eyes", (AssetRequestMode)2).Value;
		spriteBatch.Draw(sprite, position, (Rectangle?)null, Color.Crimson * opacity * (1f - instance.Completion), 0f, sprite.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
