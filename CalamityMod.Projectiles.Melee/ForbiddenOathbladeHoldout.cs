using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Packets.Entities;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class ForbiddenOathbladeHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public float hitboxMult = 1.3f;

	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing = true;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int swingCount;

	public bool finalFlip;

	public bool playSwingSound = true;

	public bool holding = true;

	public int postSwingCooldown;

	public bool willDie;

	public bool hasLaunchedBlades;

	public bool swooshFade;

	private int lastSwingId;

	public override int AssignedItemID => ModContent.ItemType<ForbiddenOathblade>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<ForbiddenOathblade>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/ForbiddenOathblade";

	public override float HitboxOutset => 60f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(120f, 120f) * hitboxMult;
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-3f, 74f);
		}
	}

	public int postSwingCooldownMax => (int)((float)useAnim * 0.65f);

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		IgnoreActiveAnimation = true;
		DrawUnconditionally = true;
		CanHit = false;
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1.15f;
		base.Projectile.ai[1] = -1f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			mousePos = Owner.Calamity().mouseWorld;
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			base.Projectile.netUpdate = true;
		}
		else
		{
			Vector2 syncedDelta = Owner.Calamity().mouseWorldDeltaFromPlayer;
			if (((Vector2)(ref syncedDelta)).LengthSquared() > 0.001f)
			{
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			}
			else
			{
				aimVel = Vector2.UnitX * (float)Owner.direction * 65f;
			}
			mousePos = Owner.Center - aimVel;
		}
		useAnim = (int)((float)Owner.HeldItem.useAnimation / Owner.GetTotalAttackSpeed<MeleeDamageClass>());
		postSwingCooldown = postSwingCooldownMax / 2;
		lastSwingId = (int)base.Projectile.ai[0];
		if (mousePos.X < Owner.Center.X)
		{
			Owner.direction = -1;
		}
		else
		{
			Owner.direction = 1;
		}
		FlipAsSword = Owner.direction == -1;
	}

	public override void OnKill(int timeLeft)
	{
		Owner.Calamity().demonSwordKillMode = false;
	}

	public override void UseStyle()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		bool isOwner = Main.myPlayer == base.Projectile.owner;
		bool hasKillMode = Owner.Calamity().cooldowns.TryGetValue(KillMode.ID, out var killModeCD);
		if (!isOwner)
		{
			Vector2 syncedDelta = Owner.Calamity().mouseWorldDeltaFromPlayer;
			if (((Vector2)(ref syncedDelta)).LengthSquared() > 0.001f)
			{
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			}
		}
		if (isOwner && (Main.mouseLeft || (hasKillMode && killModeCD.timeLeft == KillMode.cooldownMax + 1)) && holding && postSwingCooldown == 0)
		{
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			base.Projectile.ai[0]++;
			base.Projectile.netUpdate = true;
		}
		int swingId = (int)base.Projectile.ai[0];
		if (swingId != lastSwingId && holding && postSwingCooldown == 0)
		{
			Animation = (int)((float)useAnim * 0.7f);
			holding = false;
			if (isOwner)
			{
				killModeCD.timeLeft = KillMode.cooldownMax;
				Owner.Calamity().killModeCooldown = KillMode.cooldownMax - 1;
			}
			swingCount++;
			lastSwingId = swingId;
		}
		if (postSwingCooldown > 0)
		{
			postSwingCooldown--;
		}
		else if (willDie)
		{
			if (isOwner)
			{
				if (hasKillMode)
				{
					killModeCD.timeLeft = KillMode.cooldownMax;
				}
				Owner.Calamity().killModeCooldown = KillMode.cooldownMax;
				Owner.Calamity().demonSwordKillMode = false;
			}
			DrawUnconditionally = false;
			base.Projectile.Kill();
			return;
		}
		if (isOwner && killModeCD.timeLeft < KillMode.cooldownMax)
		{
			killModeCD.timeLeft = KillMode.cooldownMax;
			Owner.Calamity().killModeCooldown = KillMode.cooldownMax;
		}
		if (holding)
		{
			Animation--;
		}
		AnimationProgress = Animation % (float)useAnim;
		if (isOwner && !holding && Main.netMode != 0 && (int)Animation % 3 == 0)
		{
			base.Projectile.netUpdate = true;
		}
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimVel;
		}
		else
		{
			mousePos = (isOwner ? Owner.Calamity().mouseWorld : (Owner.Center - aimVel));
		}
		if (CanHit && !swooshFade)
		{
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.5f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.23f);
		}
		if (!doSwing)
		{
			base.Projectile.ai[1] = 0f - base.Projectile.ai[1];
			holding = true;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				base.Projectile.localNPCImmunity[i] = 0;
			}
			base.Projectile.numHits = 0;
			if (isOwner)
			{
				mousePos = Owner.Calamity().mouseWorld;
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			}
			else
			{
				mousePos = Owner.Center - aimVel;
			}
			CanHit = false;
			if (mousePos.X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			FlipAsSword = Owner.direction == -1;
			doSwing = true;
			finalFlip = false;
			playSwingSound = true;
			hasLaunchedBlades = false;
			if (isOwner && !Owner.Calamity().demonSwordKillMode && postSwingCooldown == 0)
			{
				if (!willDie)
				{
					base.Projectile.netUpdate = true;
				}
				willDie = true;
			}
			postSwingCooldown = postSwingCooldownMax;
		}
		else
		{
			if (!CanHit && !postSwing)
			{
				if (mousePos.X < Owner.Center.X)
				{
					Owner.direction = -1;
				}
				else
				{
					Owner.direction = 1;
				}
			}
			else if ((Owner.Center - aimVel).X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.1f);
			if (holding)
			{
				if (isOwner)
				{
					aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				}
				CanHit = false;
				postSwing = false;
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction * (1f + Utils.GetLerpValue((float)useAnim * 0.7f, useAnim, Animation, clamped: true) * 0.35f)), 0.2f);
			}
			else if (!willDie)
			{
				if (!finalFlip)
				{
					FlipAsSword = Owner.direction < 0;
				}
				float time = AnimationProgress - (float)(useAnim / 3);
				float timeMax = useAnim - useAnim / 3;
				if (time >= (float)(int)(timeMax * 0.4f) && playSwingSound)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordSwing", 2);
					style.Volume = 0.85f;
					style.Pitch = Main.rand.NextFloat(-0.4f, -0.5f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/HeavySwing");
					style.Volume = 0.65f;
					style.Pitch = Main.rand.NextFloat(0.4f, 0.5f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					playSwingSound = false;
					if (isOwner)
					{
						Owner.Calamity().demonSwordKillMode = false;
					}
				}
				if (time > (float)(int)(timeMax * 0.2f) && time < (float)(int)(timeMax * 0.9f))
				{
					CanHit = true;
				}
				else
				{
					CanHit = false;
				}
				if (time > (float)(int)(timeMax * 0.7f))
				{
					swooshFade = true;
				}
				else
				{
					swooshFade = false;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * base.Projectile.ai[1] * (float)Owner.direction, 120f * (0f - base.Projectile.ai[1]) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(time / timeMax * 0.9f, 1))), 0.2f);
				if (time >= timeMax * 0.9f)
				{
					doSwing = false;
				}
				if (time < (float)(int)(timeMax * 0.75f))
				{
					postSwing = true;
				}
				if (CanHit)
				{
					for (int j = 0; j < 4; j++)
					{
						Dust dust = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(123f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(3.1415927410125732) * 0.6f, 0, default(Color), Main.rand.NextFloat(1.15f, 1.5f));
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
					}
					float randRot = Main.rand.NextFloat(-30f, -60f);
					Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 8f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + Utils.RotatedBy(new Vector2(123f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), dustVel, "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 23, Main.rand.NextFloat(0.23f, 0.36f), Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		bool hasKillMode = Owner.Calamity().cooldowns.TryGetValue(KillMode.ID, out var killModeCD);
		int bonusDamage = 120;
		if (target.Calamity().demonicFlamesBonusDamage <= bonusDamage)
		{
			target.Calamity().demonicFlamesBonusDamage = bonusDamage;
			target.AddBuff(ModContent.BuffType<DemonicFlames>(), 180);
			if (Main.netMode != 0)
			{
				DemonicFlamesSyncPacket.Send(target);
			}
		}
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, 17f, ignoreKBImmune: true);
		int dustNum = MathHelper.Clamp(12 - base.Projectile.numHits * 3, 3, 12);
		for (int i = 0; i < dustNum; i++)
		{
			float variance = Main.rand.NextFloat(-0.5f, 0.5f);
			int dustStyle = 278;
			Dust dust = Dust.NewDustPerfect(target.Center, dustStyle, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(1.2f, 1.4f) - Math.Abs(variance);
			dust.velocity = (launchVel * 25f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
		}
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.BlueViolet, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f * (float)(j + 1), 1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.35f * (float)(j + 1), 0.5f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		if (!hasLaunchedBlades)
		{
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (projectile.active && projectile.type == ModContent.ProjectileType<ForbiddenOathbladeThrownBlade>() && projectile.ai[2] == (float)target.whoAmI && projectile.localAI[0] != 5f)
				{
					projectile.owner = Owner.whoAmI;
					projectile.localAI[0] = 5f;
					projectile.velocity = (launchVel * 16.5f).RotatedByRandom(0.25);
					Owner.Calamity().demonSwordKillMode = true;
					if (hasKillMode)
					{
						killModeCD.timeLeft = KillMode.cooldownMax + KillMode.buffMax;
					}
					Owner.Calamity().killModeCooldown = KillMode.cooldownMax + KillMode.buffMax;
					hasLaunchedBlades = true;
				}
			}
		}
		if (base.Projectile.numHits == 0)
		{
			Owner.SetScreenshake(5.5f);
			for (int k = 0; k < 4; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center + launchVel * 15f, launchVel.RotatedBy((0.15f - 0.05f * (float)k) * (float)((k % 2 != 0) ? 1 : (-1))) * (float)(10 + 10 * k), "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 11, 0.7f - 0.15f * (float)k, Color.MediumOrchid, new Vector2(1.5f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians((float)((k % 2 == 0) ? 90 : 0)), fadeIn: false, affectedByLight: false, (k % 2 == 0) ? (-0.8f) : 0.8f));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordStrongImpact");
			style.Volume = 1f;
			style.Pitch = MathHelper.Clamp((float)swingCount * 0.05f, -0.1f, 0.65f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.3f;
		int hitsToMinMult = 7;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		if (useAnim > 0 || DrawUnconditionally)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			float deathFade = (willDie ? Utils.GetLerpValue(0f, postSwingCooldownMax, postSwingCooldown, clamped: true) : 1f);
			Color val;
			for (int i = 0; i < 20; i++)
			{
				val = Color.MediumOrchid;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.18f * (willDie ? deathFade : fadeIn);
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 20f).ToRotationVector2() * 6f * (willDie ? ((1f - deathFade) * 2f) : fadeIn);
				Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Texture2D value = swoosh.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			val = Color.BlueViolet;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * fadeIn * 0.65f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 == 0) ? (-70) : 70)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, base.Projectile.scale * 0.29f * hitboxMult, (SpriteEffects)0);
			Texture2D value2 = tex.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			Rectangle? sourceRectangle = tex.Frame(1, FrameCount, 0, Frame);
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position2, sourceRectangle, Color.Lerp(val, lightColor, deathFade) * deathFade, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(willDie);
		writer.Write7BitEncodedInt(useAnim);
		writer.Write7BitEncodedInt(postSwingCooldown);
		writer.WriteVector2(aimVel);
		writer.Write(Animation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		willDie = reader.ReadBoolean();
		useAnim = reader.Read7BitEncodedInt();
		postSwingCooldown = reader.Read7BitEncodedInt();
		aimVel = reader.ReadVector2();
		Animation = reader.ReadSingle();
		mousePos = Owner.Center - aimVel;
	}
}
