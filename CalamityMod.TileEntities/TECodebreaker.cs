using System.IO;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Pets;
using CalamityMod.Packets;
using CalamityMod.Systems.Collections;
using CalamityMod.Tiles.DraedonSummoner;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.TileEntities;

public class TECodebreaker : ModTileEntity
{
	public int InputtedCellCount;

	public int InitialCellCountBeforeDecrypting;

	public int HeldSchematicID;

	public int DecryptionCountdown;

	public bool ContainsDecryptionComputer;

	public bool ContainsSensorArray;

	public bool ContainsAdvancedDisplay;

	public bool ContainsVoltageRegulationSystem;

	public bool ContainsCoolingCell;

	public bool ContainsBloodyVein;

	public const int MaxCellCapacity = 9999;

	public int DecryptionTotalTime
	{
		get
		{
			int decryptTime = 7200;
			if (ContainsCoolingCell)
			{
				decryptTime = 900;
			}
			return decryptTime;
		}
	}

	public int DecryptionCellCost
	{
		get
		{
			if (HeldSchematicID == 0 || !EncryptedSchematicIDRelationshipDict.TryGet(HeldSchematicID, out var schematicItemType))
			{
				return 0;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicPlanetoid>())
			{
				return 500;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicJungle>())
			{
				return 950;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicHell>())
			{
				return 1750;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicIce>())
			{
				return 5000;
			}
			return 0;
		}
	}

	public float DecryptionCompletion => 1f - (float)DecryptionCountdown / (float)DecryptionTotalTime;

	public bool ReadyToSummonDraedon
	{
		get
		{
			if (ContainsCoolingCell)
			{
				return DecryptionCountdown <= 0;
			}
			return false;
		}
	}

	public bool CanDecryptHeldSchematic
	{
		get
		{
			if (HeldSchematicID == 0 || !EncryptedSchematicIDRelationshipDict.TryGet(HeldSchematicID, out var schematicItemType))
			{
				return false;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicPlanetoid>())
			{
				return ContainsDecryptionComputer;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicJungle>())
			{
				if (ContainsDecryptionComputer)
				{
					return ContainsSensorArray;
				}
				return false;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicHell>())
			{
				if (ContainsDecryptionComputer && ContainsSensorArray)
				{
					return ContainsAdvancedDisplay;
				}
				return false;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicIce>())
			{
				if (ContainsDecryptionComputer && ContainsSensorArray && ContainsAdvancedDisplay)
				{
					return ContainsVoltageRegulationSystem;
				}
				return false;
			}
			return false;
		}
	}

	public string UnderlyingSchematicText
	{
		get
		{
			if (HeldSchematicID == 0 || !EncryptedSchematicIDRelationshipDict.TryGet(HeldSchematicID, out var schematicItemType))
			{
				return string.Empty;
			}
			if (schematicItemType == ModContent.ItemType<EncryptedSchematicPlanetoid>() || schematicItemType == ModContent.ItemType<EncryptedSchematicJungle>() || schematicItemType == ModContent.ItemType<EncryptedSchematicHell>() || schematicItemType == ModContent.ItemType<EncryptedSchematicIce>())
			{
				return CalamityUtils.GetTextValueFromModItem(schematicItemType, "Content");
			}
			return string.Empty;
		}
	}

	public Vector2 Center
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return Position.ToWorldCoordinates(40f, 64f);
		}
	}

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
		if (tile.HasTile && tile.TileType == ModContent.TileType<CodebreakerTile>() && tile.TileFrameX == 0)
		{
			return tile.TileFrameY == 0;
		}
		return false;
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, i, j, 5, 8);
			NetMessage.SendData(87, -1, -1, null, i, j, base.Type);
			return -1;
		}
		return Place(i, j);
	}

	public override void OnNetPlace()
	{
		NetMessage.SendData(86, -1, -1, null, ID, Position.X, Position.Y);
	}

	public override void Update()
	{
		UpdateTime();
	}

	public void DropConstituents(int x, int y)
	{
		EntitySource_TileEntity source = new EntitySource_TileEntity(this);
		if (ContainsDecryptionComputer)
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, ModContent.ItemType<DecryptionComputer>());
		}
		if (ContainsSensorArray)
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, ModContent.ItemType<LongRangedSensorArray>());
		}
		if (ContainsAdvancedDisplay)
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, ModContent.ItemType<AdvancedDisplay>());
		}
		if (ContainsVoltageRegulationSystem)
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, ModContent.ItemType<VoltageRegulationSystem>());
		}
		if (ContainsCoolingCell)
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, ModContent.ItemType<AuricQuantumCoolingCell>());
		}
		if (EncryptedSchematicIDRelationshipDict.TryGet(HeldSchematicID, out var schematicItemType))
		{
			Item.NewItem(source, x * 16, y * 16, 32, 32, schematicItemType);
		}
		while (InputtedCellCount > 0)
		{
			int totalCellsToDrop = InputtedCellCount;
			if (totalCellsToDrop > 999)
			{
				totalCellsToDrop = 999;
			}
			InputtedCellCount -= totalCellsToDrop;
			int itemType = (ContainsBloodyVein ? ModContent.ItemType<BloodyVein>() : ModContent.ItemType<DraedonPowerCell>());
			Item.NewItem(new EntitySource_TileEntity(this), x * 16, y * 16, 32, 32, itemType, totalCellsToDrop);
		}
	}

	public void SyncConstituents(short sender)
	{
		if (Main.netMode != 0)
		{
			TEUpdateCodebreakerConstituentsPacket.Send(this);
		}
	}

	public void SyncContainedStuff()
	{
		if (Main.netMode != 0)
		{
			TEUpdateCodebreakerContainedStuffPacket.Send(this);
		}
	}

	public void SyncDecryptCountdown()
	{
		if (Main.netMode != 0)
		{
			TEUpdateCodebreakerDecryptCountdownPacket.Send(this);
		}
	}

	public void UpdateTime()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (DecryptionCountdown <= 0)
		{
			return;
		}
		DecryptionCountdown--;
		if (DecryptionCountdown % 5 == 0)
		{
			InputtedCellCount = InitialCellCountBeforeDecrypting - (int)((float)DecryptionCellCost * DecryptionCompletion);
			if (Main.netMode != 1)
			{
				SyncContainedStuff();
			}
		}
		if (DecryptionCountdown == 0)
		{
			InitialCellCountBeforeDecrypting = 0;
			LearnFromHeldSchematic(out var anythingChanged);
			if (Main.dedServ)
			{
				SyncDecryptCountdown();
				SyncContainedStuff();
			}
			else if (anythingChanged)
			{
				CombatText.NewText(Main.LocalPlayer.Hitbox, Color.Cyan, CalamityUtils.GetTextValue("Misc.LearnedSchematic"), dramatic: true);
			}
		}
	}

	public void LearnFromHeldSchematic(out bool anythingChanged)
	{
		anythingChanged = false;
		if (EncryptedSchematicIDRelationshipDict.TryGet(HeldSchematicID, out var schematicItemType))
		{
			if (!RecipeUnlockHandler.HasUnlockedT2ArsenalRecipes && schematicItemType == ModContent.ItemType<EncryptedSchematicPlanetoid>())
			{
				RecipeUnlockHandler.HasUnlockedT2ArsenalRecipes = true;
				anythingChanged = true;
			}
			if (!RecipeUnlockHandler.HasUnlockedT3ArsenalRecipes && schematicItemType == ModContent.ItemType<EncryptedSchematicJungle>())
			{
				RecipeUnlockHandler.HasUnlockedT3ArsenalRecipes = true;
				anythingChanged = true;
			}
			if (!RecipeUnlockHandler.HasUnlockedT4ArsenalRecipes && schematicItemType == ModContent.ItemType<EncryptedSchematicHell>())
			{
				RecipeUnlockHandler.HasUnlockedT4ArsenalRecipes = true;
				anythingChanged = true;
			}
			if (!RecipeUnlockHandler.HasUnlockedT5ArsenalRecipes && schematicItemType == ModContent.ItemType<EncryptedSchematicIce>())
			{
				RecipeUnlockHandler.HasUnlockedT5ArsenalRecipes = true;
				anythingChanged = true;
			}
			if (Main.dedServ & anythingChanged)
			{
				CalamityNetcode.SyncWorld();
			}
		}
	}

	public override void SaveData(TagCompound tag)
	{
		tag["ContainsDecryptionComputer"] = ContainsDecryptionComputer;
		tag["ContainsSensorArray"] = ContainsSensorArray;
		tag["ContainsAdvancedDisplay"] = ContainsAdvancedDisplay;
		tag["ContainsVoltageRegulationSystem"] = ContainsVoltageRegulationSystem;
		tag["ContainsCoolingCell"] = ContainsCoolingCell;
		tag["InputtedCellCount"] = InputtedCellCount;
		tag["HeldSchematicID"] = HeldSchematicID;
		tag["DecryptionCountdown"] = DecryptionCountdown;
		tag["InitialCellCountBeforeDecrypting"] = InitialCellCountBeforeDecrypting;
		tag["ContainsBloodyVein"] = ContainsBloodyVein;
	}

	public override void LoadData(TagCompound tag)
	{
		ContainsDecryptionComputer = tag.GetBool("ContainsDecryptionComputer");
		ContainsSensorArray = tag.GetBool("ContainsSensorArray");
		ContainsAdvancedDisplay = tag.GetBool("ContainsAdvancedDisplay");
		ContainsVoltageRegulationSystem = tag.GetBool("ContainsVoltageRegulationSystem");
		ContainsCoolingCell = tag.GetBool("ContainsCoolingCell");
		InputtedCellCount = tag.GetInt("InputtedCellCount");
		HeldSchematicID = tag.GetInt("HeldSchematicID");
		DecryptionCountdown = tag.GetInt("DecryptionCountdown");
		InitialCellCountBeforeDecrypting = tag.GetInt("InitialCellCountBeforeDecrypting");
		ContainsBloodyVein = tag.GetBool("ContainsBloodyVein");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(ContainsDecryptionComputer);
		writer.Write(ContainsSensorArray);
		writer.Write(ContainsAdvancedDisplay);
		writer.Write(ContainsVoltageRegulationSystem);
		writer.Write(ContainsCoolingCell);
		writer.Write(InputtedCellCount);
		writer.Write(HeldSchematicID);
		writer.Write(DecryptionCountdown);
		writer.Write(InitialCellCountBeforeDecrypting);
		writer.Write(ContainsBloodyVein);
	}

	public override void NetReceive(BinaryReader reader)
	{
		ContainsDecryptionComputer = reader.ReadBoolean();
		ContainsSensorArray = reader.ReadBoolean();
		ContainsAdvancedDisplay = reader.ReadBoolean();
		ContainsVoltageRegulationSystem = reader.ReadBoolean();
		ContainsCoolingCell = reader.ReadBoolean();
		InputtedCellCount = reader.ReadInt32();
		HeldSchematicID = reader.ReadInt32();
		DecryptionCountdown = reader.ReadInt32();
		InitialCellCountBeforeDecrypting = reader.ReadInt32();
		ContainsBloodyVein = reader.ReadBoolean();
	}
}
