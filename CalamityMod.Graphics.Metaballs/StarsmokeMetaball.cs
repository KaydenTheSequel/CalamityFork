using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class StarsmokeMetaball : Metaball
{
	public class StarsmokeParticle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public Vector2 BaseSquash;

		public float Rotation;

		public float ShrinkSpeed;

		public int Lifetime;

		public float SquashPower;

		public int time;

		public StarsmokeParticle(Vector2 center, Vector2 velocity, float size, int lifetime, Vector2 squash, float shrinkSpeed = 0f, float velocitySquash = 0f)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			BaseSquash = Vector2.One;
			base._002Ector();
			Center = center;
			Velocity = velocity;
			Size = size;
			Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
			BaseSquash = squash;
			Lifetime = lifetime;
			ShrinkSpeed = shrinkSpeed;
			SquashPower = velocitySquash;
		}

		public void Update()
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			float sine = (float)Math.Sin((float)time * 0.15f * SquashPower) * 0.8f;
			Center += Velocity;
			Velocity *= 0.96f;
			Velocity += Velocity.RotatedBy(0.36f * sine * SquashPower * (float)Math.Sign(ShrinkSpeed)) * 0.02f;
			if ((float)time / (float)Lifetime > 0.8f)
			{
				Size *= 0.9f;
			}
			BaseSquash.X *= 1f - 0.2f * ShrinkSpeed;
			BaseSquash.Y *= 1f + 0.2f * ShrinkSpeed;
			Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
			time++;
		}
	}

	public Color borderColor;

	public Vector2 goalDir;

	public Vector2 scrollDir;

	public static Asset<Texture2D> LayerAsset { get; private set; }

	public static List<StarsmokeParticle> Particles { get; private set; } = new List<StarsmokeParticle>();

	public override bool AnythingToDraw => Particles.Any();

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return LayerAsset.Value;
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.BeforeNPCs;

	public override Color EdgeColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			Color val = borderColor;
			((Color)(ref val)).A = 0;
			return val * 0.3f;
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSquareParticleBig", (AssetRequestMode)1);
		}
	}

	public override void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if (goalDir == Vector2.Zero)
		{
			goalDir = Vector2.One.RotatedByRandom(Math.PI);
		}
		if ((int)(Main.GlobalTimeWrappedHourly * 50f) % 5 == 0)
		{
			goalDir = goalDir.RotatedByRandom(0.12999999523162842);
		}
		scrollDir = Vector2.Lerp(scrollDir, goalDir, 0.01f);
		float rate = Main.GlobalTimeWrappedHourly * 19f;
		List<Color> colors = new List<Color>
		{
			new Color(192, 10, 111),
			Color.Coral,
			Color.DarkOrange
		};
		int colorIndex = (int)(rate / 2f % (float)colors.Count);
		Color currentColor = colors[colorIndex];
		Color nextColor = colors[(colorIndex + 1) % colors.Count];
		borderColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((StarsmokeParticle p) => p.Size <= 2f);
	}

	public static void SpawnParticle(Vector2 position, Vector2 velocity, float size, int lifetime, Vector2 squash, float shrinkSpeed = 0f, float velocitySquash = 0f)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new StarsmokeParticle(position, velocity, size, lifetime, squash, shrinkSpeed, velocitySquash));
	}

	public override Vector2 CalculateManualOffsetForLayer(int layerIndex)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return scrollDir * 0.5f;
	}

	public override void DrawInstances()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/StarsmokeMetaball2", (AssetRequestMode)2).Value;
		foreach (StarsmokeParticle particle in Particles)
		{
			float sine = (float)Math.Sin((float)particle.time * 0.2f) * 0.5f;
			float lifetimeVal = 1 - particle.time / particle.Lifetime;
			new Vector2(1f + 1f * (1f + sine) * lifetimeVal, 1f + 1f * (1f - sine) * lifetimeVal);
			Vector2 squash = Vector2.Lerp(particle.BaseSquash, new Vector2(Utils.Remap(((Vector2)(ref particle.Velocity)).Length(), 2f, 7f, 1f, 0.5f), Utils.Remap(((Vector2)(ref particle.Velocity)).Length(), 2f, 7f, 1f, 2.5f)), particle.SquashPower);
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White, particle.Rotation, origin, squash * scale, (SpriteEffects)0, 0f);
		}
	}

	public StarsmokeMetaball()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		borderColor = Color.Purple;
		goalDir = Vector2.Zero;
		scrollDir = Vector2.Zero;
		base._002Ector();
	}
}
