using System.IO;
using System.Reflection;
using CalamityMod.CalPlayer;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Packets;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.TileEntities;

public class TEPowerCellFactory : ModTileEntity
{
	public long Time;

	internal short Stack_Internal;

	public Vector2 Center
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return Position.ToWorldCoordinates(32f, 32f);
		}
	}

	public short CellStack
	{
		get
		{
			return Stack_Internal;
		}
		set
		{
			Stack_Internal = value;
			SendSyncPacket();
		}
	}

	private long CycleFrameCounter
	{
		get
		{
			long totalCycleTime = 900L;
			return Time % totalCycleTime;
		}
	}

	private bool IsCellFrame
	{
		get
		{
			long magicFrame = 889L;
			return CycleFrameCounter == magicFrame;
		}
	}

	public int AnimationFrame
	{
		get
		{
			int f = (int)CycleFrameCounter;
			if (f < 675)
			{
				return 44;
			}
			return (f - 675) / 5;
		}
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile.HasTile && tile.TileType == ModContent.TileType<PowerCellFactory>() && tile.TileFrameX == 0)
		{
			return tile.TileFrameY == 0;
		}
		return false;
	}

	public override void Update()
	{
		for (int t = 0; (double)t < Main.desiredWorldTilesUpdateRate; t++)
		{
			Time++;
			int maxCellStack = ModContent.GetModItem(ModContent.ItemType<DraedonPowerCell>()).Item.maxStack;
			if (IsCellFrame && CellStack < maxCellStack)
			{
				CellStack++;
			}
		}
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, 4, 4);
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
				if (mp.CurrentlyViewedFactoryID == ID)
				{
					mp.CurrentlyViewedFactoryID = -1;
				}
			}
		}
	}

	public override void SaveData(TagCompound tag)
	{
		tag["time"] = Time;
		tag["cells"] = Stack_Internal;
	}

	public override void LoadData(TagCompound tag)
	{
		Time = tag.GetLong("time");
		Stack_Internal = tag.GetShort("cells");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(Time);
		writer.Write(Stack_Internal);
	}

	public override void NetReceive(BinaryReader reader)
	{
		Time = reader.ReadInt64();
		Stack_Internal = reader.ReadInt16();
	}

	private void SendSyncPacket()
	{
		if (Main.netMode != 0)
		{
			TEPowerCellFactoryPacket.Send(this, Time, Stack_Internal);
		}
	}
}
