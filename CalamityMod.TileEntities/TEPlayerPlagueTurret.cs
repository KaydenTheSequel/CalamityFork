using System.IO;
using CalamityMod.Projectiles.Turret;
using CalamityMod.Tiles.PlayerTurrets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.TileEntities;

public class TEPlayerPlagueTurret : TEBaseTurret
{
	private int _npcTargetIndex = -1;

	private const int BytesUsed = 2;

	private static readonly byte[] JunkData = new byte[14];

	public override int TileType => ModContent.TileType<PlayerPlagueTurret>();

	public override int HostTileWidth => 3;

	public override int HostTileHeight => 2;

	public override int ProjectileType => ModContent.ProjectileType<PlagueShotBuffer>();

	public override int ProjectileDamage => 100;

	public override float ProjectileKnockback => 6f;

	public override float ShootSpeed => 16f;

	public override int FiringStartupDelay => 50;

	public override int FiringUseTime => 50;

	public override Vector2 TurretCenterOffset
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(22f + 4f * (float)base.Direction, -2f);
		}
	}

	protected override float ShootForwardsOffset => 24f;

	public override float MaxRange => 900f;

	protected override float MaxTargetAngleDeviance => MathHelper.ToRadians(50f);

	protected override float MaxDeltaAnglePerFrame => MathHelper.ToRadians(6f);

	protected override float CloseAimThreshold => MathHelper.ToRadians(12f);

	protected override float CloseAimLerpFactor => 0.08f;

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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.AnyBossNPCS())
		{
			NPCTargetIndex = -1;
			return TEBaseTurret.InvalidTarget;
		}
		int indexToSet = NPCTargetIndex;
		Vector2 ret = TEBaseTurret.InvalidTarget;
		float distSQToBeat = MaxRange * MaxRange;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.friendly && !npc.CountsAsACritter)
			{
				float distSQ = npc.DistanceSQ(targetingCenter);
				if (distSQ < distSQToBeat)
				{
					distSQToBeat = distSQ;
					ret = npc.Center;
					indexToSet = npc.whoAmI;
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
