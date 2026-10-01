using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class BossRoar : Particle
{
	private float OriginalScale;

	private float FinalScale;

	private float BaseOpacity;

	private float opacity;

	private Color BaseColor;

	public override string Texture => "CalamityMod/Particles/RoarPulse";

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public BossRoar(Vector2 position, Color color, float rotation, float originalScale, float finalScale, int lifeTime, float baseOpacity = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		BaseColor = color;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		Scale = originalScale;
		Lifetime = lifeTime;
		BaseOpacity = baseOpacity;
		Rotation = rotation;
	}

	public override void Update()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, base.LifetimeCompletion);
		opacity = 1f;
		if (base.LifetimeCompletion < 0.1f)
		{
			opacity = MathHelper.Lerp(0f, 1f, base.LifetimeCompletion * 10f);
		}
		Color = BaseColor * opacity;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * BaseOpacity, Rotation, tex.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
