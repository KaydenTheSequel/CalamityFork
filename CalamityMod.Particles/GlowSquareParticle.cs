using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class GlowSquareParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public float fadeOut;

	public bool glowCenter;

	public float RotationUse;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Particles/GlowSquareParticle";

	public GlowSquareParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, bool GlowCenter = true, float rotation = 0f)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		fadeOut = 1f;
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		AffectedByGravity = affectedByGravity;
		Scale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		glowCenter = GlowCenter;
		RotationUse = rotation;
	}

	public override void Update()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		fadeOut -= 0.1f;
		Scale *= 0.93f;
		Color = Color.Lerp(InitialColor, InitialColor * 0.2f, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		Velocity *= 0.95f;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		if (RotationUse != 0f)
		{
			Rotation += RotationUse;
			RotationUse *= 0.992f;
		}
		else
		{
			Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = new Vector2(1f, 1f) * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		if (glowCenter)
		{
			spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.White * fadeOut * 0.7f, Rotation, texture.Size() * 0.5f, scale * new Vector2(0.9f, 0.9f), (SpriteEffects)0, 0f);
		}
	}
}
