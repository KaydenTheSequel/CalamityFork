using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class BigRipMetaball : Metaball
{
	public class Particle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public Texture2D TextureToUse;

		public float rotation;

		public Vector2 Scale;

		public float SizeScaling;

		public int CurrentFrame;

		public int MaxFrames;

		public int ShrinkDelay;

		public int TimeAlive;

		public Particle(Vector2 center, Vector2 velocity, float size)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Scale = Vector2.One;
			SizeScaling = 0.85f;
			MaxFrames = 1;
			ShrinkDelay = 15;
			base._002Ector();
			Center = center;
			Velocity = velocity;
			Size = size;
		}

		public void Update()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			TimeAlive++;
			Center += Velocity;
			Velocity *= 0.96f;
			if (ShrinkDelay < TimeAlive || (TimeAlive > 1 && SizeScaling <= 0f))
			{
				Scale.X *= SizeScaling;
			}
		}
	}

	public override bool FixedToScreen => true;

	public static List<Particle> Particles { get; private set; } = new List<Particle>();

	public override bool AnythingToDraw => Particles.Any();

	public static Asset<Texture2D> LayerAsset { get; private set; }

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return LayerAsset.Value;
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.BeforeProjectiles;

	public override Color EdgeColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			return new Color(239, 111, 85) * 0.5f;
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/BigRipLayer", (AssetRequestMode)1);
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((Particle p) => p.Scale.X <= 0.05f);
	}

	public static Particle SpawnParticle(Vector2 position, Vector2 velocity, float size)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Particle particle = new Particle(position, velocity, size);
		Particles.Add(particle);
		return particle;
	}

	public override Vector2 CalculateManualOffsetForLayer(int layerIndex)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Zero;
	}

	public override void DrawInstances()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (Particle particle in Particles)
		{
			Texture2D texture2d = particle.TextureToUse ?? tex;
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 modScale = particle.Scale;
			if (particle.TimeAlive < 5)
			{
				modScale.X *= 0.25f + 0.75f * (float)particle.TimeAlive / 5f;
			}
			Vector2 scale = modScale * particle.Size / (float)texture2d.Height;
			Rectangle frame = texture2d.Frame(1, particle.MaxFrames, 0, particle.CurrentFrame);
			Vector2 origin = frame.Size() * 0.5f;
			Main.spriteBatch.Draw(texture2d, drawPosition, (Rectangle?)frame, Color.White, particle.rotation, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
