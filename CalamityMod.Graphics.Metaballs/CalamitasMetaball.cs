using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class CalamitasMetaball : Metaball
{
	public class Particle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public Texture2D TextureToUse;

		public float rotation;

		public float SizeScaling;

		public Vector2 Scale;

		private bool firstFrame;

		public Particle(Vector2 center, Vector2 velocity, float size)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			SizeScaling = 0.85f;
			Scale = Vector2.One;
			firstFrame = true;
			base._002Ector();
			Center = center;
			Velocity = velocity;
			Size = size;
		}

		public void Update()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			Center += Velocity;
			Velocity *= 0.96f;
			if (firstFrame)
			{
				firstFrame = false;
			}
			else
			{
				Size *= SizeScaling;
			}
		}
	}

	public override bool IgnoreFPS => true;

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
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.Crimson, Color.Black, 0f);
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/CalamitasLayer", (AssetRequestMode)1);
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((Particle p) => p.Size <= 2f);
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.UnitX * Main.GlobalTimeWrappedHourly * 0.037f;
	}

	public override void DrawInstances()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (Particle particle in Particles)
		{
			Texture2D texture2d = particle.TextureToUse ?? tex;
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = texture2d.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / (float)texture2d.Width;
			Main.spriteBatch.Draw(texture2d, drawPosition, (Rectangle?)null, Color.White, particle.rotation, origin, particle.Scale * scale, (SpriteEffects)0, 0f);
		}
	}
}
