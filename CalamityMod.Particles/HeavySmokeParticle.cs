using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class HeavySmokeParticle : Particle
{
	private float Opacity;

	private float Spin;

	private bool StrongVisual;

	private bool Glowing;

	private float HueShift;

	private static int FrameAmount = 6;

	public override bool SetLifetime => true;

	public override int FrameVariants => 7;

	public override bool UseCustomDraw => true;

	public override bool Important => StrongVisual;

	public override bool UseAdditiveBlend => Glowing;

	public override bool UseHalfTransparency => !Glowing;

	public override string Texture => "CalamityMod/Particles/HeavySmoke";

	public HeavySmokeParticle(Vector2 position, Vector2 velocity, Color color, int lifetime, float scale, float opacity, float rotationSpeed = 0f, bool glowing = false, float hueshift = 0f, bool required = false, bool affectedByLight = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Variant = Main.rand.Next(7);
		Lifetime = lifetime;
		Opacity = opacity;
		Spin = rotationSpeed;
		StrongVisual = required;
		Glowing = glowing;
		HueShift = hueshift;
		AffectedByLight = affectedByLight;
	}

	public override void Update()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if ((float)Time / (float)Lifetime < 0.2f)
		{
			Scale += 0.01f;
		}
		else
		{
			Scale *= 0.975f;
		}
		Color = Main.hslToRgb((Main.rgbToHsl(Color).X + HueShift) % 1f, Main.rgbToHsl(Color).Y, Main.rgbToHsl(Color).Z);
		Opacity *= 0.98f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Velocity *= 0.85f;
		float opacity = Utils.GetLerpValue(1f, 0.85f, base.LifetimeCompletion, clamped: true);
		Color *= opacity;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		int animationFrame = (int)Math.Floor((float)Time / ((float)Lifetime / (float)FrameAmount));
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(80 * Variant, 80 * animationFrame, 80, 80);
		Color col = Color * Opacity;
		if (AffectedByLight)
		{
			col = col.MultiplyRGBA(Lighting.GetColor((Position / 16f).ToPoint()));
		}
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)frame, col, Rotation, frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
