using System;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SpeedBlasterShot : ModProjectile, ILocalizedModType, IModType
{
	public Color MainColor;

	public bool PostHit;

	public int time;

	public static readonly SoundStyle ShotImpact = new SoundStyle("CalamityMod/Sounds/Item/SplatshotImpact")
	{
		PitchVariance = 0.3f,
		Volume = 2.5f
	};

	public static readonly SoundStyle ShotImpactBig = new SoundStyle("CalamityMod/Sounds/Item/SplatshotBigImpact")
	{
		PitchVariance = 0.3f,
		Volume = 4f
	};

	public new string LocalizationCategory => "Projectiles.Ranged";

	public bool DashShot => base.Projectile.ai[1] == 3f;

	public bool PostDashShot => base.Projectile.ai[1] == 2f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 150 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		if (PostHit)
		{
			base.Projectile.velocity = base.Projectile.velocity * 0.01f;
			base.Projectile.scale *= 0.95f;
			base.Projectile.tileCollide = false;
		}
		float num = base.Projectile.ai[0];
		if (num == 0f)
		{
			goto IL_0081;
		}
		if (num != 1f)
		{
			if (num != 2f)
			{
				if (num != 3f)
				{
					if (num != 4f)
					{
						goto IL_0081;
					}
					MainColor = Color.Yellow;
				}
				else
				{
					MainColor = Color.Lime;
				}
			}
			else
			{
				MainColor = Color.Magenta;
			}
		}
		else
		{
			MainColor = Color.Blue;
		}
		goto IL_00c0;
		IL_0081:
		MainColor = Color.Cyan;
		goto IL_00c0;
		IL_00c0:
		if (base.Projectile.ai[2] != 1f)
		{
			if (DashShot)
			{
				base.Projectile.scale = 2f;
				base.Projectile.penetrate = 4;
				base.Projectile.extraUpdates = 90;
				base.Projectile.timeLeft = 120 * base.Projectile.extraUpdates;
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.3f;
			}
			else if (PostDashShot)
			{
				base.Projectile.MaxUpdates = 3;
			}
			base.Projectile.ai[2] = 1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (!PostHit && time > (PostDashShot ? 35 : 25))
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= (DashShot ? 1f : 0.945f);
			base.Projectile.velocity.Y += (DashShot ? 0f : 0.6f);
		}
		Color ColorUsed = GetColor(base.Projectile.ai[0]);
		if (Main.rand.NextBool(20) && !DashShot && !PostHit)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 192);
			dust.noLight = true;
			dust.noGravity = false;
			dust.scale = 1.2f;
			dust.velocity = new Vector2((float)Main.rand.Next(-1, 1), 3f);
			dust.color = ColorUsed;
			dust.alpha = 75;
		}
		if (DashShot)
		{
			float num2 = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
			_ = 1400f;
			if (num2 < 1400f)
			{
				Main.rand.NextBool();
			}
			if (num2 < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 11, 0.15f, MainColor, new Vector2(0.7f, 1.1f), useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.65f));
			}
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 192);
			dust2.noLight = true;
			dust2.noGravity = false;
			dust2.scale = Main.rand.NextFloat(1.3f, 1.5f);
			dust2.velocity = Utils.RotatedByRandom(new Vector2((float)Main.rand.Next(-1, 1), (float)Main.rand.Next(0, 8)), MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.05f, 0.3f);
			dust2.color = ColorUsed;
			dust2.alpha = Main.rand.Next(145, 240);
		}
		if (base.Projectile.timeLeft == 300 && DashShot)
		{
			for (int i = 0; i <= 10; i++)
			{
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 192, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.4f, 1.2f));
				dust3.noGravity = true;
				dust3.color = ColorUsed;
				dust3.alpha = Main.rand.Next(40, 90);
				dust3.scale = Main.rand.NextFloat(1.2f, 2.3f);
				dust3.noLight = true;
			}
		}
		if (base.Projectile.timeLeft == 300 && !DashShot)
		{
			for (int j = 0; j <= 7; j++)
			{
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, 192, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(PostDashShot ? 13f : 23f)) * Main.rand.NextFloat(0.4f, 1.2f));
				dust4.noGravity = true;
				dust4.color = ColorUsed;
				dust4.alpha = Main.rand.Next(40, 90);
				dust4.scale = Main.rand.NextFloat(0.7f, 1.6f);
				dust4.noLight = true;
			}
		}
		time++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (PostHit)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		GetColor(base.Projectile.ai[0]);
		if (DashShot)
		{
			if (base.Projectile.numHits == 0)
			{
				SoundEngine.PlaySound(in ShotImpactBig, base.Projectile.position);
			}
			Vector2 BurstFXDirection = Utils.RotatedByRandom(new Vector2(5f, 0f), 100.0);
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, BurstFXDirection * (float)(i + 1), affectedByGravity: false, 7, 1.5f - (float)i * 0.6f, MainColor * 0.8f));
			}
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -BurstFXDirection * (float)(j + 1), affectedByGravity: false, 7, 1.5f - (float)j * 0.6f, MainColor * 0.8f));
			}
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, MainColor, 0.6f, 13, produceLight: false));
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, Color.White, 0.5f, 12, produceLight: false));
		}
		else
		{
			for (int k = 0; k <= 8; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(7f, 7f), 192, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20f)) * Main.rand.NextFloat(0.05f, 0.45f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.2f));
				dust.noLight = true;
				dust.noGravity = false;
				dust.color = GetColor(base.Projectile.ai[0]);
				dust.alpha = 75;
			}
			base.Projectile.timeLeft = 15;
			PostHit = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Vector2 paintPos = base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f) + base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(4f, 20f);
			float paintSize = Main.rand.NextFloat(40f, 70f);
			float num = base.Projectile.ai[0];
			if (num != 0f)
			{
				if (num == 1f)
				{
					ModContent.GetInstance<BluePaint>().SpawnParticle(paintPos, paintSize);
					continue;
				}
				if (num == 2f)
				{
					ModContent.GetInstance<MagentaPaint>().SpawnParticle(paintPos, paintSize);
					continue;
				}
				if (num == 3f)
				{
					ModContent.GetInstance<LimePaint>().SpawnParticle(paintPos, paintSize);
					continue;
				}
				if (num == 4f)
				{
					ModContent.GetInstance<YellowPaint>().SpawnParticle(paintPos, paintSize);
					continue;
				}
			}
			ModContent.GetInstance<CyanPaint>().SpawnParticle(paintPos, paintSize);
		}
		base.Projectile.timeLeft = 15;
		PostHit = true;
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!DashShot)
		{
			SoundEngine.PlaySound(in ShotImpact, base.Projectile.position);
		}
	}

	public static Color GetColor(float type)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (type != 0f)
		{
			if (type != 1f)
			{
				if (type != 2f)
				{
					if (type != 3f)
					{
						return Color.Yellow;
					}
					return Color.Lime;
				}
				return Color.Fuchsia;
			}
			return Color.Blue;
		}
		return Color.Aqua;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		return GetColor(base.Projectile.ai[0]) * (float)(int)((Color)(ref drawColor)).A * base.Projectile.Opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 6f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return GetColor(base.Projectile.ai[0]) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}), 20);
		return true;
	}
}
