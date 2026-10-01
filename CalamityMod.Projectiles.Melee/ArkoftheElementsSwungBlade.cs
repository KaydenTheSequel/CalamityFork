using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
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

public class ArkoftheElementsSwungBlade : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private const float MaxSwingTime = 35f;

	private float SwingWidth;

	private const float MaxThrowTime = 80f;

	private const float ThrowReachMax = 500f;

	private const float ThrowReachMin = 200f;

	public float ThrowReach;

	private const float SnapWindowStart = 0.25f;

	private const float SnapWindowEnd = 0.75f;

	public CalamityUtils.CurveSegment anticipation;

	public CalamityUtils.CurveSegment thrust;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment shoot;

	public CalamityUtils.CurveSegment remain;

	public CalamityUtils.CurveSegment goback;

	public CalamityUtils.CurveSegment sizeCurve;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/RendingScissorsRight";

	public ref float Combo => ref base.Projectile.ai[0];

	public ref float Charge => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

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

	public Vector2 DistanceFromPlayer
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return direction * 30f;
		}
	}

	public float SwingTimer => 35f - (float)base.Projectile.timeLeft;

	public float SwingCompletion => SwingTimer / 35f;

	public ref float HasFired => ref base.Projectile.localAI[0];

	private bool OwnerCanShoot
	{
		get
		{
			if (!Owner.CantUseHoldout())
			{
				return Owner.HeldItem.type == ModContent.ItemType<ArkoftheElements>();
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

	public float ThrowTimer => 80f - (float)base.Projectile.timeLeft;

	public float ThrowCompletion => ThrowTimer / 80f;

	public float SnapEndTime => 20f;

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
		base.Projectile.localNPCHitCooldown = (Thrown ? 10 : 35);
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
		float bladeLength = 142f * base.Projectile.scale;
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

	internal float ThrowRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ThrowCompletion, shoot, remain, goback);
	}

	internal float ThrowScaleRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ThrowCompletion, sizeCurve);
	}

	public override void AI()
	{
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = (Thrown ? 80 : 35);
			SoundEngine.PlaySound((Charge > 0f || Thrown) ? CommonCalamitySounds.LouderPhantomPhoenix : SoundID.Item71, base.Projectile.Center);
			direction = base.Projectile.velocity;
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			if (Thrown)
			{
				Vector2 val = Owner.Center - Owner.Calamity().mouseWorld;
				ThrowReach = MathHelper.Clamp(((Vector2)(ref val)).Length(), 200f, 500f);
			}
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		if (!Thrown)
		{
			base.Projectile.Center = Owner.Center + DistanceFromPlayer;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.Lerp(SwingWidth / 2f * (float)SwingDirection, (0f - SwingWidth) / 2f * (float)SwingDirection, SwingRatio()) - ((Combo == 1f) ? ((float)Math.PI / 4f) : 0f);
			base.Projectile.scale = 1.2f + (float)Math.Sin(SwingRatio() * (float)Math.PI) * 0.6f + Charge / 10f * 0.2f;
		}
		else
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 90f * base.Projectile.scale + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 20f * base.Projectile.scale, base.Projectile.rotation.ToRotationVector2() * 7f, Color.White, Color.OrangeRed, Main.rand.NextFloat(1f, 2f), 10 + Main.rand.Next(10), 0.1f, 3f, Main.rand.NextFloat(0f, 0.01f)));
			if (Math.Abs(ThrowCompletion - 0.25f + 0.1f) <= 0.005f && ChanceMissed == 0f && Main.myPlayer == Owner.whoAmI)
			{
				GeneralParticleHandler.SpawnParticle(new PulseRing(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, 0.05f, 1.8f, 8));
				SoundEngine.PlaySound(in SoundID.Item4);
			}
			base.Projectile.Center = Owner.Center + direction * ThrowRatio() * ThrowReach;
			base.Projectile.rotation -= 0.23561947f;
			base.Projectile.scale = 1f + ThrowScaleRatio() * 0.5f;
			if (!OwnerCanShoot && Combo == 2f && ThrowCompletion >= 0.15f && ThrowCompletion < 0.75f && ChanceMissed == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Owner.velocity - base.Projectile.velocity.SafeNormalize(Vector2.Zero), Color.White, Color.OrangeRed, Main.rand.NextFloat(1f, 2f), 10 + Main.rand.Next(10), 0.1f, 3f));
				Main.LocalPlayer.SetScreenshake(3f);
				if (Owner.whoAmI == Main.myPlayer)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						base.Projectile.localNPCImmunity[i] = 0;
					}
				}
				Combo = 3f;
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
				base.Projectile.Center = Owner.Center + direction * ThrowReach * curveDownGently;
				base.Projectile.scale = 1.5f;
				float orientateProperly = (float)Math.Sqrt(1f - (float)Math.Pow(MathHelper.Clamp(SnapEndCompletion + 0.2f, 0f, 1f) - 1f, 2.0));
				float extraRotations = ((direction.ToRotation() + (float)Math.PI / 4f > base.Projectile.velocity.ToRotation()) ? ((float)Math.PI * -2f) : 0f);
				base.Projectile.rotation = MathHelper.Lerp(base.Projectile.velocity.ToRotation(), direction.ToRotation() + (float)Math.PI / 20f + extraRotations, orientateProperly);
			}
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

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Combo == 3f)
		{
			modifiers.SourceDamage *= ArkoftheElements.snapDamageMultiplier;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (Combo == 3f)
		{
			Main.LocalPlayer.SetScreenshake(3f);
			SoundEngine.PlaySound(in SoundID.Item84, base.Projectile.Center);
			Vector2 sliceDirection = direction * 40f;
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
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>((Combo == 0f) ? "CalamityMod/Projectiles/Melee/RendingScissorsRight" : "CalamityMod/Projectiles/Melee/RendingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>((Combo == 0f) ? "CalamityMod/Projectiles/Melee/RendingScissorsRightGlow" : "CalamityMod/Projectiles/Melee/RendingScissorsLeftGlow", (AssetRequestMode)2).Value;
		bool flipped = Owner.direction < 0;
		SpriteEffects flip = (SpriteEffects)(flipped ? 1 : 0);
		float extraAngle = ((Owner.direction < 0) ? ((float)Math.PI / 2f) : 0f);
		float drawAngle = base.Projectile.rotation;
		float angleShift = ((Combo == 0f) ? ((float)Math.PI / 4f) : ((float)Math.PI / 2f));
		float drawRotation = base.Projectile.rotation + angleShift + extraAngle;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((Combo == 1f) ? ((float)sword.Width / 2f) : (flipped ? ((float)sword.Width) : 0f), (float)sword.Height);
		Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && SwingTimer > (float)ProjectileID.Sets.TrailCacheLength[base.Type])
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = Main.hslToRgb((float)i / (float)base.Projectile.oldRot.Length * 0.1f, 1f, 0.6f + ((Charge > 0f) ? 0.3f : 0f));
				float afterimageRotation = base.Projectile.oldRot[i] + angleShift + extraAngle;
				Main.spriteBatch.Draw(glowmask, drawOffset, (Rectangle?)null, color * 0.15f, afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip, 0f);
			}
		}
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(glowmask, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, flip);
		if (SwingCompletion > 0.5f)
		{
			Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Sin(SwingCompletion * (float)Math.PI);
			float rotation = (-(float)Math.PI / 8f + (float)Math.PI / 8f * SwingCompletion + ((Combo == 1f) ? ((float)Math.PI / 4f) : 0f)) * (float)SwingDirection;
			Color smearColor = Main.hslToRgb((SwingTimer - 17.5f) / 17.5f * 0.15f + ((Combo == 1f) ? 0.85f : 0f), 1f, 0.6f);
			Main.EntitySpriteDraw(smear, Owner.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, base.Projectile.velocity.ToRotation() + (float)Math.PI + rotation, smear.Size() / 2f, base.Projectile.scale * 2.3f, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
	}

	public void DrawSwungScissors(Color lightColor)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D frontBlade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D frontBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRightGlow", (AssetRequestMode)2).Value;
		Texture2D backBlade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D backBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeftGlow", (AssetRequestMode)2).Value;
		bool flipped = Owner.direction < 0;
		SpriteEffects flip = (SpriteEffects)(flipped ? 1 : 0);
		float extraAngle = (flipped ? ((float)Math.PI / 2f) : 0f);
		float drawAngle = base.Projectile.rotation;
		float angleShift = ((Combo == 0f || !flipped) ? ((float)Math.PI / 4f) : ((float)Math.PI * 3f / 4f));
		float drawRotation = base.Projectile.rotation + angleShift + extraAngle;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(flipped ? ((float)frontBlade.Width) : 0f, (float)frontBlade.Height);
		Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
		float functionalDrawAngle = drawAngle + (((Combo == 1f) & flipped) ? ((float)Math.PI / 2f) : 0f);
		Vector2 backScissorOrigin = default(Vector2);
		((Vector2)(ref backScissorOrigin))._002Ector(flipped ? 11f : 20f, 109f);
		Vector2 backScissorDrawPosition = Owner.Center + drawAngle.ToRotationVector2() * 10f + functionalDrawAngle.ToRotationVector2() * 70f * base.Projectile.scale - Main.screenPosition;
		float backScissorRotation = drawRotation + ((Combo != 1f) ? 0f : ((!flipped) ? ((float)Math.PI * 3f / 16f) : ((float)Math.PI * -3f / 16f)));
		if (CalamityClientConfig.Instance.Afterimages && SwingTimer > (float)ProjectileID.Sets.TrailCacheLength[base.Type])
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = Main.hslToRgb((float)i / (float)base.Projectile.oldRot.Length * 0.1f, 1f, 0.6f + ((Charge > 0f) ? 0.3f : 0f));
				float afterimageRotation = base.Projectile.oldRot[i] + angleShift + extraAngle;
				float afterimageBackRotation = afterimageRotation + (backScissorRotation - drawRotation);
				Main.EntitySpriteDraw(backBladeGlow, backScissorDrawPosition, null, color * 0.15f, afterimageBackRotation, backScissorOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip);
				Main.EntitySpriteDraw(frontBladeGlow, drawOffset, null, color * 0.15f, afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip);
			}
		}
		Main.EntitySpriteDraw(backBlade, backScissorDrawPosition, null, lightColor, backScissorRotation, backScissorOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(backBladeGlow, backScissorDrawPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), backScissorRotation, backScissorOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(frontBlade, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(frontBladeGlow, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, flip);
		if (SwingCompletion > 0.5f)
		{
			Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Sin(SwingCompletion * (float)Math.PI);
			float rotation = (-(float)Math.PI / 8f + (float)Math.PI / 8f * SwingCompletion + ((Combo == 1f) ? ((float)Math.PI / 4f) : 0f)) * (float)SwingDirection;
			Color smearColor = Main.hslToRgb((SwingTimer - 17.5f) / 17.5f * 0.15f + ((Combo == 1f) ? 0.85f : 0f), 1f, 0.6f);
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
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRightGlow", (AssetRequestMode)2).Value;
		if (Combo == 3f)
		{
			Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeft", (AssetRequestMode)2).Value;
			Texture2D thrownGlowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeftGlow", (AssetRequestMode)2).Value;
			Vector2 drawPos2 = Vector2.SmoothStep(Owner.Center, base.Projectile.Center, MathHelper.Clamp(SnapEndCompletion + 0.25f, 0f, 1f));
			Vector2 drawOrigin2 = default(Vector2);
			((Vector2)(ref drawOrigin2))._002Ector(22f, 109f);
			float drawRotation2 = direction.ToRotation() + (float)Math.PI / 2f;
			Main.EntitySpriteDraw(value2, drawPos2 - Main.screenPosition, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(thrownGlowmask, drawPos2 - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		}
		Vector2 drawPos3 = base.Projectile.Center;
		Vector2 drawOrigin3 = default(Vector2);
		((Vector2)(ref drawOrigin3))._002Ector(51f, 86f);
		float drawRotation3 = base.Projectile.rotation + (float)Math.PI / 4f;
		Main.EntitySpriteDraw(value, drawPos3 - Main.screenPosition, null, lightColor, drawRotation3, drawOrigin3, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowmask, drawPos3 - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation3, drawOrigin3, base.Projectile.scale, (SpriteEffects)0);
		Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		float opacity = ((Combo == 3f) ? ((float)Math.Sin(SnapEndCompletion * ((float)Math.PI / 2f) + (float)Math.PI / 2f)) : ((float)Math.Sin(ThrowCompletion * (float)Math.PI)));
		float rotation = drawRotation3 + (float)Math.PI * 3f / 4f;
		Color smearColor = Main.hslToRgb((SwingTimer - 17.5f) / 17.5f * 0.15f, 1f, 0.6f);
		Main.EntitySpriteDraw(smear, base.Projectile.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, rotation, smear.Size() / 2f, base.Projectile.scale * 1.4f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
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
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D frontBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRightGlow", (AssetRequestMode)2).Value;
		Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D backBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeftGlow", (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(51f, 86f);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		Vector2 drawOrigin2 = default(Vector2);
		((Vector2)(ref drawOrigin2))._002Ector(22f, 109f);
		float drawRotation2 = base.Projectile.rotation + MathHelper.Lerp(0f, -0.2591814f, MathHelper.Clamp(ThrowCompletion * 2f, 0f, 1f));
		if (Combo == 3f)
		{
			drawRotation2 = base.Projectile.rotation + MathHelper.Lerp(-0.2591814f, (float)Math.PI * 17f / 40f, MathHelper.Clamp(SnapEndCompletion + 0.5f, 0f, 1f));
		}
		Main.EntitySpriteDraw(value2, drawPos - Main.screenPosition, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(backBladeGlow, drawPos - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPos - Main.screenPosition, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(frontBladeGlow, drawPos - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		float opacity = ((Combo == 3f) ? ((float)Math.Sin(SnapEndCompletion * ((float)Math.PI / 2f) + (float)Math.PI / 2f)) : ((float)Math.Sin(ThrowCompletion * (float)Math.PI)));
		float rotation = drawRotation + (float)Math.PI * 3f / 4f;
		Color smearColor = Main.hslToRgb((SwingTimer - 17.5f) / 17.5f * 0.15f, 1f, 0.6f);
		Main.EntitySpriteDraw(smear, base.Projectile.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, rotation, smear.Size() / 2f, base.Projectile.scale * 1.4f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
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

	public ArkoftheElementsSwungBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		SwingWidth = (float)Math.PI * 3f / 4f;
		anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpOut, 0f, 0f, 0.15f);
		thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.1f, 0.15f, 0.85f, 3);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0.2f);
		shoot = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircOut, 0f, 0f, 1f);
		remain = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.25f, 1f, 0f);
		goback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.75f, 1f, -1f);
		sizeCurve = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, 1f);
		base._002Ector();
	}
}
