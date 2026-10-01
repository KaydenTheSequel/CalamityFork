namespace CalamityMod.Balancing;

public static class BalancingConstants
{
	internal static readonly float DefaultMoveSpeedBoost = 1.5f;

	internal static readonly float ConfigBoostedBaseJumpSpeed = 5.71f;

	internal static readonly float HoldingDownGravityMultiplier = 2f;

	internal const float BalloonJumpSpeedBoost = 0.75f;

	internal const float VanillaFrogLegJumpSpeedBoost = 1.6f;

	internal static readonly float AmphibianBootsJumpSpeedBoost = 1.6f;

	internal static readonly float ShadowArmorRunAccelerationMultiplier = 1.25f;

	internal static readonly float ShadowArmorMaxRunSpeedMultiplier = 1.05f;

	internal static readonly float ShadowArmorAccRunSpeedMultiplier = 1.05f;

	internal static readonly float ShadowArmorRunSlowdownMultiplier = 1.5f;

	internal const float SoaringInsigniaRunAccelerationMultiplier = 1.25f;

	internal const int VanillaDefaultIFrames = 40;

	internal const int VanillaParryIFrames = 60;

	internal const int VanillaDodgeIFrames = 80;

	internal const int CrossNecklaceIFrameBoost = 40;

	internal const int CrossNecklaceIFrameBoost_Parry = 30;

	internal const int UniversalDashCooldown = 30;

	internal const int UniversalSashDashCooldown = 30;

	internal const int UniversalShieldSlamCooldown = 30;

	internal const int UniversalShieldBonkCooldown = 30;

	internal const int OnShieldBonkCooldown = 30;

	internal const int ShieldOfCthulhuBonkNoCollideFrames = 6;

	internal const int SolarFlareIFrames = 12;

	internal const float SolarFlareBaseDamage = 400f;

	internal static readonly int DodgeCooldownMax = 5400;

	internal const int NewDefaultDamageVariationPercent = 5;

	internal static readonly float SummonerCrossClassNerf = 0.75f;

	internal static readonly float SummonAllClassScalingFactor = 0.75f;

	internal static readonly float MinimumAllowedAttackSpeed = 0.25f;

	internal static readonly float MaximumAllowedAttackSpeed = 10f;

	internal static readonly float LeatherWhipTagDamageMultiplier = 1.08f;

	internal static readonly float SnapthornTagDamageMultiplier = 1.04f;

	internal static readonly float SpinalTapTagDamageMultiplier = 1.08f;

	internal static readonly float FirecrackerExplosionDamageMultiplier = 2f;

	internal static readonly float CoolWhipTagDamageMultiplier = 1.08f;

	internal static readonly float DurendalTagDamageMultiplier = 1.09f;

	internal static readonly float MorningStarTagDamageMultiplier = 1.11f;

	internal static readonly float KaleidoscopeTagDamageMultiplier = 1.12f;

	internal const float SharpeningStationArmorPenetration = 5f;

	internal static readonly float BeetleScaleMailMeleeDamagePerBeetle = 0.1f;

	internal static readonly float BeetleScaleMailMeleeSpeedPerBeetle = 0.05f;

	internal static readonly float NebulaDamagePerBooster = 0.075f;

	internal static readonly int LifeStealCap = 100;

	internal static readonly float LifeStealCap_Classic = 80f;

	internal static readonly float LifeStealCap_Expert = 70f;

	internal static readonly float LifeStealRecoveryRate_Classic = 0.6f;

	internal static readonly float LifeStealRecoveryRate_Expert = 0.5f;

	internal static readonly float LifeStealRecoveryRateReduction_Classic = 0.4f;

	internal static readonly float LifeStealRecoveryRateReduction_Expert = 0.35f;

	public static double UniversalStealthStrikeDamageFactor = 0.42;

	internal static readonly float BaseStealthGenTime = 4f;

	internal static readonly float MovingStealthGenRatio = 0.5f;

	internal static readonly float BeetleShellDRPerBeetle = 0.1f;

	internal static readonly float SolarFlareShieldDR = 0.25f;

	internal static readonly int NebulaLifeRegenPerBooster = 4;

	internal static readonly int NebulaManaRegenFrameCounterThreshold = 12;

	internal const double DefaultDefenseDamageRatio = 0.3333;

	internal static readonly int DefenseDamageFloor_NormalPHM = 3;

	internal static readonly int DefenseDamageFloor_NormalHM = 8;

	internal static readonly int DefenseDamageFloor_NormalPML = 16;

	internal static readonly int DefenseDamageFloor_RevPHM = 4;

	internal static readonly int DefenseDamageFloor_RevHM = 10;

	internal static readonly int DefenseDamageFloor_RevPML = 20;

	internal static readonly int DefenseDamageFloor_DeathPHM = 5;

	internal static readonly int DefenseDamageFloor_DeathHM = 12;

	internal static readonly int DefenseDamageFloor_DeathPML = 24;

	internal static readonly int DefenseDamageFloor_BossRush = 25;

	internal static readonly int DefaultRageDuration = CalamityUtils.SecondsToFrames(9);

	internal static readonly int RageDurationPerBooster = CalamityUtils.SecondsToFrames(1);

	internal static readonly int RageCombatDelayTime = CalamityUtils.SecondsToFrames(10);

	internal static readonly int RageFadeTime = CalamityUtils.SecondsToFrames(30);

	internal static readonly float DefaultRageDamageBoost = 0.35f;

	internal static readonly float AdrenalineDamageBoost = 1.5f;

	internal static readonly float AdrenalineDamagePerBooster = 0.2f;

	internal static readonly float FullAdrenalineDR = 0.5f;

	internal static readonly float AdrenalineDRPerBooster = 0.05f;

	internal static readonly int AdrenalinePauseAfterDamage = CalamityUtils.SecondsToFrames(1);

	internal static readonly float MinimumAdrenalineLoss = 0.25f;

	internal static readonly float AdrenalineFalloffTinyHitHealthRatio = 0.05f;

	internal static readonly float TrueMeleeRipperReductionFactor = 0.5f;

	public const float PierceResistHarshness = 0.12f;

	public const float PierceResistCap = 0.8f;

	internal const int TinyHealthThreshold = 5;

	internal const int TinyDamageThreshold = 5;

	internal const int NoContactDamageHealthThreshold = 3000;

	internal const int UnreasonableHealthThreshold = 25000000;

	public static float ExpertHealthScalingOverride_2Players = 1.75f;

	public static float ExpertHealthScalingOverride_3Players = 2.25f;

	internal static float DodgeCooldownDamageMult => 0.33f;

	internal static float DodgeCooldownMultPerStack => 0.85f;
}
