using System;
using System.IO;
using CalamityMod.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.TileEntities;

public abstract class TEBaseTurret : ModTileEntity
{
	internal static readonly Vector2 InvalidTarget;

	public int FiringTime;

	public float Angle;

	public Vector2 TargetPos;

	public const int NumExtraBytes = 16;

	public abstract int TileType { get; }

	public abstract int HostTileWidth { get; }

	public abstract int HostTileHeight { get; }

	public abstract int ProjectileType { get; }

	public abstract int ProjectileDamage { get; }

	public abstract float ProjectileKnockback { get; }

	public abstract float ShootSpeed { get; }

	public abstract int FiringStartupDelay { get; }

	public abstract int FiringUseTime { get; }

	public abstract Vector2 TurretCenterOffset { get; }

	protected virtual float ShootForwardsOffset => 0f;

	public abstract float MaxRange { get; }

	protected virtual float MaxTargetAngleDeviance => MathHelper.ToRadians(16f);

	protected virtual float MaxDeltaAnglePerFrame => MathHelper.ToRadians(4f);

	protected virtual float CloseAimThreshold => MathHelper.ToRadians(8f);

	protected virtual float CloseAimLerpFactor => 1f;

	public Vector2 TurretPosition
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return Position.ToWorldCoordinates(0f, 0f) + TurretCenterOffset;
		}
	}

	protected bool TargetIsInvalid
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return TargetPos.HasNaNs();
		}
	}

	public int Direction => (Math.Cos(Angle) > 0.0).ToDirectionInt();

	public virtual float RestingAngle
	{
		get
		{
			if (Direction != -1)
			{
				return 0f;
			}
			return (float)Math.PI;
		}
	}

	protected float TargetAngle
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!TargetIsInvalid)
			{
				return (TargetPos - TurretPosition).ToRotation();
			}
			return RestingAngle;
		}
	}

	protected abstract Vector2 ChooseTarget(Vector2 targetingCenter);

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile.HasTile)
		{
			return tile.TileType == TileType;
		}
		return false;
	}

	public override void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Vector2 turretPos = TurretPosition;
		TargetPos = ChooseTarget(turretPos);
		if (!TargetIsInvalid)
		{
			ActiveBehavior(turretPos, TargetPos);
			FiringTime++;
		}
		else
		{
			PassiveBehavior(turretPos);
			FiringTime = 0;
		}
		UpdateAngle();
	}

	public virtual void UpdateClient()
	{
	}

	public virtual void UpdateAngle()
	{
		float targetAngle = TargetAngle;
		float deltaAngle = MathHelper.WrapAngle(Angle - targetAngle);
		bool usingCloseAiming = Math.Abs(deltaAngle) <= Math.Max(CloseAimThreshold, MaxDeltaAnglePerFrame);
		Angle = (usingCloseAiming ? Angle.AngleLerp(targetAngle, CloseAimLerpFactor) : MathHelper.WrapAngle(Angle - MaxDeltaAnglePerFrame * (float)Math.Sign(deltaAngle)));
	}

	protected virtual void PassiveBehavior(Vector2 turretPos)
	{
	}

	protected virtual void ActiveBehavior(Vector2 turretPos, Vector2 targetPos)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		float targetAngle = TargetAngle;
		if (Math.Abs(MathHelper.WrapAngle(Angle - targetAngle)) <= MaxTargetAngleDeviance && FiringTime >= FiringStartupDelay)
		{
			float rotation = (targetPos - turretPos).ToRotation();
			Vector2 val = targetPos - turretPos;
			if (CalamityUtils.PreciseCanHitInLine(turretPos, rotation, ((Vector2)(ref val)).Length()) && (FiringTime - FiringStartupDelay) % FiringUseTime == 0)
			{
				Shoot(turretPos);
			}
		}
	}

	public Projectile Shoot(Vector2 turretMuzzlePos, float ai0 = 0f, float ai1 = 0f)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return null;
		}
		SendSyncPacket();
		EntitySource_TileEntity spawnSource = new EntitySource_TileEntity(this);
		Vector2 angleVec = Angle.ToRotationVector2();
		Vector2 pos = turretMuzzlePos + angleVec * ShootForwardsOffset;
		Vector2 velocity = angleVec * ShootSpeed;
		return Projectile.NewProjectileDirect(spawnSource, pos, velocity, ProjectileType, ProjectileDamage, ProjectileKnockback, -1, ai0, ai1);
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, HostTileWidth, HostTileHeight);
			NetMessage.SendData(87, -1, -1, null, i, j, base.Type);
			return -1;
		}
		return Place(i, j);
	}

	public override void OnNetPlace()
	{
		NetMessage.SendData(86, -1, -1, null, ID, Position.X, Position.Y);
	}

	public override void NetSend(BinaryWriter writer)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(FiringTime);
		writer.Write(Angle);
		writer.WriteVector2(TargetPos);
	}

	public override void NetReceive(BinaryReader reader)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		FiringTime = reader.ReadInt32();
		Angle = reader.ReadSingle();
		TargetPos = reader.ReadVector2();
	}

	protected internal void SendSyncPacket()
	{
		if (Main.netMode != 0)
		{
			TETurretPacket.Send(this);
		}
	}

	public virtual void WriteExtraTurretData(BinaryWriter writer)
	{
		writer.Write(0uL);
		writer.Write(0uL);
	}

	public virtual void ReadExtraTurretData(BinaryReader reader)
	{
		reader.ReadBytes(16);
	}

	protected TEBaseTurret()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		TargetPos = InvalidTarget;
		base._002Ector();
	}

	static TEBaseTurret()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		InvalidTarget = new Vector2(float.NaN, float.NaN);
	}
}
