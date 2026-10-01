using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.FluidSimulation;

public struct FieldVertex2D : IVertexType
{
	public Vector2 Position;

	public Vector4 Color;

	public Vector2 TextureCoordinates;

	private static readonly VertexDeclaration _vertexDeclaration;

	public VertexDeclaration VertexDeclaration => _vertexDeclaration;

	public FieldVertex2D(Vector2 position, Vector4 color, Vector2 textureCoordinates)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		Position = position;
		Color = color;
		TextureCoordinates = textureCoordinates;
	}

	static FieldVertex2D()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		_vertexDeclaration = new VertexDeclaration((VertexElement[])(object)new VertexElement[3]
		{
			new VertexElement(0, (VertexElementFormat)1, (VertexElementUsage)0, 0),
			new VertexElement(8, (VertexElementFormat)3, (VertexElementUsage)1, 0),
			new VertexElement(24, (VertexElementFormat)1, (VertexElementUsage)2, 0)
		});
	}
}
