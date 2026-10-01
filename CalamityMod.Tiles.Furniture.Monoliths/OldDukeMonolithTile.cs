using CalamityMod.ForegroundDrawing.LoopingTextures;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.Monoliths;

public class OldDukeMonolithTile : BaseMonolith
{
	public static Asset<Texture2D> Numbers;

	public override int TileWidth => 4;

	public override int TileHeight => 8;

	public override int AnimationFrameCount => 24;

	public override int AnimationDelay => 6;

	public override int CursorItemType => ModContent.ItemType<EldenDiorama>();

	public override void SetStaticDefaults()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GlowMask = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
		}
		RegisterItemDrop(CursorItemType);
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 8;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.Origin = new Point16(1, 4);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 3, 0);
		base.AnimationFrameHeight = 144;
		TileObjectData.newTile.CoordinateHeights = new int[8] { 16, 16, 16, 16, 16, 16, 16, 16 };
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(99, 133, 48));
		base.DustType = 256;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.GetModPlayer<NuclearTorrentPlayer>().ShouldDisplayTorrentMonolith = true;
		}
	}
}
