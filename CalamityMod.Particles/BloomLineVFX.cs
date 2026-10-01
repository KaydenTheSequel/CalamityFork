using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class BloomLineVFX : Particle
{
	public bool Capped;

	public float opacity;

	public Vector2 LineVector;

	public bool Telegraph;

	public override string Texture => "CalamityMod/Particles/BloomLine";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => Telegraph;

	public BloomLineVFX(Vector2 startPoint, Vector2 lineVector, float thickness, Color color, int lifetime, bool capped = false, bool telegraph = false)
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
		Lifetime = lifetime;
		Capped = capped;
		Telegraph = telegraph;
		Velocity = Vector2.Zero;
		Rotation = 0f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float rot = LineVector.ToRotation() + (float)Math.PI / 2f;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Scale, ((Vector2)(ref LineVector)).Length() / (float)tex.Height);
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color, rot, origin, scale, (SpriteEffects)0, 0f);
		if (Capped)
		{
			Texture2D cap = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineCap", (AssetRequestMode)2).Value;
			((Vector2)(ref scale))._002Ector(Scale, Scale);
			((Vector2)(ref origin))._002Ector((float)cap.Width / 2f, (float)cap.Height);
			spriteBatch.Draw(cap, Position - Main.screenPosition, (Rectangle?)null, Color, rot + (float)Math.PI, origin, scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(cap, Position + LineVector - Main.screenPosition, (Rectangle?)null, Color, rot, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
