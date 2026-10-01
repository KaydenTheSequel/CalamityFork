using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Pathfinding;
using CalamityMod.Pathfinding.Movements;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public abstract class SunkenSeaNPC : ModNPC, IPathfinder
{
	protected PathfindingManager pathfinding;

	private NPC _currentPrey;

	private NPC _currentPredator;

	private Player _currentPlayer;

	protected abstract List<int> PreyIDs { get; }

	protected abstract List<int> PredatorIDs { get; }

	protected abstract SunkenSeaBiomeFlags BiomeDesignation { get; }

	protected NPC CurrentPrey
	{
		get
		{
			return _currentPrey;
		}
		private set
		{
			if (value != null && (_currentPrey == null || _currentPrey.whoAmI != value.whoAmI))
			{
				OnPreyDetection(value);
			}
			_currentPrey = value;
		}
	}

	protected NPC CurrentPredator
	{
		get
		{
			return _currentPredator;
		}
		private set
		{
			if (value != null && (_currentPredator == null || _currentPredator.whoAmI != value.whoAmI))
			{
				OnPredatorDetection(value);
			}
			_currentPredator = value;
		}
	}

	protected Player CurrentPlayer
	{
		get
		{
			return _currentPlayer;
		}
		private set
		{
			if (value != null && (_currentPlayer == null || _currentPlayer.whoAmI != value.whoAmI))
			{
				OnPlayerDetection(value);
			}
			_currentPlayer = value;
		}
	}

	public float Acceleration { get; set; } = 0.2f;

	public float MaxSpeed { get; set; } = 4f;

	public float MinimumPointDistance { get; set; } = 48f;

	public virtual IEnumerable<IMovement> Movements => new _003C_003Ez__ReadOnlySingleElementList<IMovement>(new SunkenSeaSwimMovement(base.NPC));

	public override void SetStaticDefaults()
	{
		NPCID.Sets.UsesNewTargetting[base.Type] = true;
		NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.SpawnModBiomes = (from flag in Enum.GetValues<SunkenSeaBiomeFlags>()
			where flag != SunkenSeaBiomeFlags.None && flag != SunkenSeaBiomeFlags.UndergroundDesert && BiomeDesignation.HasFlag(flag)
			select SunkenSeaBiomeCorrespondentDict.Dict[flag].BiomeType).ToArray();
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new _003C_003Ez__ReadOnlySingleElementList<IBestiaryInfoElement>(new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary." + Name)));
	}

	public override bool PreAI()
	{
		UpdateTargets();
		return true;
	}

	public virtual void OnHitByNPC(NPC attacker)
	{
	}

	protected virtual void OnPreyDetection(NPC prey)
	{
	}

	protected virtual void OnPredatorDetection(NPC predator)
	{
	}

	protected virtual void OnPlayerDetection(Player player)
	{
	}

	protected virtual bool PlayerSearchFilter(Player p)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.HasSight(p.Center))
		{
			return Vector2.DistanceSquared(base.NPC.Center, p.Center) < 72900f;
		}
		return false;
	}

	protected virtual bool NPCSearchFilter(NPC n)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.HasSight(n.Center) && Vector2.DistanceSquared(base.NPC.Center, n.Center) < 72900f)
		{
			if (!PreyIDs.Contains(n.type))
			{
				return PredatorIDs.Contains(n.type);
			}
			return true;
		}
		return false;
	}

	protected void UpdateTargets()
	{
		NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(base.NPC, NPCUtils.TargetSearchFlag.All, PlayerSearchFilter, NPCSearchFilter);
		if (!searchResults.FoundTarget)
		{
			CurrentPredator = null;
			CurrentPrey = null;
			CurrentPlayer = null;
			return;
		}
		CurrentPlayer = searchResults.NearestTankOwner;
		if (!searchResults.FoundNPC)
		{
			CurrentPredator = null;
			CurrentPrey = null;
		}
		else
		{
			NPC nearestNPC = searchResults.NearestNPC;
			CurrentPredator = (PredatorIDs.Contains(nearestNPC.type) ? nearestNPC : null);
			CurrentPrey = (PreyIDs.Contains(nearestNPC.type) ? nearestNPC : null);
		}
	}

	protected bool SunkenSeaTileValidity(Point point)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return SunkenSeaTileValidity(base.NPC, point);
	}

	protected bool SunkenSeaTileValiditySizeless(Point point)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return SunkenSeaTileValidity(base.NPC, point, accountForSize: false);
	}

	public static bool SunkenSeaTileValidity(NPC npc, Point point, bool accountForSize = true)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		Point actualFuckingPoint = default(Point);
		((Point)(ref actualFuckingPoint))._002Ector(point.X * 16, point.Y * 16);
		if (accountForSize)
		{
			Rectangle hitbox = npc.Hitbox;
			if (!((Rectangle)(ref hitbox)).Contains(actualFuckingPoint))
			{
				return !npc.GetIntersectingHitboxPoints(actualFuckingPoint, 10, 10).Any(delegate(Point a)
				{
					//IL_0005: Unknown result type (might be due to invalid IL or missing references)
					//IL_0017: Unknown result type (might be due to invalid IL or missing references)
					//IL_0032: Unknown result type (might be due to invalid IL or missing references)
					return Main.tile[a].IsTileSolidGround() || Main.tile[a].LiquidAmount < byte.MaxValue || Main.tile[a].LiquidType != 0;
				});
			}
			return true;
		}
		if (!Main.tile[point].IsTileSolidGround() && Main.tile[point].LiquidAmount >= byte.MaxValue)
		{
			return Main.tile[point].LiquidType == 0;
		}
		return false;
	}

	public bool LavaTileValidity(Point point)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		Point actualFuckingPoint = default(Point);
		((Point)(ref actualFuckingPoint))._002Ector(point.X * 16, point.Y * 16);
		Rectangle hitbox = base.NPC.Hitbox;
		if (!((Rectangle)(ref hitbox)).Contains(actualFuckingPoint))
		{
			return !base.NPC.GetIntersectingHitboxPoints(actualFuckingPoint, 10, 10).Any(delegate(Point a)
			{
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_0032: Unknown result type (might be due to invalid IL or missing references)
				return Main.tile[a].IsTileSolidGround() || Main.tile[a].LiquidAmount < byte.MaxValue || Main.tile[a].LiquidType != 1;
			});
		}
		return true;
	}

	public virtual void AwaitingPathBehavior()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		NPC nPC = base.NPC;
		nPC.velocity *= 0.95f;
	}
}
