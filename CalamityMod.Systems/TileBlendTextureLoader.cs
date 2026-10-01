using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

internal sealed class TileBlendTextureLoader : ModSystem
{
	internal const int EmptySlot = 0;

	internal const int StartingIndex = 1;

	internal const int MaxCount = 65535;

	internal static TileBlendTexture[] Registry = new TileBlendTexture[65535];

	private static int _UniqueSlot = 1;

	internal static IEnumerable<TileBlendTexture> AllTextures => Registry?.Where((TileBlendTexture tex) => tex != null) ?? Array.Empty<TileBlendTexture>();

	internal static int Count => _UniqueSlot - 1;

	public override void Load()
	{
		Registry = new TileBlendTexture[65535];
	}

	public override void Unload()
	{
		Registry = null;
		_UniqueSlot = 1;
	}

	public override void ResizeArrays()
	{
		IEnumerable<TileBlendTexture> textures = ModContent.GetContent<TileBlendTexture>();
		Array.Resize(ref Registry, textures.Count() + 1);
	}

	internal static int Register(TileBlendTexture sheet)
	{
		if (sheet.Slot >= 0)
		{
			throw new ArgumentException("Argument has already registered to System", "sheet");
		}
		if (_UniqueSlot >= 65535)
		{
			throw new InvalidOperationException($"Slots are all used up to {65535}, We can't allocate more!");
		}
		int slot = _UniqueSlot++;
		Registry[slot] = sheet;
		return slot;
	}
}
