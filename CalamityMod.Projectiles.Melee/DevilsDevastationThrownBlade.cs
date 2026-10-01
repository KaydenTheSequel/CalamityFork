using System;
using System.Collections.Generic;
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

public class DevilsDevastationThrownBlade : ModProjectile, ILocalizedModType, IModType
{
	public int Lifetime;

	public Color clr;

	public Color usedColor;

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

	public override string Texture => "CalamityMod/Items/Weapons/Melee/DevilsDevastation";

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
		base.Projectile.width = 35;
		base.Projectile.height = 35;
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
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
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
		if (thrown && !stuckInGround && !stuckInTarget && !exitedTarget)
		{
			base.Projectile.extraUpdates = 3;
		}
		float rate = (Main.GlobalTimeWrappedHourly + time * 3f) * 2f;
		List<Color> eColors = new List<Color>
		{
			clr,
			Color.BlueViolet
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		usedColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.spriteDirection = base.Projectile.direction;
		Color newColor = Color.MediumOrchid;
		Vector3 Light = ((Color)(ref newColor)).ToVector3();
		Lighting.AddLight(base.Projectile.Center, Light * 0.85f);
		if (base.Projectile.timeLeft == Lifetime && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 toMouse = GetAimDirection();
			base.Projectile.velocity = toMouse * 14f;
			base.Projectile.Center = Owner.MountedCenter + toMouse * 30f;
			base.Projectile.spriteDirection = base.Projectile.direction;
			thrown = true;
			time = 0f;
			base.Projectile.extraUpdates = 3;
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
				base.Projectile.rotation += 0.15f * (MathF.Abs(base.Projectile.velocity.Y) * 0.03f + 0.85f) * Main.rand.NextFloat(0.7f, 1f) * (float)base.Projectile.direction * base.Projectile.Opacity;
			}
			else
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f * (float)((base.Projectile.direction == 1) ? 1 : 3);
			}
			if (time > (float)fadeOutTime * 0.7f && !exitedTarget)
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
					base.Projectile.rotation += Main.rand.NextFloat(-1.2f, 1.2f);
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
			if (exitedTarget && !stuckInGround)
			{
				base.Projectile.extraUpdates = 5;
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.987f;
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
				Vector2 dustVel = (exitedTarget ? safeVel.RotatedBy(MathHelper.ToRadians((float)(-90 * base.Projectile.direction)) + base.Projectile.rotation) : (safeVel.RotatedBy(MathHelper.ToRadians((float)(70 * ((j == 0) ? 1 : (-1))))) * 6f));
				if (!exitedTarget)
				{
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + safeVel * 95f, -dustVel, -base.Projectile.velocity, "CalamityMod/Particles/BloomCircle", 5, 0.2f, clr * 0.95f, new Vector2(1.2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 1f, 0.22f));
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + safeVel * 95f, -dustVel, -base.Projectile.velocity, "CalamityMod/Particles/BloomCircle", 10, 0.2f, (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet) * 0.65f, new Vector2(1.2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 1f, 0.22f));
				}
				float rot = base.Projectile.rotation + (float)Math.PI * 2f * (float)j;
				Vector2 vel = (-base.Projectile.velocity).MoveTowards(Utils.RotatedBy(new Vector2(0f, -130f), (double)rot, default(Vector2)).RotatedBy(-1.3f * (float)base.Projectile.direction), Utils.GetLerpValue(5f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true));
				if (exitedTarget && j == 0)
				{
					Vector2 position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f), (double)rot, default(Vector2));
					int type = (Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, null, 0, newColor);
					dust.noGravity = dust.type != 278;
					dust.scale = ((dust.type == 278) ? 0.75f : 0.9f);
					dust.color = (Main.rand.NextBool() ? Color.BlueViolet : clr);
					dust.velocity = (vel * 2f).RotatedByRandom(0.4000000059604645);
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
			ThrowAnimation(chargeProgress);
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
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
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
			dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : clr);
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.alpha = 100;
			dust.velocity += base.Projectile.velocity;
		}
	}

	public void ThrowAnimation(float chargeProgress)
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
		base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -80f * Owner.gravDir;
		base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		int bonusDamage = 1000;
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
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteSmallHit", 3);
			style.Volume = 0.65f;
			style.Pitch = Main.rand.NextFloat(0.4f, 0.5f);
			style.MaxInstances = 3;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordImpact", 2);
			style.Volume = 0.8f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.2f);
			style.MaxInstances = 3;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.ai[2] = target.whoAmI;
			if (target.Calamity().demonSwordImpales < 0)
			{
				target.Calamity().demonSwordImpales = 0;
			}
			if (target.Calamity().demonSwordImpales >= 5)
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
				ejectedBlade.velocity = base.Projectile.velocity;
				for (int i = 0; i < 12; i++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, base.Projectile.velocity.RotatedByRandom(0.45) * Main.rand.NextFloat(2.5f, 4.5f), affectedByGravity: false, 50, Main.rand.NextFloat(0.2f, 1.3f), Main.rand.NextBool() ? clr : Color.BlueViolet));
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.35) * Main.rand.NextFloat(2f, 4f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.6f));
					dust.noGravity = true;
					dust.noLight = true;
					dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : clr);
				}
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, base.Projectile.velocity.RotatedByRandom(0.3) * Main.rand.NextFloat(0.4f, 2.5f), affectedByGravity: false, 50, Main.rand.NextFloat(0.2f, 1.3f), Main.rand.NextBool() ? Color.MediumOrchid : clr));
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.2) * Main.rand.NextFloat(0.5f, 2f), 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
					dust2.noGravity = false;
					dust2.noLight = true;
					dust2.color = (Main.rand.NextBool() ? clr : Color.BlueViolet);
				}
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 2f).RotatedByRandom(0.30000001192092896), ModContent.ProjectileType<DevilsStrike>(), 0, 0f, base.Projectile.owner, 0.8f, 1f).timeLeft = 200;
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
		float minMult = 0.45f;
		int hitsToMinMult = 9;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (exitedTarget ? 1.15f : 1f);
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
		return (base.Projectile.numHits <= 0 || exitedTarget) && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / (exitedTarget ? 0.3f : 1f), targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/DevilsDevastation", (AssetRequestMode)2).Value;
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		Color clr = Color.Lerp(Color.DeepPink, Color.Orange, 0.35f);
		float fadeScale = 1f - base.Projectile.Opacity + (stuckInTarget ? 0.55f : 0f);
		Color val;
		for (int i = 0; i < 16; i++)
		{
			val = usedColor;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.4f * fadeScale * base.Projectile.Opacity;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * 9f * fadeScale + Main.rand.NextVector2Circular(3f, 3f);
			Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset, null, auraColor, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		}
		if (exitedTarget && !stuckInGround)
		{
			Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire3", (AssetRequestMode)2);
			Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
			Texture2D value = p2.Value;
			val = clr;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.65f, base.Projectile.rotation * Main.rand.NextFloat(1.6f, 1.7f), p2.Size() * 0.5f, 1.4f * Main.rand.NextFloat(0.8f, 1.15f), (SpriteEffects)(base.Projectile.direction == -1));
			Texture2D value2 = p.Value;
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, generalDrawPos, null, val * 0.75f, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f), p.Size() * 0.5f, 1.25f, (SpriteEffects)(base.Projectile.direction == -1));
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

	public DevilsDevastationThrownBlade()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Lifetime = 300;
		clr = Color.Lerp(Color.DeepPink, Color.Orange, 0.5f);
		usedColor = Color.White;
		fadeOutTime = 240;
		tipPosition = Vector2.Zero;
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
