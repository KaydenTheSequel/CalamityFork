using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

[Autoload(true, Side = ModSide.Client)]
public sealed class TileBlendMergeSystem : ModSystem
{
	[Autoload(true, Side = ModSide.Client)]
	private class FancyTileMergeGlobalTile : GlobalTile
	{
		public override void PostSetupTileMerge()
		{
			SetupMergeData();
		}

		public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			if (CalamityClientConfig.Instance.TileTextureBlendingQuality != TileBlendingQuality.Disable && !CalamityTileSets.DrawBlendMergeAfterSolidTile[type] && WorldGen.InWorld(i, j))
			{
				Tile tile = Main.tile[i, j];
				if (tile.Get<TileSpecialDrawData>().HasBlendMergeData && TryGetBlendingRefData(i, j, out var blendRefs))
				{
					DrawOnTile(tile, i, j, in blendRefs);
				}
			}
		}

		public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
		{
			if (!WorldGen.generatingWorld)
			{
				TileBlendMergeSystem.TileFrame(i, j, type);
			}
			return base.TileFrame(i, j, type, ref resetFrame, ref noBreak);
		}
	}

	private static bool[,] _TileBlendable;

	private static bool[,] _TileBlendLooselyFillDiagonal;

	private static byte[] _TileTypeToBlendTextureSlot;

	private static readonly Rectangle[] Rects9Slice;

	private static readonly Rectangle[] Rects4Slice;

	private static readonly ThreadLocal<Color[]> ColorSliceBuffer;

	private static readonly ConcurrentDictionary<int, TileBlendingRef[]> _TileBlendingRefs;

	[ThreadStatic]
	private static Dictionary<int, BlendSideFlags> TempBlendSidesReg;

	public override void OnModLoad()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		On_Main.DrawTiles += new hook_DrawTiles(OnDrawTiles);
		Main.OnPreDraw += UpdateBaking;
	}

	public override void Unload()
	{
		Main.OnPreDraw -= UpdateBaking;
		_TileBlendable = null;
		_TileBlendLooselyFillDiagonal = null;
		_TileTypeToBlendTextureSlot = null;
	}

	public override void ResizeArrays()
	{
		int tileCount = TileLoader.TileCount;
		int blendTextureCount = TileBlendTextureLoader.Count;
		ResizeArray2D(ref _TileBlendable, tileCount, blendTextureCount + 1);
		ResizeArray2D(ref _TileBlendLooselyFillDiagonal, tileCount, blendTextureCount + 1);
		Array.Resize(ref _TileTypeToBlendTextureSlot, tileCount);
		foreach (TileBlendTexture blendTexture in TileBlendTextureLoader.AllTextures)
		{
			_TileTypeToBlendTextureSlot[blendTexture.TileType] = (byte)blendTexture.Slot;
		}
	}

	private static void ResizeArray2D<T>(ref T[,] array, int newColNum, int newRowNum)
	{
		T[,] newArray = new T[newColNum, newRowNum];
		int colCount = array.GetLength(1);
		int cols = array.GetUpperBound(0);
		for (int co = 0; co <= cols; co++)
		{
			Array.Copy(array, co * colCount, newArray, co * newRowNum, colCount);
		}
		array = newArray;
	}

	private static void SetupMergeData()
	{
		foreach (TileBlendTexture allTexture in TileBlendTextureLoader.AllTextures)
		{
			allTexture.ClearBakeCache();
		}
	}

	private static void UpdateBaking(GameTime obj)
	{
		TileBlendTexture.BakedCountInFrame = 0;
		foreach (TileBlendTexture allTexture in TileBlendTextureLoader.AllTextures)
		{
			allTexture.BakeRequestedBlendTextureCache();
		}
	}

	public static void RegisterMerge(int myType, int blendTileType, bool looselyFillDiagonal = false)
	{
		if (!Main.dedServ && myType != blendTileType && _TileTypeToBlendTextureSlot.IndexInRange(myType) && _TileTypeToBlendTextureSlot.IndexInRange(blendTileType))
		{
			byte blendTextureSlot = _TileTypeToBlendTextureSlot[blendTileType];
			if (blendTextureSlot == 0)
			{
				string tileName = TileLoader.GetTile(blendTileType)?.FullName ?? "Vanilla Tile";
				CalamityMod.Log.Error((object)$"[BlendMergeSystem] BlendTileType: {blendTileType} ({tileName}) does not have TileBlendTexture! StackTrace:\n{Environment.StackTrace}");
			}
			else
			{
				_TileBlendable[myType, blendTextureSlot] = true;
				_TileBlendLooselyFillDiagonal[myType, blendTextureSlot] = looselyFillDiagonal;
				CalamityUtils.SetMerge(myType, blendTileType);
			}
		}
	}

	public static void RegisterMerge(int myType, TileBlendTexture blendTexture, bool looselyFillDiagonal = false)
	{
		if (!Main.dedServ && blendTexture != null && blendTexture.Slot >= 0 && _TileTypeToBlendTextureSlot.IndexInRange(myType))
		{
			int blendTileType = blendTexture.TileType;
			if (myType != blendTileType)
			{
				_TileBlendable[myType, blendTexture.Slot] = true;
				_TileBlendLooselyFillDiagonal[myType, blendTexture.Slot] = looselyFillDiagonal;
				CalamityUtils.SetMerge(myType, blendTileType);
			}
		}
	}

	public static void RegisterMerge<T>(int myType, bool looselyFillDiagonal = false) where T : TileBlendTexture
	{
		if (!Main.dedServ)
		{
			T blendTexture = ModContent.GetInstance<T>();
			RegisterMerge(myType, blendTexture, looselyFillDiagonal);
		}
	}

	private void OnDrawTiles(orig_DrawTiles orig, Main self, bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self, solidLayer, forRenderTargets, intoRenderTargets, waterStyleOverride);
		if (!solidLayer || CalamityClientConfig.Instance.TileTextureBlendingQuality == TileBlendingQuality.Disable)
		{
			return;
		}
		Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
		Vector2 offset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange)) + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition);
		CalamityUtils.GetScreenDrawArea(unscaledPosition, offset, out var firstTileX, out var lastTileX, out var firstTileY, out var lastTileY);
		for (int x = firstTileX; x <= lastTileX; x++)
		{
			for (int y = firstTileY; y <= lastTileY; y++)
			{
				if (WorldGen.InWorld(x, y))
				{
					Tile tile = Main.tile[x, y];
					if (tile.Get<TileSpecialDrawData>().HasBlendMergeData && CalamityTileSets.DrawBlendMergeAfterSolidTile[tile.TileType] && TryGetBlendingRefData(x, y, out var blendRefs))
					{
						DrawOnTile(tile, x, y, in blendRefs);
					}
				}
			}
		}
	}

	public static void DrawOnTile(Tile tile, int tileX, int tileY, in TileBlendingRef[] blendRefs)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		ushort tileType = tile.TileType;
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawPos = new Vector2((float)(tileX * 16), (float)(tileY * 16)) - Main.screenPosition + zero;
		int tileRandomFrame = Math.Clamp(tile.TileFrameNumber, 0, 2);
		bool isFullBright = tile.IsTileFullbright;
		Color tileLight = Lighting.GetColor(tileX, tileY);
		int sliceLength = 0;
		Rectangle[] sliceRects = null;
		Color[] colorSliceBuffer = null;
		if (CalamityClientConfig.Instance.TileTextureBlendingQuality == TileBlendingQuality.High && Lighting.NotRetro && !tile.IsHalfBlock && !TileID.Sets.DontDrawTileSliced[tileType])
		{
			TileDrawing tileRenderer = Main.instance.TilesRenderer;
			if (tileLight.IsAnyChannelGreaterThan(tileRenderer._highQualityLightingRequirement))
			{
				sliceLength = 9;
				sliceRects = Rects9Slice;
				colorSliceBuffer = ColorSliceBuffer.Value;
				Lighting.GetColor9Slice(tileX, tileY, ref colorSliceBuffer);
			}
			else if (tileLight.IsAnyChannelGreaterThan(tileRenderer._mediumQualityLightingRequirement))
			{
				sliceLength = 4;
				sliceRects = Rects4Slice;
				colorSliceBuffer = ColorSliceBuffer.Value;
				Lighting.GetColor4Slice(tileX, tileY, ref colorSliceBuffer);
			}
		}
		float finalColorMultiplier = (tile.IsActuated ? 0.4f : (Main.tileShine2[tileType] ? 1.6f : 1f));
		Color finalMultColor = default(Color);
		((Color)(ref finalMultColor))._002Ector(finalColorMultiplier, finalColorMultiplier, finalColorMultiplier);
		TileBlendingRef[] array = blendRefs;
		for (int i = 0; i < array.Length; i++)
		{
			TileBlendingRef tileBlendingRef = array[i];
			ushort sheetIdx = tileBlendingRef.SheetIndex;
			byte data = tileBlendingRef.BlendData;
			if (sheetIdx == 0)
			{
				break;
			}
			SheetPositionKey key = new SheetPositionKey((BlendSideFlags)data, (byte)tileRandomFrame);
			TileBlendTexture obj = TileBlendTextureLoader.Registry[sheetIdx];
			obj.RequestBake(tileRandomFrame);
			if (!obj.TryGetDrawingInfo(key, out var texture, out var rect))
			{
				continue;
			}
			if ((sliceLength <= 0) | isFullBright)
			{
				Color drawColor = (isFullBright ? Color.White : tileLight);
				Color finalColor = CalamityUtils.ApplyPaint(tile.TileColor, drawColor, deepPaintOnly: false).MultiplyRGB(finalMultColor);
				Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)rect, finalColor, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
				continue;
			}
			for (int j = 0; j < sliceLength; j++)
			{
				Rectangle sourceSliceRect = sliceRects[j];
				sourceSliceRect.X += rect.X;
				sourceSliceRect.Y += rect.Y;
				Vector2 destinationSlicePos = drawPos + ((Rectangle)(ref sliceRects[j])).Location.ToVector2();
				Vector3 drawColorVec = (((Color)(ref tileLight)).ToVector3() + ((Color)(ref colorSliceBuffer[j])).ToVector3()) * 0.5f;
				Color finalColor2 = CalamityUtils.ApplyPaint(tile.TileColor, new Color(drawColorVec), deepPaintOnly: false).MultiplyRGB(finalMultColor);
				Main.spriteBatch.Draw(texture, destinationSlicePos, (Rectangle?)sourceSliceRect, finalColor2, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void ClearWorld()
	{
		_TileBlendingRefs.Clear();
	}

	public static void RemoveBlendingRefData(int tileX, int tileY)
	{
		if (WorldGen.InWorld(tileX, tileY))
		{
			int tileIdx = tileX + Main.tile.Width * tileY;
			_TileBlendingRefs.TryRemove(tileIdx, out var _);
		}
	}

	public static void SetBlendingRefData(int tileX, int tileY, TileBlendingRef[] blendingRef)
	{
		if (WorldGen.InWorld(tileX, tileY))
		{
			int tileIdx = tileX + Main.tile.Width * tileY;
			_TileBlendingRefs[tileIdx] = blendingRef;
		}
	}

	public static bool TryGetBlendingRefData(int tileX, int tileY, out TileBlendingRef[] blendingRefs)
	{
		if (!WorldGen.InWorld(tileX, tileY))
		{
			blendingRefs = null;
			return false;
		}
		int tileIdx = tileX + Main.tile.Width * tileY;
		return _TileBlendingRefs.TryGetValue(tileIdx, out blendingRefs);
	}

	private static void TileFrame(int i, int j, int type)
	{
		if (!WorldGen.InWorld(i, j))
		{
			return;
		}
		Tile tile = Main.tile[i, j];
		ref TileSpecialDrawData drawData = ref tile.Get<TileSpecialDrawData>();
		drawData.HasBlendMergeData = false;
		RemoveBlendingRefData(i, j);
		if (!tile.HasTile)
		{
			return;
		}
		int blendDataUniqueIndex = 0;
		Dictionary<int, BlendSideFlags> blendSidesReg = PopulateBlendSidesReg(i, j, tile.TileType);
		int regCount = blendSidesReg.Count;
		if (regCount <= 0)
		{
			return;
		}
		CalculateSides(i, j, in blendSidesReg);
		TileBlendingRef[] tileBlendingRefs = new TileBlendingRef[regCount];
		foreach (KeyValuePair<int, BlendSideFlags> pair in blendSidesReg)
		{
			int blendTextureSlot = pair.Key;
			BlendSideFlags sideFlags = pair.Value;
			if (sideFlags != BlendSideFlags.None)
			{
				tileBlendingRefs[blendDataUniqueIndex] = new TileBlendingRef((ushort)blendTextureSlot, (byte)sideFlags);
				blendDataUniqueIndex++;
			}
		}
		SetBlendingRefData(i, j, tileBlendingRefs);
		drawData.HasBlendMergeData = true;
	}

	private static void CalculateSides(int i, int j, in Dictionary<int, BlendSideFlags> blendSidesReg)
	{
		TryGetTile(i, j, out var centerTile, out var _);
		bool leftMerged = false;
		bool rightMerged = false;
		bool upMerged = false;
		bool downMerged = false;
		if (TryGetTile(i - 1, j, out var leftTile, out var leftType) && HasLeftMerge(centerTile, leftTile))
		{
			leftMerged = true;
			byte blendTextureSlot = _TileTypeToBlendTextureSlot[leftType];
			if (blendSidesReg.ContainsKey(blendTextureSlot))
			{
				blendSidesReg[blendTextureSlot] |= BlendSideFlags.Left;
			}
		}
		if (TryGetTile(i + 1, j, out var rightTile, out var rightType) && HasRightMerge(centerTile, rightTile))
		{
			rightMerged = true;
			byte blendTextureSlot2 = _TileTypeToBlendTextureSlot[rightType];
			if (blendSidesReg.ContainsKey(blendTextureSlot2))
			{
				blendSidesReg[blendTextureSlot2] |= BlendSideFlags.Right;
			}
		}
		if (TryGetTile(i, j - 1, out var upTile, out var upType) && HasUpMerge(centerTile, upTile))
		{
			upMerged = true;
			byte blendTextureSlot3 = _TileTypeToBlendTextureSlot[upType];
			if (blendSidesReg.ContainsKey(blendTextureSlot3))
			{
				blendSidesReg[blendTextureSlot3] |= BlendSideFlags.Up;
			}
		}
		if (TryGetTile(i, j + 1, out var downTile, out var downType) && HasDownMerge(centerTile, downTile))
		{
			downMerged = true;
			byte blendTextureSlot4 = _TileTypeToBlendTextureSlot[downType];
			if (blendSidesReg.ContainsKey(blendTextureSlot4))
			{
				blendSidesReg[blendTextureSlot4] |= BlendSideFlags.Down;
			}
		}
		bool upLeftMerged = false;
		bool upRightMerged = false;
		bool downLeftMerged = false;
		bool downRightMerged = false;
		if ((TryGetTile(i - 1, j - 1, out var upLeftTile, out var upLeftType) & upMerged & leftMerged) && HasRightMerge(upLeftTile, upTile) && HasDownMerge(upLeftTile, leftTile))
		{
			upLeftMerged = true;
			byte blendTextureSlot5 = _TileTypeToBlendTextureSlot[upLeftType];
			if (blendSidesReg.ContainsKey(blendTextureSlot5))
			{
				blendSidesReg[blendTextureSlot5] |= BlendSideFlags.UpLeft;
			}
		}
		if ((TryGetTile(i + 1, j - 1, out var upRightTile, out var upRightType) & upMerged & rightMerged) && HasLeftMerge(upRightTile, upTile) && HasDownMerge(upRightTile, leftTile))
		{
			upRightMerged = true;
			byte blendTextureSlot6 = _TileTypeToBlendTextureSlot[upRightType];
			if (blendSidesReg.ContainsKey(blendTextureSlot6))
			{
				blendSidesReg[blendTextureSlot6] |= BlendSideFlags.UpRight;
			}
		}
		if ((TryGetTile(i - 1, j + 1, out var downLeftTile, out var downLeftType) & downMerged & leftMerged) && HasRightMerge(downLeftTile, downTile) && HasUpMerge(downLeftTile, leftTile))
		{
			downLeftMerged = true;
			byte blendTextureSlot7 = _TileTypeToBlendTextureSlot[downLeftType];
			if (blendSidesReg.ContainsKey(blendTextureSlot7))
			{
				blendSidesReg[blendTextureSlot7] |= BlendSideFlags.DownLeft;
			}
		}
		if ((TryGetTile(i + 1, j + 1, out var downRightTile, out var downRightType) & downMerged & rightMerged) && HasLeftMerge(downRightTile, downTile) && HasUpMerge(downLeftTile, leftTile))
		{
			downRightMerged = true;
			byte blendTextureSlot8 = _TileTypeToBlendTextureSlot[downRightType];
			if (blendSidesReg.ContainsKey(blendTextureSlot8))
			{
				blendSidesReg[blendTextureSlot8] |= BlendSideFlags.DownRight;
			}
		}
		foreach (KeyValuePair<int, BlendSideFlags> kv in blendSidesReg)
		{
			int slot = kv.Key;
			BlendSideFlags sides = kv.Value;
			if (sides.HasFlag(BlendSideFlags.Up))
			{
				if ((leftMerged & upLeftMerged) && IsBlendableOrSame(leftType, slot) && IsBlendableOrSame(upLeftType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.UpLeft;
				}
				if ((rightMerged & upRightMerged) && IsBlendableOrSame(rightType, slot) && IsBlendableOrSame(upRightType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.UpRight;
				}
			}
			if (sides.HasFlag(BlendSideFlags.Down))
			{
				if ((leftMerged & downLeftMerged) && IsBlendableOrSame(leftType, slot) && IsBlendableOrSame(downLeftType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.DownLeft;
				}
				if ((rightMerged & downRightMerged) && IsBlendableOrSame(rightType, slot) && IsBlendableOrSame(downRightType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.DownRight;
				}
			}
			if (sides.HasFlag(BlendSideFlags.Left))
			{
				if ((upLeftMerged & upMerged) && IsBlendableOrSame(upLeftType, slot) && IsBlendableOrSame(upType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.UpLeft;
				}
				if ((downLeftMerged & downMerged) && IsBlendableOrSame(downLeftType, slot) && IsBlendableOrSame(downType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.DownLeft;
				}
			}
			if (sides.HasFlag(BlendSideFlags.Right))
			{
				if ((upRightMerged & upMerged) && IsBlendableOrSame(upRightType, slot) && IsBlendableOrSame(upType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.UpRight;
				}
				if ((downRightMerged & downMerged) && IsBlendableOrSame(downRightType, slot) && IsBlendableOrSame(downType, slot))
				{
					blendSidesReg[slot] |= BlendSideFlags.DownRight;
				}
			}
		}
	}

	private static bool IsBlendableOrSame(int tileType, int blendTextureSlot)
	{
		if (_TileBlendable[tileType, blendTextureSlot])
		{
			return true;
		}
		if (!_TileBlendLooselyFillDiagonal[tileType, blendTextureSlot] && _TileTypeToBlendTextureSlot[tileType] == blendTextureSlot)
		{
			return true;
		}
		return false;
	}

	private static bool HasLeftMerge(Tile tileOnCenter, Tile tileOnLeft)
	{
		if (!IsMergable(tileOnCenter.TileType, tileOnLeft.TileType))
		{
			return false;
		}
		if (!HasLeftSolid(tileOnCenter))
		{
			return false;
		}
		if (!HasRightSolid(tileOnLeft))
		{
			return false;
		}
		return true;
	}

	private static bool HasRightMerge(Tile tileOnCenter, Tile tileOnRight)
	{
		if (!IsMergable(tileOnCenter.TileType, tileOnRight.TileType))
		{
			return false;
		}
		if (!HasRightSolid(tileOnCenter))
		{
			return false;
		}
		if (!HasLeftSolid(tileOnRight))
		{
			return false;
		}
		return true;
	}

	private static bool HasUpMerge(Tile tileOnCenter, Tile tileOnUp)
	{
		if (!IsMergable(tileOnCenter.TileType, tileOnUp.TileType))
		{
			return false;
		}
		if (!HasUpSolid(tileOnCenter))
		{
			return false;
		}
		if (!HasDownSolid(tileOnUp))
		{
			return false;
		}
		return true;
	}

	private static bool HasDownMerge(Tile tileOnCenter, Tile tileOnDown)
	{
		if (!IsMergable(tileOnCenter.TileType, tileOnDown.TileType))
		{
			return false;
		}
		if (!HasDownSolid(tileOnCenter))
		{
			return false;
		}
		if (!HasUpSolid(tileOnDown))
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool HasLeftSolid(Tile tile)
	{
		return (tile.BlockType & BlockType.HalfBlock) == 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool HasRightSolid(Tile tile)
	{
		int b = (int)tile.BlockType;
		if (b != 0 && b != 3)
		{
			return b == 5;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool HasUpSolid(Tile tile)
	{
		int b = (int)tile.BlockType;
		if (b >= 2)
		{
			return b > 3;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool HasDownSolid(Tile tile)
	{
		int b = (int)tile.BlockType;
		if ((b & 4) == 0)
		{
			return b != 1;
		}
		return false;
	}

	private static bool IsMergable(int type, int otherType)
	{
		if (type < 0 || otherType < 0)
		{
			return false;
		}
		if (type == otherType)
		{
			return true;
		}
		bool canBeMerged = Main.tileMerge[type][otherType];
		if (Main.tileBlendAll[type])
		{
			if (canBeMerged)
			{
				return !TileID.Sets.BlockMergesWithMergeAllBlock[otherType];
			}
			return false;
		}
		return canBeMerged;
	}

	private static Dictionary<int, BlendSideFlags> PopulateBlendSidesReg(int i, int j, int centerType)
	{
		if (TempBlendSidesReg == null)
		{
			TempBlendSidesReg = new Dictionary<int, BlendSideFlags>(8);
		}
		TempBlendSidesReg.Clear();
		Dictionary<int, BlendSideFlags> reg = TempBlendSidesReg;
		int left = GetTileType(i - 1, j);
		int right = GetTileType(i + 1, j);
		int up = GetTileType(i, j - 1);
		int upLeft = GetTileType(i - 1, j - 1);
		int upRight = GetTileType(i + 1, j - 1);
		int down = GetTileType(i, j + 1);
		int downLeft = GetTileType(i - 1, j + 1);
		int tileType = GetTileType(i + 1, j + 1);
		Populate(left);
		Populate(right);
		Populate(up);
		Populate(upLeft);
		Populate(upRight);
		Populate(down);
		Populate(downLeft);
		Populate(tileType);
		return reg;
		void Populate(int sideType)
		{
			if (sideType >= 0)
			{
				byte blendTextureSlot = _TileTypeToBlendTextureSlot[sideType];
				if (blendTextureSlot != 0 && _TileBlendable[centerType, blendTextureSlot])
				{
					reg[blendTextureSlot] = BlendSideFlags.None;
				}
			}
		}
	}

	private static int GetTileType(int i, int j)
	{
		if (!WorldGen.InWorld(i, j))
		{
			return -1;
		}
		Tile tile = Main.tile[i, j];
		if (!tile.HasTile)
		{
			return -1;
		}
		return tile.TileType;
	}

	private static bool TryGetTile(int i, int j, out Tile tile, out int type)
	{
		if (!WorldGen.InWorld(i, j))
		{
			tile = default(Tile);
			type = -1;
			return false;
		}
		tile = Main.tile[i, j];
		if (!tile.HasTile)
		{
			tile = default(Tile);
			type = -1;
			return false;
		}
		type = tile.TileType;
		return true;
	}

	static TileBlendMergeSystem()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		_TileBlendable = new bool[TileID.Count, 1];
		_TileBlendLooselyFillDiagonal = new bool[TileID.Count, 1];
		_TileTypeToBlendTextureSlot = new byte[TileID.Count];
		Rects9Slice = (Rectangle[])(object)new Rectangle[9]
		{
			new Rectangle(0, 0, 4, 4),
			new Rectangle(4, 0, 8, 4),
			new Rectangle(12, 0, 4, 4),
			new Rectangle(0, 4, 4, 8),
			new Rectangle(4, 4, 8, 8),
			new Rectangle(12, 4, 4, 8),
			new Rectangle(0, 12, 4, 4),
			new Rectangle(4, 12, 8, 4),
			new Rectangle(12, 12, 4, 4)
		};
		Rects4Slice = (Rectangle[])(object)new Rectangle[4]
		{
			new Rectangle(0, 0, 8, 8),
			new Rectangle(8, 0, 8, 8),
			new Rectangle(0, 8, 8, 8),
			new Rectangle(8, 8, 8, 8)
		};
		ColorSliceBuffer = new ThreadLocal<Color[]>(() => (Color[])(object)new Color[9]);
		_TileBlendingRefs = new ConcurrentDictionary<int, TileBlendingRef[]>();
	}
}
