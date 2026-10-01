using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
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
public class StellarStrikerHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing = true;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int swingCount;

	public bool spawnBoom = true;

	public bool finalFlip;

	public override int AssignedItemID => ModContent.ItemType<StellarStriker>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<StellarStriker>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/StellarStriker";

	public override float HitboxOutset => 90f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(130f, 130f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 118f);
		}
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = -1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnim = Owner.itemAnimationMax;
		if (mousePos.X < Owner.Center.X)
		{
			Owner.direction = -1;
		}
		else
		{
			Owner.direction = 1;
		}
		FlipAsSword = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX).X > 0f;
	}

	public override void UseStyle()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		AnimationProgress = Animation % (float)useAnim;
		DrawUnconditionally = false;
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimVel;
		}
		else
		{
			mousePos = Owner.Calamity().mouseWorld;
		}
		if (CanHit)
		{
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.1f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.15f);
		}
		if (!doSwing)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				base.Projectile.localNPCImmunity[i] = 0;
			}
			base.Projectile.numHits = 0;
			mousePos = Owner.Calamity().mouseWorld;
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			CanHit = false;
			if (mousePos.X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			FlipAsSword = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX).X > 0f;
			doSwing = true;
			swingCount++;
			spawnBoom = true;
			finalFlip = false;
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
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians((float)((Owner.direction != -1) ? 120 : 0)), 0.1f);
			if (AnimationProgress < (float)(useAnim / 3))
			{
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					doSwing = false;
				}
				RotationOffset = RotationOffset.AngleLerp(MathHelper.ToRadians(-45f * base.Projectile.ai[1] * (float)Owner.direction), 0.2f);
			}
			else
			{
				if (!finalFlip)
				{
					FlipAsSword = Owner.direction < 0;
				}
				float time = AnimationProgress - (float)(useAnim / 8);
				float timeMax = useAnim - useAnim / 8;
				if (time == (float)(int)(timeMax * 0.3f))
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/TerratomereSwing");
					style.Volume = 0.4f;
					style.Pitch = Main.rand.NextFloat(0.65f, 0.75f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
					style.Volume = 0.6f;
					style.Pitch = Main.rand.NextFloat(0.05f, 0.12f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (time > (float)(int)(timeMax * 0.25f) && time < (float)(int)(timeMax * 0.85f))
				{
					CanHit = true;
				}
				else
				{
					CanHit = false;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(-45f * base.Projectile.ai[1] * (float)Owner.direction, 405f * (0f - base.Projectile.ai[1]) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
				if (time >= timeMax)
				{
					doSwing = false;
				}
				if (time < (float)(int)(timeMax * 0.85f))
				{
					postSwing = true;
				}
			}
			if (CanHit)
			{
				for (int j = 0; j < 3; j++)
				{
					Vector2 particleVel = Utils.RotatedBy(new Vector2(0f, 3f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(5, 130), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), -particleVel * Main.rand.NextFloat(1.1f, 3.2f), Main.rand.NextBool(4) ? Color.PaleTurquoise : Color.Turquoise, "CalamityMod/Particles/Sparkle", new Vector2(2f, 1f), particleVel.ToRotation(), Main.rand.NextFloat(0.4f, 1.1f), 0.2f, 23, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				for (int k = 0; k < 4; k++)
				{
					float randRot = Main.rand.NextFloat(-30f, -60f);
					Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(Owner.Center + Utils.RotatedBy(new Vector2(140f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.4000000059604645), -dustVel, affectedByGravity: false, 20, Main.rand.NextFloat(0.007f, 0.012f), Color.Turquoise, new Vector2(1.3f, 0.5f), quickShrink: true, glow: false, Main.rand.NextFloat(0.5f, 0.7f)));
				}
			}
			else if (Main.rand.NextBool(3))
			{
				Vector2 particleVel2 = Utils.RotatedBy(new Vector2(0f, 3f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(5, 130), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), particleVel2 * Main.rand.NextFloat(0.8f, 1.2f), Main.rand.NextBool(4) ? Color.PaleTurquoise : Color.Turquoise, "CalamityMod/Particles/HealingPlus", new Vector2(1f, 1f), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0.8f, 1.2f), 0.2f, 23, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			Vector2 center = base.Projectile.Center;
			Color turquoise = Color.Turquoise;
			Lighting.AddLight(center, ((Color)(ref turquoise)).ToVector3() * fadeIn);
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/CursedDaggerThrow");
		style.Volume = 0.65f;
		style.Pitch = 0.7f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");
		style.Volume = 0.55f;
		style.Pitch = 0.6f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, 17f, ignoreKBImmune: true);
		for (int i = 0; i < MathHelper.Clamp(10 - base.Projectile.numHits * 2, 2, 10); i++)
		{
			Dust dust = Dust.NewDustPerfect(target.Center, 278, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -20f).RotatedByRandom(0.4) * Main.rand.NextFloat(0.2f, 1f));
			dust.scale = Main.rand.NextFloat(0.55f, 0.95f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
			if (Main.rand.NextBool(4))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -24f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 35, Main.rand.NextFloat(0.3f, 1f), Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise, new Vector2(0.6f, 1.5f)));
			}
		}
		if (spawnBoom)
		{
			Vector2 spawnSpot = target.Center + new Vector2(Main.rand.NextFloat(-550f, 550f), Main.rand.NextFloat(-750f, -950f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, Vector2.Zero, ModContent.ProjectileType<StellarStrikerMeteor>(), (int)((float)base.Projectile.damage * 1.5f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 7f);
			spawnBoom = false;
		}
		target.AddBuff(ModContent.BuffType<Nightwither>(), 500);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.3f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		if ((useAnim > 0 || DrawUnconditionally) && Owner.ItemAnimationActive)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Texture2D value = swoosh.Value;
			Vector2 position = Owner.Center - Main.screenPosition;
			Color turquoise = Color.Turquoise;
			((Color)(ref turquoise)).A = 0;
			Main.EntitySpriteDraw(value, position, null, turquoise * (float)Math.Pow(fadeIn, 3.0), base.Projectile.rotation + (float)Math.PI / 4f * (float)Owner.direction + RotationOffset * 1.75f, swoosh.Size() * 0.5f, base.Projectile.scale * 2f, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
			for (int i = 0; i < 25; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/StellarStrikerGhost", (AssetRequestMode)2).Value;
				turquoise = Color.Turquoise;
				((Color)(ref turquoise)).A = 0;
				Color auraColor = turquoise * 0.15f * fadeIn;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * 6f * fadeIn;
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	public override void ResetStyle()
	{
	}
}
