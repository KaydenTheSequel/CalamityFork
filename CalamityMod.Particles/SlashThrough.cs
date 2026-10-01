using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SlashThrough : Particle
{
	private Vector2 OriginalPosition;

	private Vector2 CurrentScale;

	private Color BaseColor;

	public NPC StuckTo;

	public override string Texture => "CalamityMod/ExtraTextures/Trails/SwordSlashTexture";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public SlashThrough(Color color, Vector2 originalPosition, float rotation, int lifeTime, NPC stuckTo)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		BaseColor = color;
		OriginalPosition = originalPosition;
		Position = originalPosition;
		Rotation = rotation;
		Lifetime = lifeTime;
		StuckTo = stuckTo;
	}

	public override void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI) * 0.8f + 0.2f;
		Color = BaseColor * opacity;
		CurrentScale = new Vector2(128f, 64f);
		if (StuckTo != null && StuckTo.active && StuckTo.knockBackResist > 0f)
		{
			float distance = Vector2.Distance(OriginalPosition, StuckTo.Center);
			Position = Vector2.Lerp(OriginalPosition, StuckTo.Center, StuckTo.knockBackResist);
			CurrentScale.X += distance;
			if (distance > 4f)
			{
				Rotation = StuckTo.SafeDirectionTo(OriginalPosition).ToRotation() + (float)Math.PI;
			}
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, tex.Size() / 2f, CurrentScale / 256f, (SpriteEffects)0, 0f);
	}
}
