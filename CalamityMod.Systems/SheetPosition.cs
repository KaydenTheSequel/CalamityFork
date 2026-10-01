using Microsoft.Xna.Framework;

namespace CalamityMod.Systems;

public readonly struct SheetPosition
{
	public readonly byte X;

	public readonly byte Y;

	public readonly sbyte BakedSheetIndex;

	public bool IsUsingBaseTexture => BakedSheetIndex < 0;

	public SheetPosition(int x, int y, sbyte bakedSheetIndex = -1)
	{
		X = 0;
		Y = 0;
		BakedSheetIndex = bakedSheetIndex;
		if (IsUsingBaseTexture)
		{
			X = (byte)(x / 18);
			Y = (byte)(y / 18);
		}
		else
		{
			X = (byte)(x / 16);
			Y = (byte)(y / 16);
		}
	}

	public Vector2 GetDrawPosition()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (IsUsingBaseTexture)
		{
			return new Vector2((float)(int)X * 18f, (float)(int)Y * 18f);
		}
		return new Vector2((float)(X * 16), (float)(Y * 16));
	}

	public Rectangle GetDrawRect()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (IsUsingBaseTexture)
		{
			return new Rectangle(X * 18, Y * 18, 16, 16);
		}
		return new Rectangle(X * 16, Y * 16, 16, 16);
	}
}
