using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AqueousHunterDroneSummon : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int interactionPoint = 600;

	public int cooldownGiven = 450;

	public bool doingSpecialAttack;

	public float moveSpeed = 30f;

	public NPC targetedNPC;

	public float tailRot;

	public float tailRecoil;

	public int facingDir;

	public Vector2 headPlace;

	public Vector2 tailPlace;

	public float lastFirePositionX;

	public Vector2 chillSpot;

	public Vector2 spawnAnimStart;

	public Vector2 lastTargetCenter;

	public int attackDowntime;

	public int surpriseTimer;

	public float slidingDirection;

	public int animationClickTime = 70;

	public bool doSpin;

	public float specialAttackfx;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float attackTimer => ref base.Projectile.ai[0];

	public ref float interactionTimer => ref base.Projectile.ai[1];

	public bool pressedRight => base.Projectile.ai[2] == 5f;

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0f;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 4f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		_ = Owner.ownedProjectileCounts[base.Projectile.type];
		facingDir = Math.Sign(base.Projectile.velocity.X);
		GrantBuffs(Owner);
		if ((targetedNPC == null || targetedNPC.life <= 0 || !targetedNPC.CanBeChasedBy() || attackDowntime == 20) && attackDowntime < 30)
		{
			targetedNPC = base.Projectile.Center.MinionHoming(2000f, Owner);
		}
		if (time < animationClickTime + 20)
		{
			if (time == 0)
			{
				chillSpot = base.Projectile.Center;
				spawnAnimStart = base.Projectile.Center;
				int chosenDir = Math.Sign(base.Projectile.Center.DirectionTo(Owner.Center).X);
				base.Projectile.velocity.X = chosenDir * 16;
				tailRot = -2f * (float)chosenDir;
				attackTimer += 8f * base.Projectile.localAI[2];
			}
			SpawnAnim();
			facingDir = Math.Sign(base.Projectile.velocity.X);
			time++;
			return;
		}
		slidingDirection = MathHelper.Lerp(slidingDirection, (float)Owner.direction, 0.03f);
		bool isSummonWeapon = Owner.HeldItem.DamageType.CountsAsClass(DamageClass.Summon);
		if ((Owner.Calamity().mouseRight && Owner.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse && Owner.Calamity().arsenalCooldown <= 0) & isSummonWeapon)
		{
			base.Projectile.ai[2] = 5f;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == base.Projectile.type && p.owner == base.Projectile.owner)
				{
					p.ai[2] = 5f;
				}
			}
		}
		if (doingSpecialAttack)
		{
			SpecialAttack();
		}
		else
		{
			specialAttackfx = MathHelper.Lerp(specialAttackfx, 0f, 0.15f);
			if (targetedNPC != null || attackDowntime > 0)
			{
				lastTargetCenter = ((targetedNPC == null) ? Owner.Center : targetedNPC.Center);
				Attacking();
			}
			else
			{
				Passive();
				CheckForSpecialAttack();
			}
		}
		if (tailRecoil > 0f)
		{
			tailRecoil = MathHelper.Lerp(tailRecoil, 0f, 0.2f);
		}
		if (attackDowntime > 0)
		{
			attackDowntime--;
		}
		time++;
		SetVisualPlacement();
	}

	public void CheckForSpecialAttack()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (pressedRight)
		{
			base.Projectile.ai[2] = 0f;
			doingSpecialAttack = true;
			attackTimer = 0f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
			{
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 12f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.3f;
			}
			Owner.Calamity().arsenalCooldown = cooldownGiven;
			Owner.AddCooldown(ArsenalPower.ID, cooldownGiven);
		}
	}

	public void SpecialAttack()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		interactionTimer = 0f;
		int length = 33;
		specialAttackfx = MathHelper.Lerp(specialAttackfx, 1f, 0.2f);
		facingDir = Math.Sign(base.Projectile.Center.DirectionTo(Owner.Center).X);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		base.Projectile.rotation += (float)Math.PI * 6f / (float)length * (float)facingDir;
		tailRot = MathHelper.Lerp(tailRot, 0.75f * (float)(-facingDir), 0.2f);
		for (int i = -1; i <= 1; i += 2)
		{
			bool noFall = !Main.rand.NextBool(5);
			Vector2 vel = Vector2.UnitY.RotatedBy(base.Projectile.rotation * -5f) * (float)i;
			int dustStyle = (noFall ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalPlasmaDust);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + vel * 43f * specialAttackfx, dustStyle);
			dust.scale = Main.rand.NextFloat(0.8f, 1.1f) + (noFall ? 0f : 0.5f);
			dust.velocity = vel.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f) - (float)Math.PI / 4f * (float)facingDir) * Main.rand.NextFloat(4f, 5.5f);
			dust.noGravity = noFall;
			dust.color = ArsenalEffects.ArsenalPlasmaColor;
			dust.fadeIn = (noFall ? 0f : 0.7f);
		}
		if (attackTimer % 3f == 0f)
		{
			int usedTimer = (int)((facingDir == -1) ? ((float)length - attackTimer) : attackTimer);
			SoundEngine.PlaySound(AqueousHunterDrone.Fire with
			{
				Volume = 0.7f,
				Pitch = 0f,
				PitchVariance = 0.3f,
				MaxInstances = 10
			}, base.Projectile.Center);
			Vector2 shootVel = ((float)Math.PI / 20f * (float)(usedTimer / 3 - 1)).ToRotationVector2().RotatedBy(-2.356194496154785);
			Vector2 shootPlace = base.Projectile.Center + shootVel * 10f;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), shootPlace, shootVel * 8f, ModContent.ProjectileType<ShrimpPlasmaMissile>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, 0f, attackTimer + 1f);
		}
		attackTimer++;
		if (attackTimer > (float)length)
		{
			attackTimer = 8f * base.Projectile.localAI[2];
			doingSpecialAttack = false;
		}
	}

	public void SpawnAnim()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		Vector2 tailDist = Utils.RotatedBy(new Vector2((30f - Math.Abs(tailRot) * 15f) * (float)(-facingDir), 7f - Math.Abs(tailRot) * 3f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 headDist = default(Vector2);
		((Vector2)(ref headDist))._002Ector(0f, 0f);
		headPlace = base.Projectile.Center + headDist;
		if (time < animationClickTime)
		{
			tailRot += 0.05f * (float)base.Projectile.direction;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
			Projectile projectile2 = base.Projectile;
			projectile2.Center -= base.Projectile.velocity;
			spawnAnimStart = new Vector2(spawnAnimStart.X, Owner.Center.Y - 700f);
			tailPlace = chillSpot + tailDist + (Vector2.UnitY * ((float)Math.Pow(time, 2.049999952316284) * 0.085f) + Vector2.UnitX * (16f - Math.Abs(base.Projectile.velocity.X)) * 10f * (float)base.Projectile.direction);
		}
		else
		{
			tailPlace = base.Projectile.Center + tailDist;
		}
		if (time <= animationClickTime - 40)
		{
			return;
		}
		if (time == animationClickTime - 1)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy((float)Math.PI / 4f * (float)base.Projectile.direction) * 3f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
			style.Pitch = 0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			SoundStyle obj = (Main.rand.NextBool() ? AqueousHunterDrone.Sound1 : AqueousHunterDrone.Sound2);
			SoundEngine.PlaySound(obj with
			{
				Volume = 0.6f,
				Pitch = 0.3f,
				MaxInstances = 10
			}, base.Projectile.Center);
			for (int i = -9; i <= 9; i++)
			{
				int dustStyle = ArsenalEffects.ArsenalPlasmaDust;
				Dust dust = Dust.NewDustPerfect(tailPlace + Main.rand.NextVector2Circular(5f, 5f), dustStyle);
				dust.scale = Main.rand.NextFloat(0.7f, 1.3f);
				dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy((float)Math.PI * (float)((Math.Sign(i) != 1) ? 1 : 0)) * Main.rand.NextFloat(1f, 5f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
				dust.fadeIn = 0.1f;
				if (i == -1)
				{
					i++;
				}
			}
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(tailPlace, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 0.4f, ArsenalEffects.ArsenalPlasmaColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true));
			}
		}
		if (time > animationClickTime - 1)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.97f;
			tailRot = MathHelper.Lerp(tailRot, 0f, 0.1f);
		}
		else
		{
			base.Projectile.Center = Vector2.Lerp(spawnAnimStart, tailPlace, Utils.GetLerpValue(animationClickTime - 40, animationClickTime, time, clamped: true));
		}
	}

	public void Passive()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		float sine = (float)Math.Sin(((float)time + base.Projectile.localAI[2] * 3f) * 0.075f / (float)Math.PI);
		float sine2 = (float)Math.Sin(((float)time + base.Projectile.localAI[2] * 3f) * 0.075f / (float)Math.PI * 2f);
		TailRotationBasedOnMovement();
		attackTimer = 8f * base.Projectile.localAI[2];
		if (interactionTimer == (float)interactionPoint)
		{
			chillSpot = Owner.Center + Main.rand.NextVector2CircularEdge(190f, 190f);
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(0f, 0.04f);
		Vector2 destination = ((interactionTimer > (float)interactionPoint) ? (chillSpot + new Vector2(220f * sine, -90f + 130f * sine2)) : (Owner.Center + new Vector2((-50f + -85f * base.Projectile.localAI[2]) * slidingDirection, -80f + 30f * sine)));
		if (surpriseTimer == 0)
		{
			base.Projectile.velocity = (destination - base.Projectile.Center) / (moveSpeed + (float)((interactionTimer > (float)interactionPoint) ? 20 : 0) - 9f + 9f * sine2);
		}
		else
		{
			surpriseTimer--;
		}
		if (interactionTimer > (float)interactionPoint)
		{
			if (time % 25 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) - Vector2.UnitY * 20f, -Vector2.UnitY.RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(3.3f, 5.5f), "CalamityMod/Projectiles/Magic/MelterNote1", affectedByGravity: false, 32, Main.rand.NextFloat(1.2f, 1.3f), Color.Lerp(Color.Green, Color.Chartreuse, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1f, 1f)));
			}
			if ((float)time % (480f + base.Projectile.localAI[2] * 120f) == 0f && Owner.miscCounter != 0)
			{
				SoundStyle obj = (Main.rand.NextBool() ? AqueousHunterDrone.Sound1 : AqueousHunterDrone.Sound2);
				SoundEngine.PlaySound(obj with
				{
					Volume = 0.3f,
					Pitch = 0f + base.Projectile.localAI[2] * 0.1f * (float)((base.Projectile.localAI[2] % 2f != 0f) ? 1 : (-1)),
					MaxInstances = 10
				}, base.Projectile.Center);
			}
		}
		if (Owner.Center.Distance(base.Projectile.Center) > 450f && interactionTimer > (float)interactionPoint && ((Vector2)(ref Owner.velocity)).Length() > 5f)
		{
			CombatText.NewText(base.Projectile.Hitbox, ArsenalEffects.ArsenalPlasmaColor, "!", dramatic: true);
			SoundStyle style = AqueousHunterDrone.Surprise with
			{
				Volume = 0.6f,
				Pitch = Main.rand.NextFloat(-0.1f, 0.1f)
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.velocity = base.Projectile.Center.DirectionTo(Owner.Center);
			surpriseTimer = 25;
			interactionTimer = 0f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
		{
			interactionTimer++;
		}
		else if (interactionTimer > 0f && interactionTimer < (float)interactionPoint)
		{
			interactionTimer -= 2f;
		}
	}

	public void Attacking()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		interactionTimer = 0f;
		facingDir = Math.Sign(base.Projectile.Center.DirectionTo(lastTargetCenter).X);
		if (tailRecoil > 1f && doSpin)
		{
			base.Projectile.rotation += 0.35f * (float)facingDir;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(tailPlace.DirectionTo(lastTargetCenter).ToRotation() + ((facingDir == -1) ? ((float)Math.PI) : 0f) + (float)Math.PI / 4f * (float)(-facingDir), 0.18f);
		}
		if (base.Projectile.Center.Distance(lastTargetCenter) > 250f && ((Vector2)(ref base.Projectile.velocity)).Length() < 15f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity += tailPlace.DirectionTo(lastTargetCenter) * 0.3f;
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.96f;
		}
		if (lastTargetCenter.Y - 80f < tailPlace.Y)
		{
			base.Projectile.velocity.Y -= 0.7f;
		}
		Projectile projectile3 = base.Projectile;
		projectile3.velocity *= 0.99f;
		if (attackDowntime > 35)
		{
			base.Projectile.velocity.X += 0.8f * (float)Math.Sign(lastTargetCenter.X - lastFirePositionX);
			TailRotationBasedOnMovement();
		}
		else
		{
			CheckForSpecialAttack();
			tailRot = MathHelper.Lerp(tailRot, 0f, 0.1f);
		}
		if (attackTimer > 60f)
		{
			Vector2 shootVel = tailPlace.DirectionTo(lastTargetCenter);
			Vector2 shootPlace = tailPlace + shootVel * 10f;
			lastFirePositionX = base.Projectile.Center.X;
			SoundStyle style = AqueousHunterDrone.Fire with
			{
				Volume = 0.7f,
				Pitch = 0f,
				PitchVariance = 0.3f,
				MaxInstances = 10
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 13; i++)
			{
				bool noFall = !Main.rand.NextBool(5);
				float variance = Main.rand.NextFloat(-0.7f, 0.7f);
				int dustStyle = (noFall ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalPlasmaDust);
				Dust dust = Dust.NewDustPerfect(tailPlace, dustStyle);
				dust.scale = Main.rand.NextFloat(1.4f, 1.8f) - Math.Abs(variance);
				dust.velocity = shootVel.RotatedBy(variance) * Main.rand.NextFloat(9f, 9.5f) * (1f - Math.Abs(variance));
				dust.noGravity = noFall;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
				dust.fadeIn = (noFall ? 0f : 1.1f);
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(shootPlace, shootVel * 0.2f, ArsenalEffects.ArsenalPlasmaColor, "CalamityMod/Particles/BloomRing", new Vector2(0.5f, 1f), shootVel.ToRotation(), 0f, 0.6f, 23, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(shootPlace, shootVel * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 28, 0.6f, ArsenalEffects.ArsenalPlasmaColor, new Vector2(1f, 0.5f), useAddativeBlend: true, glowCenter: true));
			}
			for (int k = -1; k <= 1; k++)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), shootPlace, shootVel.RotatedBy(0.5f * (float)k) * 8f, ModContent.ProjectileType<ShrimpPlasmaMissile>(), base.Projectile.damage, 0f, base.Projectile.owner);
			}
			doSpin = Main.rand.NextBool(5);
			if (doSpin)
			{
				SoundStyle obj = (Main.rand.NextBool() ? AqueousHunterDrone.Sound1 : AqueousHunterDrone.Sound2);
				SoundEngine.PlaySound(obj with
				{
					Volume = 0.4f,
					Pitch = 0.2f,
					MaxInstances = 10
				}, base.Projectile.Center);
			}
			tailRecoil = 25f;
			Projectile projectile4 = base.Projectile;
			projectile4.velocity += shootVel * (float)(doSpin ? (-18) : (-9));
			attackTimer = 0f;
			attackDowntime = 60;
		}
		attackTimer++;
	}

	public void TailRotationBasedOnMovement()
	{
		float sine = (float)Math.Sin((float)time * 0.1f / (float)Math.PI);
		tailRot = sine * 0.2f + 1.2f * (float)Math.Pow(Utils.GetLerpValue(0f, 12 * facingDir, base.Projectile.velocity.X, clamped: true), 1.5) * (float)facingDir;
	}

	public void SetVisualPlacement()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		float sine = (float)Math.Sin((float)time * 0.35f / (float)Math.PI);
		float sine2 = (float)Math.Sin((float)time * 0.05f / (float)Math.PI);
		Vector2 shake = default(Vector2);
		((Vector2)(ref shake))._002Ector(3f * sine * (float)facingDir, 7f * sine2);
		Vector2 visualPlace = base.Projectile.Center + shake + Vector2.UnitY * ((surpriseTimer > 20) ? Utils.GetLerpValue(25f, 20f, surpriseTimer) : (1f - (float)Math.Pow(Utils.GetLerpValue(20f, 15f, surpriseTimer, clamped: true), 2.0))) * -25f;
		Vector2 tailDist = Utils.RotatedBy(new Vector2((30f - Math.Abs(tailRot) * 15f) * (float)(-facingDir), 7f - Math.Abs(tailRot) * 3f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 headDist = default(Vector2);
		((Vector2)(ref headDist))._002Ector(0f, 0f);
		Vector2 recoilPlace = (base.Projectile.rotation - ((facingDir == -1) ? ((float)Math.PI) : 0f) - (float)Math.PI / 4f * (float)(-facingDir)).ToRotationVector2() * (0f - tailRecoil);
		headPlace = visualPlace + headDist;
		tailPlace = visualPlace + tailDist + recoilPlace;
	}

	public void GrantBuffs(Player player)
	{
		bool num = base.Projectile.type == ModContent.ProjectileType<AqueousHunterDroneSummon>();
		player.AddBuff(ModContent.BuffType<AqueousHunterDroneBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				player.Calamity().aqueousHunterDrone = false;
			}
			if (player.Calamity().aqueousHunterDrone)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		Texture2D head = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/ShrimpBody", (AssetRequestMode)2).Value;
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/ShrimpBodyGlow", (AssetRequestMode)2).Value;
		Texture2D tail = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/ShrimpTail", (AssetRequestMode)2).Value;
		Texture2D glow2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/ShrimpTailGlow", (AssetRequestMode)2).Value;
		Texture2D spin = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2).Value;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		bool left = facingDir == -1;
		float rotation = base.Projectile.rotation;
		Main.EntitySpriteDraw(tail, tailPlace - Main.screenPosition, null, drawColor, rotation + tailRot, new Vector2((float)(left ? tail.Height : 0), 0f), base.Projectile.scale, (SpriteEffects)(left ? 1 : 0));
		Main.EntitySpriteDraw(glow2, tailPlace - Main.screenPosition, null, Color.White, rotation + tailRot, new Vector2((float)(left ? glow2.Height : 0), 0f), base.Projectile.scale, (SpriteEffects)(left ? 1 : 0));
		float sine = (float)Math.Sin((float)time * 0.35f / (float)Math.PI);
		float headFadeIn = (float)Math.Pow(Utils.GetLerpValue(0f, animationClickTime - 25, time, clamped: true), 8.0);
		Main.EntitySpriteDraw(head, headPlace - Main.screenPosition, null, drawColor * headFadeIn, rotation + sine * 0.1f, head.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(left ? 1 : 0));
		Main.EntitySpriteDraw(glow, headPlace - Main.screenPosition, null, Color.White * headFadeIn, rotation + sine * 0.1f, glow.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(left ? 1 : 0));
		Vector2 position = Vector2.Lerp(headPlace, tailPlace, 0f) - Main.screenPosition;
		Color val = ArsenalEffects.ArsenalPlasmaColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(spin, position, null, val * specialAttackfx, rotation * 1.8f, spin.Size() * 0.5f, base.Projectile.scale * 0.53f * specialAttackfx, (SpriteEffects)(left ? 1 : 0));
		if (tailRecoil > 0f)
		{
			float fade = Utils.GetLerpValue(0f, 15f, tailRecoil, clamped: true);
			for (int i = 0; i < 12; i++)
			{
				Vector2 vel = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2() * (2f + 2f * fade);
				Vector2 position2 = tailPlace - Main.screenPosition + vel;
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(glow2, position2, null, val * fade, rotation + tailRot, new Vector2((float)(left ? glow2.Height : 0), 0f), base.Projectile.scale, (SpriteEffects)(left ? 1 : 0));
			}
		}
		return false;
	}
}
