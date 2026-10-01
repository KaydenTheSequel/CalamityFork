using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
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

public class OntologicalDespoilerHoldout : BaseGunHoldoutProjectile
{
	public int FramesPerLoad;

	public int MaxLoadableShots;

	public float BulletSpeed;

	public SlotId OntologicalChargeSlot;

	public SlotId OntologicalChargeSlotDeep;

	public int Time;

	public float ShotsLoaded;

	public Color baseColor;

	public Color color1;

	public Color color2;

	public Color color3;

	public Color color4;

	public bool hasBegunFiring;

	public int AftershotCooldownFrames;

	public int Charge1Frames;

	public int Charge2Frames;

	public bool hasReachedLV1;

	public bool hasReachedLV2;

	public int fireFrameCounter;

	public int fireFrame;

	public int flashFrameCounter;

	public int flashFrame;

	public bool showFlash;

	public Vector2 flashPos;

	public float flashRot;

	public Vector2 voidPlacement;

	public int inverseTimer;

	public bool drawInverse;

	public override int AssociatedItemID => ModContent.ItemType<OntologicalDespoiler>();

	public override float MaxOffsetLengthFromArm => 29f;

	public override float OffsetXUpwards => -8f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 8f;

	public override float RecoilResolveSpeed => 0.5f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + (base.Projectile.velocity * 45f).RotatedBy(0.18f * (float)base.Projectile.direction);
		}
	}

	public ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	public bool Positive
	{
		get
		{
			if (base.Projectile.ai[2] != 0f)
			{
				return base.Projectile.ai[2] == 10f;
			}
			return true;
		}
	}

	public ref float ShootRecoilTimer => ref base.Projectile.ai[1];

	public bool ChargeLV1 => CurrentChargingFrames >= (float)Charge1Frames;

	public bool ChargeLV2 => CurrentChargingFrames >= (float)Charge2Frames;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void KillHoldoutLogic()
	{
		if (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1849: Unknown result type (might be due to invalid IL or missing references)
		//IL_1854: Unknown result type (might be due to invalid IL or missing references)
		//IL_185e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_108a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1404: Unknown result type (might be due to invalid IL or missing references)
		//IL_1450: Unknown result type (might be due to invalid IL or missing references)
		//IL_145e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1463: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1481: Unknown result type (might be due to invalid IL or missing references)
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_153a: Unknown result type (might be due to invalid IL or missing references)
		//IL_153d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1605: Unknown result type (might be due to invalid IL or missing references)
		//IL_160a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		//IL_1122: Unknown result type (might be due to invalid IL or missing references)
		//IL_1127: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1791: Unknown result type (might be due to invalid IL or missing references)
		//IL_1796: Unknown result type (might be due to invalid IL or missing references)
		//IL_161f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1617: Unknown result type (might be due to invalid IL or missing references)
		//IL_1499: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_113f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1131: Unknown result type (might be due to invalid IL or missing references)
		//IL_1138: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1315: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1570: Unknown result type (might be due to invalid IL or missing references)
		//IL_1576: Unknown result type (might be due to invalid IL or missing references)
		//IL_1592: Unknown result type (might be due to invalid IL or missing references)
		//IL_159c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1633: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1144: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1369: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1371: Unknown result type (might be due to invalid IL or missing references)
		//IL_1376: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1702: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1728: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1744: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1773: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1205: Unknown result type (might be due to invalid IL or missing references)
		//IL_1207: Unknown result type (might be due to invalid IL or missing references)
		//IL_1224: Unknown result type (might be due to invalid IL or missing references)
		//IL_1229: Unknown result type (might be due to invalid IL or missing references)
		//IL_1236: Unknown result type (might be due to invalid IL or missing references)
		//IL_123c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1284: Unknown result type (might be due to invalid IL or missing references)
		//IL_1289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		color1 = base.Owner.shirtColor;
		color2 = Color.Lerp(base.Owner.shirtColor, Color.Black, 0.3f);
		color3 = Color.Lerp(base.Owner.shirtColor, Color.White, 0.2f);
		color4 = Color.Lerp(base.Owner.shirtColor, Color.White, 0.4f);
		if (SoundEngine.TryGetActiveSound(OntologicalChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		if (SoundEngine.TryGetActiveSound(OntologicalChargeSlotDeep, out ActiveSound ChargeSound6) && ChargeSound6.IsPlaying)
		{
			ChargeSound6.Position = base.Projectile.Center;
		}
		if (base.Owner.shirtColor != Color.White)
		{
			float rate = Main.GlobalTimeWrappedHourly * 15f;
			List<Color> eColors = new List<Color> { color1, color2, color3, color4 };
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color currentColor = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			baseColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		}
		if ((!Positive && base.Projectile.ai[2] < 10f) || base.Projectile.ai[2] == 15f)
		{
			baseColor = Color.White;
		}
		else if (base.Owner.shirtColor == Color.White)
		{
			baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > ((!(base.Projectile.ai[2] >= 10f)) ? 1 : 2))
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		fireFrameCounter++;
		if (fireFrameCounter > ((!(base.Projectile.ai[2] >= 10f)) ? 1 : 2))
		{
			fireFrame++;
			fireFrameCounter = 0;
		}
		if (fireFrame > 5)
		{
			fireFrame = 0;
		}
		if (showFlash)
		{
			flashFrameCounter++;
			if (flashFrameCounter > ((flashFrame != 2) ? 1 : 7))
			{
				flashFrame++;
				flashFrameCounter = 0;
			}
			if (flashFrame > 6)
			{
				showFlash = false;
			}
		}
		if (Time % 2 == 0)
		{
			voidPlacement = Main.rand.NextVector2Circular(10.5f, 10.5f);
		}
		if (inverseTimer <= 9)
		{
			drawInverse = true;
			if (inverseTimer == 0)
			{
				drawInverse = false;
				inverseTimer = Main.rand.Next(20, 41);
			}
		}
		if (ChargeLV2)
		{
			inverseTimer--;
		}
		if (base.Projectile.ai[2] == 20f)
		{
			base.Projectile.frame = 6;
			ShotsLoaded = 0f;
			if (Time == 0)
			{
				base.Projectile.timeLeft = (int)((float)AftershotCooldownFrames * 1.5f);
				base.OffsetLengthFromArm = 45f;
			}
		}
		else if (base.Projectile.ai[2] >= 10f)
		{
			ShotsLoaded = 0f;
			if (Time == 0)
			{
				for (int i = 0; i < 18; i++)
				{
					Color useColor = GetRandomColor();
					Dust dust = Dust.NewDustPerfect(base.Owner.Center, (!Positive) ? (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>(), Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.1f, 1f));
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(0.9f, 1.25f);
					dust.color = ((base.Projectile.ai[2] == 15f) ? baseColor : useColor);
				}
				base.Projectile.timeLeft = (int)((float)AftershotCooldownFrames * 2.5f * (float)(base.Owner.Calamity().despoilerNerf ? 1 : 3));
				base.Owner.Calamity().despoilerNerf = false;
				base.OffsetLengthFromArm = 45f;
			}
			if (Main.rand.NextBool() && Time > 1)
			{
				Dust dust2 = Dust.NewDustPerfect(GunTipPosition - (base.Projectile.velocity * 60f).RotatedBy(0.15f * (float)base.Projectile.direction), (base.Projectile.ai[2] != 15f) ? ModContent.DustType<LightDust>() : (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()), (base.Projectile.velocity * 14f).RotatedBy(MathHelper.ToRadians(-135f) * (float)base.Projectile.direction).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.6f, 1f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.8f, 0.9f) * Utils.GetLerpValue(-10f, 15f, base.Projectile.timeLeft);
				dust2.color = baseColor;
			}
		}
		if (base.Owner.CantUseHoldout() || base.Projectile.ai[2] >= 10f)
		{
			if (base.Projectile.ai[2] < 10f && ChargeLV1)
			{
				base.Owner.Calamity().despoilerNerf = true;
			}
			hasBegunFiring = true;
			if (SoundEngine.TryGetActiveSound(OntologicalChargeSlot, out ActiveSound ChargeSound7))
			{
				ChargeSound7?.Stop();
			}
			if (SoundEngine.TryGetActiveSound(OntologicalChargeSlotDeep, out ActiveSound ChargeSound8))
			{
				ChargeSound8?.Stop();
			}
			base.KeepRefreshingLifetime = false;
			if (ChargeLV2)
			{
				if (ShotsLoaded > 0f)
				{
					base.Projectile.timeLeft = AftershotCooldownFrames * 2;
					ChargeSound?.Stop();
					Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
					int charge2DamagePos = (int)((float)base.Projectile.damage * 0.5f);
					int charge2DamageNeg = base.Projectile.damage * 20;
					if (Main.myPlayer == base.Projectile.owner)
					{
						if (Positive)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * 2.5f, ModContent.ProjectileType<OntologicalDespoilerGrenade>(), charge2DamagePos, base.Projectile.knockBack, base.Projectile.owner);
							SoundEngine.PlaySound(in OntologicalDespoiler.BigShot, base.Projectile.Center);
						}
						else
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * 0.6f, ModContent.ProjectileType<OntologicalDespoilerBeam>(), charge2DamageNeg, base.Projectile.knockBack * 3f, base.Projectile.owner);
							base.Owner.SetScreenshake(12f);
							SoundStyle style = OntologicalDespoiler.BigShot2 with
							{
								Pitch = 0.2f
							};
							SoundEngine.PlaySound(in style, base.Projectile.Center);
							style = OntologicalDespoiler.BigShot with
							{
								Pitch = -0.3f
							};
							SoundEngine.PlaySound(in style, base.Projectile.Center);
						}
					}
					flashPos = GunTipPosition + base.Projectile.velocity * 90f;
					flashRot = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
					showFlash = true;
					for (int j = 0; j < 20; j++)
					{
						Color useColor2 = GetRandomColor();
						float rand = Main.rand.NextFloat(0f, 2.5f);
						Dust dust3 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, (j % 2 == 0) ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), shootVelocity.RotatedByRandom(0.12f + rand * 0.2f) * (Main.rand.NextFloat(2.5f, 6.3f) - rand));
						dust3.noGravity = true;
						dust3.scale = Main.rand.NextFloat(1.55f, 1.85f);
						dust3.color = (Positive ? useColor2 : baseColor);
					}
					ShotsLoaded = 0f;
					ShootRecoilTimer = 34f;
					base.OffsetLengthFromArm -= 35f;
				}
				else if (ShootRecoilTimer > 0f)
				{
					ShootRecoilTimer -= 2f;
				}
			}
			else if (ShotsLoaded > 0f)
			{
				base.Projectile.timeLeft = (ChargeLV1 ? AftershotCooldownFrames : ((int)((float)AftershotCooldownFrames * 1.5f)));
				ShootRecoilTimer -= (ChargeLV1 ? 3f : 2f);
				if (ShootRecoilTimer <= 0f)
				{
					ChargeSound?.Stop();
					Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
					if (Main.myPlayer == base.Projectile.owner)
					{
						Vector2 fireVec = shootVelocity2 * Main.rand.NextFloat(0.9f, 1.1f);
						if (Positive)
						{
							for (int k = 0; k < 3; k++)
							{
								float angle = k switch
								{
									2 => 0.25f, 
									0 => -0.25f, 
									_ => 0f, 
								};
								Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, fireVec.RotatedBy(angle) * (1f - Math.Abs(angle * 0.25f)), ModContent.ProjectileType<OntologicalDespoilerShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, k);
							}
						}
						else
						{
							for (int l = 0; l < 2; l++)
							{
								Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, fireVec.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.8f, 1f), ModContent.ProjectileType<OntologicalDespoilerShot>(), (int)((float)base.Projectile.damage / 4f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 5f);
							}
						}
					}
					SoundEngine.PlaySound(OntologicalDespoiler.SmallShot with
					{
						Pitch = (Positive ? 0f : (-0.2f)),
						MaxInstances = (Positive ? 1 : (-1)),
						Volume = (Positive ? 1f : 0.5f)
					}, base.Projectile.Center);
					if (!Positive)
					{
						SoundStyle style = MagnaCannon.Fire with
						{
							Pitch = 0.3f,
							Volume = 0.5f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					for (int m = 0; m < 3; m++)
					{
						Dust dust4 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, (!Positive) ? (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>(), shootVelocity2.RotatedByRandom(0.5) * Main.rand.NextFloat(0.3f, 2.3f));
						dust4.noGravity = true;
						dust4.scale = Main.rand.NextFloat(1.15f, 1.35f);
						dust4.color = baseColor;
					}
					ShotsLoaded -= ((!Positive) ? 1 : 2);
					ShootRecoilTimer = (Positive ? 16f : 12f);
					base.OffsetLengthFromArm -= 3.2f;
				}
			}
			else if (ShootRecoilTimer > 0f)
			{
				ShootRecoilTimer -= 2f;
			}
		}
		else
		{
			ShotsLoaded = Utils.Remap(CurrentChargingFrames, 0f, Charge1Frames, 1f, MaxLoadableShots / 2);
			if (ChargeLV1)
			{
				ShotsLoaded = MaxLoadableShots;
			}
			CurrentChargingFrames += ((ChargeLV1 && !Positive) ? 0.5f : 2f) * (base.Owner.Calamity().despoilerNerf ? 0.4f : 1f);
			if (!hasBegunFiring)
			{
				if (SoundEngine.TryGetActiveSound(OntologicalChargeSlot, out ActiveSound ChargeSound9))
				{
					ChargeSound9.Volume = Utils.Remap(CurrentChargingFrames, 0f, Charge1Frames, 0f, 0.8f) * 100f;
					ChargeSound9.Pitch = Utils.Remap(CurrentChargingFrames, 0f, Charge2Frames, -1f, 0.45f);
				}
				else
				{
					SoundStyle style = OntologicalDespoiler.ChargeLoop with
					{
						Volume = 0.01f,
						Pitch = 0f,
						IsLooped = true
					};
					OntologicalChargeSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (SoundEngine.TryGetActiveSound(OntologicalChargeSlotDeep, out ActiveSound ChargeSound10))
				{
					ChargeSound10.Volume = (Positive ? 0f : (Utils.Remap(CurrentChargingFrames, Charge1Frames, Charge2Frames, 0f, 1.8f) * 100f));
					ChargeSound10.Pitch = Utils.Remap(CurrentChargingFrames, (float)Charge2Frames * 0.9f, Charge2Frames, 1f, (CurrentChargingFrames >= (float)Charge2Frames) ? (-0.1f) : 0.2f);
				}
				else
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonChargeLoop")
					{
						Volume = 0.01f,
						Pitch = 0f,
						IsLooped = true
					};
					OntologicalChargeSlotDeep = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (CurrentChargingFrames >= 10f && (Positive || (!Positive && !ChargeLV2)))
			{
				float particleScale = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)Charge2Frames);
				for (int n = 0; n < (ChargeLV2 ? 3 : ((!ChargeLV1) ? 1 : 2)); n++)
				{
					Vector2 dustVel = -base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(1.1f, 15.8f);
					Vector2 addedPlace = (Positive ? Vector2.Zero : (dustVel * 3f));
					bool dustChance = Main.rand.NextBool((int)(30f * Utils.GetLerpValue(Charge2Frames, Charge1Frames, CurrentChargingFrames, clamped: true) + 1f));
					int dustType = ((!Positive) ? (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : (ChargeLV1 ? (dustChance ? ModContent.DustType<VoidDust>() : ModContent.DustType<LightDust>()) : ModContent.DustType<LightDust>()));
					Dust dust5 = Dust.NewDustPerfect(GunTipPosition + (Positive ? Vector2.Zero : (dustVel * 15f * Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true))), dustType, dustVel - addedPlace * Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true));
					dust5.noGravity = true;
					dust5.scale = Main.rand.NextFloat(0.5f, 1.25f) * Utils.GetLerpValue(0f, Charge1Frames, CurrentChargingFrames, clamped: true);
					dust5.color = baseColor;
				}
				GeneralParticleHandler.SpawnParticle(new GenericBloom(GunTipPosition, base.Projectile.velocity, Positive ? Color.Lerp(Color.White, Color.Black, Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true)) : (Color.Black * Utils.GetLerpValue(0f, Charge1Frames, CurrentChargingFrames, clamped: true)), particleScale / 400f * Main.rand.NextFloat(0.9f, 1.1f), 2, produceLight: false, AddativeBlend: false));
				float strength = particleScale / 45f;
				Vector3 DustLight = ((Color)(ref baseColor)).ToVector3() * 0.2f;
				Lighting.AddLight(GunTipPosition, DustLight * strength);
			}
			if (ChargeLV2 && !Positive)
			{
				for (int num = 0; num < 3; num++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(1.1f, 15.8f), "CalamityMod/Particles/PearlParticleGlow", affectedByGravity: false, 3, Main.rand.NextFloat(2.3f, 2.6f), Color.Black, new Vector2(0.3f, 1.35f), useAddativeBlend: false));
					bool color = Main.rand.NextBool(4);
					Vector2 dustVel2 = -base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(1.1f, 15.8f);
					Dust dust6 = Dust.NewDustPerfect(GunTipPosition, color ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), dustVel2);
					dust6.noGravity = true;
					dust6.scale = Main.rand.NextFloat(0.5f, 1.25f);
					dust6.color = (color ? Color.Black : baseColor);
				}
			}
			if (ChargeLV1 && !hasReachedLV1)
			{
				SoundEngine.PlaySound(in OntologicalDespoiler.ChargeLV1, base.Projectile.Center);
				for (int num2 = 0; num2 < 16; num2++)
				{
					Color useColor3 = GetRandomColor();
					Dust dust7 = Dust.NewDustPerfect(GunTipPosition, (!Positive) ? ((num2 % 2 == 0) ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>());
					dust7.velocity = ((float)Math.PI * 2f * (float)num2 / 16f).ToRotationVector2() * 16f * ((num2 % 2 == 0) ? 0.7f : 1f);
					dust7.scale = Main.rand.NextFloat(1.1f, 1.3f);
					dust7.noGravity = true;
					dust7.color = (Positive ? useColor3 : baseColor);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, Vector2.Zero, Positive ? baseColor : Color.Black, "CalamityMod/Particles/SmallBloomRingLayered", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.65f, 0.85f, 11, Positive, 1f, fade: true, 1f, (SpriteEffects)0));
				hasReachedLV1 = true;
			}
			if (ChargeLV2 && !hasReachedLV2)
			{
				SoundEngine.PlaySound(in OntologicalDespoiler.ChargeLV2, base.Projectile.Center);
				for (int num3 = 0; num3 < 24; num3++)
				{
					Color useColor4 = GetRandomColor();
					Dust dust8 = Dust.NewDustPerfect(GunTipPosition, (!Positive) ? ((num3 % 2 == 0) ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>());
					dust8.velocity = ((float)Math.PI * 2f * (float)num3 / 25f).ToRotationVector2() * 22f * ((num3 % 2 == 0) ? 0.7f : 1f);
					dust8.scale = Main.rand.NextFloat(1.45f, 1.55f);
					dust8.noGravity = true;
					dust8.color = (Positive ? useColor4 : baseColor);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, Vector2.Zero, Positive ? baseColor : Color.Black, "CalamityMod/Particles/SmallBloomRingLayered", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.85f, 1.05f, 11, Positive, 1f, fade: true, 1f, (SpriteEffects)0));
				hasReachedLV2 = true;
			}
		}
		if (!hasBegunFiring && base.Projectile.ai[2] < 10f)
		{
			base.Projectile.frame = 6;
			fireFrame = 6;
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref baseColor)).ToVector3() * 0.75f);
		}
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(OntologicalChargeSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(OntologicalChargeSlotDeep, out ActiveSound ChargeSound2))
		{
			ChargeSound2?.Stop();
		}
	}

	public Color GetRandomColor()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Color useColor = (Color)(Main.rand.Next(4) switch
		{
			0 => color1, 
			1 => color2, 
			2 => color3, 
			_ => color4, 
		});
		if (base.Owner.shirtColor == Color.White)
		{
			((Color)(ref useColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
		return useColor;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 3)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerHoldout", (AssetRequestMode)2).Value;
		Texture2D textureAlt = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerHoldout2", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 rotationPoint = frame.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (!base.Owner.CantUseHoldout())
		{
			float rumble = ((base.Projectile.ai[2] == 20f) ? 200f : MathHelper.Clamp(CurrentChargingFrames, 0f, (float)Charge2Frames));
			drawPosition += Main.rand.NextVector2Circular(rumble / 120f, rumble / 120f);
		}
		Texture2D fireTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerFlame", (AssetRequestMode)2).Value;
		Texture2D fireTexture2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerFlame2", (AssetRequestMode)2).Value;
		Rectangle frame2 = fireTexture.Frame(1, 7, 0, fireFrame);
		Vector2 rotationPoint2 = frame2.Size() * 0.5f;
		Texture2D flashTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerFlash", (AssetRequestMode)2).Value;
		Rectangle frame3 = fireTexture.Frame(1, 6, 0, flashFrame);
		Vector2 rotationPoint3 = frame3.Size() * 0.5f;
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/FlameExplosion", (AssetRequestMode)2).Value;
		Texture2D rechargeTexture2 = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		float randSize = Main.rand.NextFloat(0.9f, 1.1f);
		Color val;
		if (!hasBegunFiring)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 position = GunTipPosition - Main.screenPosition;
				val = baseColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position, null, val, base.Projectile.rotation + Main.rand.NextFloat(-30f, 30f), rechargeTexture.Size() * 0.5f, 0.028f * Utils.GetLerpValue(0f, Charge2Frames, CurrentChargingFrames, clamped: true) * randSize, (SpriteEffects)0);
			}
			Main.EntitySpriteDraw(rechargeTexture2, GunTipPosition - Main.screenPosition, null, Positive ? Color.Lerp(Color.White, Color.Black, Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true)) : Color.Black, base.Projectile.rotation + Main.rand.NextFloat(-30f, 30f), rechargeTexture2.Size() * 0.5f, 0.58f * Utils.GetLerpValue(0f, Charge2Frames, CurrentChargingFrames, clamped: true) * randSize, (SpriteEffects)0);
		}
		if (ChargeLV1 && !Positive && !hasBegunFiring)
		{
			for (int j = 0; j < 2; j++)
			{
				Vector2 placement = drawPosition + voidPlacement;
				Rectangle? sourceRectangle = frame;
				val = baseColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, placement, sourceRectangle, val * 0.3f * Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true), drawRotation, rotationPoint, new Vector2(Main.rand.NextFloat(0.7f, 1f), Main.rand.NextFloat(0.8f, 1.2f)) * base.Projectile.scale * base.Owner.gravDir * 1.1f, flipSprite);
				Main.EntitySpriteDraw(texture, placement, frame, Color.Black * Utils.GetLerpValue(Charge1Frames, Charge2Frames, CurrentChargingFrames, clamped: true), drawRotation, rotationPoint, new Vector2(Main.rand.NextFloat(0.7f, 1f), Main.rand.NextFloat(0.6f, 1f)) * base.Projectile.scale * base.Owner.gravDir, flipSprite);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		if (drawInverse && !Positive)
		{
			Main.EntitySpriteDraw(textureAlt, drawPosition, frame, Color.White * Utils.GetLerpValue(0f, 5f, inverseTimer, clamped: true), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		if (base.Projectile.frame < 6)
		{
			for (int k = 0; k < 4; k++)
			{
				Vector2 position2 = drawPosition + Main.rand.NextVector2Circular(4.5f, 4.5f);
				Rectangle? sourceRectangle2 = frame2;
				val = baseColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(fireTexture, position2, sourceRectangle2, val * 0.3f, drawRotation, rotationPoint2, base.Projectile.scale * base.Owner.gravDir, flipSprite);
			}
			Texture2D texture2 = (Positive ? fireTexture : fireTexture2);
			Vector2 position3 = drawPosition;
			Rectangle? sourceRectangle3 = frame2;
			Color color;
			if (!Positive)
			{
				color = baseColor;
			}
			else
			{
				val = baseColor;
				((Color)(ref val)).A = 0;
				color = val;
			}
			Main.EntitySpriteDraw(texture2, position3, sourceRectangle3, color, drawRotation, rotationPoint2, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		if (showFlash)
		{
			for (int l = 0; l < 4; l++)
			{
				Vector2 position4 = flashPos - Main.screenPosition + Main.rand.NextVector2Circular(4.5f, 4.5f);
				Rectangle? sourceRectangle4 = frame3;
				val = baseColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(flashTexture, position4, sourceRectangle4, val * 0.3f, flashRot, rotationPoint3, new Vector2(3f, 0.8f) * base.Projectile.scale * base.Owner.gravDir, flipSprite);
			}
			Main.EntitySpriteDraw(flashTexture, flashPos - Main.screenPosition, frame3, Color.Black, flashRot, rotationPoint3, new Vector2(3f, 0.8f) * base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		return false;
	}

	public OntologicalDespoilerHoldout()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		FramesPerLoad = 9;
		MaxLoadableShots = 23;
		BulletSpeed = 6.5f;
		ShotsLoaded = 1f;
		baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		AftershotCooldownFrames = 8;
		Charge1Frames = 156;
		Charge2Frames = 312;
		inverseTimer = 14;
		base._002Ector();
	}
}
