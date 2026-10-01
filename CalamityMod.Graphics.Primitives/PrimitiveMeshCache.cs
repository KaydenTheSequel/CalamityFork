using System;
using System.Buffers;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public sealed class PrimitiveMeshCache
{
	private readonly ArrayPool<VertexPositionColorTexture> _texturedPool;

	private readonly ArrayPool<VertexPositionColor> _colorPool;

	private readonly ArrayPool<short> _indexPool;

	private readonly bool _clearOnReturn;

	public static PrimitiveMeshCache Shared { get; } = new PrimitiveMeshCache();

	public PrimitiveMeshCache(ArrayPool<VertexPositionColorTexture>? texturedPool = null, ArrayPool<VertexPositionColor>? colorPool = null, ArrayPool<short>? indexPool = null, bool clearOnReturn = false)
	{
		_texturedPool = texturedPool ?? ArrayPool<VertexPositionColorTexture>.Shared;
		_colorPool = colorPool ?? ArrayPool<VertexPositionColor>.Shared;
		_indexPool = indexPool ?? ArrayPool<short>.Shared;
		_clearOnReturn = clearOnReturn;
	}

	public PooledPrimitiveMesh RentTextured(int vertexCount, int indexCount, PrimitiveType primitiveType)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (vertexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Cannot meaningfully rent zero vertices.");
		}
		if (indexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("indexCount", "Cannot meaningfully rent an index buffer of size zero.");
		}
		if (vertexCount > 32767)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Vertex count exceeds index buffer range.");
		}
		VertexPositionColorTexture[] vertices = _texturedPool.Rent(vertexCount);
		short[] indices = _indexPool.Rent(indexCount);
		return new PooledPrimitiveMesh(this, vertices, null, indices, vertexCount, indexCount, primitiveType, usesTexture: true);
	}

	public PooledPrimitiveMesh RentColored(int vertexCount, int indexCount, PrimitiveType primitiveType)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (vertexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Cannot meaningfully rent zero vertices.");
		}
		if (indexCount <= 0)
		{
			throw new ArgumentOutOfRangeException("indexCount", "Cannot meaningfully rent an index buffer of size zero.");
		}
		if (vertexCount > 32767)
		{
			throw new ArgumentOutOfRangeException("vertexCount", "Vertex count exceeds index buffer range.");
		}
		VertexPositionColor[] vertices = _colorPool.Rent(vertexCount);
		short[] indices = _indexPool.Rent(indexCount);
		return new PooledPrimitiveMesh(this, null, vertices, indices, vertexCount, indexCount, primitiveType, usesTexture: false);
	}

	public PooledPrimitiveMesh RentFromMesh(in PrimitiveMesh mesh)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (!mesh.IsValid)
		{
			throw new ArgumentException("Mesh is invalid.", "mesh");
		}
		if (mesh.UsesTexture)
		{
			PooledPrimitiveMesh lease = RentTextured(mesh.VertexCount, mesh.Indices.Length, mesh.PrimitiveType);
			Array.Copy(mesh.TexturedVertices, lease.TexturedVertices, mesh.VertexCount);
			Array.Copy(mesh.Indices, lease.Indices, mesh.Indices.Length);
			return lease;
		}
		PooledPrimitiveMesh lease2 = RentColored(mesh.VertexCount, mesh.Indices.Length, mesh.PrimitiveType);
		Array.Copy(mesh.ColorVertices, lease2.ColorVertices, mesh.VertexCount);
		Array.Copy(mesh.Indices, lease2.Indices, mesh.Indices.Length);
		return lease2;
	}

	internal void Return(VertexPositionColorTexture[]? textured, VertexPositionColor[]? colored, short[] indices, bool usesTexture)
	{
		if (usesTexture)
		{
			if (textured != null)
			{
				_texturedPool.Return(textured, _clearOnReturn);
			}
		}
		else if (colored != null)
		{
			_colorPool.Return(colored, _clearOnReturn);
		}
		_indexPool.Return(indices, _clearOnReturn);
	}
}
