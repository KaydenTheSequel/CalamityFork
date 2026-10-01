using System.Collections.Generic;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace CalamityMod.Particles;

public class BrainOfCthulhuAfterImage : Particle
{
	private float StartFade;

	private float Opacity;

	private List<Vector2> Path;

	private Vector2 MyScale;

	private Rectangle Frame;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool UseCustomDraw => false;

	public override bool SetLifetime => true;

	public override bool Important => true;

	public BrainOfCthulhuAfterImage(BezierCurve path, float rotation, Vector2 scale, int lifeTime, Rectangle frame, float startFadeRatio = 0f)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Opacity = 1f;
		MyScale = Vector2.One;
		base._002Ector();
		Path = path.GetPoints(lifeTime);
		Position = Path[0];
		Rotation = rotation;
		StartFade = startFadeRatio;
		MyScale = scale;
		Frame = frame;
		Lifetime = lifeTime + 1;
	}

	public override void Update()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		float timeRatio = (Opacity = (float)Time / (float)Lifetime);
		if (StartFade != 0f)
		{
			Opacity = Utils.GetLerpValue(StartFade, 1f, timeRatio, clamped: true);
		}
		Opacity = 1f - CalamityUtils.SineInEasing(Opacity, 1);
		List<Vector2> pathPosition = Path;
		if (Time >= pathPosition.Count)
		{
			Kill();
		}
		else
		{
			Position = pathPosition[Time];
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		_ = (float)Time / (float)Lifetime;
		spriteBatch.Draw(TextureAssets.Npc[266].Value, Position - Main.screenPosition, (Rectangle?)Frame, Lighting.GetColor(Position.ToTileCoordinates()) * Opacity * 0.5f, Rotation, Frame.Size() * 0.5f, MyScale, (SpriteEffects)0, 0f);
	}
}
