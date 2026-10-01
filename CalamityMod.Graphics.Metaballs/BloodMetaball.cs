using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class BloodMetaball : Metaball
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
			ShrinkDelay = 45;
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
			if (ShrinkDelay < TimeAlive)
			{
				Size *= SizeScaling;
			}
		}
	}

	public override bool FixedToScreen => false;

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
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			if (ChildSafety.Disabled)
			{
				return new Color(67, 17, 17);
			}
			return Color.CornflowerBlue;
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/BloodLayer", (AssetRequestMode)1);
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
		return Vector2.UnitX * Main.GlobalTimeWrappedHourly * 0.0005f;
	}

	public override void DrawInstances()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (Particle particle in Particles)
		{
			Texture2D texture2d = particle.TextureToUse ?? tex;
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 scale = particle.Scale * particle.Size / (float)texture2d.Width;
			Rectangle frame = texture2d.Frame(1, particle.MaxFrames, 0, particle.CurrentFrame);
			Vector2 origin = frame.Size() * 0.5f;
			Main.spriteBatch.Draw(texture2d, drawPosition, (Rectangle?)frame, (!ChildSafety.Disabled) ? Color.Black : Color.White, particle.rotation, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
