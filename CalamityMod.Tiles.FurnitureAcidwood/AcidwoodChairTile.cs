using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureAcidwood;

public class AcidwoodChairTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 2, 0);
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(base.Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Chair"));
		TileID.Sets.CanBeSatOnForNPCs[base.Type] = true;
		TileID.Sets.CanBeSatOnForPlayers[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		base.AdjTiles = new int[1] { 15 };
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 7, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
	{
		FurnitureCommon.ChairSitInfo(i, j, ref info, 40, fat: true, hasOffset: true);
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.ChairRightClick(i, j);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChairMouseOver(i, j, ModContent.ItemType<AcidwoodChair>(), fat: true);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return settings.player.IsWithinSnappngRangeToTile(i, j, 40);
	}
}
