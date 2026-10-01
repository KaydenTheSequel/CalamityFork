using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod;

internal sealed class DeferredTextureLoadingManager : ModSystem
{
	private static readonly List<IDeferredLoadTexture> Textures = new List<IDeferredLoadTexture>();

	private static readonly List<IDeferredLoadTexture> TexturesToRemove = new List<IDeferredLoadTexture>();

	private static bool HasItem = false;

	public static void Enqueue(IDeferredLoadTexture texture)
	{
		Main.QueueMainThreadAction(delegate
		{
			Textures.Add(texture);
			HasItem = true;
		});
	}

	public override void Load()
	{
		Main.OnTickForThirdPartySoftwareOnly += DoUpdate;
	}

	public override void Unload()
	{
		Main.OnTickForThirdPartySoftwareOnly -= DoUpdate;
	}

	private static void DoUpdate()
	{
		if (!HasItem)
		{
			return;
		}
		int limitPerTick = 2;
		foreach (IDeferredLoadTexture texture in Textures)
		{
			if (texture.IsAssetLoaded)
			{
				texture.OnTextureLoaded();
				TexturesToRemove.Add(texture);
				limitPerTick--;
			}
			if (limitPerTick <= 0)
			{
				break;
			}
		}
		foreach (IDeferredLoadTexture toRemove in TexturesToRemove)
		{
			Textures.Remove(toRemove);
		}
		TexturesToRemove.Clear();
		HasItem = Textures.Count != 0;
	}
}
