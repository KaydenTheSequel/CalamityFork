using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AngelicBeam : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> TrailPos = new List<Vector2>();

	public static int Lifetime = 300;

	public static int Fadetime = 30;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 2000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.MaxUpdates = Lifetime;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, Fadetime, base.Projectile.timeLeft, clamped: true);
			return;
		}
		if (base.Projectile.timeLeft == 1)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.MaxUpdates = 1;
			base.Projectile.timeLeft = Fadetime;
		}
		if (base.Projectile.FinalExtraUpdate() || base.Projectile.numUpdates % 30 == 29 || TrailPos == null)
		{
			if (TrailPos == null)
			{
				TrailPos = new List<Vector2>(10);
				for (int i = 0; i < 10; i++)
				{
					TrailPos.Add(base.Projectile.Center);
				}
			}
			TrailPos.Insert(0, base.Projectile.Center);
			while (TrailPos.Count > 10)
			{
				TrailPos.RemoveAt(TrailPos.Count - 1);
			}
		}
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(10f * base.Projectile.scale, 10f * base.Projectile.scale), base.Projectile.velocity, affectedByGravity: false, 15, Main.rand.NextFloat(1f, 1.2f) * base.Projectile.scale, Color.White));
		}
		if (Main.rand.NextBool(4))
		{
			Color fireColor = Main.hslToRgb(Main.rand.NextFloat(0.08f, 0.13f) + 0.05f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.7f);
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Vector2.UnitX.RotatedByRandom(3.1415927410125732) * (Main.rand.NextFloat(1f, 3f) + 3f * base.Projectile.scale), affectedByGravity: false, 9, Main.rand.NextFloat(1f, 1.2f) * base.Projectile.scale, fireColor));
		}
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		((Rectangle)(ref hitbox)).Inflate((int)((float)base.Projectile.width * (base.Projectile.scale - 1f)), (int)((float)base.Projectile.height * (base.Projectile.scale - 1f)));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		for (int i = 0; i < 6; i++)
		{
			Dust dust = Dust.NewDustPerfect(target.Center, ModContent.DustType<LightDust>(), Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(2f, 12f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1f, 2.4f) * base.Projectile.scale;
			dust.color = Main.hslToRgb(Main.rand.NextFloat(0.033f, 0.167f), 1f, 0.7f);
			dust.noLightEmittence = true;
		}
	}

	public Color LaserColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		return Main.hslToRgb(0.08f + 0.05f * completionRatio + 0.05f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.7f) * 0.8f * base.Projectile.Opacity;
	}

	public float LaserWidth(float completionRatio, Vector2 vertexPos)
	{
		return 30f * base.Projectile.Opacity * base.Projectile.scale;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (TrailPos == null)
		{
			return false;
		}
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:Flame"].UseSaturation(0.5f);
		PrimitiveRenderer.RenderTrail(TrailPos, new PrimitiveSettings(LaserWidth, LaserColor, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 10);
		return false;
	}
}
