using System;
using CalamityMod.Systems.Graphic.LiquidSystem;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

[Obsolete("Use IWaterStyleModifyColor and IWaterStyleModifyLight Instead")]
public abstract class CalamityModWaterStyle : ModWaterStyle, IWaterStyleModifyColor, IWaterStyleModifyLight
{
	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		DrawColor(x, y, ref liquidColor, isSlope);
	}

	public virtual void DrawColor(int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
	}

	public void ModifyLight(in Tile tile, int x, int y, ref float r, ref float g, ref float b)
	{
		ModifyLight(x, y, ref r, ref g, ref b);
	}

	public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
	}

	void IWaterStyleModifyColor.ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		ModifyColor(in tile, x, y, ref liquidColor, isSlope);
	}

	void IWaterStyleModifyLight.ModifyLight(in Tile tile, int x, int y, ref float r, ref float g, ref float b)
	{
		ModifyLight(in tile, x, y, ref r, ref g, ref b);
	}
}
