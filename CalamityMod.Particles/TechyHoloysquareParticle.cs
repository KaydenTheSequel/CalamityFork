using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class TechyHoloysquareParticle : Particle
{
	internal Rectangle Frame;

	public float Opacity;

	public float OpacityMult;

	public override bool SetLifetime => true;

	public override string Texture => "CalamityMod/Particles/TechyHolosquare";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public TechyHoloysquareParticle(Vector2 position, Vector2 speed, float scale, Color color, int lifetime, float opacity = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = scale;
		Color = color;
		Velocity = speed;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Opacity = opacity;
		OpacityMult = opacity;
		Variant = Main.rand.Next(6);
		Lifetime = lifetime;
		switch (Variant)
		{
		case 0:
			Frame = new Rectangle(8, 0, 6, 6);
			break;
		case 1:
			Frame = new Rectangle(6, 8, 10, 6);
			break;
		case 2:
			Frame = new Rectangle(4, 16, 14, 8);
			break;
		case 3:
			Frame = new Rectangle(2, 26, 18, 10);
			break;
		case 4:
			Frame = new Rectangle(2, 38, 18, 8);
			break;
		case 5:
			Frame = new Rectangle(6, 48, 12, 12);
			break;
		}
	}

	public override void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Opacity = (float)Math.Pow(base.LifetimeCompletion, 0.5) * OpacityMult;
		Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * Opacity);
		Rotation = Velocity.ToRotation();
		Velocity *= 0.875f;
		Scale *= 0.96f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D baseTex = GeneralParticleHandler.GetTexture(Type);
		CalamityUtils.DrawChromaticAberration(Vector2.UnitX.RotatedBy(Rotation), 1.5f, delegate(Vector2 offset, Color colorMod)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			spriteBatch.Draw(baseTex, Position + offset - Main.screenPosition, (Rectangle?)Frame, Color.MultiplyRGB(colorMod) * Opacity, Rotation, Frame.Size() / 2f, Scale / 2f, (SpriteEffects)0, 0f);
		});
	}
}
