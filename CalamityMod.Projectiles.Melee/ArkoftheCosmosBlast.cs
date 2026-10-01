using System;
using System.IO;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ArkoftheCosmosBlast : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private const int maxStitches = 10;

	public float[] StitchRotations = new float[10];

	public float[] StitchLifetimes = new float[10];

	private const float MaxTime = 70f;

	private const float SnapTime = 25f;

	private const float HoldTime = 15f;

	public Particle PolarStar;

	public CalamityUtils.CurveSegment anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0.2f, -0.1f);

	public CalamityUtils.CurveSegment thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.3f, 0.2f, 3f, 3);

	public CalamityUtils.CurveSegment openMore = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, -0.15f);

	public CalamityUtils.CurveSegment close = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0.35f, 0f, 1f, 4);

	public CalamityUtils.CurveSegment stayClosed = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float Charge => ref base.Projectile.ai[0];

	public int CurrentStitches => (int)Math.Ceiling((1f - (float)Math.Sqrt(1f - (float)Math.Pow(MathHelper.Clamp(StitchProgress * 3f, 0f, 1f), 2.0))) * 10f);

	public float SnapTimer => 70f - (float)base.Projectile.timeLeft;

	public float HoldTimer => 70f - (float)base.Projectile.timeLeft - 25f;

	public float StitchTimer => 70f - (float)base.Projectile.timeLeft - 25f - 7.5f;

	public float SnapProgress => MathHelper.Clamp(SnapTimer / 25f, 0f, 1f);

	public float HoldProgress => MathHelper.Clamp(HoldTimer / 15f, 0f, 1f);

	public float StitchProgress => MathHelper.Clamp(StitchTimer / 37.5f, 0f, 1f);

	public int CurrentAnimation
	{
		get
		{
			if (!(70f - (float)base.Projectile.timeLeft <= 25f))
			{
				if (!(70f - (float)base.Projectile.timeLeft <= 40f))
				{
					return 2;
				}
				return 1;
			}
			return 0;
		}
	}

	public Vector2 scissorPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + ThrustDisplaceRatio() * base.Projectile.velocity * 200f;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.width = (base.Projectile.height = 300);
		base.Projectile.width = (base.Projectile.height = 300);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? CanDamage()
	{
		return HoldProgress > 0f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (HoldProgress == 0f)
		{
			return false;
		}
		float collisionPoint = 0f;
		float bladeLength = ThrustDisplaceRatio() * 242f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * bladeLength, 30f, ref collisionPoint);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style;
		if (!initialized)
		{
			base.Projectile.timeLeft = 70;
			style = SoundID.Item84 with
			{
				Volume = SoundID.Item84.Volume * 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.scale = 1.4f;
		HandleParticles();
		if (StitchProgress == 0f)
		{
			BigRipMetaball.Particle particle = BigRipMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity * MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, 0.5f), Vector2.Zero, 242f);
			particle.SizeScaling = 0f;
			particle.TextureToUse = TextureAssets.Projectile[base.Type].Value;
			particle.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			particle.Scale = new Vector2(0.25f * ThrustDisplaceRatio(), 1f * ThrustDisplaceRatio());
		}
		if (SnapTimer == 5f)
		{
			style = CommonCalamitySounds.MeatySlashSound with
			{
				Pitch = -0.5f,
				Volume = 0.5f,
				MaxInstances = 2
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (HoldTimer != 1f)
		{
			return;
		}
		BigRipMetaball.Particle particle2 = BigRipMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity * MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, 0.5f), Vector2.Zero, 242f);
		particle2.SizeScaling = 0.925f;
		particle2.TextureToUse = TextureAssets.Projectile[base.Type].Value;
		particle2.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		particle2.Scale = new Vector2(0.25f * ThrustDisplaceRatio(), 1f * ThrustDisplaceRatio());
		style = CommonCalamitySounds.SwiftSliceSound with
		{
			Pitch = 0f,
			MaxInstances = 2
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Main.LocalPlayer.SetScreenshake(7.5f);
		for (int i = 0; i < 20; i++)
		{
			float positionAlongLine = MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, Main.rand.NextFloat(0f, 1f));
			Vector2 particlePosition = base.Projectile.Center + base.Projectile.velocity * positionAlongLine;
			Color particleColor = (Main.rand.NextBool() ? Color.OrangeRed : (Main.rand.NextBool() ? Color.White : Color.Orange));
			float particleScale = Main.rand.NextFloat(0.05f, 0.4f) * (0.4f + 0.6f * (float)Math.Sin(positionAlongLine / (ThrustDisplaceRatio() * 242f) * (float)Math.PI));
			switch (Main.rand.Next(3))
			{
			case 0:
				GeneralParticleHandler.SpawnParticle(new StrongBloom(particlePosition, Vector2.UnitY * Main.rand.NextFloat(-4f, -1f), particleColor, particleScale, Main.rand.Next(20) + 10));
				break;
			case 1:
				GeneralParticleHandler.SpawnParticle(new GenericBloom(particlePosition, Vector2.UnitY * Main.rand.NextFloat(-4f, -1f), particleColor, particleScale, Main.rand.Next(20) + 10));
				break;
			case 2:
				GeneralParticleHandler.SpawnParticle(new CritSpark(particlePosition, Vector2.UnitY * Main.rand.NextFloat(-10f, -1f), Color.White, particleColor, particleScale * 7f, Main.rand.Next(20) + 10, 0.1f, 3f));
				break;
			}
		}
		if (Owner.whoAmI == Main.myPlayer)
		{
			int starAmt = 5;
			for (int s = 1; s <= starAmt; s++)
			{
				float lerpRatio = (float)s / (float)starAmt;
				float positionAlongLine2 = MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, lerpRatio);
				Vector2 starPosition = base.Projectile.Center + base.Projectile.velocity * positionAlongLine2;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), starPosition, Main.rand.NextVector2CircularEdge(28f, 28f), ModContent.ProjectileType<EonBolt>(), (int)(ArkoftheCosmos.BlastBoltsDamageMultiplier * (float)base.Projectile.damage), 0f, Owner.whoAmI, 0.55f, 0.21991149f).timeLeft = 100;
			}
		}
	}

	public void HandleParticles()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		if (PolarStar == null)
		{
			PolarStar = new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, Color.CornflowerBlue, base.Projectile.scale * 2f, 2, 0.1f, 5f, needed: true);
			GeneralParticleHandler.SpawnParticle(PolarStar);
		}
		else if (HoldProgress <= 0.4f)
		{
			PolarStar.Time = 0;
			PolarStar.Position = scissorPosition + base.Projectile.velocity * SnapProgress * 150f;
			PolarStar.Scale = base.Projectile.scale * 2f;
		}
		for (int i = 0; i < CurrentStitches; i++)
		{
			if (StitchRotations[i] == 0f)
			{
				StitchRotations[i] = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f) + (float)Math.PI / 2f;
				SoundStyle sewSound = ((i % 3 == 0) ? SoundID.Item63 : ((i % 3 == 1) ? SoundID.Item64 : SoundID.Item65));
				SoundEngine.PlaySound(sewSound with
				{
					Volume = sewSound.Volume * 0.5f
				}, Owner.Center);
				float positionAlongLine = ThrustDisplaceRatio() * 242f / 10f * 0.5f + MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, (float)i / 10f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + base.Projectile.velocity * positionAlongLine, Vector2.Zero, Color.White, Color.Cyan, 3f, 8, 0.1f, 3f));
			}
			StitchLifetimes[i]++;
		}
		if (StitchProgress > 0f)
		{
			for (int m = 0; m < 2; m++)
			{
				float positionAlongLine2 = MathHelper.Lerp(0f, ThrustDisplaceRatio() * 242f, Main.rand.NextFloat());
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + base.Projectile.velocity * positionAlongLine2, Main.rand.NextVector2CircularEdge(4f, 4f), new Color(117, 36, 32), 12, 0.8f, 0.6f, 0f, glowing: true));
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		Color pulseColor = ((!Main.rand.NextBool()) ? (Main.rand.NextBool() ? Color.OrangeRed : Color.Gold) : (Main.rand.NextBool() ? Color.Orange : Color.Coral));
		GeneralParticleHandler.SpawnParticle(new PulseRing(target.Center, Vector2.Zero, pulseColor, 0.05f, 0.2f + Main.rand.NextFloat(0f, 1f), 30));
		for (int i = 0; i < 10; i++)
		{
			Vector2 particleSpeed = base.Projectile.velocity.RotatedByRandom(0.6283185482025146) * Main.rand.NextFloat(2.6f, 4f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(target.Center, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Red, 60, 1f, 1.5f, 3f, 0.002f));
		}
	}

	internal float ThrustDisplaceRatio()
	{
		return CalamityUtils.PiecewiseAnimation(SnapProgress, anticipation, thrust);
	}

	internal float RotationRatio()
	{
		return CalamityUtils.PiecewiseAnimation(SnapProgress, openMore, close, stayClosed);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (HoldProgress <= 0.4f)
		{
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeft", (AssetRequestMode)2).Value;
			Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRight", (AssetRequestMode)2).Value;
			float snippingRotation = base.Projectile.rotation + (float)Math.PI / 4f;
			float drawRotation = MathHelper.Lerp(snippingRotation - (float)Math.PI / 4f, snippingRotation, RotationRatio());
			float drawRotationBack = MathHelper.Lerp(snippingRotation + (float)Math.PI / 4f, snippingRotation, RotationRatio());
			Vector2 drawOrigin = default(Vector2);
			((Vector2)(ref drawOrigin))._002Ector(33f, 86f);
			Vector2 drawOriginBack = default(Vector2);
			((Vector2)(ref drawOriginBack))._002Ector(44f, 86f);
			Vector2 drawPosition = scissorPosition - Main.screenPosition;
			float opacity = (0.4f - HoldProgress) / 0.4f;
			Color drawColor = Color.Tomato * opacity * 0.9f;
			Color drawColorBack = Color.DeepSkyBlue * opacity * 0.9f;
			Main.EntitySpriteDraw(value2, drawPosition, null, drawColorBack, drawRotationBack, drawOriginBack, base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPosition, null, drawColor * opacity, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(initialized);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		initialized = reader.ReadBoolean();
	}
}
