using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Packets.Entities;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ExaltedOathbladeThrownBlade : ModProjectile, ILocalizedModType, IModType
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

	public CalamityUtils.CurveSegment pullback;

	public CalamityUtils.CurveSegment throwout;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/ExaltedOathblade";

	public int ChargeupTime => (int)base.Projectile.localAI[2];

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public ref float stabOrder => ref base.Projectile.ai[1];

	public ref NPC stabbedTarget => ref Main.npc[(int)base.Projectile.ai[2]];

	public bool fadingOut => base.Projectile.timeLeft <= Lifetime - fadeOutTime;

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override bool ShouldUpdatePosition()
	{
		if (ChargeProgress >= 1f && !stuckInGround)
		{
			return !stuckInTarget;
		}
		return false;
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(ChargeProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.spriteDirection = base.Projectile.direction;
		Color newColor = Color.MediumOrchid;
		Vector3 Light = ((Color)(ref newColor)).ToVector3();
		Lighting.AddLight(base.Projectile.Center, Light * 0.85f);
		if (base.Projectile.timeLeft == Lifetime)
		{
			Vector2 toMouse = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			base.Projectile.velocity = toMouse * 14f;
			base.Projectile.Center = Owner.MountedCenter + toMouse * 12f;
			base.Projectile.spriteDirection = base.Projectile.direction;
			thrown = true;
			time = 0f;
			base.Projectile.extraUpdates = 2;
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
					fadeOutEffect();
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
			if (exitedTarget && !stuckInGround)
			{
				base.Projectile.extraUpdates = 2;
				if (base.Projectile.velocity.Y < 14f && base.Projectile.timeLeft < ((bounces > 0) ? Lifetime : (Lifetime - 50)))
				{
					base.Projectile.velocity.Y += 0.35f;
				}
			}
			if (fadingOut)
			{
				fadeOutEffect();
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
					GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + safeVel * 75f, -dustVel, -base.Projectile.velocity, "CalamityMod/Particles/BloomCircle", 9, 0.2f, (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet) * 0.65f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 1f, 0.2f));
				}
				if (exitedTarget && j == 0 && Main.rand.NextBool())
				{
					Vector2 position = base.Projectile.Center + safeVel.RotatedBy(base.Projectile.rotation - MathHelper.ToRadians((float)(base.Projectile.direction * 45))) * 45f;
					int type = ModContent.DustType<LightDust>();
					Vector2? velocity = dustVel * Main.rand.NextFloat(1f, 4f);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(1.2f, 1.4f));
					dust.noGravity = true;
					dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
				}
			}
			if (Main.rand.NextBool(4))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 0.6f), "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 17, Main.rand.NextFloat(0.2f, 0.3f), Color.Lerp(Color.MediumOrchid, Color.BlueViolet, Main.rand.NextFloat(0f, 0.7f)) * 0.6f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-1f, 1f)));
			}
		}
		if (ChargeProgress < 1f)
		{
			throwAnimation();
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

	public void fadeOutEffect()
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

	public void throwAnimation()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
		float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
		Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.spriteDirection = Owner.direction;
		base.Projectile.direction = Owner.direction;
		base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -65f * Owner.gravDir;
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
		int bonusDamage = 100;
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
			if (target.Calamity().demonSwordImpales >= 4)
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
				for (int i = 0; i < 12; i++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, base.Projectile.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.4f, 2.5f), affectedByGravity: false, 50, Main.rand.NextFloat(0.2f, 1.3f), Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet));
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.3) * Main.rand.NextFloat(0.5f, 2f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.6f));
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
		float minMult = 0.45f;
		int hitsToMinMult = 9;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (exitedTarget ? 1.15f : 1f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (exitedTarget)
		{
			if (bounces >= 2)
			{
				impaleGround(oldVelocity);
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
				base.Projectile.velocity = base.Projectile.velocity.RotatedByRandom(0.20000000298023224);
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
			impaleGround(oldVelocity);
		}
		base.Projectile.netUpdate = true;
		return false;
	}

	public void impaleGround(Vector2 oldVelocity)
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
		return (base.Projectile.numHits <= 0 || exitedTarget) && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / (exitedTarget ? 0.25f : 1f), targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ExaltedOathblade", (AssetRequestMode)2).Value;
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
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
		if (exitedTarget && !stuckInGround)
		{
			Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
			Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
			Texture2D value = p2.Value;
			val = Color.BlueViolet;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.45f, base.Projectile.rotation * Main.rand.NextFloat(1.6f, 1.7f), p2.Size() * 0.5f, 1.2f * Main.rand.NextFloat(0.8f, 1.15f), (SpriteEffects)0);
			Texture2D value2 = p.Value;
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, generalDrawPos, null, val * 0.65f, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f), p.Size() * 0.5f, 0.95f, (SpriteEffects)0);
		}
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		val = Color.BlueViolet;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(centerTexture, position, null, Color.Lerp(val, lightColor, base.Projectile.Opacity) * base.Projectile.Opacity, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		return false;
	}

	public ExaltedOathbladeThrownBlade()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Lifetime = 150;
		fadeOutTime = 120;
		tipPosition = Vector2.Zero;
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
