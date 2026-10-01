using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.CraftingStations;

public class CosmicAnvil : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileSolidTop[base.Type] = true;
		Main.tileTable[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.Direction = TileObjectDirection.None;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(159, 125, 201), CalamityUtils.GetItemName<CosmicAnvilItem>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.DustType = 179;
		base.AdjTiles = new int[2] { 16, 134 };
	}
}
