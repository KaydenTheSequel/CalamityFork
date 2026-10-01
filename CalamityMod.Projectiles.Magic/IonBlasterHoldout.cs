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

public class IonBlasterHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private Color _003CeffectsColor_003Ek__BackingField;

	public bool fullyCharged;

	public int time;

	public int shotNum;

	[CompilerGenerated]
	private SlotId _003CionHum_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<IonBlaster>();

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

	public override float MaxOffsetLengthFromArm => 30f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -9f;

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
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		float manaPercent = (float)base.Owner.statMana / (float)base.Owner.statManaMax2;
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		Vector2 firingVelocity = shootDirection * 4f;
		float damageBoost = ((manaPower >= 1f) ? 1.9f : 1.6f);
		SoundStyle style;
		if (big)
		{
			if (SoundEngine.TryGetActiveSound(ionHum, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			if (manaPower >= 1f)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/ImpalerLaunch");
				style.Volume = 0.8f;
				style.Pitch = -0.6f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong");
			style.Volume = 0.4f;
			style.Pitch = ((manaPower >= 1f) ? 0.8f : 0.6f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Owner.SetScreenshake(6f * manaPower);
			int maxProj = (int)(manaPower * 24f * ((manaPower >= 1f) ? 1f : 0.5f));
			for (int i = 0; i < maxProj; i++)
			{
				float variance = Main.rand.NextFloat(-0.5f, 0.5f);
				Vector2 vel = firingVelocity.RotatedBy(variance * 0.3f) * (1f - Math.Abs(variance)) * ((i % 2 == 0) ? 0.7f : 1f) * Main.rand.NextFloat(5f, 5.5f);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, vel, ModContent.ProjectileType<IonBlast>(), (int)((float)base.Projectile.damage * damageBoost), base.Projectile.knockBack * 2f, base.Projectile.owner, 0f, 0f, 5f);
				}
			}
			base.OffsetLengthFromArm -= 24f * manaPower;
			Player owner = base.Owner;
			owner.velocity -= base.Projectile.velocity * 12f * manaPower;
			for (int j = 0; (float)j < Math.Max(3f, 30f * manaPower); j++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(GunTipPosition, base.Projectile.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(7f, 25f), affectedByGravity: false, 35, Main.rand.NextFloat(0.5f, 0.9f), Main.rand.NextBool() ? Color.Lerp(effectsColor, Color.White, 0.5f) : effectsColor));
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, shootDirection * 9f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 17, 0.06f, effectsColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
			return;
		}
		style = SoundID.Item91 with
		{
			Volume = 0.5f,
			Pitch = 0.8f - 1.3f * manaPercent
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity, ModContent.ProjectileType<IonBlast>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		base.OffsetLengthFromArm -= 5f;
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
		shotNum++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D texGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/IonBlasterGlow", (AssetRequestMode)2).Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f);
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Vector2 shake = Main.rand.NextVector2Circular(2f, 2f) * manaPower;
		Main.EntitySpriteDraw(tex, drawPosition + shake, null, base.Projectile.GetAlpha(lightColor), drawRotation, tex.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(texGlow, drawPosition + shake, null, Color.White, drawRotation, texGlow.Size() * 0.5f, base.Projectile.scale, flipSprite);
		if (manaPower > 0f && shootingTimer >= 0f)
		{
			for (int i = 0; i < 4; i++)
			{
				float iMult = 1f - 0.1f * (float)i;
				Vector2 position = GunTipPosition - Main.screenPosition + shake;
				Color color = Color.Lerp(effectsColor, Color.White, (float)i * 0.1f);
				((Color)(ref color)).A = 0;
				Main.EntitySpriteDraw(tex2, position, null, color, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1f, 0.55f) * 0.5f * manaPower * Main.rand.NextFloat(0.7f, 1.3f) * iMult, flipSprite);
			}
		}
		return false;
	}

	public IonBlasterHoldout()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectsColor = Color.Crimson;
		base._002Ector();
	}
}
