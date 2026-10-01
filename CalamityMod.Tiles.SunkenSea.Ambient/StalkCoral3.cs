using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class StalkCoral3 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 2);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(156, 87, 57));
		base.DustType = 78;
		base.HitSound = SoundID.Dig;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.427f;
		g = 0.384f;
		b = 0.369f;
	}
}
