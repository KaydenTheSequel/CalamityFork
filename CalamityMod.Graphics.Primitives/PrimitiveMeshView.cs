using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public readonly struct PrimitiveMeshView
{
	private readonly VertexPositionColorTexture[]? _texturedVertices;

	private readonly VertexPositionColor[]? _colorVertices;

	[CompilerGenerated]
	private readonly PrimitiveType _003CPrimitiveType_003Ek__BackingField;

	public bool UsesTexture { get; }

	public int VertexOffset { get; }

	public int VertexCount { get; }

	public int IndexOffset { get; }

	public int IndexCount { get; }

	public PrimitiveType PrimitiveType
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CPrimitiveType_003Ek__BackingField;
		}
	}

	public bool IsValid
	{
		get
		{
			if (VertexCount > 0)
			{
				return IndexCount > 0;
			}
			return false;
		}
	}

	public VertexPositionColorTexture[] TexturedVertices => _texturedVertices ?? throw new InvalidOperationException("Mesh view does not contain textured vertices.");

	public VertexPositionColor[] ColorVertices => _colorVertices ?? throw new InvalidOperationException("Mesh view does not contain color-only vertices.");

	public short[] Indices { get; }

	public PrimitiveMeshView(VertexPositionColorTexture[] vertices, short[] indices, PrimitiveType primitiveType, int vertexOffset, int vertexCount, int indexOffset, int indexCount)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_texturedVertices = vertices ?? throw new ArgumentNullException("vertices");
		_colorVertices = null;
		Indices = indices ?? throw new ArgumentNullException("indices");
		PrimitiveType = primitiveType;
		ValidateRanges(vertices.Length, indices.Length, vertexOffset, vertexCount, indexOffset, indexCount);
		VertexOffset = vertexOffset;
		VertexCount = vertexCount;
		IndexOffset = indexOffset;
		IndexCount = indexCount;
		UsesTexture = true;
	}

	public PrimitiveMeshView(VertexPositionColor[] vertices, short[] indices, PrimitiveType primitiveType, int vertexOffset, int vertexCount, int indexOffset, int indexCount)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_texturedVertices = null;
		_colorVertices = vertices ?? throw new ArgumentNullException("vertices");
		Indices = indices ?? throw new ArgumentNullException("indices");
		PrimitiveType = primitiveType;
		ValidateRanges(vertices.Length, indices.Length, vertexOffset, vertexCount, indexOffset, indexCount);
		VertexOffset = vertexOffset;
		VertexCount = vertexCount;
		IndexOffset = indexOffset;
		IndexCount = indexCount;
		UsesTexture = false;
	}

	private static void ValidateRanges(int vertexArrayLength, int indexArrayLength, int vertexOffset, int vertexCount, int indexOffset, int indexCount)
	{
		if (vertexOffset < 0)
		{
			throw new ArgumentOutOfRangeException("vertexOffset");
		}
		if (vertexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("vertexCount");
		}
		if (indexOffset < 0)
		{
			throw new ArgumentOutOfRangeException("indexOffset");
		}
		if (indexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("indexCount");
		}
		if (vertexOffset + vertexCount > vertexArrayLength)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Vertex range exceeds the vertex buffer length.");
		}
		if (indexOffset + indexCount > indexArrayLength)
		{
			throw new ArgumentOutOfRangeException("indexCount", "Index range exceeds the index buffer length.");
		}
		if (vertexCount > 32767)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Vertex count exceeds index buffer range.");
		}
	}
}
