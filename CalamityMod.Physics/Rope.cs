using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Physics;

internal class Rope
{
	[CompilerGenerated]
	private Vector2 _003CGravity_003Ek__BackingField;

	private float MovementSpeedDampingCoefficient { get; set; }

	private float WindTime { get; set; }

	public RopeSegment[] Segments { get; set; }

	public Vector2[] SegmentPositions { get; private set; }

	public float DistancePerSegment { get; set; }

	public Vector2 Gravity
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CGravity_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CGravity_003Ek__BackingField = value;
		}
	}

	public int ConstraintSteps { get; private set; }

	public RopeSettings Settings { get; set; }

	public bool InteractWithTiles => Settings.TileColliderArea.HasValue;

	public Rope(Vector2 start, Vector2 end, int segmentCount, float distancePerSegment, Vector2 gravity, RopeSettings settings, int constraintSteps = 10)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Segments = new RopeSegment[segmentCount];
		SegmentPositions = (Vector2[])(object)new Vector2[segmentCount];
		for (int i = 0; i < segmentCount; i++)
		{
			Vector2 segmentPos = Vector2.Lerp(start, end, (float)i / ((float)segmentCount - 1f));
			Segments[i] = new RopeSegment(segmentPos);
		}
		Segments[0].FixedInPlace = settings.StartIsFixed;
		Segments[^1].FixedInPlace = settings.EndIsFixed;
		RecalculateSegmentPositions();
		DistancePerSegment = distancePerSegment;
		Gravity = gravity;
		ConstraintSteps = constraintSteps;
		Settings = settings;
	}

	private void RecalculateSegmentPositions()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Length; i++)
		{
			SegmentPositions[i] = Segments[i].Position;
		}
	}

	private void Move(ref Vector2 position, Vector2 baseVelocity)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (!InteractWithTiles || !Settings.TileColliderArea.HasValue)
		{
			position += baseVelocity;
			return;
		}
		int width = (int)Settings.TileColliderArea.Value.X;
		int height = (int)Settings.TileColliderArea.Value.Y;
		Vector2 newVelocity = Collision.noSlopeCollision(position, baseVelocity, width, height + 2, fallThrough: true, fall2: true);
		newVelocity = Collision.noSlopeCollision(position, newVelocity, width, height, fallThrough: true, fall2: true);
		Vector2 finalVelocity = baseVelocity;
		if (Math.Abs(baseVelocity.X) > Math.Abs(newVelocity.X))
		{
			finalVelocity.X = 0f;
		}
		if (Math.Abs(baseVelocity.Y) > Math.Abs(newVelocity.Y))
		{
			finalVelocity.Y = 0f;
		}
		position += finalVelocity;
	}

	public void Update()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Length; i++)
		{
			Vector2 movementStep = (Segments[i].Position - Segments[i].OldPosition) * (1f - MovementSpeedDampingCoefficient);
			if (((Vector2)(ref movementStep)).Length() < 0.02f)
			{
				movementStep = Vector2.Zero;
			}
			Segments[i].OldPosition = Segments[i].Position;
			if (!Segments[i].FixedInPlace)
			{
				Move(ref Segments[i].Position, movementStep + Gravity);
			}
		}
		for (int j = 0; j < ConstraintSteps; j++)
		{
			Constrain();
		}
		RecalculateSegmentPositions();
		if (Settings.RespondToEntityMovement)
		{
			HandleEntityMovementResponse();
		}
		if (Settings.RespondToWind)
		{
			HandleWindResponse();
		}
	}

	private void HandleEntityMovementResponse()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Length; i++)
		{
			ref RopeSegment ropeSegment = ref Segments[i];
			if (!ropeSegment.FixedInPlace)
			{
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player player = enumerator.Current;
					float playerProximityInterpolant = Utils.GetLerpValue(37f, 10f, player.Distance(ropeSegment.Position), clamped: true);
					ref Vector2 position = ref ropeSegment.Position;
					position += player.velocity * playerProximityInterpolant / Settings.Mass * 0.08f;
				}
			}
		}
	}

	private void HandleWindResponse()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		WindTime += Main.windSpeedCurrent / 60f;
		if (MathF.Abs(WindTime) >= 4000f)
		{
			WindTime = 0f;
		}
		float windSpeed = Math.Clamp(Main.WindForVisuals * 2f, -1.3f, 1.3f);
		float windWave = MathF.Cos(WindTime * 3.42f + ((Vector2)(ref Segments[0].Position)).Length() * 0.06f);
		Vector2 wind = Vector2.UnitX * (windWave + Main.windSpeedCurrent) * -0.2f;
		ref Vector2 position = ref Segments[^1].Position;
		position += wind * Utils.GetLerpValue(0.3f, 0.75f, windSpeed, clamped: true) / Settings.Mass;
	}

	public void Constrain()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Length - 1; i++)
		{
			float distanceFromIdealLength = Segments[i].Position.Distance(Segments[i + 1].Position) - DistancePerSegment;
			Vector2 correctiveForce = (Segments[i].Position - Segments[i + 1].Position).SafeNormalize(Vector2.Zero) * distanceFromIdealLength;
			bool pinned = Segments[i].FixedInPlace;
			bool nextPinned = Segments[i + 1].FixedInPlace;
			correctiveForce *= ((pinned | nextPinned) ? 1f : 0.5f);
			if (!pinned)
			{
				Move(ref Segments[i].Position, -correctiveForce);
			}
			if (!nextPinned)
			{
				Move(ref Segments[i + 1].Position, correctiveForce);
			}
		}
	}
}
