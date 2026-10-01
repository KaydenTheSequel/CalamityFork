using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class DestroyerAI : VanillaAIOverride
{
	public class ProbeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_020d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Unknown result type (might be due to invalid IL or missing references)
			//IL_0278: Unknown result type (might be due to invalid IL or missing references)
			//IL_027d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0401: Unknown result type (might be due to invalid IL or missing references)
			//IL_0406: Unknown result type (might be due to invalid IL or missing references)
			//IL_0408: Unknown result type (might be due to invalid IL or missing references)
			//IL_040d: Unknown result type (might be due to invalid IL or missing references)
			//IL_040f: Unknown result type (might be due to invalid IL or missing references)
			//IL_041b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0420: Unknown result type (might be due to invalid IL or missing references)
			//IL_0425: Unknown result type (might be due to invalid IL or missing references)
			//IL_042d: Unknown result type (might be due to invalid IL or missing references)
			//IL_043c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0441: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0703: Unknown result type (might be due to invalid IL or missing references)
			//IL_076f: Unknown result type (might be due to invalid IL or missing references)
			//IL_077f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0800: Unknown result type (might be due to invalid IL or missing references)
			//IL_0810: Unknown result type (might be due to invalid IL or missing references)
			//IL_081e: Unknown result type (might be due to invalid IL or missing references)
			//IL_082e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0731: Unknown result type (might be due to invalid IL or missing references)
			//IL_0748: Unknown result type (might be due to invalid IL or missing references)
			//IL_057e: Unknown result type (might be due to invalid IL or missing references)
			//IL_059a: Unknown result type (might be due to invalid IL or missing references)
			//IL_05be: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05de: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0604: Unknown result type (might be due to invalid IL or missing references)
			//IL_060e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0613: Unknown result type (might be due to invalid IL or missing references)
			//IL_0618: Unknown result type (might be due to invalid IL or missing references)
			//IL_061d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0627: Unknown result type (might be due to invalid IL or missing references)
			//IL_062c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0876: Unknown result type (might be due to invalid IL or missing references)
			//IL_087b: Unknown result type (might be due to invalid IL or missing references)
			//IL_087d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0885: Unknown result type (might be due to invalid IL or missing references)
			//IL_088a: Unknown result type (might be due to invalid IL or missing references)
			//IL_088f: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0662: Unknown result type (might be due to invalid IL or missing references)
			//IL_0666: Unknown result type (might be due to invalid IL or missing references)
			//IL_066b: Unknown result type (might be due to invalid IL or missing references)
			//IL_067f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0684: Unknown result type (might be due to invalid IL or missing references)
			//IL_0686: Unknown result type (might be due to invalid IL or missing references)
			//IL_068b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0695: Unknown result type (might be due to invalid IL or missing references)
			//IL_069a: Unknown result type (might be due to invalid IL or missing references)
			//IL_069f: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			NPCAimedTarget targetData = base.NPC.GetTargetData();
			bool targetDead = false;
			if (targetData.Type == NPCTargetType.Player)
			{
				targetDead = Main.player[base.NPC.target].dead;
			}
			float velocity = (death ? 8.15f : 7.2f);
			float acceleration = (death ? 0.07f : 0.06f);
			float deceleration = 1f - acceleration;
			if (targetDead)
			{
				Vector2 destination = base.NPC.Center - Vector2.UnitY;
				Vector2 idealVelocity = base.NPC.SafeDirectionTo(destination) * velocity * 0.5f;
				idealVelocity.X *= base.NPC.direction;
				idealVelocity.Y *= 2.5f;
				base.NPC.SimpleFlyMovement(idealVelocity, acceleration);
				base.NPC.EncourageDespawn(10);
			}
			else if (base.NPC.Distance(targetData.Center) > 400f)
			{
				Vector2 idealVelocity2 = base.NPC.SafeDirectionTo(targetData.Center) * velocity;
				base.NPC.SimpleFlyMovement(idealVelocity2, acceleration);
			}
			else if (base.NPC.Distance(targetData.Center) < 160f)
			{
				Vector2 idealVelocity3 = base.NPC.SafeDirectionTo(targetData.Center) * velocity;
				base.NPC.SimpleFlyMovement(-idealVelocity3, acceleration);
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= deceleration;
			}
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i != base.NPC.whoAmI && Main.npc[i].active && Main.npc[i].type == base.NPC.type)
				{
					Vector2 otherProbeDist = Main.npc[i].Center - base.NPC.Center;
					if (((Vector2)(ref otherProbeDist)).Length() < (float)(base.NPC.width + base.NPC.height))
					{
						otherProbeDist = otherProbeDist.SafeNormalize(Vector2.UnitY);
						otherProbeDist *= -0.1f;
						NPC nPC2 = base.NPC;
						nPC2.velocity += otherProbeDist;
						NPC obj = Main.npc[i];
						obj.velocity -= otherProbeDist;
					}
				}
			}
			if (base.NPC.ai[3] != 0f)
			{
				if (NPC.IsMechQueenUp)
				{
					NPC nPC3 = Main.npc[NPC.mechQueen];
					Vector2 tileConvertedPosition = default(Vector2);
					((Vector2)(ref tileConvertedPosition))._002Ector(26f * base.NPC.ai[3], 0f);
					int mechdusaProbe = (int)base.NPC.ai[2];
					if (mechdusaProbe < 0 || mechdusaProbe >= Main.maxNPCs)
					{
						mechdusaProbe = NPC.FindFirstNPC(134);
						base.NPC.ai[2] = mechdusaProbe;
						base.NPC.netUpdate = true;
					}
					if (mechdusaProbe > -1)
					{
						NPC nPC4 = Main.npc[mechdusaProbe];
						if (!nPC4.active || nPC4.type != 134)
						{
							base.NPC.dontTakeDamage = false;
							if (base.NPC.ai[3] > 0f)
							{
								base.NPC.netUpdate = true;
							}
							base.NPC.ai[3] = 0f;
						}
						else
						{
							Vector2 spinningpoint = nPC4.Center + tileConvertedPosition;
							spinningpoint = spinningpoint.RotatedBy(nPC4.rotation, nPC4.Center);
							base.NPC.Center = spinningpoint;
							base.NPC.velocity = nPC3.velocity;
							base.NPC.dontTakeDamage = true;
						}
					}
					else
					{
						base.NPC.dontTakeDamage = false;
						if (base.NPC.ai[3] > 0f)
						{
							base.NPC.netUpdate = true;
						}
						base.NPC.ai[3] = 0f;
					}
				}
				else
				{
					base.NPC.dontTakeDamage = false;
					if (base.NPC.ai[3] > 0f)
					{
						base.NPC.netUpdate = true;
					}
					base.NPC.ai[3] = 0f;
				}
			}
			else
			{
				base.NPC.dontTakeDamage = false;
			}
			base.NPC.localAI[0]++;
			if ((base.NPC.justHit && !death) | targetDead)
			{
				base.NPC.localAI[0] = 0f;
			}
			float laserGateValue = (NPC.IsMechQueenUp ? 360f : 240f);
			if (Main.netMode != 1 && base.NPC.localAI[0] >= laserGateValue)
			{
				base.NPC.localAI[0] = 0f;
				if (targetData.Type != NPCTargetType.None && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, targetData.Position, targetData.Width, targetData.Height))
				{
					int type = 84;
					int totalProjectiles = 1;
					Vector2 projectileVelocity = (targetData.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity;
					if (NPC.IsMechQueenUp)
					{
						projectileVelocity = (targetData.Center - base.NPC.Center - targetData.Velocity * 20f).SafeNormalize(Vector2.UnitY) * 8f;
					}
					for (int j = 0; j < totalProjectiles; j++)
					{
						float velocityMultiplier = 1f;
						switch (j)
						{
						case 1:
							velocityMultiplier = 0.95f;
							break;
						case 2:
							velocityMultiplier = 0.9f;
							break;
						}
						Vector2 laserVelocity = projectileVelocity * velocityMultiplier;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserVelocity.SafeNormalize(Vector2.UnitY) * 50f, laserVelocity, type, ProbeLaserDamage.CalculateMechDamage(), 0f, Main.myPlayer);
					}
					base.NPC.netUpdate = true;
				}
			}
			int x = (int)base.NPC.Center.X / 16;
			int y = (int)base.NPC.Center.Y / 16;
			if (WorldGen.InWorld(x, y) && !WorldGen.SolidTile(x, y))
			{
				Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.3f, 0.1f, 0.05f);
			}
			if (targetData.Center.X - base.NPC.Center.X > 0f)
			{
				base.NPC.spriteDirection = 1;
				base.NPC.rotation = (float)Math.Atan2(targetData.Center.Y - base.NPC.Center.Y, targetData.Center.X - base.NPC.Center.X);
			}
			else
			{
				base.NPC.spriteDirection = -1;
				base.NPC.rotation = (float)Math.Atan2(targetData.Center.Y - base.NPC.Center.Y, targetData.Center.X - base.NPC.Center.X) + (float)Math.PI;
			}
			if (NPC.IsMechQueenUp && base.NPC.ai[2] == 0f)
			{
				Vector2 center = base.NPC.GetTargetData().Center;
				Vector2 v2 = center - base.NPC.Center;
				if (((Vector2)(ref v2)).Length() < 120f)
				{
					base.NPC.Center = center - v2.SafeNormalize(Vector2.UnitY) * 120f;
				}
			}
			if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
			{
				base.NPC.netUpdate = true;
			}
			return false;
		}
	}

	public const float DRIncreaseTime = 600f;

	public const float LaserTelegraphTime = 120f;

	public const float SparkTelegraphTime = 30f;

	public const float FlightPhaseGateValue = 900f;

	public const float FlightPhaseResetGateValue = 1800f;

	private const float Phase4FlightPhaseTimerSetValue = 450f;

	private const float Phase5FlightPhaseTimerSetValue = 900f;

	public const float PhaseTransitionTelegraphTime = 180f;

	public const float GroundTelegraphStartGateValue = 1620f;

	public const float FlightTelegraphStartGateValue = 720f;

	public const float ProbeLaserGateValue_Mechdusa = 360f;

	public const float ProbeLaserGateValue_Rev = 240f;

	public const float ProbeLaserGateValue = 120f;

	public const float ProbeLaserTelegraphTime = 60f;

	public static int ProbeLaserDamage = 22;

	public static int LaserDamage = 25;

	public sbyte LaserColor = -1;

	public override void SendExtraAI(BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		binaryWriter.Write(LaserColor);
	}

	public override void ReceiveExtraAI(BitReader bitReader, BinaryReader binaryReader)
	{
		LaserColor = binaryReader.ReadSByte();
	}

	public override bool AI(Mod mod)
	{
		//IL_164c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1651: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2426: Unknown result type (might be due to invalid IL or missing references)
		//IL_2431: Unknown result type (might be due to invalid IL or missing references)
		//IL_2436: Unknown result type (might be due to invalid IL or missing references)
		//IL_243b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2356: Unknown result type (might be due to invalid IL or missing references)
		//IL_235b: Unknown result type (might be due to invalid IL or missing references)
		//IL_236e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2370: Unknown result type (might be due to invalid IL or missing references)
		//IL_2372: Unknown result type (might be due to invalid IL or missing references)
		//IL_2377: Unknown result type (might be due to invalid IL or missing references)
		//IL_238d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2392: Unknown result type (might be due to invalid IL or missing references)
		//IL_2394: Unknown result type (might be due to invalid IL or missing references)
		//IL_2399: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_23bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2303: Unknown result type (might be due to invalid IL or missing references)
		//IL_2308: Unknown result type (might be due to invalid IL or missing references)
		//IL_230d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2312: Unknown result type (might be due to invalid IL or missing references)
		//IL_2319: Unknown result type (might be due to invalid IL or missing references)
		//IL_231e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1236: Unknown result type (might be due to invalid IL or missing references)
		//IL_123b: Unknown result type (might be due to invalid IL or missing references)
		//IL_123f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1254: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1270: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_1279: Unknown result type (might be due to invalid IL or missing references)
		//IL_127b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1354: Unknown result type (might be due to invalid IL or missing references)
		//IL_135b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_144c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1450: Unknown result type (might be due to invalid IL or missing references)
		//IL_145e: Unknown result type (might be due to invalid IL or missing references)
		//IL_148a: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_106e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1087: Unknown result type (might be due to invalid IL or missing references)
		//IL_1531: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_154c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1556: Unknown result type (might be due to invalid IL or missing references)
		//IL_1574: Unknown result type (might be due to invalid IL or missing references)
		//IL_157e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_194c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab2: Unknown result type (might be due to invalid IL or missing references)
		int mechdusaCurvedSpineSegmentIndex = 0;
		int mechdusaCurvedSpineSegments = 10;
		if (NPC.IsMechQueenUp && base.NPC.type != 134)
		{
			int mechdusaIndex = (int)base.NPC.ai[1];
			while (mechdusaIndex > 0 && mechdusaIndex < Main.maxNPCs)
			{
				if (Main.npc[mechdusaIndex].active && Main.npc[mechdusaIndex].type >= 134 && Main.npc[mechdusaIndex].type <= 136)
				{
					mechdusaCurvedSpineSegmentIndex++;
					if (Main.npc[mechdusaIndex].type == 134)
					{
						break;
					}
					if (mechdusaCurvedSpineSegmentIndex >= mechdusaCurvedSpineSegments)
					{
						mechdusaCurvedSpineSegmentIndex = 0;
						break;
					}
					mechdusaIndex = (int)Main.npc[mechdusaIndex].ai[1];
					continue;
				}
				mechdusaCurvedSpineSegmentIndex = 0;
				break;
			}
		}
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = calamityGlobalNPC.newAI[1] < 600f;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (lifeRatio < 0.85f) | death;
		bool phase3 = (lifeRatio < 0.7f) | death;
		bool startFlightPhase = lifeRatio < 0.5f;
		bool phase4 = lifeRatio < (death ? 0.4f : 0.25f);
		bool phase5 = lifeRatio < (death ? 0.2f : 0.1f);
		if (startFlightPhase)
		{
			calamityGlobalNPC.newAI[3]++;
		}
		float flightPhaseTimerSetValue = (phase5 ? 900f : (phase4 ? 450f : 0f));
		if (calamityGlobalNPC.newAI[3] < flightPhaseTimerSetValue)
		{
			calamityGlobalNPC.newAI[3] = flightPhaseTimerSetValue;
		}
		if (calamityGlobalNPC.newAI[3] >= 1800f)
		{
			calamityGlobalNPC.newAI[3] = flightPhaseTimerSetValue;
		}
		bool hasSpawnDR = calamityGlobalNPC.newAI[1] < 600f && calamityGlobalNPC.newAI[1] > 60f;
		float phaseTransitionColorAmount = ((hasSpawnDR | phase5) ? 1f : 0f);
		if (!hasSpawnDR && !phase5)
		{
			if (calamityGlobalNPC.newAI[3] >= 1620f)
			{
				phaseTransitionColorAmount = MathHelper.Clamp(1f - (calamityGlobalNPC.newAI[3] - 1620f) / 180f, 0f, 1f);
			}
			else if (calamityGlobalNPC.newAI[3] >= 720f)
			{
				phaseTransitionColorAmount = MathHelper.Clamp((calamityGlobalNPC.newAI[3] - 720f) / 180f, 0f, 1f);
			}
		}
		if (base.NPC.ai[3] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[3];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		Player player = Main.player[base.NPC.target];
		bool increaseSpeed = Vector2.Distance(player.Center, base.NPC.Center) > 3200f;
		bool increaseSpeedMore = Vector2.Distance(player.Center, base.NPC.Center) > 5600f;
		bool flyAtTarget = ((calamityGlobalNPC.newAI[3] >= 900f) & startFlightPhase) | hasSpawnDR;
		Color newColor;
		if (base.NPC.type == 134 || (base.NPC.type != 134 && Main.npc[(int)base.NPC.ai[1]].alpha < 128))
		{
			if (base.NPC.alpha != 0)
			{
				for (int i = 0; i < 2; i++)
				{
					Vector2 position = base.NPC.position;
					int width = base.NPC.width;
					int height = base.NPC.height;
					newColor = default(Color);
					int spawnDust = Dust.NewDust(position, width, height, 182, 0f, 0f, 100, newColor, 2f);
					Main.dust[spawnDust].noGravity = true;
					Main.dust[spawnDust].noLight = true;
				}
			}
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (base.NPC.type > 134)
		{
			bool shouldDespawn = true;
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				if (Main.npc[j].active && Main.npc[j].type == 134)
				{
					shouldDespawn = false;
					break;
				}
			}
			if (!shouldDespawn)
			{
				if (base.NPC.ai[1] <= 0f)
				{
					shouldDespawn = true;
				}
				else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
				{
					shouldDespawn = true;
				}
			}
			if (shouldDespawn)
			{
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				base.NPC.active = false;
			}
		}
		int totalSegments = (Main.getGoodWorld ? 100 : 80);
		float brokenSegmentAggressionMultiplier = 1f;
		if (base.NPC.type == 134)
		{
			int numProbeSegments = 0;
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				if (Main.npc[k].active && Main.npc[k].type == 135 && Main.npc[k].ai[2] == 0f)
				{
					numProbeSegments++;
				}
			}
			brokenSegmentAggressionMultiplier += (1f - MathHelper.Clamp((float)numProbeSegments / (float)totalSegments, 0f, 1f)) * 0.25f;
		}
		int noFlyZoneBoxHeight = (death ? 1500 : 1800);
		float speed = (death ? 0.12f : 0.1f);
		float turnSpeed = (death ? 0.18f : 0.15f);
		float segmentVelocity = (flyAtTarget ? 15f : 20f);
		float velocityMultiplier = (increaseSpeedMore ? 2f : (increaseSpeed ? 1.5f : 1f));
		noFlyZoneBoxHeight -= (death ? 400 : ((int)(400f * (1f - lifeRatio))));
		float segmentVelocityBoost = (death ? ((flyAtTarget ? 4f : 5.25f) * (1f - lifeRatio)) : ((flyAtTarget ? 3f : 4f) * (1f - lifeRatio)));
		float speedBoost = (death ? ((flyAtTarget ? 0.1f : 0.135f) * (1f - lifeRatio)) : ((flyAtTarget ? 0.075f : 0.1f) * (1f - lifeRatio)));
		float turnSpeedBoost = (death ? (0.15f * (1f - lifeRatio)) : (0.12f * (1f - lifeRatio)));
		segmentVelocity += segmentVelocityBoost;
		speed += speedBoost;
		turnSpeed += turnSpeedBoost;
		if (flyAtTarget)
		{
			float speedMultiplier = (phase5 ? 1.8f : (phase4 ? 1.65f : 1.5f));
			speed *= speedMultiplier;
		}
		segmentVelocity *= velocityMultiplier;
		speed *= velocityMultiplier;
		turnSpeed *= velocityMultiplier;
		segmentVelocity *= brokenSegmentAggressionMultiplier;
		speed *= brokenSegmentAggressionMultiplier;
		turnSpeed *= brokenSegmentAggressionMultiplier;
		if (Main.getGoodWorld)
		{
			segmentVelocity *= 1.2f;
			speed *= 1.2f;
			turnSpeed *= 1.2f;
		}
		bool probeLaunched = base.NPC.ai[2] == 1f;
		if (base.NPC.type == 134 && base.NPC.ai[0] == 0f && Main.netMode != 1)
		{
			base.NPC.ai[3] = base.NPC.whoAmI;
			base.NPC.realLife = base.NPC.whoAmI;
			int index = base.NPC.whoAmI;
			for (int l = 0; l <= totalSegments; l++)
			{
				int type = 135;
				if (l == totalSegments)
				{
					type = 136;
				}
				int segment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.position.Y + (float)base.NPC.height), type, base.NPC.whoAmI);
				Main.npc[segment].ai[3] = base.NPC.whoAmI;
				Main.npc[segment].realLife = base.NPC.whoAmI;
				Main.npc[segment].ai[1] = index;
				Main.npc[index].ai[0] = segment;
				Main.npc[index].Calamity().newAI[0] = -90f - Main.npc[index].ai[0] * (death ? 8f : 3f);
				NetMessage.SendData(23, -1, -1, null, segment);
				index = segment;
			}
		}
		if (base.NPC.type == 135)
		{
			bool ableToFireLaser = LaserColor != -1;
			if (LaserColor == -1 && !probeLaunched)
			{
				int random = (phase3 ? 4 : (phase2 ? 3 : 2));
				switch (Main.rand.Next(random))
				{
				case 0:
				case 1:
					LaserColor = 0;
					break;
				case 2:
					LaserColor = 1;
					break;
				case 3:
					LaserColor = 2;
					break;
				}
				base.NPC.netUpdate = true;
			}
			if (probeLaunched & ableToFireLaser)
			{
				LaserColor = -1;
				base.NPC.netUpdate = true;
			}
			float shootProjectileTime = ((!death) ? 450f : (phase5 ? 350f : (phase4 ? 400f : 450f)));
			if (ableToFireLaser)
			{
				calamityGlobalNPC.newAI[0]++;
			}
			if (Main.netMode != 1 && ((calamityGlobalNPC.newAI[0] % 20f == 10f) & ableToFireLaser))
			{
				base.NPC.SyncExtraAI();
			}
			Color telegraphColor = Color.Transparent;
			switch (LaserColor)
			{
			case 0:
				telegraphColor = Color.Red;
				break;
			case 1:
				telegraphColor = Color.Green;
				break;
			case 2:
				telegraphColor = Color.Cyan;
				break;
			}
			if (calamityGlobalNPC.newAI[0] == shootProjectileTime - 120f)
			{
				GeneralParticleHandler.SpawnParticle(new DestroyerReticleTelegraph(base.NPC, telegraphColor, 1.5f, 0.15f, 120));
			}
			if (calamityGlobalNPC.newAI[0] == shootProjectileTime - 30f)
			{
				GeneralParticleHandler.SpawnParticle(new DestroyerSparkTelegraph(base.NPC, telegraphColor * 2f, Color.White, 3f, 30, Main.rand.NextFloat(MathHelper.ToRadians(3f)) * (float)Main.rand.NextBool().ToDirectionInt()));
			}
			if ((calamityGlobalNPC.newAI[0] >= shootProjectileTime) & ableToFireLaser)
			{
				int numProbeSegments2 = 0;
				for (int m = 0; m < Main.maxNPCs; m++)
				{
					if (Main.npc[m].active && Main.npc[m].type == base.NPC.type && Main.npc[m].ai[2] == 0f)
					{
						numProbeSegments2++;
					}
				}
				float lerpAmount = MathHelper.Clamp((float)numProbeSegments2 / (float)totalSegments, 0f, 1f);
				float laserShootTimeBonus = (int)MathHelper.Lerp(0f, shootProjectileTime - 120f, 1f - lerpAmount);
				if (!death)
				{
					if ((base.NPC.localAI[3] == 0f) & phase3)
					{
						base.NPC.localAI[3] = 1f;
						base.NPC.SyncVanillaLocalAI();
						laserShootTimeBonus -= base.NPC.ai[0] * 2f;
					}
					else if ((base.NPC.localAI[3] == 1f) & startFlightPhase)
					{
						base.NPC.localAI[3] = 2f;
						base.NPC.SyncVanillaLocalAI();
						laserShootTimeBonus -= base.NPC.ai[0] * 2f;
					}
				}
				calamityGlobalNPC.newAI[0] = laserShootTimeBonus;
				base.NPC.SyncExtraAI();
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					float projectileSpeed = (death ? 5f : 4f);
					int projectileType = 100;
					switch (LaserColor)
					{
					case 1:
						projectileType = ModContent.ProjectileType<DestroyerCursedLaser>();
						break;
					case 2:
						projectileType = ModContent.ProjectileType<DestroyerElectricLaser>();
						break;
					}
					Vector2 projectileVelocity = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * projectileSpeed;
					Vector2 projectileSpawn = base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 100f;
					if (Main.netMode != 1)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, projectileVelocity, projectileType, LaserDamage.CalculateMechDamage(), 0f, Main.myPlayer, 1f);
						Main.projectile[proj].timeLeft = 1200;
					}
					base.NPC.netUpdate = true;
					if (death)
					{
						LaserColor = -1;
						base.NPC.netUpdate = true;
					}
				}
				if (!death)
				{
					LaserColor = -1;
					base.NPC.netUpdate = true;
				}
			}
		}
		if (base.NPC.type == 134)
		{
			if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
			{
				base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
			}
		}
		else if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		int tilePosX = (int)(base.NPC.position.X / 16f) - 1;
		int tileWidthPosX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
		int tilePosY = (int)(base.NPC.position.Y / 16f) - 1;
		int tileWidthPosY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
		if (tilePosX < 0)
		{
			tilePosX = 0;
		}
		if (tileWidthPosX > Main.maxTilesX)
		{
			tileWidthPosX = Main.maxTilesX;
		}
		if (tilePosY < 0)
		{
			tilePosY = 0;
		}
		if (tileWidthPosY > Main.maxTilesY)
		{
			tileWidthPosY = Main.maxTilesY;
		}
		bool shouldFly = flyAtTarget;
		if (!shouldFly)
		{
			Vector2 tileConvertedPosition = default(Vector2);
			for (int n = tilePosX; n < tileWidthPosX; n++)
			{
				for (int num = tilePosY; num < tileWidthPosY; num++)
				{
					if (Main.tile[n, num] != null && ((Main.tile[n, num].HasUnactuatedTile && (Main.tileSolid[Main.tile[n, num].TileType] || (Main.tileSolidTop[Main.tile[n, num].TileType] && Main.tile[n, num].TileFrameY == 0))) || Main.tile[n, num].LiquidAmount > 64))
					{
						tileConvertedPosition.X = n * 16;
						tileConvertedPosition.Y = num * 16;
						if (base.NPC.position.X + (float)base.NPC.width > tileConvertedPosition.X && base.NPC.position.X < tileConvertedPosition.X + 16f && base.NPC.position.Y + (float)base.NPC.height > tileConvertedPosition.Y && base.NPC.position.Y < tileConvertedPosition.Y + 16f)
						{
							shouldFly = true;
							break;
						}
					}
				}
			}
		}
		if (!shouldFly)
		{
			base.NPC.localAI[1] = 1f;
			if (base.NPC.type == 134)
			{
				Rectangle rectangle = default(Rectangle);
				((Rectangle)(ref rectangle))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
				int noFlyZone = 1000;
				bool outsideNoFlyZone = true;
				if (base.NPC.position.Y > player.position.Y)
				{
					Rectangle noFlyRectangle = default(Rectangle);
					for (int num2 = 0; num2 < 255; num2++)
					{
						if (Main.player[num2].active)
						{
							((Rectangle)(ref noFlyRectangle))._002Ector((int)Main.player[num2].position.X - noFlyZone, (int)Main.player[num2].position.Y - noFlyZone, noFlyZone * 2, noFlyZoneBoxHeight);
							if (((Rectangle)(ref rectangle)).Intersects(noFlyRectangle))
							{
								outsideNoFlyZone = false;
								break;
							}
						}
					}
					if (outsideNoFlyZone)
					{
						shouldFly = true;
					}
				}
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		if (base.NPC.type != 135 || !probeLaunched)
		{
			newColor = Color.Red;
			((Color)(ref newColor)).ToVector3();
			Vector3 val = new Vector3(0.3f, 0.1f, 0.05f);
			Vector3 flightColor = default(Vector3);
			((Vector3)(ref flightColor))._002Ector(0.05f, 0.1f, 0.3f);
			Vector3 segmentColor = Vector3.Lerp(val, flightColor, phaseTransitionColorAmount);
			Vector3 telegraphColor2 = val;
			float telegraphProgress = 0f;
			if (LaserColor != -1 && base.NPC.type == 135)
			{
				float telegraphGateValue = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 400f : 450f) - 120f;
				if (calamityGlobalNPC.newAI[0] > telegraphGateValue)
				{
					switch (LaserColor)
					{
					case 1:
						((Vector3)(ref telegraphColor2))._002Ector(0.1f, 0.3f, 0.05f);
						break;
					case 2:
						((Vector3)(ref telegraphColor2))._002Ector(0.05f, 0.2f, 0.2f);
						break;
					}
					telegraphProgress = MathHelper.Clamp((calamityGlobalNPC.newAI[0] - telegraphGateValue) / 120f, 0f, 1f);
				}
			}
			Lighting.AddLight(base.NPC.Center, Vector3.Lerp(segmentColor, telegraphColor2 * 2f, telegraphProgress));
		}
		if ((player.dead || Main.IsItDay()) && !BossRushEvent.BossRushActive)
		{
			shouldFly = false;
			base.NPC.velocity.Y += 2f;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y += 2f;
				segmentVelocity *= 2f;
			}
			if ((double)base.NPC.position.Y > Main.rockLayer * 16.0)
			{
				for (int num3 = 0; num3 < Main.maxNPCs; num3++)
				{
					if (Main.npc[num3].aiStyle == base.NPC.aiStyle)
					{
						Main.npc[num3].active = false;
					}
				}
			}
		}
		Vector2 npcCenter = base.NPC.Center;
		float targetTilePosX = player.Center.X;
		float targetTilePosY = player.Center.Y;
		targetTilePosX = (int)(targetTilePosX / 16f) * 16;
		targetTilePosY = (int)(targetTilePosY / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		targetTilePosX -= npcCenter.X;
		targetTilePosY -= npcCenter.Y;
		float targetTileDist = (float)Math.Sqrt(targetTilePosX * targetTilePosX + targetTilePosY * targetTilePosY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			int mechdusaSegmentScale = (int)(44f * base.NPC.scale);
			try
			{
				npcCenter = base.NPC.Center;
				targetTilePosX = Main.npc[(int)base.NPC.ai[1]].Center.X - npcCenter.X;
				targetTilePosY = Main.npc[(int)base.NPC.ai[1]].Center.Y - npcCenter.Y;
			}
			catch
			{
			}
			if (mechdusaCurvedSpineSegmentIndex > 0)
			{
				float absoluteTilePosX = (float)mechdusaSegmentScale - (float)mechdusaSegmentScale * (((float)mechdusaCurvedSpineSegmentIndex - 1f) * 0.1f);
				if (absoluteTilePosX < 0f)
				{
					absoluteTilePosX = 0f;
				}
				if (absoluteTilePosX > (float)mechdusaSegmentScale)
				{
					absoluteTilePosX = mechdusaSegmentScale;
				}
				targetTilePosY = Main.npc[(int)base.NPC.ai[1]].Center.Y + absoluteTilePosX - npcCenter.Y;
			}
			base.NPC.rotation = (float)Math.Atan2(targetTilePosY, targetTilePosX) + (float)Math.PI / 2f;
			targetTileDist = (float)Math.Sqrt(targetTilePosX * targetTilePosX + targetTilePosY * targetTilePosY);
			if (mechdusaCurvedSpineSegmentIndex > 0)
			{
				mechdusaSegmentScale = mechdusaSegmentScale / mechdusaCurvedSpineSegments * mechdusaCurvedSpineSegmentIndex;
			}
			targetTileDist = (targetTileDist - (float)mechdusaSegmentScale) / targetTileDist;
			targetTilePosX *= targetTileDist;
			targetTilePosY *= targetTileDist;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X += targetTilePosX;
			base.NPC.position.Y += targetTilePosY;
		}
		else
		{
			if (!shouldFly)
			{
				base.NPC.velocity.Y += 0.15f;
				if (death && base.NPC.velocity.Y > 0f && Math.Abs(base.NPC.Center.Y - player.Center.Y) > 360f)
				{
					base.NPC.velocity.Y += 0.05f;
				}
				if (base.NPC.velocity.Y > segmentVelocity)
				{
					base.NPC.velocity.Y = segmentVelocity;
				}
				bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > speed;
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.4)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X -= speed * 1.1f;
					}
					else
					{
						base.NPC.velocity.X += speed * 1.1f;
					}
				}
				else if (base.NPC.velocity.Y == segmentVelocity)
				{
					if (slowXVelocity)
					{
						if (base.NPC.velocity.X < targetTilePosX)
						{
							base.NPC.velocity.X += speed;
						}
						else if (base.NPC.velocity.X > targetTilePosX)
						{
							base.NPC.velocity.X -= speed;
						}
					}
					else
					{
						base.NPC.velocity.X = 0f;
					}
				}
				else if (base.NPC.velocity.Y > 4f)
				{
					if (slowXVelocity)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X += speed * 0.9f;
						}
						else
						{
							base.NPC.velocity.X -= speed * 0.9f;
						}
					}
					else
					{
						base.NPC.velocity.X = 0f;
					}
				}
			}
			else
			{
				if (base.NPC.soundDelay == 0)
				{
					float soundDelay = targetTileDist / 40f;
					if (soundDelay < 10f)
					{
						soundDelay = 10f;
					}
					if (soundDelay > 20f)
					{
						soundDelay = 20f;
					}
					base.NPC.soundDelay = (int)soundDelay;
					SoundEngine.PlaySound(in SoundID.WormDig, base.NPC.Center);
				}
				targetTileDist = (float)Math.Sqrt(targetTilePosX * targetTilePosX + targetTilePosY * targetTilePosY);
				float absoluteTilePosX2 = Math.Abs(targetTilePosX);
				float absoluteTilePosY = Math.Abs(targetTilePosY);
				float tileToReachTarget = segmentVelocity / targetTileDist;
				targetTilePosX *= tileToReachTarget;
				targetTilePosY *= tileToReachTarget;
				bool flyWyvernMovement = false;
				if (flyAtTarget)
				{
					float chargeDistance = 600f;
					if (((base.NPC.velocity.X > 0f && targetTilePosX < 0f) || (base.NPC.velocity.X < 0f && targetTilePosX > 0f) || (base.NPC.velocity.Y > 0f && targetTilePosY < 0f) || (base.NPC.velocity.Y < 0f && targetTilePosY > 0f)) && Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) > speed / 2f && targetTileDist < chargeDistance)
					{
						flyWyvernMovement = true;
						if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < segmentVelocity)
						{
							NPC nPC = base.NPC;
							nPC.velocity *= 1.1f;
						}
					}
					if (base.NPC.position.Y > player.position.Y)
					{
						flyWyvernMovement = true;
						if (Math.Abs(base.NPC.velocity.X) < segmentVelocity / 2f)
						{
							if (base.NPC.velocity.X == 0f)
							{
								base.NPC.velocity.X -= base.NPC.direction;
							}
							base.NPC.velocity.X *= 1.1f;
						}
						else if (base.NPC.velocity.Y > 0f - segmentVelocity)
						{
							base.NPC.velocity.Y -= speed;
						}
					}
				}
				if (!flyWyvernMovement)
				{
					if (!flyAtTarget && ((base.NPC.velocity.X > 0f && targetTilePosX > 0f) || (base.NPC.velocity.X < 0f && targetTilePosX < 0f)) && ((base.NPC.velocity.Y > 0f && targetTilePosY > 0f) || (base.NPC.velocity.Y < 0f && targetTilePosY < 0f)))
					{
						if (base.NPC.velocity.X < targetTilePosX)
						{
							base.NPC.velocity.X += turnSpeed;
						}
						else if (base.NPC.velocity.X > targetTilePosX)
						{
							base.NPC.velocity.X -= turnSpeed;
						}
						if (base.NPC.velocity.Y < targetTilePosY)
						{
							base.NPC.velocity.Y += turnSpeed;
						}
						else if (base.NPC.velocity.Y > targetTilePosY)
						{
							base.NPC.velocity.Y -= turnSpeed;
						}
					}
					if ((base.NPC.velocity.X > 0f && targetTilePosX > 0f) || (base.NPC.velocity.X < 0f && targetTilePosX < 0f) || (base.NPC.velocity.Y > 0f && targetTilePosY > 0f) || (base.NPC.velocity.Y < 0f && targetTilePosY < 0f))
					{
						if (base.NPC.velocity.X < targetTilePosX)
						{
							base.NPC.velocity.X += speed;
						}
						else if (base.NPC.velocity.X > targetTilePosX)
						{
							base.NPC.velocity.X -= speed;
						}
						if (base.NPC.velocity.Y < targetTilePosY)
						{
							base.NPC.velocity.Y += speed;
						}
						else if (base.NPC.velocity.Y > targetTilePosY)
						{
							base.NPC.velocity.Y -= speed;
						}
						if ((double)Math.Abs(targetTilePosY) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.X > 0f && targetTilePosX < 0f) || (base.NPC.velocity.X < 0f && targetTilePosX > 0f)))
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += speed * 2f;
							}
							else
							{
								base.NPC.velocity.Y -= speed * 2f;
							}
						}
						if ((double)Math.Abs(targetTilePosX) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.Y > 0f && targetTilePosY < 0f) || (base.NPC.velocity.Y < 0f && targetTilePosY > 0f)))
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += speed * 2f;
							}
							else
							{
								base.NPC.velocity.X -= speed * 2f;
							}
						}
					}
					else if (absoluteTilePosX2 > absoluteTilePosY)
					{
						if (base.NPC.velocity.X < targetTilePosX)
						{
							base.NPC.velocity.X += speed * 1.1f;
						}
						else if (base.NPC.velocity.X > targetTilePosX)
						{
							base.NPC.velocity.X -= speed * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += speed;
							}
							else
							{
								base.NPC.velocity.Y -= speed;
							}
						}
					}
					else
					{
						if (base.NPC.velocity.Y < targetTilePosY)
						{
							base.NPC.velocity.Y += speed * 1.1f;
						}
						else if (base.NPC.velocity.Y > targetTilePosY)
						{
							base.NPC.velocity.Y -= speed * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += speed;
							}
							else
							{
								base.NPC.velocity.X -= speed;
							}
						}
					}
				}
			}
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			if (base.NPC.type == 134)
			{
				if (shouldFly)
				{
					if (base.NPC.localAI[0] != 1f)
					{
						base.NPC.netUpdate = true;
					}
					base.NPC.localAI[0] = 1f;
				}
				else
				{
					if (base.NPC.localAI[0] != 0f)
					{
						base.NPC.netUpdate = true;
					}
					base.NPC.localAI[0] = 0f;
				}
				if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
				{
					base.NPC.netUpdate = true;
				}
			}
		}
		if (((base.NPC.type == 134) & death) && !flyAtTarget && base.NPC.Distance(player.Center) > 2000f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity += (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
		}
		if (NPC.IsMechQueenUp && base.NPC.type == 134)
		{
			NPC nPC3 = Main.npc[NPC.mechQueen];
			Vector2 mechQueenCenter = nPC3.GetMechQueenCenter();
			Vector2 mechdusaSpinningVector = default(Vector2);
			((Vector2)(ref mechdusaSpinningVector))._002Ector(0f, 100f);
			Vector2 spinningpoint = mechQueenCenter + mechdusaSpinningVector;
			float mechdusaRotation = nPC3.velocity.X * 0.025f;
			spinningpoint = spinningpoint.RotatedBy(mechdusaRotation, mechQueenCenter);
			base.NPC.position = spinningpoint - base.NPC.Size / 2f + nPC3.velocity;
			base.NPC.velocity.X = 0f;
			base.NPC.velocity.Y = 0f;
			base.NPC.rotation = mechdusaRotation * 0.75f + (float)Math.PI;
		}
		if (calamityGlobalNPC.newAI[1] < 600f)
		{
			Vector2 val2 = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val2)).Length() > 2f || calamityGlobalNPC.newAI[1] > 0f)
			{
				calamityGlobalNPC.newAI[1]++;
			}
		}
		return false;
	}
}
