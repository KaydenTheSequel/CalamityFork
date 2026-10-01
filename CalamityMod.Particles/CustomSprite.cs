using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class CustomSprite : Particle
{
	private bool addBlend;

	private bool important;

	private Texture2D Tex;

	private int frames;

	private int currentFrame;

	private int Timer;

	private float maxGravity;

	private float Opacity;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => addBlend;

	public override bool Important => important;

	public override string Texture => "CalamityMod/Particles/CuteStars";

	public override int FrameVariants => frames;

	public CustomSprite(Vector2 relativePosition, Vector2 velocity, int lifetime, string tex, float scale, Color color, float grav = 0f, bool AddativeBlend = true, bool needed = false, int frameCount = 1, int frame = 0)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		frames = 1;
		currentFrame = 1;
		Opacity = 1f;
		base._002Ector();
		maxGravity = grav;
		Position = relativePosition;
		Velocity = velocity;
		Scale = scale;
		Lifetime = lifetime;
		addBlend = AddativeBlend;
		important = needed;
		Color = color;
		Tex = ModContent.Request<Texture2D>(tex, (AssetRequestMode)2).Value;
		frames = frameCount;
		currentFrame = frame;
	}

	public CustomSprite(Vector2 relativePosition, Vector2 velocity, int lifetime, Texture2D tex, float scale, Color color, float grav = 0f, bool AddativeBlend = true, bool needed = false, int frameCount = 1, int frame = 0)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		frames = 1;
		currentFrame = 1;
		Opacity = 1f;
		base._002Ector();
		maxGravity = grav;
		Position = relativePosition;
		Velocity = velocity;
		Scale = scale;
		Lifetime = lifetime;
		addBlend = AddativeBlend;
		important = needed;
		Color = color;
		Tex = tex;
		frames = frameCount;
		currentFrame = frame;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Position += Velocity;
		Timer++;
		if (Timer > Lifetime - 20)
		{
			Scale *= 0.9f;
			Opacity *= 0.9f;
		}
		Velocity *= 0.85f;
		if (maxGravity != 0f && ((Vector2)(ref Velocity)).Length() < maxGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += maxGravity / 10f;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		Rectangle fr = Tex.Frame(1, frames, 0, currentFrame);
		Main.EntitySpriteDraw(Tex, Position - Main.screenPosition, fr, Color.Lerp(Color.Transparent, Color, Opacity), Rotation, new Vector2((float)Tex.Width * 0.5f, (float)(Tex.Height / frames) * 0.5f), 1f, (SpriteEffects)0);
	}
}
