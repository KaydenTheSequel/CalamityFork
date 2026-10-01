using CalamityMod.Dusts;
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

namespace CalamityMod.Tiles.Astral;

public class AstralTreeSapling : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
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
		TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<AstralGrass>() };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.DrawFlipHorizontal = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Sapling"));
		base.DustType = ModContent.DustType<AstralBasic>();
		base.AdjTiles = new int[1] { 20 };
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void RandomUpdate(int i, int j)
	{
		if (WorldGen.genRand.NextBool(20))
		{
			bool isPlayerNear = WorldGen.PlayerLOS(i, j);
			if (WorldGen.GrowTree(i, j) & isPlayerNear)
			{
				WorldGen.TreeGrowFXCheck(i, j);
			}
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
