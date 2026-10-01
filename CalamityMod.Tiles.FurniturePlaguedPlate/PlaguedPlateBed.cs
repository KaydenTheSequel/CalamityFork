using CalamityMod.Items.Placeables.FurniturePlagued;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurniturePlaguedPlate;

public class PlaguedPlateBed : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileID.Sets.InteractibleByNPCs[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileID.Sets.IsValidSpawnPoint[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.addTile(base.Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		AddMapEntry(new Color(191, 142, 111), CreateMapEntryName());
		base.AdjTiles = new int[1] { 79 };
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 178, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		int spawnX = i - tile.TileFrameX / 18;
		int spawnY = j + 2;
		spawnX += ((tile.TileFrameX >= 54) ? 5 : 2);
		spawnY -= tile.TileFrameY / 18;
		player.FindSpawn();
		if (player.SpawnX == spawnX && player.SpawnY == spawnY)
		{
			player.RemoveSpawn();
			Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
		}
		else if (Player.CheckSpawn(spawnX, spawnY))
		{
			player.ChangeSpawn(spawnX, spawnY);
			Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
		}
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateBed>());
	}
}
