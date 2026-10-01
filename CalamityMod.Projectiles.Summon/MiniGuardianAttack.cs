using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianAttack : ModProjectile, ILocalizedModType, IModType
{
	public enum MiniOffenseAIState
	{
		Vanity,
		Psa,
		Spears,
		Charges,
		Fireballs
	}

	private int attackDelay;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool SpawnedFromPSC => base.Projectile.ai[0] == 1f;

	internal int phaseTimer
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
			base.Projectile.netUpdate = true;
		}
	}

	public MiniOffenseAIState getAiState
	{
		get
		{
			if (!SpawnedFromPSC)
			{
				return MiniOffenseAIState.Psa;
			}
			return (MiniOffenseAIState)Math.Clamp(base.Projectile.ai[2], 2f, 4f);
		}
	}

	public bool ForcedVanity
	{
		get
		{
			if (SpawnedFromPSC)
			{
				return !Owner.Calamity().profanedCrystalBuffs;
			}
			return false;
		}
	}

	public MiniOffenseAIState updateAiState(Player player, MiniOffenseAIState currentAIState)
	{
		MiniOffenseAIState? result = null;
		if (SpawnedFromPSC && ForcedVanity)
		{
			result = MiniOffenseAIState.Vanity;
		}
		else if (!SpawnedFromPSC)
		{
			result = MiniOffenseAIState.Psa;
		}
		if (result.HasValue)
		{
			base.Projectile.ai[2] = (float)result.Value;
			return result.Value;
		}
		int currentPhase = (int)currentAIState;
		if (phaseTimer <= 0)
		{
			currentPhase++;
			if (currentPhase > 4)
			{
				currentPhase = 2;
			}
			result = (MiniOffenseAIState)currentPhase;
			bool whip = player.HasBuff<ProfanedCrystalWhipBuff>();
			int newPhaseTimer = ((result == MiniOffenseAIState.Charges) ? (60 * (whip ? 10 : 6)) : ((result == MiniOffenseAIState.Fireballs) ? (60 * (whip ? 10 : 6)) : (60 * (whip ? 8 : 6))));
			phaseTimer = newPhaseTimer;
		}
		else
		{
			result = (MiniOffenseAIState)currentPhase;
			phaseTimer--;
		}
		base.Projectile.ai[2] = (float)result.Value;
		return result.Value;
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.tileCollide = false;
		base.Projectile.width = 60;
		base.Projectile.height = 88;
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	private void BaseAI(NPC potentialTarget)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (potentialTarget != null && !ForcedVanity)
		{
			Vector2 targetDestination = potentialTarget.Center - base.Projectile.Center;
			float targetDist = ((Vector2)(ref targetDestination)).Length();
			float num = ((targetDist < 100f) ? 28f : 24f) * (SpawnedFromPSC ? 2f : 0.95f);
			float inertia = (SpawnedFromPSC ? 20f : 12f);
			targetDist = num / targetDist;
			targetDestination *= targetDist;
			base.Projectile.velocity = (base.Projectile.velocity * inertia + targetDestination) / (inertia + 1f);
			return;
		}
		base.Projectile.rotation = (float)Math.Atan(0.0);
		Vector2 playerDestination = Owner.Center - base.Projectile.Center;
		playerDestination.X += Main.rand.NextFloat(-10f, 20f) - 60f * (float)Owner.direction;
		playerDestination.Y += Main.rand.NextFloat(-10f, 20f) - 60f;
		float playerDist = ((Vector2)(ref playerDestination)).Length();
		float acceleration = 0.5f;
		float returnSpeed = 28f;
		if (playerDist > 2000f)
		{
			base.Projectile.position = Owner.position;
			base.Projectile.netUpdate = true;
		}
		else if (playerDist < 50f)
		{
			acceleration = 0.01f;
			if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9f;
			}
		}
		else
		{
			if (playerDist < 100f)
			{
				acceleration = 0.1f;
			}
			if (playerDist > 300f)
			{
				acceleration = 1f;
			}
			playerDist = returnSpeed / playerDist;
			playerDestination *= playerDist;
		}
		if (base.Projectile.velocity.X < playerDestination.X)
		{
			base.Projectile.velocity.X += acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X += acceleration;
			}
		}
		if (base.Projectile.velocity.X > playerDestination.X)
		{
			base.Projectile.velocity.X -= acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X -= acceleration;
			}
		}
		if (base.Projectile.velocity.Y < playerDestination.Y)
		{
			base.Projectile.velocity.Y += acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y += acceleration * 2f;
			}
		}
		if (base.Projectile.velocity.Y > playerDestination.Y)
		{
			base.Projectile.velocity.Y -= acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= acceleration * 2f;
			}
		}
	}

	public void AdvancedAI(NPC potentialTarget, Player owner, MiniOffenseAIState aiState)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		bool buffedAi = owner.HasBuff<ProfanedCrystalWhipBuff>();
		Vector2 targetDestination = potentialTarget.Center - base.Projectile.Center;
		if (attackDelay > 0)
		{
			attackDelay--;
		}
		switch (aiState)
		{
		case MiniOffenseAIState.Fireballs:
			targetDestination.X += Main.rand.NextFloat(-5f, 5f);
			targetDestination.Y += Main.rand.NextFloat(155f, 160f);
			break;
		case MiniOffenseAIState.Spears:
			targetDestination.X += Main.rand.NextFloat(-5f, 5f);
			targetDestination.Y += Main.rand.NextFloat(155f, 160f);
			break;
		}
		if (aiState != MiniOffenseAIState.Charges)
		{
			base.Projectile.rotation = (float)Math.Atan(0.0);
			float targetDist = ((Vector2)(ref targetDestination)).Length();
			float num = ((targetDist < 100f) ? 28f : 24f) * 2f;
			float inertia = 20f;
			targetDist = num / targetDist;
			targetDestination *= targetDist;
			base.Projectile.velocity = (base.Projectile.velocity * inertia + targetDestination) / (inertia + 1f);
			if (aiState == MiniOffenseAIState.Fireballs)
			{
				if (attackDelay != 0)
				{
					return;
				}
				if (buffedAi)
				{
					attackDelay = 100;
					base.Projectile.velocity = base.Projectile.Center - potentialTarget.Center;
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile = base.Projectile;
					projectile.velocity *= 29f;
					int damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
					Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, potentialTarget, 28f);
					int shotCount = 3;
					int spread = -20;
					for (int i = 0; i < shotCount; i++)
					{
						Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X, velocity.Y), (double)MathHelper.ToRadians((float)spread), default(Vector2));
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedspeed, ModContent.ProjectileType<MiniGuardianFireball>(), damage, 1f, base.Projectile.owner, 1f);
						spread += 20;
					}
				}
				else
				{
					base.Projectile.velocity = base.Projectile.Center - potentialTarget.Center;
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 20f;
					attackDelay = 75;
					int damage2 = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
					Vector2 velocity2 = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, potentialTarget, 25f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, ModContent.ProjectileType<MiniGuardianFireball>(), damage2, 1f, base.Projectile.owner);
				}
				return;
			}
			if (attackDelay % (buffedAi ? 6 : 8) == 0)
			{
				SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
				if (attackDelay == 0)
				{
					int numProj = 5;
					Vector2 velocity3 = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, potentialTarget, buffedAi ? 28f : 20f);
					int spread2 = (buffedAi ? (-10) : (-20));
					for (int j = 0; j < numProj; j++)
					{
						Vector2 perturbedspeed2 = velocity3.RotatedBy(MathHelper.ToRadians((float)spread2));
						int separation = j * 4 - 8;
						int spearBaseDamage = (int)((float)base.Projectile.originalDamage * 0.5f);
						int spearDamage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(spearBaseDamage);
						int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y - (float)separation, perturbedspeed2.X, perturbedspeed2.Y, ModContent.ProjectileType<MiniGuardianSpear>(), spearDamage, 1f, base.Projectile.owner, 1f, 1f);
						if (proj.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[proj].DamageType = DamageClass.Summon;
							Main.projectile[proj].originalDamage = spearBaseDamage;
						}
						spread2 += (buffedAi ? 5 : 10);
					}
				}
				else
				{
					int spearBaseDamage2 = base.Projectile.originalDamage;
					int spearDamage2 = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(spearBaseDamage2);
					Vector2 velocity4 = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, potentialTarget, buffedAi ? 28f : 20f);
					int proj2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity4, ModContent.ProjectileType<MiniGuardianSpear>(), spearDamage2, 1f, base.Projectile.owner, 1f, 1f);
					if (proj2.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj2].DamageType = DamageClass.Summon;
						Main.projectile[proj2].originalDamage = base.Projectile.originalDamage;
					}
				}
			}
			if (attackDelay == 0)
			{
				attackDelay = (buffedAi ? 30 : 40);
			}
			return;
		}
		bool num2 = buffedAi || attackDelay > 0;
		if (buffedAi)
		{
			if (attackDelay <= 0)
			{
				owner.Calamity().rollBabSpears(1, chaseable: true);
			}
			float distToTarget = base.Projectile.Distance(potentialTarget.Center) + 0.01f;
			base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * (28f + 28f / (distToTarget * 0.01f));
			base.Projectile.velocity = Vector2.Clamp(base.Projectile.velocity, Vector2.One * -50f, Vector2.One * 50f);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(potentialTarget.Center), 0.001f * distToTarget);
		}
		else if (attackDelay == 24)
		{
			base.Projectile.velocity = base.Projectile.SuperhomeTowardsTarget(potentialTarget, 35f, 1f);
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.369f;
		}
		if (attackDelay <= 0)
		{
			attackDelay = (buffedAi ? 20 : 25);
		}
		if (num2)
		{
			bool shouldAdjust = !Main.dayTime & buffedAi;
			int dustId = ProvUtils.GetDustID(!Main.dayTime);
			for (int k = 0; k < 6; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + (base.Projectile.Size / 2f).RotatedBy(base.Projectile.rotation), dustId);
				dust.velocity = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(20f * (float)(k % 2 == 0).ToDirectionInt()));
				dust.noGravity = true;
				dust.fadeIn = (shouldAdjust ? 0.9f : 1.8f);
				dust.scale = (Main.dayTime ? dust.scale : 0.45f);
			}
		}
	}

	public override void AI()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Owner;
		if (owner.Calamity().pSoulGuardians)
		{
			base.Projectile.timeLeft = 2;
		}
		if (!owner.Calamity().pSoulArtifact || owner.dead || !owner.active)
		{
			owner.Calamity().pSoulGuardians = false;
			base.Projectile.active = false;
			return;
		}
		bool psc = owner.Calamity().profanedCrystal;
		if ((psc && !SpawnedFromPSC) || (!psc && SpawnedFromPSC))
		{
			base.Projectile.active = false;
		}
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		base.Projectile.localNPCHitCooldown = (SpawnedFromPSC ? 6 : 9);
		MiniOffenseAIState currentAIState = getAiState;
		if (owner.Calamity().profanedCrystalAnim != -1)
		{
			currentAIState = MiniOffenseAIState.Vanity;
		}
		MiniOffenseAIState newAIState = updateAiState(owner, currentAIState);
		if (newAIState != currentAIState)
		{
			base.Projectile.netUpdate = true;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(3000f, owner);
		if ((uint)currentAIState <= 1u)
		{
			BaseAI(potentialTarget);
		}
		else if (potentialTarget != null && !ForcedVanity)
		{
			AdvancedAI(potentialTarget, owner, newAIState);
		}
		else
		{
			BaseAI(null);
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.direction = (base.Projectile.spriteDirection = Math.Sign(base.Projectile.velocity.X));
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 6 % Main.projFrames[base.Type];
	}

	public override bool? CanDamage()
	{
		return !ForcedVanity;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Owner.Calamity().angelicAlliance)
		{
			target.AddBuff(ModContent.BuffType<BanishingFire>(), 300);
		}
		if (!Owner.Calamity().profanedCrystal)
		{
			if (base.Projectile.ai[1] == 0f)
			{
				Owner.Calamity().rollBabSpears(1, target.chaseable);
			}
			base.Projectile.ai[1]--;
			if (base.Projectile.ai[1] < 0f)
			{
				base.Projectile.ai[1] = 15f;
			}
		}
		else
		{
			bool buffedAI = Owner.HasBuff<ProfanedCrystalWhipBuff>();
			if (getAiState == MiniOffenseAIState.Charges && !buffedAI)
			{
				Owner.Calamity().rollBabSpears(1, chaseable: true);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Owner.Calamity().angelicAlliance)
		{
			target.AddBuff(ModContent.BuffType<BanishingFire>(), 300);
		}
		if (!Owner.Calamity().profanedCrystal)
		{
			if (base.Projectile.ai[1] == 0f)
			{
				Owner.Calamity().rollBabSpears(1, chaseable: true);
			}
			base.Projectile.ai[1]--;
			if (base.Projectile.ai[1] < 0f)
			{
				base.Projectile.ai[1] = 15f;
			}
		}
		else
		{
			bool buffedAI = Owner.HasBuff<ProfanedCrystalWhipBuff>();
			if (getAiState == MiniOffenseAIState.Charges && !buffedAI)
			{
				Owner.Calamity().rollBabSpears(1, chaseable: true);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (!ForcedVanity && SpawnedFromPSC)
		{
			int dye = Owner?.cMinion ?? 0;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
			return false;
		}
		return true;
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		attackDelay = reader.ReadInt32();
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(attackDelay);
	}
}
