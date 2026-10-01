using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class GruesomeMetaball : Metaball
{
	public class GruesomeParticle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public GruesomeParticle(Vector2 center, Vector2 velocity, float size)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Center = center;
			Velocity = velocity;
			Size = size;
		}

		public void Update()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			Size *= 0.94f;
			Center += Velocity;
			Velocity *= 0.96f;
		}
	}

	private static List<Asset<Texture2D>> layerAssets;

	public static List<GruesomeParticle> Particles { get; private set; } = new List<GruesomeParticle>();

	public override bool AnythingToDraw
	{
		get
		{
			if (!Particles.Any())
			{
				return CalamityUtils.AnyProjectiles(ModContent.ProjectileType<SpiritCongregation>());
			}
			return true;
		}
	}

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			for (int i = 0; i < layerAssets.Count; i++)
			{
				yield return layerAssets[i].Value;
			}
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.AfterProjectiles;

	public override Color EdgeColor
	{
		get
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			return new Color(61, 6, 2);
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			layerAssets = new List<Asset<Texture2D>>();
			for (int i = 1; i <= 5; i++)
			{
				layerAssets.Add(ModContent.Request<Texture2D>($"CalamityMod/Graphics/Metaballs/GruesomeEminence_Ghost_Layer{i}", (AssetRequestMode)1));
			}
		}
	}

	public override void ClearInstances()
	{
		Particles.Clear();
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((GruesomeParticle p) => p.Size <= 2.5f);
	}

	public static void SpawnParticle(Vector2 position, Vector2 velocity, float size)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new GruesomeParticle(position, velocity, size));
	}

	public override Vector2 CalculateManualOffsetForLayer(int layerIndex)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		switch (layerIndex)
		{
		case 0:
			return Vector2.UnitX * Main.GlobalTimeWrappedHourly * 0.03f;
		case 1:
		{
			Vector2 offset = Vector2.One * (float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.041f) * 2f;
			return offset.RotatedBy((float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.08f) * 0.97f);
		}
		case 2:
		{
			Vector2 offset = (Main.GlobalTimeWrappedHourly * 2.02f).ToRotationVector2() * 0.036f;
			offset.Y += (float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.161f) * 0.5f + 0.5f;
			return offset;
		}
		case 3:
		{
			Vector2 offset = Vector2.UnitX * Main.GlobalTimeWrappedHourly * -0.04f + (Main.GlobalTimeWrappedHourly * 1.89f).ToRotationVector2() * 0.03f;
			offset.Y += CalamityUtils.PerlinNoise2D(Main.GlobalTimeWrappedHourly * 0.187f, Main.GlobalTimeWrappedHourly * 0.193f, 2, 466920161) * 0.025f;
			return offset;
		}
		case 4:
		{
			Vector2 offset = Vector2.UnitX * Main.GlobalTimeWrappedHourly * 0.037f + (Main.GlobalTimeWrappedHourly * 1.77f).ToRotationVector2() * 0.04725f;
			offset.Y += CalamityUtils.PerlinNoise2D(Main.GlobalTimeWrappedHourly * 0.187f, Main.GlobalTimeWrappedHourly * 0.193f, 2, 577215664) * 0.05f;
			return offset;
		}
		default:
			return Vector2.Zero;
		}
	}

	public override void DrawInstances()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (GruesomeParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
		int eminenceType = ModContent.ProjectileType<SpiritCongregation>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile p = enumerator2.Current;
			if (p.type == eminenceType)
			{
				p.ModProjectile<SpiritCongregation>().DrawHeadForMetaball();
			}
		}
	}
}
