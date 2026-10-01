using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public readonly struct PooledPrimitiveMesh : IDisposable
{
	private readonly PrimitiveMeshCache? _cache;

	private readonly VertexPositionColorTexture[]? _textured;

	private readonly VertexPositionColor[]? _colored;

	private readonly short[] _indices;

	[CompilerGenerated]
	private readonly PrimitiveType _003CPrimitiveType_003Ek__BackingField;

	public bool UsesTexture { get; }

	public int VertexCount { get; }

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

	public VertexPositionColorTexture[] TexturedVertices => _textured ?? throw new InvalidOperationException("Mesh lease does not contain textured vertices.");

	public VertexPositionColor[] ColorVertices => _colored ?? throw new InvalidOperationException("Mesh lease does not contain color-only vertices.");

	public short[] Indices => _indices;

	public PrimitiveMeshView View
	{
		get
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			if (!UsesTexture)
			{
				return new PrimitiveMeshView(_colored, _indices, PrimitiveType, 0, VertexCount, 0, IndexCount);
			}
			return new PrimitiveMeshView(_textured, _indices, PrimitiveType, 0, VertexCount, 0, IndexCount);
		}
	}

	internal PooledPrimitiveMesh(PrimitiveMeshCache cache, VertexPositionColorTexture[]? textured, VertexPositionColor[]? colored, short[] indices, int vertexCount, int indexCount, PrimitiveType primitiveType, bool usesTexture)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		_cache = cache;
		_textured = textured;
		_colored = colored;
		_indices = indices ?? throw new ArgumentNullException("indices");
		VertexCount = vertexCount;
		IndexCount = indexCount;
		PrimitiveType = primitiveType;
		UsesTexture = usesTexture;
	}

	public void Dispose()
	{
		_cache?.Return(_textured, _colored, _indices, UsesTexture);
	}
}
