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
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class StarfleetHoldout : BaseGunHoldoutProjectile
{
	public int time;

	public int lastUseTime;

	public static int perfectLeniancy = 3;

	public static int goodLeniancy = perfectLeniancy + 6;

	public static int starburstPerfectTime = 23;

	public float recoilIntensity;

	public int recoilTimerMax;

	public Vector2 recoilDirection;

	public bool setVel;

	public float glowIntensity;

	public Color c1;

	public Color c2;

	public Color c3;

	public Color shiftColor;

	public Vector2 gunBackPosition;

	public override int AssociatedItemID => ModContent.ItemType<Starfleet>();

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

	public override float MaxOffsetLengthFromArm => 25f;

	public override float OffsetXUpwards => -12f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 10f;

	public override float WeaponTurnSpeed => 0.6f;

	public ref float shootingCooldown => ref base.Projectile.ai[0];

	public ref float starburstTimer => ref base.Projectile.ai[1];

	public int extendedCooldown => (int)((float)lastUseTime * 1.2f);

	public int naildriverCooldown => (int)((float)lastUseTime * 1.5f);

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

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(lastUseTime);
		writer.Write(base.Projectile.spriteDirection);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		lastUseTime = reader.ReadInt32();
		base.Projectile.spriteDirection = reader.ReadInt32();
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
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
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
		if (((base.Owner.HeldItem.type != ModContent.ItemType<Starfleet>()) & doingNothing) || (doingNothing && (Main.mapFullscreen || base.Owner.mouseInterface)) || base.Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		bool hasAmmo = base.Owner.PickAmmo(base.HeldItem, out projToShoot, out var _, out var _, out var _, out var _, dontConsume: true);
		bool leftShootChecks = (base.Owner.whoAmI == Main.myPlayer && Main.mouseLeft && !Main.mapFullscreen && !base.Owner.mouseInterface && shootingCooldown == 0f) & hasAmmo;
		bool num = base.Owner.whoAmI == Main.myPlayer && base.Owner.Calamity().mouseRight && !Main.mapFullscreen && !base.Owner.mouseInterface && starburstCooldown == 0f && starburstTimer == 0f;
		if (base.Owner.whoAmI == Main.myPlayer && Main.mouseLeft && !hasAmmo && base.OffsetLengthFromArm >= 24.5f)
		{
			base.OffsetLengthFromArm -= 8f;
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
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/StarfleetStarburst");
			style.Volume = 1f;
			style.Pitch = 0f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			starburstTimer++;
		}
		if (starburstTimer > 0f)
		{
			if (starburstTimer < (float)(starburstPerfectTime / 2))
			{
				base.OffsetLengthFromArm = 25f - 12f * (1f - (float)Math.Pow(Utils.GetLerpValue(starburstPerfectTime / 2 - 1, 0f, starburstTimer, clamped: true), 5.0));
			}
			else
			{
				base.OffsetLengthFromArm = 13f + 20f * (float)Math.Pow(Utils.GetLerpValue(starburstPerfectTime / 2, starburstPerfectTime - 1, starburstTimer, clamped: true), 8.0);
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
			shootingCooldown--;
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
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ForceNetUpdate();
		base.Owner.PickAmmo(base.HeldItem, out var _, out var _, out var _, out var _, out var _, Main.rand.NextBool());
		SoundStyle shotgunFire = new SoundStyle("CalamityMod/Sounds/Item/StarfleetFire");
		for (int i = 0; i < ((!naildriver) ? 1 : 2); i++)
		{
			SoundEngine.PlaySound(shotgunFire with
			{
				Volume = ((naildriver && i == 0) ? 0.3f : 0.6f),
				Pitch = ((naildriver && i == 0) ? 0f : 0.2f),
				MaxInstances = 2
			}, base.Projectile.Center);
		}
		if (naildriver)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteFullCharge");
			style.Volume = 0.7f;
			style.Pitch = 1.3f;
			style.MaxInstances = 2;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		int cooldown = (recoilTimerMax = (naildriver ? naildriverCooldown : lastUseTime));
		shootingCooldown = cooldown;
		recoilDirection = -base.Projectile.velocity;
		base.Owner.SetScreenshake(naildriver ? 9 : (scattershot ? 7 : 4));
		base.OffsetLengthFromArm = ((!naildriver) ? (scattershot ? 7 : 15) : 0);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int baseShotCount = 6;
			for (int j = 0; j < baseShotCount; j++)
			{
				float randomVel = Main.rand.NextFloat(0.8f, 1f);
				float damageMult = ((naildriver || scattershot) ? 1.75f : 1f) / (float)baseShotCount;
				float spread = (naildriver ? 0.06f : (scattershot ? 0.9f : 0.25f));
				int starExtraUpdates = (naildriver ? 9 : (scattershot ? 7 : 3));
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, randomVel * base.Projectile.velocity.RotatedByRandom(spread) * 8f, ModContent.ProjectileType<StarfleetStar>(), (int)((float)base.Projectile.damage * damageMult), base.Projectile.knockBack, base.Projectile.owner, 0f, starExtraUpdates, Main.rand.Next(0, 301)).extraUpdates = starExtraUpdates;
			}
		}
		for (int b = 0; b < 24; b++)
		{
			int parts2 = 4;
			for (int k = 0; k < parts2; k++)
			{
				float power = Main.rand.NextFloat(0.2f, 1f);
				Vector2 vel = ((float)Math.PI * 2f * (float)k / (float)parts2).ToRotationVector2().RotatedBy(base.Projectile.rotation) * 12f;
				float size = 0.8f * Main.rand.NextFloat(0.9f, 1.1f) * (1.1f - power);
				int dustStyle = ModContent.DustType<SquashDust>();
				Dust dust = Dust.NewDustPerfect(gunBackPosition, dustStyle);
				dust.scale = size;
				dust.velocity = vel * power * 0.7f;
				dust.noGravity = true;
				dust.color = GetRandomColor();
				dust.fadeIn = (naildriver ? (-0.6f) : 0f);
				if (b == 0)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(gunBackPosition, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, naildriver ? 35 : 20, 0.5f, shiftColor, new Vector2(0.65f, 1f), useAddativeBlend: true, glowCenter: true, base.Projectile.rotation + ((k % 2 == 0) ? ((float)Math.PI / 2f) : 0f), fadeIn: false, affectedByLight: false, 0.1f, 0.85f, 0.8f));
				}
			}
		}
		for (int l = 0; l < 25; l++)
		{
			float variance = Main.rand.NextFloat(-0.7f, 0.7f);
			int dustStyle2 = ModContent.DustType<SquashDust>();
			Dust dust2 = Dust.NewDustPerfect(GunTipPosition, dustStyle2);
			dust2.scale = (Main.rand.NextFloat(1.4f, 1.8f) - Math.Abs(variance)) * 3f;
			dust2.velocity = base.Projectile.velocity.RotatedBy(variance) * Main.rand.NextFloat(18f, 19f) * (float)Math.Pow(1f - Math.Abs(variance), 2.0);
			dust2.noGravity = true;
			dust2.color = GetRandomColor();
			dust2.fadeIn = 4.75f;
		}
		recoilIntensity = (naildriver ? 55f : (scattershot ? 20f : 0f));
	}

	public void FireStarburst()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.SetScreenshake(7f);
		recoilDirection = -base.Projectile.velocity;
		if (recoilIntensity < 15f)
		{
			recoilIntensity = 15f;
		}
		if (recoilTimerMax < extendedCooldown)
		{
			recoilTimerMax = extendedCooldown;
		}
		if (starburstCooldown < (float)extendedCooldown)
		{
			starburstCooldown = extendedCooldown;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			float blastSize = 140f;
			float minMultiplier = 0.1f;
			int hitsToMinMult = 6;
			Projectile.NewProjectileDirect(base.Owner.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), base.Projectile.damage * 3, -45f, base.Owner.whoAmI, blastSize, minMultiplier, hitsToMinMult).timeLeft = 15;
		}
		for (int i = 0; i < 14; i++)
		{
			float dist = Main.rand.NextFloat(0f, 3f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition + Main.rand.NextVector2CircularEdge(dist * 5f, dist * 5f), base.Projectile.velocity * Main.rand.NextFloat(4f, 5f) * (6f - dist * 2f), "CalamityMod/Particles/ForwardSmear", affectedByGravity: false, (int)((float)Main.rand.Next(9, 16) + dist * 3f), Main.rand.NextFloat(0.1f, 0.2f), GetRandomColor(), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		for (int j = 0; j < 34; j++)
		{
			float rot = Main.rand.NextFloat(0.05f, 0.35f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			Vector2 startVel = base.Projectile.velocity.RotatedBy(rot) * Main.rand.NextFloat(8f, 18f) * (Main.rand.NextBool(4) ? 2f : 1f);
			GeneralParticleHandler.SpawnParticle(new VelChangingSpark(GunTipPosition, startVel, startVel.RotatedBy(rot * 5f), "CalamityMod/Particles/PulseStar", Main.rand.Next(25, 46), Main.rand.NextFloat(0.1f, 0.35f), GetRandomColor(), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, Main.rand.NextFloat(0.02f, 0.06f), 0.02f));
		}
		int parts = 60;
		for (int k = 0; k < parts; k++)
		{
			Vector2 intenededVel = ((float)Math.PI * 2f * (float)k / (float)parts).ToRotationVector2() * 4f;
			Vector2 fxVel = Utils.RotatedBy(new Vector2(intenededVel.X, intenededVel.Y * 2.3f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			Utils.RotatedBy(new Vector2(intenededVel.X * 0.5f, intenededVel.Y * 6f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			Vector2 relativePosition = GunTipPosition + fxVel.RotatedBy(base.Projectile.velocity.ToRotation());
			float size = Utils.GetLerpValue(0f, -4f, intenededVel.X, clamped: true);
			float width = Utils.GetLerpValue(0f, 4 * Math.Sign(fxVel.X), fxVel.X, clamped: true);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(color: ((size <= 0.5f) ? Color.Lerp(c3, c2, size * 2f) : Color.Lerp(c2, c1, size * 2f - 1f)) * 0.7f, relativePosition: relativePosition, velocity: fxVel * 1.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: (int)(15f + size * 5f), scale: 0.35f + size * 0.2f, stretch: new Vector2(1f + width * size, 1f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0f, glowCenterScale: 0.75f, glowOpacity: size * 0.85f));
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		gunBackPosition = base.Projectile.Center - base.Projectile.velocity * 22f + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction) * -2f;
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/StarfleetGlow", (AssetRequestMode)2).Value;
		Texture2D orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		float glowMult = (float)Math.Pow(Utils.GetLerpValue(recoilTimerMax / 2, recoilTimerMax, Math.Max(shootingCooldown, starburstCooldown), clamped: true), 4.0);
		int draws = 18;
		Math.Sin((float)time * 0.02f);
		float attackMult = (float)Math.Pow(Utils.GetLerpValue(0f, starburstPerfectTime - 1, starburstTimer, clamped: true), 2.0);
		float sine2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 55.5f / (float)Math.PI);
		float fastSine = (float)Math.Sin((float)time * 0.2f);
		Color glowColor = shiftColor;
		Color val;
		if (starburstTimer > 0f && starburstCooldown == 0f)
		{
			for (int i = 0; i < draws; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / (float)draws).ToRotationVector2().RotatedBy(time * 2);
				Vector2 position = drawPosition + drawOffset * 6f * attackMult;
				val = shiftColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position, null, val * 0.7f * attackMult, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		for (int j = 0; j < draws; j++)
		{
			Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)j / (float)draws).ToRotationVector2().RotatedBy(time / 5) * (1.25f + (fastSine + 2f) * 0.2f + glowMult * 4f);
			Vector2 position2 = drawPosition + drawOffset2;
			Color val2 = Color.Gray * 0.15f;
			val = glowColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(glowTexture, position2, null, Color.Lerp(val2, val, glowIntensity) * (0.1f + 0.5f * glowMult), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			Color val3 = Color.Gray * 0.15f;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(glowTexture, drawPosition, null, Color.Lerp(val3, val, glowIntensity), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		if (starburstTimer > 0f && starburstCooldown == 0f)
		{
			for (int k = 0; k < 6; k++)
			{
				val = shiftColor;
				((Color)(ref val)).A = 0;
				Color orbColor = val * 0.5f;
				Vector2 scale = new Vector2(Math.Abs(sine2 * 0.5f) + 0.1f, 1f) * (0.05f + (float)k * 0.01f) * attackMult * Main.rand.NextFloat(0.9f, 1.1f) * 8.5f;
				Main.EntitySpriteDraw(orb, GunTipPosition - Main.screenPosition, null, orbColor, Main.rand.NextFloat(-5f, 5f), orb.Size() * 0.5f, scale, (SpriteEffects)0);
			}
		}
		return false;
	}

	public StarfleetHoldout()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		recoilTimerMax = 62;
		setVel = true;
		glowIntensity = 1f;
		c1 = new Color(146, 255, 211);
		c2 = new Color(222, 225, 146);
		c3 = new Color(255, 233, 146);
		base._002Ector();
	}
}
