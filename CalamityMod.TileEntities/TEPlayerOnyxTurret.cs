using System.IO;
using CalamityMod.Projectiles.Turret;
using CalamityMod.Tiles.PlayerTurrets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.TileEntities;

public class TEPlayerOnyxTurret : TEBaseTurret
{
	private int _npcTargetIndex = -1;

	private const int BytesUsed = 2;

	private static readonly byte[] JunkData = new byte[14];

	public override int TileType => ModContent.TileType<PlayerOnyxTurret>();

	public override int HostTileWidth => 3;

	public override int HostTileHeight => 2;

	public override int ProjectileType => ModContent.ProjectileType<OnyxShotBuffer>();

	public override int ProjectileDamage => 20;

	public override float ProjectileKnockback => 2f;

	public override float ShootSpeed => 6.5f;

	public override int FiringStartupDelay => 55;

	public override int FiringUseTime => 55;

	public override Vector2 TurretCenterOffset
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(22f + 4f * (float)base.Direction, -2f);
		}
	}

	protected override float ShootForwardsOffset => 30f;

	public override float MaxRange => 300f;

	protected override float MaxTargetAngleDeviance => MathHelper.ToRadians(36f);

	protected override float MaxDeltaAnglePerFrame => MathHelper.ToRadians(5f);

	protected override float CloseAimThreshold => MathHelper.ToRadians(2f);

	protected override float CloseAimLerpFactor => 0.2f;

	public int NPCTargetIndex
	{
		get
		{
			return _npcTargetIndex;
		}
		set
		{
			bool num = _npcTargetIndex != value;
			_npcTargetIndex = value;
			if (num)
			{
				SendSyncPacket();
			}
		}
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile.HasTile && tile.TileType == TileType && tile.TileFrameX == 0)
		{
			return tile.TileFrameY == 0;
		}
		return false;
	}

	protected override Vector2 ChooseTarget(Vector2 targetingCenter)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.AnyBossNPCS())
		{
			NPCTargetIndex = -1;
			return TEBaseTurret.InvalidTarget;
		}
		int indexToSet = NPCTargetIndex;
		if (indexToSet != -1)
		{
			NPC npc = Main.npc[NPCTargetIndex];
			if (!npc.active)
			{
				indexToSet = -1;
			}
			else
			{
				Vector2 npcPos = npc.Center;
				if (Vector2.DistanceSquared(targetingCenter, npcPos) <= MaxRange * MaxRange)
				{
					return npcPos;
				}
				indexToSet = -1;
			}
		}
		Vector2 ret = TEBaseTurret.InvalidTarget;
		float distSQToBeat = MaxRange * MaxRange;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc2 = enumerator.Current;
			if (!npc2.friendly && !npc2.CountsAsACritter)
			{
				float distSQ = npc2.DistanceSQ(targetingCenter);
				if (distSQ < distSQToBeat)
				{
					distSQToBeat = distSQ;
					ret = npc2.Center;
					indexToSet = npc2.whoAmI;
				}
			}
		}
		NPCTargetIndex = indexToSet;
		return ret;
	}

	public override void UpdateClient()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (NPCTargetIndex != -1)
		{
			NPC npc = Main.npc[NPCTargetIndex];
			if (npc.active)
			{
				TargetPos = npc.Center;
			}
		}
	}

	public override void WriteExtraTurretData(BinaryWriter writer)
	{
		writer.Write((short)_npcTargetIndex);
		writer.Write(JunkData);
	}

	public override void ReadExtraTurretData(BinaryReader reader)
	{
		_npcTargetIndex = reader.ReadInt16();
		reader.ReadBytes(14);
	}
}
