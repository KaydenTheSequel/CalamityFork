using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class LineVFX : Particle
{
	public float opacity;

	public Vector2 LineVector;

	public bool Concave;

	public bool Telegraph;

	public float Expansion;

	public override string Texture => "CalamityMod/Particles/ThinEndedLine";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => Telegraph;

	public LineVFX(Vector2 startPoint, Vector2 lineVector, float thickness, Color color, bool concave = false, bool telegraph = false, float expansion = 0f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = startPoint;
		LineVector = lineVector;
		Scale = thickness;
		Color = color;
		Concave = concave;
		Telegraph = telegraph;
		Expansion = expansion;
		Velocity = Vector2.Zero;
		Rotation = 0f;
		Lifetime = 2;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ((!Concave) ? ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Particles/ThickEndedLine", (AssetRequestMode)2).Value);
		Vector2 drawPosition = Position - LineVector.SafeNormalize(Vector2.Zero) * (float)Math.Sqrt(1f - (float)Math.Pow(base.LifetimeCompletion - 1f, 2.0)) * Expansion / 2f;
		Vector2 expandedLine = LineVector + LineVector.SafeNormalize(Vector2.Zero) * (float)Math.Sqrt(1f - (float)Math.Pow(base.LifetimeCompletion - 1f, 2.0)) * Expansion;
		float rot = LineVector.ToRotation() + (float)Math.PI / 2f;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Scale, ((Vector2)(ref expandedLine)).Length() / (float)tex.Height);
		spriteBatch.Draw(tex, drawPosition - Main.screenPosition, (Rectangle?)null, Color, rot, origin, scale, (SpriteEffects)0, 0f);
	}
}
