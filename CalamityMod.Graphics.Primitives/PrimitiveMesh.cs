using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public readonly struct PrimitiveMesh
{
	private readonly VertexPositionColorTexture[]? _texturedVertices;

	private readonly VertexPositionColor[]? _colorVertices;

	[CompilerGenerated]
	private readonly PrimitiveType _003CPrimitiveType_003Ek__BackingField;

	public bool UsesTexture { get; }

	public int VertexCount
	{
		get
		{
			if (!UsesTexture)
			{
				return _colorVertices.Length;
			}
			return _texturedVertices.Length;
		}
	}

	public VertexPositionColorTexture[] TexturedVertices => _texturedVertices ?? throw new InvalidOperationException("Mesh does not contain textured vertices.");

	public VertexPositionColor[] ColorVertices => _colorVertices ?? throw new InvalidOperationException("Mesh does not contain color-only vertices.");

	public short[] Indices { get; }

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
				return Indices.Length != 0;
			}
			return false;
		}
	}

	public PrimitiveMesh(VertexPositionColorTexture[] vertices, short[] indices, PrimitiveType primitiveType)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_texturedVertices = vertices ?? throw new ArgumentNullException("vertices");
		_colorVertices = null;
		Indices = indices ?? throw new ArgumentNullException("indices");
		PrimitiveType = primitiveType;
		if (_texturedVertices.Length == 0 || Indices.Length == 0)
		{
			throw new ArgumentException("Mesh must contain vertices and indices.");
		}
		if (_texturedVertices.Length > 32767)
		{
			throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
		}
		UsesTexture = true;
	}

	public PrimitiveMesh(VertexPositionColor[] vertices, short[] indices, PrimitiveType primitiveType)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_texturedVertices = null;
		_colorVertices = vertices ?? throw new ArgumentNullException("vertices");
		Indices = indices ?? throw new ArgumentNullException("indices");
		PrimitiveType = primitiveType;
		if (_colorVertices.Length == 0 || Indices.Length == 0)
		{
			throw new ArgumentException("Mesh must contain vertices and indices.");
		}
		if (_colorVertices.Length > 32767)
		{
			throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
		}
		UsesTexture = false;
	}

	public PrimitiveMeshView AsView()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (!UsesTexture)
		{
			return new PrimitiveMeshView(_colorVertices, Indices, PrimitiveType, 0, _colorVertices.Length, 0, Indices.Length);
		}
		return new PrimitiveMeshView(_texturedVertices, Indices, PrimitiveType, 0, _texturedVertices.Length, 0, Indices.Length);
	}

	public static PrimitiveMesh FromSequential(VertexPositionColorTexture[] vertices, PrimitiveType primitiveType)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length == 0)
		{
			throw new ArgumentException("Vertices collection is empty.", "vertices");
		}
		if (vertices.Length > 32767)
		{
			throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
		}
		short[] indices = new short[vertices.Length];
		PrimitiveSimd.FillSequentialIndices(indices.AsSpan());
		return new PrimitiveMesh(vertices, indices, primitiveType);
	}

	public static PrimitiveMesh FromSequential(VertexPositionColor[] vertices, PrimitiveType primitiveType)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length == 0)
		{
			throw new ArgumentException("Vertices collection is empty.", "vertices");
		}
		if (vertices.Length > 32767)
		{
			throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
		}
		short[] indices = new short[vertices.Length];
		PrimitiveSimd.FillSequentialIndices(indices.AsSpan());
		return new PrimitiveMesh(vertices, indices, primitiveType);
	}
}
