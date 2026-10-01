using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PhoenixsPride : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float throwOutTime = 90f;

	public const float throwOutDistance = 440f;

	public static float snapPoint = 0.45f;

	public static float retractionPoint = 0.6f;

	public float OverEmpowerment;

	public const float maxEmpowerment = 600f;

	public float AngleReset;

	public bool CanDirectFire;

	public Particle smear;

	public CalamityUtils.CurveSegment launch;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment retract;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxiaExtra2";

	public ref float CurrentState => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public float snapTimer
	{
		get
		{
			if (!(throwTimer / 90f < snapPoint))
			{
				return (throwTimer / 90f - snapPoint) / (1f - snapPoint);
			}
			return 0f;
		}
	}

	public float retractionTimer
	{
		get
		{
			if (!(throwTimer / 90f < retractionPoint))
			{
				return (throwTimer / 90f - retractionPoint) / (1f - retractionPoint);
			}
			return 0f;
		}
	}

	public ref float Empowerment => ref base.Projectile.ai[1];

	public ref float hasMadeSound => ref base.Projectile.localAI[0];

	public ref float hasMadeChargeSound => ref base.Projectile.localAI[1];

	public float throwTimer => 90f - (float)base.Projectile.timeLeft;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 74);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = FourSeasonsGalaxia.PhoenixAttunement_LocalIFrames;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 145f * base.Projectile.scale;
		float bladeWidth = 25f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	internal float ThrowCurve()
	{
		return CalamityUtils.PiecewiseAnimation((90f - (float)base.Projectile.timeLeft) / 90f, launch, hold, retract);
	}

	public override void AI()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_093d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_082e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			base.Projectile.velocity = Vector2.Zero;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			initialized = true;
		}
		base.Projectile.rotation = direction.ToRotation();
		if (Owner.CantUseHoldout())
		{
			if (CurrentState == 2f || (CurrentState == 0f && (double)(Empowerment / 600f) < 0.5))
			{
				SoundEngine.PlaySound(in SoundID.Item77, base.Projectile.Center);
				base.Projectile.Kill();
				return;
			}
			if (CurrentState == 0f)
			{
				CurrentState = 1f;
				Main.LocalPlayer.SetScreenshake(3f);
				SoundEngine.PlaySound(in SoundID.Item80, base.Projectile.Center);
				direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
				for (int i = 0; i <= 8; i++)
				{
					float variation = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f);
					float strength = (float)Math.Sin(variation * 2f + (float)Math.PI / 2f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity + direction.RotatedBy(variation) * (1f + strength) * 2f * Main.rand.NextFloat(7.5f, 20f), Color.Coral, Color.Chocolate, 2f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 2f));
				}
				if (Owner.whoAmI == Main.myPlayer)
				{
					for (int j = 0; j <= 5; j++)
					{
						float angle = direction.ToRotation() + MathHelper.Lerp(-(float)Math.PI / 4f, (float)Math.PI / 4f, (float)j / 5f);
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, angle.ToRotationVector2() * 30f, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.PhoenixAttunement_BoltThrowDamageMultiplier), 0f, Owner.whoAmI, 0.1f, (float)Math.PI / 50f);
					}
				}
			}
		}
		if (CurrentState == 0f)
		{
			if (Owner.whoAmI == Main.myPlayer)
			{
				if (hasMadeChargeSound == 0f && (double)(Empowerment / 600f) >= 0.5)
				{
					hasMadeChargeSound = 1f;
					SoundEngine.PlaySound(in SoundID.Item76);
				}
				float rotation = direction.ToRotation();
				if (rotation > (float)Math.PI * -3f / 4f && rotation < -(float)Math.PI / 4f && hasMadeSound == 1f)
				{
					hasMadeSound = 0f;
				}
				else if (rotation > (float)Math.PI / 4f && rotation < (float)Math.PI * 3f / 4f && hasMadeSound == 0f)
				{
					CanDirectFire = true;
					hasMadeSound = 1f;
					SoundEngine.PlaySound(in SoundID.Item71);
				}
			}
			if ((double)(Empowerment / 600f) >= 0.5)
			{
				Empowerment++;
				if (Main.rand.NextBool(2))
				{
					float Opacity = MathHelper.Clamp((Empowerment / 600f - 0.5f) * 3f, 0f, 1f) * 0.5f;
					float scaleFactor = MathHelper.Clamp((Empowerment / 600f - 0.5f) * 3f, 0f, 1f);
					for (float i2 = 0f; i2 < 2f; i2++)
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + direction * (60f + 20f * i2) * base.Projectile.scale + direction.RotatedBy(-1.5707963705062866) * 30f * scaleFactor * Main.rand.NextFloat(), direction.RotatedBy(-1.5707963705062866) * 20f * scaleFactor + Owner.velocity, Color.Lerp(Color.MidnightBlue, Color.Indigo, i2), 10 + Main.rand.Next(5), scaleFactor * Main.rand.NextFloat(2.8f, 3.1f), Opacity + Main.rand.NextFloat(0f, 0.2f), 0f, glowing: false, 0f, required: true));
					}
				}
				if ((Empowerment + OverEmpowerment) % 30f == 29f && Owner.whoAmI == Main.myPlayer)
				{
					Vector2 shotDirection = Main.rand.NextVector2CircularEdge(15f, 15f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, shotDirection, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.PhoenixAttunement_BoltDamageReduction), 0f, Owner.whoAmI, 0.1f, (float)Math.PI / 50f);
				}
			}
			if ((double)(Empowerment / 600f) >= 0.75)
			{
				Color currentColor = Color.Chocolate * ((Empowerment / 600f - 0.75f) / 0.25f * 0.8f);
				if (smear == null)
				{
					smear = new CircularSmearSmokeyVFX(Owner.Center, currentColor, direction.ToRotation(), base.Projectile.scale * 1.5f);
					GeneralParticleHandler.SpawnParticle(smear);
				}
				else
				{
					smear.Rotation = direction.ToRotation() + (float)Math.PI / 2f;
					smear.Time = 0;
					smear.Position = Owner.Center;
					smear.Scale = base.Projectile.scale * 1.9f;
					smear.Color = currentColor;
				}
				float num = MathHelper.WrapAngle(base.Projectile.rotation) + (float)Math.PI;
				float mouseAngleAdjusted = MathHelper.WrapAngle(Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.One).ToRotation()) + (float)Math.PI;
				float deltaAngleShoot = Math.Abs(MathHelper.WrapAngle(num - mouseAngleAdjusted));
				if (CanDirectFire && deltaAngleShoot < 0.1f)
				{
					if (Owner.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.One) * 15f, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.PhoenixAttunement_BoltDamageReduction), 0f, Owner.whoAmI, 0.1f, (float)Math.PI / 50f);
					}
					CanDirectFire = false;
					AngleReset = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.One).ToRotation();
				}
				if (Main.rand.NextBool())
				{
					float maxDistance = base.Projectile.scale * 1.9f * 78f;
					Vector2 distance = Main.rand.NextVector2Circular(maxDistance, maxDistance);
					Vector2 angularVelocity = distance.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * 2f * (1f + ((Vector2)(ref distance)).Length() / 15f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(Owner.Center + distance, Owner.velocity + angularVelocity, Main.rand.NextBool(3) ? Color.Turquoise : Color.Coral, currentColor, 1f + 1f * (((Vector2)(ref distance)).Length() / maxDistance), 10, 0.05f, 3f));
				}
			}
			base.Projectile.scale = 1f + Empowerment / 600f * 1.5f;
			direction = direction.RotatedBy(MathHelper.Clamp(Empowerment / 600f, 0.4f, 1f) * ((float)Math.PI / 4f) * 0.2f);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			base.Projectile.Center = Owner.Center + direction * base.Projectile.scale * 10f;
			base.Projectile.timeLeft = 91;
			Empowerment++;
			if (Empowerment > 600f)
			{
				Empowerment = 600f;
				OverEmpowerment++;
			}
		}
		if (CurrentState == 1f)
		{
			base.Projectile.Center = Owner.Center + direction * base.Projectile.scale * 10f + direction * 440f * ThrowCurve();
			base.Projectile.scale = (1f + Empowerment / 600f * 1.5f) * MathHelper.Clamp(1f - retractionTimer, 0.3f, 1f);
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentState == 1f && Main.myPlayer == base.Projectile.owner && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<PhoenixsPrideFirewall>()))
		{
			(Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PhoenixsPrideFirewall>(), base.Projectile.damage, 0f, base.Projectile.owner, target.whoAmI).ModProjectile as PhoenixsPrideFirewall).Scale = MathHelper.Clamp((float)target.width / 100f, 0.3f, 1.25f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (CurrentState == 1f)
		{
			modifiers.SourceDamage *= MathHelper.Lerp(1f, FourSeasonsGalaxia.PhoenixAttunement_ThrowDamageBoost, Empowerment / 600f);
		}
		else
		{
			modifiers.SourceDamage *= FourSeasonsGalaxia.PhoenixAttunement_BaseDamageReduction + FourSeasonsGalaxia.PhoenixAttunement_FullChargeDamageBoost * Empowerment / 600f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		smear?.Kill();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxiaExtra2", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)sword.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && CurrentState == 0f && Empowerment / 600f > 0.4f)
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = base.Projectile.GetAlpha(lightColor) * (1f - (float)i / (float)base.Projectile.oldRot.Length);
				float afterimageRotation = base.Projectile.oldRot[i] + (float)Math.PI / 4f;
				Main.spriteBatch.Draw(sword, drawOffset, (Rectangle?)null, color * MathHelper.Lerp(0f, 0.5f, MathHelper.Clamp((Empowerment - 240f) / 360f, 0f, 1f)), afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), (SpriteEffects)0, 0f);
			}
		}
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (CurrentState != 0f)
		{
			_ = retractionTimer;
		}
		if (snapTimer > 0f && retractionTimer <= 0f)
		{
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(MathHelper.Clamp(0.8f - snapTimer, 0f, 1f));
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Color.White);
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		}
		if (CurrentState == 1f && snapTimer > 0f)
		{
			drawChain(snapTimer, retractionTimer);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public void drawChain(float snapProgress, float retractProgress)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMightChain", (AssetRequestMode)2).Value;
		float opacity = (((double)retractProgress < 0.5) ? 1f : ((retractProgress - 0.5f) / 0.5f));
		Vector2 Shake = ((retractProgress > 0f) ? Vector2.Zero : (Vector2.One.RotatedByRandom(6.2831854820251465) * (1f - snapProgress) * 10f));
		int dist = (int)Vector2.Distance(Owner.Center, base.Projectile.Center) / 16;
		Vector2[] Nodes = (Vector2[])(object)new Vector2[dist + 1];
		Nodes[0] = Owner.Center;
		Nodes[dist] = base.Projectile.Center;
		Rectangle frame = default(Rectangle);
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		for (int i = 1; i < dist + 1; i++)
		{
			((Rectangle)(ref frame))._002Ector(0, 18 * (i % 2), 12, 18);
			Vector2 positionAlongLine = Vector2.Lerp(Owner.Center, base.Projectile.Center, (float)i / (float)dist);
			Nodes[i] = positionAlongLine + Shake * (float)Math.Sin((float)i / (float)dist * (float)Math.PI);
			float rotation = (Nodes[i] - Nodes[i - 1]).ToRotation() - (float)Math.PI / 2f;
			float yScale = Vector2.Distance(Nodes[i], Nodes[i - 1]) / (float)frame.Height;
			((Vector2)(ref scale))._002Ector(1f, yScale);
			Color chainLightColor = Lighting.GetColor((int)Nodes[i].X / 16, (int)Nodes[i].Y / 16);
			((Vector2)(ref origin))._002Ector((float)(frame.Width / 2), (float)frame.Height);
			Main.EntitySpriteDraw(chainTex, Nodes[i] - Main.screenPosition, frame, chainLightColor * opacity * 0.7f, rotation, origin, scale, (SpriteEffects)0);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public PhoenixsPride()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		CanDirectFire = true;
		launch = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircOut, 0f, 0f, 1f, 4);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, snapPoint, 1f, 0f);
		retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, retractionPoint, 1f, -1.05f, 3);
		base._002Ector();
	}
}
