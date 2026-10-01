using System;
using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

internal sealed class MultiVariantWallDrawingSystem : ModSystem
{
	public sealed override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		IL_WallDrawing.DrawWalls += new Manipulator(DrawingFrameOffsetForWallSupport);
	}

	private void DrawingFrameOffsetForWallSupport(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCall<Tile>(x, "wallFrameY")
		}))
		{
			LogILFailure("call::Tile.wallFrameY was not found!");
			return;
		}
		int tileLocalIdx = -1;
		int rectLocalIdx = -1;
		if (!cursor.TryGotoPrev(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdloca(x, ref tileLocalIdx)
		}))
		{
			LogILFailure("Ldloca::tile was not found!");
			return;
		}
		if (!cursor.TryGotoPrev(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdloca(x, ref rectLocalIdx)
		}))
		{
			LogILFailure("Ldloca::rect was not found!");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchStfld<Rectangle>(x, "Y")
		}))
		{
			LogILFailure("Stfld::Rectangle.Y was not found!");
			return;
		}
		cursor.EmitLdloc(tileLocalIdx);
		cursor.EmitLdloca(rectLocalIdx);
		cursor.EmitDelegate<_003C_003EA_007B00000008_007D<Tile, Rectangle>>((_003C_003EA_007B00000008_007D<Tile, Rectangle>)delegate(Tile tile, ref Rectangle drawRect)
		{
			if (ModContent.GetModWall(tile.WallType) is MultiVariantModWall multiVariantModWall)
			{
				tile.TilePos(out var x, out var y);
				int frameXOffset = 0;
				int frameYOffset = 0;
				multiVariantModWall.PopulateWallVariant(x, y, ref frameXOffset, ref frameYOffset);
				drawRect.X += frameXOffset;
				drawRect.Y += frameYOffset;
			}
		});
	}

	private static void LogILFailure(string reason)
	{
		CalamityMod.Log.ILFailure("Support for Wall FrameOffset", reason);
	}
}
