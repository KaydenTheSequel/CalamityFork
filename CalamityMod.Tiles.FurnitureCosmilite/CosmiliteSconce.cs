using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureCosmilite;

public class CosmiliteSconce : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.AnchorWall = true;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(253, 221, 3), CalamityUtils.GetText("Tiles.Sconce"));
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileID.Sets.FramesOnKillWall[base.Type] = true;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 132, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 134, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 1f;
			g = 0.6f;
			b = 1f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 3);
	}
}
