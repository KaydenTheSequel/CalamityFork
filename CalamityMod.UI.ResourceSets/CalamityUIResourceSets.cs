using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.UI.ResourceSets;

public sealed class CalamityUIResourceSets : ILoadable
{
	public static string BasePath => "CalamityMod/UI/ResourceSets/";

	public static CalamityUIResourceSet HPChalice { get; private set; }

	public static CalamityUIResourceSet HPChaliceBleed { get; private set; }

	public static CalamityUIResourceSet HPMiracleFruit { get; private set; }

	public static CalamityUIResourceSet HPSacredStrawberry { get; private set; }

	public static CalamityUIResourceSet HPSanguineTangerine { get; private set; }

	public static CalamityUIResourceSet HPTaintedCloudberry { get; private set; }

	public static CalamityUIResourceSet MPCometShard { get; private set; }

	public static CalamityUIResourceSet MPEtherealCore { get; private set; }

	public static CalamityUIResourceSet MPManaBurn { get; private set; }

	public static CalamityUIResourceSet MPPhantomHeart { get; private set; }

	private static CalamityUIResourceSet LoadResourceSet(string path, bool isHP)
	{
		return new CalamityUIResourceSet(ModContent.Request<Texture2D>(BasePath + path + "Bar", (AssetRequestMode)2), ModContent.Request<Texture2D>(BasePath + path + (isHP ? "Heart" : "Star"), (AssetRequestMode)2));
	}

	void ILoadable.Load(Mod mod)
	{
		HPChalice = LoadResourceSet("HPChalice", isHP: true);
		HPChaliceBleed = LoadResourceSet("HPChaliceBleed", isHP: true);
		HPMiracleFruit = LoadResourceSet("HPMiracleFruit", isHP: true);
		HPSacredStrawberry = LoadResourceSet("HPSacredStrawberry", isHP: true);
		HPSanguineTangerine = LoadResourceSet("HPSanguineTangerine", isHP: true);
		HPTaintedCloudberry = LoadResourceSet("HPTaintedCloudberry", isHP: true);
		MPCometShard = LoadResourceSet("MPCometShard", isHP: false);
		MPEtherealCore = LoadResourceSet("MPEtherealCore", isHP: false);
		MPManaBurn = LoadResourceSet("MPManaBurn", isHP: false);
		MPPhantomHeart = LoadResourceSet("MPPhantomHeart", isHP: false);
	}

	void ILoadable.Unload()
	{
		HPChalice = null;
		HPChaliceBleed = null;
		HPMiracleFruit = null;
		HPSacredStrawberry = null;
		HPSanguineTangerine = null;
		HPTaintedCloudberry = null;
		MPCometShard = null;
		MPEtherealCore = null;
		MPManaBurn = null;
		MPPhantomHeart = null;
	}
}
