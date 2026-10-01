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
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class DevilsDevastationHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public float hitboxMult;

	public Color clr;

	public SlotId SoundSlot;

	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int swingCount;

	public bool finalFlip;

	public bool playSwingSound;

	public bool holding;

	public int postSwingCooldown;

	public bool willDie;

	public bool hasLaunchedBlades;

	public bool swooshFade;

	private int lastSwingId;

	public NPC lastHitTarget;

	private int lastHitTargetIndex;

	public override int AssignedItemID => ModContent.ItemType<DevilsDevastation>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DevilsDevastation>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/DevilsDevastation";

	public override float HitboxOutset => 335f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(395f, 395f) * hitboxMult;
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-3f, 118f);
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

	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		base.Projectile.originalDamage *= 15;
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
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		Owner.Calamity().demonSwordKillMode = false;
		if (lastHitTarget != null && lastHitTarget.life > 0 && lastHitTarget.active && Owner.HeldItem.type == AssignedItemID && Main.myPlayer == base.Projectile.owner)
		{
			Owner.SetScreenshake(12.5f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(lastHitTarget.Center, Vector2.Zero, clr, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.05f, 0.6f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 4; i++)
			{
				Vector2 vel = ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * 2f;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), lastHitTarget.Center, vel.RotatedBy(MathHelper.ToRadians(45f)), ModContent.ProjectileType<DevilsStrike>(), 0, 0f, Owner.whoAmI, 1f);
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), lastHitTarget.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), (int)((float)base.Projectile.damage * 1.666f), 0f, Owner.whoAmI, lastHitTarget.whoAmI).DamageType = DamageClass.Melee;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong");
			style.Volume = 0.9f;
			style.Pitch = 0.3f;
			SoundEngine.PlaySound(in style, lastHitTarget.Center);
			int bonusDamage = 4000;
			if (lastHitTarget.Calamity().demonicFlamesBonusDamage <= bonusDamage)
			{
				lastHitTarget.Calamity().demonicFlamesBonusDamage = bonusDamage;
				lastHitTarget.AddBuff(ModContent.BuffType<DemonicFlames>(), 210);
			}
		}
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
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
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
		if (lastHitTargetIndex >= 0)
		{
			NPC syncedTarget = Main.npc[lastHitTargetIndex];
			if (syncedTarget != null && syncedTarget.active && syncedTarget.life > 0)
			{
				lastHitTarget = syncedTarget;
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
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound Sound) && Sound.IsPlaying && lastHitTarget != null)
		{
			Sound.Position = lastHitTarget.Center;
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
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.2f);
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
				if (lastHitTarget != null)
				{
					useAnim = 115;
					SoundStyle dieSound = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordFinalStrike");
					SoundStyle style = dieSound with
					{
						Volume = 1f,
						Pitch = 0f
					};
					SoundSlot = SoundEngine.PlaySound(in style, lastHitTarget.Center);
				}
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
			for (int j = 0; j < 4; j++)
			{
				float deathFade = (willDie ? Utils.GetLerpValue(0f, postSwingCooldownMax, postSwingCooldown, clamped: true) : 1f);
				Main.rand.NextFloat(-30f, 30f);
				Vector2 dustVel = -Vector2.One.RotatedBy(base.Projectile.rotation + RotationOffset + MathHelper.ToRadians(90f)).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(7f, 17f) - Owner.velocity * 0.8f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(70f, 500f) * deathFade, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), dustVel, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, Main.rand.NextFloat(0.04f, 0.06f) * 10f, (Main.rand.NextBool() ? Color.MediumOrchid : clr) * deathFade, new Vector2(0.9f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.95f));
			}
			if (lastHitTarget != null && lastHitTarget.life > 0 && lastHitTarget.active)
			{
				float growth = (1f - (willDie ? Utils.GetLerpValue(0f, postSwingCooldownMax, postSwingCooldown, clamped: true) : 1f)) * 15f;
				for (int k = 0; k < 2; k++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(lastHitTarget.Center, Vector2.One.RotatedBy((k == 0) ? MathHelper.ToRadians(90f) : 0f) * Main.rand.NextFloat(-3f - growth, 3f + growth), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, Main.rand.NextFloat(0.04f, 0.06f) * (10f + growth), Main.rand.NextBool() ? Color.MediumOrchid : clr, new Vector2(0.7f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
				}
			}
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
					style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
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
					for (int l = 0; l < 8; l++)
					{
						float randRot = Main.rand.NextFloat(-20f, -100f);
						Dust dust = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(560f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(MathHelper.ToRadians(randRot)), ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(3.1415927410125732) * 0.9f, 0, default(Color), Main.rand.NextFloat(1.45f, 2.4f));
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool() ? clr : Color.BlueViolet);
					}
					for (int m = 0; m < 4; m++)
					{
						float randRot2 = Main.rand.NextFloat(-20f, -100f);
						Vector2 dustVel2 = Utils.RotatedBy(new Vector2(0f, 11f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot2)), default(Vector2));
						GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + Utils.RotatedBy(new Vector2(560f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), dustVel2, "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 33, Main.rand.NextFloat(0.43f, 0.56f), Main.rand.NextBool() ? Color.MediumOrchid : clr, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		if (target != null && target.life > 0 && target.active)
		{
			lastHitTarget = target;
			lastHitTargetIndex = target.whoAmI;
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.netUpdate = true;
			}
		}
		bool hasKillMode = Owner.Calamity().cooldowns.TryGetValue(KillMode.ID, out var killModeCD);
		int bonusDamage = 4000;
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
		target.MoveNPC(launchVel, 40f, ignoreKBImmune: true);
		if (target.Calamity().demonSwordImpales != 0 || base.Projectile.numHits == 0)
		{
			int dustNum = MathHelper.Clamp(12 - base.Projectile.numHits * 3, 3, 12);
			for (int i = 0; i < dustNum; i++)
			{
				float variance = Main.rand.NextFloat(-0.5f, 0.5f);
				Vector2 vel = (launchVel * 55f).RotatedBy(variance) * Main.rand.NextFloat(0.2f, 1f) * (1f - Math.Abs(variance));
				int dustStyle = 278;
				Dust dust = Dust.NewDustPerfect(target.Center, dustStyle, base.Projectile.velocity);
				dust.scale = Main.rand.NextFloat(1.2f, 1.4f) - Math.Abs(variance);
				dust.velocity = vel;
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, vel, affectedByGravity: true, 55, 1.25f, Main.rand.NextBool() ? clr : Color.MediumOrchid));
			}
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.BlueViolet, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f * (float)(j + 1), 1.3f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.35f * (float)(j + 1), 0.8f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		if (!hasLaunchedBlades)
		{
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (projectile.active && projectile.type == ModContent.ProjectileType<DevilsDevastationThrownBlade>() && projectile.ai[2] == (float)target.whoAmI && projectile.localAI[0] != 5f)
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
			Owner.SetScreenshake(12f);
			for (int k = 0; k < 5; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center + launchVel * 15f, launchVel.RotatedBy((0.15f - 0.05f * (float)k) * (float)((k % 2 != 0) ? 1 : (-1))) * (float)(10 + 10 * k), "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 11, 0.7f - 0.15f * (float)k, Main.rand.NextBool() ? clr : Color.MediumOrchid, new Vector2(1.5f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians((float)((k % 2 == 0) ? 90 : 0)), fadeIn: false, affectedByLight: false, (k % 2 == 0) ? (-0.8f) : 0.8f));
			}
			for (int l = 0; l < 20; l++)
			{
				float distPow = Main.rand.NextFloat(0f, 15f);
				Vector2 sparkVel = launchVel.RotatedByRandom(distPow * 0.01f) * Main.rand.NextFloat(5f, 35f + distPow);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center + (Vector2.One * distPow).RotatedByRandom(3.1415927410125732), sparkVel, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, Main.rand.Next(13, 21) * 2, (Main.rand.NextFloat(0.5f, 0.85f) - distPow * 0.12f) * 0.2f, (Main.rand.NextBool() ? clr : Color.BlueViolet) * 0.75f, new Vector2(1.3f, 0.4f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordInsaneImpact");
			style.Volume = 0.8f;
			style.Pitch = MathHelper.Clamp((float)swingCount * 0.05f, -0.25f, 0.5f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteBigHit1");
			style.Volume = 0.8f;
			style.Pitch = MathHelper.Clamp((float)swingCount * 0.025f, 0.4f, 0.65f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.35f;
		int hitsToMinMult = 8;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2);
		ModContent.Request<Texture2D>("CalamityMod/Particles/CircularTrail", (AssetRequestMode)2);
		float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
		float deathFade = (willDie ? Utils.GetLerpValue(0f, postSwingCooldownMax, postSwingCooldown, clamped: true) : 1f);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineAngled", (AssetRequestMode)2);
		int draws = 30;
		Color val;
		for (int i = 0; i < draws; i++)
		{
			Vector2 offsetDir = Vector2.One.RotatedBy(base.Projectile.rotation + RotationOffset + MathHelper.ToRadians(90f));
			val = Color.Lerp(Color.MediumOrchid, clr, Utils.GetLerpValue(0f, draws - 1, i));
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.38f * (willDie ? deathFade : 1f);
			Vector2 drawOffset = -offsetDir * 10f * deathFade * (float)i;
			Main.EntitySpriteDraw(tex2.Value, base.Projectile.Center - offsetDir * 70f - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY) + Main.rand.NextVector2Circular(18f, 18f), tex2.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, tex2.Size() * 0.5f, Vector2.One * (1f - (float)i * 0.02f) * 0.06f, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
		}
		for (int j = 0; j < 20; j++)
		{
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Color auraColor2 = val * 0.18f * (willDie ? deathFade : 1f);
			Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)j / 20f).ToRotationVector2() * 7f * (willDie ? ((1f - deathFade) * 3f) : 2f);
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + drawOffset2 + new Vector2(0f, Owner.gfxOffY) + Main.rand.NextVector2Circular(7f, 7f), tex.Frame(1, FrameCount, 0, Frame), auraColor2, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
		}
		if (useAnim > 0 || DrawUnconditionally)
		{
			Texture2D value = swoosh.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			val = Color.BlueViolet;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * fadeIn * 0.75f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 == 0) ? (-65) : 65)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, base.Projectile.scale * 0.9f * hitboxMult, (SpriteEffects)0);
			Texture2D value2 = swoosh.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			val = clr;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, val * fadeIn * 0.85f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 == 0) ? (-65) : 65)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, base.Projectile.scale * 1.2f * hitboxMult, (SpriteEffects)0);
			Texture2D value3 = tex.Value;
			Vector2 position3 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			Rectangle? sourceRectangle = tex.Frame(1, FrameCount, 0, Frame);
			val = Color.MediumOrchid;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value3, position3, sourceRectangle, Color.Lerp(val, lightColor, deathFade) * deathFade, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(willDie);
		writer.Write7BitEncodedInt(useAnim);
		writer.Write7BitEncodedInt(lastHitTargetIndex);
		writer.Write7BitEncodedInt(postSwingCooldown);
		writer.WriteVector2(aimVel);
		writer.Write(Animation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		willDie = reader.ReadBoolean();
		useAnim = reader.Read7BitEncodedInt();
		lastHitTargetIndex = reader.Read7BitEncodedInt();
		postSwingCooldown = reader.Read7BitEncodedInt();
		aimVel = reader.ReadVector2();
		Animation = reader.ReadSingle();
		mousePos = Owner.Center - aimVel;
	}

	public DevilsDevastationHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		hitboxMult = 1.3f;
		clr = Color.Lerp(Color.DeepPink, Color.Orange, 0.5f);
		doSwing = true;
		playSwingSound = true;
		holding = true;
		lastHitTargetIndex = -1;
		base._002Ector();
	}
}
