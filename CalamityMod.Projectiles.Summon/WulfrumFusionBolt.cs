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

namespace CalamityMod.Projectiles.Summon;

public class WulfrumFusionBolt : ModProjectile, ILocalizedModType, IModType
{
	public static float MaxDeviationAngle = (float)Math.PI / 4f;

	public static float HomingRange = 250f;

	public static float HomingAngle = (float)Math.PI * 33f / 80f;

	internal Color PrimColorMult;

	public new string LocalizationCategory => "Projectiles.Summon";

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
		base.Projectile.DamageType = DamageClass.Summon;
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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 140)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			Target = null;
		}
		else
		{
			Target = FindTarget();
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.DeepSkyBlue;
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
			if (Main.rand.NextBool(5))
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
			if (Main.rand.NextBool(8))
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
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Sqrt(1f - completionRatio);
		return Color.Lerp(Color.DeepSkyBlue, Color.YellowGreen, (float)base.Projectile.timeLeft / 140f).MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 6.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
		CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 0.5f, delegate(Vector2 offset, Color colorMod)
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = WulfrumProsthesis.HitSound with
		{
			Volume = WulfrumProsthesis.HitSound.Volume * 0.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
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
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
		int numParticles = Main.rand.Next(1, 3);
		for (int i = 0; i < numParticles; i++)
		{
			Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.5235987901687622) * Main.rand.NextFloat(3f, 14f);
			GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(target.Center, velocity, Main.rand.NextFloat(2.5f, 3f), Main.rand.NextBool() ? new Color(99, 255, 229) : new Color(25, 132, 247), 25));
		}
	}

	public WulfrumFusionBolt()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PrimColorMult = Color.White;
		base._002Ector();
	}
}
