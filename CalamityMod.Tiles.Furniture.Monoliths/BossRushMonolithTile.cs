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

public class BossRushMonolithTile : BaseMonolith
{
	public override int TileWidth => 4;

	public override int TileHeight => 4;

	public override int AnimationFrameCount => 5;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<BossRushMonolith>();

	public override void SetStaticDefaults()
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GlowMask = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
		}
		RegisterItemDrop(ModContent.ItemType<BossRushMonolith>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Origin = new Point16(2, 3);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 4, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(64, 8, 12));
		base.DustType = 235;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithBossRushShader = 30;
		}
	}
}
