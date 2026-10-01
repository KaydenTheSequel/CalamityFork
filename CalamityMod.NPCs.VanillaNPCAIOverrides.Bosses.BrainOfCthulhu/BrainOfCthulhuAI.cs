using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CalamityMod.DataStructures;
using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss.BrainOfCthulhu;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;

public class BrainOfCthulhuAI : VanillaAIOverride
{
	internal enum BrainAIState : byte
	{
		UndergroundSpawnAnimation,
		SurfaceSpawnAnimation,
		Phase1Idle,
		CreeperSwipes,
		CreeperSwings,
		CreeperOrbit,
		CreeperSpiral,
		TelekineticOnslaught,
		Stunned,
		Phase2TransitionClosed,
		Phase2TransitionOpen,
		Phase2Idle,
		CrimsonEyes,
		SanguineScythes,
		Bloodletting,
		IllusionDash,
		IllusionTrick,
		DeathAnimation
	}

	private static SoundStyle StunnedHit = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Stun_Hit", 3);

	private static SoundStyle ShieldDown = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Shield_Down")
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	private static SoundStyle ShieldUp = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Shield_Up")
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	private static SoundStyle IntroRoar = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Roar")
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	private static SoundStyle Roar = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Short_Roar")
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	public static SoundStyle Laugh = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Laugh")
	{
		PauseBehavior = PauseBehavior.PauseWithGame,
		MaxInstances = 5
	};

	private static SoundStyle Growl = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Growl", 2)
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	private static SoundStyle Death = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Death_Roar")
	{
		PauseBehavior = PauseBehavior.PauseWithGame
	};

	private static SoundStyle BloodShot = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_BloodShot");

	private static SoundStyle BloodBomb = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_BloodBomb");

	private static SoundStyle BloodExplosion = new SoundStyle("CalamityMod/Sounds/Custom/BrainOfCthulhu/BoC_Rev_Explosion", 2);

	internal static bool SummonedViaItem = false;

	internal List<Particle> BoCAfterImages;

	internal float ShieldOpacity;

	internal float ShieldScale;

	private Vector2 BoCDrawOffset;

	private Rectangle BoCFrame;

	internal BrainAIState PreviousAttack;

	internal float TeleportTime;

	internal float TeleportDuration;

	internal float SpawnTime;

	internal int SpawnDelay;

	internal bool OnSecondCreeperPhase;

	private bool isNegative;

	internal float AttackRotation;

	internal float AttackTime;

	internal int AttackCounter;

	internal bool AttackFlag;

	internal Vector2 AttackPosition;

	internal List<BrainAIState> availableAttacks;

	internal List<byte> AttackList;

	internal HashSet<int> TargetsSet;

	internal static int BrainIllusionDamage => 15;

	internal static int BloodShotDamage => 12;

	internal static int BloodScytheDamage => 12;

	internal static int IchorShotDamage => 12;

	internal static int CrimsonEyeDamage => 12;

	internal static float Phase1DefenseMultiplier => 1.5f;

	internal static float DesperateOnslaughtCreeperHealthGate => 0.1f;

	internal static float Phase2HealthGate => 0.5f;

	internal static float DespawnRangeSQ => 36000000f;

	internal static float DespawnRange => 6000f;

	internal static int IdlePeriodDuration => 180;

	internal static int CreeperChargeDelayMin => 70;

	internal static int CreeperChargeDelayMax => 100;

	internal static int CreeperChargePositioningTime => 60;

	internal static int CreeperChargeWindUpTime => 22;

	internal static int StunDuration => 480;

	internal static int SwipesStartupDuration => 120;

	internal int SwipeDuration => 60 + SwipeDelay;

	internal int SwipeDelay
	{
		get
		{
			if (!AttackFlag)
			{
				if (!CalamityWorld.death)
				{
					return 50;
				}
				return 40;
			}
			return 30;
		}
	}

	internal static int SwipeAmount => 4;

	internal int SwipeIchorDelay => 30 + SwipeDelay;

	internal static int LightSwipeDelay => 60;

	internal static int LightSwipeAmount
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 6;
			}
			return 8;
		}
	}

	internal static int LightSwipeTravelTime => 30;

	internal static int LightSwipeAttackDelay => 10;

	internal static int LightSwipeDuration
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 60;
			}
			return 45;
		}
	}

	internal static int StrongSwipeDelay => 90;

	internal static int StrongSwipeAmount
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 5;
			}
			return 7;
		}
	}

	internal static int StrongSwipeTravelTime => 45;

	internal static int StrongSwipeAttackDelay => 15;

	internal static int StrongSwipeDuration
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 80;
			}
			return 60;
		}
	}

	internal static int OrbitSetupDuration => 60;

	internal static int OrbitDuration => 720;

	internal static int OrbitAttackInterval => 120;

	internal static int OrbitAttackParticipantCount
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 3;
			}
			return 4;
		}
	}

	internal static float OrbitStandardRadius => 320f;

	internal static float OrbitTelegraphRadius => 480f;

	internal static float BaseRotationSpeed => 0.0175f;

	internal static int SpiralDuration => 720;

	internal static int SpiralSetupTime => 90;

	internal static int TendrilCount => 3;

	internal static float TendrilLength => 512f;

	internal static float TendrilStartDistance => 64f;

	internal static float MaxCreeperSway => 64f;

	internal static int StartingTimePerRevolutionMax
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 300;
			}
			return 270;
		}
	}

	internal static int StartingTimePerRevolutionMin
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 210;
			}
			return 180;
		}
	}

	internal static int EndingTimePerRevolutionMax
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 240;
			}
			return 210;
		}
	}

	internal static int EndingTimePerRevolutionMin
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 150;
			}
			return 120;
		}
	}

	internal static int SpeedUpDelayTime => 120;

	internal static int SpeedUpExtensionTime => 120;

	internal static float TurnAroundRatio => 0.6f;

	internal static float TurnAroundDurationRatio
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 0.125f;
			}
			return 0.1f;
		}
	}

	internal static float DefaultTeleportDistance => 360f;

	internal static int ChaseTime => 160;

	internal static int ChaseAmount => 2;

	internal static int IdleTeleportDuration
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 44;
			}
			return 36;
		}
	}

	internal static float ChaseMinSpeed => 3f;

	internal static float ChaseMaxSpeed => CalamityWorld.death ? 18 : 15;

	internal static int BloodlettingDuration => 675;

	internal static Vector2 HoverDistance
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(420f, 300f);
		}
	}

	internal static float HoverEndHeight => 300f;

	internal static int IchorRate
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 12;
			}
			return 10;
		}
	}

	internal static float IchorSpread => 1.5f;

	internal static float IchorVelocity => 3f;

	internal static int BloodshotRate => 90;

	internal static float BloodshotVelocity => 10f;

	internal static int DashPrepTime => 90;

	internal static int DashReelbackTime => 20;

	internal static int DashDuration => 30;

	internal static float DashVelocity => 32f;

	internal static int DashScytheRate
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 6;
			}
			return 5;
		}
	}

	internal static int SanguineTeleportCount => 5;

	internal static int SanguineScytheCount
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 10;
			}
			return 12;
		}
	}

	internal static int SanguineTeleportDuration => 30;

	internal static float SanguineTeleportDistance => 440f;

	internal static Vector2 SanguineFinalTeleportOffset
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(720f, 300f);
		}
	}

	internal static int SanguineAttackEndDelay => 30;

	internal static int SanguineAttackEndDuration => 100;

	internal static int SanguineAttackEndIchorRate => 10;

	internal static int CrimsonEyeAttackIdleDuration => 210;

	internal static int CrimsonEyeAttackSetUpDuration => 30;

	internal static int CrimsonEyeAttackBuildUpDuration => 120;

	internal static int CrimsonEyeRate => 60;

	internal static int CrimsonEyeCap => 40;

	internal static int CrimsonEyeAttackDuration => 960;

	internal static int CrimsonEyeAttackEndDuration => 210;

	internal static float TurnAccelerationMultiplier => 0.01f;

	internal static float TurnAccelerationDistanceBuffer => 160f;

	internal static float TurnAccelerationDistanceDivisor => 72f;

	internal static float IllusionDashTeleportDistance => 300f;

	internal static int IllusionDashTeleportDuration => 30;

	internal static float IllusionDashCloseInDistance => 280f;

	internal static float IllusionDashStartingSpinSpeed => 0.125f;

	internal static int IllusionDashSpinDuration => 100;

	internal static int IllusionDashFakeoutTeleportDuration => 16;

	internal static float IllusionDashVelocity => 30f;

	internal static int IllusionTrickAngleGroups
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 6;
			}
			return 8;
		}
	}

	internal static int IllusionTrickGroupSize
	{
		get
		{
			if (!CalamityWorld.death)
			{
				return 4;
			}
			return 5;
		}
	}

	internal static int IllusionTrickStunDuration => 120;

	internal static int IllusionTrickTimeLimit => 960;

	internal BrainAIState AIState
	{
		get
		{
			return (BrainAIState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (int)value;
		}
	}

	internal ref float Time => ref base.NPC.ai[1];

	internal ref float DespawnTime => ref base.NPC.ai[2];

	internal ref float CachedRatio => ref base.NPC.ai[3];

	internal int AttackSign
	{
		get
		{
			if (!isNegative)
			{
				return 1;
			}
			return -1;
		}
		set
		{
			isNegative = value == -1;
		}
	}

	private Player Target => Main.player[base.NPC.target];

	private static float CreeperHPRatio
	{
		get
		{
			float ratio = 0f;
			foreach (NPC creeper in Main.npc.Where((NPC n) => n.active && n.type == 267))
			{
				ratio += (float)creeper.life / (float)creeper.lifeMax;
			}
			if (ratio != 0f)
			{
				ratio /= (float)GetBrainOfCthuluCreepersCountRevDeath();
			}
			return ratio;
		}
	}

	private static float CreeperAmountRatio => (float)NPC.CountNPCS(267) / (float)GetBrainOfCthuluCreepersCountRevDeath();

	public override bool EnableMultiplayerSmoothingAheadOfAI => true;

	public override void SetDefaults(Mod mod)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = (base.NPC.defDamage = 36);
		BoCDrawOffset = Vector2.Zero;
		ShieldOpacity = 1f;
		ShieldScale = 1f;
		BrainOfCthulhuSystem.ScreenBlurStrength = 0f;
		if (Main.dedServ)
		{
			return;
		}
		int brainOfCthuluCreepersCount = GetBrainOfCthuluCreepersCountRevDeath();
		BrainOfCthulhuSystem.VerletTendrils = new(int, List<VerletSimulatedSegment>, int)[brainOfCthuluCreepersCount];
		for (int i = 0; i < brainOfCthuluCreepersCount; i++)
		{
			List<VerletSimulatedSegment> tendril = new List<VerletSimulatedSegment>();
			for (int j = 0; j < 28; j++)
			{
				tendril.Add(new VerletSimulatedSegment(base.NPC.Center));
			}
			BrainOfCthulhuSystem.VerletTendrils[i].tendril = tendril;
		}
	}

	public override void OnSpawn(Mod mod)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
			options.aggroRatio = -1f;
			options.finishThemOff = true;
			base.NPC.CalamityTargeting(options);
		}
		Player target = Main.player[base.NPC.target];
		bool onSurface = (double)(target.Center.Y / 16f) < Main.worldSurface;
		base.NPC.Center = target.Center + Vector2.UnitY * (float)(onSurface ? 900 : (-900));
		base.DisableMultiplayerSmoothing = true;
		base.NPC.dontTakeDamage = true;
		AIState = (onSurface ? BrainAIState.SurfaceSpawnAnimation : BrainAIState.UndergroundSpawnAnimation);
		PreviousAttack = BrainAIState.Phase1Idle;
		SpawnDelay = ((SummonedViaItem || BossRushEvent.BossRushActive) ? 2 : 60);
		if (SummonedViaItem || BossRushEvent.BossRushActive)
		{
			SpawnTime = -1f;
		}
		if (Main.netMode != 1)
		{
			int brainOfCthuluCreepersCount = GetBrainOfCthuluCreepersCountRevDeath();
			for (int i = 0; i < brainOfCthuluCreepersCount; i++)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 267, base.NPC.whoAmI, i, 0f, -1f);
			}
		}
		base.NPC.netUpdate = true;
	}

	public override bool AI(Mod mod)
	{
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		NPC.crimsonBoss = base.NPC.whoAmI;
		bool phase2 = (int)AIState >= 9;
		if (phase2)
		{
			base.NPC.knockBackResist = 0f;
			base.NPC.defense = base.NPC.defDefense;
			base.NPC.chaseable = AIState != BrainAIState.IllusionDash && AIState != BrainAIState.IllusionTrick;
		}
		else
		{
			base.NPC.defense = (int)((float)base.NPC.defDefense * Phase1DefenseMultiplier);
		}
		base.NPC.extraValue = 0;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
			options.aggroRatio = -1f;
			options.finishThemOff = true;
			options.maxSearchRange = DespawnRange;
			base.NPC.CalamityTargeting(options);
		}
		if (!BossRushEvent.BossRushActive && AIState != BrainAIState.DeathAnimation)
		{
			bool despawn = Target.dead || !Target.ZoneCrimson;
			if (despawn)
			{
				List<int> v = GetAllValidTargets(base.NPC.Center);
				if (v.Count > 0)
				{
					despawn = false;
					base.NPC.target = v[0];
				}
			}
			if (despawn)
			{
				if (DespawnTime < 90f)
				{
					DespawnTime++;
				}
				if (DespawnTime == 90f)
				{
					base.NPC.velocity.Y += 0.1f;
				}
			}
			else if (DespawnTime > 0f)
			{
				DespawnTime--;
			}
			if (Main.netMode != 1 && Target.DistanceSQ(base.NPC.Center) > DespawnRangeSQ)
			{
				base.NPC.active = false;
				base.NPC.life = 0;
				if (Main.dedServ)
				{
					NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
				}
			}
			if (DespawnTime > 60f)
			{
				return false;
			}
		}
		if (AIState == BrainAIState.Stunned)
		{
			base.NPC.HitSound = StunnedHit;
		}
		else
		{
			base.NPC.HitSound = SoundID.NPCHit9;
		}
		if (!phase2 && CreeperHPRatio == 0f && AIState != BrainAIState.Stunned && (int)AIState >= 2)
		{
			AIState = BrainAIState.Stunned;
			Time = 0f;
			ResetAttackValues();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<TelekineticEnemyGrab>())
				{
					p.ai[1] = 0f;
				}
			}
		}
		if (AIState == BrainAIState.Stunned && (float)base.NPC.life / (float)base.NPC.lifeMax < Phase2HealthGate)
		{
			AIState = BrainAIState.Phase2TransitionClosed;
			Time = 0f;
			TeleportTime = 0f;
		}
		switch (AIState)
		{
		case BrainAIState.UndergroundSpawnAnimation:
		case BrainAIState.SurfaceSpawnAnimation:
			SpawnAnimation();
			break;
		case BrainAIState.Phase1Idle:
			Phase1Idle();
			break;
		case BrainAIState.TelekineticOnslaught:
			TelekineticOnslaught();
			break;
		case BrainAIState.Stunned:
			Stunned();
			break;
		case BrainAIState.CreeperSwipes:
			CreeperSwipes();
			break;
		case BrainAIState.CreeperSwings:
			CreeperSwings();
			break;
		case BrainAIState.CreeperOrbit:
			CreeperOrbit();
			break;
		case BrainAIState.CreeperSpiral:
			CreeperSpiral();
			break;
		case BrainAIState.Phase2TransitionClosed:
		case BrainAIState.Phase2TransitionOpen:
			PhaseTransition();
			break;
		case BrainAIState.Phase2Idle:
			Phase2Idle();
			break;
		case BrainAIState.Bloodletting:
			Bloodletting();
			break;
		case BrainAIState.SanguineScythes:
			SanguineScythes();
			break;
		case BrainAIState.CrimsonEyes:
			CrimsonEyes();
			break;
		case BrainAIState.IllusionDash:
			IllusionDash();
			break;
		case BrainAIState.IllusionTrick:
			IllusionTrick();
			break;
		case BrainAIState.DeathAnimation:
			DeathAnimation();
			break;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile p2 = enumerator2.Current;
			if (p2.type != 814 || p2.ai[0] == 0f)
			{
				continue;
			}
			int startUpTime = 20;
			float speedUpTime = 30f;
			float slowDownMult = 0.96f;
			float speedUpMult = 1.025f;
			if (AIState == BrainAIState.IllusionDash)
			{
				startUpTime = 20;
				speedUpTime = 30f;
				slowDownMult = 0.96f;
				speedUpMult = 1.025f;
			}
			if (p2.ai[2] <= (float)startUpTime)
			{
				p2.velocity *= slowDownMult;
			}
			else
			{
				p2.velocity *= speedUpMult;
				if (p2.ai[2] <= (float)startUpTime + speedUpTime)
				{
					float newAngle = p2.ai[1].AngleLerp(p2.ai[0] - (float)Math.PI * 2f, (p2.ai[2] - (float)startUpTime) / speedUpTime);
					p2.velocity = newAngle.ToRotationVector2() * ((Vector2)(ref p2.velocity)).Length();
				}
			}
			p2.ai[2]++;
		}
		base.NPC.oldVelocity = base.NPC.velocity;
		Time++;
		if (AIState != BrainAIState.DeathAnimation && base.NPC.lifeRegen < 0 && Math.Abs(base.NPC.lifeRegen) >= base.NPC.life)
		{
			TriggerDeathAnimation();
		}
		return false;
	}

	private void SpawnAnimation()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Calamity().adrenaline = 0f;
		}
		if (SpawnTime != 0f)
		{
			float d = Main.LocalPlayer.DistanceSQ(base.NPC.Center);
			float distanceScaleFactor = 1f;
			if (d > 592900f)
			{
				distanceScaleFactor = 1f / (1f + ((float)Math.Sqrt(d) - 770f) / 32f);
			}
			float spawnCounter = Time - Math.Abs(SpawnTime);
			if (spawnCounter < 180f)
			{
				float shakeIntensity = CalamityUtils.CircOutEasing(spawnCounter / 180f, 1) * 3f * distanceScaleFactor;
				Main.LocalPlayer.SetScreenshake(shakeIntensity);
				for (int i = 0; (float)i < shakeIntensity; i++)
				{
					Point start = Target.Center.ToTileCoordinates() + new Point(Main.rand.Next(-64, 65), 48);
					for (int j = 0; j < 96; j++)
					{
						Point current = start - new Point(0, j);
						if (!Main.tile[current].IsTileSolid() && Main.tile[current - new Point(0, 1)].TileType == 203)
						{
							Dust.NewDust(current.ToWorldCoordinates(0f, -16f), 16, 16, 117, 0f, 3f);
						}
					}
				}
			}
			if (spawnCounter == 180f)
			{
				base.NPC.velocity = Vector2.UnitY * (float)((AIState == BrainAIState.UndergroundSpawnAnimation) ? 32 : (-50));
			}
			else if (spawnCounter > 180f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.955f;
				if (spawnCounter == 240f)
				{
					SoundEngine.PlaySound(in IntroRoar, base.NPC.Center);
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue("Announcement.HasAwoken", base.NPC.TypeName), 175, 75);
					}
					else if (Main.dedServ)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", base.NPC.TypeName), new Color(175, 75, 255));
					}
				}
				if (spawnCounter > 240f && spawnCounter < 390f)
				{
					BrainOfCthulhuSystem.ScreenBlurStrength = 0.5f;
					if (spawnCounter < 250f)
					{
						BrainOfCthulhuSystem.ScreenBlurStrength = MathHelper.Lerp(0f, 0.5f, (spawnCounter - 240f) / 10f);
					}
					base.NPC.frameCounter++;
					BoCDrawOffset = Main.rand.NextVector2Circular(4f, 4f);
					for (int k = 0; k < 3; k++)
					{
						Point start2 = Target.Center.ToTileCoordinates() + new Point(Main.rand.Next(-64, 65), 48);
						for (int l = 0; l < 96; l++)
						{
							Point current2 = start2 - new Point(0, l);
							if (!Main.tile[current2].IsTileSolid() && Main.tile[current2 - new Point(0, 1)].TileType == 203)
							{
								Dust.NewDust(current2.ToWorldCoordinates(0f, -16f), 16, 16, 117, 0f, 3f);
							}
						}
					}
					Main.LocalPlayer.SetScreenshake(6f * BrainOfCthulhuSystem.ScreenBlurStrength * distanceScaleFactor);
					if (spawnCounter % 15f == 0f)
					{
						GeneralParticleHandler.SpawnParticle(new BossRoar(base.NPC.Center, Color.Black, Main.rand.NextFloatDirection(), 0.1f, 3f, 30));
					}
				}
				else if (spawnCounter >= 390f && spawnCounter <= 420f)
				{
					BrainOfCthulhuSystem.ScreenBlurStrength = MathHelper.Lerp(0.5f, 0f, (spawnCounter - 390f) / 30f);
					BoCDrawOffset *= 0.75f;
				}
				else if (spawnCounter > 420f)
				{
					BrainOfCthulhuSystem.ScreenBlurStrength = 0f;
					BoCDrawOffset = Vector2.Zero;
					AIState = BrainAIState.Phase1Idle;
					base.NPC.damage = base.NPC.defDamage;
					ResetAttackValues();
					Time = -1f;
					SpawnTime = -1f;
					Main.musicFade[Main.curMusic] = 1f;
					return;
				}
			}
		}
		if (AttackCounter < GetBrainOfCthuluCreepersCountRevDeath())
		{
			if (SpawnTime == 0f)
			{
				base.NPC.Center = Target.Center + Vector2.UnitY * (float)((AIState == BrainAIState.UndergroundSpawnAnimation) ? (-900) : 900);
				base.DisableMultiplayerSmoothing = true;
			}
			if (SpawnDelay == 1 && Main.netMode != 1)
			{
				bool targetLeft = AttackCounter % 2 == 0;
				List<NPC> creepers = Main.npc.Where((NPC n) => n.active && n.type == 267 && n.AIOverride<CreeperAI>().Time == -1 && n.AIOverride<CreeperAI>().CreeperID % 2 == ((!targetLeft) ? 1 : 0)).ToList();
				if (creepers.Count > 0)
				{
					AttackTime = creepers[Main.rand.Next(creepers.Count)].whoAmI;
				}
				else
				{
					creepers = Main.npc.Where((NPC n) => n.active && n.type == 267 && n.AIOverride<CreeperAI>().Time == -1).ToList();
					AttackTime = ((creepers.Count == 0) ? (-1) : creepers[Main.rand.Next(creepers.Count)].whoAmI);
				}
				base.NPC.netUpdate = true;
			}
			if (SpawnDelay <= 0)
			{
				if (AttackTime != -1f)
				{
					NPC obj = Main.npc[(int)AttackTime];
					obj.AIOverride<CreeperAI>().Time = 1;
					obj.netUpdate = true;
					AttackCounter++;
					if (SummonedViaItem || BossRushEvent.BossRushActive)
					{
						SpawnDelay = 2;
						return;
					}
					int spawnDelay;
					switch (AttackCounter)
					{
					case 1:
						spawnDelay = 90;
						break;
					case 2:
					case 3:
					case 4:
						spawnDelay = 24;
						break;
					case 5:
						spawnDelay = 60;
						break;
					case 6:
					case 7:
					case 8:
						spawnDelay = 24;
						break;
					case 9:
						spawnDelay = 60;
						break;
					default:
						spawnDelay = 4;
						break;
					}
					SpawnDelay = spawnDelay;
				}
				else
				{
					SpawnTime = Time;
				}
			}
			else
			{
				SpawnDelay--;
			}
		}
		else if (SpawnTime == 0f)
		{
			SpawnTime = Time;
		}
	}

	private void Phase1Idle()
	{
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		BrainOfCthulhuSystem.ScreenBlurStrength = 0f;
		if (Main.netMode != 1)
		{
			if (CreeperHPRatio <= DesperateOnslaughtCreeperHealthGate)
			{
				Time = -1f;
				AIState = BrainAIState.TelekineticOnslaught;
				AttackSign = ((!Main.rand.NextBool()) ? 1 : (-1));
				base.NPC.netUpdate = true;
			}
			else if (Time > (float)IdlePeriodDuration)
			{
				Time = -1f;
				ResetAttackValues();
				if (availableAttacks.Count == 0)
				{
					int num = 4;
					List<BrainAIState> list = new List<BrainAIState>(num);
					CollectionsMarshal.SetCount(list, num);
					Span<BrainAIState> span = CollectionsMarshal.AsSpan(list);
					int num2 = 0;
					span[num2] = BrainAIState.CreeperSwipes;
					num2++;
					span[num2] = BrainAIState.CreeperSwings;
					num2++;
					span[num2] = BrainAIState.CreeperOrbit;
					num2++;
					span[num2] = BrainAIState.CreeperSpiral;
					availableAttacks = list;
					if (PreviousAttack != BrainAIState.Phase1Idle)
					{
						availableAttacks.Remove(PreviousAttack);
					}
				}
				int pick = Main.rand.Next(availableAttacks.Count);
				AIState = availableAttacks[pick];
				availableAttacks.RemoveAt(pick);
				PreviousAttack = AIState;
				foreach (NPC item in Main.npc.Where((NPC n) => n.active && n.type == 267))
				{
					item.AIOverride<CreeperAI>().Time = -1;
				}
				SoundEngine.PlaySound(in Growl, base.NPC.Center);
				base.NPC.netUpdate = true;
			}
		}
		if (Time == 0f)
		{
			AttackSign = ((!Main.rand.NextBool()) ? 1 : (-1));
			base.NPC.netUpdate = true;
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC creeper = enumerator2.Current;
				if (creeper.type == 267)
				{
					creeper.netUpdate = true;
				}
			}
		}
		float rotateDir = AttackSign;
		Vector2 dir = base.NPC.DirectionFrom(Target.Center).RotatedBy(Math.Sin(Time / 60f) * (double)rotateDir) * new Vector2(2f, 1f);
		float rayDist = CalamityUtils.PreciseDistanceToTileCollisionHit(Target.Center, dir.ToRotation(), 360f);
		Vector2 offset = dir * (rayDist - (float)base.NPC.width);
		Vector2 goalPos = Target.Center + offset;
		float distSQ = base.NPC.DistanceSQ(goalPos);
		if (distSQ > 129600f)
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos) * (4f + (base.NPC.Distance(goalPos) - 360f) / 64f);
		}
		else if (distSQ <= 2048f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
		}
		else if (((Vector2)(ref base.NPC.velocity)).LengthSquared() < 16f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity += base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.Zero) * 0.15f;
		}
		else
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.Zero) * 6f;
		}
	}

	private void TelekineticOnslaught()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
		{
			base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * 4f;
			Time = -1f;
		}
		else
		{
			float distSQ = base.NPC.DistanceSQ(Target.Center);
			if (distSQ > 230400f)
			{
				base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * (MathF.Sqrt(distSQ) - 480f) / 128f;
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
		}
		float wrappedCounter = Time % 90f;
		if (Time <= 60f)
		{
			if (Time == 0f)
			{
				SoundEngine.PlaySound(in Roar, base.NPC.Center);
				if (Main.netMode != 1)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC creeper = enumerator.Current;
						if (creeper.type == 267)
						{
							CreeperAI ai = creeper.AIOverride<CreeperAI>();
							int dirSign = ((!ai.evenID) ? 1 : (-1));
							Vector2 dir = Vector2.UnitX.RotatedBy(Main.rand.NextFloat((float)Math.PI * -2f / 3f, (float)Math.PI * 2f / 3f)) * (float)dirSign;
							if ((double)(Target.Center.Y / 16f) < Main.worldSurface)
							{
								float dirY = dir.Y;
								dir.Y -= dirY * 0.5f;
								dir.X += dirY * 0.5f * (float)Math.Sign(dir.X);
								dir.SafeNormalize(Vector2.unitXVector * (float)dirSign);
							}
							ai.AttackAngle = dir.ToRotation();
							float rayDist = CalamityUtils.PreciseDistanceToTileCollisionHit(base.NPC.Center, ai.AttackAngle, 800f, 4f);
							ai.AttackPosition = base.NPC.Center + dir * (rayDist - 64f);
							creeper.netUpdate = true;
						}
					}
					base.NPC.netUpdate = true;
				}
			}
			if (Time < 30f)
			{
				BrainOfCthulhuSystem.ScreenBlurStrength = 0.5f;
				base.NPC.frameCounter++;
				for (int i = 0; i < 3; i++)
				{
					Point start = Target.Center.ToTileCoordinates() + new Point(Main.rand.Next(-64, 65), 48);
					for (int j = 0; j < 96; j++)
					{
						Point current = start - new Point(0, j);
						if (!Main.tile[current].IsTileSolid() && Main.tile[current - new Point(0, 1)].TileType == 203)
						{
							Dust.NewDust(current.ToWorldCoordinates(0f, -16f), 16, 16, 117, 0f, 3f);
						}
					}
				}
				float d = Main.LocalPlayer.DistanceSQ(base.NPC.Center);
				float distanceScaleFactor = 1f;
				if (d > 592900f)
				{
					distanceScaleFactor = 1f / (1f + ((float)Math.Sqrt(d) - 770f) / 32f);
				}
				Main.LocalPlayer.SetScreenshake(4f * BrainOfCthulhuSystem.ScreenBlurStrength * distanceScaleFactor);
				if (Time % 15f == 0f)
				{
					GeneralParticleHandler.SpawnParticle(new BossRoar(base.NPC.Center, Color.Black, Main.rand.NextFloatDirection(), 0.1f, 3f, 30));
				}
			}
			else
			{
				BrainOfCthulhuSystem.ScreenBlurStrength = MathHelper.Lerp(0.5f, 0f, CalamityUtils.CircOutEasing((Time - 30f) / 30f, 1));
			}
			return;
		}
		BrainOfCthulhuSystem.ScreenBlurStrength = 0f;
		if (wrappedCounter != 65f)
		{
			return;
		}
		if (Main.netMode != 0)
		{
			SelectNewTarget();
			base.NPC.netUpdate = true;
		}
		int checkCount = 8;
		float wallDist = CalamityUtils.PreciseDistanceToTileCollisionHit(base.NPC.Center, (AttackSign == -1) ? ((float)Math.PI) : 0f, 480 + base.NPC.width) - (float)base.NPC.width;
		Vector2[] starts = (Vector2[])(object)new Vector2[checkCount];
		for (int k = 0; k < checkCount; k++)
		{
			float completion = (float)(k + 1) / (float)(checkCount + 1);
			starts[k] = base.NPC.Center + Vector2.UnitX * (wallDist * completion + (float)base.NPC.width) * (float)AttackSign;
		}
		Vector2[] ends = (Vector2[])(object)new Vector2[checkCount];
		List<Vector2> goodEnds = new List<Vector2>();
		List<Vector2> farEnds = new List<Vector2>();
		List<Vector2> closeEnds = new List<Vector2>();
		for (int l = 0; l < checkCount; l++)
		{
			float maxDist = 960f;
			float floorDist = CalamityUtils.PreciseDistanceToTileCollisionHit(base.NPC.Center, Vector2.UnitY.ToRotation(), maxDist);
			ends[l] = starts[l] + Vector2.UnitY * (floorDist + 48f);
			if (floorDist >= 600f)
			{
				farEnds.Add(ends[l]);
			}
			else if (floorDist > 240f)
			{
				goodEnds.Add(ends[l]);
			}
			else
			{
				closeEnds.Add(ends[l]);
			}
		}
		Vector2 chosenEnd = ((goodEnds.Count > 0) ? goodEnds[Main.rand.Next(goodEnds.Count)] : ((closeEnds.Count <= 0) ? farEnds[Main.rand.Next(farEnds.Count)] : closeEnds[Main.rand.Next(closeEnds.Count)]));
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromThis(), chosenEnd, Vector2.Zero, ModContent.ProjectileType<TelekineticEnemyGrab>(), 10, 0.5f);
		}
		AttackSign *= -1;
	}

	private void Stunned()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 6f);
		if (Time == 0f)
		{
			base.NPC.velocity = (base.NPC.Center - Target.Center).SafeNormalize(Vector2.UnitX) * 4f;
			SoundEngine.PlaySound(in ShieldDown, base.NPC.Center);
		}
		if (base.NPC.velocity != Vector2.Zero)
		{
			if (Time >= (float)StunDuration)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.8f;
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.93f;
			}
		}
		if (Time < (float)StunDuration)
		{
			base.NPC.position.Y += (float)Math.Sin(Time / 8f) * 2f * (1f - MathHelper.Clamp((Time - (float)(StunDuration - 30)) / 30f, 0f, 1f));
		}
		if (Time <= (float)(StunDuration - 30))
		{
			if (AttackTime > 0f)
			{
				float kbCounter = 30f - AttackTime;
				if (kbCounter < 10f)
				{
					float lerp = CalamityUtils.SineOutEasing(kbCounter / 10f, 1);
					base.NPC.rotation = AttackRotation.AngleLerp(0f - AttackRotation, lerp);
				}
				else
				{
					float lerp2 = CalamityUtils.SineInOutEasing((kbCounter - 10f) / 20f, 1);
					base.NPC.rotation = (0f - AttackRotation).AngleLerp(0f, lerp2);
				}
			}
			else if (Math.Abs(base.NPC.oldVelocity.X) < Math.Abs(base.NPC.velocity.X) || Time <= 0f)
			{
				AttackRotation = base.NPC.rotation;
				TeleportTime = 0f;
			}
			else
			{
				TeleportTime++;
				base.NPC.rotation = MathHelper.Lerp(AttackRotation, (float)Math.PI / 24f * base.NPC.oldVelocity.X, CalamityUtils.CircOutEasing(MathHelper.Clamp(TeleportTime / 30f, 0f, 1f), 1));
			}
		}
		if (Time < (float)StunDuration)
		{
			if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * 4f;
			}
			else if (Collision.SolidCollision(base.NPC.position + base.NPC.velocity, base.NPC.width, base.NPC.height))
			{
				if (base.NPC.velocity.X != base.NPC.oldVelocity.X)
				{
					base.NPC.velocity.X = 0f - base.NPC.oldVelocity.X;
				}
				if (base.NPC.velocity.Y != base.NPC.oldVelocity.Y)
				{
					base.NPC.velocity.Y = 0f - base.NPC.oldVelocity.Y;
				}
				base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 8f);
				AttackTime = 30f;
				AttackRotation = base.NPC.rotation;
			}
			if (AttackTime > 0f)
			{
				base.NPC.knockBackResist = 0f;
				AttackTime--;
				if (AttackTime == 0f)
				{
					base.NPC.velocity = Vector2.Zero;
					AttackRotation = 0f;
				}
			}
			else
			{
				base.NPC.knockBackResist = 1f;
			}
		}
		BrainOfCthulhuSystem.ScreenBlurStrength = 0f;
		base.NPC.dontTakeDamage = false;
		base.NPC.damage = 0;
		if (Time <= 15f)
		{
			float lerp3 = Time / 15f;
			ShieldOpacity = 1f - CalamityUtils.CircOutEasing(lerp3, 1);
			ShieldScale = MathHelper.Lerp(1f, 1.5f, lerp3);
		}
		if (Time > 15f && Time < (float)StunDuration)
		{
			ShieldOpacity = 0f;
			ShieldScale = 1.5f;
		}
		if (OnSecondCreeperPhase && Time == (float)(StunDuration - 30))
		{
			AIState = BrainAIState.Phase2TransitionClosed;
			Time = -1f;
			TeleportTime = 0f;
			return;
		}
		if (Time > (float)(StunDuration - 30))
		{
			base.NPC.rotation = base.NPC.rotation.AngleLerp(0f, CalamityUtils.SineInOutEasing((Time - (float)(StunDuration - 30)) / 30f, 1));
			if (Time == (float)(StunDuration - 15))
			{
				SoundEngine.PlaySound(in ShieldUp, base.NPC.Center);
			}
		}
		if (!(Time >= (float)StunDuration))
		{
			return;
		}
		if (base.NPC.velocity.X < 0.001f && base.NPC.velocity.X < 0.001f)
		{
			base.NPC.velocity = Vector2.Zero;
		}
		int creeperRate = 5;
		float wrappedCounter = (Time - (float)StunDuration) % (float)creeperRate;
		int spawnTime = GetBrainOfCthuluCreepersCountRevDeath() / 2 * creeperRate;
		if (Time == (float)StunDuration)
		{
			AttackCounter = GetBrainOfCthuluCreepersCountRevDeath() - 1;
			SoundEngine.PlaySound(in Roar, base.NPC.Center);
		}
		base.NPC.knockBackResist = 0f;
		base.NPC.dontTakeDamage = true;
		base.NPC.rotation = 0f;
		float shieldAppearTime = 15f;
		float lerp4 = MathHelper.Clamp((Time - (float)StunDuration) / shieldAppearTime, 0f, 1f);
		if (lerp4 >= 1f)
		{
			ShieldOpacity = 1f;
			ShieldScale = 1f;
		}
		else
		{
			ShieldOpacity = CalamityUtils.CircOutEasing(lerp4, 1);
			ShieldScale = MathHelper.Lerp(1.5f, 1f, CalamityUtils.SineOutEasing(lerp4, 1));
		}
		if (AttackCounter == -1 && Time > (float)(StunDuration + spawnTime + 30))
		{
			OnSecondCreeperPhase = true;
			AIState = BrainAIState.Phase1Idle;
			Time = -1f;
			base.NPC.damage = base.NPC.defDamage;
		}
		else
		{
			if (AttackCounter <= -1 || wrappedCounter != 0f)
			{
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 dir = Vector2.UnitY.RotatedBy((float)((AttackCounter % 2 == 0) ? 1 : (-1)) * ((float)Math.PI / 16f + (float)Math.PI * 7f / 8f * ((float)AttackCounter / 2f / ((float)GetBrainOfCthuluCreepersCountRevDeath() / 2f))));
				Vector2 spawnPos = base.NPC.Center + dir * 72f;
				if (Main.netMode != 1)
				{
					NPC.NewNPCDirect(base.NPC.GetSource_FromAI(), spawnPos, 267, base.NPC.whoAmI, AttackCounter, 0f, -1f, 1f).velocity = dir * 24f;
				}
				for (int j = 0; j < 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new BloodParticle(spawnPos, dir.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(8f, 12f), 32, 1f, Color.Red));
				}
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(spawnPos, dir * 10f, 16, 0.5f, Color.Red));
				AttackCounter--;
				if (AttackCounter <= -1)
				{
					return;
				}
			}
			SoundEngine.PlaySound(in SoundID.NPCHit9, base.NPC.Center);
		}
	}

	private void CreeperSwipes()
	{
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f && Main.netMode != 1)
		{
			int leftAmt = 0;
			int rightAmt = 0;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC creeper = enumerator.Current;
				if (creeper.type == 267)
				{
					if (creeper.AIOverride<CreeperAI>().CreeperID % 2 == 0)
					{
						leftAmt++;
					}
					else
					{
						rightAmt++;
					}
				}
			}
			if (leftAmt > 5 && rightAmt > 5)
			{
				AttackSign = ((!Main.rand.NextBool()) ? 1 : (-1));
				AttackFlag = false;
			}
			else
			{
				AttackFlag = true;
			}
			base.NPC.damage = 0;
			base.NPC.netUpdate = true;
			AttackList.Clear();
			TargetsSet.Clear();
		}
		float wrappedCount = Time % (float)(SwipeDuration + SwipeDelay);
		if (Time >= (float)SwipesStartupDuration)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (wrappedCount == 0f)
			{
				bool useEven = Main.rand.NextBool();
				bool anyActivated = false;
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC Npc = enumerator2.Current;
					if (Npc.type == 267 && (AttackFlag || ((Npc.AIOverride<CreeperAI>().CreeperID % 2 == 0) ^ useEven)))
					{
						AttackList.Add((byte)Npc.whoAmI);
						anyActivated = true;
					}
				}
				if (!anyActivated)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						NPC Npc2 = enumerator3.Current;
						if (Npc2.type == 267)
						{
							AttackList.Add((byte)Npc2.whoAmI);
						}
					}
				}
				if (Main.netMode != 0)
				{
					SelectNewTarget();
				}
				base.NPC.netUpdate = true;
			}
			else if (wrappedCount == (float)SwipeDuration && Main.netMode != 1)
			{
				AttackSign *= -1;
				AttackList.Clear();
				base.NPC.netUpdate = true;
			}
			if (wrappedCount > 1f && wrappedCount <= (float)SwipeIchorDelay && Time % 3f == 0f)
			{
				Vector2 spawnPosition = base.NPC.Center;
				spawnPosition.Y += Main.rand.NextFloat(38f, 50f);
				spawnPosition.X += Main.rand.NextFloat(-56f, 56f);
				GeneralParticleHandler.SpawnParticle(new BloodParticle(spawnPosition, Main.rand.NextVector2Unit() * Main.rand.NextFloat(1.5f, 2.5f), Main.rand.Next(30, 40), Main.rand.NextFloat(0.6f, 1f), Color.Gold));
			}
			if (wrappedCount < 60f)
			{
				Vector2 vibrationVector = Main.rand.NextVector2CircularEdge(1f, 1f) * MathHelper.Lerp(0f, 12f, CalamityUtils.CircInEasing(wrappedCount / 80f, 1));
				BoCDrawOffset = vibrationVector;
			}
			else if (wrappedCount > 60f && wrappedCount < 80f)
			{
				float progress = (wrappedCount - 60f) / 20f;
				BoCDrawOffset = new Vector2(0f, MathHelper.Lerp(10f, 0f, 1f - (float)Math.Pow(1f - progress, 3.0)));
			}
			if (Main.netMode != 1 && wrappedCount > (float)SwipeIchorDelay && wrappedCount <= (float)SwipeDuration && Time % 2f == 0f)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center + new Vector2(Main.rand.NextFloat(-72f, 72f), 56f), Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)) * 4f, 288, IchorShotDamage, 0.5f);
			}
			if (Time >= (float)(SwipesStartupDuration + (SwipeDuration + SwipeDelay) * SwipeAmount) && Main.netMode != 1)
			{
				Time = 0f;
				AIState = BrainAIState.Phase1Idle;
				base.NPC.netUpdate = true;
				base.NPC.damage = base.NPC.defDamage;
				AttackList.Clear();
			}
		}
		else
		{
			base.NPC.damage = 0;
		}
		Vector2 goalPos = Target.Center + Vector2.UnitY * -270f;
		float distSQ = base.NPC.DistanceSQ(goalPos);
		if ((Time > (float)SwipesStartupDuration && wrappedCount > 30f && wrappedCount <= (float)SwipeDuration) || base.NPC.DistanceSQ(goalPos) <= 2048f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
		}
		else if (distSQ > 14400f)
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos) * (8f + (base.NPC.Distance(goalPos) - 120f) / 16f);
		}
		else
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.UnitX * (float)(-Target.direction)) * (8f * distSQ / 14400f);
		}
	}

	private void CreeperSwings()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		Vector2 fromTarget;
		if (Main.netMode == 0)
		{
			fromTarget = base.NPC.Center - Target.Center;
		}
		else
		{
			Vector2 averagePlayerPos = Vector2.zeroVector;
			List<int> targets = GetAllValidTargets(base.NPC.Center);
			foreach (int p in targets)
			{
				averagePlayerPos += Main.player[p].Center;
			}
			averagePlayerPos /= (float)targets.Count;
			fromTarget = base.NPC.Center - averagePlayerPos;
		}
		Vector2 goalDir = ((!(Math.Abs(fromTarget.X) > Math.Abs(fromTarget.Y))) ? (Vector2.UnitY * (float)Math.Sign(fromTarget.Y)) : (Vector2.UnitX * (float)Math.Sign(fromTarget.X)));
		Vector2 goalPos = Target.Center + goalDir * 360f - Vector2.UnitY * 32f;
		if (base.NPC.DistanceSQ(goalPos) <= 2048f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
		}
		else if (((Vector2)(ref base.NPC.velocity)).LengthSquared() <= 56.25f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity += base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.UnitX * (float)(-Target.direction)) * 0.5f;
		}
		else
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.UnitX * (float)(-Target.direction)) * 8f;
		}
		int delay = (OnSecondCreeperPhase ? StrongSwipeDelay : LightSwipeDelay);
		if (Time == 0f)
		{
			AttackList.Clear();
		}
		if (!(Time > (float)delay))
		{
			return;
		}
		foreach (NPC item in Main.npc.Where((NPC n) => n.active && n.type == 267 && n.AIOverride<CreeperAI>().Time == -1 && !AttackList.Contains((byte)n.whoAmI)))
		{
			item.position += base.NPC.velocity;
		}
		int crushCount;
		int attackDelay;
		if (!OnSecondCreeperPhase)
		{
			crushCount = LightSwipeAmount;
			attackDelay = LightSwipeDuration;
		}
		else
		{
			crushCount = StrongSwipeAmount;
			attackDelay = StrongSwipeDuration;
		}
		int attackDur = delay + (attackDelay + 1) * crushCount;
		if (Time < (float)attackDur && Time % (float)attackDelay == 0f)
		{
			List<NPC> creepers = Main.npc.Where((NPC n) => n.active && n.type == 267 && n.AIOverride<CreeperAI>().Time == -1 && !AttackList.Contains((byte)n.whoAmI)).ToList();
			if (creepers.Count > 1 && Main.netMode != 1)
			{
				float rotation;
				if (!OnSecondCreeperPhase)
				{
					rotation = Target.velocity.ToRotation();
					if (Target.velocity == Vector2.Zero)
					{
						rotation = ((Target.direction == 1) ? 0f : ((float)Math.PI));
					}
					rotation += Main.rand.NextFloat(-(float)Math.PI / 8f, (float)Math.PI / 8f);
				}
				else
				{
					rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
				}
				int rand = Main.rand.Next(creepers.Count);
				NPC first = creepers[rand];
				first.netUpdate = true;
				CreeperAI creeper1 = first.AIOverride<CreeperAI>();
				AttackList.Add((byte)first.whoAmI);
				creeper1.AttackAngle = rotation;
				creepers.RemoveAt(rand);
				NPC second = creepers[Main.rand.Next(creepers.Count)];
				second.netUpdate = true;
				CreeperAI? creeperAI = second.AIOverride<CreeperAI>();
				AttackList.Add((byte)second.whoAmI);
				creeperAI.AttackAngle = rotation + (float)Math.PI;
				creeper1.PartnerIndex = second.whoAmI;
				creeperAI.PartnerIndex = first.whoAmI;
				if (Main.netMode != 0)
				{
					SelectNewTarget();
				}
				base.NPC.netUpdate = true;
			}
		}
		if (Time > (float)(attackDur + attackDelay))
		{
			Time = 0f;
			base.NPC.damage = base.NPC.defDamage;
			AIState = BrainAIState.Phase1Idle;
			AttackList.Clear();
		}
	}

	private void CreeperOrbit()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		Vector2 fromTarget = (base.NPC.Center - Target.Center).SafeNormalize(Vector2.UnitX);
		Vector2 goalPos = Target.Center + fromTarget * 440f - Vector2.UnitY * 32f;
		float distSQ = base.NPC.DistanceSQ(goalPos);
		if (distSQ > 14400f)
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos) * (4f + (base.NPC.Distance(goalPos) - 120f) / 16f);
		}
		else
		{
			base.NPC.velocity = base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.Zero) * (2f + 2f * (distSQ / 14400f));
		}
		if (Time == 0f && Main.netMode != 1)
		{
			AttackSign = ((!Main.rand.NextBool()) ? 1 : (-1));
			AttackPosition = Target.Center;
			AttackList.Clear();
			base.NPC.netUpdate = true;
			base.NPC.damage = 0;
			if (Main.netMode != 0)
			{
				List<int> extraTargets = GetAllValidTargets(base.NPC.Center);
				extraTargets.Remove(base.NPC.target);
				int targetCount = extraTargets.Count;
				int creepersPerExtraPlayer = (CalamityWorld.death ? 3 : 2);
				int creepersDesired = targetCount * creepersPerExtraPlayer;
				int creepersToSpare = NPC.CountNPCS(267) - 4;
				for (int i = 0; i < creepersPerExtraPlayer; i++)
				{
					if (creepersToSpare >= creepersDesired)
					{
						break;
					}
					if (creepersPerExtraPlayer == 1)
					{
						break;
					}
					if (creepersToSpare < creepersDesired && creepersPerExtraPlayer > 1)
					{
						creepersPerExtraPlayer--;
						creepersDesired = targetCount * creepersPerExtraPlayer;
					}
				}
				int creepersSparedForTarget = 0;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.type != 267)
					{
						continue;
					}
					n.TryGetAIOverride<CreeperAI>(out var creeper);
					if (creepersDesired > 0 && creepersToSpare > 0)
					{
						creeper.CachedValue2 = extraTargets[0];
						creepersToSpare--;
						creepersDesired--;
						creeper.Time -= 30 * creepersSparedForTarget;
						if (++creepersSparedForTarget >= creepersPerExtraPlayer)
						{
							extraTargets.RemoveAt(0);
							creepersSparedForTarget = 0;
						}
						AttackCounter++;
					}
					else
					{
						creeper.CachedValue2 = -1;
					}
				}
			}
			else
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC n2 = enumerator2.Current;
					if (n2.type == 267)
					{
						n2.TryGetAIOverride<CreeperAI>(out var creeper2);
						creeper2.CachedValue2 = -1;
					}
				}
			}
		}
		if (Time < (float)OrbitSetupDuration)
		{
			AttackPosition = Target.Center;
		}
		else
		{
			float prox = Target.DistanceSQ(AttackPosition);
			if (prox > 65536f)
			{
				AttackPosition += Target.DirectionFrom(AttackPosition) * (((float)Math.Sqrt(prox) - 256f) / 16f);
			}
		}
		if (Time >= (float)(OrbitDuration + 30))
		{
			Time = -1f;
			AIState = BrainAIState.Phase1Idle;
			AttackList.Clear();
			base.NPC.damage = base.NPC.defDamage;
			ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				NPC creep = enumerator3.Current;
				if (creep.type == 267)
				{
					creep.AIOverride<CreeperAI>().Time = -1;
				}
			}
		}
		else
		{
			if (!(Time >= (float)OrbitAttackInterval) || !(Time < (float)OrbitDuration) || Time % (float)OrbitAttackInterval != 0f || Main.netMode == 1)
			{
				return;
			}
			List<NPC> mainOrbitMembers = Main.npc.Where((NPC nPC) => nPC.active && nPC.type == 267 && nPC.TryGetAIOverride<CreeperAI>(out var aiInstance) && aiInstance.CachedValue2 == -1).ToList();
			if (mainOrbitMembers.Count <= 0)
			{
				return;
			}
			int rand = Main.rand.Next(mainOrbitMembers.Count);
			for (int i2 = 0; i2 < OrbitAttackParticipantCount; i2++)
			{
				if (rand >= mainOrbitMembers.Count)
				{
					rand -= mainOrbitMembers.Count;
				}
				NPC creeper3 = mainOrbitMembers[rand];
				AttackList.Add((byte)creeper3.whoAmI);
				rand += (int)Math.Round((float)mainOrbitMembers.Count / (float)OrbitAttackParticipantCount);
			}
			base.NPC.netUpdate = true;
		}
	}

	private void CreeperSpiral()
	{
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f && Main.netMode != 1)
		{
			AttackSign = ((!Main.rand.NextBool()) ? 1 : (-1));
			AttackRotation = 0f;
			base.NPC.netUpdate = true;
		}
		float num = MathHelper.Lerp((float)StartingTimePerRevolutionMax, (float)StartingTimePerRevolutionMin, 1f - CreeperAmountRatio);
		float endTimePerRev = MathHelper.Lerp((float)EndingTimePerRevolutionMax, (float)EndingTimePerRevolutionMin, 1f - CreeperAmountRatio);
		float spinSpeedCompletion = MathHelper.Clamp((Time - (float)SpeedUpDelayTime) / (float)(SpiralDuration - SpeedUpDelayTime - SpeedUpExtensionTime), 0f, 1f);
		float timePerRev = MathHelper.Lerp(num, endTimePerRev, spinSpeedCompletion);
		if (Time > (float)(SpiralDuration - 30))
		{
			timePerRev *= MathHelper.Lerp(1f, 10f, CalamityUtils.CircOutEasing(MathHelper.Clamp((Time - (float)(SpiralDuration - 30)) / 30f, 0f, 1f), 1));
		}
		else if (Time < (float)SpiralSetupTime)
		{
			timePerRev *= MathHelper.Lerp(1f, 10f, CalamityUtils.CircInEasing(MathHelper.Clamp(Time / (float)SpiralSetupTime, 0f, 1f), 1));
		}
		float rotToAdd = (float)Math.PI * 2f / timePerRev * (float)AttackSign;
		if (OnSecondCreeperPhase)
		{
			float attackComplationRatio = Time / (float)SpiralDuration;
			float lerp = Utils.GetLerpValue(TurnAroundRatio - TurnAroundDurationRatio / 2f, TurnAroundRatio + TurnAroundDurationRatio / 2f, attackComplationRatio, clamped: true);
			rotToAdd *= MathHelper.Lerp(1f, -1f, lerp);
		}
		AttackRotation += rotToAdd;
		if (base.NPC.DistanceSQ(Target.Center) > 57600f)
		{
			base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * (base.NPC.Distance(Target.Center) - 240f) / 32f;
		}
		else
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
		}
		if (!(Time > (float)SpiralDuration))
		{
			return;
		}
		Time = -1f;
		AIState = BrainAIState.Phase1Idle;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC creep = enumerator.Current;
			if (creep.type == 267)
			{
				creep.AIOverride<CreeperAI>().Time = -1;
			}
		}
	}

	private void PhaseTransition()
	{
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.dontTakeDamage = true;
		base.NPC.rotation *= 0.9f;
		TeleportTime = 0f;
		base.NPC.damage = 0;
		float animCounter = Time - 60f;
		if (animCounter >= 0f)
		{
			if (animCounter == 0f)
			{
				base.NPC.velocity = Vector2.UnitY * 2f;
			}
			else if (animCounter < 60f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.99f;
			}
			else if (animCounter == 60f)
			{
				base.NPC.velocity = Vector2.UnitY * -8f;
			}
			else if (animCounter == 65f)
			{
				AIState = BrainAIState.Phase2TransitionOpen;
				PreviousAttack = BrainAIState.Phase1Idle;
				availableAttacks.Clear();
				base.NPC.netUpdate = true;
				SoundEngine.PlaySound(in SoundID.NPCHit1, base.NPC.Center);
				if (!Main.dedServ)
				{
					for (int i = 392; i <= 395; i++)
					{
						Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, Main.rand.NextVector2Circular(6f, 6f), i);
					}
				}
				for (int j = 0; j < 20; j++)
				{
					Vector2 edgeBloodDir = Main.rand.NextVector2CircularEdge(1f, 1f);
					GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center - Vector2.unitYVector * 32f + edgeBloodDir * new Vector2(16f, 24f), edgeBloodDir.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 10f, (float)Math.PI / 10f)) * Main.rand.NextFloat(4f, 8f), 24, 0.75f, Color.Red));
					Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(base.NPC.Hitbox), 5, Main.rand.NextVector2Circular(6f, 6f));
				}
				for (int k = 1; k <= 3; k++)
				{
					Color color = (Color)(k switch
					{
						1 => Color.Yellow, 
						2 => Color.Orange, 
						_ => Color.Red, 
					});
					GeneralParticleHandler.SpawnParticle(new PulseRing(base.NPC.Center, base.NPC.velocity * 0.5f, color, 0f, 1f + (float)k * 0.5f, 24));
				}
				BoCAfterImages = new List<Particle>();
				SoundEngine.PlaySound(in Roar, base.NPC.Center);
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.9f;
			}
			if (animCounter < 60f)
			{
				BoCDrawOffset = Main.rand.NextVector2CircularEdge(1f, 1f) * MathHelper.Lerp(0f, 16f, CalamityUtils.CircInEasing(animCounter / 60f, 1));
			}
			else if (animCounter < 70f)
			{
				BoCDrawOffset = Main.rand.NextVector2CircularEdge(1f, 1f) * MathHelper.Lerp(16f, 0f, CalamityUtils.CircOutEasing((animCounter - 60f) / 10f, 1));
			}
			if (animCounter >= 120f)
			{
				Time = 0f;
				ResetAttackValues();
				AIState = BrainAIState.Phase2Idle;
				base.NPC.dontTakeDamage = false;
				base.NPC.damage = base.NPC.defDamage;
			}
			return;
		}
		NPC nPC3 = base.NPC;
		nPC3.velocity *= 0.8f;
		if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
		{
			base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * 4f;
		}
		else if (Collision.SolidCollision(base.NPC.position + base.NPC.velocity, base.NPC.width, base.NPC.height))
		{
			if (base.NPC.velocity.X != base.NPC.oldVelocity.X)
			{
				base.NPC.velocity.X = 0f - base.NPC.oldVelocity.X;
			}
			if (base.NPC.velocity.Y != base.NPC.oldVelocity.Y)
			{
				base.NPC.velocity.Y = 0f - base.NPC.oldVelocity.Y;
			}
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 2f;
			base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 8f);
		}
	}

	private void Phase2Idle()
	{
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.rotation = base.NPC.velocity.X / 6f * (float)Math.PI / 8f;
		if (Time == (float)(ChaseTime - 5))
		{
			AttackCounter++;
		}
		if (Time <= (float)ChaseTime)
		{
			base.NPC.damage = base.NPC.defDamage;
			float speedUp = MathHelper.Clamp((Time - 10f) / 10f, 0f, 1f);
			float slowDown = 1f - MathHelper.Clamp((Time - (float)(ChaseTime - 15)) / 15f, 0f, 1f);
			float angleChange = MathHelper.Lerp((float)Math.PI / 24f, 0f, MathHelper.Clamp(Time / ((float)ChaseTime * 0.666f), 0f, 1f));
			base.NPC.velocity = base.NPC.velocity.RotateDirectionTowards(base.NPC.DirectionTo(Target.Center).ToRotation(), angleChange) * (MathHelper.Lerp(ChaseMinSpeed, ChaseMaxSpeed, Time / (float)ChaseTime) * speedUp * slowDown);
			if (Time == (float)ChaseTime)
			{
				if (Main.netMode != 0)
				{
					SelectNewTarget();
				}
				Vector2 direction = Target.velocity.SafeNormalize(Vector2.UnitX * (float)Target.direction).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 3f, (float)Math.PI / 3f));
				AttackPosition = Target.Center + direction * DefaultTeleportDistance;
				BoCAfterImages = new List<Particle>();
				base.NPC.damage = 0;
				base.NPC.netUpdate = true;
			}
			else
			{
				TeleportTime = 0f;
				base.NPC.Opacity = 1f;
			}
			return;
		}
		TeleportDuration = IdleTeleportDuration;
		base.NPC.damage = 0;
		Vector2 endPoint = AttackPosition;
		base.NPC.velocity = Vector2.Zero;
		if (Time < (float)ChaseTime + TeleportDuration / 2f)
		{
			if (Time % 4f == 0f)
			{
				Vector2 startPoint = base.NPC.Center;
				Vector2 direction2 = endPoint - startPoint;
				float curveIntensity = Main.rand.NextFloat(-0.2f, 0.2f);
				Vector2 perpindicular = direction2.RotatedBy(1.5707963705062866);
				Vector2 controlPoint1 = startPoint + direction2 * 0.25f + perpindicular * curveIntensity;
				Vector2 controlPoint2 = startPoint + direction2 * 0.75f + perpindicular * curveIntensity;
				BrainOfCthulhuAfterImage afterimage = new BrainOfCthulhuAfterImage(new BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint), base.NPC.rotation, Vector2.One, (int)((float)ChaseTime + TeleportDuration * 0.75f - Time), BoCFrame);
				BoCAfterImages.Add(afterimage);
				GeneralParticleHandler.SpawnParticle(afterimage);
			}
			TeleportTime++;
		}
		else if (Time == (float)ChaseTime + TeleportDuration / 2f && !AttackFlag)
		{
			base.NPC.Center = endPoint;
			base.NPC.damage = base.NPC.defDamage;
			base.DisableMultiplayerSmoothing = true;
			AttackFlag = true;
			base.NPC.netUpdate = true;
		}
		else
		{
			TeleportTime--;
			if (TeleportTime < 0f)
			{
				TeleportTime = 0f;
				Time = -1f;
				AttackFlag = false;
				if (AttackCounter >= ChaseAmount)
				{
					base.NPC.rotation = 0f;
					ResetAttackValues();
					if (availableAttacks.Count == 0)
					{
						bool quickChoice = Main.rand.NextBool();
						int num = 5;
						List<BrainAIState> list = new List<BrainAIState>(num);
						CollectionsMarshal.SetCount(list, num);
						Span<BrainAIState> span = CollectionsMarshal.AsSpan(list);
						int num2 = 0;
						span[num2] = BrainAIState.Bloodletting;
						num2++;
						span[num2] = (quickChoice ? BrainAIState.SanguineScythes : BrainAIState.IllusionDash);
						num2++;
						span[num2] = (Main.rand.NextBool() ? BrainAIState.Phase2Idle : BrainAIState.Bloodletting);
						num2++;
						span[num2] = (quickChoice ? BrainAIState.IllusionDash : BrainAIState.SanguineScythes);
						num2++;
						span[num2] = BrainAIState.IllusionTrick;
						availableAttacks = list;
					}
					AIState = availableAttacks[0];
					availableAttacks.RemoveAt(0);
					PreviousAttack = AIState;
					if (AIState == BrainAIState.SanguineScythes)
					{
						Time = -31f;
						BoCAfterImages = new List<Particle>();
					}
					base.NPC.netUpdate = true;
				}
			}
		}
		base.NPC.Opacity = 1f - TeleportTime / (TeleportDuration / 2f);
	}

	private void Bloodletting()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0973: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		float endTime = Time - (float)BloodlettingDuration;
		if (endTime < 0f)
		{
			if (Time == 0f)
			{
				AttackPosition = base.NPC.Center;
				if (base.NPC.Center.X < Target.Center.X)
				{
					AttackSign = -1;
				}
				else
				{
					AttackSign = 1;
				}
				CachedRatio = float.MaxValue;
				AttackTime = float.MaxValue;
				base.NPC.netUpdate = true;
			}
			if (Time == 30f)
			{
				AttackPosition = Vector2.zeroVector;
			}
			if (Main.netMode != 0 && Time % 2f == 0f)
			{
				Player furthestLeft = null;
				Player furthestRight = null;
				Player highestUp = null;
				foreach (int who in GetAllValidTargets(base.NPC.Center))
				{
					Player p = Main.player[who];
					if (furthestLeft == null || p.Center.X < furthestLeft.Center.X)
					{
						furthestLeft = p;
					}
					if (furthestRight == null || p.Center.X > furthestRight.Center.X)
					{
						furthestRight = p;
					}
					if (highestUp == null || p.Center.Y < highestUp.Center.Y)
					{
						highestUp = p;
					}
				}
				AttackList.Clear();
				AttackList.Add((byte)furthestLeft.whoAmI);
				AttackList.Add((byte)furthestRight.whoAmI);
				AttackList.Add((byte)highestUp.whoAmI);
			}
			float waveValue = Time * (float)Math.PI / (float)BloodshotRate;
			Vector2 goalPos;
			if (Main.netMode == 0)
			{
				goalPos = Target.Center + new Vector2((float)Math.Cos(waveValue) * HoverDistance.X * (float)AttackSign, (float)(-0.5 * Math.Cos(2f * waveValue) + 0.5) * (0f - HoverDistance.Y));
			}
			else
			{
				Player furthestLeft2 = Main.player[AttackList[0]];
				Player furthestRight2 = Main.player[AttackList[1]];
				Player highestUp2 = Main.player[AttackList[2]];
				Vector2 hoverCenter = (furthestLeft2.Center + furthestRight2.Center) / 2f;
				float xDist = furthestRight2.Center.X - furthestLeft2.Center.X;
				float yDist = hoverCenter.Y - highestUp2.Center.Y;
				if (AttackPosition == Vector2.zeroVector)
				{
					AttackPosition = hoverCenter;
				}
				else
				{
					AttackPosition += (hoverCenter - AttackPosition) / 30f;
				}
				if (CachedRatio == float.MaxValue || Math.Abs(xDist - CachedRatio) < 0.01f)
				{
					CachedRatio = xDist;
				}
				else
				{
					CachedRatio += (xDist - CachedRatio) / 30f;
				}
				if (AttackTime == float.MaxValue || Math.Abs(yDist - AttackTime) < 0.01f)
				{
					AttackTime = yDist;
				}
				AttackTime += (yDist - AttackTime) / 30f;
				float xMag = HoverDistance.X + CachedRatio;
				float yMag = HoverDistance.Y + AttackTime;
				goalPos = AttackPosition + new Vector2((float)Math.Cos(waveValue) * xMag * (float)AttackSign, (float)(-0.5 * Math.Cos(2f * waveValue) + 0.5) * (0f - yMag));
			}
			base.NPC.velocity = Vector2.Zero;
			base.DisableMultiplayerSmoothing = true;
			if (Time < 30f)
			{
				base.NPC.Center = Vector2.Lerp(AttackPosition, goalPos, CalamityUtils.SineOutEasing(Time / 30f, 1));
			}
			else
			{
				base.NPC.Center = goalPos;
			}
		}
		else if (endTime < (float)DashPrepTime)
		{
			if (endTime == 0f)
			{
				base.NPC.velocity = Vector2.UnitX * (float)AttackSign * 10f;
			}
			else
			{
				Vector2 goalPos2 = Target.Center - Vector2.UnitY * HoverEndHeight;
				Vector2 accel = default(Vector2);
				((Vector2)(ref accel))._002Ector(0.5f, 1.5f);
				NPC nPC = base.NPC;
				nPC.velocity += base.NPC.DirectionTo(goalPos2).SafeNormalize(Vector2.Zero) * accel;
				base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 8f);
			}
		}
		if (endTime < 0f)
		{
			base.NPC.rotation = (float)Math.Sin(Time / 8f) * (float)Math.PI / 8f;
			BoCDrawOffset = Vector2.Zero;
			base.NPC.damage = 0;
			if (!(Time > (float)BloodshotRate))
			{
				return;
			}
			if (Time % (float)IchorRate == 0f)
			{
				if (Time > (float)(BloodshotRate + 30) && Time % (float)(IchorRate * 10) == 0f)
				{
					SoundEngine.PlaySound(in BloodBomb, base.NPC.Center);
					if (Main.netMode != 1)
					{
						NPC.NewNPC(base.NPC.GetSource_FromThis(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<BloodBomb>(), base.NPC.whoAmI);
					}
				}
				else
				{
					if (Time % (float)(IchorRate * 2) == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item17, base.NPC.Center);
					}
					if (Main.netMode == 0)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center + base.NPC.velocity + Main.rand.NextVector2Circular(72f, 72f), new Vector2(Main.rand.NextFloat(0f - IchorSpread, IchorSpread), 0f - IchorVelocity), ModContent.ProjectileType<IchorShower>(), IchorShotDamage, 0.5f);
					}
					else if (Main.dedServ)
					{
						Player furthestLeft3 = Main.player[AttackList[0]];
						float xDist2 = Main.player[AttackList[1]].Center.X - furthestLeft3.Center.X;
						int projCount = 1 + (int)(xDist2 / 360f);
						for (int i = 0; i < projCount; i++)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center + base.NPC.velocity + Main.rand.NextVector2Circular(72f, 72f), new Vector2(Main.rand.NextFloat(0f - IchorSpread, IchorSpread), 0f - IchorVelocity), ModContent.ProjectileType<IchorShower>(), IchorShotDamage, 0.5f);
						}
					}
				}
			}
			if (Time % (float)BloodshotRate != 0f)
			{
				return;
			}
			if (Main.netMode != 1)
			{
				Vector2 dir = base.NPC.DirectionTo(Target.Center);
				for (int j = -2; j <= 2; j++)
				{
					Vector2 initialDir = dir.RotatedBy((float)j * (float)Math.PI / 4f);
					Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, initialDir * BloodshotVelocity, 814, BloodShotDamage, 0.5f, -1, dir.ToRotation() + (float)Math.PI * 2f, initialDir.ToRotation());
				}
				if (CalamityWorld.death)
				{
					Vector2 initialDir2 = dir.RotatedBy(0.5235987901687622);
					Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, initialDir2 * BloodshotVelocity / 2.1f, 814, BloodShotDamage, 0.5f, -1, dir.ToRotation() + (float)Math.PI * 2f, initialDir2.ToRotation());
					initialDir2 = dir.RotatedBy(-0.5235987901687622);
					Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, initialDir2 * BloodshotVelocity / 2.1f, 814, BloodShotDamage, 0.5f, -1, dir.ToRotation() + (float)Math.PI * 2f, initialDir2.ToRotation());
				}
			}
			SoundEngine.PlaySound(BloodShot with
			{
				PitchVariance = 0.5f
			}, base.NPC.Center);
			return;
		}
		base.NPC.rotation *= 0.9f;
		base.NPC.damage = base.NPC.defDamage;
		if (endTime == 0f)
		{
			SoundEngine.PlaySound(in Roar, base.NPC.Center);
		}
		if (endTime >= (float)DashPrepTime)
		{
			if (endTime < (float)(DashPrepTime + DashReelbackTime))
			{
				float reelBackSpeedExponent = 2.6f;
				float reelBackCompletion = Utils.GetLerpValue(0f, 30f, endTime - (float)DashPrepTime, clamped: true);
				float reelBackSpeed = MathHelper.Lerp(4f, 16f, MathF.Pow(reelBackCompletion, reelBackSpeedExponent));
				Vector2 reelBackVelocity = Vector2.UnitY * (0f - reelBackSpeed);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity, 0.25f);
			}
			else if (endTime == (float)(DashPrepTime + 20))
			{
				base.NPC.velocity = Vector2.UnitY * DashVelocity;
			}
			if (endTime >= (float)(DashPrepTime + DashReelbackTime) && Time % (float)DashScytheRate == 0f && Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.UnitX * 16f, ModContent.ProjectileType<BloodScythe>(), BloodScytheDamage, 0.5f);
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.UnitX * -16f, ModContent.ProjectileType<BloodScythe>(), BloodScytheDamage, 0.5f);
			}
			if (endTime > (float)(DashPrepTime + DashReelbackTime + DashDuration))
			{
				base.NPC.rotation = 0f;
				SetupForNextAttack();
			}
		}
	}

	private void SanguineScythes()
	{
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		if (AttackCounter > SanguineTeleportCount)
		{
			if (Time == (float)SanguineAttackEndDelay)
			{
				SoundEngine.PlaySound(in Roar, base.NPC.Center);
				bool left = Target.Center.X > base.NPC.Center.X;
				base.NPC.velocity = Vector2.UnitX * (float)(left ? 18 : (-18));
				base.NPC.rotation = (float)Math.PI / 8f * (float)(left ? 1 : (-1));
			}
			if (Time > (float)(SanguineAttackEndDelay + SanguineAttackEndDuration))
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.8f;
				base.NPC.rotation *= 0.8f;
			}
			else if (Time >= (float)SanguineAttackEndDelay && Time % (float)SanguineAttackEndIchorRate == 0f && Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, new Vector2((float)Math.Sign(base.NPC.velocity.X), -2f), ModContent.ProjectileType<IchorShower>(), IchorShotDamage, 0.5f);
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, new Vector2((float)Math.Sign(base.NPC.velocity.X), -6f), ModContent.ProjectileType<IchorShower>(), IchorShotDamage, 0.5f);
			}
			if (Time > (float)(SanguineAttackEndDelay + SanguineAttackEndDuration + (CalamityWorld.death ? 20 : 40)))
			{
				SetupForNextAttack();
			}
			return;
		}
		if (Time < 0f)
		{
			if (Time == -25f)
			{
				base.NPC.netUpdate = true;
			}
			if (Time == -24f)
			{
				SoundEngine.PlaySound(in Roar, base.NPC.Center);
				for (int i = 1; i <= 3; i++)
				{
					Color color = (Color)(i switch
					{
						1 => Color.Yellow, 
						2 => Color.Orange, 
						_ => Color.Red, 
					});
					GeneralParticleHandler.SpawnParticle(new PulseRing(base.NPC.Center, base.NPC.velocity * 0.5f, color, 0f, 1f + (float)i * 0.5f, 24));
				}
			}
			if (Time == -1f)
			{
				Vector2 direction = Target.velocity.SafeNormalize(Vector2.UnitX * (float)Target.direction).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 3f, (float)Math.PI / 3f));
				float distance = SanguineTeleportDistance;
				AttackPosition = Target.Center + direction * distance;
				base.NPC.netUpdate = true;
			}
			return;
		}
		TeleportDuration = SanguineTeleportDuration;
		Vector2 endPoint = AttackPosition;
		if (Time < TeleportDuration / 2f)
		{
			if (Time % 4f == 0f)
			{
				Vector2 startPoint = base.NPC.Center;
				Vector2 direction2 = endPoint - startPoint;
				float curveIntensity = Main.rand.NextFloat(-0.2f, 0.2f);
				Vector2 perpindicular = direction2.RotatedBy(1.5707963705062866);
				Vector2 controlPoint1 = startPoint + direction2 * 0.25f + perpindicular * curveIntensity;
				Vector2 controlPoint2 = startPoint + direction2 * 0.75f + perpindicular * curveIntensity;
				BrainOfCthulhuAfterImage afterimage = new BrainOfCthulhuAfterImage(new BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint), base.NPC.rotation, Vector2.One, (int)(TeleportDuration * 0.75f - Time), BoCFrame);
				BoCAfterImages.Add(afterimage);
				GeneralParticleHandler.SpawnParticle(afterimage);
			}
			TeleportTime++;
		}
		else if (Time == TeleportDuration / 2f && !AttackFlag)
		{
			_ = base.NPC.Center;
			base.NPC.Center = endPoint;
			base.DisableMultiplayerSmoothing = true;
			AttackFlag = true;
			base.NPC.netUpdate = true;
		}
		else
		{
			TeleportTime--;
			if (TeleportTime < 0f)
			{
				TeleportTime = 0f;
				Time = -1f;
				AttackFlag = false;
				AttackCounter++;
				if (AttackCounter <= SanguineTeleportCount)
				{
					SoundEngine.PlaySound(in BloodExplosion, base.NPC.Center);
					for (int j = 0; j < SanguineScytheCount; j++)
					{
						float initalSpeed = 16f;
						if (CalamityWorld.death && j % 2 == 0)
						{
							initalSpeed /= 2f;
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)SanguineScytheCount * (float)j) * initalSpeed, ModContent.ProjectileType<BloodScythe>(), BloodScytheDamage, 0.5f);
						}
					}
				}
				if (AttackCounter < SanguineTeleportCount)
				{
					Vector2 direction3 = Main.rand.NextFloat(0f, (float)Math.PI * 2f).ToRotationVector2();
					AttackPosition = Target.Center + direction3 * SanguineTeleportDistance;
				}
				else
				{
					Vector2 direction3 = Vector2.UnitX * (float)((!Main.rand.NextBool()) ? 1 : (-1));
					AttackPosition = Target.Center + direction3 * SanguineFinalTeleportOffset.X + Vector2.UnitY * (0f - SanguineFinalTeleportOffset.Y);
				}
				base.NPC.netUpdate = true;
				BoCAfterImages = new List<Particle>();
				base.NPC.Opacity = 1f;
			}
		}
		base.NPC.Opacity = 1f - TeleportTime / (TeleportDuration / 2f);
	}

	private void CrimsonEyes()
	{
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		if (Time <= 30f)
		{
			if (!(Time >= 6f) || !(Time <= 12f))
			{
				return;
			}
			if (Time == 6f)
			{
				SoundEngine.PlaySound(in Roar, base.NPC.Center);
				for (int i = 1; i <= 3; i++)
				{
					Color color = (Color)(i switch
					{
						1 => Color.Yellow, 
						2 => Color.Orange, 
						_ => Color.Red, 
					});
					GeneralParticleHandler.SpawnParticle(new PulseRing(base.NPC.Center, base.NPC.velocity * 0.5f, color, 0f, 1f + (float)i * 0.5f, 24));
				}
				CalamityUtils.AddScreenshakeAt(base.NPC.Center, 10f);
			}
			for (int j = 0; j < 12; j++)
			{
				Point start = Target.Center.ToTileCoordinates() + new Point(Main.rand.Next(-64, 65), 48);
				for (int k = 0; k < 96; k++)
				{
					Point current = start - new Point(0, k);
					if (!Main.tile[current].IsTileSolid() && Main.tile[current - new Point(0, 1)].IsTileSolid())
					{
						Dust.NewDust(current.ToWorldCoordinates(0f, 0f), 16, 16, 117, 0f, 3f);
					}
				}
			}
			return;
		}
		if (Time < (float)CrimsonEyeAttackDuration && CalamityUtils.CountProjectiles(ModContent.ProjectileType<CrimsonEye>()) < CrimsonEyeCap && Time % (float)CrimsonEyeRate == 0f)
		{
			Vector2 spawnPos = Target.Center;
			int i2 = 0;
			Rectangle hitbox = default(Rectangle);
			for (i2 = 0; i2 <= 32; i2++)
			{
				spawnPos = Target.Center + Main.rand.NextVector2Circular(256f, 256f);
				if (Collision.IsWorldPointSolid(spawnPos))
				{
					continue;
				}
				bool alreadyFilled = false;
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.type == ModContent.ProjectileType<CrimsonEye>())
					{
						((Rectangle)(ref hitbox))._002Ector((int)spawnPos.X - 50, (int)spawnPos.Y - 18, 100, 36);
						Rectangle hitbox2 = p.Hitbox;
						if (((Rectangle)(ref hitbox2)).Intersects(hitbox))
						{
							alreadyFilled = true;
							break;
						}
					}
				}
				if (!alreadyFilled && Collision.CanHitLine(spawnPos, 1, 1, Target.position, Target.width, Target.height))
				{
					break;
				}
			}
			if (i2 == 32)
			{
				spawnPos = Target.Center;
			}
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<CrimsonEye>(), CrimsonEyeDamage, 0f);
			}
		}
		if (Time < 180f)
		{
			float dist = 210f;
			Vector2 fromTarget = (base.NPC.Center - Target.Center).SafeNormalize(Vector2.UnitX);
			Vector2 goalPos = Target.Center + fromTarget * dist - Vector2.UnitY * 32f;
			if (base.NPC.DistanceSQ(goalPos) <= 2048f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
			else if (((Vector2)(ref base.NPC.velocity)).LengthSquared() < 16f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity += base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.Zero) * 0.1f;
			}
			else
			{
				base.NPC.velocity = base.NPC.DirectionTo(goalPos).SafeNormalize(Vector2.Zero) * 4f;
			}
			return;
		}
		if (Time < (float)CrimsonEyeAttackIdleDuration)
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.9f;
		}
		if (Time >= (float)(CrimsonEyeAttackIdleDuration + CrimsonEyeAttackSetUpDuration) && Time < (float)CrimsonEyeAttackDuration)
		{
			if (Time == (float)(CrimsonEyeAttackIdleDuration + CrimsonEyeAttackSetUpDuration))
			{
				base.NPC.velocity = base.NPC.DirectionTo(Target.Center);
			}
			float speed = (((double)(Target.Center.Y / 16f) < Main.worldSurface) ? 10f : 8f);
			if (Time < (float)(CrimsonEyeAttackIdleDuration + CrimsonEyeAttackSetUpDuration + CrimsonEyeAttackBuildUpDuration))
			{
				float lerp = CalamityUtils.SineInEasing((Time - (float)(CrimsonEyeAttackIdleDuration + CrimsonEyeAttackSetUpDuration)) / (float)CrimsonEyeAttackBuildUpDuration, 1);
				speed = MathHelper.Lerp(0f, speed, lerp);
			}
			float turnAmt = TurnAccelerationMultiplier * ((base.NPC.Distance(Target.Center) - TurnAccelerationDistanceBuffer) / TurnAccelerationDistanceDivisor);
			base.NPC.velocity = base.NPC.velocity.RotateDirectionTowards(base.NPC.AngleTo(Target.Center), turnAmt) * speed;
		}
		else
		{
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 0.9f;
			if (Time == (float)CrimsonEyeAttackDuration)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Projectile p2 = enumerator2.Current;
					if (p2.type == ModContent.ProjectileType<CrimsonEye>())
					{
						p2.timeLeft = 60;
					}
				}
			}
			if (Time > (float)(CrimsonEyeAttackDuration + CrimsonEyeAttackEndDuration))
			{
				SetupForNextAttack();
			}
		}
		if (Time != (float)CrimsonEyeAttackIdleDuration)
		{
			return;
		}
		SoundEngine.PlaySound(in BloodExplosion, base.NPC.Center);
		if (Main.netMode != 1)
		{
			float projCount = 10f;
			for (int l = 0; (float)l < projCount; l++)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<CirclingBloodScythe>(), BloodScytheDamage, 0.5f, -1, (float)Math.PI * 2f / projCount * (float)l);
			}
		}
	}

	private void IllusionDash()
	{
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		if (Time < (float)IllusionDashTeleportDuration)
		{
			if (Time == 0f)
			{
				AttackPosition = Target.Center;
				AttackRotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
				base.NPC.netUpdate = true;
			}
			TeleportDuration = IllusionDashTeleportDuration;
			if (Time < TeleportDuration / 2f)
			{
				if (Time % 4f == 0f)
				{
					Vector2 startPoint = base.NPC.Center;
					for (int i = 0; i < (CalamityWorld.death ? 8 : 4); i++)
					{
						Vector2 myEndPoint = AttackPosition + (AttackRotation + (CalamityWorld.death ? ((float)Math.PI / 4f) : ((float)Math.PI / 2f)) * (float)i).ToRotationVector2() * IllusionDashTeleportDistance;
						Vector2 direction = myEndPoint - startPoint;
						float curveIntensity = Main.rand.NextFloat(-0.2f, 0.2f);
						Vector2 perpindicular = direction.RotatedBy(1.5707963705062866);
						Vector2 controlPoint1 = startPoint + direction * 0.25f + perpindicular * curveIntensity;
						Vector2 controlPoint2 = startPoint + direction * 0.75f + perpindicular * curveIntensity;
						BrainOfCthulhuAfterImage afterimage = new BrainOfCthulhuAfterImage(new BezierCurve(startPoint, controlPoint1, controlPoint2, myEndPoint), base.NPC.rotation, Vector2.One, (int)(TeleportDuration * 0.75f - Time), BoCFrame);
						BoCAfterImages.Add(afterimage);
						GeneralParticleHandler.SpawnParticle(afterimage);
					}
				}
				TeleportTime++;
			}
			else if (Time == TeleportDuration / 2f && !AttackFlag)
			{
				AttackFlag = true;
				for (int j = 0; j < (CalamityWorld.death ? 8 : 4); j++)
				{
					float rot = AttackRotation + (CalamityWorld.death ? ((float)Math.PI / 4f) : ((float)Math.PI / 2f)) * (float)j;
					Vector2 spawnPos = AttackPosition + rot.ToRotationVector2() * IllusionDashTeleportDistance;
					if (j == 0)
					{
						base.NPC.Center = spawnPos;
						base.DisableMultiplayerSmoothing = true;
					}
					else if (Main.netMode != 1)
					{
						NPC.NewNPCDirect(base.NPC.GetSource_FromThis(), spawnPos, ModContent.NPCType<BrainIllusion>(), 0, 15f, 30f, rot).target = base.NPC.target;
					}
				}
				if (Main.netMode != 0)
				{
					List<int> allValidTargets = GetAllValidTargets(base.NPC.Center);
					allValidTargets.Remove(Target.whoAmI);
					foreach (int who in allValidTargets)
					{
						Player p = Main.player[who];
						float baseRot = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
						for (int k = 0; k < (CalamityWorld.death ? 4 : 2); k++)
						{
							float rot2 = baseRot + (CalamityWorld.death ? ((float)Math.PI / 2f) : ((float)Math.PI)) * (float)k;
							Vector2 spawnPos2 = p.Center + rot2.ToRotationVector2() * IllusionDashTeleportDistance;
							if (Main.netMode != 1)
							{
								NPC.NewNPCDirect(base.NPC.GetSource_FromThis(), spawnPos2, ModContent.NPCType<BrainIllusion>(), 0, 15f, 30f, rot2).target = p.whoAmI;
							}
						}
					}
				}
				base.NPC.netUpdate = true;
			}
			else
			{
				TeleportTime--;
			}
			base.NPC.Opacity = 1f - TeleportTime / (TeleportDuration / 2f);
			return;
		}
		float startTime = Time - (float)IllusionDashTeleportDuration;
		if (startTime == 0f)
		{
			AttackPosition = base.NPC.Center;
			TeleportTime = 0f;
			AttackFlag = false;
			BoCAfterImages = new List<Particle>();
			base.NPC.Opacity = 1f;
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.NPC.Center + new Vector2(16f, -8f), Vector2.Zero, Color.Yellow, Color.Orange, 2f, 16, 1f, 1f, needed: true));
			base.NPC.netUpdate = true;
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC n = enumerator2.Current;
				if (n.type == ModContent.NPCType<BrainIllusion>())
				{
					n.netUpdate = true;
				}
			}
		}
		if (startTime < 30f)
		{
			float lerp = startTime / 30f;
			float circleDist = MathHelper.Lerp(IllusionDashTeleportDistance, IllusionDashCloseInDistance, CalamityUtils.SineOutEasing(lerp, 1));
			base.NPC.Center = Vector2.Lerp(AttackPosition, Target.Center + AttackRotation.ToRotationVector2() * circleDist, lerp);
			AttackRotation += MathHelper.Lerp(0f, IllusionDashStartingSpinSpeed, CalamityUtils.SineInEasing(lerp, 1));
			base.DisableMultiplayerSmoothing = true;
		}
		else if (startTime <= (float)(30 + IllusionDashSpinDuration))
		{
			base.NPC.Center = Target.Center + AttackRotation.ToRotationVector2() * IllusionDashCloseInDistance;
			base.DisableMultiplayerSmoothing = true;
			AttackRotation += MathHelper.Lerp(IllusionDashStartingSpinSpeed, 0f, CalamityUtils.SineOutEasing((startTime - 30f) / (float)IllusionDashSpinDuration, 1));
		}
		else if (startTime < (float)(30 + IllusionDashSpinDuration + 30))
		{
			if (startTime < (float)(30 + IllusionDashSpinDuration + 15))
			{
				float reelBackSpeedExponent = 2.6f;
				float reelBackCompletion = Utils.GetLerpValue(0f, 30f, startTime - 130f, clamped: true);
				float reelBackSpeed = MathHelper.Lerp(2.5f, 16f, MathF.Pow(reelBackCompletion, reelBackSpeedExponent));
				Vector2 reelBackVelocity = (AttackRotation + (float)Math.PI).ToRotationVector2() * (0f - reelBackSpeed);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity, 0.25f);
			}
			else if (startTime == (float)(30 + IllusionDashSpinDuration + 15))
			{
				base.NPC.velocity = (AttackRotation + (float)Math.PI).ToRotationVector2() * -32f;
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
		}
		else if (startTime <= (float)(30 + IllusionDashSpinDuration + 30 + IllusionDashFakeoutTeleportDuration))
		{
			base.NPC.velocity = Vector2.Zero;
			if (startTime == (float)(30 + IllusionDashSpinDuration + 30))
			{
				AttackPosition = Target.Center;
				AttackRotation = AttackRotation + (float)Math.PI + Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f);
				base.NPC.netUpdate = true;
			}
			float wrappedCounter = startTime - (float)(30 + IllusionDashSpinDuration + 30);
			TeleportDuration = IllusionDashFakeoutTeleportDuration;
			Vector2 endPoint = AttackPosition + AttackRotation.ToRotationVector2() * 270f;
			if (wrappedCounter < TeleportDuration / 2f)
			{
				if (wrappedCounter % 2f == 0f)
				{
					Vector2 startPoint2 = base.NPC.Center;
					Vector2 direction2 = endPoint - startPoint2;
					float curveIntensity2 = Main.rand.NextFloat(-0.2f, 0.2f);
					Vector2 perpindicular2 = direction2.RotatedBy(1.5707963705062866);
					Vector2 controlPoint3 = startPoint2 + direction2 * 0.25f + perpindicular2 * curveIntensity2;
					Vector2 controlPoint4 = startPoint2 + direction2 * 0.75f + perpindicular2 * curveIntensity2;
					BrainOfCthulhuAfterImage afterimage2 = new BrainOfCthulhuAfterImage(new BezierCurve(startPoint2, controlPoint3, controlPoint4, endPoint), base.NPC.rotation, Vector2.One, (int)(TeleportDuration * 0.75f - wrappedCounter), BoCFrame);
					BoCAfterImages.Add(afterimage2);
					GeneralParticleHandler.SpawnParticle(afterimage2);
				}
				TeleportTime++;
			}
			else if (wrappedCounter == (float)(int)(TeleportDuration / 2f) && !AttackFlag)
			{
				AttackFlag = true;
				base.NPC.Center = endPoint;
				base.DisableMultiplayerSmoothing = true;
			}
			else
			{
				TeleportTime--;
				if (TeleportTime <= 0f)
				{
					TeleportTime = 0f;
					BoCAfterImages = new List<Particle>();
					ResetAttackValues();
					base.NPC.Opacity = 1f;
					base.NPC.netUpdate = true;
					AttackRotation = base.NPC.AngleTo(Target.Center);
				}
			}
			base.NPC.Opacity = 1f - TeleportTime / (TeleportDuration / 2f);
		}
		else if (startTime <= (float)(150 + IllusionDashSpinDuration + 30 + IllusionDashFakeoutTeleportDuration + 30))
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.9f;
			if (startTime % 15f != 5f || !(startTime > (float)(60 + IllusionDashSpinDuration + 30 + IllusionDashFakeoutTeleportDuration)))
			{
				return;
			}
			Vector2 dir = base.NPC.DirectionTo(Target.Center);
			for (int l = 0; l < ((!CalamityWorld.death) ? 1 : 2); l++)
			{
				if (Main.netMode != 1)
				{
					Vector2 initialDir = dir.RotatedBy((float)Math.PI + Main.rand.NextFloat(-0.01f, 0.01f));
					Vector2 spawnPos3 = base.NPC.Center + (dir.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-base.NPC.width, base.NPC.width) - dir * 48f);
					Projectile.NewProjectile(base.NPC.GetSource_FromThis(), spawnPos3, initialDir * Main.rand.NextFloat(6f, 8f), 814, BloodShotDamage, 0.5f, -1, dir.ToRotation() + (float)Math.PI * 2f, initialDir.ToRotation());
				}
				NPC nPC3 = base.NPC;
				nPC3.velocity -= dir;
			}
		}
		else
		{
			if (!(startTime >= (float)(180 + IllusionDashSpinDuration + 30 + IllusionDashFakeoutTeleportDuration + 30)))
			{
				return;
			}
			ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				NPC n2 = enumerator3.Current;
				if (n2.type == ModContent.NPCType<BrainIllusion>())
				{
					n2.active = false;
				}
			}
			SetupForNextAttack();
		}
	}

	private void IllusionTrick()
	{
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_093d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		if (Time >= 90f)
		{
			if (Time == 90f)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.friendly)
					{
						p.Calamity().IgnoreBoCIllusions = true;
					}
				}
				int brainAngleSlot = Main.rand.Next(0, IllusionTrickAngleGroups);
				int brainDistSlot = Main.rand.Next(0, IllusionTrickGroupSize);
				if (Main.netMode != 1)
				{
					for (int a = 0; a < IllusionTrickAngleGroups; a++)
					{
						for (int d = 0; d < IllusionTrickGroupSize; d++)
						{
							if (a != brainAngleSlot || d != brainDistSlot)
							{
								NPC.NewNPC(base.NPC.GetSource_FromThis(), 0, 0, ModContent.NPCType<FalseBrain>(), 0, (float)Math.PI * 2f / (float)IllusionTrickAngleGroups * (float)a, FalseBrain.TimeDivisor / (float)IllusionTrickGroupSize * (float)d);
							}
						}
					}
				}
				AttackTime = (int)(FalseBrain.TimeDivisor / (float)IllusionTrickGroupSize * (float)brainDistSlot);
				AttackRotation = (float)Math.PI * 2f / (float)IllusionTrickAngleGroups * (float)brainAngleSlot;
				AttackFlag = false;
				if (Main.netMode == 0)
				{
					AttackPosition = Target.Center;
				}
				else
				{
					List<int> nearbyPlayers = GetAllValidTargets(base.NPC.Center);
					Vector2 averagePosition = Vector2.zeroVector;
					foreach (int p2 in nearbyPlayers)
					{
						TargetsSet.Add(p2);
						averagePosition += Main.player[p2].Center;
					}
					averagePosition /= (float)nearbyPlayers.Count;
					AttackPosition = averagePosition;
				}
				base.NPC.ShowNameOnHover = false;
				base.NPC.netUpdate = true;
			}
			else
			{
				Vector2 goalPos;
				if (Main.netMode == 0)
				{
					goalPos = Target.Center;
				}
				else
				{
					Vector2 averagePosition2 = Vector2.zeroVector;
					foreach (int who in TargetsSet)
					{
						averagePosition2 += Main.player[who].Center;
					}
					averagePosition2 /= (float)TargetsSet.Count;
					goalPos = averagePosition2;
				}
				float distSq = AttackPosition.DistanceSQ(goalPos);
				if (distSq > 90000f)
				{
					float dist = MathF.Sqrt(distSq);
					Vector2 dir = (goalPos - AttackPosition).SafeNormalize(Vector2.zeroVector);
					AttackPosition += dir * (dist - 300f) / 60f;
				}
			}
			base.NPC.damage = 0;
			base.NPC.dontTakeDamage = false;
			if (AttackFlag)
			{
				if (AttackCounter == 0)
				{
					base.NPC.ShowNameOnHover = true;
					base.NPC.velocity = base.NPC.DirectionFrom(Target.Center) * 4f;
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.95f;
				}
				if (AttackCounter >= IllusionTrickStunDuration)
				{
					SetupForNextAttack();
					base.NPC.Opacity = 1f;
					TeleportTime = 0f;
					return;
				}
				AttackCounter++;
			}
			else if (Time >= (float)IllusionTrickTimeLimit)
			{
				if (Time == (float)IllusionTrickTimeLimit)
				{
					List<int> allValidTargets = GetAllValidTargets(base.NPC.Center);
					AttackList.Clear();
					foreach (int p3 in allValidTargets)
					{
						AttackList.Add((byte)p3);
					}
					ActiveEntityIterator<NPC>.Enumerator enumerator4 = Main.ActiveNPCs.GetEnumerator();
					while (enumerator4.MoveNext())
					{
						NPC n = enumerator4.Current;
						if (n.type == ModContent.NPCType<FalseBrain>())
						{
							n.ModNPC<FalseBrain>().BeenHit = true;
							n.netUpdate = true;
						}
					}
					for (int i = 1; i <= 3; i++)
					{
						Color color = (Color)(i switch
						{
							1 => Color.Yellow, 
							2 => Color.Orange, 
							_ => Color.Red, 
						});
						GeneralParticleHandler.SpawnParticle(new PulseRing(base.NPC.Center, base.NPC.velocity * 0.5f, color, 0f, 1f + (float)i * 0.5f, 24));
					}
					base.NPC.netUpdate = true;
				}
				else if (Time % 30f == 0f)
				{
					if (AttackList.Count > 0)
					{
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<TelekineticBlast>(), 0, 0f, -1, (int)AttackList[0], 0f, base.NPC.whoAmI);
						}
						AttackList.RemoveAt(0);
					}
					else
					{
						Vector2 direction = Target.velocity.SafeNormalize(Vector2.UnitX * (float)Target.direction).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 3f, (float)Math.PI / 3f));
						float distance = DefaultTeleportDistance;
						base.NPC.damage = base.NPC.defDamage;
						AttackPosition = Target.Center + direction * distance;
						BoCAfterImages = new List<Particle>();
						base.NPC.Opacity = 1f;
						TeleportTime = 0f;
						Time = ChaseTime - 1;
						base.NPC.netUpdate = true;
						ResetAttackValues();
						AIState = BrainAIState.Phase2Idle;
					}
				}
			}
			else
			{
				CachedRatio = (float)Math.Cos(AttackTime * ((float)Math.PI * 2f) / FalseBrain.TimeDivisor);
				float lerp = CalamityUtils.SineInOutEasing(MathHelper.Clamp((Time - 150f) / 30f, 0f, 1f), 1);
				float baseDist = 240f;
				float circleDist = 480f;
				if (Time - 150f < 30f)
				{
					baseDist = MathHelper.Lerp(480f, 240f, lerp);
					circleDist = MathHelper.Lerp(240f, 480f, lerp);
				}
				base.NPC.Center = AttackPosition + Vector2.UnitX.RotatedBy(AttackRotation) * (baseDist + circleDist * ((float)Math.Sin((0f - AttackTime) * ((float)Math.PI * 2f) / FalseBrain.TimeDivisor) / 2f + 0.5f));
				NPC nPC2 = base.NPC;
				nPC2.Center += Vector2.UnitX.RotatedBy(AttackRotation + (float)Math.PI / 2f) * (90f * (float)Math.Cos(AttackTime * ((float)Math.PI * 2f) / FalseBrain.TimeDivisor));
				base.NPC.Opacity = 1f;
				base.DisableMultiplayerSmoothing = true;
				AttackCounter = 0;
			}
		}
		else if (Time <= 60f)
		{
			TeleportDuration = 60f;
			TeleportTime++;
			base.NPC.Opacity = 1f - Time / 60f;
			if (Time < 50f)
			{
				Vector2 startPoint = base.NPC.Center;
				Vector2 endPoint = base.NPC.Center + Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0f, (float)Math.PI * 2f)) * Main.rand.NextFloat(240f, 480f);
				Vector2 direction2 = endPoint - startPoint;
				float curveIntensity = Main.rand.NextFloat(-0.2f, 0.2f);
				Vector2 perpindicular = direction2.RotatedBy(1.5707963705062866);
				Vector2 controlPoint1 = startPoint + direction2 * 0.25f + perpindicular * curveIntensity;
				Vector2 controlPoint2 = startPoint + direction2 * 0.75f + perpindicular * curveIntensity;
				BrainOfCthulhuAfterImage afterimage = new BrainOfCthulhuAfterImage(new BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint), base.NPC.rotation, Vector2.One, (int)(60f - Time), BoCFrame);
				BoCAfterImages.Add(afterimage);
				GeneralParticleHandler.SpawnParticle(afterimage);
			}
		}
		else
		{
			base.NPC.Opacity = 0f;
			BoCAfterImages = new List<Particle>();
			base.NPC.damage = 0;
			base.NPC.dontTakeDamage = true;
		}
		if (Time >= 180f)
		{
			AttackTime++;
		}
		else if (Time >= 150f)
		{
			AttackTime += (Time - 150f) / 30f;
		}
	}

	private void DeathAnimation()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			base.NPC.velocity = base.NPC.DirectionFrom(Target.Center) * 6f;
		}
		else
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.95f;
		}
		base.NPC.damage = 0;
		base.NPC.rotation = (float)Math.PI / 24f * base.NPC.oldVelocity.X;
		TeleportTime *= 0.6f;
		if (TeleportTime < 0.005f)
		{
			TeleportTime = 0f;
		}
		BoCDrawOffset *= 0.6f;
		base.NPC.Opacity = BringOpacityTo(base.NPC.Opacity, 1f, 0.1f);
		(float, Vector2, int)[] bloodGushingData = new(float, Vector2, int)[5]
		{
			((float)Math.PI / 6f, new Vector2(18f, 12f), 60),
			((float)Math.PI, new Vector2(-30f, -10f), 150),
			(-(float)Math.PI / 4f, new Vector2(20f, -40f), 210),
			(3.926991f, new Vector2(-20f, -40f), 240),
			(1.7951958f, new Vector2(-5f, 22f), 260)
		};
		float EndTime = 270f;
		for (int i = 0; i < bloodGushingData.Length; i++)
		{
			if (!(Time >= (float)bloodGushingData[i].Item3))
			{
				continue;
			}
			if (Time == (float)bloodGushingData[i].Item3)
			{
				if (i == 3)
				{
					SoundEngine.PlaySound(in Death, base.NPC.Center);
				}
				Vector2 bloodDir = bloodGushingData[i].Item1.ToRotationVector2();
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.NPC.Center + bloodGushingData[i].Item2.RotatedBy(base.NPC.rotation), bloodDir * 7.5f, 16, 0.5f, Color.Red));
				base.NPC.velocity = bloodDir * -4f;
				SoundEngine.PlaySound(BloodShot with
				{
					Pitch = (float)i / (float)(bloodGushingData.Length - 1)
				}, base.NPC.Center);
				Main.LocalPlayer.SetScreenshake(1f);
			}
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center + bloodGushingData[i].Item2.RotatedBy(base.NPC.rotation), (bloodGushingData[i].Item1 + Main.rand.NextFloat(-(float)Math.PI / 10f, (float)Math.PI / 10f)).ToRotationVector2() * Main.rand.NextFloat(5f, 10f), 32, 1f, Color.Red));
			}
		}
		if (Time >= EndTime)
		{
			SoundEngine.PlaySound(in BloodExplosion, base.NPC.Center);
			SoundEngine.PlaySound(in BloodShot, base.NPC.Center);
			Main.LocalPlayer.SetScreenshake(2f);
			int pCount = 10;
			for (int k = 0; k < pCount; k++)
			{
				float initalSpeed = 24f;
				Vector2 pVelo = Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)pCount * (float)k) * initalSpeed;
				for (int l = 0; l < 2; l++)
				{
					GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center, pVelo.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.5f, 1f), 32, 1f, Color.Red));
				}
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.NPC.Center, pVelo * 0.75f, 16, 0.5f, Color.Red));
			}
			base.NPC.dontTakeDamage = false;
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
		float animationCompletion = Time / EndTime;
		base.NPC.frameCounter += 2f * animationCompletion;
		BoCDrawOffset = Main.rand.NextVector2Circular(4f, 4f) * animationCompletion;
		if (Main.rand.NextFloat(0.5f, 1f) < animationCompletion)
		{
			Vector2 edgeBloodDir = Main.rand.NextVector2CircularEdge(1f, 1f);
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center + edgeBloodDir * base.NPC.Size * 0.75f, edgeBloodDir.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 10f, (float)Math.PI / 10f)) * Main.rand.NextFloat(2f, 4f), 16, 0.75f, Color.Red));
		}
	}

	public override void SendExtraAI(BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		binaryWriter.Write((byte)PreviousAttack);
		if (AIState == BrainAIState.Stunned || (int)AIState >= 9)
		{
			binaryWriter.Write(TeleportTime);
			binaryWriter.Write(TeleportDuration);
		}
		if ((int)AIState <= 1)
		{
			binaryWriter.Write(SpawnTime);
			binaryWriter.Write(SpawnDelay);
		}
		binaryWriter.WriteFlags(OnSecondCreeperPhase, isNegative, AttackFlag);
		binaryWriter.Write(AttackRotation);
		binaryWriter.Write(AttackTime);
		binaryWriter.Write(AttackCounter);
		binaryWriter.WritePackedWorldPosition(AttackPosition);
		binaryWriter.Write((byte)availableAttacks.Count);
		binaryWriter.Write(availableAttacks.Select((BrainAIState e) => (byte)e).ToArray());
		binaryWriter.Write((byte)AttackList.Count);
		binaryWriter.Write(AttackList.ToArray());
	}

	public override void ReceiveExtraAI(BitReader bitReader, BinaryReader binaryReader)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		PreviousAttack = (BrainAIState)binaryReader.ReadByte();
		if (AIState == BrainAIState.Stunned || (int)AIState >= 9)
		{
			TeleportTime = binaryReader.ReadSingle();
			TeleportDuration = binaryReader.ReadSingle();
		}
		if ((int)AIState <= 1)
		{
			SpawnTime = binaryReader.ReadSingle();
			SpawnDelay = binaryReader.ReadInt32();
		}
		binaryReader.ReadFlags(out OnSecondCreeperPhase, out isNegative, out AttackFlag);
		AttackRotation = binaryReader.ReadSingle();
		AttackTime = binaryReader.ReadSingle();
		AttackCounter = binaryReader.ReadInt32();
		AttackPosition = binaryReader.ReadPackedWorldPosition();
		int availableLength = binaryReader.ReadByte();
		availableAttacks = (from e in binaryReader.ReadBytes(availableLength)
			select (BrainAIState)e).ToList();
		byte attackLength = binaryReader.ReadByte();
		AttackList = binaryReader.ReadBytes(attackLength).ToList();
	}

	public override bool? CanBeHitByProjectile(Mod mod, Projectile projectile)
	{
		if (AIState == BrainAIState.IllusionTrick && !AttackFlag && projectile.Calamity().IgnoreBoCIllusions)
		{
			return false;
		}
		return base.CanBeHitByProjectile(mod, projectile);
	}

	public override void ModifyHitByItem(Mod mod, Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (AIState != BrainAIState.DeathAnimation)
		{
			modifiers.SetMaxDamage(base.NPC.life - 1);
		}
	}

	public override void ModifyHitByProjectile(Mod mod, Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (AIState != BrainAIState.DeathAnimation)
		{
			modifiers.SetMaxDamage(base.NPC.life - 1);
		}
	}

	public override void HitEffect(Mod mod, NPC.HitInfo hit)
	{
		if (AIState != BrainAIState.DeathAnimation && base.NPC.life + 1 <= hit.Damage)
		{
			TriggerDeathAnimation();
		}
		else if (AIState == BrainAIState.IllusionTrick && Time < 960f)
		{
			AttackFlag = true;
			base.NPC.netUpdate = true;
		}
	}

	private void TriggerDeathAnimation()
	{
		base.NPC.life = 1;
		base.NPC.lifeRegen = 0;
		base.NPC.BossBar = null;
		base.NPC.dontTakeDamage = true;
		if (AIState == BrainAIState.Stunned || AIState == BrainAIState.IllusionTrick)
		{
			TeleportTime = 0f;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.type == ModContent.NPCType<FalseBrain>())
			{
				n.ModNPC<FalseBrain>().BeenHit = true;
			}
		}
		AIState = BrainAIState.DeathAnimation;
		ResetAttackValues();
		Time = 0f;
		base.NPC.netUpdate = true;
	}

	public override void FindFrame(Mod mod, int frameHeight)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (BoCFrame == Rectangle.Empty)
		{
			BoCFrame = TextureAssets.Npc[266].Frame(1, 8);
		}
		if (base.NPC.frameCounter == 0.0)
		{
			BoCFrame.Y += frameHeight;
		}
		if ((int)AIState <= 9)
		{
			if (BoCFrame.Y > frameHeight * 3)
			{
				BoCFrame.Y = 0;
			}
			return;
		}
		if (BoCFrame.Y < frameHeight * 4)
		{
			BoCFrame.Y = frameHeight * 4;
		}
		if (BoCFrame.Y > frameHeight * 7)
		{
			BoCFrame.Y = frameHeight * 4;
		}
	}

	public override bool PreDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		bool num = (int)AIState <= 10;
		bool drawBrain = true;
		if (num)
		{
			List<NPC> creepers = Main.npc.Where((NPC nPC) => nPC.active && nPC.type == 267).ToList();
			creepers.Sort(delegate(NPC a, NPC b)
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				return b.DistanceSQ(base.NPC.Center).CompareTo(a.DistanceSQ(base.NPC.Center));
			});
			List<(int creeper, List<VerletSimulatedSegment> tendril, int reelInTimer)> list = BrainOfCthulhuSystem.VerletTendrils.ToList();
			list.Sort(delegate((int creeper, List<VerletSimulatedSegment> tendril, int reelInTimer) a, (int creeper, List<VerletSimulatedSegment> tendril, int reelInTimer) b)
			{
				int num2 = creepers.IndexOf(Main.npc[b.creeper]);
				if (num2 == -1)
				{
					num2 = int.MaxValue;
				}
				int num3 = creepers.IndexOf(Main.npc[a.creeper]);
				if (num3 == -1)
				{
					num3 = int.MaxValue;
				}
				return num2.CompareTo(num3);
			});
			Vector2 tendrilScale = default(Vector2);
			foreach (var v in list)
			{
				List<VerletSimulatedSegment> curvePoints = v.tendril;
				if (curvePoints == null)
				{
					continue;
				}
				NPC creeper = Main.npc[v.creeper];
				if (creeper == null || !creeper.active || creeper.type != 267)
				{
					creeper = null;
				}
				float glowIntensity = creeper?.AIOverride<CreeperAI>().ConnectionOpacity ?? 0f;
				Color ichorLess = Color.Lerp(Color.Transparent, Color.OrangeRed * 0.333f, glowIntensity);
				Color ichorful = Color.Lerp(Color.OrangeRed * 0.25f, Color.Orange * 0.666f, glowIntensity);
				for (int i = 0; i < curvePoints.Count; i++)
				{
					Vector2 start = curvePoints[i].position;
					Vector2 end = ((i != curvePoints.Count - 1) ? curvePoints[i + 1].position : (creeper?.Center ?? curvePoints[i].position));
					Vector2 center = (end + start) / 2f;
					start -= Main.screenPosition;
					end -= Main.screenPosition;
					float rotation = (end - start).ToRotation() - (float)Math.PI / 2f;
					float ichorRatio = 0f;
					if (creeper != null)
					{
						float flowTime = creeper.AIOverride<CreeperAI>().FlowTime;
						float flowAmt = creeper.AIOverride<CreeperAI>().FlowAmount;
						ichorRatio = CalamityUtils.ExpInEasing((float)Math.Sin((flowTime + (float)i * flowAmt) * 2f) / 2f + 0.5f, 1);
					}
					Color glowColor = Color.Lerp(ichorLess, ichorful, ichorRatio);
					float dist = Vector2.Distance(start, end);
					((Vector2)(ref tendrilScale))._002Ector(1f + 0.5f * (ichorRatio * (1f + glowIntensity * 0.5f)), dist / (float)BrainOfCthulhuSystem.tendril.Height());
					spriteBatch.Draw(BrainOfCthulhuSystem.tendril.Value, start, (Rectangle?)null, Lighting.GetColor(center.ToTileCoordinates()) * base.NPC.Opacity, rotation, BrainOfCthulhuSystem.tendril.Size() * Vector2.UnitX * 0.5f, tendrilScale, (SpriteEffects)0, 0f);
					spriteBatch.Draw(BrainOfCthulhuSystem.GetTendrilGlow(), start, (Rectangle?)null, glowColor * base.NPC.Opacity, rotation, BrainOfCthulhuSystem.GetTendrilGlow().Size() * Vector2.UnitX * 0.5f, tendrilScale, (SpriteEffects)0, 0f);
				}
			}
			foreach (NPC creeper2 in creepers)
			{
				float glowIntensity2 = creeper2.AIOverride<CreeperAI>().ConnectionOpacity;
				spriteBatch.Draw(BrainOfCthulhuSystem.GetCreeperGlow(), creeper2.Center - Main.screenPosition, (Rectangle?)null, Color.Orange * glowIntensity2, creeper2.rotation, TextureAssets.Npc[267].Size() * 0.5f, creeper2.scale * 1.15f, (SpriteEffects)0, 0f);
				spriteBatch.Draw(TextureAssets.Npc[267].Value, creeper2.Center - Main.screenPosition, (Rectangle?)null, Lighting.GetColor(creeper2.Center.ToTileCoordinates()).MultiplyRGB(Color.Lerp(Color.White, new Color(255, 180, 180), glowIntensity2)), creeper2.rotation, TextureAssets.Npc[267].Size() * 0.5f, creeper2.scale, (SpriteEffects)0, 0f);
			}
		}
		else
		{
			List<NPC> falseBrains = Main.npc.Where((NPC nPC) => nPC.active && nPC.type == ModContent.NPCType<FalseBrain>()).ToList();
			if (falseBrains.Count > 0)
			{
				drawBrain = false;
				falseBrains.Add(base.NPC);
				falseBrains.Sort(delegate(NPC a, NPC b)
				{
					float num2 = ((!(a.ModNPC is FalseBrain falseBrain2)) ? a.AIOverride<BrainOfCthulhuAI>().CachedRatio : falseBrain2.DrawPriority);
					float value = ((!(b.ModNPC is FalseBrain falseBrain3)) ? b.AIOverride<BrainOfCthulhuAI>().CachedRatio : falseBrain3.DrawPriority);
					return num2.CompareTo(value);
				});
				foreach (NPC n in falseBrains)
				{
					if (n.ModNPC is FalseBrain falseBrain)
					{
						falseBrain.DrawSelf(spriteBatch, screenPos, Lighting.GetColor(n.Center.ToTileCoordinates()));
					}
					else if (AIState == BrainAIState.DeathAnimation)
					{
						drawBrain = true;
					}
					else
					{
						DrawBrainLikeFakes(spriteBatch, n);
					}
				}
			}
		}
		if (drawBrain)
		{
			DrawBrain(spriteBatch, base.NPC);
		}
		return false;
	}

	public static int GetBrainOfCthuluCreepersCountRevDeath()
	{
		if (!CalamityWorld.death)
		{
			return 21;
		}
		return 30;
	}

	public static List<int> GetAllValidTargets(Vector2 brainPosition)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		List<int> validTargets = new List<int>();
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			if (ValidateTarget(p, brainPosition))
			{
				validTargets.Add(p.whoAmI);
			}
		}
		return validTargets;
	}

	public static bool ValidateTarget(Player p, Vector2 brainPosition)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (!p.dead && p.ZoneCrimson)
		{
			return p.Center.DistanceSQ(brainPosition) <= DespawnRangeSQ;
		}
		return false;
	}

	private void SelectNewTarget()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
		options.aggroRatio = -1f;
		options.finishThemOff = true;
		options.maxSearchRange = DespawnRange;
		options.targetType = NPCTargetType.ForceSwitch;
		if (GetAllValidTargets(base.NPC.Center).Any((int p) => !TargetsSet.Contains(p)))
		{
			options.excludedPlayers = TargetsSet;
		}
		else
		{
			TargetsSet.Clear();
			options.excludedPlayers = new HashSet<int> { base.NPC.target };
		}
		base.NPC.CalamityTargeting(options);
		TargetsSet.Add(base.NPC.target);
	}

	internal static float BringOpacityTo(float currentOpacity, float goalOpacity, float changeAmount = 0.025f)
	{
		if (currentOpacity == goalOpacity)
		{
			return goalOpacity;
		}
		if (currentOpacity < goalOpacity)
		{
			currentOpacity += changeAmount;
			if (currentOpacity >= goalOpacity)
			{
				return goalOpacity;
			}
			return currentOpacity;
		}
		currentOpacity -= changeAmount;
		if (currentOpacity <= goalOpacity)
		{
			return goalOpacity;
		}
		return currentOpacity;
	}

	private static void DrawBrain(SpriteBatch spriteBatch, NPC brain)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		BrainOfCthulhuAI ai = brain.AIOverride<BrainOfCthulhuAI>();
		bool num = (int)ai.AIState < 9;
		Vector2 drawPos = brain.Center + ai.BoCDrawOffset + Vector2.UnitY * 16f - Main.screenPosition;
		Vector2 scale = Vector2.One;
		if (!num && ai.TeleportTime != 0f)
		{
			ai.BoCAfterImages.RemoveAll((Particle p) => p.Time > p.Lifetime);
			foreach (Particle boCAfterImage in ai.BoCAfterImages)
			{
				boCAfterImage.CustomDraw(spriteBatch);
			}
			scale = Vector2.Lerp(Vector2.One, new Vector2(0.5f + ((float)Math.Cos(ai.Time / (ai.TeleportDuration / 2f) * ((float)Math.PI * 2f)) / 2f + 0.5f), 0.5f + ((float)Math.Sin(ai.Time / (ai.TeleportDuration / 2f) * ((float)Math.PI * 2f)) / 2f + 0.5f)), CalamityUtils.SineInOutEasing(ai.TeleportTime / (ai.TeleportDuration / 2f), 1));
			spriteBatch.Draw(TextureAssets.Npc[266].Value, drawPos, (Rectangle?)ai.BoCFrame, Lighting.GetColor(brain.Center.ToTileCoordinates()) * brain.Opacity, brain.rotation, ai.BoCFrame.Size() * 0.5f, scale * brain.scale, (SpriteEffects)0, 0f);
		}
		else
		{
			spriteBatch.Draw(TextureAssets.Npc[266].Value, drawPos, (Rectangle?)ai.BoCFrame, Lighting.GetColor(brain.Center.ToTileCoordinates()) * brain.Opacity, brain.rotation, ai.BoCFrame.Size() * 0.5f, scale * brain.scale, (SpriteEffects)0, 0f);
		}
	}

	private static void DrawBrainLikeFakes(SpriteBatch spriteBatch, NPC brain)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		BrainOfCthulhuAI ai = brain.AIOverride<BrainOfCthulhuAI>();
		Vector2 scaleDistort = default(Vector2);
		((Vector2)(ref scaleDistort))._002Ector((float)Math.Cos(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) * 2f) / 2f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) * 2f) / 2f);
		int num = 150 - (int)ai.Time;
		float startLerp = (float)num / 60f;
		Color drawColor = Lighting.GetColor(brain.Center.ToTileCoordinates());
		if (num > 0)
		{
			drawColor *= 1f - startLerp;
			scaleDistort *= startLerp;
		}
		else
		{
			scaleDistort = Vector2.Zero;
		}
		spriteBatch.Draw(TextureAssets.Npc[266].Value, brain.Center + Vector2.UnitY * 16f - Main.screenPosition, (Rectangle?)ai.BoCFrame, drawColor, brain.rotation, ai.BoCFrame.Size() * 0.5f, (Vector2.One + scaleDistort) * brain.scale, (SpriteEffects)0, 0f);
	}

	private void SetupForNextAttack()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Player target = Main.player[base.NPC.target];
		Vector2 direction = target.velocity.SafeNormalize(Vector2.UnitX * (float)target.direction).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 3f, (float)Math.PI / 3f));
		float distance = DefaultTeleportDistance;
		AttackPosition = target.Center + direction * distance;
		BoCAfterImages = new List<Particle>();
		Time = ChaseTime - 1;
		ResetAttackValues();
		base.NPC.netUpdate = true;
		if (availableAttacks.Count != 0)
		{
			if (availableAttacks[0] != BrainAIState.Phase2Idle)
			{
				AttackCounter = 4;
			}
			else
			{
				availableAttacks.RemoveAt(0);
			}
		}
		AIState = BrainAIState.Phase2Idle;
	}

	private void ResetAttackValues()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		isNegative = false;
		AttackRotation = 0f;
		AttackTime = 0f;
		AttackFlag = false;
		AttackPosition = Vector2.Zero;
		AttackCounter = 0;
	}

	public BrainOfCthulhuAI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		BoCAfterImages = new List<Particle>();
		ShieldOpacity = 1f;
		ShieldScale = 1f;
		BoCDrawOffset = Vector2.Zero;
		BoCFrame = new Rectangle(0, 0, 198, 180);
		PreviousAttack = BrainAIState.Phase1Idle;
		AttackPosition = Vector2.Zero;
		availableAttacks = new List<BrainAIState>();
		AttackList = new List<byte>();
		TargetsSet = new HashSet<int>();
		base._002Ector();
	}
}
