using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class AgedLaboratoryTerminal : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(base.Type);
		AddMapEntry(Color.DarkSlateGray, CalamityUtils.GetText("Tiles.Terminal"));
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 182);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
