using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public static class PrimitiveShapeBuilder
{
	public static PrimitiveMesh BuildRectangularQuad(Vector3 center, Vector2 size, Color color, Vector3 normal, Vector3 upHint, bool textured = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		BuildFrame(normal, upHint, out var right, out var up);
		Vector3 halfRight = right * (size.X * 0.5f);
		Vector3 halfUp = up * (size.Y * 0.5f);
		if (textured)
		{
			VertexPositionColorTexture[] vertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[4]
			{
				new VertexPositionColorTexture(center - halfRight - halfUp, color, new Vector2(0f, 1f)),
				new VertexPositionColorTexture(center + halfRight - halfUp, color, new Vector2(1f, 1f)),
				new VertexPositionColorTexture(center + halfRight + halfUp, color, new Vector2(1f, 0f)),
				new VertexPositionColorTexture(center - halfRight + halfUp, color, new Vector2(0f, 0f))
			};
			short[] indices = new short[6] { 0, 1, 2, 0, 2, 3 };
			return new PrimitiveMesh(vertices, indices, (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[4]
		{
			new VertexPositionColor(center - halfRight - halfUp, color),
			new VertexPositionColor(center + halfRight - halfUp, color),
			new VertexPositionColor(center + halfRight + halfUp, color),
			new VertexPositionColor(center - halfRight + halfUp, color)
		};
		short[] colorIndices = new short[6] { 0, 1, 2, 0, 2, 3 };
		return new PrimitiveMesh(colorVertices, colorIndices, (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildRegularPolygon(Vector3 center, float radius, int sides, Color color, Vector3 normal, Vector3 upHint, bool textured = false)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		if (sides < 3)
		{
			throw new ArgumentOutOfRangeException("sides", "Polygon requires at least three sides.");
		}
		BuildFrame(normal, upHint, out var right, out var up);
		if (textured)
		{
			VertexPositionColorTexture[] texturedVertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[sides + 1];
			texturedVertices[0] = new VertexPositionColorTexture(center, color, new Vector2(0.5f, 0.5f));
			float safeRadius = Math.Max(radius, 1E-06f);
			for (int i = 0; i < sides; i++)
			{
				Vector3 direction = PolarToCartesian(i, sides, right, up) * radius;
				Vector3 point = center + direction;
				float u = 0.5f + 0.5f * MathHelper.Clamp(Vector3.Dot(direction, right) / safeRadius, -1f, 1f);
				float v = 0.5f - 0.5f * MathHelper.Clamp(Vector3.Dot(direction, up) / safeRadius, -1f, 1f);
				texturedVertices[i + 1] = new VertexPositionColorTexture(point, color, new Vector2(u, v));
			}
			short[] indices = new short[sides * 3];
			for (int j = 0; j < sides; j++)
			{
				int next = (j + 1) % sides;
				int baseIndex = j * 3;
				indices[baseIndex] = 0;
				indices[baseIndex + 1] = (short)(j + 1);
				indices[baseIndex + 2] = (short)(next + 1);
			}
			return new PrimitiveMesh(texturedVertices, indices, (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[sides + 1];
		colorVertices[0] = new VertexPositionColor(center, color);
		for (int k = 0; k < sides; k++)
		{
			Vector3 direction2 = PolarToCartesian(k, sides, right, up) * radius;
			colorVertices[k + 1] = new VertexPositionColor(center + direction2, color);
		}
		short[] colorIndices = new short[sides * 3];
		for (int l = 0; l < sides; l++)
		{
			int next2 = (l + 1) % sides;
			int baseIndex2 = l * 3;
			colorIndices[baseIndex2] = 0;
			colorIndices[baseIndex2 + 1] = (short)(l + 1);
			colorIndices[baseIndex2 + 2] = (short)(next2 + 1);
		}
		return new PrimitiveMesh(colorVertices, colorIndices, (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildArbitraryPolygon(IReadOnlyList<Vector3> points, Color color, bool textured = false)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (points == null)
		{
			throw new ArgumentNullException("points");
		}
		if (points.Count < 3)
		{
			throw new ArgumentOutOfRangeException("points", "Polygon requires at least three points.");
		}
		if (textured)
		{
			float minX = points[0].X;
			float maxX = points[0].X;
			float minY = points[0].Y;
			float maxY = points[0].Y;
			for (int i = 1; i < points.Count; i++)
			{
				Vector3 p = points[i];
				if (p.X < minX)
				{
					minX = p.X;
				}
				if (p.X > maxX)
				{
					maxX = p.X;
				}
				if (p.Y < minY)
				{
					minY = p.Y;
				}
				if (p.Y > maxY)
				{
					maxY = p.Y;
				}
			}
			float width = Math.Max(maxX - minX, 1E-06f);
			float height = Math.Max(maxY - minY, 1E-06f);
			VertexPositionColorTexture[] texturedVertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[points.Count];
			for (int j = 0; j < points.Count; j++)
			{
				Vector3 point = points[j];
				float u = (point.X - minX) / width;
				float v = (point.Y - minY) / height;
				texturedVertices[j] = new VertexPositionColorTexture(point, color, new Vector2(u, v));
			}
			short[] indices = new short[(points.Count - 2) * 3];
			for (int k = 0; k < points.Count - 2; k++)
			{
				indices[k * 3] = 0;
				indices[k * 3 + 1] = (short)(k + 1);
				indices[k * 3 + 2] = (short)(k + 2);
			}
			return new PrimitiveMesh(texturedVertices, indices, (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[points.Count];
		for (int l = 0; l < points.Count; l++)
		{
			Vector3 point2 = points[l];
			colorVertices[l] = new VertexPositionColor(point2, color);
		}
		short[] colorIndices = new short[(points.Count - 2) * 3];
		for (int m = 0; m < points.Count - 2; m++)
		{
			colorIndices[m * 3] = 0;
			colorIndices[m * 3 + 1] = (short)(m + 1);
			colorIndices[m * 3 + 2] = (short)(m + 2);
		}
		return new PrimitiveMesh(colorVertices, colorIndices, (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildEllipse(Vector3 center, Vector2 radii, int segments, Color color, Vector3 normal, Vector3 upHint, bool textured = false)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (segments < 3)
		{
			throw new ArgumentOutOfRangeException("segments", "Ellipse requires at least three segments.");
		}
		BuildFrame(normal, upHint, out var right, out var up);
		if (textured)
		{
			VertexPositionColorTexture[] vertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[segments + 1];
			vertices[0] = new VertexPositionColorTexture(center, color, new Vector2(0.5f, 0.5f));
			float safeX = Math.Max(radii.X, 1E-06f);
			float safeY = Math.Max(radii.Y, 1E-06f);
			for (int i = 0; i < segments; i++)
			{
				Vector3 offset = EllipseDirection(i, segments, right, up, radii);
				Vector3 point = center + offset;
				float u = 0.5f + 0.5f * MathHelper.Clamp(Vector3.Dot(offset, right) / safeX, -1f, 1f);
				float v = 0.5f - 0.5f * MathHelper.Clamp(Vector3.Dot(offset, up) / safeY, -1f, 1f);
				vertices[i + 1] = new VertexPositionColorTexture(point, color, new Vector2(u, v));
			}
			short[] indices = new short[segments * 3];
			for (int j = 0; j < segments; j++)
			{
				int next = (j + 1) % segments;
				int baseIndex = j * 3;
				indices[baseIndex] = 0;
				indices[baseIndex + 1] = (short)(j + 1);
				indices[baseIndex + 2] = (short)(next + 1);
			}
			return new PrimitiveMesh(vertices, indices, (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[segments + 1];
		colorVertices[0] = new VertexPositionColor(center, color);
		for (int k = 0; k < segments; k++)
		{
			colorVertices[k + 1] = new VertexPositionColor(center + EllipseDirection(k, segments, right, up, radii), color);
		}
		short[] colorIndices = new short[segments * 3];
		for (int l = 0; l < segments; l++)
		{
			int next2 = (l + 1) % segments;
			int baseIndex2 = l * 3;
			colorIndices[baseIndex2] = 0;
			colorIndices[baseIndex2 + 1] = (short)(l + 1);
			colorIndices[baseIndex2 + 2] = (short)(next2 + 1);
		}
		return new PrimitiveMesh(colorVertices, colorIndices, (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildSphere(Vector3 center, float radius, int latitudeSegments, int longitudeSegments, Func<Vector3, Color>? colorFunc = null, bool textured = false)
	{
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (latitudeSegments < 2)
		{
			throw new ArgumentOutOfRangeException("latitudeSegments", "Sphere requires at least two latitude segments.");
		}
		if (longitudeSegments < 3)
		{
			throw new ArgumentOutOfRangeException("longitudeSegments", "Sphere requires at least three longitude segments.");
		}
		if (colorFunc == null)
		{
			colorFunc = delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				return Color.White;
			};
		}
		int num = latitudeSegments + 1;
		int vertexCols = longitudeSegments + 1;
		int vertexCount = num * vertexCols;
		if (vertexCount > 32767)
		{
			throw new InvalidOperationException("Sphere produced more vertices than supported by the index buffer.");
		}
		if (textured)
		{
			VertexPositionColorTexture[] vertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[vertexCount];
			int v = 0;
			for (int lat = 0; lat <= latitudeSegments; lat++)
			{
				float x = (float)Math.PI * (float)lat / (float)latitudeSegments;
				float sinPhi = MathF.Sin(x);
				float cosPhi = MathF.Cos(x);
				float vCoord = 1f - (float)lat / (float)latitudeSegments;
				for (int lon = 0; lon <= longitudeSegments; lon++)
				{
					float theta = (float)Math.PI * 2f * (float)lon / (float)longitudeSegments;
					Vector3 normal = SphericalDirection(sinPhi, cosPhi, theta);
					float uCoord = (float)lon / (float)longitudeSegments;
					vertices[v++] = new VertexPositionColorTexture(center + normal * radius, colorFunc(normal), new Vector2(uCoord, vCoord));
				}
			}
			List<short> indices = new List<short>(latitudeSegments * longitudeSegments * 6);
			for (int lat2 = 0; lat2 < latitudeSegments; lat2++)
			{
				for (int lon2 = 0; lon2 < longitudeSegments; lon2++)
				{
					int current = lat2 * vertexCols + lon2;
					int next = current + vertexCols;
					indices.Add((short)current);
					indices.Add((short)(current + 1));
					indices.Add((short)next);
					indices.Add((short)(current + 1));
					indices.Add((short)(next + 1));
					indices.Add((short)next);
				}
			}
			return new PrimitiveMesh(vertices, indices.ToArray(), (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[vertexCount];
		int vIndex = 0;
		for (int lat3 = 0; lat3 <= latitudeSegments; lat3++)
		{
			float x2 = (float)Math.PI * (float)lat3 / (float)latitudeSegments;
			float sinPhi2 = MathF.Sin(x2);
			float cosPhi2 = MathF.Cos(x2);
			for (int lon3 = 0; lon3 <= longitudeSegments; lon3++)
			{
				float theta2 = (float)Math.PI * 2f * (float)lon3 / (float)longitudeSegments;
				Vector3 normal2 = SphericalDirection(sinPhi2, cosPhi2, theta2);
				colorVertices[vIndex++] = new VertexPositionColor(center + normal2 * radius, colorFunc(normal2));
			}
		}
		List<short> colorIndices = new List<short>(latitudeSegments * longitudeSegments * 6);
		for (int lat4 = 0; lat4 < latitudeSegments; lat4++)
		{
			for (int lon4 = 0; lon4 < longitudeSegments; lon4++)
			{
				int current2 = lat4 * vertexCols + lon4;
				int next2 = current2 + vertexCols;
				colorIndices.Add((short)current2);
				colorIndices.Add((short)(current2 + 1));
				colorIndices.Add((short)next2);
				colorIndices.Add((short)(current2 + 1));
				colorIndices.Add((short)(next2 + 1));
				colorIndices.Add((short)next2);
			}
		}
		return new PrimitiveMesh(colorVertices, colorIndices.ToArray(), (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildTriangle(Vector3 a, Vector3 b, Vector3 c, Color color, bool textured = false)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (textured)
		{
			return new PrimitiveMesh((VertexPositionColorTexture[])(object)new VertexPositionColorTexture[3]
			{
				new VertexPositionColorTexture(a, color, new Vector2(0f, 1f)),
				new VertexPositionColorTexture(b, color, new Vector2(1f, 1f)),
				new VertexPositionColorTexture(c, color, new Vector2(0.5f, 0f))
			}, new short[3] { 0, 1, 2 }, (PrimitiveType)0);
		}
		return new PrimitiveMesh((VertexPositionColor[])(object)new VertexPositionColor[3]
		{
			new VertexPositionColor(a, color),
			new VertexPositionColor(b, color),
			new VertexPositionColor(c, color)
		}, new short[3] { 0, 1, 2 }, (PrimitiveType)0);
	}

	public static PrimitiveMesh BuildSemiCircle(Vector3 center, float radius, int segments, Color color, Vector3 normal, Vector3 forward, bool textured = false)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		if (segments < 2)
		{
			throw new ArgumentOutOfRangeException("segments", "Semi-circle requires at least two segments.");
		}
		Vector3 n = ((((Vector3)(ref normal)).LengthSquared() < 1E-06f) ? Vector3.Backward : Vector3.Normalize(normal));
		Vector3 f = ((((Vector3)(ref forward)).LengthSquared() < 1E-06f) ? Vector3.Forward : Vector3.Normalize(forward));
		if (MathF.Abs(Vector3.Dot(f, n)) > 0.999f)
		{
			f = Vector3.Normalize(Vector3.Cross(n, Vector3.Right));
		}
		Vector3 right = Vector3.Normalize(Vector3.Cross(n, f));
		Vector3 tangent = Vector3.Normalize(Vector3.Cross(right, n));
		if (textured)
		{
			VertexPositionColorTexture[] texturedVertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[segments + 2];
			texturedVertices[0] = new VertexPositionColorTexture(center, color, new Vector2(0.5f, 1f));
			Math.Max(radius, 1E-06f);
			float newStep = (float)Math.PI / (float)segments;
			for (int i = 0; i <= segments; i++)
			{
				float angle = newStep * (float)i;
				Vector3 offset = right * MathF.Cos(angle) + tangent * MathF.Sin(angle);
				Vector3 point = center + offset * radius;
				float x = MathHelper.Clamp(Vector3.Dot(offset, right), -1f, 1f);
				float y = MathHelper.Clamp(Vector3.Dot(offset, tangent), 0f, 1f);
				float u = 0.5f + 0.5f * x;
				float v = 1f - y;
				texturedVertices[i + 1] = new VertexPositionColorTexture(point, color, new Vector2(u, v));
			}
			short[] indices = new short[segments * 3];
			for (int j = 0; j < segments; j++)
			{
				int baseIndex = j * 3;
				indices[baseIndex] = 0;
				indices[baseIndex + 1] = (short)(j + 1);
				indices[baseIndex + 2] = (short)(j + 2);
			}
			return new PrimitiveMesh(texturedVertices, indices, (PrimitiveType)0);
		}
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)new VertexPositionColor[segments + 2];
		colorVertices[0] = new VertexPositionColor(center, color);
		float step = (float)Math.PI / (float)segments;
		for (int k = 0; k <= segments; k++)
		{
			float angle2 = step * (float)k;
			Vector3 offset2 = right * MathF.Cos(angle2) + tangent * MathF.Sin(angle2);
			colorVertices[k + 1] = new VertexPositionColor(center + offset2 * radius, color);
		}
		short[] colorIndices = new short[segments * 3];
		for (int l = 0; l < segments; l++)
		{
			int baseIndex2 = l * 3;
			colorIndices[baseIndex2] = 0;
			colorIndices[baseIndex2 + 1] = (short)(l + 1);
			colorIndices[baseIndex2 + 2] = (short)(l + 2);
		}
		return new PrimitiveMesh(colorVertices, colorIndices, (PrimitiveType)0);
	}

	private static void BuildFrame(Vector3 normal, Vector3 upHint, out Vector3 right, out Vector3 up)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Vector3 n = ((((Vector3)(ref normal)).LengthSquared() < 1E-06f) ? Vector3.Backward : Vector3.Normalize(normal));
		up = ((((Vector3)(ref upHint)).LengthSquared() < 1E-06f) ? Vector3.Up : Vector3.Normalize(upHint));
		if (MathF.Abs(Vector3.Dot(n, up)) > 0.999f)
		{
			up = Vector3.Normalize(Vector3.Cross(n, Vector3.Right));
		}
		right = Vector3.Cross(up, n);
		if (((Vector3)(ref right)).LengthSquared() < 1E-06f)
		{
			right = Vector3.Cross(Vector3.Forward, n);
		}
		((Vector3)(ref right)).Normalize();
		up = Vector3.Normalize(Vector3.Cross(n, right));
	}

	private static Vector3 PolarToCartesian(int index, int total, Vector3 right, Vector3 up)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		float angle = (float)Math.PI * 2f * (float)index / (float)total;
		return right * MathF.Cos(angle) + up * MathF.Sin(angle);
	}

	private static Vector3 EllipseDirection(int index, int total, Vector3 right, Vector3 up, Vector2 radii)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		float angle = (float)Math.PI * 2f * (float)index / (float)total;
		return right * (MathF.Cos(angle) * radii.X) + up * (MathF.Sin(angle) * radii.Y);
	}

	private static Vector3 SphericalDirection(float sinPhi, float cosPhi, float theta)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		float cosTheta = MathF.Cos(theta);
		float sinTheta = MathF.Sin(theta);
		return new Vector3(sinPhi * cosTheta, cosPhi, sinPhi * sinTheta);
	}
}
