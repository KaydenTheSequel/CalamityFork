using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.CraftingStations;

public class DraedonsForge : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 18 };
		TileObjectData.newTile.Origin = new Point16(2, 1);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(230, 157, 41), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Furniture.CraftingStations.DraedonsForge>());
		base.AnimationFrameHeight = 54;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.DustType = 84;
		int[] obj = new int[12]
		{
			18, 15, 14, 16, 134, 0, 17, 77, 133, 114,
			412, 26
		};
		obj[5] = ModContent.TileType<CosmicAnvil>();
		base.AdjTiles = obj;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 11)
		{
			frame = (frame + 1) % 4;
			frameCounter = 0;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = (float)Main.DiscoR / 255f;
		g = (float)Main.DiscoG / 255f;
		b = (float)Main.DiscoB / 255f;
	}
}
