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

public class SignusMetaball : Metaball
{
	public class CosmicParticle
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

		public CosmicParticle(Vector2 center, Vector2 velocity, float size, int lifetime, Vector2 squash, float shrinkSpeed = 0f, float velocitySquash = 0f)
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
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			Center += Velocity;
			Velocity *= 0.95f;
			if ((float)time / (float)Lifetime > 0.5f)
			{
				Size *= 0.94f;
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

	public static List<CosmicParticle> Particles { get; private set; } = new List<CosmicParticle>();

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
			Color result = borderColor;
			((Color)(ref result)).A = 0;
			return result;
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/SignusLayer", (AssetRequestMode)1);
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
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
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
			Color.Purple,
			Color.BlueViolet,
			Color.DarkViolet
		};
		int colorIndex = (int)(rate / 2f % (float)colors.Count);
		Color currentColor = colors[colorIndex];
		Color nextColor = colors[(colorIndex + 1) % colors.Count];
		borderColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((CosmicParticle p) => p.Size <= 2f);
	}

	public static void SpawnParticle(Vector2 position, Vector2 velocity, float size, int lifetime, Vector2 squash, float shrinkSpeed = 0f, float velocitySquash = 0f)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new CosmicParticle(position, velocity, size, lifetime, squash, shrinkSpeed, velocitySquash));
	}

	public override Vector2 CalculateManualOffsetForLayer(int layerIndex)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return scrollDir * 0.5f;
	}

	public override void DrawInstances()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicMess", (AssetRequestMode)2).Value;
		foreach (CosmicParticle particle in Particles)
		{
			Vector2 squash = Vector2.Lerp(particle.BaseSquash, new Vector2(Utils.Remap(((Vector2)(ref particle.Velocity)).Length(), 2f, 7f, 1f, 0.5f), Utils.Remap(((Vector2)(ref particle.Velocity)).Length(), 2f, 7f, 1f, 2.5f)), particle.SquashPower);
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White, particle.Rotation, origin, squash * scale, (SpriteEffects)0, 0f);
		}
	}

	public SignusMetaball()
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
