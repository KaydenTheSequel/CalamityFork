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

public class DraconicIncenseTile : BaseMonolith
{
	public override int TileWidth => 2;

	public override int TileHeight => 6;

	public override int AnimationFrameCount => 6;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<DraconicIncense>();

	public override void SetStaticDefaults()
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GlowMask = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
		}
		RegisterItemDrop(ModContent.ItemType<DraconicIncense>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 6;
		TileObjectData.newTile.Origin = new Point16(0, 5);
		TileObjectData.newTile.CoordinateHeights = new int[6] { 16, 16, 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 2, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(143, 106, 41));
		base.DustType = 127;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithYharonShader = 30;
		}
	}
}
