using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.Stalactite;

public class SulphurousStalactite2 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.DrawYOffset = -2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(57, 48, 83), CalamityUtils.GetText("Tiles.Stalactite"));
		base.DustType = 75;
		base.SetStaticDefaults();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}
}
