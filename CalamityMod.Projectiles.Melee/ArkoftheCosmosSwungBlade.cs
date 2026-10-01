using System;
using System.IO;
using System.Linq;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ArkoftheCosmosSwungBlade : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private Particle smear;

	private float SwingWidth;

	public const float MaxThrowTime = 140f;

	public float ThrowReach;

	public const float SnapWindowStart = 0.35f;

	public const float SnapWindowEnd = 0.75f;

	public CalamityUtils.CurveSegment anticipation;

	public CalamityUtils.CurveSegment thrust;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment startup;

	public CalamityUtils.CurveSegment swing;

	public CalamityUtils.CurveSegment shoot;

	public CalamityUtils.CurveSegment remain;

	public CalamityUtils.CurveSegment retract;

	public CalamityUtils.CurveSegment sizeCurve;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/SunderingScissorsRight";

	public ref float Combo => ref base.Projectile.ai[0];

	public ref float Charge => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public float MaxSwingTime => SwirlSwing ? 55 : 35;

	public int SwingDirection
	{
		get
		{
			float combo = Combo;
			if (combo != 0f)
			{
				if (combo == 1f)
				{
					return -1 * Math.Sign(direction.X);
				}
				return 0;
			}
			return Math.Sign(direction.X);
		}
	}

	public bool SwirlSwing => Combo == 1f;

	public Vector2 DistanceFromPlayer
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return direction * 30f;
		}
	}

	public float SwingTimer => MaxSwingTime - (float)base.Projectile.timeLeft;

	public float SwingCompletion => SwingTimer / MaxSwingTime;

	public ref float HasFired => ref base.Projectile.localAI[0];

	private bool OwnerCanShoot
	{
		get
		{
			if (!Owner.CantUseHoldout())
			{
				return Owner.HeldItem.type == ModContent.ItemType<ArkoftheCosmos>();
			}
			return false;
		}
	}

	public bool Thrown
	{
		get
		{
			if (Combo != 2f)
			{
				return Combo == 3f;
			}
			return true;
		}
	}

	public float ThrowTimer => 140f - (float)base.Projectile.timeLeft;

	public float ThrowCompletion => ThrowTimer / 140f;

	public float SnapEndTime => 35f;

	public float SnapEndCompletion => (SnapEndTime - (float)base.Projectile.timeLeft) / SnapEndTime;

	public ref float ChanceMissed => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = (Thrown ? 10 : ((int)MaxSwingTime));
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		float bladeLength = 172f * base.Projectile.scale;
		if (Thrown)
		{
			bool mainCollision = Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - Vector2.One * bladeLength / 2f, Vector2.One * bladeLength);
			if (Combo == 2f)
			{
				return mainCollision;
			}
			Vector2 thrownBladeStart = Vector2.SmoothStep(Owner.Center, base.Projectile.Center, MathHelper.Clamp(SnapEndCompletion + 0.25f, 0f, 1f));
			bool thrownScissorCollision = Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), thrownBladeStart, thrownBladeStart + direction * bladeLength);
			return mainCollision | thrownScissorCollision;
		}
		float collisionPoint = 0f;
		Vector2 distanceFromPlayer = DistanceFromPlayer;
		Vector2 holdPoint = ((Vector2)(ref distanceFromPlayer)).Length() * base.Projectile.rotation.ToRotationVector2();
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + holdPoint, Owner.Center + holdPoint + base.Projectile.rotation.ToRotationVector2() * bladeLength, 24f, ref collisionPoint);
	}

	internal float SwingRatio()
	{
		return CalamityUtils.PiecewiseAnimation(SwingCompletion, anticipation, thrust, hold);
	}

	internal float SwirlRatio()
	{
		return CalamityUtils.PiecewiseAnimation(SwingCompletion, startup, swing);
	}

	internal float ThrowRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ThrowCompletion, shoot, remain, retract);
	}

	internal float ThrowScaleRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ThrowCompletion, sizeCurve);
	}

	public override void AI()
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = (Thrown ? 140 : ((int)MaxSwingTime));
			SoundEngine.PlaySound((Charge > 0f || Thrown) ? CommonCalamitySounds.LouderPhantomPhoenix : SoundID.Item71, base.Projectile.Center);
			direction = base.Projectile.velocity;
			((Vector2)(ref direction)).Normalize();
			base.Projectile.velocity = direction;
			base.Projectile.rotation = direction.ToRotation();
			if (SwirlSwing)
			{
				base.Projectile.localNPCHitCooldown = (int)((float)base.Projectile.localNPCHitCooldown / 4f);
			}
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		if (!Thrown)
		{
			base.Projectile.Center = Owner.Center + DistanceFromPlayer;
			if (!SwirlSwing)
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.Lerp(SwingWidth / 2f * (float)SwingDirection, (0f - SwingWidth) / 2f * (float)SwingDirection, SwingRatio());
			}
			else
			{
				float startRot = (float)Math.PI * 3f / 4f * (float)SwingDirection;
				float endRot = -7.4612827f * (float)SwingDirection;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.Lerp(startRot, endRot, SwirlRatio());
				DoParticleEffects(swirlSwing: true);
				if (Owner.whoAmI == Main.myPlayer && (double)(base.Projectile.timeLeft - 1) % Math.Ceiling(MaxSwingTime / ArkoftheCosmos.SwirlBoltAmount) == 0.0)
				{
					float adjustedBlastRotation = base.Projectile.rotation - (float)Math.PI * 23f / 80f * (float)Owner.direction;
					Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center + adjustedBlastRotation.ToRotationVector2() * 10f, adjustedBlastRotation.ToRotationVector2() * 20f, ModContent.ProjectileType<EonBolt>(), (int)(ArkoftheCosmos.SwirlBoltDamageMultiplier / ArkoftheCosmos.SwirlBoltAmount * (float)base.Projectile.damage), 0f, Owner.whoAmI, 0.55f, (float)Math.PI / 20f).timeLeft = 100;
				}
			}
			base.Projectile.scale = 1.2f + (float)Math.Sin(SwingRatio() * (float)Math.PI) * 0.6f + Charge / 10f * 0.2f;
		}
		else
		{
			if (Math.Abs(ThrowCompletion - 0.35f + 0.1f) <= 0.005f && ChanceMissed == 0f && Main.myPlayer == Owner.whoAmI)
			{
				GeneralParticleHandler.SpawnParticle(new PulseRing(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, 0.05f, 1.8f, 8));
				SoundEngine.PlaySound(in SoundID.Item4);
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, Vector2.Zero, ModContent.ProjectileType<ArkoftheCosmosConstellation>(), (int)((float)base.Projectile.damage * ArkoftheCosmos.chainDamageMultiplier), 0f, Owner.whoAmI, (int)((float)base.Projectile.timeLeft / 2f)).timeLeft = (int)((float)base.Projectile.timeLeft / 2f);
			}
			Vector2 mouse = Owner.ClampedMouseWorld();
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, mouse, 0.025f * ThrowRatio());
			base.Projectile.Center = base.Projectile.Center.MoveTowards(mouse, 20f * ThrowRatio());
			Vector2 val = base.Projectile.Center - Owner.Center;
			if (((Vector2)(ref val)).Length() > ArkoftheCosmos.MaxThrowReach)
			{
				base.Projectile.Center = Owner.Center + Owner.DirectionTo(base.Projectile.Center) * ArkoftheCosmos.MaxThrowReach;
			}
			base.Projectile.rotation -= 0.23561947f;
			base.Projectile.scale = 1f + ThrowScaleRatio() * 0.5f;
			if (Math.Abs(ThrowCompletion - 0.75f) <= 0.005f)
			{
				direction = base.Projectile.Center - Owner.Center;
			}
			if (ThrowCompletion > 0.75f)
			{
				base.Projectile.Center = Owner.Center + direction * ThrowRatio();
			}
			if (!OwnerCanShoot && Combo == 2f && ThrowCompletion >= 0.25f && ThrowCompletion < 0.75f && ChanceMissed == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Owner.velocity - base.Projectile.velocity.SafeNormalize(Vector2.Zero), Color.White, Color.OrangeRed, Main.rand.NextFloat(1f, 2f), 10 + Main.rand.Next(10), 0.1f, 3f));
				Main.LocalPlayer.SetScreenshake(3f);
				if (Owner.whoAmI == Main.myPlayer)
				{
					float rotationOffset = (float)Math.PI * 2f * Main.rand.NextFloat();
					for (int i = 0; i < 3; i++)
					{
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + ((float)Math.PI * 2f * ((float)i / 3f) + rotationOffset).ToRotationVector2() * 30f, ((float)Math.PI * 2f * ((float)i / 3f) + rotationOffset).ToRotationVector2() * 20f, ModContent.ProjectileType<EonBolt>(), (int)(ArkoftheCosmos.SnapBoltsDamageMultiplier * (float)base.Projectile.damage), 0f, Owner.whoAmI, 0.55f, (float)Math.PI / 20f).timeLeft = 100;
					}
					for (int j = 0; j < Main.maxNPCs; j++)
					{
						base.Projectile.localNPCImmunity[j] = 0;
					}
				}
				Combo = 3f;
				direction = base.Projectile.Center - Owner.Center;
				base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2();
				base.Projectile.timeLeft = (int)SnapEndTime;
				base.Projectile.localNPCHitCooldown = (int)SnapEndTime;
			}
			else if (!OwnerCanShoot && Combo == 2f && ChanceMissed == 0f)
			{
				ChanceMissed = 1f;
			}
			if (Combo == 3f)
			{
				float curveDownGently = MathHelper.Lerp(1f, 0.8f, 1f - (float)Math.Sqrt(1f - (float)Math.Pow(SnapEndCompletion, 2.0)));
				base.Projectile.Center = Owner.Center + direction * curveDownGently;
				base.Projectile.scale = 1.5f;
				float orientateProperly = (float)Math.Sqrt(1f - (float)Math.Pow(MathHelper.Clamp(SnapEndCompletion + 0.2f, 0f, 1f) - 1f, 2.0));
				float extraRotations = ((direction.ToRotation() + (float)Math.PI / 4f > base.Projectile.velocity.ToRotation()) ? ((float)Math.PI * -2f) : 0f);
				base.Projectile.rotation = MathHelper.Lerp(base.Projectile.velocity.ToRotation(), direction.ToRotation() + extraRotations, orientateProperly);
				if (ChanceMissed == 0f && Owner.controlUseTile && ThrowCompletion > 0.99f)
				{
					ArkoftheCosmos sword = Owner.HeldItem.ModItem as ArkoftheCosmos;
					Projectile parrier = Main.projectile.FirstOrDefault((Projectile p) => p.active && p.owner == Owner.whoAmI && p.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>(), null);
					if (sword != null && parrier == null)
					{
						Projectile.NewProjectile(Owner.GetSource_ItemUse(sword.Item), Owner.Center, Owner.DirectionTo(base.Projectile.Center), ModContent.ProjectileType<ArkoftheCosmosParryHoldout>(), 0, 0f, Owner.whoAmI);
					}
				}
			}
			DoParticleEffects(swirlSwing: false);
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
	}

	public void DoParticleEffects(bool swirlSwing)
	{
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		if (swirlSwing)
		{
			base.Projectile.scale = 1.6f + (float)Math.Sin(SwirlRatio() * (float)Math.PI) * 1f + Charge / 10f * 0.05f;
			Color currentColor = Color.Chocolate * (MathHelper.Clamp((float)Math.Sin((SwirlRatio() - 0.2f) * (float)Math.PI), 0f, 1f) * 0.8f);
			if (smear == null)
			{
				smear = new CircularSmearSmokeyVFX(Owner.Center, currentColor, base.Projectile.rotation, base.Projectile.scale * 2.4f);
				GeneralParticleHandler.SpawnParticle(smear);
			}
			else
			{
				smear.Rotation = base.Projectile.rotation + (float)Math.PI / 4f + ((Owner.direction < 0) ? ((float)Math.PI) : 0f);
				smear.Time = 0;
				smear.Position = Owner.Center;
				smear.Scale = MathHelper.Lerp(2.6f, 3.5f, (base.Projectile.scale - 1.6f) / 1f);
				smear.Color = currentColor;
			}
			if (Main.rand.NextBool())
			{
				float maxDistance = base.Projectile.scale * 78f;
				Vector2 distance = Main.rand.NextVector2Circular(maxDistance, maxDistance);
				Vector2 angularVelocity = distance.RotatedBy((float)Math.PI / 2f * (float)Owner.direction).SafeNormalize(Vector2.Zero) * 2f * (1f + ((Vector2)(ref distance)).Length() / 15f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(Owner.Center + distance, Owner.velocity + angularVelocity, Main.rand.NextBool(3) ? Color.Turquoise : Color.Coral, currentColor, 1f + 1f * (((Vector2)(ref distance)).Length() / maxDistance), 10, 0.05f, 3f));
			}
			float Opacity = MathHelper.Clamp(MathHelper.Clamp((float)Math.Sin((SwirlRatio() - 0.2f) * (float)Math.PI), 0f, 1f) * 2f, 0f, 1f) * 0.25f;
			float scaleFactor = MathHelper.Clamp(MathHelper.Clamp((float)Math.Sin((SwirlRatio() - 0.2f) * (float)Math.PI), 0f, 1f), 0f, 1f);
			if (!Main.rand.NextBool())
			{
				return;
			}
			for (float i = 0f; i <= 1f; i += 0.5f)
			{
				Vector2 smokepos = Owner.Center + base.Projectile.rotation.ToRotationVector2() * (30f + 50f * i) * base.Projectile.scale + base.Projectile.rotation.ToRotationVector2().RotatedBy(-1.5707963705062866) * 30f * scaleFactor * Main.rand.NextFloat();
				Vector2 smokespeed = base.Projectile.rotation.ToRotationVector2().RotatedBy(-(float)Math.PI / 2f * (float)Owner.direction) * 20f * scaleFactor + Owner.velocity;
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(smokepos, smokespeed, Color.Lerp(Color.DodgerBlue, Color.MediumVioletRed, i), 6 + Main.rand.Next(5), scaleFactor * Main.rand.NextFloat(2.8f, 3.1f), Opacity + Main.rand.NextFloat(0f, 0.2f), 0f, glowing: false, 0f, required: true));
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(smokepos, smokespeed, Main.rand.NextBool(5) ? Color.Gold : Color.Chocolate, 5, scaleFactor * Main.rand.NextFloat(2f, 2.4f), Opacity * 2.5f, 0f, glowing: true, 0.004f, required: true));
				}
			}
			return;
		}
		Color smearColor = Main.hslToRgb((SwingTimer - MaxSwingTime * 0.5f) / (MaxSwingTime * 0.5f) * 0.15f, 1f, 0.8f);
		float opacity = ((Combo == 3f) ? ((float)Math.Sin(SnapEndCompletion * ((float)Math.PI / 2f) + (float)Math.PI / 2f)) : ((float)Math.Sin(ThrowCompletion * (float)Math.PI))) * 0.5f;
		if (smear == null)
		{
			if (Charge <= 0f)
			{
				smear = new TrientCircularSmear(base.Projectile.Center, smearColor * opacity, base.Projectile.rotation, base.Projectile.scale * 1.7f);
			}
			else
			{
				smear = new CircularSmearSmokeyVFX(base.Projectile.Center, smearColor * opacity, base.Projectile.rotation, base.Projectile.scale * 1.7f);
			}
			GeneralParticleHandler.SpawnParticle(smear);
		}
		else
		{
			smear.Rotation = base.Projectile.rotation - (float)Math.PI * 7f / 8f;
			smear.Time = 0;
			smear.Position = base.Projectile.Center;
			smear.Scale = base.Projectile.scale * 1.65f;
			smear.Color = smearColor * opacity;
		}
		if (Combo != 2f)
		{
			return;
		}
		if (Main.rand.NextBool())
		{
			float maxDistance2 = base.Projectile.scale * 78f;
			Vector2 distance2 = Main.rand.NextVector2Circular(maxDistance2, maxDistance2);
			Vector2 angularVelocity2 = distance2.RotatedBy(-1.5707963705062866).SafeNormalize(Vector2.Zero) * 2f * (1f + ((Vector2)(ref distance2)).Length() / 15f);
			Color glitterColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + distance2, Owner.velocity + angularVelocity2, Color.White, glitterColor, 1f + 1f * (((Vector2)(ref distance2)).Length() / maxDistance2), 10, 0.05f, 3f));
		}
		opacity = 0.25f;
		float scaleFactor2 = 0.7f;
		if (!Main.rand.NextBool())
		{
			return;
		}
		for (float i2 = 0.5f; i2 <= 1f; i2 += 0.5f)
		{
			Vector2 smokepos2 = base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * (60f * i2) * base.Projectile.scale + base.Projectile.rotation.ToRotationVector2().RotatedBy(-1.5707963705062866) * 30f * scaleFactor2 * Main.rand.NextFloat();
			Vector2 smokespeed2 = base.Projectile.rotation.ToRotationVector2().RotatedBy(1.5707963705062866) * 20f * scaleFactor2 + Owner.velocity;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(smokepos2, smokespeed2, Color.Lerp(Color.DodgerBlue, Color.MediumVioletRed, i2), 10 + Main.rand.Next(5), scaleFactor2 * Main.rand.NextFloat(2.8f, 3.1f), opacity + Main.rand.NextFloat(0f, 0.2f), 0f, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(smokepos2, smokespeed2, Main.rand.NextBool(5) ? Color.Gold : Color.Chocolate, 7, scaleFactor2 * Main.rand.NextFloat(2f, 2.4f), opacity * 2.5f, 0f, glowing: true, 0.004f, required: true));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Combo == 3f)
		{
			modifiers.SourceDamage *= ArkoftheCosmos.SnapDamageMultiplier;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Vector2 particleSpeed = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.One).RotatedByRandom(0.6283185482025146) * Main.rand.NextFloat(3.6f, 8f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(target.Center, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.OrangeRed, 60, 2f, 2.5f, 3f, 0.06f));
		}
		if (Combo == 3f)
		{
			SoundStyle style = CommonCalamitySounds.ScissorGuillotineSnapSound with
			{
				Volume = CommonCalamitySounds.ScissorGuillotineSnapSound.Volume * 1.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (Combo == 3f)
		{
			Main.LocalPlayer.SetScreenshake(3f);
			SoundEngine.PlaySound(in SoundID.Item84, base.Projectile.Center);
			Vector2 sliceDirection = direction.SafeNormalize(Vector2.One) * 40f;
			GeneralParticleHandler.SpawnParticle(new LineVFX(base.Projectile.Center - sliceDirection, sliceDirection * 2f, 0.2f, Color.Orange * 0.7f, concave: false, telegraph: false, 250f)
			{
				Lifetime = 10
			});
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!Thrown)
		{
			if (Charge > 0f)
			{
				DrawSwungScissors(lightColor);
			}
			else
			{
				DrawSingleSwungScissorBlade(lightColor);
			}
		}
		else if (Charge > 0f)
		{
			DrawThrownScissors(lightColor);
		}
		else
		{
			DrawSingleThrownScissorBlade(lightColor);
		}
		return false;
	}

	public void DrawSingleSwungScissorBlade(Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>((Combo == 0f) ? "CalamityMod/Projectiles/Melee/SunderingScissorsRight" : "CalamityMod/Projectiles/Melee/SunderingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>((Combo == 0f) ? "CalamityMod/Projectiles/Melee/SunderingScissorsRightGlow" : "CalamityMod/Projectiles/Melee/SunderingScissorsLeftGlow", (AssetRequestMode)2).Value;
		bool flipped = Owner.direction < 0;
		SpriteEffects flip = (SpriteEffects)(flipped ? 1 : 0);
		float extraAngle = ((Owner.direction < 0) ? ((float)Math.PI / 2f) : 0f);
		float drawAngle = base.Projectile.rotation;
		float angleShift = (float)Math.PI / 4f;
		float drawRotation = base.Projectile.rotation + angleShift + extraAngle;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(flipped ? ((float)sword.Width) : 0f, (float)sword.Height);
		Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && SwingTimer > (float)ProjectileID.Sets.TrailCacheLength[base.Type] && Combo == 0f)
		{
			for (int i = 1; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = Main.hslToRgb((float)i / (float)base.Projectile.oldRot.Length * 0.1f, 1f, 0.6f + ((Charge > 0f) ? 0.3f : 0f));
				float afterimageRotation = base.Projectile.oldRot[i] + angleShift + extraAngle;
				Main.spriteBatch.Draw(glowmask, drawOffset, (Rectangle?)null, color * 0.05f, afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip, 0f);
			}
		}
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(glowmask, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, flip);
		if (SwingCompletion > 0.5f && Combo == 0f)
		{
			Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Sin(SwingCompletion * (float)Math.PI);
			float rotation = (-(float)Math.PI / 8f + (float)Math.PI / 8f * SwingCompletion + ((Combo == 1f) ? ((float)Math.PI / 4f) : 0f)) * (float)SwingDirection;
			Color smearColor = Main.hslToRgb((SwingTimer - MaxSwingTime * 0.5f) / (MaxSwingTime * 0.5f) * 0.15f + ((Combo == 1f) ? 0.85f : 0f), 1f, 0.6f);
			Main.EntitySpriteDraw(smear, Owner.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, base.Projectile.velocity.ToRotation() + (float)Math.PI + rotation, smear.Size() / 2f, base.Projectile.scale * 2.3f, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
	}

	public void DrawSwungScissors(Color lightColor)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D frontBlade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D frontBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeftGlow", (AssetRequestMode)2).Value;
		Texture2D backBlade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D backBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRightGlow", (AssetRequestMode)2).Value;
		bool flipped = Owner.direction < 0;
		SpriteEffects flip = (SpriteEffects)(flipped ? 1 : 0);
		float extraAngle = (flipped ? ((float)Math.PI / 2f) : 0f);
		float drawAngle = base.Projectile.rotation;
		float angleShift = (float)Math.PI / 4f;
		float drawRotation = base.Projectile.rotation + angleShift + extraAngle;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(flipped ? ((float)frontBlade.Width) : 0f, (float)frontBlade.Height);
		Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
		Vector2 backScissorOrigin = default(Vector2);
		((Vector2)(ref backScissorOrigin))._002Ector(flipped ? 90f : 44f, 86f);
		Vector2 backScissorDrawPosition = Owner.Center + drawAngle.ToRotationVector2() * 10f + (drawAngle.ToRotationVector2() * 56f + (drawAngle - (float)Math.PI / 2f).ToRotationVector2() * 11f * (float)Owner.direction) * base.Projectile.scale - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && SwingTimer > (float)ProjectileID.Sets.TrailCacheLength[base.Type])
		{
			Texture2D afterimage = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsGlow", (AssetRequestMode)2).Value;
			for (int i = 1; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = Main.hslToRgb((float)i / (float)base.Projectile.oldRot.Length * 0.1f, 1f, 0.6f + ((Charge > 0f) ? 0.3f : 0f));
				float afterimageRotation = base.Projectile.oldRot[i] + angleShift + extraAngle;
				Main.EntitySpriteDraw(afterimage, drawOffset, null, color * 0.15f, afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip);
			}
		}
		Main.EntitySpriteDraw(backBlade, backScissorDrawPosition, null, lightColor, drawRotation, backScissorOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(backBladeGlow, backScissorDrawPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, backScissorOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(frontBlade, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(frontBladeGlow, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, flip);
		if (SwingCompletion > 0.5f && Combo == 0f)
		{
			Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Sin(SwingCompletion * (float)Math.PI);
			float rotation = (-(float)Math.PI / 8f + (float)Math.PI / 8f * SwingCompletion + ((Combo == 1f) ? ((float)Math.PI / 4f) : 0f)) * (float)SwingDirection;
			Color smearColor = Main.hslToRgb((SwingTimer - MaxSwingTime * 0.5f) / (MaxSwingTime * 0.5f) * 0.15f + ((Combo == 1f) ? 0.85f : 0f), 1f, 0.6f);
			Main.EntitySpriteDraw(smear, Owner.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, base.Projectile.velocity.ToRotation() + (float)Math.PI + rotation, smear.Size() / 2f, base.Projectile.scale * 2.3f, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
	}

	public void DrawSingleThrownScissorBlade(Color lightColor)
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeftGlow", (AssetRequestMode)2).Value;
		if (Combo == 3f)
		{
			Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRight", (AssetRequestMode)2).Value;
			Texture2D thrownGlowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRightGlow", (AssetRequestMode)2).Value;
			Vector2 drawPos2 = Vector2.SmoothStep(Owner.Center, base.Projectile.Center, MathHelper.Clamp(SnapEndCompletion + 0.25f, 0f, 1f));
			float drawRotation2 = direction.ToRotation() + (float)Math.PI / 4f;
			Vector2 drawOrigin2 = default(Vector2);
			((Vector2)(ref drawOrigin2))._002Ector(44f, 86f);
			Main.EntitySpriteDraw(value2, drawPos2 - Main.screenPosition, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(thrownGlowmask, drawPos2 - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		}
		Vector2 drawPos3 = base.Projectile.Center;
		float drawRotation3 = base.Projectile.rotation + (float)Math.PI / 4f;
		Vector2 drawOrigin3 = default(Vector2);
		((Vector2)(ref drawOrigin3))._002Ector(32f, 86f);
		Main.EntitySpriteDraw(value, drawPos3 - Main.screenPosition, null, lightColor, drawRotation3, drawOrigin3, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowmask, drawPos3 - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation3, drawOrigin3, base.Projectile.scale, (SpriteEffects)0);
	}

	public void DrawThrownScissors(Color lightColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D frontBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsLeftGlow", (AssetRequestMode)2).Value;
		Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D backBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SunderingScissorsRightGlow", (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(32f, 86f);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		Vector2 drawOrigin2 = default(Vector2);
		((Vector2)(ref drawOrigin2))._002Ector(44f, 86f);
		float drawRotation2 = base.Projectile.rotation + MathHelper.Lerp((float)Math.PI / 4f, (float)Math.PI * 133f / 200f, MathHelper.Clamp(ThrowCompletion * 2f, 0f, 1f));
		if (Combo == 3f)
		{
			drawRotation2 = base.Projectile.rotation + MathHelper.Lerp((float)Math.PI * 133f / 200f, (float)Math.PI / 4f, MathHelper.Clamp(SnapEndCompletion + 0.5f, 0f, 1f));
		}
		Main.EntitySpriteDraw(value2, drawPos - Main.screenPosition, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(backBladeGlow, drawPos - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPos - Main.screenPosition, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(frontBladeGlow, drawPos - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
		writer.Write(ChanceMissed);
		writer.Write(ThrowReach);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
		ChanceMissed = reader.ReadSingle();
		ThrowReach = reader.ReadSingle();
	}

	public ArkoftheCosmosSwungBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		SwingWidth = (float)Math.PI * 3f / 4f;
		anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpOut, 0f, 0f, 0.15f);
		thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.1f, 0.15f, 0.85f, 3);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0.2f);
		startup = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineIn, 0f, 0f, 0.25f);
		swing = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.1f, 0.25f, 0.75f);
		shoot = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0f, 1f, -0.2f, 3);
		remain = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.35f, 0.8f, 0f);
		retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineIn, 0.75f, 1f, -1f);
		sizeCurve = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, 1f);
		base._002Ector();
	}
}
