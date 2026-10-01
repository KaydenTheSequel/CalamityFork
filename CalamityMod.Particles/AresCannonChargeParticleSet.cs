using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class AresCannonChargeParticleSet : BaseParticleSet
{
	public Color ParticleColor;

	public float SpawnAreaCompactness;

	public float MoveRotationOffset;

	public float chargeProgress;

	public Particle bloom;

	public List<Particle> Pulses;

	public CalamityUtils.CurveSegment Rise1;

	public CalamityUtils.CurveSegment Fall1;

	public CalamityUtils.CurveSegment Rise2;

	public CalamityUtils.CurveSegment Fall2;

	public CalamityUtils.CurveSegment Rise3;

	public CalamityUtils.CurveSegment Fall3;

	public CalamityUtils.CurveSegment Rise4;

	public CalamityUtils.CurveSegment Fall4;

	public CalamityUtils.CurveSegment Rise5;

	public CalamityUtils.CurveSegment Finale;

	public override int ParticleLifetime => 30;

	public AresCannonChargeParticleSet(int setLifetime, int particleSpawnRate, float spawnAreaCompactness, Color particleColor)
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		Pulses = new List<Particle>();
		Rise1 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0f, 0f, 0.25f);
		Fall1 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.13f, 0.25f, -0.05f);
		Rise2 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.2f, 0.2f, 0.3f);
		Fall2 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.33f, 0.5f, -0.1f);
		Rise3 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.4f, 0.4f, 0.3f);
		Fall3 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.53f, 0.7f, -0.1f);
		Rise4 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.6f, 0.6f, 0.3f);
		Fall4 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.73f, 0.9f, -0.1f);
		Rise5 = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.8f, 0.8f, 0.3f);
		Finale = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineIn, 0.93f, 1.1f, -1.1f);
		base._002Ector(setLifetime, particleSpawnRate);
		ParticleColor = particleColor;
		MoveRotationOffset = Main.rand.NextFloat(-0.36f, 0.36f);
		SpawnAreaCompactness = spawnAreaCompactness;
	}

	public override Particle SpawnParticle()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Main.rand.NextVector2CircularEdge(1f, 1f) * SpawnAreaCompactness;
		return new ChargeUpLineVFX(val, val.ToRotation(), 0.5f, ParticleColor, ParticleLifetime, 0.75f, telegraph: true, 0.2f, 10f);
	}

	public float ChargeSize()
	{
		return CalamityUtils.PiecewiseAnimation(chargeProgress, Rise1, Fall1, Rise2, Fall2, Rise3, Fall3, Rise4, Fall4, Rise5, Finale);
	}

	public override void Update()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		if (bloom == null)
		{
			bloom = new StrongBloom(Vector2.Zero, Vector2.Zero, ParticleColor, 0.5f, 2);
		}
		else
		{
			bloom.Time = 0;
			bloom.Color = ParticleColor * 0.8f * chargeProgress;
			bloom.Scale = ChargeSize() * 2f;
		}
		bool closeToDeath = base.LocalTimer >= base.SetLifetime - ParticleLifetime && base.SetLifetime > 0;
		if (base.LocalTimer % ParticleSpawnRate == ParticleSpawnRate - 1 && !closeToDeath)
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
		foreach (Particle pulse in Pulses)
		{
			pulse.Time++;
			pulse.Update();
		}
		Particles.RemoveAll((Particle particle3) => particle3.Time >= particle3.Lifetime && particle3.SetLifetime);
		Pulses.RemoveAll((Particle pulse) => pulse.Time >= pulse.Lifetime && pulse.SetLifetime);
		base.LocalTimer++;
	}

	public void DrawBloom(Vector2 basePosition)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (bloom != null)
		{
			bloom.CustomDraw(Main.spriteBatch, basePosition);
		}
	}

	public void AddPulse(float pulseCounter)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Particle pulse = new PulseRing(Vector2.Zero, Vector2.Zero, (pulseCounter == 5f) ? Color.Lerp(Color.White, ParticleColor, 0.5f) : ParticleColor, 0.2f, 0.5f * pulseCounter * 0.5f, 20);
		Pulses.Add(pulse);
	}

	public void DrawPulses(Vector2 basePosition)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		foreach (Particle pulse in Pulses)
		{
			pulse.CustomDraw(Main.spriteBatch, basePosition);
		}
	}
}
