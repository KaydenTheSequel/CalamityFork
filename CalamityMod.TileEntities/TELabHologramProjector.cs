using System.IO;
using System.Reflection;
using CalamityMod.CalPlayer;
using CalamityMod.Packets;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.TileEntities;

public class TELabHologramProjector : ModTileEntity
{
	public const float PopupDistance = 560f;

	public bool PoppingUp;

	public Vector2 Center
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return Position.ToWorldCoordinates(48f, 56f);
		}
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		int style = 0;
		int alt = 0;
		TileObjectData.GetTileInfo(tile, ref style, ref alt);
		TileObjectData data = TileObjectData.GetTileData(tile.TileType, style, alt);
		int sheetSquare = 16 + data.CoordinatePadding;
		int FrameX = tile.TileFrameX / sheetSquare % data.Width;
		int FrameY = tile.TileFrameY / sheetSquare % data.Height;
		if (tile.HasTile && tile.TileType == ModContent.TileType<LabHologramProjector>() && FrameX == 0)
		{
			return FrameY == 0;
		}
		return false;
	}

	public override void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		bool wasPoppingUp = PoppingUp;
		PoppingUp = false;
		Vector2 projectorCenterPos = Center;
		float distSQ = 313600f;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.DistanceSQ(projectorCenterPos) < distSQ)
			{
				PoppingUp = true;
				break;
			}
		}
		if (PoppingUp != wasPoppingUp)
		{
			SendSyncPacket();
		}
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, 6, 7);
			NetMessage.SendData(87, -1, -1, null, i, j, base.Type);
			return -1;
		}
		return Place(i, j);
	}

	public override void OnNetPlace()
	{
		NetMessage.SendData(86, -1, -1, null, ID, Position.X, Position.Y);
	}

	public override void OnKill()
	{
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			if (((ModPlayer[])typeof(Player).GetField("modPlayers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(p)).Length != 0)
			{
				CalamityPlayer mp = p.Calamity();
				if (mp.CurrentlyViewedHologramID == ID)
				{
					mp.CurrentlyViewedHologramID = -1;
					mp.CurrentlyViewedHologramText = string.Empty;
				}
			}
		}
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(PoppingUp);
	}

	public override void NetReceive(BinaryReader reader)
	{
		PoppingUp = reader.ReadBoolean();
	}

	private void SendSyncPacket()
	{
		if (Main.netMode != 0)
		{
			TELabHologramProjectorPacket.Send(this, PoppingUp);
		}
	}
}
