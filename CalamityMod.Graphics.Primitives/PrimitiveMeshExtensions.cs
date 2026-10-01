using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public static class PrimitiveMeshExtensions
{
	private readonly struct EdgeKey(short min, short max) : IEquatable<EdgeKey>
	{
		public readonly short Min = min;

		public readonly short Max = max;

		public bool Equals(EdgeKey other)
		{
			if (Min == other.Min)
			{
				return Max == other.Max;
			}
			return false;
		}

		public override bool Equals(object? obj)
		{
			if (obj is EdgeKey other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (Min * 397) ^ Max;
		}
	}

	private struct EdgeAccumulator
	{
		public int Count;

		public OrientedEdge Orientation;
	}

	private struct OrientedEdge(short start, short end, float length)
	{
		public short Start = start;

		public short End = end;

		public float Length = length;

		public OrientedEdge Reversed()
		{
			return new OrientedEdge(End, Start, Length);
		}
	}

	public static PrimitiveMesh Transform(this in PrimitiveMesh mesh, in Matrix transform)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (!mesh.IsValid)
		{
			return mesh;
		}
		short[] indices = (short[])mesh.Indices.Clone();
		if (mesh.UsesTexture)
		{
			VertexPositionColorTexture[] source = mesh.TexturedVertices;
			VertexPositionColorTexture[] transformed = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[source.Length];
			for (int i = 0; i < source.Length; i++)
			{
				VertexPositionColorTexture v = source[i];
				transformed[i] = new VertexPositionColorTexture(Vector3.Transform(v.Position, transform), v.Color, v.TextureCoordinate);
			}
			return new PrimitiveMesh(transformed, indices, mesh.PrimitiveType);
		}
		VertexPositionColor[] source2 = mesh.ColorVertices;
		VertexPositionColor[] transformed2 = (VertexPositionColor[])(object)new VertexPositionColor[source2.Length];
		for (int j = 0; j < source2.Length; j++)
		{
			VertexPositionColor v2 = source2[j];
			transformed2[j] = new VertexPositionColor(Vector3.Transform(v2.Position, transform), v2.Color);
		}
		return new PrimitiveMesh(transformed2, indices, mesh.PrimitiveType);
	}

	public static PrimitiveMesh Translate(this in PrimitiveMesh mesh, in Vector3 offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return mesh.Transform(Matrix.CreateTranslation(offset));
	}

	public static PrimitiveMesh Scale(this in PrimitiveMesh mesh, in Vector3 scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return mesh.Transform(Matrix.CreateScale(scale));
	}

	public static PrimitiveMesh Rotate(this in PrimitiveMesh mesh, in Quaternion rotation)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return mesh.Transform(Matrix.CreateFromQuaternion(rotation));
	}

	public static PrimitiveMesh Extrude(this in PrimitiveMesh mesh, in Vector3 direction, bool closeCaps = true)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (!mesh.IsValid)
		{
			throw new InvalidOperationException("Cannot extrude an invalid mesh.");
		}
		if ((int)mesh.PrimitiveType != 0)
		{
			throw new NotSupportedException("Extrusion currently supports triangle list meshes only.");
		}
		Vector3 val = direction;
		if (((Vector3)(ref val)).LengthSquared() <= 1E-06f)
		{
			return mesh;
		}
		if (!mesh.UsesTexture)
		{
			return ExtrudeColored(in mesh, direction, closeCaps);
		}
		return ExtrudeTextured(in mesh, direction, closeCaps);
	}

	private static PrimitiveMesh ExtrudeTextured(in PrimitiveMesh mesh, Vector3 direction, bool closeCaps)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		VertexPositionColorTexture[] top = mesh.TexturedVertices;
		short[] baseIndices = mesh.Indices;
		int originalVertexCount = top.Length;
		List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>(originalVertexCount * 2);
		vertices.AddRange(top);
		for (int i = 0; i < originalVertexCount; i++)
		{
			VertexPositionColorTexture v = top[i];
			vertices.Add(new VertexPositionColorTexture(v.Position + direction, v.Color, v.TextureCoordinate));
		}
		List<short> indices = new List<short>(closeCaps ? (baseIndices.Length * 2) : (baseIndices.Length + originalVertexCount * 6));
		indices.AddRange(baseIndices);
		if (closeCaps)
		{
			for (int j = 0; j < baseIndices.Length; j += 3)
			{
				short a = (short)(baseIndices[j] + originalVertexCount);
				short b = (short)(baseIndices[j + 1] + originalVertexCount);
				short c = (short)(baseIndices[j + 2] + originalVertexCount);
				indices.Add(c);
				indices.Add(b);
				indices.Add(a);
			}
		}
		Vector3[] positions = (Vector3[])(object)new Vector3[originalVertexCount];
		for (int k = 0; k < originalVertexCount; k++)
		{
			positions[k] = top[k].Position;
		}
		foreach (List<OrientedEdge> loop in BuildBoundaryLoops(positions, baseIndices))
		{
			float perimeter = 0f;
			for (int l = 0; l < loop.Count; l++)
			{
				perimeter += loop[l].Length;
			}
			if (perimeter <= 1E-06f)
			{
				continue;
			}
			float accumulated = 0f;
			foreach (OrientedEdge edge in loop)
			{
				float u0 = accumulated / perimeter;
				accumulated += edge.Length;
				float u1 = accumulated / perimeter;
				VertexPositionColorTexture startTop = top[edge.Start];
				VertexPositionColorTexture endTop = top[edge.End];
				Vector3 startBottomPos = startTop.Position + direction;
				Vector3 endBottomPos = endTop.Position + direction;
				short topStartIndex = AddVertex(vertices, new VertexPositionColorTexture(startTop.Position, startTop.Color, new Vector2(u0, 0f)));
				short bottomStartIndex = AddVertex(vertices, new VertexPositionColorTexture(startBottomPos, startTop.Color, new Vector2(u0, 1f)));
				short topEndIndex = AddVertex(vertices, new VertexPositionColorTexture(endTop.Position, endTop.Color, new Vector2(u1, 0f)));
				short bottomEndIndex = AddVertex(vertices, new VertexPositionColorTexture(endBottomPos, endTop.Color, new Vector2(u1, 1f)));
				indices.Add(topStartIndex);
				indices.Add(topEndIndex);
				indices.Add(bottomEndIndex);
				indices.Add(topStartIndex);
				indices.Add(bottomEndIndex);
				indices.Add(bottomStartIndex);
			}
		}
		return new PrimitiveMesh(vertices.ToArray(), indices.ToArray(), (PrimitiveType)0);
	}

	private static PrimitiveMesh ExtrudeColored(in PrimitiveMesh mesh, Vector3 direction, bool closeCaps)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		VertexPositionColor[] top = mesh.ColorVertices;
		short[] baseIndices = mesh.Indices;
		int originalVertexCount = top.Length;
		List<VertexPositionColor> vertices = new List<VertexPositionColor>(originalVertexCount * 2);
		vertices.AddRange(top);
		for (int i = 0; i < originalVertexCount; i++)
		{
			VertexPositionColor v = top[i];
			vertices.Add(new VertexPositionColor(v.Position + direction, v.Color));
		}
		List<short> indices = new List<short>(closeCaps ? (baseIndices.Length * 2) : (baseIndices.Length + originalVertexCount * 6));
		indices.AddRange(baseIndices);
		if (closeCaps)
		{
			for (int j = 0; j < baseIndices.Length; j += 3)
			{
				short a = (short)(baseIndices[j] + originalVertexCount);
				short b = (short)(baseIndices[j + 1] + originalVertexCount);
				short c = (short)(baseIndices[j + 2] + originalVertexCount);
				indices.Add(c);
				indices.Add(b);
				indices.Add(a);
			}
		}
		Vector3[] positions = (Vector3[])(object)new Vector3[originalVertexCount];
		for (int k = 0; k < originalVertexCount; k++)
		{
			positions[k] = top[k].Position;
		}
		foreach (List<OrientedEdge> loop in BuildBoundaryLoops(positions, baseIndices))
		{
			float perimeter = 0f;
			for (int l = 0; l < loop.Count; l++)
			{
				perimeter += loop[l].Length;
			}
			if (perimeter <= 1E-06f)
			{
				continue;
			}
			float accumulated = 0f;
			foreach (OrientedEdge edge in loop)
			{
				_ = accumulated / perimeter;
				accumulated += edge.Length;
				_ = accumulated / perimeter;
				VertexPositionColor startTop = top[edge.Start];
				VertexPositionColor endTop = top[edge.End];
				Vector3 startBottomPos = startTop.Position + direction;
				Vector3 endBottomPos = endTop.Position + direction;
				short topStartIndex = AddVertex(vertices, new VertexPositionColor(startTop.Position, startTop.Color));
				short bottomStartIndex = AddVertex(vertices, new VertexPositionColor(startBottomPos, startTop.Color));
				short topEndIndex = AddVertex(vertices, new VertexPositionColor(endTop.Position, endTop.Color));
				short bottomEndIndex = AddVertex(vertices, new VertexPositionColor(endBottomPos, endTop.Color));
				indices.Add(topStartIndex);
				indices.Add(topEndIndex);
				indices.Add(bottomEndIndex);
				indices.Add(topStartIndex);
				indices.Add(bottomEndIndex);
				indices.Add(bottomStartIndex);
			}
		}
		return new PrimitiveMesh(vertices.ToArray(), indices.ToArray(), (PrimitiveType)0);
	}

	public static PrimitiveMesh CurveEdges(this in PrimitiveMesh mesh, in Vector3 axis, float magnitude, float exponent = 2f)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		if (!mesh.IsValid)
		{
			return mesh;
		}
		Vector3 val = axis;
		if (((Vector3)(ref val)).LengthSquared() <= 1E-06f)
		{
			throw new ArgumentException("Axis must be non-zero.", "axis");
		}
		if ((int)mesh.PrimitiveType != 0 && (int)mesh.PrimitiveType != 1)
		{
			throw new NotSupportedException("CurveEdges supports triangle-based meshes.");
		}
		float clampedExponent = Math.Max(exponent, 0.001f);
		Vector3 axisDir = Vector3.Normalize(axis);
		short[] indices = (short[])mesh.Indices.Clone();
		if (mesh.UsesTexture)
		{
			VertexPositionColorTexture[] source = mesh.TexturedVertices;
			VertexPositionColorTexture[] vertices = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[source.Length];
			ComputeCentroidAndRadius(source, out var centroid, out var radius);
			for (int i = 0; i < source.Length; i++)
			{
				VertexPositionColorTexture v = source[i];
				Vector3 diff = v.Position - centroid;
				float factor = MathF.Pow(MathHelper.Clamp((radius <= 1E-06f) ? 0f : (((Vector3)(ref diff)).Length() / radius), 0f, 1f), clampedExponent);
				Vector3 offset = axisDir * magnitude * factor;
				vertices[i] = new VertexPositionColorTexture(v.Position + offset, v.Color, v.TextureCoordinate);
			}
			return new PrimitiveMesh(vertices, indices, mesh.PrimitiveType);
		}
		VertexPositionColor[] source2 = mesh.ColorVertices;
		VertexPositionColor[] vertices2 = (VertexPositionColor[])(object)new VertexPositionColor[source2.Length];
		ComputeCentroidAndRadius(source2, out var centroid2, out var radius2);
		for (int j = 0; j < source2.Length; j++)
		{
			VertexPositionColor v2 = source2[j];
			Vector3 diff2 = v2.Position - centroid2;
			float factor2 = MathF.Pow(MathHelper.Clamp((radius2 <= 1E-06f) ? 0f : (((Vector3)(ref diff2)).Length() / radius2), 0f, 1f), clampedExponent);
			Vector3 offset2 = axisDir * magnitude * factor2;
			vertices2[j] = new VertexPositionColor(v2.Position + offset2, v2.Color);
		}
		return new PrimitiveMesh(vertices2, indices, mesh.PrimitiveType);
	}

	private static void ComputeCentroidAndRadius(VertexPositionColorTexture[] vertices, out Vector3 centroid, out float radius)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		centroid = Vector3.Zero;
		for (int i = 0; i < vertices.Length; i++)
		{
			centroid += vertices[i].Position;
		}
		centroid /= (float)vertices.Length;
		radius = 1E-06f;
		for (int j = 0; j < vertices.Length; j++)
		{
			float length = Vector3.Distance(centroid, vertices[j].Position);
			if (length > radius)
			{
				radius = length;
			}
		}
	}

	private static void ComputeCentroidAndRadius(VertexPositionColor[] vertices, out Vector3 centroid, out float radius)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		centroid = Vector3.Zero;
		for (int i = 0; i < vertices.Length; i++)
		{
			centroid += vertices[i].Position;
		}
		centroid /= (float)vertices.Length;
		radius = 1E-06f;
		for (int j = 0; j < vertices.Length; j++)
		{
			float length = Vector3.Distance(centroid, vertices[j].Position);
			if (length > radius)
			{
				radius = length;
			}
		}
	}

	private static List<List<OrientedEdge>> BuildBoundaryLoops(IReadOnlyList<Vector3> positions, short[] indices)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<EdgeKey, EdgeAccumulator> edgeMap = new Dictionary<EdgeKey, EdgeAccumulator>();
		for (int i = 0; i < indices.Length; i += 3)
		{
			RegisterEdge(edgeMap, indices[i], indices[i + 1]);
			RegisterEdge(edgeMap, indices[i + 1], indices[i + 2]);
			RegisterEdge(edgeMap, indices[i + 2], indices[i]);
		}
		List<OrientedEdge> edges = new List<OrientedEdge>();
		foreach (KeyValuePair<EdgeKey, EdgeAccumulator> pair in edgeMap)
		{
			if (pair.Value.Count == 1)
			{
				OrientedEdge oriented = pair.Value.Orientation;
				float length = Vector3.Distance(positions[oriented.Start], positions[oriented.End]);
				if (length > 1E-06f)
				{
					edges.Add(new OrientedEdge(oriented.Start, oriented.End, length));
				}
			}
		}
		List<List<OrientedEdge>> loops = new List<List<OrientedEdge>>();
		bool[] used = new bool[edges.Count];
		for (int j = 0; j < edges.Count; j++)
		{
			if (used[j])
			{
				continue;
			}
			List<OrientedEdge> loop = new List<OrientedEdge>();
			used[j] = true;
			loop.Add(edges[j]);
			short head = edges[j].End;
			bool closed = head == loop[0].Start;
			while (!closed)
			{
				bool found = false;
				for (int k = 0; k < edges.Count; k++)
				{
					if (!used[k])
					{
						OrientedEdge candidate = edges[k];
						if (candidate.Start == head)
						{
							used[k] = true;
							loop.Add(candidate);
							head = candidate.End;
							found = true;
						}
						else if (candidate.End == head)
						{
							candidate = (edges[k] = candidate.Reversed());
							used[k] = true;
							loop.Add(candidate);
							head = candidate.End;
							found = true;
						}
						if (found)
						{
							closed = head == loop[0].Start;
							break;
						}
					}
				}
				if (!found)
				{
					break;
				}
			}
			if ((loop.Count > 1) & closed)
			{
				loops.Add(loop);
			}
		}
		return loops;
	}

	private static void RegisterEdge(Dictionary<EdgeKey, EdgeAccumulator> edgeMap, short start, short end)
	{
		EdgeKey key = new EdgeKey(Math.Min(start, end), Math.Max(start, end));
		if (edgeMap.TryGetValue(key, out var accumulator))
		{
			accumulator.Count++;
			edgeMap[key] = accumulator;
		}
		else
		{
			edgeMap[key] = new EdgeAccumulator
			{
				Count = 1,
				Orientation = new OrientedEdge(start, end, 0f)
			};
		}
	}

	private static short AddVertex(List<VertexPositionColorTexture> vertices, VertexPositionColorTexture vertex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (vertices.Count >= 32767)
		{
			throw new InvalidOperationException("Primitive mesh exceeded 16-bit vertex capacity.");
		}
		vertices.Add(vertex);
		return (short)(vertices.Count - 1);
	}

	private static short AddVertex(List<VertexPositionColor> vertices, VertexPositionColor vertex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (vertices.Count >= 32767)
		{
			throw new InvalidOperationException("Primitive mesh exceeded 16-bit vertex capacity.");
		}
		vertices.Add(vertex);
		return (short)(vertices.Count - 1);
	}
}
