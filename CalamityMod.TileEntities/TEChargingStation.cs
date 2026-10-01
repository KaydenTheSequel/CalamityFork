using System.IO;
using System.Reflection;
using CalamityMod.CalPlayer;
using CalamityMod.Items;
using CalamityMod.Packets;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.TileEntities;

public class TEChargingStation : ModTileEntity
{
	internal short Internal_ChargingTimer;

	internal short Internal_Stack;

	public Item PluggedItem = new Item();

	private bool syncItemCharge;

	public bool ClientChargingDust;

	public Vector2 Center
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return Position.ToWorldCoordinates(24f, 16f);
		}
	}

	public short CellStack
	{
		get
		{
			return Internal_Stack;
		}
		set
		{
			Internal_Stack = value;
			SendSyncPacket();
		}
	}

	private bool PluggedItemCanCharge
	{
		get
		{
			if (PluggedItem == null || PluggedItem.IsAir)
			{
				return false;
			}
			CalamityGlobalItem modItem = PluggedItem.Calamity();
			if (modItem.UsesCharge)
			{
				return modItem.Charge < modItem.MaxCharge;
			}
			return false;
		}
	}

	public bool CanDoWork
	{
		get
		{
			if (Internal_Stack > 0)
			{
				return PluggedItemCanCharge;
			}
			return false;
		}
	}

	public Color LightColor
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			if (!CanDoWork)
			{
				return Color.Red;
			}
			return Color.MediumSpringGreen;
		}
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile.HasTile && tile.TileType == ModContent.TileType<ChargingStation>() && tile.TileFrameX == 0)
		{
			return tile.TileFrameY == 0;
		}
		return false;
	}

	public override void Update()
	{
		if (!CanDoWork)
		{
			Internal_ChargingTimer = 0;
			return;
		}
		Internal_ChargingTimer++;
		if (Internal_ChargingTimer >= 8)
		{
			CalamityGlobalItem modItem = PluggedItem.Calamity();
			modItem.Charge++;
			if (modItem.Charge >= modItem.MaxCharge)
			{
				modItem.Charge = modItem.MaxCharge;
			}
			SpawnChargingDust();
			syncItemCharge = true;
			CellStack--;
			Internal_ChargingTimer = 0;
		}
	}

	public void SpawnChargingDust()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		bool chargeComplete = false;
		if (!PluggedItem.IsAir)
		{
			CalamityGlobalItem modItem = PluggedItem.Calamity();
			chargeComplete = modItem.Charge == modItem.MaxCharge;
		}
		int dustID = 182;
		int numDust = 18;
		if (chargeComplete)
		{
			numDust *= 3;
		}
		Vector2 dustPos = Position.ToWorldCoordinates(20f, 1f);
		for (int i = 0; i < numDust; i += 2)
		{
			float pairSpeed = Main.rand.NextFloat(0.5f, 7f);
			float pairScale = (chargeComplete ? 2.4f : 1f);
			Dust dust = Dust.NewDustDirect(dustPos, 0, 0, dustID);
			dust.velocity = Vector2.UnitX * pairSpeed;
			dust.scale = pairScale;
			dust.noGravity = true;
			Dust dust2 = Dust.NewDustDirect(dustPos, 0, 0, dustID);
			dust2.velocity = Vector2.UnitX * (0f - pairSpeed);
			dust2.scale = pairScale;
			dust2.noGravity = true;
		}
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, 3, 2);
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
			if (!p.dead && ((ModPlayer[])typeof(Player).GetField("modPlayers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(p)).Length != 0)
			{
				CalamityPlayer mp = p.Calamity();
				if (mp.CurrentlyViewedChargerID == ID)
				{
					mp.CurrentlyViewedChargerID = -1;
				}
			}
		}
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("time", Internal_ChargingTimer);
		tag.Add("cells", Internal_Stack);
		Item forSaving;
		if (PluggedItem == null)
		{
			forSaving = new Item();
			forSaving.TurnToAir();
		}
		else
		{
			forSaving = PluggedItem;
		}
		tag.Add("item", forSaving);
	}

	public override void LoadData(TagCompound tag)
	{
		Internal_ChargingTimer = tag.GetShort("time");
		Internal_Stack = tag.GetShort("cells");
		PluggedItem = ItemIO.Load(tag.GetCompound("item"));
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(Internal_ChargingTimer);
		writer.Write(Internal_Stack);
		ItemIO.Send(PluggedItem, writer, writeStack: true, writeFavorite: true);
	}

	public override void NetReceive(BinaryReader reader)
	{
		Internal_ChargingTimer = reader.ReadInt16();
		Internal_Stack = reader.ReadInt16();
		PluggedItem = ItemIO.Receive(reader, readStack: true, readFavorite: true);
	}

	private void SendSyncPacket()
	{
		if (Main.netMode != 0)
		{
			CalamityGlobalItem modItem = (PluggedItem.IsAir ? null : PluggedItem.Calamity());
			float chargeOrNaN = ((syncItemCharge && modItem != null) ? modItem.Charge : float.NaN);
			TEChargingStationStandardPacket.Send(this, Internal_ChargingTimer, Internal_Stack, chargeOrNaN);
			syncItemCharge = false;
		}
	}

	internal void SendItemSyncPacket()
	{
		if (Main.netMode != 0)
		{
			TEChargingStationItemChangePacket.Send(this, PluggedItem);
		}
	}
}
