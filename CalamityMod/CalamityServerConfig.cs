using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace CalamityMod;

[BackgroundColor(49, 32, 36, 216)]
public class CalamityServerConfig : ModConfig
{
	public static CalamityServerConfig Instance;

	private const int MinTownNPCSpawnMultiplier = 1;

	private const int MaxTownNPCSpawnMultiplier = 10;

	private const int MinPlayerRespawnTime_BossAlive = 15;

	private const int MaxPlayerRespawnTime_BossAlive = 60;

	private const float MinBossHealthBoost = 0f;

	private const float MaxBossHealthBoost = 900f;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("Gameplay")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool RemoveReforgeRNG { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool EarlyHardmodeProgressionRework { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool BossZen { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool TownNPCsSpawnAtNight { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[Range(1, 10)]
	[Increment(1)]
	[DrawTicks]
	[DefaultValue(1)]
	public int TownNPCSpawnRateMultiplier { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[Range(15, 60)]
	[Increment(1)]
	[DrawTicks]
	[DefaultValue(15)]
	public int PlayerRespawnTime_BossAlive { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 900f)]
	[Increment(25f)]
	[DrawTicks]
	[DefaultValue(0f)]
	public float BossHealthBoost { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool BossesStopWeather { get; set; }

	[Header("BaseBoosts")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool DefaultDashEnabled { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FasterBaseSpeed { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FasterRopeClimbSpeed { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FasterJumpSpeed { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FasterTilePlacement { get; set; }

	[Header("ExpertMaster")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool NerfExpertDebuffs { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool ChilledWaterRework { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool RemoveLavaDropsFromLavaSlimes { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool ForceTownSafety { get; set; }

	public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
	{
		if (whoAmI == 0)
		{
			return true;
		}
		if (whoAmI != 0)
		{
			message = CalamityUtils.GetText("Configs.CalamityServerConfig.Denied").ToNetworkText();
			return false;
		}
		return false;
	}

	[OnDeserialized]
	internal void ClampValues(StreamingContext context)
	{
		BossHealthBoost = Utils.Clamp(BossHealthBoost, 0f, 900f);
	}
}
