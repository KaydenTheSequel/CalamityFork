using System.ComponentModel;
using System.Runtime.Serialization;
using CalamityMod.Enums;
using CalamityMod.Systems;
using Terraria;
using Terraria.ModLoader.Config;

namespace CalamityMod;

[BackgroundColor(49, 32, 36, 216)]
public class CalamityClientConfig : ModConfig
{
	public static CalamityClientConfig Instance;

	private const int MinParticleLimit = 500;

	private const int MaxParticleLimit = 10000;

	private const float MinMeterShake = 0f;

	private const float MaxMeterShake = 4f;

	public override ConfigScope Mode => ConfigScope.ClientSide;

	[Header("Graphics")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool DisableGravityScreenSwap { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool TextEffects { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool Afterimages { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool Photosensitivity { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool EnableVanillaTextureEdits { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool SunkenSeaBackgroundDistortion { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FancyBackgroundVisuals { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(500, 10000)]
	[DefaultValue(5000)]
	public int ParticleLimit { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 10f)]
	[DefaultValue(1f)]
	public float ScreenshakePower { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool StealthInvisibility { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[DefaultValue(1f)]
	[Range(0f, 1f)]
	public float EnergyShieldOpacity { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(TileBlendingQuality.Normal)]
	public TileBlendingQuality TileTextureBlendingQuality { get; set; }

	[Header("UI")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool WikiStatusMessage { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool VCMMStatusMessage { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool ShopNewAlert { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool BossHealthBarExtraInfo { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool DebuffDisplay { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(CooldownDisplayOptions.Full)]
	public CooldownDisplayOptions CooldownDisplay { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool VanillaCooldownDisplay { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool MeterPosLock { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool StealthMeter { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool ChargeMeter { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 4f)]
	[Increment(1f)]
	[DrawTicks]
	[DefaultValue(2f)]
	public float RipperMeterShake { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool SpeedrunTimer { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FlightBar { get; set; }

	[Header("MeterPositions")]
	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(50.104603f)]
	public float StealthMeterPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(55.765408f)]
	public float StealthMeterPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(50.104603f)]
	public float SulphuricWaterMeterPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(58.05169f)]
	public float SulphuricWaterMeterPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(50.104603f)]
	public float ChargeMeterPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(58.05169f)]
	public float ChargeMeterPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(35.77406f)]
	public float RageMeterPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(4.5761433f)]
	public float RageMeterPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(35.77406f)]
	public float AdrenalineMeterPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(8.846918f)]
	public float AdrenalineMeterPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(46f)]
	public float SpeedrunTimerPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(1.481f)]
	public float SpeedrunTimerPosY { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(40.9375f)]
	public float FlightBarPosX { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[SliderColor(224, 165, 56, 128)]
	[Range(0f, 100f)]
	[DefaultValue(7.2222223f)]
	public float FlightBarPosY { get; set; }

	[Header("MusicToggles")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool Interludes { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool DevourerofGodsEulogy { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool AbyssLayer3Alt { get; set; }

	[Header("Gameplay")]
	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(true)]
	public bool FasterFallHotkey { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(SetBonusDoubleTapOptions.Auto)]
	public SetBonusDoubleTapOptions SetBonusDoubleTap { get; set; }

	[BackgroundColor(192, 54, 64, 192)]
	[DefaultValue(false)]
	public bool StutterFix { get; set; }

	[OnDeserialized]
	internal void ClampValues(StreamingContext context)
	{
		RipperMeterShake = Utils.Clamp(RipperMeterShake, 0f, 4f);
		ParticleLimit = Utils.Clamp(ParticleLimit, 500, 10000);
	}
}
