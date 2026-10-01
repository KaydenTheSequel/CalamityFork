using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class PlayerCenteredPulseRing : Particle
{
	public Player player;

	private float OriginalScale;

	private float FinalScale;

	private float opacity;

	private Vector2 Squish;

	private Color BaseColor;

	public override string Texture => "CalamityMod/Particles/HollowCircleHardEdge";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public PlayerCenteredPulseRing(Player p, Vector2 velocity, Color color, Vector2 squish, float rotation, float originalScale, float finalScale, int lifeTime)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		player = Main.LocalPlayer;
		base._002Ector();
		player = p;
		Position = p.MountedCenter;
		Velocity = velocity;
		BaseColor = color;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		Scale = originalScale;
		Lifetime = lifeTime;
		Squish = squish;
		Rotation = rotation;
	}

	public override void Update()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress);
		opacity = (float)Math.Sin((float)Math.PI / 2f + base.LifetimeCompletion * ((float)Math.PI / 2f));
		Color = BaseColor * opacity;
		Lighting.AddLight(Position, (float)(int)((Color)(ref Color)).R / 255f, (float)(int)((Color)(ref Color)).G / 255f, (float)(int)((Color)(ref Color)).B / 255f);
		Velocity *= 0.95f;
		Position = player.MountedCenter;
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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, tex.Size() / 2f, Scale * Squish, (SpriteEffects)0, 0f);
	}
}
