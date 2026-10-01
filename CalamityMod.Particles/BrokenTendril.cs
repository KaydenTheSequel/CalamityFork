using System;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace CalamityMod.Particles;

public class BrokenTendril : Particle
{
	private int TimeLeft;

	private float Opacity;

	private Vector2 InitalScale;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool UseCustomDraw => true;

	public BrokenTendril(Vector2 position, Vector2 velocity, float rotation, Vector2 scale, int lifeTime)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Opacity = 1f;
		InitalScale = Vector2.One;
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Scale = 1f;
		Rotation = rotation;
		TimeLeft = lifeTime;
		InitalScale = scale - Vector2.One;
	}

	public override void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		if (InitalScale != Vector2.Zero)
		{
			InitalScale *= 0.98f;
			if (InitalScale.X < 0.05f)
			{
				InitalScale.X = 0f;
			}
			if (InitalScale.Y < 0.05f)
			{
				InitalScale.Y = 0f;
			}
		}
		Point tilePos = Position.ToTileCoordinates();
		if (!WorldGen.InWorld(tilePos.X, tilePos.Y))
		{
			Kill();
			return;
		}
		if (Main.tile[tilePos].IsTileSolid() || TileID.Sets.Platforms[Main.tile[tilePos].TileType])
		{
			Velocity.Y = 0f;
			if (Velocity.X > 0.05f)
			{
				Velocity.X *= 0.9f;
			}
			else
			{
				Velocity.X = 0f;
			}
		}
		else
		{
			Velocity.Y += 0.25f;
			Velocity.X *= 0.975f;
		}
		Rotation += Velocity.X * 0.025f;
		if (!(Velocity == Vector2.Zero))
		{
			return;
		}
		if (TimeLeft < 30)
		{
			Opacity = (float)TimeLeft / 30f;
			if (TimeLeft <= 0)
			{
				Kill();
			}
		}
		TimeLeft--;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = BrainOfCthulhuSystem.tendril.Value;
		float rot = Rotation - (float)Math.PI / 2f;
		Vector2 center = Position + rot.ToRotationVector2() * tex.Size().Y * InitalScale.Y * 0.5f;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Lighting.GetColor(center.ToTileCoordinates()) * Opacity, rot, tex.Size() * Vector2.UnitX * 0.5f, Vector2.One + InitalScale, (SpriteEffects)0, 0f);
	}
}
