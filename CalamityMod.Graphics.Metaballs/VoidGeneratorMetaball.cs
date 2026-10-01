using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class VoidGeneratorMetaball : Metaball
{
	public static List<StreamGougeMetaball.CosmicParticle> Particles { get; private set; } = new List<StreamGougeMetaball.CosmicParticle>();

	public override bool AnythingToDraw => Particles.Any();

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return StreamGougeMetaball.LayerAsset.Value;
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
			return Color.Lerp(Color.Purple, Color.Black, 0.75f);
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((StreamGougeMetaball.CosmicParticle p) => p.Size <= 2f);
	}

	public static StreamGougeMetaball.CosmicParticle SpawnParticle(Vector2 position, Vector2 velocity, float size)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		StreamGougeMetaball.CosmicParticle particle = new StreamGougeMetaball.CosmicParticle(position, velocity, size);
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
		foreach (StreamGougeMetaball.CosmicParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
