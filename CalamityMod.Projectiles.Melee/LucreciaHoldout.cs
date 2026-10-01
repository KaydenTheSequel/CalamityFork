using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LucreciaHoldout : BaseSwordHoldoutProjectile, ILocalizedModType, IModType
{
	public bool helixFired;

	public bool gotEnergyThisSwing;

	public bool trailFXTriggered;

	public bool particlesSpawned;

	public bool sparkTriggered;

	public int standardStartupTime = 8;

	public int standardSwingTime = 10;

	public int standardCooldownTime = 12;

	public int thrustStartupTime = 33;

	public int thrustForwardTime = 7;

	public int thrustCooldownTime = 17;

	public float thrustSpinRotation;

	public bool playedAlternateThrustStartupWhoosh;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override bool useMeleeSpeed => true;

	public override bool useMeleeSize => false;

	public override int swingWidth => 310;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<Lucrecia>()).Item;

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Lucrecia";

	public override SoundStyle? UseSound => SoundID.Item71 with
	{
		Volume = 0.85f
	};

	public override int StartupTime { get; set; }

	public override int CooldownTime { get; set; }

	public override int swingTime { get; set; }

	public override bool AlternateSwings
	{
		get
		{
			return base.AlternateSwings;
		}
		set
		{
			base.AlternateSwings = value;
		}
	}

	public override float lineCollisionLength => 225f;

	private bool IsThrusting => base.Projectile.localAI[0] == 1f;

	public bool IsAlternateThrust { get; set; }

	public override void Defaults()
	{
		base.Projectile.extraUpdates = 3;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.Opacity = 0.2f;
		base.Projectile.width = (base.Projectile.height = 54);
		base.Projectile.scale = 1.25f;
	}

	public override void Spawn()
	{
		Player player = Main.player[base.Projectile.owner];
		player.GetModPlayer<BaseSwordHoldoutPlayer>();
		CalamityPlayer calamityPlayer = player.Calamity();
		int baseUseTime = BaseItem.useTime;
		if (player.altFunctionUse == 2)
		{
			if (calamityPlayer.darklightEnergy < 100)
			{
				base.Projectile.Kill();
				return;
			}
			IsAlternateThrust = true;
			StartupTime = (int)((float)thrustStartupTime * (float)player.itemAnimationMax / (float)baseUseTime);
			swingTime = (int)((float)thrustForwardTime * (float)player.itemAnimationMax / (float)baseUseTime);
			CooldownTime = (int)((float)thrustCooldownTime * (float)player.itemAnimationMax / (float)baseUseTime);
			base.Projectile.DamageType = DamageClass.Melee;
			base.Projectile.width = (base.Projectile.height = 36);
			useMeleeSize = true;
			UseSound = SoundID.DD2_JavelinThrowersAttack;
			OffsetDistance = -30;
			RotateInStartup = 0.8f;
			RotateInCooldown = 1f;
			base.Projectile.knockBack = 15f;
			calamityPlayer.darklightEnergy = 0;
		}
		else
		{
			IsAlternateThrust = false;
			StartupTime = (int)((float)standardStartupTime * (float)player.itemAnimationMax / (float)baseUseTime);
			CooldownTime = (int)((float)standardSwingTime * (float)player.itemAnimationMax / (float)baseUseTime);
			swingTime = (int)((float)standardCooldownTime * (float)player.itemAnimationMax / (float)baseUseTime);
			OffsetDistance = 36;
			RotateInStartup = 0.8f;
			RotateInCooldown = 0f;
		}
	}

	public override void AdditionalAI()
	{
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		BaseSwordHoldoutPlayer modplayer = player.GetModPlayer<BaseSwordHoldoutPlayer>();
		if (IsAlternateThrust)
		{
			if (base.inStartup)
			{
				trailFXTriggered = false;
				base.Projectile.Opacity += 0.02f;
				base.Projectile.scale = baseScale * MathHelper.Lerp(1f, 0.85f, MathF.Pow(base.StartupCompletion, 0.3f));
				if (base.StartupCompletion >= 0.12f && !playedAlternateThrustStartupWhoosh)
				{
					SoundStyle style = CommonCalamitySounds.MeatySlashSound with
					{
						Volume = 0.55f,
						Pitch = -0.05f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					playedAlternateThrustStartupWhoosh = true;
				}
				if (base.StartupCompletion > 0.12f && base.StartupCompletion < 0.44f)
				{
					if (base.timer % 8 == 0)
					{
						GeneralParticleHandler.SpawnParticle(new CritSpark(player.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 60f, Utils.RotatedBy(new Vector2(7f, 0f), (double)base.Projectile.rotation, default(Vector2)), Color.Lerp(Color.CornflowerBlue, Color.MediumPurple, Main.rand.NextFloat(1f)), Color.White * 0.33f, 1.2f, 12, 0.3f, 1.2f));
					}
					GeneralParticleHandler.SpawnParticle(new CircularSmearVFX(player.MountedCenter, Color.CornflowerBlue * 0.35f, base.Projectile.rotation, base.Projectile.scale * 1.25f));
				}
				float t = MathHelper.Clamp(base.StartupCompletion * 2f, 0f, 1f);
				float eased = 0.5f - 0.5f * MathF.Cos((float)Math.PI * t);
				thrustSpinRotation = eased * ((float)Math.PI * 2f);
				float totalRotation = player.Center.DirectionTo(Main.MouseWorld).ToRotation() + thrustSpinRotation * (float)player.direction;
				player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, totalRotation - (float)Math.PI / 2f);
				if (base.StartupCompletion >= 0.5f)
				{
					float easedPullback = MathF.Pow((base.StartupCompletion - 0.5f) * 2f, 0.3f);
					OffsetDistance = (int)MathHelper.Lerp(44f, 34f, easedPullback);
				}
				else
				{
					OffsetDistance = 50;
				}
			}
			else if (base.inCooldown)
			{
				base.Projectile.Opacity -= 0.025f;
				base.Projectile.scale = baseScale * MathHelper.Lerp(1.4f, 1.25f, base.CooldownCompletion);
				OffsetDistance = (int)MathHelper.Lerp(78f, 38f, base.CooldownCompletion);
			}
			else if (base.inSwing)
			{
				OffsetDistance = (int)MathHelper.Lerp(34f, 78f, base.SwingCompletion);
				Main.player[base.Projectile.owner].SetScreenshake(3.5f);
				float eased2 = MathF.Pow(MathHelper.Clamp(base.SwingCompletion, 0f, 1f), 0.125f);
				base.Projectile.scale = baseScale * MathHelper.Lerp(0.9f, 1.4f, eased2);
				Vector2 fireDirection = Vector2.Normalize(player.Calamity().mouseWorld - player.Center);
				if (!trailFXTriggered)
				{
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center, fireDirection * 5f, ModContent.ProjectileType<LucreciaDNATrailCreator>(), base.Projectile.damage * 7, base.Projectile.knockBack, base.Projectile.owner);
					}
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/MeatySlash");
					style.Volume = 0.3f;
					style.Pitch = Main.rand.NextFloat(0.1f, 0.2f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/OmicronBeam");
					style.Volume = 0.85f;
					style.Pitch = Main.rand.NextFloat(0.15f, 0.2f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				trailFXTriggered = true;
				if (!particlesSpawned)
				{
					for (int i = 0; i < 6; i++)
					{
						Vector2 center = base.Projectile.Center;
						Vector2 particleSpeed = fireDirection.RotatedByRandom(MathHelper.ToRadians(46f)) * Main.rand.NextFloat(20f, 37f);
						GeneralParticleHandler.SpawnParticle(new CritSpark(center, particleSpeed, Color.Lerp(Color.CornflowerBlue, Color.MediumPurple, Main.rand.NextFloat(0f, 1f)), Color.NavajoWhite * 0.7f, Main.rand.NextFloat(0.9f, 2f), Main.rand.Next(38, 50), 0.1f, 1.5f, 0.01f));
					}
					particlesSpawned = true;
				}
			}
		}
		else if (base.inStartup)
		{
			base.Projectile.Opacity += 0.02f;
			gotEnergyThisSwing = false;
			helixFired = false;
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.625f, 0.8f, base.StartupCompletion);
		}
		else if (base.inCooldown)
		{
			helixFired = false;
			base.Projectile.Opacity -= 0.1f;
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.85f, 0.625f, base.CooldownCompletion);
		}
		else if (base.inSwing)
		{
			if (!helixFired)
			{
				helixFired = true;
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 shootDir = player.MountedCenter.DirectionTo(mousePosition) * 10f;
				int dir = -Math.Sign(mousePosition.X);
				float scale = lineCollisionLength / 550f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(player.MountedCenter - shootDir * 4f, shootDir.RotatedBy(0.075f * (float)(dir * ((modplayer.swingNum % 2 == 0) ? 1 : (-1)))) * 1.22f, "CalamityMod/Particles/VerticalSmearLarge", affectedByGravity: false, (int)(14f / player.GetAttackSpeed(DamageClass.Melee)), scale, (modplayer.swingNum % 2 == 0) ? (Color.CornflowerBlue * 0.85f) : (Color.MediumPurple * 0.8f), new Vector2(1.1f, 1.3f)));
				Vector2 fireDirection2 = Vector2.Normalize(mousePosition - player.MountedCenter);
				float helixSpeed = 12f;
				Vector2 helixVelocity = fireDirection2 * helixSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.MountedCenter + fireDirection2 * 3f, helixVelocity, ModContent.ProjectileType<LucreciaBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LucreciaBoltFire");
				style.Volume = 0.8f;
				style.Pitch = Main.rand.NextFloat(-0.06f, 0.1f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			float t2 = MathHelper.Clamp(base.SwingCompletion, 0f, 1f);
			float easedMovement = MathF.Pow(t2, 0.4f);
			float parabola = 1f - MathF.Pow(easedMovement - 0.5f, 2f) * 4f;
			OffsetDistance = (int)MathHelper.Lerp(36f, 51.659996f, parabola);
			float scaleEase = MathF.Pow((easedMovement <= 0.5f) ? (easedMovement * 0.5f) : ((1f - easedMovement) * 0.5f), 2.6f);
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.8f, 1.4f, scaleEase);
			if (t2 > 0.05f && t2 < 0.5f && base.timer % 5 == 0)
			{
				Vector2 fireDirection3 = Vector2.Normalize(Main.MouseWorld - player.Center);
				Vector2 helixVelocity2 = fireDirection3 * 14f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(player.Center + fireDirection3 * 3f, helixVelocity2.RotatedByRandom(1.0), affectedByGravity: true, 16, 0.5f, Color.Lerp(Color.CornflowerBlue, Color.MediumPurple, Main.rand.NextFloat()) * 0.66f, fadeIn: true));
			}
		}
		base.AdditionalAI();
	}

	public override float SwingFunction()
	{
		Player player = Main.player[base.Projectile.owner];
		if (IsAlternateThrust)
		{
			if (base.inStartup)
			{
				return thrustSpinRotation;
			}
			return 0f;
		}
		BaseSwordHoldoutPlayer modPlayer = player.GetModPlayer<BaseSwordHoldoutPlayer>();
		float swingDirection = base.Projectile.spriteDirection;
		if (modPlayer.swingNum % 2 == 1)
		{
			swingDirection *= -1f;
		}
		float easedCompletion = MathF.Pow(base.SwingCompletion, 0.4f);
		float num = (float)(-swingWidth) / 2.15f;
		float endAnglePrimary = (float)swingWidth / 2.15f;
		float num2 = MathHelper.Lerp(num, endAnglePrimary, easedCompletion);
		float parabolicFactorPrimary = 1f - MathF.Pow(easedCompletion - 0.5f, 2f) * 4f;
		parabolicFactorPrimary = MathHelper.Clamp(parabolicFactorPrimary, 0f, 1f);
		base.Projectile.localAI[0] = parabolicFactorPrimary;
		return MathHelper.ToRadians(num2 * (0f - swingDirection));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (!IsAlternateThrust && !gotEnergyThisSwing)
		{
			SoundStyle style = CommonCalamitySounds.SwiftSliceSound with
			{
				Volume = CommonCalamitySounds.SwiftSliceSound.Volume * 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			gotEnergyThisSwing = true;
			Player obj = Main.player[base.Projectile.owner];
			CalamityPlayer calamityPlayer = obj.Calamity();
			calamityPlayer.darklightEnergy += 25;
			calamityPlayer.darklightEnergy = Math.Min(calamityPlayer.darklightEnergy, Lucrecia.MaxEnergy);
			int points = 2;
			float radians = (float)Math.PI * 2f / (float)points;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f)).RotatedByRandom(100.0);
			Color useColor = ((obj.GetModPlayer<BaseSwordHoldoutPlayer>().swingNum % 2 == 0) ? Color.CornflowerBlue : Color.MediumPurple);
			for (int k = 0; k < points; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center + velocity * 7.5f, velocity * 0.5f, affectedByGravity: false, 9, 0.05f, useColor, new Vector2(0.5f, 0.6f), quickShrink: true, glow: false));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.White;
		return base.PreDraw(ref lightColor);
	}
}
