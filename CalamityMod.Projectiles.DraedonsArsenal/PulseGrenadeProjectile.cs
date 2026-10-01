using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseGrenadeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool caught;

	public int tileHits;

	public bool flung;

	public float fxScale;

	public NPC targeted;

	public bool hasStoppedHolding;

	public Color col;

	public bool pullPin;

	public bool onSpawn;

	public int beepTimer;

	public int beepRate;

	public NPC lastHitTarget;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/PulseGrenade";

	public ref float time => ref base.Projectile.ai[0];

	public bool catching => base.Projectile.ai[1] == 5f;

	public Player Owner => Main.player[base.Projectile.owner];

	public float UseTimer => Owner.HeldItem.useTime;

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		return flung;
	}

	public override bool? CanDamage()
	{
		if (!flung)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.dead && !flung)
		{
			base.Projectile.Kill();
			return;
		}
		if (Owner.HeldItem.type != ModContent.ItemType<PulseGrenade>())
		{
			base.Projectile.Kill();
			return;
		}
		if (onSpawn)
		{
			if (catching)
			{
				caught = true;
			}
			if (Owner.Calamity().StealthStrikeAvailable())
			{
				base.Projectile.Calamity().stealthStrike = true;
				time = 0f;
			}
			Owner.Calamity().ConsumeStealthByAttacking();
			onSpawn = false;
		}
		if (flung)
		{
			FlungState();
		}
		else
		{
			HeldState();
		}
		if (catching)
		{
			time--;
		}
		else
		{
			time++;
		}
		if (base.Projectile.Opacity < 1f)
		{
			base.Projectile.Opacity += 0.03f;
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref col)).ToVector3() * 0.3f);
	}

	public void HeldState()
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (beepTimer < 40)
			{
				if (base.Projectile.Opacity < 1f)
				{
					base.Projectile.Opacity += 0.2f;
				}
				beepTimer += beepRate;
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PulseSound");
				style.Volume = 0.3f;
				style.Pitch = 0.1f + (float)beepRate * 0.04f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.Opacity = 0f;
				beepTimer = 0;
				beepRate += 2;
			}
		}
		base.Projectile.velocity = Owner.velocity;
		float completion = time / (UseTimer * 0.7f * (float)((!base.Projectile.Calamity().stealthStrike) ? 1 : 2));
		if (completion >= 1f && !catching)
		{
			time = -1f;
			base.Projectile.Center = Owner.Center;
			base.Projectile.extraUpdates = (base.Projectile.Calamity().stealthStrike ? 22 : 5);
			base.Projectile.rotation += Main.rand.NextFloat(-4f, 4f);
			Vector2 velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * 9f;
			base.Projectile.velocity = velocity;
			SoundStyle style;
			if (base.Projectile.Calamity().stealthStrike)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
				style.Volume = 0.9f;
				style.Pitch = 0.5f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.timeLeft = 240;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 1.5f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 13, 0.08f, ArsenalEffects.ArsenalPulseColor, new Vector2(1.2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
				for (int i = 0; i <= 12; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
					dust.scale = Main.rand.NextFloat(1.2f, 1.9f);
					dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.5) * Main.rand.NextFloat(7f, 13f);
					dust.noGravity = true;
					dust.color = ArsenalEffects.ArsenalPulseColor;
					dust.fadeIn = 1f;
				}
				for (int j = 0; j <= 7; j++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
					dust2.scale = Main.rand.NextFloat(1.2f, 1.9f);
					dust2.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.30000001192092896) * Main.rand.NextFloat(9f, 18f);
					dust2.noGravity = true;
					dust2.color = ArsenalEffects.ArsenalPulseColor;
					dust2.fadeIn = 0.3f;
				}
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/LightThrow");
			style.Volume = 1f;
			style.Pitch = 0f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.tileCollide = true;
			flung = true;
			return;
		}
		fxScale = (float)Math.Pow(completion, 2.0);
		Owner.direction = Math.Sign(Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).X);
		float grenadeRot = 0f;
		float pinTime = 0.75f;
		Vector2 aimDirection = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		float dir = ((Owner.direction == -1) ? MathHelper.ToRadians(180f) : 0f);
		if (completion >= 0.7f)
		{
			if (pullPin && completion >= pinTime && !catching)
			{
				Vector2 vel = aimDirection * -6f - Vector2.UnitY * 3f + Owner.velocity;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel, "CalamityMod/Projectiles/DraedonsArsenal/PulseGrenadePin", affectedByGravity: true, 43, 1f, Color.White, Vector2.One, useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: true, 0.1f * (float)Math.Sign(vel.X)));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LightMetal");
				style.Volume = 0.6f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				pullPin = false;
			}
			float completionLerp = (float)Math.Pow(Utils.GetLerpValue(0.7f, 1f, completion, clamped: true), catching ? 2 : 7);
			grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(-75f, 130f, completionLerp) * (float)Owner.direction);
		}
		else
		{
			if (catching && !Main.mouseLeft)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.ai[1] = 0f;
			float completionLerp2 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
			grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp2) * (float)Owner.direction);
		}
		grenadeRot += aimDirection.ToRotation();
		Vector2 grenadePos = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, Owner.compositeFrontArm.rotation) + Utils.RotatedBy(new Vector2(0f, (float)(-18 * Owner.direction)), (double)grenadeRot, default(Vector2));
		float completionLerp3 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
		MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp3) * dir);
		base.Projectile.Center = grenadePos;
		base.Projectile.rotation = (-aimDirection).ToRotation() + (float)Math.PI / 2f * (float)Owner.direction + dir;
		float frontArmRot = aimDirection.ToRotation() - MathHelper.ToRadians(90f);
		float backArmRot = grenadeRot - ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : MathHelper.ToRadians(0f));
		if (completion >= 0.4f && completion <= pinTime && !catching)
		{
			float goalRot = MathHelper.Lerp(frontArmRot, backArmRot, (float)Math.Pow(Utils.GetLerpValue(caught ? 0.6f : 0.4f, pinTime, completion, clamped: true), ((!base.Projectile.Calamity().stealthStrike) ? 1 : 2) * (caught ? 5 : 3)));
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, goalRot);
		}
		else
		{
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (completion > pinTime && !catching) ? MathHelper.Lerp(Owner.compositeFrontArm.rotation, frontArmRot, 0.07f) : frontArmRot);
		}
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, backArmRot);
	}

	public void FlungState()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		if (time / (float)base.Projectile.extraUpdates > (float)Owner.HeldItem.useAnimation * 0.45f)
		{
			base.Projectile.localAI[1] = 5f;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 23, 0.4f, col * 0.6f, new Vector2(0.8f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.3f, 1f, 0.35f));
		}
		else if (targeted != null && time >= 30f)
		{
			base.Projectile.timeLeft++;
			float homeSpeed = Utils.GetLerpValue(30f, 180f, time, clamped: true);
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.8f * homeSpeed, 8f, 1f - 0.15f * homeSpeed, 0.99f, accelerate: true);
			if (targeted.life <= 0)
			{
				targeted = null;
			}
		}
		else
		{
			targeted = base.Projectile.Center.ClosestNPCAt(500f, ignoreTiles: false, bossPriority: true);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		bool squash = Main.rand.NextBool();
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, squash ? ModContent.DustType<SquashDust>() : ModContent.DustType<SquashDustHollow>(), -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(0.1f, 1f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.05f));
		dust.noGravity = true;
		dust.color = col;
		dust.noLightEmittence = true;
		if (squash)
		{
			dust.fadeIn = 1f;
			dust.scale *= 2f;
		}
		else
		{
			dust.velocity = dust.velocity.RotatedByRandom(0.20000000298023224);
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			dust.velocity = dust.velocity.RotatedByRandom(0.699999988079071) * 12.5f;
		}
		if (base.Projectile.timeLeft == 1)
		{
			Explode();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle soundStyle = CommonCalamitySounds.VoidstoneMine with
		{
			Volume = 1f
		};
		soundStyle = soundStyle with
		{
			Volume = 0.3f,
			Pitch = -0.25f - 0.1f * (float)tileHits,
			MaxInstances = 6
		};
		SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		tileHits++;
		if (tileHits > 2)
		{
			Explode();
		}
		return false;
	}

	public void Explode()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		Owner.SetScreenshake(3.5f * (float)((!base.Projectile.Calamity().stealthStrike) ? 1 : 2));
		int spin = ((!Main.rand.NextBool()) ? 1 : (-1));
		for (int i = 0; i < 8; i++)
		{
			Vector2 vel = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2();
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDustHollow>());
			dust.velocity = vel * 15f * ((i % 2 == 0) ? 0.6f : 1f);
			dust.scale = ((i % 2 == 0) ? 2.2f : 1.9f);
			dust.noGravity = true;
			dust.color = col;
			dust.noLightEmittence = true;
			dust.fadeIn = 0.3f;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (vel * 7f).RotatedBy(0.39269909262657166), ModContent.ProjectileType<PulseGrenadeOrb>(), base.Projectile.damage, 0f, Owner.whoAmI, 0f, spin, i).Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			if (base.Projectile.Calamity().stealthStrike)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel * 10f, ModContent.ProjectileType<PulseGrenadeOrb>(), base.Projectile.damage, 0f, Owner.whoAmI, 0f, -spin, i + 1).Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			}
		}
		SoundStyle style;
		if (base.Projectile.Calamity().stealthStrike)
		{
			style = new SoundStyle("CalamityMod/Sounds/Item/PulseSoundHeavy");
			style.Volume = 0.8f;
			style.Pitch = -0.6f;
			style.MaxInstances = 2;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		style = new SoundStyle("CalamityMod/Sounds/Item/PulseSoundHeavy");
		style.Volume = 0.9f;
		style.Pitch = 0f;
		style.MaxInstances = 2;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		base.Projectile.Kill();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.2f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		Explode();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 15f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		float fade = 1f;
		float reformMult = Utils.GetLerpValue(1f, 0f, base.Projectile.Opacity) * 3.5f;
		Texture2D tex = (pullPin ? ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PulseGrenade", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseGrenadeNoPin", (AssetRequestMode)2).Value);
		for (int i = 0; i < 15; i++)
		{
			Color val = col;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.25f * fade * fxScale;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2() * 2f * Math.Max(fxScale, reformMult);
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + drawOffset, null, auraColor, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(((!flung) ? Owner.direction : base.Projectile.direction) != 1));
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor * fade * base.Projectile.Opacity, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)(((!flung) ? Owner.direction : base.Projectile.direction) != 1));
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PulseGrenadeGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White * fade * base.Projectile.Opacity, base.Projectile.rotation, tex.Size() * 0.5f, 1f, (SpriteEffects)(((!flung) ? Owner.direction : base.Projectile.direction) != 1));
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCs.Add(index);
	}

	public PulseGrenadeProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		col = ArsenalEffects.ArsenalPulseColor;
		pullPin = true;
		onSpawn = true;
		beepRate = 1;
		base._002Ector();
	}
}
