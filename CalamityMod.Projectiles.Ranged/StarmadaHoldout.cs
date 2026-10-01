using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class StarmadaHoldout : BaseGunHoldoutProjectile
{
	public int time;

	public int lastUseTime;

	public static int perfectLeniancy = 3;

	public static int goodLeniancy = perfectLeniancy + 6;

	public static int starburstPerfectTime = 23;

	public float frontRecoil;

	public float recoilIntensity;

	public int recoilTimerMax;

	public Vector2 recoilDirection;

	public bool setVel;

	public float glowIntensity;

	public float attackVisualMult;

	public Color c1;

	public Color c2;

	public Color c3;

	public Color shiftColor;

	public Vector2 gunBackPosition;

	public int gunPower;

	public int lastGunPower;

	public SlotId AudSlot1;

	public SlotId AudSlot2;

	public bool failedChain;

	public float shake;

	public override int AssociatedItemID => ModContent.ItemType<Starmada>();

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction) * -5f;
		}
	}

	public override float RecoilResolveSpeed => 0.1f;

	public override float MaxOffsetLengthFromArm => 32f;

	public override float OffsetXUpwards => -12f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 10f;

	public override float WeaponTurnSpeed => 0.6f;

	public ref float shootingCooldown => ref base.Projectile.ai[0];

	public ref float starburstTimer => ref base.Projectile.ai[1];

	public int extendedCooldown => (int)((float)lastUseTime * 1.2f);

	public int perfectCooldown => (int)((float)lastUseTime * 1.5f);

	public ref float starburstCooldown => ref base.Projectile.ai[2];

	public bool naildriver
	{
		get
		{
			if (starburstTimer <= (float)(starburstPerfectTime + perfectLeniancy))
			{
				return starburstTimer >= (float)(starburstPerfectTime - perfectLeniancy);
			}
			return false;
		}
	}

	public bool scattershot
	{
		get
		{
			if (!naildriver)
			{
				if (starburstTimer <= (float)(starburstPerfectTime + goodLeniancy))
				{
					return starburstTimer >= (float)(starburstPerfectTime - goodLeniancy);
				}
				return false;
			}
			return false;
		}
	}

	public override void KillHoldoutLogic()
	{
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(lastUseTime);
		writer.Write(gunPower);
		writer.Write(lastGunPower);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		lastUseTime = reader.ReadInt32();
		gunPower = reader.ReadInt32();
		lastGunPower = reader.ReadInt32();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
	}

	public override void HoldoutAI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		float rate = (float)time * 0.05f;
		List<Color> eColors = new List<Color> { c1, c2, c3 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		shiftColor = Color.Lerp(currentColor, nextColor, (rate % 2f >= 1f) ? 1f : (rate % 1f));
		base.SetUsage = false;
		bool doingNothing = shootingCooldown == 0f && starburstCooldown == 0f && starburstTimer == 0f;
		if ((lastUseTime == 0) | doingNothing)
		{
			lastUseTime = base.Owner.HeldItem.useAnimation;
		}
		int projToShoot;
		if (!doingNothing)
		{
			projToShoot = (base.Owner.itemTime = (base.Owner.itemAnimation = 5));
		}
		glowIntensity = MathHelper.Lerp(glowIntensity, (float)Math.Pow(Utils.GetLerpValue(recoilTimerMax, 0f, shootingCooldown, clamped: true), 5.0), 0.2f);
		attackVisualMult = MathHelper.Lerp(attackVisualMult, (float)Math.Pow(Math.Min(Utils.GetLerpValue(0f, starburstPerfectTime - 1, starburstTimer, clamped: true), 2f) + (float)(gunPower - 1) * 0.25f, 1.0) * glowIntensity, 0.2f);
		shake = MathHelper.Lerp(shake, 0f, 0.05f);
		frontRecoil = MathHelper.Lerp(frontRecoil, 0f, 0.11f);
		if (((base.Owner.HeldItem.type != ModContent.ItemType<Starmada>()) & doingNothing) || (doingNothing && (Main.mapFullscreen || base.Owner.mouseInterface)) || base.Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		bool hasAmmo = base.Owner.PickAmmo(base.HeldItem, out projToShoot, out var _, out var _, out var _, out var _, dontConsume: true);
		bool leftShootChecks = (base.Owner.whoAmI == Main.myPlayer && Main.mouseLeft && !Main.mapFullscreen && !base.Owner.mouseInterface && shootingCooldown == 0f) & hasAmmo;
		bool num = base.Owner.whoAmI == Main.myPlayer && base.Owner.Calamity().mouseRight && !Main.mapFullscreen && !base.Owner.mouseInterface && starburstCooldown == 0f && starburstTimer == 0f;
		if (base.Owner.whoAmI == Main.myPlayer && Main.mouseLeft && !hasAmmo && shake < 0.1f)
		{
			shake = 0.8f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
			{
				Volume = 0.6f,
				Pitch = -0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (leftShootChecks)
		{
			FireShotgun();
		}
		if (num)
		{
			base.Projectile.ForceNetUpdate();
			SoundStyle blast1 = new SoundStyle("CalamityMod/Sounds/Item/StarfleetStarburst");
			SoundStyle style = blast1 with
			{
				Volume = 0.7f + (float)gunPower * 0.1f,
				Pitch = 0f,
				MaxInstances = 8
			};
			AudSlot1 = SoundEngine.PlaySound(in style, base.Projectile.Center);
			SoundStyle blast2 = new SoundStyle("CalamityMod/Sounds/Item/StarfleetStarburst");
			style = blast2 with
			{
				Volume = 0.5f + (float)gunPower * 0.1f,
				Pitch = -0.2f + (float)gunPower * 0.2f,
				MaxInstances = 8
			};
			AudSlot2 = SoundEngine.PlaySound(in style, base.Projectile.Center);
			lastGunPower = gunPower;
			starburstTimer++;
		}
		if (starburstTimer > 0f)
		{
			if (starburstTimer < (float)(starburstPerfectTime / 2))
			{
				float up = 1f - (float)Math.Pow(Utils.GetLerpValue(starburstPerfectTime / 2 - 1, 0f, starburstTimer, clamped: true), 2.0);
				base.OffsetLengthFromArm = 32f - 7f * up;
				frontRecoil = -25f * up;
			}
			else
			{
				float down = (float)Math.Pow(Utils.GetLerpValue(starburstPerfectTime / 2, starburstPerfectTime - 1, starburstTimer, clamped: true), 12.0);
				base.OffsetLengthFromArm = 25f + 15f * down;
				frontRecoil = -25f + 25f * down;
			}
			if (starburstTimer == (float)starburstPerfectTime)
			{
				FireStarburst();
			}
			starburstTimer++;
			if (starburstTimer > (float)(starburstPerfectTime + goodLeniancy + 1))
			{
				starburstTimer = 0f;
			}
		}
		if (shootingCooldown > 0f)
		{
			if (lastGunPower != gunPower)
			{
				if (SoundEngine.TryGetActiveSound(AudSlot1, out ActiveSound s1) && s1.IsPlaying && failedChain)
				{
					s1.Pitch = MathHelper.Lerp(s1.Pitch, -0.7f, 0.07f);
					s1.Volume = MathHelper.Lerp(s1.Volume, 0.2f, 0.07f);
				}
				if (SoundEngine.TryGetActiveSound(AudSlot2, out ActiveSound s2) && s2.IsPlaying && failedChain)
				{
					s2.Pitch = MathHelper.Lerp(s2.Pitch, -0.7f, 0.07f);
					s2.Volume = MathHelper.Lerp(s2.Volume, 0.2f, 0.07f);
				}
			}
			shootingCooldown--;
		}
		else
		{
			failedChain = false;
		}
		if (starburstCooldown > 0f)
		{
			starburstCooldown--;
		}
		if (recoilIntensity > 0f && (shootingCooldown > 0f || starburstCooldown > 0f))
		{
			ManageRecoil();
		}
		time++;
	}

	public void ManageRecoil()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		float slowdown = (float)Math.Pow(Utils.GetLerpValue(recoilTimerMax / 2, recoilTimerMax, Math.Max(shootingCooldown, starburstCooldown), clamped: true), 4.0);
		Vector2 movement = recoilDirection * recoilIntensity * slowdown;
		if (0 == 0 || Collision.SolidCollision(base.Owner.Center + movement, (int)((float)base.Owner.width * 1.1f), (int)((float)base.Owner.height * 1.1f)) || !base.Owner.Calamity().mouseRight)
		{
			recoilIntensity = 0f;
		}
		else if (slowdown > 0.1f)
		{
			if (setVel)
			{
				base.Owner.velocity = movement * 0.25f;
				setVel = false;
			}
			Projectile projectile = base.Projectile;
			projectile.Center += movement;
			Player owner = base.Owner;
			owner.Center += movement;
		}
		else
		{
			setVel = true;
		}
	}

	public void FireShotgun()
	{
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ForceNetUpdate();
		base.Owner.PickAmmo(base.HeldItem, out var _, out var _, out var _, out var _, out var _, Main.rand.NextBool());
		if (!naildriver && gunPower > 1)
		{
			shake = 4f;
			failedChain = true;
			SoundStyle oops = new SoundStyle("CalamityMod/Sounds/Item/TaserLaunch");
			SoundStyle oops2 = new SoundStyle("CalamityMod/Sounds/Item/LightMetal");
			for (int i = 0; i < 2; i++)
			{
				SoundStyle obj = ((i == 0) ? oops : oops2);
				SoundEngine.PlaySound(obj with
				{
					Volume = 1f,
					Pitch = ((i == 0) ? 0.5f : (-0.3f)),
					MaxInstances = 2
				}, base.Projectile.Center);
			}
		}
		else
		{
			SoundStyle shotgunFire = new SoundStyle("CalamityMod/Sounds/Item/StarmadaFire");
			for (int j = 0; j < ((!naildriver) ? 1 : 2); j++)
			{
				SoundEngine.PlaySound(shotgunFire with
				{
					Volume = ((naildriver && j == 0) ? 0.3f : 0.6f),
					Pitch = ((naildriver && j == 0) ? (-0.2f) : 0f),
					MaxInstances = 2
				}, base.Projectile.Center);
			}
			if (naildriver)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteFullCharge");
				style.Volume = 0.7f;
				style.Pitch = 1.2f + 0.1f * (float)gunPower;
				style.MaxInstances = 2;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			for (int b = 0; b < 24; b++)
			{
				int parts2 = 4;
				for (int k = 0; k < parts2; k++)
				{
					float power = Main.rand.NextFloat(0.2f, 1f);
					Vector2 vel = ((float)Math.PI * 2f * (float)k / (float)parts2).ToRotationVector2().RotatedBy(base.Projectile.rotation) * 12f;
					float size = (0.8f + 0.35f * (float)gunPower) * Main.rand.NextFloat(0.9f, 1.1f) * (1.1f - power);
					int dustStyle = ModContent.DustType<SquashDust>();
					Dust dust = Dust.NewDustPerfect(gunBackPosition, dustStyle);
					dust.scale = size;
					dust.velocity = vel * power * (0.7f + (float)gunPower * 0.2f);
					dust.noGravity = true;
					dust.color = GetRandomColor();
					dust.fadeIn = (naildriver ? (-0.5f) : 0f);
					if (b == 0)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(gunBackPosition, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, naildriver ? 35 : 20, (0.8f + 0.35f * (float)gunPower) * 0.85f, shiftColor, new Vector2(0.65f, 1f), useAddativeBlend: true, glowCenter: true, base.Projectile.rotation + ((k % 2 == 0) ? ((float)Math.PI / 2f) : 0f), fadeIn: false, affectedByLight: false, 0.1f, 0.85f, 0.8f));
					}
				}
			}
		}
		int cooldown = (recoilTimerMax = (naildriver ? perfectCooldown : lastUseTime));
		shootingCooldown = cooldown;
		recoilDirection = -base.Projectile.velocity;
		base.Owner.SetScreenshake(naildriver ? (9 + gunPower) : (scattershot ? 8 : 5));
		base.OffsetLengthFromArm = ((!naildriver) ? (scattershot ? 7 : 15) : 0);
		frontRecoil = (naildriver ? (-25) : (scattershot ? (-18) : (-10)));
		float gunPowerMult = MathHelper.Lerp((float)gunPower, 1f, 0.7f);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int baseShotCount = 7;
			for (int l = 0; l < baseShotCount + gunPower; l++)
			{
				float randomVel = Main.rand.NextFloat(0.8f, 1f);
				float damageMult = ((naildriver || scattershot) ? 1.75f : 1f) / (float)baseShotCount;
				float spread = ((l == 0) ? 0f : (naildriver ? 0.06f : (scattershot ? 0.9f : 0.25f))) * MathHelper.Lerp((float)gunPower, 1f, 0.75f);
				int starExtraUpdates = (naildriver ? 9 : (scattershot ? 7 : 3));
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, randomVel * base.Projectile.velocity.RotatedByRandom(spread) * 8f, ModContent.ProjectileType<StarmadaStar>(), (int)((float)base.Projectile.damage * damageMult), base.Projectile.knockBack, base.Projectile.owner, 0f, starExtraUpdates, Main.rand.Next(0, 301)).extraUpdates = starExtraUpdates;
			}
		}
		for (int m = 0; m < (int)(25f * gunPowerMult); m++)
		{
			float variance = Main.rand.NextFloat(-0.7f, 0.7f);
			int dustStyle2 = ModContent.DustType<SquashDust>();
			Dust dust2 = Dust.NewDustPerfect(GunTipPosition, dustStyle2);
			dust2.scale = (Main.rand.NextFloat(1.4f, 1.8f) - Math.Abs(variance)) * 3f * gunPowerMult;
			dust2.velocity = base.Projectile.velocity.RotatedBy(variance) * Main.rand.NextFloat(18f, 19f) * (float)Math.Pow(1f - Math.Abs(variance), 2.0) * gunPowerMult;
			dust2.noGravity = true;
			dust2.color = GetRandomColor();
			dust2.fadeIn = 3.75f;
		}
		if (naildriver && gunPower < 3)
		{
			gunPower++;
		}
		if (scattershot || (!naildriver && !scattershot))
		{
			gunPower = 1;
		}
		recoilIntensity = (naildriver ? 70f : (scattershot ? 25f : 0f));
		setVel = true;
	}

	public void FireStarburst()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.SetScreenshake(8f);
		recoilDirection = -base.Projectile.velocity;
		if (recoilIntensity < 19f)
		{
			recoilIntensity = 19f;
		}
		if (recoilTimerMax < extendedCooldown)
		{
			recoilTimerMax = extendedCooldown;
		}
		if (starburstCooldown < (float)extendedCooldown)
		{
			starburstCooldown = extendedCooldown;
		}
		setVel = true;
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 blastCenter = GunTipPosition + base.Projectile.velocity * 10f;
			float blastSize = 140 + 15 * lastGunPower;
			float minMultiplier = 0.1f;
			int hitsToMinMult = 8;
			int damage = (int)((float)base.Projectile.damage * (1f + (float)lastGunPower * 0.5f));
			Projectile.NewProjectileDirect(base.Owner.GetSource_FromThis(), blastCenter, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), damage, -45f, base.Owner.whoAmI, blastSize, minMultiplier, hitsToMinMult).timeLeft = 15;
		}
		float gunPowerMult = MathHelper.Lerp((float)lastGunPower, 1f, 0.7f);
		for (int i = 0; i < 20; i++)
		{
			float dist = Main.rand.NextFloat(1f, 4f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition + Main.rand.NextVector2CircularEdge(dist * 3.5f, dist * 3.5f), base.Projectile.velocity * Main.rand.NextFloat(5f, 6f) * (8f - dist * 2f) * gunPowerMult, "CalamityMod/Particles/ForwardSmear", affectedByGravity: false, (int)((float)Main.rand.Next(9, 16) + dist * 3f), Main.rand.NextFloat(0.15f, 0.25f) * gunPowerMult, GetRandomColor(), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		for (int j = 0; j < 34; j++)
		{
			float rot = Main.rand.NextFloat(0.05f, 0.35f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			Vector2 startVel = base.Projectile.velocity.RotatedBy(rot) * Main.rand.NextFloat(8f, 18f) * (Main.rand.NextBool(4) ? 2f : 1f);
			GeneralParticleHandler.SpawnParticle(new VelChangingSpark(GunTipPosition, startVel * gunPowerMult, startVel.RotatedBy(rot * 5f) * gunPowerMult, "CalamityMod/Particles/PulseStar", Main.rand.Next(25, 46), Main.rand.NextFloat(0.1f, 0.35f) * gunPowerMult, GetRandomColor(), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, Main.rand.NextFloat(0.02f, 0.06f), 0.02f));
		}
		if (lastGunPower == 3)
		{
			int parts2 = 45;
			for (int k = 0; k < parts2; k++)
			{
				Vector2 intenededVel = ((float)Math.PI * 2f * (float)k / (float)parts2).ToRotationVector2() * 3f;
				Vector2 fxVel = Utils.RotatedBy(new Vector2(intenededVel.X, intenededVel.Y * 2.3f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Utils.RotatedBy(new Vector2(intenededVel.X * 0.5f, intenededVel.Y * 6f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Vector2 relativePosition = GunTipPosition + base.Projectile.velocity * 45f + fxVel.RotatedBy(base.Projectile.velocity.ToRotation());
				float size = Utils.GetLerpValue(0f, -3f, intenededVel.X, clamped: true);
				float width = Utils.GetLerpValue(0f, 3 * Math.Sign(fxVel.X), fxVel.X, clamped: true);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(color: ((size <= 0.5f) ? Color.Lerp(c3, c2, size * 2f) : Color.Lerp(c2, c1, size * 2f - 1f)) * 0.7f, relativePosition: relativePosition, velocity: fxVel * 1.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: (int)(15f + size * 5f), scale: 0.35f + size * 0.2f, stretch: new Vector2(1f + width * size, 1f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0f, glowCenterScale: 0.75f, glowOpacity: size * 0.85f));
			}
		}
		if (lastGunPower >= 2)
		{
			int parts3 = 85;
			for (int l = 0; l < parts3; l++)
			{
				Vector2 intenededVel2 = ((float)Math.PI * 2f * (float)l / (float)parts3).ToRotationVector2() * 5f;
				Vector2 fxVel2 = Utils.RotatedBy(new Vector2(intenededVel2.X, intenededVel2.Y * 2.3f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Utils.RotatedBy(new Vector2(intenededVel2.X * 0.5f, intenededVel2.Y * 6f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Vector2 relativePosition2 = GunTipPosition + fxVel2.RotatedBy(base.Projectile.velocity.ToRotation());
				float size2 = Utils.GetLerpValue(0f, -5f, intenededVel2.X, clamped: true);
				float width2 = Utils.GetLerpValue(0f, 5 * Math.Sign(fxVel2.X), fxVel2.X, clamped: true);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(color: ((size2 <= 0.5f) ? Color.Lerp(c3, c2, size2 * 2f) : Color.Lerp(c2, c1, size2 * 2f - 1f)) * 0.7f, relativePosition: relativePosition2, velocity: fxVel2 * 1.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: (int)(19f + size2 * 6f), scale: 0.45f + size2 * 0.23f, stretch: new Vector2(1f + width2 * size2, 1f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0f, glowCenterScale: 0.75f, glowOpacity: size2 * 0.85f));
			}
		}
		else
		{
			int parts4 = 70;
			for (int m = 0; m < parts4; m++)
			{
				Vector2 intenededVel3 = ((float)Math.PI * 2f * (float)m / (float)parts4).ToRotationVector2() * 4f;
				Vector2 fxVel3 = Utils.RotatedBy(new Vector2(intenededVel3.X, intenededVel3.Y * 2.3f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Utils.RotatedBy(new Vector2(intenededVel3.X * 0.5f, intenededVel3.Y * 6f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				Vector2 relativePosition3 = GunTipPosition + fxVel3.RotatedBy(base.Projectile.velocity.ToRotation());
				float size3 = Utils.GetLerpValue(0f, -4f, intenededVel3.X, clamped: true);
				float width3 = Utils.GetLerpValue(0f, 4 * Math.Sign(fxVel3.X), fxVel3.X, clamped: true);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(color: ((size3 <= 0.5f) ? Color.Lerp(c3, c2, size3 * 2f) : Color.Lerp(c2, c1, size3 * 2f - 1f)) * 0.7f, relativePosition: relativePosition3, velocity: fxVel3 * 1.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: (int)(18f + size3 * 5f), scale: 0.35f + size3 * 0.23f, stretch: new Vector2(1f + width3 * size3, 1f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0f, glowCenterScale: 0.75f, glowOpacity: size3 * 0.85f));
			}
		}
	}

	public Color GetRandomColor()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(Main.rand.Next(4) switch
		{
			0 => c1, 
			1 => c2, 
			_ => c3, 
		});
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		gunBackPosition = base.Projectile.Center - base.Projectile.velocity * 38f + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction) * -2f;
		Texture2D orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D shine = ModContent.Request<Texture2D>("CalamityMod/Particles/FadeStreak", (AssetRequestMode)2).Value;
		Texture2D front = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/StarmadaFront", (AssetRequestMode)2).Value;
		Texture2D frontGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/StarmadaFrontGlow", (AssetRequestMode)2).Value;
		Texture2D back = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/StarmadaBack", (AssetRequestMode)2).Value;
		Texture2D backGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/StarmadaBackGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(8f * shake, 8f * shake);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		float glowMult = (float)Math.Pow(Utils.GetLerpValue(recoilTimerMax / 2, recoilTimerMax, Math.Max(shootingCooldown, starburstCooldown), clamped: true), 4.0);
		int draws = 14 + 4 * gunPower;
		Math.Sin((float)time * 0.02f);
		float sine2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 55.5f / (float)Math.PI);
		float fastSine = (float)Math.Sin((float)time * 0.2f);
		Color val = Color.Gray * 0.15f;
		Color val2 = shiftColor;
		((Color)(ref val2)).A = 0;
		Color glowColor = Color.Lerp(val, val2, glowIntensity) * (0.1f * (float)gunPower + 0.5f * glowMult);
		Vector2 frontRecoilPlace = base.Projectile.velocity * frontRecoil;
		if ((starburstTimer > 0f && starburstCooldown == 0f) || gunPower > 1)
		{
			for (int i = 0; i < draws; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / (float)draws).ToRotationVector2().RotatedBy(time * 2);
				Vector2 position = drawPosition + drawOffset * (float)(6 + gunPower) * attackVisualMult;
				val2 = shiftColor;
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(back, position, null, val2 * 0.7f * attackVisualMult, drawRotation, back.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
				Vector2 position2 = drawPosition + drawOffset * (float)(6 + gunPower) * attackVisualMult + frontRecoilPlace;
				val2 = shiftColor;
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(front, position2, null, val2 * 0.7f * attackVisualMult, drawRotation, front.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			}
		}
		Main.EntitySpriteDraw(back, drawPosition, null, drawColor, drawRotation, back.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		for (int j = 0; j < draws; j++)
		{
			Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)j / (float)draws).ToRotationVector2().RotatedBy(time / 5) * (1.25f + (fastSine + 2f) * 0.2f + glowMult * 4f) * MathHelper.Lerp((float)gunPower, 1f, 0.75f);
			Main.EntitySpriteDraw(backGlow, drawPosition + drawOffset2, null, glowColor, drawRotation, backGlow.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			Color val3 = Color.Gray * 0.15f;
			val2 = Color.White;
			((Color)(ref val2)).A = 0;
			Main.EntitySpriteDraw(backGlow, drawPosition, null, Color.Lerp(val3, val2, glowIntensity), drawRotation, backGlow.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		Main.EntitySpriteDraw(front, drawPosition + frontRecoilPlace, null, drawColor, drawRotation, front.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		for (int k = 0; k < draws; k++)
		{
			Vector2 drawOffset3 = ((float)Math.PI * 2f * (float)k / (float)draws).ToRotationVector2().RotatedBy(time / 5) * (1.25f + (fastSine + 2f) * 0.2f + glowMult * 4f) * MathHelper.Lerp((float)gunPower, 1f, 0.75f);
			Main.EntitySpriteDraw(frontGlow, drawPosition + drawOffset3 + frontRecoilPlace, null, glowColor, drawRotation, frontGlow.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			Vector2 position3 = drawPosition + frontRecoilPlace;
			Color val4 = Color.Gray * 0.15f;
			val2 = Color.White;
			((Color)(ref val2)).A = 0;
			Main.EntitySpriteDraw(frontGlow, position3, null, Color.Lerp(val4, val2, glowIntensity), drawRotation, frontGlow.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		if (starburstTimer > 0f && starburstCooldown == 0f)
		{
			for (int l = 0; l < 6; l++)
			{
				val2 = shiftColor;
				((Color)(ref val2)).A = 0;
				Color orbColor = val2 * 0.5f;
				Vector2 scale = new Vector2(Math.Abs(sine2 * 0.5f) + 0.1f, 1f) * (0.05f + (float)l * 0.01f) * attackVisualMult * Main.rand.NextFloat(0.9f, 1.1f) * 8.5f;
				Main.EntitySpriteDraw(orb, GunTipPosition - Main.screenPosition, null, orbColor, Main.rand.NextFloat(-5f, 5f), orb.Size() * 0.5f, scale, (SpriteEffects)0);
			}
		}
		Color powerColor = Color.Lerp(shiftColor, (gunPower == 1) ? c1 : ((gunPower == 2) ? c2 : c3), 0.7f);
		float scaleMod = (0.15f + (float)gunPower * 0.1f) * attackVisualMult;
		float rand = Main.rand.NextFloat(0.7f, 1f);
		for (int m = 0; m < 4; m++)
		{
			for (int y = 0; y < 2; y++)
			{
				Vector2 position4 = gunBackPosition - Main.screenPosition;
				val2 = powerColor;
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(orb, position4, null, val2, (y == 0) ? ((float)Math.PI / 2f) : 0f, orb.Size() * 0.5f, new Vector2(0.5f, 1f) * scaleMod * 0.6f * rand, (SpriteEffects)0);
			}
			Vector2 offset = ((float)Math.PI * 2f * (float)m / 4f).ToRotationVector2().RotatedBy(base.Projectile.rotation);
			for (int t = 0; t < 3; t++)
			{
				Vector2 position5 = gunBackPosition - Main.screenPosition;
				val2 = powerColor;
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(shine, position5, null, val2, offset.ToRotation(), new Vector2((float)shine.Width * 0.5f, 0f), new Vector2((1f - (float)t * 0.1f) * rand + (float)gunPower * 0.05f, 0.5f + (float)t * 0.15f) * base.Projectile.scale * base.Owner.gravDir * scaleMod, (SpriteEffects)2);
			}
			Vector2 position6 = gunBackPosition - Main.screenPosition;
			val2 = Color.White;
			((Color)(ref val2)).A = 0;
			Main.EntitySpriteDraw(shine, position6, null, val2, offset.ToRotation(), new Vector2((float)shine.Width * 0.5f, 0f), new Vector2(0.5f, 0.75f) * base.Projectile.scale * base.Owner.gravDir * scaleMod, (SpriteEffects)2);
		}
		return false;
	}

	public StarmadaHoldout()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		recoilTimerMax = 62;
		setVel = true;
		glowIntensity = 1f;
		c1 = new Color(164, 47, 160);
		c2 = new Color(227, 97, 72);
		c3 = new Color(193, 255, 146);
		gunPower = 1;
		lastGunPower = 1;
		base._002Ector();
	}
}
