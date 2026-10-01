using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class ZenithThrone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ZenithThrone>());
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.CanBeSatOnForPlayers[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Height = 11;
		TileObjectData.newTile.Width = 8;
		TileObjectData.newTile.Origin = new Point16(4, 9);
		TileObjectData.newTile.CoordinateHeights = new int[11]
		{
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16
		};
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
		TileObjectData.addTile(base.Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		AddMapEntry(new Color(43, 199, 217), Language.GetText("ItemName.Throne"));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 135);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		Player player = Main.LocalPlayer;
		int tileNum = tile.TileFrameX / 18;
		info.DirectionOffset = 0;
		float offset = 0f;
		if (tileNum <= 1)
		{
			offset = 18 * (2 - tileNum);
		}
		else if (tileNum >= 6)
		{
			offset = 18 * -(tileNum - 5);
		}
		if (player.direction == -1)
		{
			offset *= -1f;
		}
		info.VisualOffset = new Vector2(offset, -8f);
		info.TargetDirection = player.direction;
		info.AnchorTilePosition.X = i;
		info.AnchorTilePosition.Y = j;
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.ChairRightClick(i, j);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.BenchMouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ZenithThrone>());
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return settings.player.IsWithinSnappngRangeToTile(i, j, 40);
	}
}
