using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.Monoliths;

public class FrigidMonolithTile : BaseMonolith
{
	public override int TileWidth => 3;

	public override int TileHeight => 4;

	public override int AnimationFrameCount => 6;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<FrigidMonolith>();

	public override void SetStaticDefaults()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(ModContent.ItemType<FrigidMonolith>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.Origin = new Point16(0, 3);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 3, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(55, 212, 230));
		base.DustType = 80;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithCryogenShader = 30;
		}
	}
}
