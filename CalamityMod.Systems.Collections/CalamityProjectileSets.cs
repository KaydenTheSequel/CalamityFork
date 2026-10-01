using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityProjectileSets
{
	public static SetFactory Factory = new SetFactory(ProjectileLoader.ProjectileCount, "CalamityMod/ProjectileID", Search);

	public static IdDictionary Search = IdDictionary.Create<ProjectileID, int>();

	public static bool[] MinionWhichIgnoresSummonerNerf = Factory.CreateBoolSet();

	public static bool[] IsBuffedDungeonProjectile = Factory.CreateBoolSet(300, 290, 291, 292, 293, 302, 303, 180, 299);

	public static bool[] IsBuffedEventProjectile = Factory.CreateBoolSet(325, 326, 327, 328, 329, 82, 345, 346, 347, 348, 349, 350, 351, 352, 180, 83, 498, 501);

	public static bool[] IsFriendlyBeeProjectile;

	public static bool[] ResistedExplosiveProjectile;

	public static bool[] ShouldNotBeReflected;

	public static bool[] DoesNotGetHomingWithGrapeBeer;

	public static int[] SharedIDStaticIFrames;

	static CalamityProjectileSets()
	{
		SetFactory factory = Factory;
		int[] obj = new int[11]
		{
			566, 181, 189, 373, 374, 0, 0, 0, 0, 0,
			0
		};
		obj[5] = ModContent.ProjectileType<PlaguenadeBee>();
		obj[6] = ModContent.ProjectileType<PlaguePrincess>();
		obj[7] = ModContent.ProjectileType<BabyPlaguebringer>();
		obj[8] = ModContent.ProjectileType<PlagueBeeSmall>();
		obj[9] = ModContent.ProjectileType<BetterHornetStinger>();
		obj[10] = ModContent.ProjectileType<BasicPlagueBee>();
		IsFriendlyBeeProjectile = factory.CreateBoolSet(obj);
		SetFactory factory2 = Factory;
		int[] obj2 = new int[16]
		{
			30, 397, 517, 28, 37, 516, 29, 470, 637, 108,
			281, 588, 519, 773, 1002, 0
		};
		obj2[15] = ModContent.ProjectileType<AeroExplosive>();
		ResistedExplosiveProjectile = factory2.CreateBoolSet(obj2);
		ShouldNotBeReflected = Factory.CreateBoolSet(447, 455, ModContent.ProjectileType<BrimstoneMonster>(), ModContent.ProjectileType<InfernadoRevenge>(), ModContent.ProjectileType<OverlyDramaticDukeSummoner>(), ModContent.ProjectileType<ProvidenceHolyRay>(), ModContent.ProjectileType<OldDukeVortex>(), ModContent.ProjectileType<BrimstoneRay>(), ModContent.ProjectileType<AresDeathBeamStart>(), ModContent.ProjectileType<AresGaussNukeProjectileBoom>(), ModContent.ProjectileType<AresLaserBeamStart>(), ModContent.ProjectileType<ArtemisSpinLaserbeam>(), ModContent.ProjectileType<BirbAura>(), ModContent.ProjectileType<ThanatosBeamStart>());
		DoesNotGetHomingWithGrapeBeer = Factory.CreateBoolSet(ModContent.ProjectileType<NukeOfBliss>(), ModContent.ProjectileType<PrismaticEnergyBlast>(), ModContent.ProjectileType<PrismEnergyBullet>(), ModContent.ProjectileType<PrismMine>(), ModContent.ProjectileType<ScorchedEarthRocket>(), ModContent.ProjectileType<UltimaRay>(), ModContent.ProjectileType<SproutingArrowMain>());
		SetFactory factory3 = Factory;
		int[] obj3 = new int[54]
		{
			181, 181, 566, 181, 7, 7, 8, 7, 493, 493,
			494, 493, 150, 150, 151, 150, 152, 150, 76, 76,
			77, 76, 78, 76, 342, 342, 343, 342, 344, 342,
			567, 567, 568, 567, 569, 567, 570, 567, 571, 567,
			0, 0, 0, 0, 0, 0, 0, 0, 511, 511,
			512, 511, 513, 511
		};
		obj3[40] = ModContent.ProjectileType<AstralCrystal>();
		obj3[41] = ModContent.ProjectileType<AstralCrystal>();
		obj3[42] = ModContent.ProjectileType<AstralCrystalInvisibleExplosion>();
		obj3[43] = ModContent.ProjectileType<AstralCrystal>();
		obj3[44] = ModContent.ProjectileType<KeelhaulGeyserBottom>();
		obj3[45] = ModContent.ProjectileType<KeelhaulGeyserBottom>();
		obj3[46] = ModContent.ProjectileType<KeelhaulGeyserTop>();
		obj3[47] = ModContent.ProjectileType<KeelhaulGeyserBottom>();
		SharedIDStaticIFrames = factory3.CreateIntSet(-1, obj3);
	}
}
