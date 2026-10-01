using System.IO;
using CalamityMod.Projectiles.Turret;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.TileEntities;

public class TEHostileIceTurret : TEBaseTurret
{
	private int _playerTargetIndex = -1;

	private const int BytesUsed = 2;

	private static readonly byte[] JunkData = new byte[14];

	public override int TileType => ModContent.TileType<HostileIceTurret>();

	public override int HostTileWidth => 3;

	public override int HostTileHeight => 2;

	public override int ProjectileType => ModContent.ProjectileType<IceShotBuffer>();

	public override int ProjectileDamage
	{
		get
		{
			if (!Main.expertMode)
			{
				return 24;
			}
			return 18;
		}
	}

	public override float ProjectileKnockback => 0f;

	public override float ShootSpeed => 8f;

	public override int FiringStartupDelay => 45;

	public override int FiringUseTime => 45;

	public override Vector2 TurretCenterOffset
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(22f + 4f * (float)base.Direction, -2f);
		}
	}

	protected override float ShootForwardsOffset => 24f;

	public override float MaxRange => 450f;

	protected override float MaxTargetAngleDeviance => MathHelper.ToRadians(36f);

	protected override float MaxDeltaAnglePerFrame => MathHelper.ToRadians(3f);

	protected override float CloseAimThreshold => MathHelper.ToRadians(12f);

	protected override float CloseAimLerpFactor => 0.08f;

	public int PlayerTargetIndex
	{
		get
		{
			return _playerTargetIndex;
		}
		set
		{
			bool num = _playerTargetIndex != value;
			_playerTargetIndex = value;
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
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		int indexToSet = PlayerTargetIndex;
		if (indexToSet != -1)
		{
			Player p = Main.player[PlayerTargetIndex];
			if (!p.active || p.dead)
			{
				indexToSet = -1;
			}
			else
			{
				Vector2 playerPos = p.Center;
				if (Vector2.DistanceSquared(targetingCenter, playerPos) <= MaxRange * MaxRange)
				{
					return playerPos;
				}
				indexToSet = -1;
			}
		}
		Vector2 ret = TEBaseTurret.InvalidTarget;
		float distSQToBeat = MaxRange * MaxRange;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p2 = enumerator.Current;
			if (!p2.dead)
			{
				float distSQ = p2.DistanceSQ(targetingCenter);
				if (distSQ < distSQToBeat)
				{
					distSQToBeat = distSQ;
					ret = p2.Center;
					indexToSet = p2.whoAmI;
				}
			}
		}
		PlayerTargetIndex = indexToSet;
		return ret;
	}

	public override void UpdateClient()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerTargetIndex != -1)
		{
			Player p = Main.player[PlayerTargetIndex];
			if (p.active && !p.dead)
			{
				TargetPos = p.Center;
			}
		}
	}

	public override void WriteExtraTurretData(BinaryWriter writer)
	{
		writer.Write((short)_playerTargetIndex);
		writer.Write(JunkData);
	}

	public override void ReadExtraTurretData(BinaryReader reader)
	{
		_playerTargetIndex = reader.ReadInt16();
		reader.ReadBytes(14);
	}
}
