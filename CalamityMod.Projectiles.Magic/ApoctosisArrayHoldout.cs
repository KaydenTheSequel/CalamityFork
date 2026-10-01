using System;
using System.Runtime.CompilerServices;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ApoctosisArrayHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private Color _003CeffectsColor_003Ek__BackingField;

	public bool fullyCharged;

	public int time;

	public int shotNum;

	[CompilerGenerated]
	private SlotId _003CionHum_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<ApoctosisArray>();

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 1.5f * (float)base.Projectile.spriteDirection;
		}
	}

	public override float MaxOffsetLengthFromArm => 47f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -13f;

	public override float OffsetYDownwards => 5f;

	public override float WeaponTurnSpeed => 0.35f;

	public override float RecoilResolveSpeed
	{
		get
		{
			if (!(shootingTimer >= 0f))
			{
				return 0.05f;
			}
			return 0.3f;
		}
	}

	public bool firing => base.Projectile.ai[2] == 0f;

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float manaPower => ref base.Projectile.ai[1];

	public Color effectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CeffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CeffectsColor_003Ek__BackingField = value;
		}
	}

	public int fireRate => base.Owner.HeldItem.useAnimation;

	public SlotId ionHum
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CionHum_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CionHum_003Ek__BackingField = value;
		}
	}

	public override void KillHoldoutLogic()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.dead)
		{
			if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		bool isAtMaxMana = (float)base.Owner.statMana / (float)base.Owner.statManaMax2 >= 1f;
		base.Projectile.timeLeft++;
		if (time == 0)
		{
			shootingTimer = fireRate;
		}
		if (shootingTimer < 0f)
		{
			OnCooldown();
			shootingTimer++;
			if (shootingTimer == 0f)
			{
				shootingTimer = fireRate;
				if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum) && hum.IsPlaying)
				{
					hum?.Stop();
				}
			}
			return;
		}
		if (base.HeldItem.type != base.Owner.HeldItem.type)
		{
			if (manaPower > 0f)
			{
				shootingTimer = -75f;
			}
			if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum2) && hum2.IsPlaying)
			{
				hum2?.Stop();
			}
			base.Projectile.Kill();
		}
		if (shootingTimer == 0f)
		{
			if (firing)
			{
				if (base.Owner.Calamity().mouseRight)
				{
					base.Projectile.ai[2] = 5f;
					return;
				}
				if (base.Owner.CheckMana(base.HeldItem, -1, pay: true))
				{
					Shoot(big: false);
					shootingTimer = fireRate;
				}
				else
				{
					SoundStyle style = SoundID.MaxMana with
					{
						Pitch = -0.5f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					shootingTimer = -40f;
				}
			}
			else
			{
				if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum3) && hum3.IsPlaying)
				{
					hum3.Position = base.Projectile.Center;
					hum3.Pitch = MathHelper.Lerp(-0.5f, 0.1f, manaPower);
				}
				else
				{
					SoundStyle charge = new SoundStyle("CalamityMod/Sounds/Item/IonChargeLoop");
					SoundStyle style = charge with
					{
						Volume = 0.7f,
						IsLooped = true
					};
					ionHum = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (base.Owner.Calamity().mouseRight)
				{
					if (!isAtMaxMana)
					{
						if (manaPower == 0f)
						{
							time = 100;
						}
						if (time % 2 == 0)
						{
							base.Owner.statMana += (int)((float)base.Owner.statManaMax2 * 0.025f);
							manaPower += 0.0275f;
							if (base.Owner.statMana > base.Owner.statManaMax2)
							{
								base.Owner.statMana = base.Owner.statManaMax2;
							}
						}
						if (manaPower > 1f)
						{
							manaPower = 1f;
						}
						Vector2 vel = base.Projectile.velocity.RotateRandom(0.699999988079071) * Main.rand.NextFloat(7f, 14f);
						Dust dust = Dust.NewDustPerfect(GunTipPosition + vel * 10f, ModContent.DustType<LightDust>(), -vel);
						dust.scale = Main.rand.NextFloat(0.35f, 0.55f);
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.5f) : effectsColor);
					}
					if (isAtMaxMana && !fullyCharged && manaPower >= 0.25f)
					{
						SoundStyle fullCharge = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
						for (int i = 0; i < 2; i++)
						{
							SoundEngine.PlaySound(fullCharge with
							{
								Volume = 0.6f,
								Pitch = -0.7f + (float)i * 1.5f,
								MaxInstances = 2
							}, base.Projectile.Center);
						}
						fullyCharged = true;
						base.OffsetLengthFromArm -= 7f * manaPower;
					}
					base.OffsetLengthFromArm += 1.3f * manaPower;
				}
				else
				{
					if (fullyCharged)
					{
						Shoot(big: true);
						fullyCharged = false;
					}
					else
					{
						SoundStyle style = SoundID.Item109 with
						{
							Volume = 0.7f,
							Pitch = 1f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						for (int j = 0; j < 10; j++)
						{
							Vector2 vel2 = base.Projectile.velocity.RotateRandom(Math.PI);
							Dust dust2 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), -vel2);
							dust2.scale = Main.rand.NextFloat(0.95f, 1.35f) * (manaPower + 0.3f);
							dust2.noGravity = false;
							dust2.color = (Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.5f) : effectsColor);
						}
					}
					manaPower = 0f;
					shootingTimer = -75f;
					base.Projectile.ai[2] = 0f;
				}
			}
		}
		else if (!base.Owner.Calamity().mouseRight && !Main.mouseLeft)
		{
			if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum4) && hum4.IsPlaying)
			{
				hum4?.Stop();
			}
			base.Projectile.Kill();
		}
		if (shootingTimer > 0f)
		{
			shootingTimer--;
		}
		time++;
	}

	public void OnCooldown()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft++;
		if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum) && hum.IsPlaying)
		{
			float lerp = Utils.GetLerpValue(-60f, 0f, shootingTimer, clamped: true);
			hum.Position = base.Projectile.Center;
			hum.Pitch = MathHelper.Lerp(0f, -0.6f, lerp);
			hum.Volume = 1f - lerp;
		}
		Vector2 smokeVel = new Vector2(0f, -6f) * Main.rand.NextFloat(0.1f, 1.1f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel, Color.SlateGray, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool()));
	}

	public void Shoot(bool big)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		float manaPercent = (float)base.Owner.statMana / (float)base.Owner.statManaMax2;
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		Vector2 firingVelocity = shootDirection * 4f;
		float damageBoost = Math.Max(manaPower * 60f * ((manaPower >= 1f) ? 1.5f : 1f), 1f);
		SoundStyle style;
		if (big)
		{
			if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			SoundStyle fire = new SoundStyle("CalamityMod/Sounds/Item/ImpalerLaunch");
			for (int i = 0; i < ((!(manaPower >= 1f)) ? 1 : 2); i++)
			{
				SoundEngine.PlaySound(fire with
				{
					Volume = 0.9f,
					Pitch = -0.7f,
					MaxInstances = 2
				}, base.Projectile.Center);
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/PulseRifleFire");
			style.Volume = 0.75f;
			style.Pitch = ((manaPower >= 1f) ? 0.7f : 0.5f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Owner.SetScreenshake(12f * manaPower);
			float projSpeedMult = (manaPower + 0.4f) * ((manaPower >= 1f) ? 3f : 2f);
			Main.rand.NextFloat(-0.8f, 0.8f);
			Vector2 vel = firingVelocity * projSpeedMult;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, vel, ModContent.ProjectileType<ApoctosisBlast>(), (int)((float)base.Projectile.damage * damageBoost), base.Projectile.knockBack * 4f, base.Projectile.owner, 0f, 0f, 5f);
			}
			base.OffsetLengthFromArm -= 24f * manaPower;
			Player owner = base.Owner;
			owner.velocity -= base.Projectile.velocity * 14f * manaPower;
			base.Projectile.rotation += 0.5f * (float)base.Projectile.direction;
			for (int j = 0; (float)j < Math.Max(3f, 30f * manaPower); j++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(GunTipPosition, base.Projectile.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(7f, 25f), affectedByGravity: false, 35, Main.rand.NextFloat(0.5f, 0.9f), Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.5f) : effectsColor));
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, shootDirection * 7f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 23, 0.085f * manaPower, effectsColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, shootDirection * 13f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 18, 0.06f * manaPower, effectsColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
			return;
		}
		style = new SoundStyle("CalamityMod/Sounds/Item/ApoctosisShoot");
		style.Volume = 0.25f;
		style.Pitch = 0.5f - 1.1f * manaPercent;
		style.MaxInstances = -1;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (shotNum % 2 == 0)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity, ModContent.ProjectileType<IonBlast>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			for (int k = 1; k <= 10; k++)
			{
				Color useColor = (Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.3f) : effectsColor);
				Vector2 shootVel = (shootDirection * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), shootVel);
				dust.scale = Main.rand.NextFloat(0.85f, 1f);
				dust.noGravity = true;
				dust.color = useColor;
				if (k % 2 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, base.Projectile.velocity * 3f * (float)k, "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 11, 0.04f, useColor, new Vector2(1f, 0.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
				}
			}
		}
		else
		{
			for (int l = -1; l <= 1; l += 2)
			{
				Vector2 vel2 = firingVelocity.RotatedBy(0.23f * (float)l);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, vel2, ModContent.ProjectileType<IonBlast>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, l);
				}
				shootDirection = shootDirection.RotatedBy(0.23f * (float)l);
				for (int m = 1; m <= 10; m++)
				{
					Color useColor2 = (Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.3f) : effectsColor);
					Vector2 shootVel2 = (shootDirection * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
					Dust dust2 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), shootVel2);
					dust2.scale = Main.rand.NextFloat(0.85f, 1f);
					dust2.noGravity = true;
					dust2.color = useColor2;
					if (m % 2 == 0)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, vel2.SafeNormalize(Vector2.UnitX) * 3f * (float)m, "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 11, 0.04f, useColor2, new Vector2(1f, 0.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
					}
				}
			}
		}
		base.OffsetLengthFromArm -= 5f;
		shotNum++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		float manaPercent = (float)base.Owner.statMana / (float)base.Owner.statManaMax2;
		if (time < 2)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D texGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/ApoctosisArrayGlow", (AssetRequestMode)2).Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D sparkle = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Vector2 shake = Main.rand.NextVector2Circular(2f, 2f) * manaPower;
		Main.EntitySpriteDraw(tex, drawPosition + shake, null, base.Projectile.GetAlpha(lightColor), drawRotation, tex.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(texGlow, drawPosition + shake, null, Color.White, drawRotation, texGlow.Size() * 0.5f, base.Projectile.scale, flipSprite);
		if (shootingTimer >= 0f)
		{
			float reverseManaPower = MathHelper.Lerp(0.7f, 0.1f, (manaPower > 0f) ? (1f - manaPower) : manaPercent);
			for (int i = 0; i < 5; i++)
			{
				float iMult = 1f - 0.1f * (float)i;
				Color color;
				if (manaPower > 0f)
				{
					Vector2 position = GunTipPosition - Main.screenPosition + shake;
					color = Color.Lerp(effectsColor, Color.White, (float)i * 0.1f);
					((Color)(ref color)).A = 0;
					Main.EntitySpriteDraw(tex2, position, null, color, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1f, 0.35f) * 0.75f * manaPower * Main.rand.NextFloat(0.7f, 1.3f) * iMult, flipSprite);
				}
				for (int b = -1; b <= 1; b += 2)
				{
					float sine = MathHelper.Lerp((float)Math.Sin(Main.GlobalTimeWrappedHourly * (firing ? 20f : 35f) / (float)Math.PI), reverseManaPower * (float)b, 0.75f);
					Vector2 scale = new Vector2(0.3f, 1f * sine * (float)b) * (Main.rand.NextFloat(3f, 4.5f) * iMult + manaPower * 1.2f);
					float rotation = base.Projectile.rotation + (float)time * manaPower * (float)Math.Max(i - 2, 0) * 0.2f + (float)Math.PI / 4f * (float)b;
					Vector2 position2 = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity.RotatedBy(0.44f * (float)base.Projectile.direction) * 11.7f;
					color = Color.Lerp(effectsColor, Color.White, (float)i * 0.1f);
					((Color)(ref color)).A = 0;
					Main.EntitySpriteDraw(sparkle, position2, null, color, rotation, sparkle.Size() * 0.5f, scale, flipSprite);
				}
			}
		}
		return false;
	}

	public ApoctosisArrayHoldout()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectsColor = Color.Crimson;
		base._002Ector();
	}
}
