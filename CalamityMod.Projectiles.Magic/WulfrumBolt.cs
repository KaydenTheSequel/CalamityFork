using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WulfrumBolt : ModProjectile, ILocalizedModType, IModType
{
	public static float MaxDeviationAngle = (float)Math.PI / 4f;

	public static float HomingRange = 250f;

	public static float HomingAngle = (float)Math.PI * 33f / 80f;

	internal Color PrimColorMult;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float OriginalRotation => ref base.Projectile.ai[0];

	public NPC Target
	{
		get
		{
			if (base.Projectile.ai[1] < 0f || base.Projectile.ai[1] > (float)Main.maxNPCs)
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
		set
		{
			if (value == null)
			{
				base.Projectile.ai[1] = -1f;
			}
			else
			{
				base.Projectile.ai[1] = value.whoAmI;
			}
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 140;
		base.Projectile.extraUpdates = 2;
	}

	public NPC FindTarget()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		float bestScore = 0f;
		NPC bestTarget = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC potentialTarget = enumerator.Current;
			if (!potentialTarget.CanBeChasedBy())
			{
				continue;
			}
			float distance = potentialTarget.Distance(base.Projectile.Center);
			float angle = base.Projectile.velocity.AngleBetween(potentialTarget.Center - base.Projectile.Center);
			float extraDistance = potentialTarget.width / 2 + potentialTarget.height / 2;
			if (distance - extraDistance < HomingRange && angle < HomingAngle / 2f && (Collision.CanHit(base.Projectile.Center, 1, 1, potentialTarget.Center, 1, 1) || !(extraDistance < distance)))
			{
				float attemptedScore = EvaluatePotentialTarget(distance - extraDistance, angle / 2f);
				if (attemptedScore > bestScore)
				{
					bestTarget = potentialTarget;
					bestScore = attemptedScore;
				}
			}
		}
		return bestTarget;
	}

	public float EvaluatePotentialTarget(float distance, float angle)
	{
		return 1f - distance / HomingRange * 0.5f + (1f - Math.Abs(angle) / (HomingAngle / 2f)) * 0.5f;
	}

	public override void AI()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 140)
		{
			if (OriginalRotation == 0f)
			{
				OriginalRotation = base.Projectile.velocity.ToRotation();
				base.Projectile.rotation = OriginalRotation;
			}
			Target = null;
		}
		else
		{
			Target = FindTarget();
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.GreenYellow * 0.8f;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		Vector2 center2;
		if (Target != null)
		{
			center2 = Target.Center - base.Projectile.Center;
			float distanceFromTarget = ((Vector2)(ref center2)).Length();
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards((Target.Center - base.Projectile.Center).ToRotation(), 0.07f * (float)Math.Pow(1f - distanceFromTarget / HomingRange, 2.0));
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.983f;
		base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		if (base.Projectile.timeLeft == 140)
		{
			Vector2 dustCenter = base.Projectile.Center + base.Projectile.velocity * 1f;
			for (int i = 0; i < 5; i++)
			{
				Vector2? velocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(0.2f, 0.5f);
				float scale = Main.rand.NextFloat(1.2f, 1.8f);
				newColor = default(Color);
				Dust.NewDustPerfect(dustCenter, 15, velocity, 0, newColor, scale).noGravity = true;
			}
		}
		if (base.Projectile.timeLeft <= 137)
		{
			if (Main.rand.NextBool())
			{
				Vector2 center3 = base.Projectile.Center;
				Vector2 velocity2 = base.Projectile.velocity;
				center2 = default(Vector2);
				Vector2 position = center3 + velocity2.RotatedBy(1.5707963705062866, center2).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2? velocity3 = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.5f);
				float scale = Main.rand.NextFloat(1.2f, 1.8f);
				newColor = default(Color);
				Dust.NewDustPerfect(position, 15, velocity3, 0, newColor, scale).noGravity = true;
			}
			if (Main.rand.NextBool(4))
			{
				Vector2 center4 = base.Projectile.Center;
				Vector2 velocity4 = base.Projectile.velocity;
				center2 = default(Vector2);
				Vector2 position2 = center4 + velocity4.RotatedBy(1.5707963705062866, center2).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2? velocity5 = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f);
				float scale = Main.rand.NextFloat(0.4f, 1f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position2, 257, velocity5, 0, newColor, scale);
				dust.noGravity = true;
				dust.noLight = true;
			}
			if (Main.rand.NextBool(5))
			{
				Vector2 center5 = base.Projectile.Center;
				Vector2 velocity6 = base.Projectile.velocity;
				center2 = default(Vector2);
				Vector2 position3 = center5 + velocity6.RotatedBy(1.5707963705062866, center2).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2 velocity7 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.5235987901687622) * Main.rand.NextFloat(4f, 10f);
				GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(position3, velocity7, Main.rand.NextFloat(1f, 2f), Main.rand.NextBool() ? new Color(99, 255, 229) : new Color(25, 132, 247), 25));
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Sqrt(1f - completionRatio);
		return Color.DeepSkyBlue.MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 9.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
		CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 3.5f, delegate(Vector2 offset, Color colorMod)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			PrimColorMult = colorMod;
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f + offset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
		});
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in WulfrumProsthesis.HitSound, base.Projectile.Center);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		return base.OnTileCollide(oldVelocity);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		int numParticles = Main.rand.Next(4, 7);
		for (int i = 0; i < numParticles; i++)
		{
			Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.5235987901687622) * Main.rand.NextFloat(3f, 14f);
			GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(target.Center, velocity, Main.rand.NextFloat(2.5f, 3f), Main.rand.NextBool() ? new Color(99, 255, 229) : new Color(25, 132, 247), 25));
		}
	}

	public WulfrumBolt()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PrimColorMult = Color.White;
		base._002Ector();
	}
}
