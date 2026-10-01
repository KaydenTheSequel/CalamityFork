using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.AbyssAmbient;

public class SpiderCoral1 : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(82, 49, 27));
		base.DustType = 32;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.48f;
		g = 0.12f;
		b = 0.08f;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
