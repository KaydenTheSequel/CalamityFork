using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VolterionShot : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> TrailPos = new List<Vector2>();

	public const int TrailLength = 50;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float OrbType => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 5000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.MaxUpdates = 15;
		base.Projectile.timeLeft = 24 * base.Projectile.MaxUpdates;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.scale -= 0.005f;
			base.Projectile.Opacity -= 0.005f;
			if (base.Projectile.Opacity <= 0f)
			{
				base.Projectile.Kill();
			}
			return;
		}
		if (base.Projectile.FinalExtraUpdate() || base.Projectile.numUpdates % 3 == 2 || TrailPos == null)
		{
			if (TrailPos == null)
			{
				TrailPos = new List<Vector2>(15);
				for (int i = 0; i < 15; i++)
				{
					TrailPos.Add(base.Projectile.Center);
				}
			}
			Vector2 randOffset = (Vector2.UnitY * Main.rand.NextFloat(-24f, 24f)).RotatedBy(base.Projectile.rotation);
			TrailPos.Insert(0, base.Projectile.Center + randOffset);
			while (TrailPos.Count > 50)
			{
				TrailPos.RemoveAt(TrailPos.Count - 1);
			}
		}
		if (Main.rand.NextBool(6))
		{
			GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(36f)), affectedByGravity: false, 10, 0.3f, TrailColorFunction(0f, Vector2.Zero), Vector2.One, glowCenter: true));
		}
		if (base.Projectile.timeLeft < 50)
		{
			Decay();
		}
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits != 0)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 60);
		if (base.Projectile.numHits == 0)
		{
			Decay();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (base.Projectile.numHits == 0)
		{
			Decay();
		}
		return false;
	}

	public void Decay()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.numHits++;
		base.Projectile.timeLeft = 200;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.netUpdate = true;
		TrailPos.Insert(0, base.Projectile.Center);
		while (TrailPos.Count > 50)
		{
			TrailPos.RemoveAt(TrailPos.Count - 1);
		}
		bool secondary = OrbType > 0f;
		if (base.Projectile.owner == Main.myPlayer && !secondary)
		{
			float rotOffset = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			for (int i = 0; i < 5; i++)
			{
				Vector2 velocity = Vector2.UnitX.RotatedBy(rotOffset + (float)Math.PI * 2f / 5f * ((float)i + Main.rand.NextFloat(-0.4f, 0.4f))) * Main.rand.NextFloat(40f, 45f);
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<VolterionOrb>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, i).rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			}
		}
		int boltCount = (secondary ? 6 : 10);
		for (int j = 0; j < boltCount; j++)
		{
			Color color = ((OrbType > 0f) ? VolterionOrb.GetColor(OrbType - 1f) : (Main.rand.NextBool() ? Color.Cyan : Color.Orchid));
			Vector2 velocity2 = Vector2.UnitX.RotatedBy((float)Math.PI * 2f / 5f * ((float)j + Main.rand.NextFloat(-0.4f, 0.4f))) * Main.rand.NextFloat(12f, 15f);
			GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, velocity2, affectedByGravity: false, 18, Main.rand.NextFloat(0.4f, 0.6f), color, new Vector2(0.6f, 1f), glowCenter: true));
		}
		for (int k = 0; k < 7; k++)
		{
			Vector2 velocity3 = Main.rand.NextVector2Unit() * Main.rand.NextFloat(8f, 14f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity3);
			dust.noLight = true;
			dust.color = (Main.rand.NextBool() ? Color.Cyan : Color.Orchid);
		}
	}

	internal float TrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 15f * Utils.GetLerpValue(0.5f, 0.4f, MathF.Abs(0.5f - completionRatio), clamped: true);
	}

	internal Color TrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (!(OrbType > 0f))
		{
			return Color.Lerp(new Color(51, 197, 255), new Color(143, 51, 255), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 20f)) * base.Projectile.Opacity;
		}
		return VolterionOrb.GetColor(OrbType - 1f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (TrailPos == null)
		{
			return false;
		}
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].Apply();
		PrimitiveRenderer.RenderTrail(TrailPos, new PrimitiveSettings(TrailWidthFunction, TrailColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"]), 50);
		return false;
	}
}
