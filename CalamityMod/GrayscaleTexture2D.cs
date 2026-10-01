using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Threading;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod;

public sealed class GrayscaleTexture2D : IDeferredLoadTexture
{
	private int _Width;

	private int _Height;

	private float[] _Scales;

	private Asset<Texture2D> _Asset;

	private bool _Prepared;

	public Texture2D Texture { get; private set; }

	public bool IsAssetLoaded => _Asset?.IsLoaded ?? false;

	public GrayscaleTexture2D(string assetName)
	{
		if (!Main.dedServ)
		{
			_Asset = ModContent.Request<Texture2D>(assetName, (AssetRequestMode)2);
			if (_Asset != null)
			{
				Texture = _Asset.Value;
				DeferredTextureLoadingManager.Enqueue(this);
			}
		}
	}

	public void Unload()
	{
		_Width = 0;
		_Height = 0;
		_Scales = null;
		_Asset = null;
		Texture = null;
		_Prepared = false;
	}

	public void OnTextureLoaded()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		if (_Prepared)
		{
			return;
		}
		Texture = _Asset.Value;
		if (Texture == null)
		{
			return;
		}
		_Width = Texture.Width;
		_Height = Texture.Height;
		_Scales = new float[_Width * _Height];
		Color[] colorScheme = (Color[])(object)new Color[_Width * _Height];
		Texture.GetData<Color>(colorScheme);
		FastParallel.For(0, _Width * _Height, (ParallelForAction)delegate(int startInclusive, int endExclusive, object context)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				_Scales[i] = (float)(int)((Color)(ref colorScheme[i])).R / 255f;
			}
		}, (object)null);
		_Prepared = true;
	}

	public float GetClamp(int x, int y)
	{
		if (!_Prepared)
		{
			return 0f;
		}
		if (_Width == 0 || _Height == 0)
		{
			return 0f;
		}
		x = Math.Clamp(x, 0, _Width - 1);
		y = Math.Clamp(x, 0, _Height - 1);
		return _Scales[x + y * _Height];
	}

	public float GetRepeat(int x, int y)
	{
		if (!_Prepared)
		{
			return 0f;
		}
		if (_Width == 0 || _Height == 0)
		{
			return 0f;
		}
		x %= _Width;
		y %= _Height;
		return _Scales[x + y * _Height];
	}
}
