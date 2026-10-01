using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DazzlingStabber : ModProjectile, ILocalizedModType, IModType
{
	public Color bladeColor;

	public int attackCooldown;

	public int attacksDone;

	private bool justHit;

	public NPC potentialTarget;

	public float moveSmoothing;

	public float damageMult;

	public Vector2 storedPos;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AttackDelay => ref base.Projectile.ai[0];

	public ref float RestOffsetAngle => ref base.Projectile.ai[1];

	public ref float KnifeType => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = false;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 50;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0.333f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 28;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(attackCooldown);
		writer.Write(attacksDone);
		writer.Write(justHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		attackCooldown = reader.ReadInt32();
		attacksDone = reader.ReadInt32();
		justHit = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		float knifeType = KnifeType;
		if (knifeType != 1f)
		{
			if (knifeType == 2f)
			{
				bladeColor = Color.OrangeRed;
				base.Projectile.extraUpdates = 2;
				damageMult = 3f;
			}
			else
			{
				bladeColor = Color.Orange;
				damageMult = 1f;
				base.Projectile.ArmorPenetration = 20;
			}
		}
		else
		{
			bladeColor = Color.Orchid;
			base.Projectile.extraUpdates = 1;
			damageMult = 0.5f;
			base.Projectile.ArmorPenetration = 40;
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		ApplyPlayerBuffs();
		potentialTarget = base.Projectile.Center.MinionHoming(1100f, Owner);
		if (potentialTarget != null && attackCooldown == 0 && AttackDelay <= 0f)
		{
			storedPos = potentialTarget.Center;
		}
		if (justHit)
		{
			if (KnifeType == 1f)
			{
				SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
				{
					Volume = 0.7f,
					Pitch = (float)attacksDone * 0.05f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (attacksDone >= 6)
				{
					for (int i = 0; i < 9; i++)
					{
						Vector2 center2 = base.Projectile.Center;
						int type = ModContent.DustType<LightDust>();
						Vector2? velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.5) * Main.rand.NextFloat(10f, 20f);
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
						dust.noGravity = true;
						dust.scale = Main.rand.NextFloat(1.25f, 1.75f);
						dust.color = Color.Orchid;
						dust.noLightEmittence = true;
					}
					for (int j = 0; j < 4; j++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(15f, 20f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 25, Main.rand.NextFloat(1.35f, 1.6f), Color.Orchid, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.25f, 0.35f)));
					}
				}
			}
			if (KnifeType == 2f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit", 2);
				style.Volume = 0.4f;
				style.Pitch = Main.rand.NextFloat(-0.25f, -0.15f);
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int k = 0; k < 7; k++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 5f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard6", affectedByGravity: false, Main.rand.Next(15, 26), Main.rand.NextFloat(0.75f, 1.2f), Color.White, new Vector2(1.3f, 0.5f), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
				}
			}
		}
		if (potentialTarget != null)
		{
			if (KnifeType == 1f && attacksDone > 0 && base.Projectile.Center.Distance(storedPos) < 150f && attackCooldown == 0)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.1f, affectedByGravity: false, 20, 0.03f, bladeColor * 0.55f, new Vector2(0.8f, 2.3f), quickShrink: true, glow: false, 0.45f));
			}
			if (KnifeType == 2f && attackCooldown == 0)
			{
				if (Main.rand.NextBool(4))
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.8f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard6", affectedByGravity: false, Main.rand.Next(25, 36), Main.rand.NextFloat(0.55f, 1.2f), Color.White, Vector2.One, useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
				}
				else
				{
					bool memesHatesLists = Main.rand.NextBool(5);
					Vector2 center3 = base.Projectile.Center;
					int type2 = (memesHatesLists ? 278 : ModContent.DustType<LightDust>());
					Vector2? velocity2 = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(1f, 4f);
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(center3, type2, velocity2, 0, newColor);
					dust2.noGravity = !memesHatesLists;
					dust2.scale = Main.rand.NextFloat(0.55f, 1.1f);
					dust2.color = bladeColor;
				}
			}
			if (KnifeType == 3f)
			{
				Vector2 center4 = base.Projectile.Center;
				int type3 = ModContent.DustType<LightDust>();
				Vector2? velocity3 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(2f, 8f);
				newColor = default(Color);
				Dust dust3 = Dust.NewDustPerfect(center4, type3, velocity3, 0, newColor);
				dust3.noGravity = true;
				dust3.scale = Main.rand.NextFloat(0.65f, 1f);
				dust3.color = Color.Goldenrod;
				dust3.noLightEmittence = true;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.5) * Main.rand.NextFloat(1f, 5f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 14, Main.rand.NextFloat(0.9f, 1.2f), Color.Gold, new Vector2(0.5f, 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.25f));
				for (int l = 0; l < 2; l++)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + (float)l * base.Projectile.velocity * 0.4f, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 9, 0.05f, Color.Orange * 0.6f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 0.85f));
				}
			}
			if (attackCooldown == 0)
			{
				SliceTarget(potentialTarget);
			}
			else
			{
				manageAttackCooldown();
			}
		}
		else
		{
			manageAttackCooldown();
			ReturnToRestingPosition();
		}
	}

	public void manageAttackCooldown()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		if (attackCooldown <= 0)
		{
			return;
		}
		attackCooldown--;
		if (KnifeType == 1f)
		{
			ReturnToRestingPosition();
		}
		else if (KnifeType == 2f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
		}
		if (attackCooldown != 0)
		{
			return;
		}
		attacksDone = 0;
		if (KnifeType == 1f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.7f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.35f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 6; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(4f, 8f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.65f, 1.15f);
				dust.color = Color.Orchid;
				dust.noLightEmittence = true;
			}
		}
	}

	public void ApplyPlayerBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<DazzlingStabberBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<DazzlingStabber>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().providenceStabber = false;
			}
			if (Owner.Calamity().providenceStabber)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void SliceTarget(NPC target)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		moveSmoothing = 50f;
		if (AttackDelay > -60f)
		{
			AttackDelay--;
			if (AttackDelay > 0f)
			{
				if (KnifeType == 1f)
				{
					if (!base.Projectile.WithinRange(storedPos, 150f))
					{
						base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.10000000149011612);
						return;
					}
					Projectile projectile = base.Projectile;
					projectile.velocity *= 1.004f;
				}
				return;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 3f)
		{
			base.Projectile.velocity = Vector2.UnitY * -12f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 5f && base.Projectile.WithinRange(storedPos, 50f))
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.93f;
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < ((KnifeType == 2f) ? 15f : 40f))
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.03f;
		}
		float angularTurnSpeed = 0.15f;
		float angleToTargetCoords = base.Projectile.AngleTo(storedPos);
		if (KnifeType == 3f && !base.Projectile.WithinRange(storedPos, 50f))
		{
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(angleToTargetCoords, angularTurnSpeed).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		if (KnifeType == 2f)
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(storedPos) * 15f;
		}
		if (KnifeType == 1f && !base.Projectile.WithinRange(storedPos, 400f))
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(storedPos) * 20f;
		}
		if (!base.Projectile.WithinRange(storedPos, 75f) && Vector2.Dot(base.Projectile.SafeDirectionTo(storedPos), base.Projectile.velocity.SafeNormalize(Vector2.Zero)) > 0.99f && KnifeType == 3f)
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(storedPos) * 36f;
			AttackDelay = 15f;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public void ReturnToRestingPosition()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (moveSmoothing > 2f)
		{
			moveSmoothing--;
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(RestOffsetAngle + Main.GlobalTimeWrappedHourly, 0.25f);
		float sine = Math.Abs((float)Math.Sin((Main.GlobalTimeWrappedHourly + RestOffsetAngle * 1.8f) * 8f / (float)Math.PI));
		Vector2 destination = Owner.Center + Vector2.UnitY.RotatedBy(RestOffsetAngle + Main.GlobalTimeWrappedHourly) * -220f * MathHelper.Lerp(sine, 0.8f, 0.8f);
		base.Projectile.velocity = (destination - base.Projectile.Center) / ((potentialTarget == null) ? moveSmoothing : 18f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		justHit = true;
		if (KnifeType == 1f)
		{
			AttackDelay = 15f;
			attacksDone++;
			if (attacksDone >= 6)
			{
				attackCooldown = Main.rand.Next(80, 96);
			}
		}
		if (KnifeType == 2f)
		{
			base.Projectile.velocity = (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f).RotatedByRandom(0.699999988079071);
			attackCooldown = Main.rand.Next(45, 61);
		}
		if (KnifeType == 3f)
		{
			if (attackCooldown == 0)
			{
				attackCooldown = 5;
			}
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
		base.Projectile.netUpdate = true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override bool? CanDamage()
	{
		if (attackCooldown != 0)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (KnifeType == 1f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/DazzlingStabber", (AssetRequestMode)2).Value;
		}
		if (KnifeType == 2f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/DazzlingStabber2", (AssetRequestMode)2).Value;
		}
		if (KnifeType == 3f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/DazzlingStabber3", (AssetRequestMode)2).Value;
		}
		Projectile projectile = base.Projectile;
		Color backglowColor = bladeColor;
		((Color)(ref backglowColor)).A = 0;
		projectile.DrawProjectileWithBackglow(backglowColor, lightColor, 3.5f, tex, null, (SpriteEffects)0);
		return false;
	}

	public DazzlingStabber()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		bladeColor = Color.White;
		moveSmoothing = 50f;
		damageMult = 1f;
		base._002Ector();
	}
}
