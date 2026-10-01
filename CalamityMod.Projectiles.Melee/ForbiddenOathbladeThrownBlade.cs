using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Packets.Entities;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ForbiddenOathbladeThrownBlade : ModProjectile, ILocalizedModType, IModType
{
	public int Lifetime;

	public bool thrown;

	public int fadeOutTime;

	public bool stuckInTarget;

	public int stuckTimer;

	public bool exitedTarget;

	public bool stuckInGround;

	public Vector2 impalePos;

	public int bounces;

	public Vector2 tipPosition;

	private bool visualChargeInitialized;

	private int visualChargeTimer;

	private int visualChargeTime;

	public CalamityUtils.CurveSegment pullback;

	public CalamityUtils.CurveSegment throwout;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/ForbiddenOathblade";

	public int ChargeupTime => (int)base.Projectile.localAI[2];

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public ref float stabOrder => ref base.Projectile.ai[1];

	public ref NPC stabbedTarget => ref Main.npc[(int)base.Projectile.ai[2]];

	public bool fadingOut => base.Projectile.timeLeft <= Lifetime - fadeOutTime;

	private Vector2 GetAimDirection()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 val = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			float aimAngle = val.ToRotation();
			if (Math.Abs(MathHelper.WrapAngle(aimAngle - base.Projectile.localAI[1])) > 0.02f)
			{
				base.Projectile.localAI[1] = aimAngle;
				if (base.Projectile.timeLeft % 6 == 0)
				{
					base.Projectile.netUpdate = true;
				}
			}
			return val;
		}
		return base.Projectile.localAI[1].ToRotationVector2();
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 toMouse = (Owner.Calamity().mouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			base.Projectile.localAI[1] = toMouse.ToRotation();
			base.Projectile.netUpdate = true;
		}
	}

	public override bool ShouldUpdatePosition()
	{
		if (ChargeProgress >= 1f && !stuckInGround)
		{
			return !stuckInTarget;
		}
		return false;
	}

	internal float ArmAnticipationMovement(float progress)
	{
		return CalamityUtils.PiecewiseAnimation(progress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		bool isOwner = Main.myPlayer == base.Projectile.owner;
		if (!visualChargeInitialized && !isOwner)
		{
			int fallbackCharge = (int)MathHelper.Clamp((float)Owner.HeldItem.useTime / 2.8f, 1f, 100f);
			visualChargeTime = (int)((base.Projectile.localAI[2] > 0f) ? base.Projectile.localAI[2] : ((float)fallbackCharge));
			visualChargeTimer = visualChargeTime;
			visualChargeInitialized = true;
		}
		if (!thrown && ChargeProgress >= 1f)
		{
			thrown = true;
		}
		if (thrown && !stuckInGround && !stuckInTarget)
		{
			base.Projectile.extraUpdates = 1;
		}
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.spriteDirection = base.Projectile.direction;
		Color newColor = Color.MediumOrchid;
		Vector3 Light = ((Color)(ref newColor)).ToVector3();
		Lighting.AddLight(base.Projectile.Center, Light * 0.5f);
		if (base.Projectile.timeLeft == Lifetime && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 toMouse = GetAimDirection();
			base.Projectile.velocity = toMouse * 14f;
			base.Projectile.Center = Owner.MountedCenter + toMouse * 12f;
			base.Projectile.spriteDirection = base.Projectile.direction;
			thrown = true;
			time = 0f;
			base.Projectile.extraUpdates = 1;
			base.Projectile.tileCollide = true;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else
		{
			base.Projectile.direction = -1;
		}
		if (thrown)
		{
			base.Projectile.spriteDirection = base.Projectile.direction;
			if (!stuckInGround && exitedTarget)
			{
				base.Projectile.rotation += 0.35f * (MathF.Abs(base.Projectile.velocity.Y) * 0.03f + 0.85f) * Main.rand.NextFloat(0.7f, 1f) * (float)base.Projectile.direction * base.Projectile.Opacity;
			}
			else
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f * (float)((base.Projectile.direction == 1) ? 1 : 3);
			}
			if (time > (float)fadeOutTime * 0.7f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.93f;
			}
			if (stuckInTarget)
			{
				base.Projectile.tileCollide = false;
				time--;
				base.Projectile.timeLeft++;
				if (stuckTimer > 0)
				{
					stuckTimer--;
				}
				else
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.01f;
					stabbedTarget.Calamity().demonSwordImpales--;
					stuckInTarget = false;
					FadeOutEffect();
				}
				base.Projectile.Center = stabbedTarget.Center + impalePos;
				if (stabbedTarget.life <= 0 || stabbedTarget == null || base.Projectile.localAI[0] == 5f || !stabbedTarget.active)
				{
					base.Projectile.timeLeft = Lifetime;
					stabbedTarget.Calamity().demonSwordImpales--;
					stuckInTarget = false;
					exitedTarget = true;
					base.Projectile.tileCollide = true;
					base.Projectile.rotation += Main.rand.NextFloat(-1.5f, 1.5f);
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						base.Projectile.localNPCImmunity[i] = 0;
					}
					base.Projectile.numHits = 0;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);
					style.Volume = 0.85f;
					style.Pitch = Main.rand.NextFloat(0.3f, 0.4f);
					style.MaxInstances = 3;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (exitedTarget && !stuckInGround && base.Projectile.velocity.Y < 14f && base.Projectile.timeLeft < ((bounces == 1) ? (Lifetime - 20) : Lifetime))
			{
				base.Projectile.velocity.Y += 0.1f * (float)(bounces * 3);
			}
			if (fadingOut)
			{
				FadeOutEffect();
			}
		}
		if (!fadingOut && !stuckInTarget && !stuckInGround && ChargeProgress >= 1f)
		{
			for (int j = 0; j < 2; j++)
			{
				Vector2 safeVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
				Vector2 dustVel = (exitedTarget ? safeVel.RotatedBy(MathHelper.ToRadians((float)(-90 * base.Projectile.direction)) + base.Projectile.rotation) : (safeVel.RotatedBy(MathHelper.ToRadians((float)(90 * ((j == 0) ? 1 : (-1))))).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(3f, 4f)));
				if (!exitedTarget && Main.rand.NextBool(3))
				{
					Vector2 position = base.Projectile.Center + safeVel * 60f;
					int type = ModContent.DustType<LightDust>();
					Vector2? velocity = dustVel;
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.9f, 1.1f));
					dust.noGravity = true;
					dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
					dust.noLight = true;
					dust.noLightEmittence = true;
					dust.alpha = 50;
				}
				if (exitedTarget && j == 0)
				{
					Vector2 position2 = base.Projectile.Center + safeVel.RotatedBy(base.Projectile.rotation - MathHelper.ToRadians((float)(base.Projectile.direction * 45))) * 45f;
					Vector2? velocity2 = dustVel * Main.rand.NextFloat(1f, 4f);
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, 278, velocity2, 0, newColor, Main.rand.NextFloat(0.4f, 0.6f));
					dust2.noGravity = true;
					dust2.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
				}
			}
			if (Main.rand.NextBool(4))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 0.6f), "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 17, Main.rand.NextFloat(0.2f, 0.3f), Color.Lerp(Color.MediumOrchid, Color.BlueViolet, Main.rand.NextFloat(0f, 0.7f)) * 0.6f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-1f, 1f)));
			}
		}
		float chargeProgress = ChargeProgress;
		if (!isOwner && ((Vector2)(ref base.Projectile.velocity)).LengthSquared() < 0.01f && visualChargeTimer > 0)
		{
			chargeProgress = 1f - (float)visualChargeTimer / (float)visualChargeTime;
			visualChargeTimer--;
		}
		if (chargeProgress < 1f)
		{
			throwAnimation(chargeProgress);
			if (ChargeProgress >= 0.6f && time == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordSwing", 2);
				style.Volume = 0.65f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				time++;
			}
		}
		else
		{
			time++;
		}
	}

	public void FadeOutEffect()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = false;
		if (base.Projectile.timeLeft > Lifetime - fadeOutTime)
		{
			base.Projectile.timeLeft = Lifetime - fadeOutTime;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, Lifetime - fadeOutTime, base.Projectile.timeLeft, clamped: true);
		Vector2 dustVel = Utils.RotatedByRandom(new Vector2(10f, 10f), Math.PI) * Main.rand.NextFloat(0.2f, 1f) * base.Projectile.Opacity;
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), dustVel, 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.alpha = 100;
			dust.velocity += base.Projectile.velocity;
		}
	}

	public void throwAnimation(float chargeProgress)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimDirection = GetAimDirection();
		Owner.ChangeDir(MathF.Sign(aimDirection.X));
		float armRotation = ArmAnticipationMovement(chargeProgress) * (float)Owner.direction;
		Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.spriteDirection = Owner.direction;
		base.Projectile.direction = Owner.direction;
		base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -55f * Owner.gravDir;
		base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		int bonusDamage = 60;
		if (target.Calamity().demonicFlamesBonusDamage <= bonusDamage)
		{
			target.Calamity().demonicFlamesBonusDamage = bonusDamage;
			target.AddBuff(ModContent.BuffType<DemonicFlames>(), 120);
			if (Main.netMode != 0)
			{
				DemonicFlamesSyncPacket.Send(target);
			}
		}
		if (!exitedTarget)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordImpact", 2);
			style.Volume = 0.75f;
			style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
			style.MaxInstances = 3;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.ai[2] = target.whoAmI;
			if (target.Calamity().demonSwordImpales < 0)
			{
				target.Calamity().demonSwordImpales = 0;
			}
			if (target.Calamity().demonSwordImpales >= 3)
			{
				float bladeValue = -1f;
				Projectile ejectedBlade = null;
				for (int x = 0; x < Main.maxProjectiles; x++)
				{
					Projectile projectile = Main.projectile[x];
					if (projectile.owner == base.Projectile.owner && projectile.type == base.Projectile.type && base.Projectile.localAI[0] != 5f && projectile.ai[2] == base.Projectile.ai[2] && projectile.timeLeft > Lifetime - fadeOutTime && (bladeValue == -1f || bladeValue > projectile.ai[1]))
					{
						bladeValue = projectile.ai[1];
						ejectedBlade = projectile;
					}
				}
				ejectedBlade.localAI[0] = 5f;
				ejectedBlade.ai[1] += 1000f;
				ejectedBlade.velocity = base.Projectile.velocity.RotatedByRandom(0.11999999731779099);
				for (int i = 0; i < 10; i++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 1.5f), affectedByGravity: false, 45, Main.rand.NextFloat(0.3f, 1f), Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet));
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.3f, 1f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.6f));
					dust.noGravity = true;
					dust.noLight = true;
					dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
				}
			}
			target.Calamity().demonSwordImpales++;
			stuckInTarget = true;
			impalePos = base.Projectile.Center - stabbedTarget.Center;
			stuckTimer = 3600;
		}
		base.Projectile.netUpdate = true;
		if (Main.netMode != 0)
		{
			DemonSwordImpalesSyncPacket.Send(target);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.4f;
		int hitsToMinMult = 8;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (exitedTarget ? 1.15f : 1f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (exitedTarget)
		{
			if (bounces >= 2)
			{
				ImpaleGround(oldVelocity);
			}
			else
			{
				if (base.Projectile.velocity.X != oldVelocity.X)
				{
					base.Projectile.velocity.X = 0f - oldVelocity.X;
				}
				if (base.Projectile.velocity.Y != oldVelocity.Y)
				{
					base.Projectile.velocity.Y = 0f - oldVelocity.Y;
				}
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
				{
					base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f;
				}
				base.Projectile.velocity.Y *= 0.85f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2);
				style.Volume = 0.35f;
				style.Pitch = Main.rand.NextFloat(-0.4f, -0.5f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				bounces++;
				base.Projectile.timeLeft = Lifetime;
			}
		}
		else
		{
			ImpaleGround(oldVelocity);
		}
		base.Projectile.netUpdate = true;
		return false;
	}

	public void ImpaleGround(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = oldVelocity;
		stuckInGround = true;
		base.Projectile.timeLeft = (int)((float)Lifetime - (float)fadeOutTime * 0.3f);
		base.Projectile.tileCollide = false;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordImpact", 2);
		style.Volume = 0.75f;
		style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
		style.MaxInstances = 3;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		base.Projectile.netUpdate = true;
	}

	public override bool? CanDamage()
	{
		if (ChargeProgress < 1f || fadingOut || stuckInGround || stuckInTarget || (base.Projectile.numHits > 0 && !exitedTarget))
		{
			return false;
		}
		return base.CanDamage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		return (base.Projectile.numHits <= 0 || exitedTarget) && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / (exitedTarget ? 0.5f : 1f), targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ForbiddenOathblade", (AssetRequestMode)2).Value;
		_ = base.Projectile.Center - Main.screenPosition;
		float fadeScale = 1f - base.Projectile.Opacity;
		Color val;
		for (int i = 0; i < 16; i++)
		{
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.4f * fadeScale * base.Projectile.Opacity;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * 9f * fadeScale;
			Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset, null, auraColor, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		}
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		val = Color.BlueViolet;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(centerTexture, position, null, Color.Lerp(val, lightColor, base.Projectile.Opacity) * base.Projectile.Opacity, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		byte state = 0;
		if (thrown)
		{
			state |= 1;
		}
		if (stuckInTarget)
		{
			state |= 2;
		}
		if (exitedTarget)
		{
			state |= 4;
		}
		if (stuckInGround)
		{
			state |= 8;
		}
		writer.Write(state);
		writer.Write(base.Projectile.rotation);
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(base.Projectile.localAI[2]);
		writer.Write(impalePos.X);
		writer.Write(impalePos.Y);
		writer.Write(stuckTimer);
		writer.Write((short)bounces);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		byte state = reader.ReadByte();
		thrown = (state & 1) != 0;
		stuckInTarget = (state & 2) != 0;
		exitedTarget = (state & 4) != 0;
		stuckInGround = (state & 8) != 0;
		base.Projectile.rotation = reader.ReadSingle();
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		base.Projectile.localAI[2] = reader.ReadSingle();
		impalePos = new Vector2(reader.ReadSingle(), reader.ReadSingle());
		stuckTimer = reader.ReadInt32();
		bounces = reader.ReadInt16();
	}

	public ForbiddenOathbladeThrownBlade()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Lifetime = 120;
		fadeOutTime = 80;
		tipPosition = Vector2.Zero;
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
