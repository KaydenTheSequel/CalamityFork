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

public class ExoObeliskTile : BaseMonolith
{
	public static Asset<Texture2D> Numbers;

	public override int TileWidth => 3;

	public override int TileHeight => 5;

	public override int AnimationFrameCount => 13;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<ExoObelisk>();

	public override void SetStaticDefaults()
	{
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GlowMask = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
			Numbers = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/Monoliths/ExoObeliskText", (AssetRequestMode)2);
		}
		RegisterItemDrop(ModContent.ItemType<ExoObelisk>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(1, 4);
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 3, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(54, 54, 54));
		base.DustType = 107;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithExoShader = 30;
		}
	}

	public override void DrawExtra(Vector2 drawPos, Rectangle rect, Color tileColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (Numbers != null)
		{
			int colorIndex = (int)((double)Main.GlobalTimeWrappedHourly / 0.5 % (double)CalamityUtils.ExoPalette.Length);
			Color val = CalamityUtils.ExoPalette[colorIndex];
			Color nextColor = CalamityUtils.ExoPalette[(colorIndex + 1) % CalamityUtils.ExoPalette.Length];
			Color exoColors = Color.Lerp(val, nextColor, (Main.GlobalTimeWrappedHourly % 0.5f > 1f) ? 1f : (Main.GlobalTimeWrappedHourly % 1f));
			Main.spriteBatch.Draw(Numbers.Value, drawPos, (Rectangle?)rect, exoColors, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}
}
