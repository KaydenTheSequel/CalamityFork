using CalamityMod.Items.Placeables.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class LaboratoryDoorClosed : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.NotReallySolid[base.Type] = true;
		TileID.Sets.DrawsWalls[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Origin = new Point16(0, 3);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		AddMapEntry(new Color(119, 105, 79), Language.GetText("MapObject.Door"));
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.AdjTiles = new int[1] { 10 };
		base.DustType = 8;
		TileID.Sets.OpenDoorID[base.Type] = ModContent.TileType<LaboratoryDoorOpen>();
	}

	public override bool Slope(int i, int j)
	{
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<LaboratoryDoorItem>();
	}
}
