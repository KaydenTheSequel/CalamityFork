using System;
using System.Linq;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LightspeedHoldout : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Vector2 innateOffset;

	public Vector2 handPos;

	public float bladeRot;

	private int stabTimer;

	public int stabSoundTimer;

	public bool pressedRight;

	public bool firstSecondaryIteration;

	public int initialDirectionForThisAnim;

	private const float DashPrepTime = 40f;

	private const float DashSpeed = 46f;

	private const float DashDuration = 38f;

	private const float DashAcceleration = 0.9865f;

	private float AltSpinRotation;

	public bool createdSmear;

	public bool gotEnergyThisSwing;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Lightspeed";

	public ref float attackTimer => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public int primaryStabfireRate => 2;

	public ref float DashState => ref base.Projectile.ai[1];

	public ref float DashTimer => ref base.Projectile.ai[2];

	public float LungeProgression
	{
		get
		{
			float duration = 38f;
			return MathHelper.Clamp((38f - DashTimer * 2f) / duration, 0f, 1f);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 120;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 74;
		base.Projectile.height = 94;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
	}

	public void Positioning(Vector2 toMouse)
	{
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		if (DashState == 2f)
		{
			Vector2 dashDirection = base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			Owner.ChangeDir(Math.Sign(dashDirection.X));
			float dashArmRotation = dashDirection.ToRotation();
			float dashCompositeArmRotation = dashArmRotation + 4.712389f;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, dashCompositeArmRotation);
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, 0f);
			bladeRot = 0f;
			handPos = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, dashCompositeArmRotation);
			base.Projectile.Center = handPos;
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.itemTime = (Owner.itemAnimation = 2);
			base.Projectile.rotation = dashArmRotation;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((Owner.direction == 1) ? ((float)Math.PI / 4f) : ((float)Math.PI * 3f / 4f));
			base.Projectile.rotation += 0.15f * (float)Owner.direction;
			Owner.itemRotation = dashArmRotation + (float)Math.PI / 4f;
			if (Owner.direction != 1)
			{
				Owner.itemRotation -= 4.712389f;
			}
			Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
			return;
		}
		Owner.ChangeDir(Math.Sign(toMouse.X));
		float baseArmRotation = toMouse.ToRotation();
		float compositeArmRotation = baseArmRotation + bladeRot - (float)Math.PI / 2f;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, compositeArmRotation);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, 0f);
		Vector2 actualInnateOffset = innateOffset;
		if (Owner.direction == -1)
		{
			actualInnateOffset.X++;
			actualInnateOffset.Y += 2f;
		}
		handPos = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, compositeArmRotation) + actualInnateOffset.RotatedBy(baseArmRotation);
		base.Projectile.velocity = toMouse;
		base.Projectile.rotation = toMouse.ToRotation() + bladeRot;
		base.Projectile.rotation += (float)Math.PI / 4f;
		base.Projectile.rotation += 0.15f * (float)Owner.direction;
		if (Owner.direction == -1)
		{
			base.Projectile.rotation -= 4.712389f;
		}
		base.Projectile.Center = handPos;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = base.Projectile.rotation - (float)Math.PI / 4f;
		if (Owner.direction == -1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		else
		{
			Owner.itemRotation -= (float)Math.PI / 2f;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 0.6f;
		if (!Owner.channel && DashState == 0f)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 toMouse = Owner.MountedCenter.DirectionTo(Owner.ClampedMouseWorld());
		Positioning(toMouse);
		if (DashState == 0f && Owner.altFunctionUse == 2 && Owner.Calamity().mouseRight)
		{
			if (Owner.Calamity().elementalMastery < 100)
			{
				base.Projectile.Kill();
				return;
			}
			DashState = 1f;
			DashTimer = 40f;
			base.Projectile.localAI[0] = Owner.direction;
			Owner.Calamity().elementalMastery = 0;
		}
		if (Owner.altFunctionUse == 0 && DashState == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += Owner.MountedCenter.DirectionTo(Owner.ClampedMouseWorld()) * Main.rand.NextFloat(-5f, 8f);
			UsePrimary(toMouse);
		}
		if (DashState > 0f)
		{
			UseSecondary();
		}
	}

	private void UsePrimary(Vector2 toMouse)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		stabTimer++;
		if (stabTimer % primaryStabfireRate == 0)
		{
			float offset = Main.rand.NextFloat(0f - MathHelper.ToRadians(10f), MathHelper.ToRadians(6f));
			Vector2 stabDir = toMouse.RotatedBy(offset);
			Vector2 stabTip = base.Projectile.Center + stabDir * 62f;
			for (int i = 0; i < 4; i++)
			{
				Vector2 relativePosition = stabTip + Main.rand.NextVector2Circular(18f, 12f);
				Vector2 vel = stabDir * Main.rand.NextFloat(5f, 19f);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(relativePosition, vel, affectedByGravity: false, Main.rand.Next(5, 8), Main.rand.NextFloat(0.02f, 0.07f), Color.Lerp(Color.Aqua, Color.OrangeRed, Main.rand.NextFloat(1f)) * 0.55f, new Vector2(Main.rand.NextFloat(0.475f, 0.535f), Main.rand.NextFloat(1.2f, 1.3f)), quickShrink: true, glow: false));
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + stabDir * 10f + Main.rand.NextVector2Circular(4f, 11f), (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2(), new string("CalamityMod/Items/Weapons/Melee/Lightspeed".AsSpan()), affectedByGravity: false, Main.rand.Next(5, 9), base.Projectile.scale * Main.rand.NextFloat(0.9f, 1.02f), Color.White * Main.rand.NextFloat(0.66f, 0.825f), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 1f, Owner.direction == -1));
			bladeRot = Main.rand.NextFloat(-0.12f, 0.3f) * (float)Owner.direction;
			base.Projectile.scale *= Main.rand.NextFloat(0.75f, 1f);
			Projectile projectile = base.Projectile;
			projectile.Center += stabDir + Main.rand.NextVector2Circular(7f, 1.5f);
			base.Projectile.Opacity = 0.925f;
			Owner.itemRotation += bladeRot * 0.1f;
			if (Main.rand.NextBool())
			{
				Owner.SetCompositeArmFront(enabled: true, Main.rand.NextBool() ? Player.CompositeArmStretchAmount.ThreeQuarters : Player.CompositeArmStretchAmount.Quarter, Owner.itemRotation);
			}
			stabSoundTimer++;
			if (stabSoundTimer % 3 == 0)
			{
				SoundStyle style = SoundID.Item1 with
				{
					Volume = 0.65f,
					MaxInstances = -1
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				stabSoundTimer = 0;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.MountedCenter + toMouse * 20f * (float)Math.Pow(base.Projectile.scale, 4.0), toMouse * 25f, ModContent.ProjectileType<LightspeedM1Hitbox>(), base.Projectile.damage, 0f, base.Projectile.owner);
		}
	}

	private void UseSecondary()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		Vector2 stabOffset = Owner.MountedCenter + Owner.MountedCenter.DirectionTo(Owner.ClampedMouseWorld()) * 30f;
		base.Projectile.Center = stabOffset;
		if (DashState == 1f)
		{
			DashTimer--;
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.itemTime = (Owner.itemAnimation = 2);
			base.Projectile.scale = 0.6f;
			if (!firstSecondaryIteration)
			{
				SoundStyle style = CommonCalamitySounds.MeatySlashSound with
				{
					Volume = 0.4f,
					Pitch = -0.05f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				initialDirectionForThisAnim = Owner.direction;
				firstSecondaryIteration = true;
			}
			if (DashTimer > 6f)
			{
				float duration = 34f;
				float eased = MathF.Pow(MathHelper.Clamp((duration - DashTimer) / duration, 0f, 1f), 1.2f);
				AltSpinRotation = eased * 9.016371f;
				float orbitalAngle = AltSpinRotation * (float)initialDirectionForThisAnim + ((initialDirectionForThisAnim == -1) ? ((float)Math.PI) : 0f);
				float orbitRadius = 40f;
				base.Projectile.Center = Owner.MountedCenter + orbitalAngle.ToRotationVector2() * orbitRadius;
				float tangentAngle = orbitalAngle + ((initialDirectionForThisAnim == 1) ? ((float)Math.PI / 4f) : ((float)Math.PI * 3f / 4f));
				base.Projectile.rotation = tangentAngle;
				float baseArmRotation = (base.Projectile.Center - Owner.MountedCenter).ToRotation();
				float compositeArmRotation = baseArmRotation + ((initialDirectionForThisAnim == 1) ? 4.712389f : (-(float)Math.PI / 2f));
				Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, compositeArmRotation);
				Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, 0f);
				Owner.itemRotation = baseArmRotation;
				if (Owner.direction == -1)
				{
					Owner.itemRotation += (float)Math.PI;
				}
				Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
			}
			if (DashTimer < 32f && DashTimer > 8f)
			{
				if (DashTimer % 4f == 0f)
				{
					GeneralParticleHandler.SpawnParticle(new CritSpark(Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 60f, Utils.RotatedBy(new Vector2(7f, 0f), (double)base.Projectile.rotation, default(Vector2)), Color.Lerp(Color.Aqua, Color.MediumPurple, Main.rand.NextFloat(1f)), Color.White * 0.33f, 1.2f, 12, 0.3f, 1.2f, 0.06f));
				}
				GeneralParticleHandler.SpawnParticle(new CircularSmearVFX(Owner.MountedCenter, Color.Aqua * 0.4f, base.Projectile.rotation, base.Projectile.scale * 1.66f));
			}
			if (DashTimer <= 0f)
			{
				DashState = 2f;
				DashTimer = 38f;
				Vector2 toMouse = Owner.MountedCenter.DirectionTo(Owner.ClampedMouseWorld());
				base.Projectile.velocity = toMouse * 46f;
				Owner.mount?.Dismount(Owner);
				Owner.RemoveAllGrapplingHooks();
				SoundEngine.PlaySound(in Exoblade.DashSound, Owner.MountedCenter);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/OmicronBeam");
				style.Volume = 0.6f;
				style.Pitch = Main.rand.NextFloat(0.2f, 0.25f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Owner.immune = true;
				Owner.immuneNoBlink = true;
				Owner.immuneTime = 40;
				for (int k = 0; k < Owner.hurtCooldowns.Length; k++)
				{
					Owner.hurtCooldowns[k] = Owner.immuneTime;
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.MountedCenter, base.Projectile.velocity, ModContent.ProjectileType<LightspeedDashHitbox>(), base.Projectile.damage * 24, base.Projectile.knockBack * 4f, base.Projectile.owner);
			}
		}
		else if (DashState == 2f)
		{
			DashTimer--;
			Vector2 dashVelocity = base.Projectile.velocity;
			Owner.velocity = dashVelocity;
			Owner.ChangeDir(Math.Sign(dashVelocity.X));
			Owner.Calamity().LungingDown = true;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9865f;
			base.Projectile.scale = MathHelper.Lerp(1f, 0.4f, MathF.Pow(1f - DashTimer / 38f, 5f));
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.itemTime = (Owner.itemAnimation = 2);
			if (DashTimer <= 0f)
			{
				Player owner = Owner;
				owner.velocity *= 0.1f;
				Owner.Calamity().LungingDown = false;
				base.Projectile.Kill();
			}
		}
	}

	public float PierceWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(0f, 0.2f, completionRatio, clamped: true) * base.Projectile.scale * 24f * (1f - (float)Math.Pow(LungeProgression, 4.0));
	}

	public Color PierceColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		int frameHeight = texture.Height / Main.projFrames[base.Projectile.type];
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, base.Projectile.frame * frameHeight, texture.Width, frameHeight);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)frameHeight / 2f);
		SpriteEffects spriteEffects = (SpriteEffects)(Owner.direction != 1);
		lightColor = Color.White;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, lightColor * base.Projectile.Opacity, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects);
		DrawPierceTrail();
		return false;
	}

	public void DrawPierceTrail()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		if (DashState == 2f)
		{
			Main.spriteBatch.EnterShaderRegion();
			Color mainColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 2f % 1f, Color.Aqua, Color.MediumAquamarine, Color.DarkOrange, Color.OrangeRed);
			Color secondaryColor = CalamityUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * 2f + 0.2f) % 1f, Color.Aqua, Color.MediumAquamarine, Color.DarkOrange, Color.OrangeRed);
			mainColor = Color.Lerp(Color.White, mainColor, 0.4f + 0.6f * (float)Math.Pow(LungeProgression, 0.5));
			secondaryColor = Color.Lerp(Color.White, secondaryColor, 0.4f + 0.6f * (float)Math.Pow(LungeProgression, 0.5));
			Vector2 trailOffset = base.Projectile.Size * 0.5f;
			GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/EternityStreak", (AssetRequestMode)2));
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
			GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
			int numPointsRendered = 30;
			int numPointsProvided = 60;
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos.Take(numPointsProvided).ToArray(), new PrimitiveSettings(PierceWidthFunction, PierceColorFunction, delegate
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return trailOffset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), numPointsRendered);
			Main.spriteBatch.ExitShaderRegion();
		}
	}

	public override void OnKill(int timeLeft)
	{
		DashState = 0f;
		DashTimer = 0f;
		base.Projectile.scale = 0.6f;
	}

	public LightspeedHoldout()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		innateOffset = new Vector2(23f, -0.1f);
		stabSoundTimer = 3;
		base._002Ector();
	}
}
