using CalamityMod.Skies;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Effects;

[Autoload(true, Side = ModSide.Client)]
public sealed class CalamityShaders : ModSystem
{
	private const string ShaderPath = "Effects/";

	internal const string CalamityShaderPrefix = "CalamityMod:";

	internal static Asset<Effect> AstralFogShader;

	internal static Asset<Effect> DanceOfLightBlindingShader;

	internal static Asset<Effect> CalamityAccessoryMouseShader;

	internal static Asset<Effect> SubsumingVortexTentacleShader;

	internal static Asset<Effect> DraedonTeleportShader;

	internal static Asset<Effect> LightDistortionShader;

	internal static Asset<Effect> PhaseslayerRipShader;

	internal static Asset<Effect> FadedUVMapStreakShader;

	internal static Asset<Effect> FlameStreakShader;

	internal static Asset<Effect> FadingSolidTrailShader;

	internal static Asset<Effect> ScarletDevilShader;

	internal static Asset<Effect> BordernadoFireShader;

	internal static Asset<Effect> PrismCrystalShader;

	internal static Asset<Effect> ImpFlameTrailShader;

	internal static Asset<Effect> SCalShieldShader;

	internal static Asset<Effect> RancorMagicCircleShader;

	internal static Asset<Effect> BasicTintShader;

	internal static Asset<Effect> CircularBarShader;

	internal static Asset<Effect> CircularBarSpriteShader;

	internal static Asset<Effect> DoGDisintegrationShader;

	internal static Asset<Effect> ArtAttackTrailShader;

	internal static Asset<Effect> CircularAoETelegraph;

	internal static Asset<Effect> IntersectionClipShader;

	internal static Asset<Effect> LocalLinearTransformationShader;

	internal static Asset<Effect> BasicPrimitiveShader;

	internal static Asset<Effect> ArtemisLaserShader;

	internal static Asset<Effect> ExobladeSlashShader;

	internal static Asset<Effect> ExobladePierceShader;

	internal static Asset<Effect> ExoVortexShader;

	internal static Asset<Effect> SideStreakTrailShader;

	internal static Asset<Effect> HeavenlyGaleTrailShader;

	internal static Asset<Effect> HeavenlyGaleLightningShader;

	internal static Asset<Effect> BlueStaticShader;

	internal static Asset<Effect> PrimTextureOverlayShader;

	internal static Asset<Effect> StandardPrimitiveShader;

	internal static Asset<Effect> DoGPortalShader;

	internal static Asset<Effect> FogShader;

	internal static Asset<Effect> WaterfallShader;

	internal static Asset<Effect> MetaballEdgeShader;

	internal static Asset<Effect> AdditiveMetaballEdgeShader;

	internal static Asset<Effect> FluidShaders;

	internal static Asset<Effect> SylvestaffProjectileShader;

	internal static Asset<Effect> SylvestaffRibbonShader;

	internal static Asset<Effect> SpreadTelegraph;

	internal static Asset<Effect> PixelatedSightLine;

	internal static Asset<Effect> WulfrumTilePing;

	internal static Asset<Effect> WulfrumScaffoldSelection;

	internal static Asset<Effect> RoverDriveShield;

	internal static Asset<Effect> RotateSprite;

	internal static Asset<Effect> SwingSprite;

	internal static Asset<Effect> MiracleBlight;

	internal static Asset<Effect> CircularGradientWithEdge;

	internal static Asset<Effect> GaleforceArrowTrailShader;

	internal static Asset<Effect> WavyOpacity;

	internal static Asset<Effect> HellBall;

	internal static Asset<Effect> PrimitiveClearShader;

	internal static Asset<Effect> HolyInfernoShader;

	internal static Asset<Effect> TeslaTrailShader;

	internal static Asset<Effect> DozeLightingShader;

	internal static Asset<Effect> FlipScreenShader;

	internal static Asset<Effect> OtherworldBarrierDistortionShader;

	internal static Asset<Effect> BasicTextureDistortionShader;

	internal static Asset<Effect> UnderwaterRaysShader;

	internal static Asset<Effect> DoGDistortionWindsShader;

	internal static Asset<Effect> DoGBackgroundFogShader;

	internal static Asset<Effect> CircularOpacityShader;

	internal static Asset<Effect> DoGRealityCrackShader;

	internal static Asset<Effect> DoGRiftAuraShader;

	internal static Asset<Effect> RadialBlur;

	internal static Asset<Effect> SeaPrismColorBlendingShader;

	internal static Asset<Effect> Dissolve;

	internal static Asset<Effect> BrainOfCthulhuForcefield;

	internal static Asset<Effect> NanoblackSlashShader;

	internal static Asset<Effect> GaussianBloomShader;

	internal static Asset<Effect> SunkenSeaMenuLogoWater;

	public override void PostSetupContent()
	{
		AssetRepository calAss = CalamityMod.Instance.Assets;
		AstralFogShader = LoadShader("AstralFogShader");
		RegisterSceneFilter(new AstralScreenShaderData(AstralFogShader, "AstralPass").UseColor(0.18f, 0.08f, 0.24f), "Astral", EffectPriority.VeryHigh);
		DanceOfLightBlindingShader = LoadShader("LightBurstShader");
		RegisterScreenShader(DanceOfLightBlindingShader, "BurstPass", "LightBurst");
		SubsumingVortexTentacleShader = LoadShader("TentacleShader");
		RegisterMiscShader(SubsumingVortexTentacleShader, "BurstPass", "SubsumingTentacle");
		DraedonTeleportShader = LoadShader("TeleportDisplacementShader");
		RegisterMiscShader(DraedonTeleportShader, "GlitchPass", "TeleportDisplacement");
		CalamityAccessoryMouseShader = LoadShader("SCalMouseShader");
		RegisterMiscShader(CalamityAccessoryMouseShader, "DyePass", "FireMouse");
		LightDistortionShader = LoadShader("DistortionShader");
		PhaseslayerRipShader = LoadShader("PhaseslayerRipShader");
		RegisterMiscShader(PhaseslayerRipShader, "TrailPass", "PhaseslayerRipEffect");
		FadedUVMapStreakShader = LoadShader("FadedUVMapStreak");
		RegisterMiscShader(FadedUVMapStreakShader, "TrailPass", "TrailStreak");
		FlameStreakShader = LoadShader("Flame");
		RegisterMiscShader(FlameStreakShader, "TrailPass", "Flame");
		FadingSolidTrailShader = LoadShader("FadingSolidTrail");
		RegisterMiscShader(FadingSolidTrailShader, "TrailPass", "FadingSolidTrail");
		ScarletDevilShader = LoadShader("ScarletDevilStreak");
		RegisterMiscShader(ScarletDevilShader, "TrailPass", "OverpoweredTouhouSpearShader");
		BordernadoFireShader = LoadShader("BordernadoFire");
		RegisterMiscShader(BordernadoFireShader, "TrailPass", "Bordernado");
		PrismCrystalShader = LoadShader("PrismCrystalStreak");
		RegisterMiscShader(PrismCrystalShader, "TrailPass", "PrismaticStreak");
		ImpFlameTrailShader = LoadShader("ImpFlameTrail");
		RegisterMiscShader(ImpFlameTrailShader, "TrailPass", "ImpFlameTrail");
		SCalShieldShader = LoadShader("SupremeShieldShader");
		RegisterMiscShader(SCalShieldShader, "ShieldPass", "SupremeShield");
		RancorMagicCircleShader = LoadShader("RancorMagicCircle");
		RegisterMiscShader(RancorMagicCircleShader, "ShieldPass", "RancorMagicCircle");
		BasicTintShader = LoadShader("BasicTint");
		RegisterMiscShader(BasicTintShader, "TintPass", "BasicTint");
		CircularBarShader = LoadShader("CircularBarShader");
		RegisterMiscShader(CircularBarShader, "Pass0", "CircularBarShader");
		CircularBarSpriteShader = LoadShader("CircularBarSpriteShader");
		RegisterMiscShader(CircularBarSpriteShader, "Pass0", "CircularBarSpriteShader");
		DoGDisintegrationShader = LoadShader("DoGDisintegration");
		RegisterMiscShader(DoGDisintegrationShader, "DisintegrationPass", "DoGDisintegration");
		ArtAttackTrailShader = LoadShader("ArtAttackTrail");
		RegisterMiscShader(ArtAttackTrailShader, "TrailPass", "ArtAttack");
		CircularAoETelegraph = LoadShader("CircularAoETelegraph");
		RegisterMiscShader(CircularAoETelegraph, "TelegraphPass", "CircularAoETelegraph");
		IntersectionClipShader = LoadShader("IntersectionClipShader");
		RegisterMiscShader(IntersectionClipShader, "ClipPass", "IntersectionClip");
		LocalLinearTransformationShader = LoadShader("LocalLinearTransformationShader");
		RegisterMiscShader(LocalLinearTransformationShader, "TransformationPass", "LinearTransformation");
		BasicPrimitiveShader = LoadShader("BasicPrimitiveShader");
		RegisterMiscShader(BasicPrimitiveShader, "TrailPass", "PrimitiveDrawer");
		ArtemisLaserShader = LoadShader("ArtemisLaserShader");
		RegisterMiscShader(ArtemisLaserShader, "TrailPass", "ArtemisLaser");
		ExobladeSlashShader = LoadShader("ExobladeSlashShader");
		RegisterMiscShader(ExobladeSlashShader, "TrailPass", "ExobladeSlash");
		ExobladePierceShader = LoadShader("ExobladePierceShader");
		RegisterMiscShader(ExobladePierceShader, "PiercePass", "ExobladePierce");
		ExoVortexShader = LoadShader("ExoVortexShader");
		RegisterMiscShader(ExoVortexShader, "VortexPass", "ExoVortex");
		SideStreakTrailShader = LoadShader("SideStreakTrail");
		RegisterMiscShader(SideStreakTrailShader, "TrailPass", "SideStreakTrail");
		HeavenlyGaleTrailShader = LoadShader("HeavenlyGaleTrailShader");
		RegisterMiscShader(HeavenlyGaleTrailShader, "PiercePass", "HeavenlyGaleTrail");
		HeavenlyGaleLightningShader = LoadShader("HeavenlyGaleLightningShader");
		RegisterMiscShader(HeavenlyGaleLightningShader, "TrailPass", "HeavenlyGaleLightningArc");
		BlueStaticShader = LoadShader("BlueStaticShader");
		RegisterMiscShader(BlueStaticShader, "GlitchPass", "BlueStatic");
		PrimTextureOverlayShader = LoadShader("PrimTextureOverlayShader");
		RegisterMiscShader(PrimTextureOverlayShader, "TrailPass", "PrimitiveTexture");
		StandardPrimitiveShader = LoadShader("StandardPrimitiveShader");
		RegisterMiscShader(StandardPrimitiveShader, "PrimitivePass", "StandardPrimitiveShader");
		DoGPortalShader = LoadShader("ScreenShaders/DoGPortalShader");
		RegisterMiscShader(DoGPortalShader, "ScreenPass", "DoGPortal");
		FogShader = LoadShader("ScreenShaders/Fog");
		RegisterMiscShader(FogShader, "DyePass", "Fog");
		WaterfallShader = LoadShader("WaterfallShader");
		RegisterMiscShader(WaterfallShader, "TrailPass", "Waterfall");
		MetaballEdgeShader = LoadShader("Metaballs/MetaballEdgeShader");
		RegisterMiscShader(MetaballEdgeShader, "ParticlePass", "MetaballEdge");
		AdditiveMetaballEdgeShader = LoadShader("Metaballs/AdditiveMetaballEdgeShader");
		RegisterMiscShader(AdditiveMetaballEdgeShader, "ParticlePass", "AdditiveMetaballEdge");
		FluidShaders = LoadShader("FluidShaders");
		SylvestaffProjectileShader = LoadShader("SylvestaffProjectileShader");
		RegisterMiscShader(SylvestaffProjectileShader, "TrailPass", "SylvestaffProjectile");
		SylvestaffRibbonShader = LoadShader("SylvestaffRibbonShader");
		RegisterMiscShader(SylvestaffRibbonShader, "AutoloadPass", "SylvestaffRibbon");
		SpreadTelegraph = LoadShader("SpreadTelegraph");
		RegisterScreenShader(SpreadTelegraph, "TelegraphPass", "SpreadTelegraph");
		PixelatedSightLine = LoadShader("PixelatedSightLine");
		RegisterScreenShader(PixelatedSightLine, "SightLinePass", "PixelatedSightLine");
		WulfrumTilePing = LoadShader("WulfrumTilePing");
		RegisterScreenShader(WulfrumTilePing, "TilePingPass", "WulfrumTilePing");
		WulfrumScaffoldSelection = LoadShader("WulfrumScaffoldSelection");
		RegisterScreenShader(WulfrumScaffoldSelection, "TilePingPass", "WulfrumScaffoldSelection");
		RoverDriveShield = LoadShader("RoverDriveShield");
		RegisterScreenShader(RoverDriveShield, "ShieldPass", "RoverDriveShield");
		RotateSprite = LoadShader("RotateSprite");
		RegisterScreenShader(RotateSprite, "RotationPass", "RotateSprite");
		SwingSprite = LoadShader("SwingSprite");
		RegisterScreenShader(SwingSprite, "SwingPass", "SwingSprite");
		MiracleBlight = LoadShader("MiracleBlight");
		RegisterMiscShader(MiracleBlight, "BlightPass", "MiracleBlight");
		CircularGradientWithEdge = LoadShader("CircularGradientWithEdge");
		RegisterMiscShader(CircularGradientWithEdge, "CircularGradientWithEdgePass", "CircularGradientWithEdge");
		GaleforceArrowTrailShader = LoadShader("GaleforceArrowTrail");
		RegisterMiscShader(ArtAttackTrailShader, "TrailPass", "GaleforceArrowTrail");
		WavyOpacity = LoadShader("WavyOpacity");
		RegisterMiscShader(WavyOpacity, "WavyOpacityPass", "WavyOpacity");
		HellBall = LoadShader("HellBall");
		RegisterScreenShader(HellBall, "HellBallPass", "HellBall");
		PrimitiveClearShader = LoadShader("PrimitiveClearShader");
		RegisterScreenShader(PrimitiveClearShader, "AutoloadPass", "PrimitiveClearShader");
		HolyInfernoShader = LoadShader("ScreenShaders/HolyInfernoShader");
		RegisterMiscShader(HolyInfernoShader, "InfernoPass", "HolyInfernoShader");
		TeslaTrailShader = LoadShader("TeslaTrail");
		RegisterMiscShader(TeslaTrailShader, "TrailPass", "TeslaTrail");
		DozeLightingShader = LoadShader("DozeLightingShader");
		RegisterMiscShader(DozeLightingShader, "ShadowPass", "DozeLightingShader");
		FlipScreenShader = LoadShader("ScreenShaders/FlipScreen");
		RegisterScreenShader(FlipScreenShader, "FlipTheScreen", "FlipScreen", EffectPriority.VeryHigh);
		OtherworldBarrierDistortionShader = LoadShader("OtherworldBarrierDistortion");
		RegisterMiscShader(OtherworldBarrierDistortionShader, "DistortionPass", "OtherworldBarrierDistortion");
		BasicTextureDistortionShader = LoadShader("BasicTextureDistortionShader");
		RegisterMiscShader(BasicTextureDistortionShader, "DistortionPass", "BasicTextureDistortion");
		UnderwaterRaysShader = LoadShader("UnderwaterRaysShader");
		RegisterMiscShader(UnderwaterRaysShader, "UnderwaterRayPass", "UnderwaterRays");
		DoGDistortionWindsShader = LoadShader("DoGDistortionWindsShader");
		RegisterMiscShader(DoGDistortionWindsShader, "DistortionWindsPass", "DoGDistortionWinds");
		DoGBackgroundFogShader = LoadShader("DoGBackgroundFogShader");
		RegisterMiscShader(DoGBackgroundFogShader, "DoGFogPass", "DoGBackgroundFog");
		CircularOpacityShader = LoadShader("CircularOpacityShader");
		RegisterMiscShader(CircularOpacityShader, "CircularOpacityPass", "CircularOpacity");
		DoGRealityCrackShader = LoadShader("DoGRealityCrackShader");
		RegisterMiscShader(DoGRealityCrackShader, "DoGRealityCrackPass", "DoGRealityCrack");
		DoGRiftAuraShader = LoadShader("DoGRiftAuraShader");
		RegisterMiscShader(DoGRiftAuraShader, "DoGRiftAuraPass", "DoGRiftAura");
		RadialBlur = LoadShader("ScreenShaders/RadialBlur");
		RegisterScreenShader(RadialBlur, "RadialBlurPass", "RadialBlurShader");
		SeaPrismColorBlendingShader = LoadShader("SeaPrismColorBlending");
		RegisterMiscShader(SeaPrismColorBlendingShader, "SeaPrismBlendingPass", "SeaPrismColorBlending");
		Dissolve = LoadShader("Dissolve");
		RegisterMiscShader(Dissolve, "DissolvePass", "Dissolve");
		BrainOfCthulhuForcefield = LoadShader("ScreenShaders/BrainOfCthulhuForcefield");
		RegisterScreenShader(BrainOfCthulhuForcefield, "BoCShieldPass", "BrainOfCthulhuForcefield");
		NanoblackSlashShader = LoadShader("SlashEffects/NanoblackSlash");
		RegisterMiscShader(NanoblackSlashShader, "SlashPass", "NanoblackSlash");
		GaussianBloomShader = LoadShader("SlashEffects/GaussianBloom");
		RegisterMiscShader(GaussianBloomShader, "BloomShader", "GaussianBloom");
		SunkenSeaMenuLogoWater = LoadShader("UI/SunkenSeaMenuLogoWater");
		Asset<Effect> LoadShader(string path)
		{
			return calAss.Request<Effect>("Effects/" + path, (AssetRequestMode)1);
		}
	}

	private static void RegisterMiscShader(Asset<Effect> shader, string passName, string registrationName)
	{
		MiscShaderData passParamRegistration = new MiscShaderData(shader, passName);
		GameShaders.Misc["CalamityMod:" + registrationName] = passParamRegistration;
	}

	private static void RegisterSceneFilter(ScreenShaderData passReg, string registrationName, EffectPriority priority = EffectPriority.High)
	{
		string prefixedRegistrationName = "CalamityMod:" + registrationName;
		Filters.Scene[prefixedRegistrationName] = new Filter(passReg, priority);
		Filters.Scene[prefixedRegistrationName].Load();
	}

	private static void RegisterScreenShader(Asset<Effect> shader, string passName, string registrationName, EffectPriority priority = EffectPriority.High)
	{
		RegisterSceneFilter(new ScreenShaderData(shader, passName), registrationName, priority);
	}
}
