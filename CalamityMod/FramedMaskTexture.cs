using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod;

public sealed class FramedMaskTexture : IDeferredLoadTexture
{
	public Texture2D Texture;

	private int _FrameWidth;

	private int _FrameHeight;

	private int _FrameXCount;

	private int _FrameYCount;

	private int _TextureWidth;

	private int _TextureHeight;

	private bool[] _HasMaskContent;

	private Asset<Texture2D> _Asset;

	private readonly bool _EveryFrameHasContent;

	private bool _Prepared;

	public bool IsAssetLoaded => _Asset?.IsLoaded ?? false;

	public int FrameXCount => _FrameXCount;

	public int FrameYCount => _FrameYCount;

	public int FrameWidth => _FrameWidth;

	public int FrameHeight => _FrameHeight;

	public int TextureWidth => _TextureWidth;

	public int TextureHeight => _TextureHeight;

	public FramedMaskTexture(string asset, int frameWidth, int frameHeight, bool pretendEveryFrameHaveContent = false)
	{
		_FrameWidth = frameWidth;
		_FrameHeight = frameHeight;
		_EveryFrameHasContent = pretendEveryFrameHaveContent;
		_Prepared = pretendEveryFrameHaveContent;
		if (!Main.dedServ)
		{
			_Asset = ModContent.Request<Texture2D>(asset, (AssetRequestMode)2);
			Texture = _Asset.Value;
			DeferredTextureLoadingManager.Enqueue(this);
		}
	}

	public void OnTextureLoaded()
	{
		Texture = _Asset.Value;
		if (_Prepared || Texture == null)
		{
			return;
		}
		_TextureWidth = Texture.Width;
		_TextureHeight = Texture.Height;
		_FrameXCount = _TextureWidth / _FrameWidth;
		_FrameYCount = _TextureHeight / _FrameHeight;
		_HasMaskContent = new bool[FrameXCount * FrameYCount];
		Color[] colData = (Color[])(object)new Color[_TextureWidth * _TextureHeight];
		Texture.GetData<Color>(colData);
		Parallel.For(0, FrameXCount * FrameYCount, delegate(int i)
		{
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			int num = i % FrameXCount;
			int num2 = i / FrameXCount;
			int num3 = num * _FrameWidth;
			int num4 = Math.Min(num3 + _FrameWidth, _TextureWidth);
			int num5 = num2 * _FrameHeight;
			int num6 = Math.Min(num5 + _FrameHeight, _TextureHeight);
			bool flag = false;
			for (int j = num3; j < num4; j++)
			{
				if (flag)
				{
					break;
				}
				for (int k = num5; k < num6; k++)
				{
					Color val = colData[j + k * _TextureWidth];
					if (((Color)(ref val)).A >= 1)
					{
						flag = true;
						break;
					}
				}
			}
			_HasMaskContent[num + num2 * _FrameXCount] = flag;
		});
		_Prepared = true;
	}

	public void Unload()
	{
		Texture = null;
		_Asset = null;
		_Prepared = false;
		_FrameWidth = 0;
		_FrameHeight = 0;
		_FrameXCount = 0;
		_FrameYCount = 0;
	}

	public bool HasContentInFrameIndex(int xFrame, int yFrame)
	{
		if (!_Prepared)
		{
			return false;
		}
		if (_EveryFrameHasContent)
		{
			return true;
		}
		if (Texture == null)
		{
			return false;
		}
		if (xFrame < 0 || xFrame >= _FrameXCount)
		{
			return false;
		}
		if (yFrame < 0 || yFrame >= _FrameYCount)
		{
			return false;
		}
		return _HasMaskContent[xFrame + yFrame * _FrameXCount];
	}

	public bool HasContentInFramePos(int xPos, int yPos)
	{
		if (!_Prepared)
		{
			return false;
		}
		if (_EveryFrameHasContent)
		{
			return true;
		}
		int xFrame = xPos / _FrameWidth;
		int yFrame = yPos / _FrameHeight;
		if (xFrame < 0 || xFrame >= _FrameXCount)
		{
			return false;
		}
		if (yFrame < 0 || yFrame >= _FrameYCount)
		{
			return false;
		}
		return _HasMaskContent[xFrame + yFrame * _FrameXCount];
	}
}
