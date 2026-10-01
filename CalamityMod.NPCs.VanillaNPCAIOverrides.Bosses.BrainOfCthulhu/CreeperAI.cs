using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;

public class CreeperAI : VanillaAIOverride
{
	internal enum CreeperAIState
	{
		Idle,
		Charge
	}

	internal float FlowTime;

	internal int Time;

	internal int CachedValue2;

	internal int PartnerIndex;

	internal Vector2 AttackPosition;

	internal float ConnectionOpacity;

	private bool useBossAIState;

	internal float FlowAmount => MathHelper.Lerp(0.1f, 0.15f, base.NPC.localAI[3]);

	internal int CreeperID
	{
		get
		{
			return (int)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	internal CreeperAIState AIState
	{
		get
		{
			return (CreeperAIState)base.NPC.ai[1];
		}
		set
		{
			base.NPC.ai[1] = (float)value;
		}
	}

	internal ref float AttackAngle => ref base.NPC.ai[2];

	internal ref float CachedValue1 => ref base.NPC.ai[3];

	private NPC brain => Main.npc[NPC.crimsonBoss];

	private BrainOfCthulhuAI bocAI => brain.AIOverride<BrainOfCthulhuAI>();

	private float bossCounter => bocAI.Time;

	internal bool evenID => CreeperID % 2 == 0;

	private int creeperCount
	{
		get
		{
			int c = NPC.CountNPCS(base.NPC.type);
			if (c > BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath())
			{
				c = BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath();
			}
			return c;
		}
	}

	private int localCreeperID => Main.npc.Where((NPC n) => n.active && n.type == 267).ToList().IndexOf(base.NPC);

	private float CreeperAmountRatio => (float)creeperCount / (float)BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath();

	public override bool EnableMultiplayerSmoothingAheadOfAI => true;

	public override void SetDefaults(Mod mod)
	{
		base.NPC.damage = (base.NPC.defDamage = 36);
	}

	public override void OnSpawn(Mod mod)
	{
		base.NPC.damage = 0;
		base.NPC.netUpdate = true;
	}

	public override bool AI(Mod mod)
	{
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.crimsonBoss < 0)
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return false;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
		}
		if (!Main.dedServ)
		{
			BrainOfCthulhuSystem.VerletTendrils[CreeperID].creeper = base.NPC.whoAmI;
		}
		if ((int)bocAI.AIState < 1)
		{
			base.NPC.damage = 0;
		}
		int num = 8;
		List<BrainOfCthulhuAI.BrainAIState> list = new List<BrainOfCthulhuAI.BrainAIState>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<BrainOfCthulhuAI.BrainAIState> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = BrainOfCthulhuAI.BrainAIState.UndergroundSpawnAnimation;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.SurfaceSpawnAnimation;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.Stunned;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.CreeperSwipes;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.CreeperSwings;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.CreeperOrbit;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.CreeperSpiral;
		num2++;
		span[num2] = BrainOfCthulhuAI.BrainAIState.TelekineticOnslaught;
		List<BrainOfCthulhuAI.BrainAIState> bossAIStatesToUse = list;
		useBossAIState = bossAIStatesToUse.Contains(bocAI.AIState) && bossCounter >= 0f;
		if (bocAI.AIState == BrainOfCthulhuAI.BrainAIState.UndergroundSpawnAnimation || bocAI.AIState == BrainOfCthulhuAI.BrainAIState.SurfaceSpawnAnimation || bocAI.AIState == BrainOfCthulhuAI.BrainAIState.Stunned)
		{
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.dontTakeDamage = false;
		}
		if (useBossAIState)
		{
			switch (bocAI.AIState)
			{
			case BrainOfCthulhuAI.BrainAIState.UndergroundSpawnAnimation:
			case BrainOfCthulhuAI.BrainAIState.SurfaceSpawnAnimation:
				base.NPC.damage = 0;
				SpawnAnimation();
				break;
			case BrainOfCthulhuAI.BrainAIState.TelekineticOnslaught:
				TelekineticOnslaught();
				break;
			case BrainOfCthulhuAI.BrainAIState.Stunned:
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
				base.NPC.damage = 0;
				break;
			}
			case BrainOfCthulhuAI.BrainAIState.CreeperSwipes:
				CreeperSwipes();
				break;
			case BrainOfCthulhuAI.BrainAIState.CreeperSwings:
				CreeperSwings();
				break;
			case BrainOfCthulhuAI.BrainAIState.CreeperOrbit:
				CreeperOrbit();
				break;
			case BrainOfCthulhuAI.BrainAIState.CreeperSpiral:
				CreeperSpiral();
				break;
			}
		}
		else
		{
			switch (AIState)
			{
			case CreeperAIState.Idle:
				CreeperIdle();
				break;
			case CreeperAIState.Charge:
				CreeperCharge();
				break;
			}
		}
		FlowTime += 0.01f * (2f * (1f + ConnectionOpacity));
		return false;
	}

	private void SpawnAnimation()
	{
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		float baseRotation = (float)Math.PI * 2f / ((float)BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath() / 2f) * ((float)CreeperID / 2f);
		float speedCap = 8f;
		float accel = 1f;
		float acceptableDist = 9216f;
		Player player = Main.player[brain.target];
		Vector2 goalLocation;
		if (bocAI.SpawnTime != 0f && Time >= 0)
		{
			float brainTime = bossCounter - Math.Abs(bocAI.SpawnTime);
			if (brainTime < 180f)
			{
				Vector2 baseOffset = default(Vector2);
				((Vector2)(ref baseOffset))._002Ector((float)(evenID ? (-360) : 360), 0f);
				Vector2 rotationOffset = Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 120f * (float)((!evenID) ? 1 : (-1))) * 128f;
				goalLocation = player.Center + baseOffset + rotationOffset;
			}
			else if (brainTime < 240f)
			{
				Vector2 baseOffset2 = default(Vector2);
				((Vector2)(ref baseOffset2))._002Ector((float)(evenID ? (-200) : 200), 64f);
				Vector2 rotationOffset2 = Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 60f * (float)((!evenID) ? 1 : (-1))) * 32f;
				goalLocation = brain.Center + baseOffset2 + rotationOffset2;
				speedCap = 12f;
				accel = 2f;
				acceptableDist = 4096f;
			}
			else
			{
				Vector2 baseOffset3 = default(Vector2);
				((Vector2)(ref baseOffset3))._002Ector((float)(evenID ? (-360) : 360), -64f);
				Vector2 rotationOffset3 = Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 60f * (float)((!evenID) ? 1 : (-1))) * 128f;
				goalLocation = brain.Center + baseOffset3 + rotationOffset3;
				speedCap = 12f;
				accel = 2f;
				acceptableDist = 4096f;
			}
			Time++;
		}
		else if (Time >= 0)
		{
			Vector2 baseOffset4 = default(Vector2);
			((Vector2)(ref baseOffset4))._002Ector((float)(evenID ? (-360) : 360), 0f);
			Vector2 rotationOffset4 = Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 120f * (float)((!evenID) ? 1 : (-1))) * 128f;
			goalLocation = player.Center + baseOffset4 + rotationOffset4;
			Time++;
		}
		else
		{
			Vector2 baseOffset5 = default(Vector2);
			((Vector2)(ref baseOffset5))._002Ector((float)(evenID ? (-256) : 256), 0f);
			Vector2 rotationOffset5 = Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 120f * (float)((!evenID) ? 1 : (-1))) * 64f;
			goalLocation = brain.Center + baseOffset5 + rotationOffset5;
		}
		if (base.NPC.DistanceSQ(goalLocation) > acceptableDist)
		{
			NPC nPC = base.NPC;
			nPC.velocity += base.NPC.DirectionTo(goalLocation) * accel;
			base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, speedCap);
		}
	}

	private void TelekineticOnslaught()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		if (Time > 0)
		{
			Point tileCoords = AttackPosition.ToTileCoordinates();
			if (WorldGen.SolidOrSlopedTile(tileCoords.X, tileCoords.Y))
			{
				float rayDist = CalamityUtils.PreciseDistanceToTileCollisionHit(brain.Center, AttackAngle, 800f, 4f);
				AttackPosition = brain.Center + AttackAngle.ToRotationVector2() * (rayDist - 64f);
			}
			if (base.NPC.DistanceSQ(AttackPosition) > 4096f)
			{
				NPC nPC = base.NPC;
				nPC.velocity += base.NPC.DirectionTo(AttackPosition);
				base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 10f);
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.8f;
			}
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f);
			base.NPC.damage = 0;
		}
		else
		{
			useBossAIState = false;
		}
		Time++;
	}

	private void CreeperSwipes()
	{
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		if (bossCounter < 15f)
		{
			useBossAIState = false;
			Time = -1;
			AIState = CreeperAIState.Idle;
		}
		if (brain.AIOverride<BrainOfCthulhuAI>().AttackList.Contains((byte)base.NPC.whoAmI))
		{
			if (Time < 0)
			{
				Time = 0;
			}
		}
		else if (Time >= 0)
		{
			Time = -1;
		}
		float baseRotation = (float)Math.PI * 2f / ((float)BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath() / 2f) * ((float)CreeperID / 2f);
		bool singleHand = bocAI.AttackFlag;
		int handSide = bocAI.AttackSign;
		if (Time == -1)
		{
			base.NPC.damage = 0;
			base.NPC.knockBackResist = 0.72f;
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0f);
			Vector2 goalLocation = (singleHand ? (brain.Center + Vector2.UnitY * -32f + Vector2.UnitX * 256f * (float)handSide + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)handSide) * new Vector2(32f, 96f)) : (brain.Center + Vector2.UnitY * -32f + Vector2.UnitX * (float)(evenID ? (-256) : 256) + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)((!evenID) ? 1 : (-1))) * new Vector2(32f, 96f)));
			goalLocation += Main.player[brain.target].velocity * 24f;
			float distToGoal = base.NPC.Center.Distance(goalLocation);
			base.NPC.velocity = base.NPC.DirectionTo(goalLocation) * (2f + MathHelper.Clamp(distToGoal / 24f, 0f, 64f));
			return;
		}
		if (Time == 0)
		{
			base.NPC.netUpdate = true;
		}
		base.NPC.damage = base.NPC.defDamage;
		base.NPC.knockBackResist = 0f;
		ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f);
		float accel = 2f;
		float speed = 12f;
		if (Time < brain.AIOverride<BrainOfCthulhuAI>().SwipeDelay)
		{
			Vector2 goalLocation = (singleHand ? (brain.Center + Vector2.UnitY * -96f + Vector2.UnitX * 300f * (float)handSide + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)handSide) * new Vector2(32f, 96f)) : (brain.Center + Vector2.UnitY * -96f + Vector2.UnitX * (float)(evenID ? (-300) : 300) + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)((!evenID) ? 1 : (-1))) * new Vector2(32f, 96f)));
			goalLocation += Main.player[brain.target].velocity * 24f;
			NPC nPC = base.NPC;
			nPC.velocity += base.NPC.DirectionTo(goalLocation) * accel;
			base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, speed);
		}
		else
		{
			Vector2 goalLocation = (singleHand ? (brain.Center + Vector2.UnitY * -96f + Vector2.UnitX * 256f * (float)handSide + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)handSide) * new Vector2(32f, 96f)) : (brain.Center + Vector2.UnitY * -96f + Vector2.UnitX * (float)(evenID ? (-256) : 256) + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 30f * (float)((!evenID) ? 1 : (-1))) * new Vector2(32f, 96f)));
			goalLocation.Y += MathHelper.Lerp(0f, 420f, CalamityUtils.SineInOutEasing(MathHelper.Clamp((float)(Time - brain.AIOverride<BrainOfCthulhuAI>().SwipeDelay) / 20f, 0f, 1f), 1));
			if (!singleHand)
			{
				goalLocation.X += MathHelper.Lerp(0f, 900f, CalamityUtils.SineInOutEasing(MathHelper.Clamp((float)(Time - (brain.AIOverride<BrainOfCthulhuAI>().SwipeDelay + 5)) / 55f, 0f, 1f), 1)) * (float)(evenID ? 1 : (-1));
			}
			else
			{
				goalLocation.X += MathHelper.Lerp(0f, 900f, CalamityUtils.SineInOutEasing(MathHelper.Clamp((float)(Time - (brain.AIOverride<BrainOfCthulhuAI>().SwipeDelay + 5)) / 55f, 0f, 1f), 1)) * (float)((handSide == -1) ? 1 : (-1));
			}
			base.NPC.Center = Vector2.Lerp(base.NPC.Center, goalLocation, MathHelper.Clamp((float)(Time - brain.AIOverride<BrainOfCthulhuAI>().SwipeDelay) / 10f, 0f, 1f));
			base.DisableMultiplayerSmoothing = true;
		}
		Time++;
	}

	private void CreeperSwings()
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		if (brain.AIOverride<BrainOfCthulhuAI>().AttackList.Contains((byte)base.NPC.whoAmI))
		{
			if (Time < 0)
			{
				Time = 0;
				base.NPC.netUpdate = true;
				if (Main.netMode != 1)
				{
					AttackPosition = base.NPC.Center;
				}
			}
		}
		else if (Time >= 0)
		{
			Time = -1;
		}
		if (Time < 0)
		{
			float baseRotation = (brain.Center - Main.player[brain.target].Center).ToRotation();
			bool evenID = CreeperID % 2 == 0;
			Vector2 goalLocation = brain.Center + Vector2.UnitX.RotatedBy((double)baseRotation + Math.Sin(bossCounter / 60f + (float)CreeperID) * (double)((!evenID) ? 1 : (-1))) * (float)(evenID ? 175 : 250);
			if (base.NPC.DistanceSQ(goalLocation) > 9216f)
			{
				NPC nPC = base.NPC;
				nPC.velocity += base.NPC.DirectionTo(goalLocation);
				base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 8f);
			}
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0f);
			return;
		}
		Player sharedTarget = Main.player[brain.target];
		Vector2 dashDir = AttackAngle.ToRotationVector2();
		if (!bocAI.OnSecondCreeperPhase)
		{
			float positioningTime = BrainOfCthulhuAI.LightSwipeTravelTime + BrainOfCthulhuAI.LightSwipeAttackDelay;
			if ((float)Time <= positioningTime)
			{
				Vector2 goalPosition = sharedTarget.Center - dashDir * 128f;
				goalPosition += dashDir.RotatedBy(1.5707963705062866) * 16f;
				base.NPC.Center = Vector2.Lerp(AttackPosition, goalPosition, CalamityUtils.SineOutEasing(MathHelper.Clamp((float)Time / (float)BrainOfCthulhuAI.LightSwipeTravelTime, 0f, 1f), 1));
				base.NPC.velocity = Vector2.Zero;
				base.DisableMultiplayerSmoothing = true;
			}
			else
			{
				AttackPosition = Vector2.Zero;
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.knockBackResist = 0f;
				int reelbackTime = 22;
				if ((float)Time < positioningTime + (float)reelbackTime)
				{
					float reelBackSpeedExponent = 2.6f;
					float reelBackCompletion = Utils.GetLerpValue(0f, reelbackTime, (float)Time - positioningTime, clamped: true);
					float reelBackSpeed = MathHelper.Lerp(2.5f, 16f, MathF.Pow(reelBackCompletion, reelBackSpeedExponent));
					Vector2 reelBackVelocity = dashDir * (0f - reelBackSpeed);
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity, 0.25f);
					if ((float)Time == positioningTime + (float)reelbackTime - 5f)
					{
						SoundEngine.PlaySound(in SoundID.DD2_MonkStaffSwing, base.NPC.Center);
					}
				}
				else if ((float)Time == positioningTime + (float)reelbackTime)
				{
					base.NPC.velocity = dashDir * 32f;
				}
				else
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 0.9f;
					if ((float)Time >= positioningTime + (float)reelbackTime + 15f)
					{
						base.NPC.damage = 0;
						Time = -1;
						brain.AIOverride<BrainOfCthulhuAI>().AttackList.Remove((byte)base.NPC.whoAmI);
						AttackPosition = Vector2.Zero;
						ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0f);
						return;
					}
				}
			}
		}
		else
		{
			float positioningTime2 = BrainOfCthulhuAI.StrongSwipeTravelTime + BrainOfCthulhuAI.StrongSwipeAttackDelay;
			if ((float)Time <= positioningTime2)
			{
				if (AttackPosition == Vector2.Zero)
				{
					AttackPosition = base.NPC.Center;
				}
				Vector2 goalPosition2 = sharedTarget.Center - dashDir * 128f;
				base.NPC.Center = Vector2.Lerp(AttackPosition, goalPosition2, CalamityUtils.SineOutEasing(MathHelper.Clamp((float)Time / (float)BrainOfCthulhuAI.StrongSwipeTravelTime, 0f, 1f), 1));
				base.NPC.velocity = Vector2.Zero;
				base.DisableMultiplayerSmoothing = true;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.knockBackResist = 0f;
				int reelbackTime2 = 18;
				float swingTime = 10f;
				if ((float)Time <= positioningTime2 + (float)reelbackTime2)
				{
					float reelBackSpeedExponent2 = 2.6f;
					float reelBackCompletion2 = Utils.GetLerpValue(0f, reelbackTime2, (float)Time - positioningTime2, clamped: true);
					float reelBackSpeed2 = MathHelper.Lerp(2.5f, 16f, MathF.Pow(reelBackCompletion2, reelBackSpeedExponent2));
					Vector2 reelBackVelocity2 = dashDir * (0f - reelBackSpeed2);
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity2, 0.25f);
					if ((float)Time == positioningTime2 + (float)reelbackTime2 - 5f)
					{
						SoundEngine.PlaySound(in SoundID.DD2_MonkStaffSwing, base.NPC.Center);
					}
					if ((float)Time == positioningTime2 + (float)reelbackTime2)
					{
						AttackPosition = base.NPC.Center;
					}
				}
				else if ((float)Time <= positioningTime2 + (float)reelbackTime2 + swingTime)
				{
					if (Main.npc[PartnerIndex].active)
					{
						float moveDist = 196f;
						base.NPC.Center = Vector2.Lerp(AttackPosition, AttackPosition + dashDir * moveDist, ((float)Time - (positioningTime2 + (float)reelbackTime2)) / swingTime);
						base.NPC.velocity = Vector2.Zero;
						base.DisableMultiplayerSmoothing = true;
						if ((float)Time == positioningTime2 + (float)reelbackTime2 + swingTime)
						{
							SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, base.NPC.Center);
							SoundEngine.PlaySound(base.NPC.HitSound, (Vector2?)base.NPC.Center);
							base.NPC.velocity = dashDir * -8f;
							Vector2 attackCenter = AttackPosition + dashDir * 200f;
							if (dashDir.X <= 0f)
							{
								GeneralParticleHandler.SpawnParticle(new CustomPulse(attackCenter, Vector2.Zero, Color.Red, "CalamityMod/Particles/SmokeExplosion", Vector2.One, Main.rand.NextFloatDirection(), 0.05f, 0.15f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
							}
							if (Main.netMode != 1)
							{
								for (int i = -1; i <= 1; i++)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromThis(), attackCenter, dashDir.RotatedBy((float)Math.PI / 2f + (float)Math.PI / 6f * (float)i) * 8f, 811, BrainOfCthulhuAI.BloodShotDamage, 0.5f);
								}
							}
						}
					}
					else
					{
						base.NPC.velocity = dashDir * 19.6f;
					}
				}
				else
				{
					if (!((float)Time < positioningTime2 + (float)reelbackTime2 + swingTime + 30f))
					{
						base.NPC.damage = 0;
						Time = -1;
						brain.AIOverride<BrainOfCthulhuAI>().AttackList.Remove((byte)base.NPC.whoAmI);
						AttackPosition = Vector2.Zero;
						ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0.5f);
						return;
					}
					NPC nPC3 = base.NPC;
					nPC3.velocity *= 0.96f;
				}
			}
		}
		ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f);
		Time++;
	}

	private void CreeperOrbit()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 0 && CachedValue2 != -1)
		{
			CreeperCharge();
			return;
		}
		BrainOfCthulhuAI brainAI = brain.AIOverride<BrainOfCthulhuAI>();
		if (Main.netMode == 0)
		{
			if (bossCounter == 0f)
			{
				CachedValue1 = (float)Math.PI * 2f / (float)creeperCount * (float)localCreeperID;
				AttackPosition = base.NPC.Center;
				AttackAngle = 0f;
				Time = -1;
				base.NPC.netUpdate = true;
			}
		}
		else if (bossCounter == 1f)
		{
			List<NPC> mainOrbitMembers = Main.npc.Where((NPC n) => n.active && n.type == 267 && n.TryGetAIOverride<CreeperAI>(out var aiInstance) && aiInstance.CachedValue2 == -1).ToList();
			CachedValue1 = (float)Math.PI * 2f / (float)mainOrbitMembers.Count * (float)mainOrbitMembers.IndexOf(base.NPC);
		}
		else if (bossCounter == 0f && Main.netMode != 1)
		{
			AttackPosition = base.NPC.Center;
			AttackAngle = 0f;
			base.NPC.netUpdate = true;
		}
		float dist = BrainOfCthulhuAI.OrbitStandardRadius + (float)Math.Sin(CachedValue1 * 7f + bossCounter / 20f) * 24f;
		if (Main.netMode != 1)
		{
			if (brainAI.AttackList.Contains((byte)base.NPC.whoAmI))
			{
				if (Time < 0)
				{
					Time = 0;
					base.NPC.netUpdate = true;
				}
			}
			else if (Time >= 0)
			{
				Time = -1;
				base.NPC.netUpdate = true;
			}
		}
		if (Time >= 0)
		{
			float telegraphPeriod = (float)BrainOfCthulhuAI.OrbitAttackInterval * 0.25f;
			float shiftPeriod = ((float)BrainOfCthulhuAI.OrbitAttackInterval - telegraphPeriod) / 2f;
			if ((float)Time < telegraphPeriod)
			{
				dist = MathHelper.Lerp(dist, BrainOfCthulhuAI.OrbitTelegraphRadius, CalamityUtils.SineOutEasing((float)Time / telegraphPeriod, 1));
			}
			else if ((float)Time < shiftPeriod + telegraphPeriod)
			{
				dist = MathHelper.Lerp(BrainOfCthulhuAI.OrbitTelegraphRadius, 16f, CalamityUtils.SineInOutEasing(((float)Time - telegraphPeriod) / shiftPeriod, 1));
			}
			else
			{
				dist = MathHelper.Lerp(16f, dist, CalamityUtils.SineInOutEasing(((float)Time - telegraphPeriod - shiftPeriod) / shiftPeriod, 1));
				if (Time > BrainOfCthulhuAI.OrbitAttackInterval)
				{
					Time = -2;
					brain.AIOverride<BrainOfCthulhuAI>().AttackList.Remove((byte)base.NPC.whoAmI);
				}
			}
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f, 0.1f);
			Time++;
		}
		else
		{
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0f, 0.05f);
		}
		float slowDown = 1f - MathHelper.Clamp((bossCounter - (float)BrainOfCthulhuAI.OrbitDuration) / 30f, 0f, 1f);
		AttackAngle += BrainOfCthulhuAI.BaseRotationSpeed * (MathHelper.Lerp(1f, 0.5f, CreeperAmountRatio) + (bocAI.OnSecondCreeperPhase ? 0.75f : 0.5f)) * slowDown * (float)bocAI.AttackSign;
		Vector2 rotation = Vector2.UnitX.RotatedBy(CachedValue1 + AttackAngle) * dist;
		if (bossCounter < (float)BrainOfCthulhuAI.OrbitSetupDuration)
		{
			base.NPC.damage = 0;
			base.NPC.Center = Vector2.Lerp(AttackPosition, bocAI.AttackPosition + rotation, CalamityUtils.SineOutEasing(bossCounter / (float)BrainOfCthulhuAI.OrbitSetupDuration, 1));
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.Center = bocAI.AttackPosition + rotation;
		}
		base.DisableMultiplayerSmoothing = true;
	}

	private void CreeperSpiral()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		int tendrilID = CreeperID % BrainOfCthulhuAI.TendrilCount + 1;
		if (bossCounter == 0f && Main.netMode != 1)
		{
			AttackPosition = base.NPC.Center;
			List<NPC> myGroup = Main.npc.Where((NPC n) => n.active && n.type == 267 && n.ai[0] % (float)BrainOfCthulhuAI.TendrilCount + 1f == (float)tendrilID).ToList();
			CachedValue1 = myGroup.IndexOf(base.NPC);
			CachedValue2 = myGroup.Count;
			AttackAngle = 0f;
			base.NPC.netUpdate = true;
		}
		int index = (int)CachedValue1;
		int groupCount = CachedValue2;
		bool isLast = groupCount > 1 && index == groupCount - 1;
		float placementRatio = (float)(index + 1) / (float)(groupCount + 1);
		float angularVelocity = (bocAI.AttackRotation + (float)Math.PI * 2f / (float)BrainOfCthulhuAI.TendrilCount * (float)tendrilID - AttackAngle) / (16f + placementRatio * 16f);
		AttackAngle += angularVelocity;
		float goalRadius = BrainOfCthulhuAI.TendrilStartDistance + BrainOfCthulhuAI.TendrilLength * placementRatio;
		goalRadius += (float)Math.Sin(bossCounter / 20f) * MathHelper.Lerp(BrainOfCthulhuAI.MaxCreeperSway, 0f, (float)groupCount / (float)(BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath() / 3)) * (float)((index % 2 != 0) ? 1 : (-1));
		Vector2 angleVec = AttackAngle.ToRotationVector2();
		Vector2 position = brain.Center + angleVec * goalRadius;
		if (bossCounter >= (float)BrainOfCthulhuAI.SpiralSetupTime)
		{
			base.NPC.Center = position;
			base.DisableMultiplayerSmoothing = true;
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.knockBackResist = 0f;
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f);
		}
		else if (bossCounter < (float)BrainOfCthulhuAI.SpiralSetupTime / 10f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.66f;
		}
		else
		{
			base.NPC.velocity = Vector2.Zero;
			float lerp = (bossCounter - (float)BrainOfCthulhuAI.SpiralSetupTime / 10f) / ((float)BrainOfCthulhuAI.SpiralSetupTime * 0.9f);
			base.NPC.Center = Vector2.Lerp(AttackPosition, position, CalamityUtils.SineInOutEasing(MathHelper.Clamp(lerp, 0f, 1f), 1));
			base.DisableMultiplayerSmoothing = true;
		}
		if (((Main.dedServ && bossCounter > (float)BrainOfCthulhuAI.SpiralSetupTime) & isLast) && bossCounter % 15f == 0f)
		{
			Projectile.NewProjectileDirect(base.NPC.GetSource_FromThis(), base.NPC.Center, angleVec.RotatedBy(0.2617993950843811) * 18f, 811, BrainOfCthulhuAI.BloodShotDamage, 0f).timeLeft /= 4;
			Projectile.NewProjectileDirect(base.NPC.GetSource_FromThis(), base.NPC.Center, angleVec * 18f, 811, BrainOfCthulhuAI.BloodShotDamage, 0f).timeLeft /= 4;
			Projectile.NewProjectileDirect(base.NPC.GetSource_FromThis(), base.NPC.Center, angleVec.RotatedBy(-0.2617993950843811) * 18f, 811, BrainOfCthulhuAI.BloodShotDamage, 0f).timeLeft /= 4;
		}
	}

	private void CreeperIdle()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0)
		{
			AIState = CreeperAIState.Charge;
			return;
		}
		base.NPC.knockBackResist = 0.72f;
		base.NPC.damage = 0;
		float baseRotation = (float)Math.PI * 2f / ((float)BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath() / 2f) * ((float)CreeperID / 2f);
		Vector2 goalLocation = brain.Center + Vector2.UnitX * (float)(evenID ? (-256) : 256) + Vector2.UnitX.RotatedBy(baseRotation + bossCounter / 60f * (float)((!evenID) ? 1 : (-1))) * 64f;
		if (base.NPC.DistanceSQ(goalLocation) > 9216f)
		{
			NPC nPC = base.NPC;
			nPC.velocity += base.NPC.DirectionTo(goalLocation);
			base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 8f);
		}
		ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 0f);
		Time = -1;
		CachedValue2 = -1;
		AttackPosition = Vector2.Zero;
	}

	private void CreeperCharge()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		Player target = Main.player[CachedValue2];
		if (Time < BrainOfCthulhuAI.CreeperChargePositioningTime)
		{
			Vector2 goalLocation = target.Center + (base.NPC.Center - target.Center).SafeNormalize(-Vector2.UnitY).RotatedBy(evenID ? (-(float)Math.PI / 4f) : ((float)Math.PI / 4f)) * 96f;
			if (base.NPC.DistanceSQ(goalLocation) > 4096f)
			{
				NPC nPC = base.NPC;
				nPC.velocity += base.NPC.DirectionTo(goalLocation);
				base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 12f);
			}
			ConnectionOpacity = BrainOfCthulhuAI.BringOpacityTo(ConnectionOpacity, 1f);
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.knockBackResist = 0f;
			if (Time < BrainOfCthulhuAI.CreeperChargePositioningTime + BrainOfCthulhuAI.CreeperChargeWindUpTime)
			{
				float reelBackSpeedExponent = 2.6f;
				float reelBackCompletion = Utils.GetLerpValue(0f, BrainOfCthulhuAI.CreeperChargeWindUpTime, Time - BrainOfCthulhuAI.CreeperChargePositioningTime, clamped: true);
				float reelBackSpeed = MathHelper.Lerp(2.5f, 16f, MathF.Pow(reelBackCompletion, reelBackSpeedExponent));
				Vector2 reelBackVelocity = base.NPC.DirectionTo(target.Center) * (0f - reelBackSpeed);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity, 0.25f);
				if (Time == BrainOfCthulhuAI.CreeperChargePositioningTime + BrainOfCthulhuAI.CreeperChargeWindUpTime - 5)
				{
					SoundEngine.PlaySound(in SoundID.DD2_MonkStaffSwing, base.NPC.Center);
				}
			}
			else if (Time == BrainOfCthulhuAI.CreeperChargePositioningTime + BrainOfCthulhuAI.CreeperChargeWindUpTime)
			{
				base.NPC.velocity = base.NPC.DirectionTo(target.Center) * 24f;
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.975f;
				if (Time >= BrainOfCthulhuAI.CreeperChargePositioningTime + BrainOfCthulhuAI.CreeperChargeWindUpTime + 30)
				{
					base.NPC.damage = 0;
					Time = -10;
					AIState = CreeperAIState.Idle;
					return;
				}
			}
		}
		Time++;
	}

	public override void SendExtraAI(BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		binaryWriter.Write(Time);
		binaryWriter.Write(CachedValue2);
		binaryWriter.Write(PartnerIndex);
		binaryWriter.WritePackedWorldPosition(AttackPosition);
	}

	public override void ReceiveExtraAI(BitReader bitReader, BinaryReader binaryReader)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Time = binaryReader.ReadInt32();
		CachedValue2 = binaryReader.ReadInt32();
		PartnerIndex = binaryReader.ReadInt32();
		AttackPosition = binaryReader.ReadPackedWorldPosition();
	}

	public override void HitEffect(Mod mod, NPC.HitInfo hit)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && base.NPC.life <= 0)
		{
			List<VerletSimulatedSegment> verletTendril = BrainOfCthulhuSystem.VerletTendrils[CreeperID].tendril;
			verletTendril[verletTendril.Count - 1].position = base.NPC.Center;
			verletTendril[verletTendril.Count - 1].oldPosition = base.NPC.Center;
			verletTendril[verletTendril.Count - 1].locked = false;
			for (int i = 0; i < 5; i++)
			{
				Vector2 dir = (verletTendril[verletTendril.Count - 1].position - verletTendril[verletTendril.Count - 2].position).SafeNormalize(Vector2.unitYVector);
				GeneralParticleHandler.SpawnParticle(new BloodParticle(verletTendril[verletTendril.Count - 2].position, dir.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 10f, (float)Math.PI / 10f)) * Main.rand.NextFloat(6f, 10f), 24, Main.rand.NextFloat(0.5f, 1f), Color.Yellow * 0.75f));
			}
		}
	}

	public override bool PreDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		return false;
	}

	public CreeperAI()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Time = -1;
		PartnerIndex = -1;
		AttackPosition = Vector2.Zero;
		useBossAIState = true;
		base._002Ector();
	}
}
