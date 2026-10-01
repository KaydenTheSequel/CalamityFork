using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public readonly struct VertexPosition2DColorTexture : IVertexType
{
	public readonly Vector2 Position;

	public readonly Color Color;

	public readonly Vector3 TextureCoordinates;

	public static readonly VertexDeclaration VertexDeclaration2D;

	public VertexDeclaration VertexDeclaration => VertexDeclaration2D;

	public VertexPosition2DColorTexture(Vector2 position, Color color, Vector2 textureCoordinates, float widthCorrectionFactor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Position = position;
		Color = color;
		TextureCoordinates = new Vector3(textureCoordinates, widthCorrectionFactor);
	}

	static VertexPosition2DColorTexture()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		VertexDeclaration2D = new VertexDeclaration((VertexElement[])(object)new VertexElement[3]
		{
			new VertexElement(0, (VertexElementFormat)1, (VertexElementUsage)0, 0),
			new VertexElement(8, (VertexElementFormat)4, (VertexElementUsage)1, 0),
			new VertexElement(12, (VertexElementFormat)2, (VertexElementUsage)2, 0)
		});
	}
}
