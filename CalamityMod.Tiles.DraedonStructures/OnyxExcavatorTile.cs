using CalamityMod.Items.Mounts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class OnyxExcavatorTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.Width = 8;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(128, 0, 128), CreateMapEntryName());
		RegisterItemDrop(ModContent.ItemType<OnyxExcavatorKey>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<OnyxExcavatorKey>(), base.Type, default(int));
	}
}
