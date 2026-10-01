using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class SulphurousColumn : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileSolidTop[base.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(1, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(150, 100, 50), CalamityUtils.GetText("Tiles.Column"));
		base.DustType = 75;
		base.SetStaticDefaults();
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		Tile t = CalamityUtils.ParanoidTileRetrieval(i, j);
		Tile left = CalamityUtils.ParanoidTileRetrieval(i - 1, j);
		Tile right = CalamityUtils.ParanoidTileRetrieval(i + 1, j);
		if (t.TileFrameX % 36 == 0 && !right.HasTile)
		{
			WorldGen.KillTile(i, j);
		}
		if (t.TileFrameX % 36 == 18 && !left.HasTile)
		{
			WorldGen.KillTile(i, j);
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int k = 0; k < WorldGen.genRand.Next(3, 5); k++)
			{
				int goreID = base.Mod.Find<ModGore>($"SulphurousRockGore{WorldGen.genRand.Next(3) + 1}").Type;
				Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2Unit() * WorldGen.genRand.NextFloat(1.4f, 3.2f), goreID);
			}
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}
}
