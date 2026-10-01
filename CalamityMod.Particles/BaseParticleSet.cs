using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public abstract class BaseParticleSet
{
	public int ParticleSpawnRate;

	public List<Particle> Particles = new List<Particle>();

	public int LocalTimer { get; internal set; }

	public int SetLifetime { get; internal set; }

	public abstract int ParticleLifetime { get; }

	public virtual Func<Particle, int> OrderFunction { get; }

	public abstract Particle SpawnParticle();

	public BaseParticleSet(int setLifetime, int particleSpawnRate)
	{
		SetLifetime = setLifetime;
		ParticleSpawnRate = particleSpawnRate;
	}

	public virtual void Update()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		bool closeToDeath = LocalTimer >= SetLifetime - ParticleLifetime && SetLifetime > 0;
		if (LocalTimer % ParticleSpawnRate == ParticleSpawnRate - 1 && !closeToDeath)
		{
			Particle particle = SpawnParticle();
			Particles.Add(particle);
		}
		foreach (Particle particle2 in Particles)
		{
			particle2.RelativeOffset += particle2.Velocity;
			particle2.Time++;
			particle2.Update();
		}
		Particles.RemoveAll((Particle particle3) => particle3.Time >= particle3.Lifetime && particle3.SetLifetime);
		LocalTimer++;
	}

	public virtual void DrawSet(Vector2 basePosition)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		IEnumerable<Particle> orderedParticles = Particles;
		if (OrderFunction != null)
		{
			orderedParticles = orderedParticles.OrderBy(OrderFunction);
		}
		foreach (Particle particle in Particles.OrderBy((Particle p) => p.Time))
		{
			if (particle.UseCustomDraw)
			{
				particle.CustomDraw(Main.spriteBatch, basePosition);
				continue;
			}
			Texture2D tex = ModContent.Request<Texture2D>(particle.Texture, (AssetRequestMode)2).Value;
			Vector2 drawPosition = basePosition - Main.screenPosition + particle.RelativeOffset;
			_ = particle.Color;
			Rectangle frame = tex.Frame(1, particle.FrameVariants, 0, particle.Variant);
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)frame, particle.Color, 0f, frame.Size() * 0.5f, particle.Scale, (SpriteEffects)0, 0f);
		}
	}
}
