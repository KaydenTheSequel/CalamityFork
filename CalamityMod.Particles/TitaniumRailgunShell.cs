using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class TitaniumRailgunShell : Particle
{
	public float Opacity;

	public Point TileAttachement;

	public Color GlowColor;

	public override string Texture => "CalamityMod/Particles/TitaniumRailgunShell";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public bool isStuckToTile
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			if (Main.tile[TileAttachement].HasTile && Main.tile[TileAttachement].IsTileSolid())
			{
				return true;
			}
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					if (Main.tile[TileAttachement + new Point(i, j)].HasTile && Main.tile[TileAttachement + new Point(i, j)].IsTileSolid())
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public TitaniumRailgunShell(Vector2 position, Point tileCoords, float rotation, Color glowColor, int lifetime = 80)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = 1f;
		Color = Color.White;
		GlowColor = glowColor;
		Opacity = 1f;
		Velocity = Vector2.Zero;
		Rotation = rotation;
		Lifetime = lifetime;
		TileAttachement = tileCoords;
		if (!isStuckToTile)
		{
			Opacity = 0f;
		}
	}

	public override void Update()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		if (base.LifetimeCompletion > 0.7f)
		{
			Opacity = MathHelper.Lerp(1f, 0f, (base.LifetimeCompletion - 0.7f) / 0.3f);
		}
		Color = Lighting.GetColor(TileAttachement);
		Velocity *= 0.9f;
		if (!isStuckToTile)
		{
			Opacity = 0f;
		}
		if (Opacity <= 0.01f)
		{
			GeneralParticleHandler.RemoveParticle(this);
		}
		if (base.LifetimeCompletion < 0.2f && Main.rand.NextBool())
		{
			Dust.NewDustPerfect(Position + Main.rand.NextVector2Circular(8f, 8f), 6, Vector2.UnitY.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(-1f, -5f), 0, Color.White, 1.2f).noGravity = true;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = GeneralParticleHandler.GetTexture(Type);
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/TitaniumRailgunShellGlow", (AssetRequestMode)2).Value;
		Vector2 minorDisplacementToMakeItLookLikeItActuallyWasMovingButFast = Rotation.ToRotationVector2() * -13f * (float)Math.Pow(MathHelper.Clamp(1f - base.LifetimeCompletion * 3f, 0f, 1f), 3.0);
		spriteBatch.Draw(texture, Position + minorDisplacementToMakeItLookLikeItActuallyWasMovingButFast - Main.screenPosition, (Rectangle?)null, Color * Opacity, Rotation + (float)Math.PI / 2f, texture.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
		((Color)(ref GlowColor)).A = 4;
		float glowOpacity = 1f - Math.Clamp(base.LifetimeCompletion * 2f, 0f, 1f);
		spriteBatch.Draw(glowTexture, Position + minorDisplacementToMakeItLookLikeItActuallyWasMovingButFast - Main.screenPosition, (Rectangle?)null, GlowColor * glowOpacity, Rotation + (float)Math.PI / 2f, glowTexture.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
