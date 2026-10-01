using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CalamityMod.Enums;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class ScalArenaMetaball : Metaball
{
	public class Particle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public Texture2D TextureToUse;

		public float rotation;

		public float SizeScaling;

		public int CurrentFrame;

		public int MaxFrames;

		public Vector2 Scale;

		public int Age;

		public Particle(Vector2 center, Vector2 velocity, float size)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			SizeScaling = 0.85f;
			MaxFrames = 1;
			Scale = Vector2.One;
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
			Age++;
			Center += Velocity;
			Velocity *= 0.96f;
			if (Age > 1)
			{
				Size *= SizeScaling;
			}
		}
	}

	private static Asset<Texture2D> MainLayer;

	private static Texture2D WavyLineLayer;

	private bool hasRunTextureCorrection;

	public override bool IgnoreFPS => true;

	public static List<Particle> Particles { get; private set; } = new List<Particle>();

	public override bool AnythingToDraw => Particles.Any();

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return MainLayer.Value;
			yield return WavyLineLayer;
		}
	}

	public override List<Vector4> LayerColors
	{
		get
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			int num = 2;
			List<Vector4> list = new List<Vector4>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<Vector4> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			ref Vector4 reference = ref span[num2];
			Color val = Color.White;
			reference = ((Color)(ref val)).ToVector4();
			num2++;
			ref Vector4 reference2 = ref span[num2];
			val = EdgeColor;
			reference2 = ((Color)(ref val)).ToVector4();
			return list;
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.BeforeNPCs;

	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return SupremeCalamitas.CurrentColor;
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			MainLayer = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/ScalArenaLayerSmoke", (AssetRequestMode)1);
			WavyLineLayer = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/ScalArenaLayerWave", (AssetRequestMode)1).Value;
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((Particle p) => p.Size <= 2f && p.Age > 1);
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
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		return (Vector2)(layerIndex switch
		{
			0 => new Vector2(MathF.Sin(Main.GlobalTimeWrappedHourly * 0.03f) * 10f, MathF.Cos(Main.GlobalTimeWrappedHourly * 0.1f) * 17f), 
			1 => Vector2.UnitY * Main.GlobalTimeWrappedHourly * 0.03f, 
			_ => Vector2.Zero, 
		});
	}

	public override void PrepareSpriteBatch(SpriteBatch spriteBatch)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (!hasRunTextureCorrection)
		{
			Color[] BaseArray = (Color[])(object)new Color[WavyLineLayer.Width * WavyLineLayer.Height];
			Color[] ColorArray = (Color[])(object)new Color[WavyLineLayer.Width * WavyLineLayer.Height];
			WavyLineLayer.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color((int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R);
			}
			WavyLineLayer.SetData<Color>(ColorArray);
			hasRunTextureCorrection = true;
		}
		base.PrepareSpriteBatch(spriteBatch);
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
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (Particle particle in Particles)
		{
			Texture2D texture2d = particle.TextureToUse ?? tex;
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 scale = particle.Scale * particle.Size / (float)texture2d.Width;
			Rectangle frame = texture2d.Frame(1, particle.MaxFrames, 0, particle.CurrentFrame);
			Vector2 origin = frame.Size() * 0.5f;
			Main.spriteBatch.Draw(texture2d, drawPosition, (Rectangle?)frame, Color.White, particle.rotation, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
