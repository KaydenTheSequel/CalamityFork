using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public abstract class GlowMaskTile : ModTile
{
	public enum PaintColorTint
	{
		OnlyByDeepPaint,
		ByEveryPaint,
		None
	}

	public FramedMaskTexture GlowMask;

	internal static GlowMaskTile[] InstanceLookup;

	internal static int LookupLength;

	public PaintColorTint GlowMaskPaintInteraction;

	public bool GlowMaskAffectedByLight = true;

	public bool GlowMaskCanBeCulled = true;

	public virtual string GlowMaskAsset => Texture + "Glow";

	public sealed override void SetStaticDefaults()
	{
		if (GlowMask != null)
		{
			CalamityMod.Log.Error((object)(Name + " has called SetStaticDefaults themselve! This is not allowed!"));
			return;
		}
		GlowMask = new FramedMaskTexture(GlowMaskAsset, 18, 18);
		if (InstanceLookup == null)
		{
			InstanceLookup = new GlowMaskTile[TileLoader.TileCount];
		}
		LookupLength = InstanceLookup.Length;
		InstanceLookup[base.Type] = this;
		SetupStatic();
	}

	public sealed override void Unload()
	{
		GlowMask?.Unload();
		GlowMask = null;
		InstanceLookup = null;
		OnUnload();
	}

	public virtual void SetupStatic()
	{
	}

	public virtual void OnUnload()
	{
	}

	public abstract Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData);
}
