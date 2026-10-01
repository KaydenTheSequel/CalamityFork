using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags.Tree;

public class SpineSapling : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileID.Sets.CommonSapling[base.Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<BrimstoneSlag>() };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.DrawFlipHorizontal = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.newTile.StyleMultiplier = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(38, 25, 27), CreateMapEntryName());
		base.DustType = 5;
		base.AdjTiles = new int[1] { 20 };
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void RandomUpdate(int i, int j)
	{
		if (WorldGen.genRand.NextBool(20) && Main.tile[i, j + 1].TileType != base.Type)
		{
			SpineTree.Spawn(i, j, 22, 28, saplingExists: true);
		}
	}

	public override bool CanDrop(int i, int j)
	{
		return false;
	}
}
