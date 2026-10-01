using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AndromedasStride : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private const float MaxCharge = 360f;

	public Vector2 lastDisplacement;

	public float dashDuration;

	public CalamityUtils.CurveSegment QuickOut;

	public CalamityUtils.CurveSegment Bump;

	public CalamityUtils.CurveSegment QuickDraw;

	public CalamityUtils.CurveSegment SlowDrawOut;

	public CalamityUtils.CurveSegment OverShoot;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxiaExtra";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Charge => ref base.Projectile.ai[0];

	public ref float State => ref base.Projectile.ai[1];

	public ref float CurrentIndicator => ref base.Projectile.localAI[0];

	public ref float OverCharge => ref base.Projectile.localAI[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
	}

	public override bool? CanDamage()
	{
		if (State != 1f)
		{
			return false;
		}
		return null;
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
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	internal float ChargeDisplacement()
	{
		return CalamityUtils.PiecewiseAnimation(Charge / 360f, QuickOut, Bump, QuickDraw, SlowDrawOut, OverShoot);
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.velocity = Vector2.Zero;
			SoundEngine.PlaySound(in SoundID.Item101, base.Projectile.Center);
			initialized = true;
		}
		if (Owner.CantUseHoldout() && State == 0f)
		{
			if (Charge / 360f < 0.25f)
			{
				SoundEngine.PlaySound(in SoundID.Item109, base.Projectile.Center);
				base.Projectile.Kill();
				return;
			}
			SoundEngine.PlaySound(SoundID.Item120 with
			{
				Volume = SoundID.Item120.Volume * 0.5f
			}, base.Projectile.Center);
			float screenshakeLevel = 4f + CurrentIndicator * 2f;
			Main.LocalPlayer.SetScreenshake(screenshakeLevel);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(Owner.Center, Vector2.Zero, Color.HotPink, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.0075f * CurrentIndicator, 0.075f * CurrentIndicator, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			State = 1f;
			base.Projectile.timeLeft = (7 + (int)((Charge / 360f - 0.25f) * 20f)) * 2;
			dashDuration = base.Projectile.timeLeft;
			lastDisplacement = base.Projectile.Center - Owner.Center;
			base.Projectile.ForceNetUpdate();
		}
		if (State == 0f)
		{
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.Center = Owner.Center + direction * 70f * ChargeDisplacement();
			Charge++;
			OverCharge--;
			base.Projectile.timeLeft = 2;
			if ((Charge / 360f >= 0.25f && CurrentIndicator == 0f) || (Charge / 360f >= 0.5f && CurrentIndicator == 1f) || (Charge / 360f >= 0.75f && CurrentIndicator == 2f && Owner.whoAmI == Main.myPlayer))
			{
				for (int i = 0; i < 5; i++)
				{
					Vector2 velocity = direction.RotatedByRandom(0.7853981852531433) * 10f;
					Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, velocity, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.AndromedaAttunement_ChargeupBoltDamageMultiplier), 0f, Owner.whoAmI, 0.75f, (float)Math.PI / 50f, 1f);
				}
				SoundEngine.PlaySound(in SoundID.Item79, base.Projectile.Center);
				CurrentIndicator++;
				OverCharge = 20f;
			}
			if (Charge >= 360f)
			{
				Charge = 360f;
				if (Main.rand.NextBool())
				{
					Vector2 smokeSpeed = direction.RotatedByRandom(0.2356194704771042) * Main.rand.NextFloat(10f, 30f) * 0.9f;
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + direction * 50f, smokeSpeed + Owner.velocity, Color.Lerp(Color.Purple, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(0.6f, 1.2f), 0.8f, 0f, glowing: false, 0f, required: true));
					if (Main.rand.NextBool(3))
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + direction * 50f, smokeSpeed + Owner.velocity, Main.hslToRgb(0.85f, 1f, 0.8f), 20, Main.rand.NextFloat(0.4f, 0.7f), 0.8f, 0f, glowing: true, 0.01f, required: true));
					}
				}
				if (Owner.whoAmI == Main.myPlayer && CurrentIndicator < 4f)
				{
					for (int j = 0; j < 9; j++)
					{
						Vector2 velocity2 = direction.RotatedByRandom(0.7853981852531433) * 10f;
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, velocity2, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.AndromedaAttunement_ChargeupBoltDamageMultiplier), 0f, Owner.whoAmI, 0.75f, (float)Math.PI / 50f, 1f);
					}
					OverCharge = 20f;
					SoundEngine.PlaySound(in AstralBeacon.UseSound, base.Projectile.Center);
					CurrentIndicator++;
				}
			}
		}
		if (State == 1f)
		{
			base.Projectile.Center = Owner.Center + Vector2.Lerp(lastDisplacement, direction * 40f, MathHelper.Clamp((dashDuration - (float)base.Projectile.timeLeft) / dashDuration * 2f, 0f, 1f));
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			Owner.Calamity().LungingDown = true;
			if (Collision.SolidCollision(Owner.Center + direction * 120f * base.Projectile.scale - Vector2.One * 5f, 10, 10))
			{
				base.Projectile.timeLeft = 0;
				Owner.Calamity().LungingDown = false;
				base.Projectile.active = false;
				base.Projectile.ForceNetUpdate();
			}
			Owner.velocity = direction * 30f;
			float variation = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f);
			float strength = (float)Math.Sin(variation * 2f + (float)Math.PI / 2f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity - direction.RotatedBy(variation) * (1f + strength) * 2f * Main.rand.NextFloat(7.5f, 20f), Color.White, Main.rand.NextBool() ? Color.MediumTurquoise : Color.DarkOrange, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
		}
		Lighting.AddLight(base.Projectile.Center, new Vector3(1f, 0.56f, 0.56f) * Charge / 360f);
		base.Projectile.rotation = direction.ToRotation();
		base.Projectile.scale = 1f + Charge / 360f * 0.3f;
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Owner.GiveUniversalIFrames(FourSeasonsGalaxia.AndromedaAttunement_DashHitIFrames);
		if (!CalamityUtils.AnyProjectiles(ModContent.ProjectileType<AndromedasStrideBoltSpawner>()))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<AndromedasStrideBoltSpawner>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.AndromedaAttunement_StarDamageMultiplier), base.Projectile.knockBack, Owner.whoAmI, target.whoAmI, CurrentIndicator);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
		modifiers.SourceDamage *= FourSeasonsGalaxia.AndromedaAttunement_FullChargeMult * (float)Math.Pow(Charge / 360f, 2.0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxiaExtra", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)sword.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		Main.spriteBatch.EnterShaderRegion();
		if (OverCharge < 0f)
		{
			OverCharge = 0f;
		}
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(OverCharge / 20f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(new Color(255, 129, 153));
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (State == 1f)
		{
			Player owner = Owner;
			owner.velocity *= 0.33f;
		}
		Owner.Calamity().LungingDown = false;
		base.Projectile.active = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
		writer.Write(CurrentIndicator);
		writer.WriteVector2(lastDisplacement);
		writer.Write(dashDuration);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
		CurrentIndicator = reader.ReadSingle();
		lastDisplacement = reader.ReadVector2();
		dashDuration = reader.ReadSingle();
	}

	public AndromedasStride()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		QuickOut = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0f, 0f, 0.2f, 3);
		Bump = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.06f, 0.2f, 0.1f);
		QuickDraw = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.25f, 0.2f, -0.45f);
		SlowDrawOut = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0.5f, -0.25f, -0.2f, 3);
		OverShoot = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.93f, -0.45f, -0.1f);
		base._002Ector();
	}
}
