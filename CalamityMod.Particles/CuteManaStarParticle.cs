using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class CuteManaStarParticle : Particle
{
	public float Opacity;

	public override string Texture => "CalamityMod/Particles/CuteStars";

	public override bool UseCustomDraw => false;

	public override bool SetLifetime => true;

	public override int FrameVariants => 2;

	public CuteManaStarParticle(Vector2 position, Vector2 speed, float scale = 1f, float opacity = 1f, int lifetime = 20)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = scale;
		Color = Color.White;
		Opacity = opacity;
		Velocity = speed;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Lifetime = lifetime;
		Variant = Main.rand.Next(2);
	}

	public override void Update()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Opacity *= 0.96f;
		Color = Color.White * Opacity;
		Vector2 position = Position;
		Color dodgerBlue = Color.DodgerBlue;
		Lighting.AddLight(position, ((Color)(ref dodgerBlue)).ToVector3());
		Velocity *= 0.9f;
		if (((Vector2)(ref Velocity)).Length() <= 0.01f)
		{
			GeneralParticleHandler.RemoveParticle(this);
		}
	}
}
