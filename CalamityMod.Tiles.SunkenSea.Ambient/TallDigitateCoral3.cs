using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class TallDigitateCoral3 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(0, 4);
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(48, 201, 214));
		base.DustType = 96;
		base.HitSound = SoundID.Dig;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.14f;
		g = 0.41f;
		b = 0.45f;
	}
}
