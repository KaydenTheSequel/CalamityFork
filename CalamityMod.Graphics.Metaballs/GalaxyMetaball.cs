using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class GalaxyMetaball : Metaball
{
	public class CosmicParticle
	{
		public float Size;

		public Vector2 Velocity;

		public Vector2 Center;

		public CosmicParticle(Vector2 center, Vector2 velocity, float size)
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
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			Center += Velocity;
			Velocity *= 0.96f;
			Size *= 0.91f;
		}
	}

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
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.DarkBlue, Color.Black, 0.75f);
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			LayerAsset = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/StreamGougeLayer", (AssetRequestMode)1);
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((CosmicParticle p) => p.Size <= 2f);
	}

	public static void SpawnParticle(Vector2 position, Vector2 velocity, float size)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new CosmicParticle(position, velocity, size));
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		foreach (CosmicParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
