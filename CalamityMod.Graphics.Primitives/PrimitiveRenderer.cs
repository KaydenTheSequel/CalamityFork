using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Primitives;

[Autoload(true, Side = ModSide.Client)]
public sealed class PrimitiveRenderer : ModSystem
{
	private static DynamicVertexBuffer VertexBuffer;

	private static DynamicIndexBuffer IndexBuffer;

	private static PrimitiveSettings MainSettings;

	private static PrimitiveTopology ActiveTopology;

	private static Vector2[] MainPositions;

	private static Vector2[] MainTangents;

	private static Vector2[] MainNormals;

	private static VertexPosition2DColorTexture[] MainVertices;

	private static short[] MainIndices;

	private static VertexPositionColor[] WireframeVertices;

	private static int WireframeVertexCount;

	private static BasicEffect WireframeEffect;

	private static int[] NonSmoothIndexScratch;

	private static short StartCapCenterIndex;

	private static short EndCapCenterIndex;

	private const short MaxPositions = 1000;

	private const short MaxVertices = 3072;

	private const short MaxIndices = 8192;

	private static readonly List<Vector2> ControlPointsCache = new List<Vector2>(1000);

	private static short PositionsIndex;

	private static float[] MainCompletionRatios;

	private static float TotalTrailLength;

	public const float Epsilon = 1E-06f;

	private static short VerticesIndex;

	private static short IndicesIndex;

	public override void OnModLoad()
	{
		Main.QueueMainThreadAction(delegate
		{
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Expected O, but got Unknown
			MainPositions = (Vector2[])(object)new Vector2[1000];
			MainVertices = new VertexPosition2DColorTexture[3072];
			MainIndices = new short[8192];
			MainCompletionRatios = new float[1000];
			MainTangents = (Vector2[])(object)new Vector2[1000];
			MainNormals = (Vector2[])(object)new Vector2[1000];
			WireframeVertices = (VertexPositionColor[])(object)new VertexPositionColor[8000];
			NonSmoothIndexScratch = new int[1000];
			if (VertexBuffer == null)
			{
				VertexBuffer = new DynamicVertexBuffer(((Game)Main.instance).GraphicsDevice, VertexPosition2DColorTexture.VertexDeclaration2D, 3072, (BufferUsage)1);
			}
			if (IndexBuffer == null)
			{
				IndexBuffer = new DynamicIndexBuffer(((Game)Main.instance).GraphicsDevice, (IndexElementSize)0, 8192, (BufferUsage)1);
			}
			if (WireframeEffect == null)
			{
				WireframeEffect = new BasicEffect(((Game)Main.instance).GraphicsDevice)
				{
					VertexColorEnabled = true,
					TextureEnabled = false,
					LightingEnabled = false
				};
			}
		});
	}

	public override void OnModUnload()
	{
		Main.QueueMainThreadAction(delegate
		{
			MainPositions = null;
			MainVertices = null;
			MainIndices = null;
			MainCompletionRatios = null;
			MainTangents = null;
			MainNormals = null;
			WireframeVertices = null;
			NonSmoothIndexScratch = null;
			DynamicVertexBuffer vertexBuffer = VertexBuffer;
			if (vertexBuffer != null)
			{
				((GraphicsResource)vertexBuffer).Dispose();
			}
			VertexBuffer = null;
			DynamicIndexBuffer indexBuffer = IndexBuffer;
			if (indexBuffer != null)
			{
				((GraphicsResource)indexBuffer).Dispose();
			}
			IndexBuffer = null;
			BasicEffect wireframeEffect = WireframeEffect;
			if (wireframeEffect != null)
			{
				((GraphicsResource)wireframeEffect).Dispose();
			}
			WireframeEffect = null;
		});
	}

	private static void PerformPixelationSafetyChecks(PrimitiveSettings settings)
	{
		if (settings.Pixelate && !PrimitivePixelationSystem.CurrentlyRendering)
		{
			throw new Exception("Error: Primitives using pixelation MUST be prepared/rendered from the IPixelatedPrimitiveRenderer.RenderPixelatedPrimitives method, did you forget to use the interface?");
		}
		if (!settings.Pixelate && PrimitivePixelationSystem.CurrentlyRendering)
		{
			throw new Exception("Error: Primitives not using pixelation MUST NOT be prepared/rendered from the IPixelatedPrimitiveRenderer.RenderPixelatedPrimitives method.");
		}
	}

	public static void RenderTrail(List<Vector2> positions, PrimitiveSettings settings, int? pointsToCreate = null)
	{
		RenderTrail(positions.ToArray(), settings, pointsToCreate);
	}

	public static void RenderTrail(Vector2[] positions, PrimitiveSettings settings, int? pointsToCreate = null)
	{
		PerformPixelationSafetyChecks(settings);
		if (positions.Length <= 2 || positions.Length > 1000)
		{
			return;
		}
		int desiredPointCount = pointsToCreate ?? positions.Length;
		desiredPointCount = Math.Clamp(desiredPointCount, 2, 1000);
		MainSettings = settings;
		ActiveTopology = ((settings.CapStyle == PrimitiveCapStyle.None) ? settings.Topology : PrimitiveTopology.TriangleList);
		if (AssignPointsRectangleTrail(positions, settings, desiredPointCount))
		{
			AssignCompletionData();
			if (PositionsIndex > 2)
			{
				AssignVerticesRectangleTrail();
				AssignIndices();
				PrivateRender();
			}
		}
	}

	private static void PrivateRender()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Invalid comparison between Unknown and I4
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		if (VerticesIndex <= 3)
		{
			return;
		}
		if (ActiveTopology == PrimitiveTopology.TriangleList)
		{
			if (IndicesIndex < 6 || IndicesIndex % 3 != 0)
			{
				return;
			}
		}
		else if (ActiveTopology == PrimitiveTopology.TriangleStrip && IndicesIndex < 4)
		{
			return;
		}
		((Game)Main.instance).GraphicsDevice.RasterizerState = RasterizerState.CullNone;
		((Game)Main.instance).GraphicsDevice.RasterizerState.ScissorTestEnable = true;
		((Game)Main.instance).GraphicsDevice.ScissorRectangle = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
		Matrix view;
		Matrix projection;
		if (MainSettings.Pixelate || MainSettings.UseUnscaledMatrices)
		{
			CalcuatePixelatedPerspectiveMatrices(out view, out projection);
		}
		else
		{
			CalamityUtils.CalculatePerspectiveMatricies(out view, out projection);
		}
		MiscShaderData obj = MainSettings.Shader ?? GameShaders.Misc["CalamityMod:StandardPrimitiveShader"];
		obj.Shader.Parameters["uWorldViewProjection"].SetValue(view * projection);
		obj.Apply();
		VertexBuffer.SetData<VertexPosition2DColorTexture>(MainVertices, 0, (int)VerticesIndex, (SetDataOptions)1);
		IndexBuffer.SetData<short>(MainIndices, 0, (int)IndicesIndex, (SetDataOptions)1);
		((Game)Main.instance).GraphicsDevice.SetVertexBuffer((VertexBuffer)(object)VertexBuffer);
		((Game)Main.instance).GraphicsDevice.Indices = (IndexBuffer)(object)IndexBuffer;
		PrimitiveType primitiveType = (PrimitiveType)(ActiveTopology == PrimitiveTopology.TriangleStrip);
		int primitiveCount = (((int)primitiveType == 1) ? Math.Max(IndicesIndex - 2, 0) : (IndicesIndex / 3));
		((Game)Main.instance).GraphicsDevice.DrawIndexedPrimitives(primitiveType, 0, 0, (int)VerticesIndex, 0, primitiveCount);
		if (MainSettings.DebugWireframe && WireframeEffect != null && TryBuildWireframeGeometry(MainSettings.WireframeColor, out var lineCount))
		{
			DrawWireframe(view, projection, lineCount);
		}
	}

	private static bool AssignPointsRectangleTrail(Vector2[] positions, PrimitiveSettings settings, int pointsToCreate)
	{
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		if (!settings.Smoothen)
		{
			PositionsIndex = 0;
			int validCount = 0;
			for (int i = 0; i < positions.Length; i++)
			{
				if (!(positions[i] == Vector2.Zero))
				{
					NonSmoothIndexScratch[validCount++] = i;
				}
			}
			if (validCount <= 2)
			{
				return false;
			}
			int lastIndex = validCount - 1;
			float inversePointCount = 1f / (float)(pointsToCreate - 1);
			float lastIndexFloat = lastIndex;
			for (int j = 0; j < pointsToCreate; j++)
			{
				float completionRatio = (float)j * inversePointCount;
				float num = completionRatio * lastIndexFloat;
				int currentIndex = (int)num;
				int nextIndex = Math.Min(currentIndex + 1, lastIndex);
				float localInterpolant = num - (float)currentIndex;
				Vector2 val = positions[NonSmoothIndexScratch[currentIndex]];
				Vector2 nextPoint = positions[NonSmoothIndexScratch[nextIndex]];
				Vector2 interpolatedWorld = Vector2.Lerp(val, nextPoint, localInterpolant);
				Vector2 finalPos = interpolatedWorld - Main.screenPosition;
				if (settings.OffsetFunction != null)
				{
					finalPos += settings.OffsetFunction(completionRatio, interpolatedWorld);
				}
				MainPositions[PositionsIndex++] = finalPos;
			}
			return true;
		}
		PositionsIndex = 0;
		List<Vector2> controlPoints = ControlPointsCache;
		controlPoints.Clear();
		for (int k = 0; k < positions.Length; k++)
		{
			if (!(positions[k] == Vector2.Zero))
			{
				float completionRatio2 = (float)k / (float)positions.Length;
				Vector2 offset = -Main.screenPosition;
				if (settings.OffsetFunction != null)
				{
					offset += settings.OffsetFunction(completionRatio2, positions[k]);
				}
				controlPoints.Add(positions[k] + offset);
			}
		}
		int controlCount = controlPoints.Count;
		if (controlCount <= 1)
		{
			controlPoints.Clear();
			return false;
		}
		int segmentCount = controlCount - 1;
		if (settings.SmoothingSegments > 0)
		{
			int segmentsPerEdge = Math.Max(1, settings.SmoothingSegments);
			PrimitiveSmoothingType smoothingType = settings.SmoothingType;
			for (int segment = 0; segment < segmentCount; segment++)
			{
				Vector2 p0 = controlPoints[Math.Max(segment - 1, 0)];
				Vector2 p1 = controlPoints[segment];
				Vector2 p2 = controlPoints[segment + 1];
				Vector2 p3 = controlPoints[Math.Min(segment + 2, controlCount - 1)];
				for (int step = ((segment != 0) ? 1 : 0); step <= segmentsPerEdge; step++)
				{
					if (PositionsIndex >= 999)
					{
						controlPoints.Clear();
						return true;
					}
					float localT = (float)step / (float)segmentsPerEdge;
					Vector2 point = EvaluateCurve(smoothingType, p0, p1, p2, p3, localT, settings);
					MainPositions[PositionsIndex++] = point;
				}
			}
			controlPoints.Clear();
			return true;
		}
		PositionsIndex = 1;
		float controlCountMinusOne = (float)controlCount - 1f;
		PrimitiveSmoothingType legacyType = settings.SmoothingType;
		for (int l = 0; l < pointsToCreate; l++)
		{
			if (PositionsIndex >= 999)
			{
				break;
			}
			float num2 = (float)l / (float)pointsToCreate * controlCountMinusOne;
			int localSplineIndex = (int)num2;
			float localSplineInterpolant = num2 - (float)localSplineIndex;
			Vector2 p4 = controlPoints[Math.Max(localSplineIndex - 1, 0)];
			Vector2 p5 = controlPoints[localSplineIndex];
			Vector2 p6 = controlPoints[Math.Min(localSplineIndex + 1, controlCount - 1)];
			Vector2 p7 = controlPoints[Math.Min(localSplineIndex + 2, controlCount - 1)];
			MainPositions[PositionsIndex] = EvaluateCurve(legacyType, p4, p5, p6, p7, localSplineInterpolant, settings);
			PositionsIndex++;
		}
		MainPositions[0] = controlPoints[0];
		MainPositions[PositionsIndex] = controlPoints[controlCount - 1];
		PositionsIndex++;
		controlPoints.Clear();
		return true;
	}

	private static void AssignCompletionData()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		TotalTrailLength = 0f;
		if (PositionsIndex <= 0)
		{
			return;
		}
		MainCompletionRatios[0] = 0f;
		for (int i = 1; i < PositionsIndex; i++)
		{
			float segmentLength = Vector2.Distance(MainPositions[i], MainPositions[i - 1]);
			TotalTrailLength += segmentLength;
			MainCompletionRatios[i] = TotalTrailLength;
		}
		if (PositionsIndex <= 0)
		{
			return;
		}
		if (TotalTrailLength > 1E-06f)
		{
			float inverseTotal = 1f / TotalTrailLength;
			int lastIndex = PositionsIndex - 1;
			if (Vector.IsHardwareAccelerated && PositionsIndex - 1 >= Vector<float>.Count)
			{
				Vector<float> scale = new Vector<float>(inverseTotal);
				int j = 1;
				for (int upperBound = lastIndex - Vector<float>.Count + 1; j <= upperBound; j += Vector<float>.Count)
				{
					(new Vector<float>(MainCompletionRatios, j) * scale).CopyTo(MainCompletionRatios, j);
				}
				for (; j <= lastIndex; j++)
				{
					MainCompletionRatios[j] *= inverseTotal;
				}
			}
			else
			{
				for (int k = 1; k < PositionsIndex; k++)
				{
					MainCompletionRatios[k] *= inverseTotal;
				}
			}
			MainCompletionRatios[PositionsIndex - 1] = 1f;
		}
		else
		{
			for (int l = 1; l < PositionsIndex; l++)
			{
				MainCompletionRatios[l] = 0f;
			}
		}
	}

	private static void AssignVerticesRectangleTrail()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		VerticesIndex = 0;
		StartCapCenterIndex = -1;
		EndCapCenterIndex = -1;
		ComputeFrameData();
		Vector2 leftCurrentTextureCoord = default(Vector2);
		Vector2 rightCurrentTextureCoord = default(Vector2);
		for (int i = 0; i < PositionsIndex; i++)
		{
			float completionRatio = GetCompletionRatioForIndex(i);
			float widthAtVertex = Math.Max(MainSettings.WidthFunction(completionRatio, MainPositions[i]), 0f);
			Color vertexColor = MainSettings.ColorFunction(completionRatio, MainPositions[i]);
			float textureU = ComputeTextureCoordinateForIndex(i, completionRatio);
			ComputeEdgePositions(i, widthAtVertex, out var left, out var right, out var effectiveHalfWidth);
			if (i == 0 && MainSettings.InitialVertexPositionsOverride.HasValue && MainSettings.InitialVertexPositionsOverride.Value.Item1 != Vector2.Zero && MainSettings.InitialVertexPositionsOverride.Value.Item2 != Vector2.Zero)
			{
				left = MainSettings.InitialVertexPositionsOverride.Value.Item1;
				right = MainSettings.InitialVertexPositionsOverride.Value.Item2;
				effectiveHalfWidth = Math.Max(Vector2.Distance(left, right) * 0.5f, 1E-06f);
			}
			effectiveHalfWidth = Math.Max(effectiveHalfWidth, 1E-06f);
			((Vector2)(ref leftCurrentTextureCoord))._002Ector(textureU, 0.5f - effectiveHalfWidth * 0.5f);
			((Vector2)(ref rightCurrentTextureCoord))._002Ector(textureU, 0.5f + effectiveHalfWidth * 0.5f);
			MainVertices[VerticesIndex] = new VertexPosition2DColorTexture(left, vertexColor, leftCurrentTextureCoord, effectiveHalfWidth);
			VerticesIndex++;
			MainVertices[VerticesIndex] = new VertexPosition2DColorTexture(right, vertexColor, rightCurrentTextureCoord, effectiveHalfWidth);
			VerticesIndex++;
		}
		AddCaps();
	}

	private static void AddCaps()
	{
		if (MainSettings.CapStyle != PrimitiveCapStyle.None && PositionsIndex > 0 && ActiveTopology != PrimitiveTopology.TriangleStrip)
		{
			StartCapCenterIndex = TryCreateCapVertex(0);
			if (PositionsIndex > 1)
			{
				EndCapCenterIndex = TryCreateCapVertex(PositionsIndex - 1);
			}
		}
	}

	private static short TryCreateCapVertex(int positionIndex)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (VerticesIndex >= 3071)
		{
			return -1;
		}
		int leftVertexIndex = positionIndex * 2;
		int rightVertexIndex = leftVertexIndex + 1;
		if (rightVertexIndex >= VerticesIndex)
		{
			return -1;
		}
		ref VertexPosition2DColorTexture reference = ref MainVertices[leftVertexIndex];
		ref VertexPosition2DColorTexture rightVertex = ref MainVertices[rightVertexIndex];
		Vector2 centerPosition = MainPositions[positionIndex];
		Color centerColor = Color.Lerp(reference.Color, rightVertex.Color, 0.5f);
		float centerHalfWidth = Math.Max(Math.Max(reference.TextureCoordinates.Z, rightVertex.TextureCoordinates.Z), 1E-06f);
		float centerU = (reference.TextureCoordinates.X + rightVertex.TextureCoordinates.X) * 0.5f;
		Vector2 centerTexcoord = default(Vector2);
		((Vector2)(ref centerTexcoord))._002Ector(centerU, 0.5f);
		short verticesIndex = VerticesIndex;
		MainVertices[VerticesIndex++] = new VertexPosition2DColorTexture(centerPosition, centerColor, centerTexcoord, centerHalfWidth);
		return verticesIndex;
	}

	private static float GetCompletionRatioForIndex(int index)
	{
		if (PositionsIndex <= 0)
		{
			return 0f;
		}
		if (index <= 0)
		{
			return MainCompletionRatios[0];
		}
		if (index >= PositionsIndex)
		{
			return MainCompletionRatios[PositionsIndex - 1];
		}
		return MainCompletionRatios[index];
	}

	private static float ComputeTextureCoordinateForIndex(int index, float completionRatio)
	{
		float clampedCompletion = MathHelper.Clamp(completionRatio, 0f, 1f);
		if (MainSettings.TextureCoordinateFunction != null)
		{
			return MainSettings.TextureCoordinateFunction(clampedCompletion);
		}
		float cycleLength = MainSettings.TextureCycleLength;
		if (Math.Abs(cycleLength) <= 1E-06f)
		{
			cycleLength = ((cycleLength >= 0f) ? 1f : (-1f));
		}
		if (MainSettings.TextureCoordinateMode == PrimitiveTextureMode.Distance)
		{
			return (clampedCompletion * TotalTrailLength + MainSettings.TextureScrollOffset) / cycleLength;
		}
		return clampedCompletion * cycleLength + MainSettings.TextureScrollOffset;
	}

	private static void ComputeFrameData()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		if (PositionsIndex <= 0)
		{
			return;
		}
		Vector2 fallbackTangent = Vector2.UnitX;
		for (int i = 0; i < PositionsIndex; i++)
		{
			Vector2 tangent = ComputeTangent(i, fallbackTangent);
			tangent = tangent.SafeNormalize(fallbackTangent.SafeNormalize(Vector2.UnitX));
			MainTangents[i] = tangent;
			fallbackTangent = tangent;
		}
		Vector2 previousNormal = Vector2.Zero;
		Vector2 baseNormal = default(Vector2);
		for (int j = 0; j < PositionsIndex; j++)
		{
			Vector2 tangent2 = MainTangents[j];
			if (((Vector2)(ref tangent2)).LengthSquared() <= 1E-06f)
			{
				tangent2 = fallbackTangent.SafeNormalize(Vector2.UnitX);
			}
			((Vector2)(ref baseNormal))._002Ector(0f - tangent2.Y, tangent2.X);
			Vector2 normal;
			if (MainSettings.FrameTransportMode == PrimitiveFrameTransportMode.ParallelTransport && j > 0 && ((Vector2)(ref previousNormal)).LengthSquared() > 1E-06f)
			{
				Vector2 val = MainTangents[j - 1];
				float cosine = MathHelper.Clamp(Vector2.Dot(val, tangent2), -1f, 1f);
				float sine = Cross(val, tangent2);
				normal = new Vector2(cosine * previousNormal.X - sine * previousNormal.Y, sine * previousNormal.X + cosine * previousNormal.Y);
			}
			else
			{
				normal = baseNormal;
			}
			if (((Vector2)(ref normal)).LengthSquared() <= 1E-06f)
			{
				normal = ((((Vector2)(ref previousNormal)).LengthSquared() > 1E-06f) ? previousNormal : baseNormal);
			}
			normal = normal.SafeNormalize((((Vector2)(ref previousNormal)).LengthSquared() > 1E-06f) ? previousNormal : Vector2.UnitY);
			MainNormals[j] = normal;
			previousNormal = normal;
		}
	}

	private static Vector2 ComputeTangent(int index, Vector2 fallback)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		int last = PositionsIndex - 1;
		Vector2 tangent;
		if (PositionsIndex <= 1)
		{
			tangent = fallback;
		}
		else if (index <= 0)
		{
			tangent = MainPositions[1] - MainPositions[0];
		}
		else if (index >= last)
		{
			tangent = MainPositions[last] - MainPositions[last - 1];
		}
		else
		{
			Vector2 forward = MainPositions[index + 1] - MainPositions[index];
			Vector2 backward = MainPositions[index] - MainPositions[index - 1];
			tangent = forward + backward;
			if (((Vector2)(ref tangent)).LengthSquared() <= 1E-06f)
			{
				tangent = ((((Vector2)(ref forward)).LengthSquared() >= ((Vector2)(ref backward)).LengthSquared()) ? forward : backward);
			}
		}
		if (((Vector2)(ref tangent)).LengthSquared() <= 1E-06f)
		{
			tangent = ((((Vector2)(ref fallback)).LengthSquared() > 1E-06f) ? fallback : Vector2.UnitX);
		}
		return tangent;
	}

	private static Vector2 EvaluateCurve(PrimitiveSmoothingType type, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t, PrimitiveSettings settings)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		t = MathHelper.Clamp(t, 0f, 1f);
		switch (type)
		{
		case PrimitiveSmoothingType.Linear:
			return Vector2.Lerp(p1, p2, t);
		case PrimitiveSmoothingType.Cardinal:
		{
			float tension = MathHelper.Clamp(settings.SmoothingTension, -1f, 1f);
			float scale = (1f - tension) * 0.5f;
			Vector2 m2 = (p2 - p0) * scale;
			Vector2 m3 = (p3 - p1) * scale;
			return EvaluateHermiteSpan(p1, p2, m2, m3, t);
		}
		case PrimitiveSmoothingType.Hermite:
		{
			Vector2 m0 = 0.5f * (p2 - p0);
			Vector2 m1 = 0.5f * (p3 - p1);
			return EvaluateHermiteSpan(p1, p2, m0, m1, t);
		}
		case PrimitiveSmoothingType.CubicBezier:
		{
			Vector2 handle1 = p1 + (p2 - p0) / 3f;
			Vector2 handle2 = p2 - (p3 - p1) / 3f;
			return EvaluateBezierSpan(p1, handle1, handle2, p2, t);
		}
		default:
			return Vector2.CatmullRom(p0, p1, p2, p3, t);
		}
	}

	private static Vector2 EvaluateHermiteSpan(Vector2 start, Vector2 end, Vector2 tangentStart, Vector2 tangentEnd, float t)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		float t2 = t * t;
		float t3 = t2 * t;
		float num = 2f * t3 - 3f * t2 + 1f;
		float h10 = t3 - 2f * t2 + t;
		float h11 = -2f * t3 + 3f * t2;
		float h12 = t3 - t2;
		return num * start + h10 * tangentStart + h11 * end + h12 * tangentEnd;
	}

	private static Vector2 EvaluateBezierSpan(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		float u = 1f - t;
		float u2 = u * u;
		float num = u2 * u;
		float t2 = t * t;
		float t3 = t2 * t;
		return num * p0 + 3f * u2 * t * p1 + 3f * u * t2 * p2 + t3 * p3;
	}

	private static void ComputeEdgePositions(int index, float halfWidth, out Vector2 left, out Vector2 right, out float effectiveHalfWidth)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		Vector2 currentPosition = MainPositions[index];
		if (halfWidth <= 0f)
		{
			left = currentPosition;
			right = currentPosition;
			effectiveHalfWidth = 1E-06f;
			return;
		}
		Vector2 defaultNormal = MainNormals[index];
		if (((Vector2)(ref defaultNormal)).LengthSquared() <= 1E-06f)
		{
			defaultNormal = Vector2.UnitY;
		}
		if (MainSettings.JoinStyle == PrimitiveJoinStyle.Flat || PositionsIndex <= 2 || index == 0 || index == PositionsIndex - 1)
		{
			Vector2 offset = defaultNormal * halfWidth;
			left = currentPosition - offset;
			right = currentPosition + offset;
			effectiveHalfWidth = halfWidth;
			return;
		}
		Vector2 prevNormal = MainNormals[Math.Max(index - 1, 0)];
		if (((Vector2)(ref prevNormal)).LengthSquared() <= 1E-06f)
		{
			prevNormal = defaultNormal;
		}
		Vector2 nextNormal = MainNormals[Math.Min(index + 1, PositionsIndex - 1)];
		if (((Vector2)(ref nextNormal)).LengthSquared() <= 1E-06f)
		{
			nextNormal = defaultNormal;
		}
		switch (MainSettings.JoinStyle)
		{
		case PrimitiveJoinStyle.Smooth:
		{
			Vector2 averageNormal = (prevNormal + defaultNormal + nextNormal) * (1f / 3f);
			if (((Vector2)(ref averageNormal)).LengthSquared() <= 1E-06f)
			{
				averageNormal = defaultNormal;
			}
			Vector2 offset4 = averageNormal.SafeNormalize(defaultNormal) * halfWidth;
			left = currentPosition - offset4;
			right = currentPosition + offset4;
			effectiveHalfWidth = halfWidth;
			break;
		}
		case PrimitiveJoinStyle.Miter:
		{
			Vector2 val = prevNormal.SafeNormalize(defaultNormal);
			Vector2 next = nextNormal.SafeNormalize(defaultNormal);
			Vector2 miter = val + next;
			if (((Vector2)(ref miter)).LengthSquared() <= 1E-06f)
			{
				miter = defaultNormal;
			}
			miter = miter.SafeNormalize(defaultNormal);
			float denom = Vector2.Dot(miter, next);
			if (Math.Abs(denom) < 1E-06f)
			{
				denom = ((denom >= 0f) ? 1E-06f : (-1E-06f));
			}
			float miterLength = halfWidth / denom;
			float maxLength = halfWidth * MainSettings.JoinMiterLimit;
			miterLength = MathHelper.Clamp(miterLength, 0f - maxLength, maxLength);
			Vector2 offset3 = miter * miterLength;
			left = currentPosition - offset3;
			right = currentPosition + offset3;
			effectiveHalfWidth = Math.Max(Math.Abs(miterLength), 1E-06f);
			break;
		}
		default:
		{
			Vector2 offset2 = defaultNormal * halfWidth;
			left = currentPosition - offset2;
			right = currentPosition + offset2;
			effectiveHalfWidth = halfWidth;
			break;
		}
		}
	}

	private static bool TryBuildWireframeGeometry(Color lineColor, out int lineCount)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		lineCount = 0;
		if (WireframeVertices == null)
		{
			return false;
		}
		int segments = PositionsIndex - 1;
		if (segments <= 0)
		{
			return false;
		}
		WireframeVertexCount = 0;
		for (int i = 0; i < segments; i++)
		{
			int num = i * 2;
			int currentRight = num + 1;
			int nextLeft = num + 2;
			int nextRight = nextLeft + 1;
			AddWireframeLineFromIndices(num, nextLeft, lineColor);
			AddWireframeLineFromIndices(currentRight, nextRight, lineColor);
		}
		for (int j = 0; j < PositionsIndex; j++)
		{
			int num2 = j * 2;
			AddWireframeLineFromIndices(num2, num2 + 1, lineColor);
		}
		lineCount = WireframeVertexCount / 2;
		return lineCount > 0;
	}

	private static void AddWireframeLineFromIndices(int startVertexIndex, int endVertexIndex, Color color)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (WireframeVertexCount + 1 < WireframeVertices.Length)
		{
			int vertexCount = VerticesIndex;
			if (startVertexIndex < vertexCount && endVertexIndex < vertexCount)
			{
				ref VertexPosition2DColorTexture start = ref MainVertices[startVertexIndex];
				ref VertexPosition2DColorTexture end = ref MainVertices[endVertexIndex];
				WireframeVertices[WireframeVertexCount++] = new VertexPositionColor(new Vector3(start.Position, 0f), color);
				WireframeVertices[WireframeVertexCount++] = new VertexPositionColor(new Vector3(end.Position, 0f), color);
			}
		}
	}

	private static void DrawWireframe(Matrix view, Matrix projection, int lineCount)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (lineCount <= 0 || WireframeEffect == null)
		{
			return;
		}
		WireframeEffect.World = Matrix.Identity;
		WireframeEffect.View = view;
		WireframeEffect.Projection = projection;
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		foreach (EffectPass pass in ((Effect)WireframeEffect).CurrentTechnique.Passes)
		{
			pass.Apply();
			device.DrawUserPrimitives<VertexPositionColor>((PrimitiveType)2, WireframeVertices, 0, lineCount);
		}
	}

	private static void AssignIndices()
	{
		IndicesIndex = 0;
		if (ActiveTopology == PrimitiveTopology.TriangleStrip)
		{
			short i = 0;
			while (i < VerticesIndex && IndicesIndex < 8192)
			{
				MainIndices[IndicesIndex++] = i;
				i++;
			}
			return;
		}
		short i2 = 0;
		while (i2 < PositionsIndex - 2 && IndicesIndex + 5 < 8192)
		{
			short connectToIndex = (short)(i2 * 2);
			MainIndices[IndicesIndex] = connectToIndex;
			IndicesIndex++;
			MainIndices[IndicesIndex] = (short)(connectToIndex + 1);
			IndicesIndex++;
			MainIndices[IndicesIndex] = (short)(connectToIndex + 2);
			IndicesIndex++;
			MainIndices[IndicesIndex] = (short)(connectToIndex + 2);
			IndicesIndex++;
			MainIndices[IndicesIndex] = (short)(connectToIndex + 1);
			IndicesIndex++;
			MainIndices[IndicesIndex] = (short)(connectToIndex + 3);
			IndicesIndex++;
			i2++;
		}
		AppendCapTriangles();
	}

	private static void AppendCapTriangles()
	{
		if (MainSettings.CapStyle != PrimitiveCapStyle.None && PositionsIndex > 0)
		{
			if (StartCapCenterIndex >= 0)
			{
				AddTriangle(StartCapCenterIndex, 0, 1);
			}
			if (EndCapCenterIndex >= 0)
			{
				short num = (short)((PositionsIndex - 1) * 2);
				short rightIndex = (short)(num + 1);
				AddTriangle(num, rightIndex, EndCapCenterIndex);
			}
		}
	}

	private static void AddTriangle(short i0, short i1, short i2)
	{
		if (IndicesIndex + 2 < 8192)
		{
			MainIndices[IndicesIndex++] = i0;
			MainIndices[IndicesIndex++] = i1;
			MainIndices[IndicesIndex++] = i2;
		}
	}

	private static void CalcuatePixelatedPerspectiveMatrices(out Matrix viewMatrix, out Matrix projectionMatrix)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		projectionMatrix = Matrix.CreateOrthographicOffCenter(0f, (float)Main.screenWidth, (float)Main.screenHeight, 0f, -1f, 1f);
		viewMatrix = Matrix.Identity;
	}

	private static float Cross(Vector2 a, Vector2 b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return a.X * b.Y - a.Y * b.X;
	}
}
