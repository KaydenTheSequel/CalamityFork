using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace CalamityMod.Tiles.BaseTiles;

public class BranchDrawer
{
	public class Branch
	{
		public BezierCurve Curve;

		public Vector2 EndOfCurve;

		public float Direction;

		public float CurveLength;

		public float StartingWidth;

		public float EndingWidth;

		public Branch PreviousBranch;

		public int Generation
		{
			get
			{
				int generation = 0;
				Branch parent = PreviousBranch;
				while (parent != null)
				{
					parent = parent.PreviousBranch;
					generation++;
				}
				return generation;
			}
		}

		public Branch(BezierCurve curve, Vector2 end, float length, float direction, float startWidth, float endWidth, Branch previousBranch = null)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Curve = curve;
			EndOfCurve = end;
			CurveLength = length;
			Direction = direction;
			StartingWidth = startWidth;
			EndingWidth = endWidth;
			PreviousBranch = previousBranch;
		}
	}

	internal BasicEffect basicShader;

	internal Point PreviousPoint;

	internal VertexPositionColorTexture[] vertexCache = Array.Empty<VertexPositionColorTexture>();

	internal short[] indexCache = Array.Empty<short>();

	internal UnifiedRandom RNG = new UnifiedRandom(0);

	public Texture2D BarkTexture;

	public float MaxDistanceBeforeCutoff;

	public float DistanceUsedForTrunk;

	public float BranchMaxBendFactor;

	public float BranchTurnAngleVariance;

	public float MinBranchLength;

	public float TrunkWidth;

	public float ChanceToCreateNewBranches;

	public float VerticalStretchFactor;

	public float DownwardBiasFactor;

	public float BranchGrowthWidthDecay;

	public int MaxCutoffBranchesPerBranch;

	public const int ControlPointCountPerBranch = 8;

	public BasicEffect BasicShader
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (!Main.dedServ && basicShader == null)
			{
				basicShader = new BasicEffect(((Game)Main.instance).GraphicsDevice)
				{
					VertexColorEnabled = true,
					TextureEnabled = true
				};
			}
			return basicShader;
		}
	}

	public static int GetSeed(int x, int y)
	{
		return x * 55867 + y * 49311329 + WorldGen.currentWorldSeed.GetHashCode();
	}

	public virtual void DrawThingAtEndOfBranch(Branch branch)
	{
	}

	public Dictionary<Branch, List<Branch>> GenerateBranches(Point p)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		RNG = new UnifiedRandom(GetSeed(p.X, p.Y));
		float cutoffDistance = MaxDistanceBeforeCutoff;
		float trunkDirection = RNG.NextFloatDirection() * BranchTurnAngleVariance * 0.1f - (float)Math.PI / 2f;
		float trunkSize = RNG.NextFloat(-8f, 8f) + DistanceUsedForTrunk;
		float distanceTraversed = trunkSize;
		Vector2 startOfTrunk = Vector2.UnitY * 10f;
		Vector2 endOfTrunk = startOfTrunk + trunkDirection.ToRotationVector2() * trunkSize;
		Branch trunk = GenerateBranchCurve(startOfTrunk, endOfTrunk, TrunkWidth, TrunkWidth);
		Dictionary<Branch, List<Branch>> existingBranches = new Dictionary<Branch, List<Branch>> { [trunk] = new List<Branch>() };
		int tries = 0;
		while (distanceTraversed < cutoffDistance)
		{
			tries++;
			if (tries >= 1500)
			{
				break;
			}
			if (RNG.NextFloat() > ChanceToCreateNewBranches)
			{
				List<Branch> potentialBranchesToExtend = (from b in existingBranches
					where b.Key.CurveLength < trunkSize * 0.4f && b.Key.CurveLength > MinBranchLength
					select b.Key).ToList();
				if (potentialBranchesToExtend.Count > 0)
				{
					Branch branchToExtend = RNG.Next(potentialBranchesToExtend);
					float lengthToAdd = branchToExtend.CurveLength * 0.12f;
					extendLengthOfBranch(branchToExtend, lengthToAdd);
					distanceTraversed += lengthToAdd;
				}
				continue;
			}
			List<Branch> validBranches = (from b in existingBranches
				where b.Value.Count < MaxCutoffBranchesPerBranch && b.Key.EndingWidth >= 6f
				select b.Key).ToList();
			if (validBranches.Count <= 0)
			{
				continue;
			}
			Branch branchToAttachTo = RNG.Next(validBranches);
			float maxBranchAngleVariance = BranchTurnAngleVariance * ((branchToAttachTo == trunk) ? 2.1f : 1f);
			float directionOfNextBranch = branchToAttachTo.Direction + RNG.NextFloatDirection() * maxBranchAngleVariance;
			float downwardBiasFactor = DownwardBiasFactor;
			float downwardBiasFromGeneration = Utils.Remap(branchToAttachTo.Generation, 0f, 5f, 0f, 0.8f);
			downwardBiasFactor = MathHelper.Clamp(downwardBiasFactor + downwardBiasFromGeneration, 0f, 0.95f);
			if (downwardBiasFactor > 0f && branchToAttachTo != trunk)
			{
				float randomBias = RNG.NextFloat(0.67f, 1f) * downwardBiasFactor;
				directionOfNextBranch = Vector2.Lerp(directionOfNextBranch.ToRotationVector2(), Vector2.UnitY, randomBias).ToRotation();
			}
			if (existingBranches[branchToAttachTo].Count < 1 || !existingBranches[branchToAttachTo].Any(delegate(Branch b)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				return b.Direction.ToRotationVector2().AngleBetween(directionOfNextBranch.ToRotationVector2()) < 0.12f;
			}))
			{
				float lengthOfNextBranch = MathHelper.Max(MinBranchLength, branchToAttachTo.CurveLength * RNG.NextFloat(0.5f, 0.925f));
				Vector2 start = branchToAttachTo.EndOfCurve;
				Vector2 end = start + directionOfNextBranch.ToRotationVector2() * lengthOfNextBranch;
				if (!((float)validBranches.Count((Branch b) => MathHelper.Distance(b.EndOfCurve.Y, end.Y) < 50f) >= 4f))
				{
					Branch newBranch = GenerateBranchCurve(start, end, branchToAttachTo.EndingWidth, branchToAttachTo.EndingWidth * BranchGrowthWidthDecay, branchToAttachTo);
					existingBranches[branchToAttachTo].Add(newBranch);
					existingBranches[newBranch] = new List<Branch>();
					distanceTraversed += lengthOfNextBranch;
				}
			}
		}
		foreach (Branch branch in from b in existingBranches
			where b.Value.Count <= 0
			select b.Key)
		{
			branch.EndingWidth = MathHelper.Min(3f, branch.EndingWidth);
		}
		return existingBranches;
		void extendLengthOfBranch(Branch branch2, float num)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			branch2.CurveLength += num;
			branch2.EndOfCurve += branch2.Direction.ToRotationVector2() * num;
			foreach (Branch attachedBranch in existingBranches[branch2])
			{
				extendLengthOfBranch(attachedBranch, num);
			}
		}
	}

	public void GetVertexData(Point p, out List<VertexPositionColorTexture> vertices, out List<short> indices, out IEnumerable<Branch> outwardmostBranches)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		vertices = new List<VertexPositionColorTexture>();
		indices = new List<short>();
		Dictionary<Branch, List<Branch>> branchData = GenerateBranches(p);
		IEnumerable<Branch> source = branchData.Select((KeyValuePair<Branch, List<Branch>> b) => b.Key);
		outwardmostBranches = from b in branchData
			where (float)b.Value.Count <= 0f
			select b.Key;
		int batchIndex = 0;
		Vector2 screenOffset = (p.ToWorldCoordinates() - Main.screenPosition).Floor();
		Texture2D barkTexture = BarkTexture;
		Vector2 topLeftTexCoord = default(Vector2);
		Vector2 topRightTexCoord = default(Vector2);
		Vector2 bottomLeftTexCoord = default(Vector2);
		Vector2 bottomRightTexCoord = default(Vector2);
		foreach (Branch branch in source.OrderBy((Branch b) => b.EndOfCurve.Y))
		{
			int pointCount = 12;
			List<Vector2> smoothenedPoints = branch.Curve.GetPoints(pointCount + 1);
			Vector2? prevBottomLeft = null;
			Vector2? prevBottomRight = null;
			if (branch.PreviousBranch != null)
			{
				Vector2 previousOrthogonalDirection = (branch.PreviousBranch.Direction + (float)Math.PI / 2f).ToRotationVector2();
				prevBottomLeft = branch.PreviousBranch.EndOfCurve + previousOrthogonalDirection * branch.PreviousBranch.EndingWidth * 0.5f;
				prevBottomRight = branch.PreviousBranch.EndOfCurve - previousOrthogonalDirection * branch.PreviousBranch.EndingWidth * 0.5f;
			}
			for (int i = 0; i < pointCount; i++)
			{
				Vector2 top = smoothenedPoints[i];
				Vector2 bottom = smoothenedPoints[i + 1];
				float topCompletionRatio = (float)i / (float)pointCount;
				float bottomCompletionRatio = (float)(i + 1) / (float)pointCount;
				if ((float)i == (float)pointCount - 1f)
				{
					topCompletionRatio = 1f;
					bottomCompletionRatio = 1f;
					bottom = branch.EndOfCurve;
				}
				float topWidth = MathHelper.Lerp(branch.StartingWidth, branch.EndingWidth, topCompletionRatio);
				float bottomWidth = MathHelper.Lerp(branch.StartingWidth, branch.EndingWidth, bottomCompletionRatio);
				float topTexCoord = branch.CurveLength * topCompletionRatio / VerticalStretchFactor / (float)barkTexture.Height;
				float bottomTexCoord = branch.CurveLength * bottomCompletionRatio / VerticalStretchFactor / (float)barkTexture.Height;
				if (VerticalStretchFactor <= 0f)
				{
					topTexCoord = topWidth;
					bottomTexCoord = bottomWidth;
				}
				float stretchedHorizontalCoordTop = topWidth / (float)barkTexture.Width;
				float stretchedHorizontalCoordBottom = bottomWidth / (float)barkTexture.Width;
				if (topWidth > (float)barkTexture.Width * 0.5f)
				{
					stretchedHorizontalCoordTop = 1f;
				}
				if (bottomWidth > (float)barkTexture.Width * 0.5f)
				{
					stretchedHorizontalCoordBottom = 1f;
				}
				((Vector2)(ref topLeftTexCoord))._002Ector(stretchedHorizontalCoordTop, topTexCoord);
				((Vector2)(ref topRightTexCoord))._002Ector(0f, topTexCoord);
				((Vector2)(ref bottomLeftTexCoord))._002Ector(stretchedHorizontalCoordBottom, bottomTexCoord);
				((Vector2)(ref bottomRightTexCoord))._002Ector(0f, bottomTexCoord);
				Vector2 orthogonalDirection = (bottom - top).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866);
				Vector2 topLeft = (Vector2)(((_003F?)prevBottomLeft) ?? (top + orthogonalDirection * topWidth * 0.5f));
				Vector2 topRight = (Vector2)(((_003F?)prevBottomRight) ?? (top - orthogonalDirection * topWidth * 0.5f));
				Vector2 bottomLeft = bottom + orthogonalDirection * bottomWidth * 0.5f;
				Vector2 bottomRight = bottom - orthogonalDirection * bottomWidth * 0.5f;
				Vector2 lightOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)(-Main.offScreenRange)));
				Color topLeftColor = Lighting.GetColor((topLeft + p.ToWorldCoordinates() + lightOffset).ToTileCoordinates());
				Color topRightColor = Lighting.GetColor((topRight + p.ToWorldCoordinates() + lightOffset).ToTileCoordinates());
				Color bottomLeftColor = Lighting.GetColor((bottomLeft + p.ToWorldCoordinates() + lightOffset).ToTileCoordinates());
				Color bottomRightColor = Lighting.GetColor((bottomRight + p.ToWorldCoordinates() + lightOffset).ToTileCoordinates());
				vertices.Add(new VertexPositionColorTexture(new Vector3(topLeft.Floor() + screenOffset, 0f), topLeftColor, topLeftTexCoord));
				vertices.Add(new VertexPositionColorTexture(new Vector3(topRight.Floor() + screenOffset, 0f), topRightColor, topRightTexCoord));
				vertices.Add(new VertexPositionColorTexture(new Vector3(bottomRight.Floor() + screenOffset, 0f), bottomRightColor, bottomRightTexCoord));
				vertices.Add(new VertexPositionColorTexture(new Vector3(bottomLeft.Floor() + screenOffset, 0f), bottomLeftColor, bottomLeftTexCoord));
				indices.Add((short)(batchIndex * 4));
				indices.Add((short)(batchIndex * 4 + 1));
				indices.Add((short)(batchIndex * 4 + 2));
				indices.Add((short)(batchIndex * 4));
				indices.Add((short)(batchIndex * 4 + 2));
				indices.Add((short)(batchIndex * 4 + 3));
				prevBottomLeft = bottomLeft;
				prevBottomRight = bottomRight;
				batchIndex++;
			}
		}
	}

	public void Draw(Point p)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		GetVertexData(p, out var vertices, out var indices, out var outwardmostBranches);
		vertexCache = vertices.ToArray();
		indexCache = indices.ToArray();
		PreviousPoint = p;
		CalamityUtils.CalculatePerspectiveMatricies(out var effectView, out var effectProjection);
		BasicShader.Texture = BarkTexture;
		BasicShader.View = effectView;
		BasicShader.Projection = effectProjection;
		((Game)Main.instance).GraphicsDevice.Textures[0] = (Texture)(object)BarkTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
		((Game)Main.instance).GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>((PrimitiveType)0, vertexCache, 0, vertexCache.Length, indexCache, 0, indexCache.Length / 3);
		foreach (Branch outwardmostBranch in outwardmostBranches)
		{
			DrawThingAtEndOfBranch(outwardmostBranch);
		}
	}

	public void UseDefaultSize()
	{
		TileObjectData.newTile.Width = (int)Math.Ceiling(TrunkWidth / 16f);
		TileObjectData.newTile.Height = (int)Math.Ceiling(DistanceUsedForTrunk / 16f);
		TileObjectData.newTile.Origin = new Point16(TileObjectData.newTile.Width / 2, TileObjectData.newTile.Height - 1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = Enumerable.Repeat(16, TileObjectData.newTile.Height).ToArray();
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 2;
	}

	public Branch GenerateBranchCurve(Vector2 start, Vector2 end, float startWidth, float endWidth, Branch previousBranch = null)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		float distanceBetweenPoints = Vector2.Distance(start, end);
		Vector2[] initialPoints = (Vector2[])(object)new Vector2[8];
		Vector2 orthogonalDirection = (end - start).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866);
		for (int i = 0; i < 8; i++)
		{
			initialPoints[i] = Vector2.Lerp(start, end, (float)i / 7f);
		}
		float bendFactor = (float)Math.Pow(RNG.NextFloat(), 0.66) * (float)RNG.NextBool().ToDirectionInt() * BranchMaxBendFactor;
		bendFactor = MathHelper.Lerp(bendFactor, (float)Math.Sign(bendFactor) * BranchMaxBendFactor, Utils.GetLerpValue(DistanceUsedForTrunk * 0.4f, DistanceUsedForTrunk * 0.75f, distanceBetweenPoints, clamped: true));
		ref Vector2 reference = ref initialPoints[4];
		reference += orthogonalDirection * RNG.NextFloatDirection() * distanceBetweenPoints * bendFactor;
		return new Branch(new BezierCurve(initialPoints), end, distanceBetweenPoints, (end - start).ToRotation(), startWidth, endWidth, previousBranch);
	}
}
