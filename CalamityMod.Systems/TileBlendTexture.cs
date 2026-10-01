using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

[Autoload(true, Side = ModSide.Client)]
public abstract class TileBlendTexture : ModTexturedType
{
	public const byte EmptySheetIndex = byte.MaxValue;

	public const int VariantCount = 3;

	public const int BlendTextureXCount = 17;

	public const int BlendTextureYCount = 12;

	public const int BlendTextureFrameWidth = 16;

	public const int BlendTextureFrameHeight = 16;

	public const int BlendTextureWidth = 272;

	public const int BlendTextureHeight = 192;

	public const int BlendTextureFullHeight = 576;

	internal static int BakedCountInFrame = 0;

	private bool[] _IsBaked = new bool[3];

	private bool[] _RequestedVariants = new bool[3];

	private bool _IsRequestedAny;

	private bool _IsSheetRequested;

	private bool _ShouldClearRT = true;

	private static readonly Rectangle[] _I_Up = Create3RectsDirX(1, 2);

	private static readonly Rectangle[] _I_Up_End = Create3RectsDirX(8, 6);

	private static readonly Rectangle[] _I_Up_LeftEnd = Create3RectsDirY(6, 6);

	private static readonly Rectangle[] _I_Up_RightEnd = Create3RectsDirY(7, 6);

	private static readonly Rectangle[] _I_Down = Create3RectsDirX(1, 0);

	private static readonly Rectangle[] _I_Down_End = Create3RectsDirX(8, 3);

	private static readonly Rectangle[] _I_Down_LeftEnd = Create3RectsDirY(6, 3);

	private static readonly Rectangle[] _I_Down_RightEnd = Create3RectsDirY(7, 3);

	private static readonly Rectangle[] _I_Left = Create3RectsDirY(4, 0);

	private static readonly Rectangle[] _I_Left_End = Create3RectsDirY(8, 0);

	private static readonly Rectangle[] _I_Left_UpEnd = Create3RectsDirX(3, 9);

	private static readonly Rectangle[] _I_Left_DownEnd = Create3RectsDirX(3, 10);

	private static readonly Rectangle[] _I_Right = Create3RectsDirY(0, 0);

	private static readonly Rectangle[] _I_Right_End = Create3RectsDirY(5, 0);

	private static readonly Rectangle[] _I_Right_UpEnd = Create3RectsDirX(0, 9);

	private static readonly Rectangle[] _I_Right_DownEnd = Create3RectsDirX(0, 10);

	private static readonly Rectangle[] _L_UpLeft = Create3RectsDirX(0, 5, 2);

	private static readonly Rectangle[] _L_UpLeft_End = Create3RectsDirX(0, 7, 2);

	private static readonly Rectangle[] _L_UpLeft_RightEnd = Create3RectsDirY(11, 3, 2);

	private static readonly Rectangle[] _L_UpLeft_DownEnd = Create3RectsDirY(13, 3, 2);

	private static readonly Rectangle[] _L_UpRight = Create3RectsDirX(1, 5, 2);

	private static readonly Rectangle[] _L_UpRight_End = Create3RectsDirX(1, 7, 2);

	private static readonly Rectangle[] _L_UpRight_LeftEnd = Create3RectsDirY(12, 3, 2);

	private static readonly Rectangle[] _L_UpRight_DownEnd = Create3RectsDirY(14, 3, 2);

	private static readonly Rectangle[] _L_DownLeft = Create3RectsDirX(0, 6, 2);

	private static readonly Rectangle[] _L_DownLeft_End = Create3RectsDirX(0, 8, 2);

	private static readonly Rectangle[] _L_DownLeft_RightEnd = Create3RectsDirY(11, 4, 2);

	private static readonly Rectangle[] _L_DownLeft_UpEnd = Create3RectsDirY(13, 4, 2);

	private static readonly Rectangle[] _L_DownRight = Create3RectsDirX(1, 6, 2);

	private static readonly Rectangle[] _L_DownRight_End = Create3RectsDirX(1, 8, 2);

	private static readonly Rectangle[] _L_DownRight_LeftEnd = Create3RectsDirY(12, 4, 2);

	private static readonly Rectangle[] _L_DownRight_UpEnd = Create3RectsDirY(14, 4, 2);

	private static readonly Rectangle[] _U_UpEmpty = Create3RectsDirX(8, 5);

	private static readonly Rectangle[] _U_UpEmpty_End = Create3RectsDirX(8, 8);

	private static readonly Rectangle[] _U_UpEmpty_LeftEnd = Create3RectsDirX(8, 10);

	private static readonly Rectangle[] _U_UpEmpty_RightEnd = Create3RectsDirX(11, 10);

	private static readonly Rectangle[] _U_DownEmpty = Create3RectsDirX(8, 4);

	private static readonly Rectangle[] _U_DownEmpty_End = Create3RectsDirX(8, 7);

	private static readonly Rectangle[] _U_DownEmpty_LeftEnd = Create3RectsDirX(8, 9);

	private static readonly Rectangle[] _U_DownEmpty_RightEnd = Create3RectsDirX(11, 9);

	private static readonly Rectangle[] _U_LeftEmpty = Create3RectsDirY(7, 0);

	private static readonly Rectangle[] _U_LeftEmpty_End = Create3RectsDirY(10, 0);

	private static readonly Rectangle[] _U_LeftEmpty_UpEnd = Create3RectsDirY(12, 0);

	private static readonly Rectangle[] _U_LeftEmpty_DownEnd = Create3RectsDirY(14, 0);

	private static readonly Rectangle[] _U_RightEmpty = Create3RectsDirY(6, 0);

	private static readonly Rectangle[] _U_RightEmpty_End = Create3RectsDirY(9, 0);

	private static readonly Rectangle[] _U_RightEmpty_UpEnd = Create3RectsDirY(11, 0);

	private static readonly Rectangle[] _U_RightEmpty_DownEnd = Create3RectsDirY(13, 0);

	private static readonly Rectangle[] _AllClosed = Create3RectsDirX(1, 1);

	private static readonly Rectangle[] _Corner_UpLeft = Create3RectsDirX(1, 4, 2);

	private static readonly Rectangle[] _Corner_UpRight = Create3RectsDirX(0, 4, 2);

	private static readonly Rectangle[] _Corner_DownLeft = Create3RectsDirX(1, 3, 2);

	private static readonly Rectangle[] _Corner_DownRight = Create3RectsDirX(0, 3, 2);

	private static readonly Dictionary<BlendSideFlags, Rectangle[]> _BasicShapeLookup = new Dictionary<BlendSideFlags, Rectangle[]>
	{
		[BlendSideFlags.AllSide] = _AllClosed,
		[BlendSideFlags.UpLeft] = _Corner_UpLeft,
		[BlendSideFlags.UpRight] = _Corner_UpRight,
		[BlendSideFlags.DownLeft] = _Corner_DownLeft,
		[BlendSideFlags.DownRight] = _Corner_DownRight,
		[BlendSideFlags.ShapeI_Up] = _I_Up,
		[BlendSideFlags.Up] = _I_Up_End,
		[BlendSideFlags.ShapeI_Up_RightEnd] = _I_Up_RightEnd,
		[BlendSideFlags.ShapeI_Up_LeftEnd] = _I_Up_LeftEnd,
		[BlendSideFlags.ShapeI_Down] = _I_Down,
		[BlendSideFlags.Down] = _I_Down_End,
		[BlendSideFlags.ShapeI_Down_RightEnd] = _I_Down_RightEnd,
		[BlendSideFlags.ShapeI_Down_LeftEnd] = _I_Down_LeftEnd,
		[BlendSideFlags.ShapeI_Left] = _I_Left,
		[BlendSideFlags.Left] = _I_Left_End,
		[BlendSideFlags.ShapeI_Left_DownEnd] = _I_Left_DownEnd,
		[BlendSideFlags.ShapeI_Left_UpEnd] = _I_Left_UpEnd,
		[BlendSideFlags.ShapeI_Right] = _I_Right,
		[BlendSideFlags.Right] = _I_Right_End,
		[BlendSideFlags.ShapeI_Right_DownEnd] = _I_Right_DownEnd,
		[BlendSideFlags.ShapeI_Right_UpEnd] = _I_Right_UpEnd,
		[BlendSideFlags.ShapeL_UpLeft] = _L_UpLeft,
		[BlendSideFlags.ShapeL_UpLeft_End] = _L_UpLeft_End,
		[BlendSideFlags.ShapeL_UpLeft_RightEnd] = _L_UpLeft_RightEnd,
		[BlendSideFlags.ShapeL_UpLeft_DownEnd] = _L_UpLeft_DownEnd,
		[BlendSideFlags.ShapeL_UpRight] = _L_UpRight,
		[BlendSideFlags.ShapeL_UpRight_End] = _L_UpRight_End,
		[BlendSideFlags.ShapeL_UpRight_LeftEnd] = _L_UpRight_LeftEnd,
		[BlendSideFlags.ShapeL_UpRight_DownEnd] = _L_UpRight_DownEnd,
		[BlendSideFlags.ShapeL_DownLeft] = _L_DownLeft,
		[BlendSideFlags.ShapeL_DownLeft_End] = _L_DownLeft_End,
		[BlendSideFlags.ShapeL_DownLeft_RightEnd] = _L_DownLeft_RightEnd,
		[BlendSideFlags.ShapeL_DownLeft_UpEnd] = _L_DownLeft_UpEnd,
		[BlendSideFlags.ShapeL_DownRight] = _L_DownRight,
		[BlendSideFlags.ShapeL_DownRight_End] = _L_DownRight_End,
		[BlendSideFlags.ShapeL_DownRight_LeftEnd] = _L_DownRight_LeftEnd,
		[BlendSideFlags.ShapeL_DownRight_UpEnd] = _L_DownRight_UpEnd,
		[BlendSideFlags.ShapeU_UpEmpty] = _U_UpEmpty,
		[BlendSideFlags.ShapeU_UpEmpty_End] = _U_UpEmpty_End,
		[BlendSideFlags.ShapeU_UpEmpty_LeftEnd] = _U_UpEmpty_LeftEnd,
		[BlendSideFlags.ShapeU_UpEmpty_RightEnd] = _U_UpEmpty_RightEnd,
		[BlendSideFlags.ShapeU_DownEmpty] = _U_DownEmpty,
		[BlendSideFlags.ShapeU_DownEmpty_End] = _U_DownEmpty_End,
		[BlendSideFlags.ShapeU_DownEmpty_LeftEnd] = _U_DownEmpty_LeftEnd,
		[BlendSideFlags.ShapeU_DownEmpty_RightEnd] = _U_DownEmpty_RightEnd,
		[BlendSideFlags.ShapeU_LeftEmpty] = _U_LeftEmpty,
		[BlendSideFlags.ShapeU_LeftEmpty_End] = _U_LeftEmpty_End,
		[BlendSideFlags.ShapeU_LeftEmpty_UpEnd] = _U_LeftEmpty_UpEnd,
		[BlendSideFlags.ShapeU_LeftEmpty_DownEnd] = _U_LeftEmpty_DownEnd,
		[BlendSideFlags.ShapeU_RightEmpty] = _U_RightEmpty,
		[BlendSideFlags.ShapeU_RightEmpty_End] = _U_RightEmpty_End,
		[BlendSideFlags.ShapeU_RightEmpty_UpEnd] = _U_RightEmpty_UpEnd,
		[BlendSideFlags.ShapeU_RightEmpty_DownEnd] = _U_RightEmpty_DownEnd
	};

	private static SheetPosition[] _SheetPositionLookup;

	private static readonly IReadOnlyCollection<BlendSideFlags> _Corner_Shapes = new global::_003C_003Ez__ReadOnlyArray<BlendSideFlags>(new BlendSideFlags[4]
	{
		BlendSideFlags.UpLeft,
		BlendSideFlags.UpRight,
		BlendSideFlags.DownLeft,
		BlendSideFlags.DownRight
	});

	private static readonly IReadOnlyCollection<BlendSideFlags> _I_Shapes = new global::_003C_003Ez__ReadOnlyArray<BlendSideFlags>(new BlendSideFlags[16]
	{
		BlendSideFlags.ShapeI_Up,
		BlendSideFlags.Up,
		BlendSideFlags.ShapeI_Up_LeftEnd,
		BlendSideFlags.ShapeI_Up_RightEnd,
		BlendSideFlags.ShapeI_Down,
		BlendSideFlags.Down,
		BlendSideFlags.ShapeI_Down_LeftEnd,
		BlendSideFlags.ShapeI_Down_RightEnd,
		BlendSideFlags.ShapeI_Left,
		BlendSideFlags.Left,
		BlendSideFlags.ShapeI_Left_UpEnd,
		BlendSideFlags.ShapeI_Left_DownEnd,
		BlendSideFlags.ShapeI_Right,
		BlendSideFlags.Right,
		BlendSideFlags.ShapeI_Right_UpEnd,
		BlendSideFlags.ShapeI_Right_DownEnd
	});

	private static readonly IReadOnlyCollection<BlendSideFlags> _L_Shapes = new global::_003C_003Ez__ReadOnlyArray<BlendSideFlags>(new BlendSideFlags[16]
	{
		BlendSideFlags.ShapeL_UpLeft,
		BlendSideFlags.ShapeL_UpLeft_End,
		BlendSideFlags.ShapeL_UpLeft_RightEnd,
		BlendSideFlags.ShapeL_UpLeft_DownEnd,
		BlendSideFlags.ShapeL_UpRight,
		BlendSideFlags.ShapeL_UpRight_End,
		BlendSideFlags.ShapeL_UpRight_LeftEnd,
		BlendSideFlags.ShapeL_UpRight_DownEnd,
		BlendSideFlags.ShapeL_DownLeft,
		BlendSideFlags.ShapeL_DownLeft_End,
		BlendSideFlags.ShapeL_DownLeft_RightEnd,
		BlendSideFlags.ShapeL_DownLeft_UpEnd,
		BlendSideFlags.ShapeL_DownRight,
		BlendSideFlags.ShapeL_DownRight_End,
		BlendSideFlags.ShapeL_DownRight_LeftEnd,
		BlendSideFlags.ShapeL_DownRight_UpEnd
	});

	private static readonly IReadOnlyCollection<BlendSideFlags> _U_Shapes = new global::_003C_003Ez__ReadOnlyArray<BlendSideFlags>(new BlendSideFlags[16]
	{
		BlendSideFlags.ShapeU_UpEmpty,
		BlendSideFlags.ShapeU_UpEmpty_End,
		BlendSideFlags.ShapeU_UpEmpty_LeftEnd,
		BlendSideFlags.ShapeU_UpEmpty_RightEnd,
		BlendSideFlags.ShapeU_DownEmpty,
		BlendSideFlags.ShapeU_DownEmpty_End,
		BlendSideFlags.ShapeU_DownEmpty_LeftEnd,
		BlendSideFlags.ShapeU_DownEmpty_RightEnd,
		BlendSideFlags.ShapeU_LeftEmpty,
		BlendSideFlags.ShapeU_LeftEmpty_End,
		BlendSideFlags.ShapeU_LeftEmpty_UpEnd,
		BlendSideFlags.ShapeU_LeftEmpty_DownEnd,
		BlendSideFlags.ShapeU_RightEmpty,
		BlendSideFlags.ShapeU_RightEmpty_End,
		BlendSideFlags.ShapeU_RightEmpty_UpEnd,
		BlendSideFlags.ShapeU_RightEmpty_DownEnd
	});

	private static readonly IReadOnlyCollection<IReadOnlyCollection<BlendSideFlags>> _ShapeConsumeMap = new global::_003C_003Ez__ReadOnlyArray<IReadOnlyCollection<BlendSideFlags>>(new IReadOnlyCollection<BlendSideFlags>[4]
	{
		_U_Shapes.OrderByDescending(HotFlagCount).ToImmutableArray(),
		_L_Shapes.OrderByDescending(HotFlagCount).ToImmutableArray(),
		_I_Shapes.OrderByDescending(HotFlagCount).ToImmutableArray(),
		_Corner_Shapes
	});

	public Asset<Texture2D> TextureAsset { get; private set; }

	public int Slot { get; private set; } = -1;

	public RenderTarget2D BakedBlendTexture { get; private set; }

	public abstract int TileType { get; }

	protected sealed override void Register()
	{
		CalculateSheetPositionLookup();
		ModTypeLookup<TileBlendTexture>.Register(this);
		Slot = TileBlendTextureLoader.Register(this);
		TextureAsset = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		BakedBlendTexture = null;
	}

	public sealed override void SetupContent()
	{
		SetStaticDefaults();
	}

	public sealed override void Unload()
	{
		Main.QueueMainThreadAction(delegate
		{
			RenderTarget2D bakedBlendTexture = BakedBlendTexture;
			if (bakedBlendTexture != null)
			{
				((GraphicsResource)bakedBlendTexture).Dispose();
			}
			BakedBlendTexture = null;
		});
		PostUnload();
	}

	public virtual void PostUnload()
	{
	}

	public void RebuildBlendSheet(Asset<Texture2D> texture = null)
	{
		TextureAsset = texture ?? ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		ClearBakeCache();
	}

	internal void ClearBakeCache()
	{
		_IsBaked = new bool[3];
		_RequestedVariants = new bool[3];
		_IsRequestedAny = false;
		_ShouldClearRT = true;
	}

	internal void RequestBake(int sheetIndex)
	{
		if (!_IsBaked[sheetIndex])
		{
			_RequestedVariants[sheetIndex] = true;
			_IsRequestedAny = true;
		}
	}

	internal void BakeRequestedBlendTextureCache()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (!_IsRequestedAny || !TextureAsset.IsLoaded)
		{
			return;
		}
		Texture2D texture = TextureAsset.Value;
		if (texture == null || ((GraphicsResource)texture).IsDisposed)
		{
			return;
		}
		_ = ((Game)Main.instance).GraphicsDevice;
		for (int v = 0; v < 3; v++)
		{
			int variant = v;
			if (!_RequestedVariants[variant] || BakedCountInFrame >= 3)
			{
				continue;
			}
			RenderTarget2D renderTarget = BakedBlendTexture;
			if (renderTarget != null && !((GraphicsResource)renderTarget).IsDisposed && !renderTarget.IsContentLost)
			{
				using (renderTarget.Scope(preserveContents: true, _ShouldClearRT ? new Color?(Color.Transparent) : ((Color?)null)))
				{
					_ShouldClearRT = false;
					Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
					BakeBlendTextureCache(v);
					Main.spriteBatch.End();
				}
				_RequestedVariants[variant] = false;
				_IsBaked[variant] = true;
			}
			else if (!_IsSheetRequested)
			{
				_IsSheetRequested = true;
				Main.QueueMainThreadAction(delegate
				{
					//IL_001a: Unknown result type (might be due to invalid IL or missing references)
					//IL_0024: Expected O, but got Unknown
					BakedBlendTexture = new RenderTarget2D(((Game)Main.instance).GraphicsDevice, 272, 576, false, (SurfaceFormat)0, (DepthFormat)0, 0, (RenderTargetUsage)1);
					_IsSheetRequested = false;
				});
			}
			BakedCountInFrame++;
		}
		bool[] requestedVariants = _RequestedVariants;
		foreach (bool requested in requestedVariants)
		{
			_IsRequestedAny |= requested;
		}
	}

	internal void BakeBlendTextureCache(int randomFrame)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 256; i++)
		{
			BlendSideFlags mergeSides = (BlendSideFlags)i;
			SheetPosition sheetPosition = _SheetPositionLookup[(int)new SheetPositionKey(mergeSides, (byte)randomFrame)];
			if (sheetPosition.IsUsingBaseTexture)
			{
				continue;
			}
			Vector2 drawPos = sheetPosition.GetDrawPosition();
			foreach (BlendSideFlags shape in ConsumeMergeSides(mergeSides))
			{
				if (_BasicShapeLookup.TryGetValue(shape, out var shapeRects))
				{
					Main.spriteBatch.Draw(TextureAsset.Value, drawPos, (Rectangle?)shapeRects[randomFrame], Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
			}
		}
	}

	private static IEnumerable<BlendSideFlags> ConsumeMergeSides(BlendSideFlags sideFlags)
	{
		if (sideFlags == BlendSideFlags.None)
		{
			yield break;
		}
		foreach (IReadOnlyCollection<BlendSideFlags> shapeGroup in _ShapeConsumeMap)
		{
			foreach (BlendSideFlags shape in shapeGroup)
			{
				if ((shape & sideFlags) == shape)
				{
					sideFlags = (BlendSideFlags)((uint)sideFlags & (uint)(byte)(~(int)shape));
					yield return shape;
				}
			}
			if (sideFlags == BlendSideFlags.None)
			{
				yield break;
			}
		}
	}

	public bool TryGetDrawingInfo(SheetPositionKey key, out Texture2D texture, out Rectangle sourceRect)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		SheetPosition pos = _SheetPositionLookup[(int)key];
		sourceRect = pos.GetDrawRect();
		texture = (Texture2D)(pos.IsUsingBaseTexture ? ((object)TextureAsset.Value) : ((object)BakedBlendTexture));
		return texture != null;
	}

	private static void CalculateSheetPositionLookup()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (_SheetPositionLookup != null)
		{
			return;
		}
		_SheetPositionLookup = new SheetPosition[768];
		int bakeSheetIndex = 0;
		for (int i = 0; i < 256; i++)
		{
			bool hasAddedToSheet = false;
			for (byte randomFrame = 0; randomFrame < 3; randomFrame++)
			{
				BlendSideFlags mergeSides = (BlendSideFlags)i;
				SheetPositionKey key = new SheetPositionKey(mergeSides, randomFrame);
				if (_BasicShapeLookup.TryGetValue(mergeSides, out var rects))
				{
					Rectangle rect = rects[randomFrame];
					_SheetPositionLookup[(int)key] = new SheetPosition(rect.X, rect.Y, -1);
				}
				else
				{
					int y = Math.DivRem(bakeSheetIndex, 17, out var x);
					int heightOffset = 192 * randomFrame;
					_SheetPositionLookup[(int)key] = new SheetPosition(x * 16, y * 16 + heightOffset, (sbyte)randomFrame);
					hasAddedToSheet = true;
				}
			}
			if (hasAddedToSheet)
			{
				bakeSheetIndex++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Rectangle CreateRect(int sheetX, int sheetY)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return new Rectangle(sheetX * 18, sheetY * 18, 16, 16);
	}

	private static Rectangle[] Create3RectsDirX(int leftSheetX, int leftSheetY, int increment = 1)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return (Rectangle[])(object)new Rectangle[3]
		{
			CreateRect(leftSheetX, leftSheetY),
			CreateRect(leftSheetX + increment, leftSheetY),
			CreateRect(leftSheetX + 2 * increment, leftSheetY)
		};
	}

	private static Rectangle[] Create3RectsDirY(int topSheetX, int topSheetY, int increment = 1)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return (Rectangle[])(object)new Rectangle[3]
		{
			CreateRect(topSheetX, topSheetY),
			CreateRect(topSheetX, topSheetY + increment),
			CreateRect(topSheetX, topSheetY + 2 * increment)
		};
	}

	private static int HotFlagCount(BlendSideFlags flags)
	{
		int count = 0;
		for (int i = 0; i < 8; i++)
		{
			BlendSideFlags flag = (BlendSideFlags)(1 << i);
			if (flag == (flags & flag))
			{
				count++;
			}
		}
		return count;
	}
}
