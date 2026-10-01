using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SmallSunkenStalactites : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.AnchorBottom = default(AnchorData);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 3;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.newTile.DrawYOffset = -2;
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(31, 92, 114));
		base.SetStaticDefaults();
	}
}
