using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles;

public class PlacedRock : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		base.DustType = 1;
		AddMapEntry(new Color(83, 91, 102));
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		WorldGen.Check1x1(i, j, base.Type);
		return true;
	}

	public override void PlaceInWorld(int i, int j, Item item)
	{
		Main.tile[i, j].TileFrameX = 0;
		Main.tile[i, j].TileFrameY = 0;
	}
}
