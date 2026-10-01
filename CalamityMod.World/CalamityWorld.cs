using CalamityMod.Items.Tools.ClimateChange;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityMod.World;

public static class CalamityWorld
{
	public static int MoneyStolenByBandit;

	public static int Reforges;

	public static bool IsWorldAfterDraedonUpdate;

	public static bool revenge;

	public static bool death;

	public static bool armageddon;

	public static Rectangle SunkenSeaLocation;

	public static int[] SChestX;

	public static int[] SChestY;

	public static bool HasGeneratedLuminitePlanetoids;

	public static bool spawnedBandit;

	public static bool foundHomePermafrost;

	public static bool catName;

	public static bool dogName;

	public static bool bunnyName;

	public static int DraedonSummonCountdown;

	public static ExoMech DraedonMechToSummon;

	public static Vector2 DraedonSummonPosition;

	public static bool TalkedToDraedon;

	public static bool DraedonMechdusa;

	public const int DraedonSummonCountdownMax = 260;

	public static Vector2 SunkenSeaLabCenter;

	public static Vector2 PlanetoidLabCenter;

	public static Vector2 JungleLabCenter;

	public static Vector2 HellLabCenter;

	public static Vector2 IceLabCenter;

	public static Vector2 CavernLabCenter;

	public static bool LegendaryMode
	{
		get
		{
			if (Main.getGoodWorld)
			{
				return ReflectMasterMode();
			}
			return false;
		}
	}

	public static bool MaliceMode
	{
		get
		{
			if (Main.getGoodWorld && ReflectMasterMode())
			{
				return revenge;
			}
			return false;
		}
	}

	public static bool AbleToSummonDraedon
	{
		get
		{
			if (DraedonSummonCountdown > 0)
			{
				return false;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<Draedon>()))
			{
				return false;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<ThanatosHead>()))
			{
				return false;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<AresBody>()))
			{
				return false;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<Artemis>()) || NPC.AnyNPCs(ModContent.NPCType<Apollo>()))
			{
				return false;
			}
			return true;
		}
	}

	public static bool ReflectMasterMode()
	{
		if (Main.GameModeInfo.IsJourneyMode)
		{
			return ((CreativePowers.ASharedSliderPower)CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>())._sliderCurrentValueCache == 1f;
		}
		return Main._currentGameModeInfo.IsMasterMode;
	}

	public static void StartRain(bool adjustSeverity = false, bool maxSeverity = false, bool worldSync = true)
	{
		int framesInDay = 86400;
		int framesInHour = framesInDay / 24;
		Main.rainTime = Main.rand.Next(framesInHour * 8, framesInDay);
		if (Main.rand.NextBool(3))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour);
		}
		if (Main.rand.NextBool(4))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour * 2);
		}
		if (Main.rand.NextBool(5))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour * 2);
		}
		if (Main.rand.NextBool(6))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour * 3);
		}
		if (Main.rand.NextBool(7))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour * 4);
		}
		if (Main.rand.NextBool(8))
		{
			Main.rainTime += Main.rand.Next(0, framesInHour * 5);
		}
		float randRainExtender = 1f;
		if (Main.rand.NextBool())
		{
			randRainExtender += 0.05f;
		}
		if (Main.rand.NextBool(3))
		{
			randRainExtender += 0.1f;
		}
		if (Main.rand.NextBool(4))
		{
			randRainExtender += 0.15f;
		}
		if (Main.rand.NextBool(5))
		{
			randRainExtender += 0.2f;
		}
		Main.rainTime = (int)(Main.rainTime * (double)randRainExtender);
		Main.raining = true;
		if (adjustSeverity)
		{
			TorrentialTear.AdjustRainSeverity(maxSeverity);
		}
		if (worldSync)
		{
			CalamityNetcode.SyncWorld();
		}
	}

	public static void StopRain(bool clearWeather = false, bool worldSync = true)
	{
		if (clearWeather)
		{
			Main.StopRain();
		}
		else
		{
			Main.raining = false;
		}
		if (worldSync)
		{
			CalamityNetcode.SyncWorld();
		}
	}

	public static void StartSandstorm()
	{
		if (Main.netMode != 1 && !Sandstorm.Happening)
		{
			float windSpeed = 0f;
			if (Main.windSpeedCurrent == 0f)
			{
				windSpeed = Main.rand.NextFloat(0.3f, 0.4f) * (float)(Main.rand.Next(0, 2) * 2 - 1);
			}
			else if (Main.windSpeedCurrent < 0.3f && Main.windSpeedCurrent > 0f)
			{
				windSpeed = Main.rand.NextFloat(0.3f, 0.4f);
			}
			else if (Main.windSpeedCurrent > -0.3f && Main.windSpeedCurrent < 0f)
			{
				windSpeed = Main.rand.NextFloat(-0.4f, -0.3f);
			}
			if (windSpeed != 0f)
			{
				Main.windSpeedCurrent = ((windSpeed < 0f) ? (-0.3f) : 0.3f);
				Main.windSpeedTarget = windSpeed;
			}
			Sandstorm.StartSandstorm();
		}
	}

	public static void StopSandstorm()
	{
		if (Main.netMode != 1 && Sandstorm.Happening)
		{
			Sandstorm.StopSandstorm();
		}
	}

	public static void ResetTime(bool changeToDay)
	{
		Main.time = 0.0;
		Main.dayTime = changeToDay;
		CalamityNetcode.SyncWorld();
	}

	static CalamityWorld()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		MoneyStolenByBandit = 0;
		IsWorldAfterDraedonUpdate = false;
		revenge = false;
		death = false;
		armageddon = false;
		SunkenSeaLocation = Rectangle.Empty;
		SChestX = new int[10];
		SChestY = new int[10];
		HasGeneratedLuminitePlanetoids = false;
		spawnedBandit = false;
		foundHomePermafrost = false;
		catName = false;
		dogName = false;
		bunnyName = false;
		DraedonSummonCountdown = 0;
		DraedonSummonPosition = Vector2.Zero;
		TalkedToDraedon = false;
		DraedonMechdusa = false;
	}
}
