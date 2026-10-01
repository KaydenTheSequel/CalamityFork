using System;
using System.IO;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TauCannonHoldout : BaseGunHoldoutProjectile
{
	private enum AIState
	{
		Level0,
		Level1,
		Level2,
		Level3
	}

	private const float TimePerCharge = 60f;

	private const int CoolingDownTime = 40;

	public bool hasReachedLV1;

	public bool hasReachedLV2;

	public bool hasReachedLV3;

	public Color color1;

	public Color color2;

	public static readonly SoundStyle ChargeLV1Sound = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV1")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle ChargeLV2Sound = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV2")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle OrbSound = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLoop");

	public static readonly SoundStyle BoltShootSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ExoLaserShoot")
	{
		Volume = 0.1f,
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle SmallBeamSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusMine")
	{
		Volume = 0.2f,
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle BigBeamSound = new SoundStyle("CalamityMod/Sounds/Item/PhotoUseSound");

	public static readonly SoundStyle CoolingDownSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ThanatosVent")
	{
		Volume = 0.025f
	};

	private SlotId OrbSoundSlot;

	private SlotId CoolingDownSoundSlot;

	private ref float Timer => ref base.Projectile.ai[0];

	private bool HasShotBeam
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = (value ? 1f : 0f);
		}
	}

	private float ChargeLV1 => 60f;

	private float ChargeLV2 => 120f;

	private float ChargeLV3 => 180f;

	private AIState State
	{
		get
		{
			float timer = Timer;
			if (!(timer >= 180f))
			{
				if (!(timer >= 120f))
				{
					if (timer >= 60f)
					{
						return AIState.Level1;
					}
					return AIState.Level0;
				}
				return AIState.Level2;
			}
			return AIState.Level3;
		}
	}

	public override int AssociatedItemID => ModContent.ItemType<TauCannon>();

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
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 3f * (float)base.Projectile.spriteDirection;
		}
	}

	public override float WeaponTurnSpeed
	{
		get
		{
			if (!HasShotBeam)
			{
				return 0.4f;
			}
			return Utils.Remap(Timer, 0f, 120f, 0.4f, 0.018f);
		}
	}

	public override float MaxOffsetLengthFromArm => 30f;

	public override float OffsetXUpwards => -15f;

	public override float OffsetXDownwards => 5f;

	public override float BaseOffsetY => -15f;

	public override float OffsetYUpwards => 10f;

	public override float OffsetYDownwards => 15f;

	public override void KillHoldoutLogic()
	{
		if (State == AIState.Level0 && base.Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
		if (!base.Owner.active || base.Owner.dead)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0973: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1f: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.HeldItem.Calamity();
		if (base.Owner.CantUseHoldout() && base.KeepRefreshingLifetime)
		{
			base.KeepRefreshingLifetime = false;
			Projectile projectile = base.Projectile;
			projectile.timeLeft = State switch
			{
				AIState.Level1 => 95, 
				AIState.Level2 => 71, 
				AIState.Level3 => 136, 
				_ => 0, 
			};
			base.Projectile.netUpdate = true;
		}
		if (!base.KeepRefreshingLifetime && Main.myPlayer == base.Projectile.owner)
		{
			switch (State)
			{
			case AIState.Level1:
				if (base.Projectile.timeLeft % 4 == 0 && base.Projectile.timeLeft > 40 && Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.RotatedByRandom(0.19634954631328583) * base.HeldItem.shootSpeed, ModContent.ProjectileType<TauCannonBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					base.OffsetLengthFromArm -= 5f;
					SoundStyle style = BoltShootSound with
					{
						Volume = 0.3f,
						Pitch = 0.8f
					};
					SoundEngine.PlaySound(in style, GunTipPosition);
				}
				if (!HasShotBeam)
				{
					HasShotBeam = true;
				}
				break;
			case AIState.Level2:
				if (!HasShotBeam && Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity, ModContent.ProjectileType<TauCannonBeam>(), (int)((float)base.Projectile.damage * 13f), base.Projectile.knockBack * 3f, base.Projectile.owner, 0f, base.Projectile.whoAmI);
					base.OffsetLengthFromArm -= 25f;
					HasShotBeam = true;
					SoundEngine.PlaySound(in SmallBeamSound, GunTipPosition);
				}
				break;
			case AIState.Level3:
				if (!HasShotBeam)
				{
					if (base.Projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition + base.Projectile.velocity * 25f, base.Projectile.velocity, ModContent.ProjectileType<TauCannonBeam>(), (int)((float)base.Projectile.damage * 0.7f), base.Projectile.knockBack * 5f, base.Projectile.owner, 0f, base.Projectile.whoAmI, 1f);
						int randomPortalAmount = Main.rand.Next(4, 6);
						for (int i = 0; i < randomPortalAmount; i++)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Owner.Center + Main.rand.NextVector2CircularEdge(480f, 480f) * Main.rand.NextFloat(0.8f, 1.2f), Vector2.Zero, ModContent.ProjectileType<TauCannonPortal>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
						}
					}
					HasShotBeam = true;
				}
				if (base.Projectile.timeLeft > 40)
				{
					if ((float)base.Projectile.timeLeft % 6f == 0f)
					{
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition - base.Projectile.velocity * 10f, base.Projectile.velocity * 5f, Main.rand.NextBool(3) ? color2 : color1, new Vector2(0.5f, 1f), base.Projectile.velocity.ToRotation(), 0.1f, 0.6f, 15));
					}
					if (Main.rand.NextBool(2))
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(4f, 9f), Main.rand.NextBool(3) ? color2 : color1, 17, Main.rand.NextFloat(0.9f, 1.7f), 0.7f, 0f, glowing: true));
					}
				}
				break;
			}
		}
		if ((Timer == ChargeLV1 && !hasReachedLV1) || (Timer == ChargeLV2 && !hasReachedLV2) || (Timer == ChargeLV3 && !hasReachedLV3))
		{
			if (Timer == ChargeLV1 && !hasReachedLV1)
			{
				PerLevelChargeEffect(State);
				hasReachedLV1 = true;
			}
			if (Timer == ChargeLV2 && !hasReachedLV2)
			{
				PerLevelChargeEffect(State);
				hasReachedLV2 = true;
			}
			if (Timer == ChargeLV3 && !hasReachedLV3)
			{
				PerLevelChargeEffect(State);
				hasReachedLV3 = true;
			}
		}
		if (base.Projectile.timeLeft < 40 && !base.KeepRefreshingLifetime && Timer > 60f)
		{
			if (base.Projectile.timeLeft % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.4f, -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(5f, 12f), Color.DarkGray, Color.Transparent, Main.rand.NextFloat(0.6f, 0.9f), Main.rand.NextFloat(300f, 400f)));
			}
			SoundStyle steam = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashSteamRelease");
			if (!SoundEngine.TryGetActiveSound(CoolingDownSoundSlot, out ActiveSound sound))
			{
				SoundStyle style = steam with
				{
					Volume = 0.5f,
					Pitch = -0.5f
				};
				CoolingDownSoundSlot = SoundEngine.PlaySound(in style, GunTipPosition);
			}
			else
			{
				sound.Position = base.Projectile.Center;
			}
		}
		if (base.KeepRefreshingLifetime)
		{
			if (Main.rand.NextBool(4))
			{
				float randomRadius = Utils.Remap(Timer, 0f, 180f, 5f, 15f);
				Vector2 position = GunTipPosition + Main.rand.NextVector2Circular(randomRadius, randomRadius);
				Vector2? velocity = -Vector2.UnitY.RotatedByRandom(100.0) * Main.rand.NextFloat(2f, 5f) * Utils.Remap(Timer, 0f, 180f, 1f, 5f);
				float scale = Main.rand.NextFloat(0.3f, 0.9f);
				Dust dust = Dust.NewDustPerfect(position, 278, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? color2 : color1);
			}
			GeneralParticleHandler.SpawnParticle(new GenericBloom(GunTipPosition, base.Projectile.velocity, color1, Utils.Remap(Timer, 0f, 180f, 0f, 1f), 2, produceLight: false));
			GeneralParticleHandler.SpawnParticle(new GenericBloom(GunTipPosition, base.Projectile.velocity, Color.White, Utils.Remap(Timer, 0f, 180f, 0f, 0.8f), 2, produceLight: false));
			float distanceFromTip = Utils.Remap(Timer, 0f, 180f, 25f, 300f);
			float sizeIncrease = Utils.Remap(Timer, 0f, 180f, 0.05f, 0.4f);
			GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(base.Owner, sizeIncrease, Main.rand.NextVector2Circular(distanceFromTip, distanceFromTip), Main.rand.NextFloat(5f, 10f), Color.White, Main.rand.NextBool(3) ? color2 : color1, Main.rand.Next(10, 21), GunTipPosition));
			if (!SoundEngine.TryGetActiveSound(OrbSoundSlot, out ActiveSound sound2))
			{
				SoundStyle style = OrbSound with
				{
					Volume = 0.01f,
					Pitch = -1f,
					IsLooped = true
				};
				OrbSoundSlot = SoundEngine.PlaySound(in style, GunTipPosition);
			}
			else
			{
				sound2.Position = base.Projectile.Center;
				sound2.Volume = Utils.GetLerpValue(0f, ChargeLV2, Timer, clamped: true) * 100f;
				sound2.Pitch = 1f * Utils.GetLerpValue(0f, ChargeLV2, Timer, clamped: true);
			}
		}
		if (base.KeepRefreshingLifetime)
		{
			Timer++;
		}
		if ((base.Projectile.timeLeft <= 1 || !base.KeepRefreshingLifetime) && SoundEngine.TryGetActiveSound(OrbSoundSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	private void PerLevelChargeEffect(AIState state)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		int dustAmount = state switch
		{
			AIState.Level3 => 36, 
			AIState.Level2 => 24, 
			AIState.Level1 => 12, 
			_ => 0, 
		};
		for (int i = 0; i < dustAmount; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * 8f;
			Vector2 gunTipPosition = GunTipPosition;
			Vector2? velocity2 = velocity;
			float scale = Utils.Remap(dustAmount, 10f, 30f, 1.5f, 2.2f);
			Dust dust = Dust.NewDustPerfect(gunTipPosition, 267, velocity2, 0, default(Color), scale);
			dust.noGravity = true;
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.color = (Main.rand.NextBool(3) ? color2 : color1);
		}
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, Vector2.Zero, color1, Vector2.One, 0f, 1f, 0f, 30));
		SoundEngine.PlaySound(state switch
		{
			AIState.Level3 => ChargeLV2Sound, 
			AIState.Level2 => ChargeLV1Sound, 
			AIState.Level1 => ChargeLV1Sound, 
			_ => ChargeLV1Sound, 
		}, GunTipPosition);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		if (Timer < 2f)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>(GlowTexture, (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color glowDrawColor = Color.Lerp(Color.Black, Color.White, Utils.GetLerpValue(0f, 180f, Timer, clamped: true));
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		if (base.Owner.gravDir == -1f)
		{
			flipSprite = (SpriteEffects)(flipSprite | 2);
		}
		if (base.Projectile.timeLeft > 40 || base.KeepRefreshingLifetime)
		{
			float shake = (base.KeepRefreshingLifetime ? Utils.Remap(Timer, 60f, 180f, 0f, 4f) : 3f);
			drawPosition += Main.rand.NextVector2Circular(shake, shake);
		}
		if (Timer >= ChargeLV2 && base.KeepRefreshingLifetime)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 position = drawPosition + Main.rand.NextVector2Circular(12f, 12f) * Utils.GetLerpValue(ChargeLV2, ChargeLV3, Timer, clamped: true);
				Color val = color1;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position, null, val * Utils.GetLerpValue(ChargeLV2, ChargeLV3, Timer, clamped: true), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, glowDrawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(base.Projectile.timeLeft);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		base.Projectile.timeLeft = reader.Read7BitEncodedInt();
	}

	public TauCannonHoldout()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		color1 = Color.MediumTurquoise;
		color2 = Color.Coral;
		base._002Ector();
	}
}
