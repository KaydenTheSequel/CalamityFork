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

public class BlueDistortedMonolithTile : BaseMonolith
{
	public override int TileWidth => 5;

	public override int TileHeight => 5;

	public override int AnimationFrameCount => 6;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<BlueDistortedMonolith>();

	public override void SetStaticDefaults()
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(ModContent.ItemType<BlueDistortedMonolith>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(2, 4);
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 5, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(50, 127, 209));
		base.DustType = 180;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithDevourerBShader = 30;
		}
	}
}
