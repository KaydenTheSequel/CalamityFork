using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class AcidWoodTreeSapling : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileID.Sets.CommonSapling[base.Type] = true;
		TileID.Sets.TreeSapling[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.AnchorValidTiles = new int[3]
		{
			ModContent.TileType<SulphurousSand>(),
			ModContent.TileType<HardenedSulphurousSandstone>(),
			ModContent.TileType<SulphurousSandstone>()
		};
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.DrawFlipHorizontal = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(113, 90, 71), Language.GetText("MapObject.Sapling"));
		base.DustType = 75;
		base.AdjTiles = new int[1] { 20 };
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void RandomUpdate(int i, int j)
	{
		if (!WorldGen.genRand.NextBool(20))
		{
			return;
		}
		int trueStartingPositionY;
		for (trueStartingPositionY = j; TileID.Sets.TreeSapling[Main.tile[i, trueStartingPositionY].TileType]; trueStartingPositionY++)
		{
		}
		Tile tileAtPosition = Main.tile[i, trueStartingPositionY];
		Tile tileAbovePosition = Main.tile[i, trueStartingPositionY - 1];
		if (!tileAtPosition.HasTile || tileAtPosition.IsHalfBlock || tileAtPosition.Slope != SlopeType.Solid || tileAbovePosition.WallType != 0 || tileAbovePosition.LiquidAmount != 0 || !WorldGen.EmptyTileCheck(i - 1, i + 1, trueStartingPositionY - 20, trueStartingPositionY - 1, base.Type))
		{
			return;
		}
		int treeHeight = WorldGen.genRand.Next(10, 21);
		int frameYIdeal = WorldGen.genRand.Next(-8, 9);
		frameYIdeal *= 2;
		short frameY = 0;
		for (int k = 0; k < treeHeight; k++)
		{
			tileAtPosition = Main.tile[i, trueStartingPositionY - 1 - k];
			if (k == 0)
			{
				tileAtPosition.Get<TileWallWireStateData>().HasTile = true;
				tileAtPosition.TileType = 323;
				tileAtPosition.TileFrameX = 66;
				tileAtPosition.TileFrameY = 0;
				continue;
			}
			if (k == treeHeight - 1)
			{
				tileAtPosition.Get<TileWallWireStateData>().HasTile = true;
				tileAtPosition.TileType = 323;
				tileAtPosition.TileFrameX = (short)(22 * WorldGen.genRand.Next(4, 7));
				tileAtPosition.TileFrameY = frameY;
				continue;
			}
			if (frameY != frameYIdeal)
			{
				float heightRatio = (float)k / (float)treeHeight;
				if (heightRatio >= 0.25f && ((heightRatio < 0.5f && WorldGen.genRand.NextBool(13)) || (heightRatio < 0.7f && WorldGen.genRand.NextBool(9)) || heightRatio >= 0.95f || WorldGen.genRand.Next(5) != 0 || true))
				{
					frameY += (short)(Math.Sign(frameYIdeal) * 2);
				}
			}
			tileAtPosition.Get<TileWallWireStateData>().HasTile = true;
			tileAtPosition.TileType = 323;
			tileAtPosition.TileFrameX = (short)(22 * WorldGen.genRand.Next(0, 3));
			tileAtPosition.TileFrameY = frameY;
		}
		bool num = WorldGen.PlayerLOS(i, j);
		WorldGen.RangeFrame(i - 2, trueStartingPositionY - treeHeight - 1, i + 2, trueStartingPositionY + 1);
		if (Main.dedServ)
		{
			NetMessage.SendTileSquare(-1, i, (int)((double)trueStartingPositionY - (double)treeHeight * 0.5), treeHeight + 1);
		}
		if (num)
		{
			WorldGen.TreeGrowFXCheck(i, j);
		}
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
	{
		if (i % 2 == 1)
		{
			effects = (SpriteEffects)1;
		}
	}
}
