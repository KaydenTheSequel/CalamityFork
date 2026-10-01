using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Effects;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using CalamityMod.Systems.Mechanic;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Deconstructors;

public class Burrower : BaseWormNPC
{
	public enum AttackState
	{
		Idle,
		Mining,
		GettingItem,
		Fleeing
	}

	public Vector2 TargetVector;

	public Vector2 SecondaryVector;

	public float StoredValue;

	public override string Texture => "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Head";

	public override int WormHitboxNpcType => ModContent.NPCType<BurrowerHitbox>();

	public override List<string> SegmentTextures => new List<string> { "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Body", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt1", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt2", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Tail" };

	public override List<string?> GlowTextures => new List<string> { null, "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyGlow", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt1Glow", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt2Glow" };

	public override int SegmentCount => 10;

	public override List<float> SegmentTypePositionOffsets => new List<float> { 32f, 32f, 32f, 32f, 32f };

	public static HashSet<int> VulnerableDebuffs => new HashSet<int>
	{
		144,
		ModContent.BuffType<StaticDischarge>(),
		ModContent.BuffType<VermillionFlux>(),
		ModContent.BuffType<AuricRebuke>()
	};

	public AttackState ActiveAttackState
	{
		get
		{
			return (AttackState)base.NPC.ai[1];
		}
		set
		{
			base.NPC.ai[1] = (float)value;
		}
	}

	public float MainTimer
	{
		get
		{
			return base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	public float AttackSubstate
	{
		get
		{
			return base.NPC.ai[2];
		}
		set
		{
			base.NPC.ai[2] = value;
		}
	}

	public float StateChangeCounter
	{
		get
		{
			return base.NPC.ai[3];
		}
		set
		{
			base.NPC.ai[3] = value;
		}
	}

	public float VelocityRotation
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.NPC.velocity.ToRotation();
		}
		set
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.velocity = value.ToRotationVector2() * ((Vector2)(ref base.NPC.velocity)).Length();
		}
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.ImmuneToRegularBuffs[base.Type] = true;
		foreach (int item in VulnerableDebuffs)
		{
			NPCID.Sets.SpecificDebuffImmunity[base.Type][item] = false;
		}
		NPCID.Sets.ImmuneToRegularBuffs[WormHitboxNpcType] = NPCID.Sets.ImmuneToRegularBuffs[base.Type];
		NPCID.Sets.SpecificDebuffImmunity[WormHitboxNpcType] = NPCID.Sets.SpecificDebuffImmunity[base.Type];
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 0;
		base.NPC.width = 38;
		base.NPC.height = 38;
		base.NPC.lifeMax = 500;
		base.NPC.value = Item.buyPrice(0, 0, 50);
		base.NPC.rarity = 3;
		base.NPC.HitSound = ThanatosHead.ThanatosHitSoundClosed;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.NPC.knockBackResist = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.chaseable = false;
		base.NPC.Calamity().DR = 0.9f;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = false;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BurrowerBanner>();
		for (int i = 0; i < SegmentCount - 1; i++)
		{
			Segments.Add(new BaseWormSegment(this, i % 3));
		}
		Segments.Add(new BaseWormSegment(this, 3));
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new global::_003C_003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Burrower")
		}));
	}

	public void SwitchAttackState(AttackState State, float Substate = 0f, bool resetVector = true)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		ActiveAttackState = State;
		AttackSubstate = Substate;
		MainTimer = 0f;
		if (resetVector)
		{
			TargetVector = Vector2.Zero;
		}
	}

	public override void AI()
	{
		if (Main.netMode != 1 && Main.BestiaryTracker.Kills.GetKillCount(base.NPC) <= 0)
		{
			Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
		}
		HandleAIStates();
		MainTimer++;
		UpdateSegments();
	}

	public static List<List<Point>> FindOreVeins(Point wormTile)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		List<List<Point>> oreVeins = new List<List<Point>>();
		HashSet<Point> visited = new HashSet<Point>();
		Point start = default(Point);
		for (int x = -30; x <= 30; x++)
		{
			int tileX = wormTile.X + x;
			if (tileX < 0 || tileX >= Main.maxTilesX)
			{
				continue;
			}
			for (int y = -30; y <= 30; y++)
			{
				int tileY = wormTile.Y + y;
				if (tileY < 0 || tileY >= Main.maxTilesY)
				{
					continue;
				}
				((Point)(ref start))._002Ector(tileX, tileY);
				if (visited.Contains(start))
				{
					continue;
				}
				Tile tile = Main.tile[start];
				if (!tile.HasTile || !TileID.Sets.Ore[tile.TileType])
				{
					continue;
				}
				List<Point> vein = new List<Point>();
				Queue<Point> queue = new Queue<Point>();
				queue.Enqueue(start);
				visited.Add(start);
				while (queue.Count > 0)
				{
					Point p = queue.Dequeue();
					vein.Add(p);
					Point[] array = (Point[])(object)new Point[4]
					{
						new Point(1, 0),
						new Point(-1, 0),
						new Point(0, 1),
						new Point(0, -1)
					};
					foreach (Point offset in array)
					{
						Point neighbor = p + offset;
						if (neighbor.X >= 0 && neighbor.X < Main.maxTilesX && neighbor.Y >= 0 && neighbor.Y < Main.maxTilesY && !visited.Contains(neighbor))
						{
							Tile neighborTile = Main.tile[neighbor];
							if (neighborTile.HasTile && TileID.Sets.Ore[neighborTile.TileType])
							{
								queue.Enqueue(neighbor);
								visited.Add(neighbor);
							}
						}
					}
				}
				oreVeins.Add(vein);
			}
		}
		return oreVeins;
	}

	public static (Point, Point)? FindTargetFromVein(List<Point> vein)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		HashSet<Point> veinSet = new HashSet<Point>(vein);
		List<(Point, Point)> outerPoints = new List<(Point, Point)>();
		foreach (Point p in vein)
		{
			Point[] array = (Point[])(object)new Point[4]
			{
				new Point(1, 0),
				new Point(-1, 0),
				new Point(0, 1),
				new Point(0, -1)
			};
			foreach (Point offset in array)
			{
				Point neighbor = p + offset;
				if (!veinSet.Contains(neighbor) && neighbor.X >= 0 && neighbor.X < Main.maxTilesX && neighbor.Y >= 0 && neighbor.Y < Main.maxTilesY)
				{
					Tile tile = Main.tile[neighbor];
					if (tile == null || !tile.HasTile || !tile.IsTileSolid())
					{
						outerPoints.Add((p, neighbor));
					}
				}
			}
		}
		if (outerPoints.Count > 0)
		{
			return outerPoints[Main.rand.Next(outerPoints.Count)];
		}
		return null;
	}

	private void LowerTargetToGround()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Point pointToCheck = TargetVector.ToTileCoordinates();
		for (int i = 0; i < 50; i++)
		{
			if (pointToCheck.X < 0)
			{
				break;
			}
			if (pointToCheck.X >= Main.maxTilesX)
			{
				break;
			}
			if (pointToCheck.Y < 0)
			{
				break;
			}
			if (pointToCheck.Y >= Main.maxTilesY)
			{
				break;
			}
			Tile targetTile = Main.tile[pointToCheck];
			if (targetTile == null || !targetTile.HasTile || !targetTile.IsTileSolidGround())
			{
				pointToCheck.Y++;
				continue;
			}
			TargetVector = pointToCheck.ToWorldCoordinates() - new Vector2(0f, 16f);
			break;
		}
	}

	public void HandleAIStates()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		if (!base.NPC.HasValidTarget)
		{
			return;
		}
		Player player = Main.player[base.NPC.target];
		SegmentMaxRotation = 0.65f;
		SegmentRigidity = 0.2f;
		if (base.NPC.life < base.NPC.lifeMax && ActiveAttackState != AttackState.Fleeing)
		{
			ActiveAttackState = AttackState.Fleeing;
			GeneralParticleHandler.SpawnParticle(new EmoteExpressionParticle(base.NPC.Top, -Vector2.UnitY * 5f, 2f, ArsenalEffects.ArsenalLaserColor, 60, EmoteExpressionParticle.EmoteType.DoubleExclamation));
		}
		base.NPC.FindClosestPlayer(out var distanceToPlayer);
		bool noGravity = distanceToPlayer > 800f || base.NPC.wet || Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height, acceptTopSurfaces: true);
		switch (ActiveAttackState)
		{
		case AttackState.Idle:
			if (TargetVector == Vector2.Zero || MainTimer > 300f || base.NPC.Distance(TargetVector) < 32f)
			{
				if (Main.rand.NextBool())
				{
					List<List<Point>> veins = FindOreVeins(base.NPC.Center.ToTileCoordinates());
					while (veins.Count > 0)
					{
						List<Point> targetVein = veins[Main.rand.Next(veins.Count)];
						(Point, Point)? foundTarget = FindTargetFromVein(targetVein);
						if (foundTarget.HasValue)
						{
							TargetVector = foundTarget.Value.Item1.ToWorldCoordinates();
							SecondaryVector = foundTarget.Value.Item2.ToWorldCoordinates();
							if (base.NPC.Distance(TargetVector) > 160f)
							{
								GeneralParticleHandler.SpawnParticle(new EmoteExpressionParticle(base.NPC.Top, -Vector2.UnitY * 5f, 2f, ArsenalEffects.ArsenalGaussColor, 60, EmoteExpressionParticle.EmoteType.Exclamation));
							}
							SwitchAttackState(AttackState.Mining, 0f, resetVector: false);
							return;
						}
						veins.Remove(targetVein);
					}
				}
				TargetVector = player.Center + Main.rand.NextVector2Circular(800f, 800f);
				LowerTargetToGround();
				MainTimer = 0f;
			}
			if ((AttackSubstate <= 0f) & noGravity)
			{
				NPC nPC5 = base.NPC;
				nPC5.velocity += base.NPC.DirectionTo(TargetVector);
				NPC nPC6 = base.NPC;
				nPC6.velocity *= 0.9f;
			}
			else
			{
				AttackSubstate--;
				if (!noGravity)
				{
					AttackSubstate = 30f;
					base.NPC.velocity.Y++;
				}
				else
				{
					if (base.NPC.velocity.Y > 8f)
					{
						base.NPC.velocity.Y *= 0.9f;
					}
					base.NPC.velocity.X *= 0.95f;
				}
			}
			base.NPC.velocity = base.NPC.velocity.ClampMagnitude(0f, 16f);
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			break;
		case AttackState.Mining:
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity += base.NPC.DirectionTo(SecondaryVector).SafeNormalize(Vector2.UnitY);
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 0.9f;
			if (MainTimer > 600f)
			{
				SwitchAttackState(AttackState.Idle);
			}
			if (base.NPC.Distance(SecondaryVector) < 4f)
			{
				Vector2 dir = SecondaryVector.DirectionTo(TargetVector);
				if (Main.tile[TargetVector.ToTileCoordinates()].TileType == ModContent.TileType<global::CalamityMod.Tiles.Ores.AuricOre>())
				{
					base.NPC.velocity = -base.NPC.DirectionTo(TargetVector) * 16f;
					base.NPC.Center = SecondaryVector + base.NPC.velocity;
					base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
					base.NPC.AddBuff(ModContent.BuffType<AuricRebuke>(), 600);
					ActiveAttackState = AttackState.Fleeing;
					global::CalamityMod.Tiles.Ores.AuricOre.Animate = true;
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1"), base.NPC.Center);
					break;
				}
				SegmentRigidity = 0f;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.rotation = SecondaryVector.DirectionTo(TargetVector).ToRotation() + (float)Math.PI / 2f;
				if (Main.netMode != 2 && !BurrowerPingTileEffect.Instance.Active)
				{
					TilePingerSystem.AddPing(BurrowerPingTileEffect.Instance, base.NPC.Center, player);
				}
				for (int i = 0; i < 1; i++)
				{
					int sparkLifetime = Main.rand.Next(10, 20);
					float sparkScale = Main.rand.NextFloat(0.8f, 1f);
					Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
					sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());
					if (Main.rand.NextBool(10))
					{
						sparkScale *= 2f;
					}
					Vector2 sparkVelocity = dir.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(6f, 16f);
					GeneralParticleHandler.SpawnParticle(new SparkParticle((TargetVector + SecondaryVector) * 0.5f, -sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
					if (MainTimer < 520f)
					{
						MainTimer = 520f;
					}
				}
				SoundEngine.PlaySound(SoundID.NPCHit18 with
				{
					Volume = 0.2f
				}, base.NPC.Center);
			}
			else
			{
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			}
			break;
		}
		case AttackState.GettingItem:
			ActiveAttackState = AttackState.Idle;
			break;
		case AttackState.Fleeing:
		{
			bool shocked = false;
			foreach (int item in VulnerableDebuffs)
			{
				if (base.NPC.HasBuff(item) || Main.npc.Any((NPC x) => x.active && x.type == WormHitboxNpcType && x.HasBuff(item)))
				{
					shocked = true;
					break;
				}
			}
			if (noGravity)
			{
				if (shocked)
				{
					SegmentRigidity = 0f;
					NPC nPC = base.NPC;
					nPC.velocity *= 0.75f;
					{
						foreach (BaseWormSegment item2 in Segments)
						{
							if (!Collision.SolidCollision(item2.Center - new Vector2(19f, 17f), 38, 38, acceptTopSurfaces: true))
							{
								item2.Center.Y += 2f;
							}
						}
						break;
					}
				}
				NPC nPC2 = base.NPC;
				nPC2.velocity += base.NPC.DirectionFrom(Main.player[base.NPC.FindClosestPlayer()].Center);
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			}
			else
			{
				base.NPC.velocity.Y += (shocked ? 0.5f : 1f);
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			}
			break;
		}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_Head").Type, base.NPC.scale);
		Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_Head2").Type, base.NPC.scale);
		foreach (BaseWormSegment item in Segments)
		{
			switch (item.segmentType)
			{
			case 0:
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_Body").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_Body2").Type, base.NPC.scale);
				break;
			case 1:
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_BodyAlt_1").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_BodyAlt_2").Type, base.NPC.scale);
				break;
			case 2:
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_BodyAlt2_1").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_BodyAlt2_2").Type, base.NPC.scale);
				break;
			case 3:
				Gore.NewGore(base.NPC.GetSource_Death(), item.Center, base.NPC.velocity, base.Mod.Find<ModGore>("DeconstructorMK1_Tail").Type, base.NPC.scale);
				break;
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<MysteriousCircuitry>(), 1, 4, 8);
		npcLoot.Add(ModContent.ItemType<DubiousPlating>(), 1, 4, 8);
		npcLoot.Add(ModContent.ItemType<BurrowerController>(), 10);
		npcLoot.Add(12, 2, 6, 32);
		npcLoot.Add(699, 2, 6, 32);
		npcLoot.Add(11, 2, 6, 32);
		npcLoot.Add(700, 2, 6, 32);
		npcLoot.Add(14, 3, 6, 32);
		npcLoot.Add(701, 3, 6, 32);
		npcLoot.Add(13, 4, 6, 32);
		npcLoot.Add(702, 4, 6, 32);
		npcLoot.Add(56, 5, 6, 32);
		npcLoot.Add(880, 5, 6, 32);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.PostPlant());
		mainRule.Add(364, 2, 6, 32);
		mainRule.Add(1104, 2, 6, 32);
		mainRule.Add(365, 3, 6, 32);
		mainRule.Add(1105, 3, 6, 32);
		mainRule.Add(366, 4, 6, 32);
		mainRule.Add(1106, 4, 6, 32);
		mainRule.Add(ModContent.ItemType<global::CalamityMod.Items.Placeables.Ores.HallowedOre>(), 5, 6, 32);
		mainRule.Add(ModContent.ItemType<global::CalamityMod.Items.Placeables.Ores.PerennialOre>(), 5, 6, 32);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().InAnyCalamityBiome || Main.npc.Any((NPC x) => x.active && x.type == base.Type))
		{
			return 0f;
		}
		return SpawnCondition.Cavern.Chance * (Main.projectile.Any((Projectile x) => x.active && x.type == ModContent.ProjectileType<WulfrumLureSignal>()) ? 5f : 0.01f);
	}

	public Burrower()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		TargetVector = Vector2.Zero;
		SecondaryVector = Vector2.Zero;
		base._002Ector();
	}
}
