using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Tools;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityItemSets
{
	public static SetFactory Factory = new SetFactory(ItemLoader.ItemCount, "CalamityMod/ItemID", Search);

	public static IdDictionary Search = IdDictionary.Create<ItemID, int>();

	public static bool[] WeaponWithToolPowerAffectedBySummonPenalty;

	public static bool[] ItemWhichDisablesSummonerNerf;

	public static bool[] FishingPoleThatNeverBreaks;

	public static bool[] ItemForcedInsideWorld;

	public static bool[] DisablesVeneratedLocketEffect;

	public static bool[] MagicGun;

	public static bool[] RogueBomb;

	public static bool[] RogueBoomerang;

	public static bool[] RogueDagger;

	public static bool[] RogueJavelin;

	public static bool[] RogueSpikyBall;

	static CalamityItemSets()
	{
		SetFactory factory = Factory;
		int[] obj = new int[8] { 3098, 5095, 2320, 0, 0, 0, 0, 0 };
		obj[3] = ModContent.ItemType<AxeofPurity>();
		obj[4] = ModContent.ItemType<HydraulicVoltCrasher>();
		obj[5] = ModContent.ItemType<InfernaCutter>();
		obj[6] = ModContent.ItemType<PhotonRipper>();
		obj[7] = ModContent.ItemType<Respiteblock>();
		WeaponWithToolPowerAffectedBySummonPenalty = factory.CreateBoolSet(obj);
		ItemWhichDisablesSummonerNerf = Factory.CreateBoolSet();
		FishingPoleThatNeverBreaks = Factory.CreateBoolSet(2294, ModContent.ItemType<EarlyBloomRod>(), ModContent.ItemType<TheDevourerofCods>());
		ItemForcedInsideWorld = Factory.CreateBoolSet(2609, 2611, 2624, 2623, 2622, 2621, 3330, 499, 575, ModContent.ItemType<SubmarineShocker>(), ModContent.ItemType<Barinautical>(), ModContent.ItemType<Downpour>(), ModContent.ItemType<DeepseaStaff>(), ModContent.ItemType<ScourgeoftheSeas>(), ModContent.ItemType<SeasSearing>(), ModContent.ItemType<InsidiousImpaler>(), ModContent.ItemType<SepticSkewer>(), ModContent.ItemType<FetidEmesis>(), ModContent.ItemType<VitriolicViper>(), ModContent.ItemType<CadaverousCarrion>(), ModContent.ItemType<MutatedTruffle>(), ModContent.ItemType<ToxicantTwister>(), ModContent.ItemType<TheOldReaper>(), ModContent.ItemType<Greentide>(), ModContent.ItemType<Leviatitan>(), ModContent.ItemType<Atlantis>(), ModContent.ItemType<AnahitasArpeggio>(), ModContent.ItemType<Whitewater>(), ModContent.ItemType<LeviathanTeeth>(), ModContent.ItemType<GastricBelcherStaff>(), ModContent.ItemType<PearlofEnthrallment>(), ModContent.ItemType<AquaticScourgeBag>(), ModContent.ItemType<OldDukeBag>(), ModContent.ItemType<LeviathanBag>(), ModContent.ItemType<OldDukeMask>(), ModContent.ItemType<LeviathanMask>(), ModContent.ItemType<AnahitaMask>(), ModContent.ItemType<AquaticScourgeMask>(), ModContent.ItemType<OldDukeTrophy>(), ModContent.ItemType<LeviathanTrophy>(), ModContent.ItemType<AquaticScourgeTrophy>(), ModContent.ItemType<LoreAquaticScourge>(), ModContent.ItemType<LoreLeviathanAnahita>(), ModContent.ItemType<LoreSulphurSea>(), ModContent.ItemType<LoreAbyss>(), ModContent.ItemType<LoreOldDuke>(), ModContent.ItemType<OldDukeRelic>(), ModContent.ItemType<LeviathanAnahitaRelic>(), ModContent.ItemType<AquaticScourgeRelic>(), ModContent.ItemType<AeroStone>(), ModContent.ItemType<CorrosiveSpine>(), ModContent.ItemType<TheCommunity>(), ModContent.ItemType<DeepSeaAnchor>(), ModContent.ItemType<BrinyBaron>(), ModContent.ItemType<DukesDecapitator>(), ModContent.ItemType<SulphurousSand>(), ModContent.ItemType<SupremeHealingPotion>(), ModContent.ItemType<EssenceofSunlight>(), ModContent.ItemType<ThankYouPainting>());
		DisablesVeneratedLocketEffect = Factory.CreateBoolSet(ModContent.ItemType<SlickCane>(), ModContent.ItemType<Mycoroot>(), ModContent.ItemType<CosmicKunai>());
		SetFactory factory2 = Factory;
		int[] obj2 = new int[35]
		{
			1121, 2623, 2882, 1295, 2795, 514, 1178, 1260, 127, 1155,
			4347, 4348, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};
		obj2[12] = ModContent.ItemType<AbyssShocker>();
		obj2[13] = ModContent.ItemType<AcidGun>();
		obj2[14] = ModContent.ItemType<AethersWhisper>();
		obj2[15] = ModContent.ItemType<AetherfluxCannon>();
		obj2[16] = ModContent.ItemType<ApoctosisArray>();
		obj2[17] = ModContent.ItemType<Cryophobia>();
		obj2[18] = ModContent.ItemType<Effervescence>();
		obj2[19] = ModContent.ItemType<EidolicWail>();
		obj2[20] = ModContent.ItemType<Genesis>();
		obj2[21] = ModContent.ItemType<IonBlaster>();
		obj2[22] = ModContent.ItemType<NanoPurge>();
		obj2[23] = ModContent.ItemType<Omicron>();
		obj2[24] = ModContent.ItemType<PlasmaCaster>();
		obj2[25] = ModContent.ItemType<PlasmaRifle>();
		obj2[26] = ModContent.ItemType<PulsePistol>();
		obj2[27] = ModContent.ItemType<PurgeGuzzler>();
		obj2[28] = ModContent.ItemType<RainbowPartyCannon>();
		obj2[29] = ModContent.ItemType<SHPC>();
		obj2[30] = ModContent.ItemType<TeslaCannon>();
		obj2[31] = ModContent.ItemType<TheSwarmer>();
		obj2[32] = ModContent.ItemType<Volterion>();
		obj2[33] = ModContent.ItemType<Vulcan>();
		obj2[34] = ModContent.ItemType<Wingman>();
		MagicGun = factory2.CreateBoolSet(obj2);
		RogueBomb = Factory.CreateBoolSet(ModContent.ItemType<BallisticPoisonBomb>(), ModContent.ItemType<BlastBarrel>(), ModContent.ItemType<ContaminatedBile>(), ModContent.ItemType<ConsecratedWater>(), ModContent.ItemType<CraniumSmasher>(), ModContent.ItemType<DesecratedWater>(), ModContent.ItemType<DuststormInABottle>(), ModContent.ItemType<Exorcism>(), ModContent.ItemType<LeonidProgenitor>(), ModContent.ItemType<MeteorFist>(), ModContent.ItemType<Penumbra>(), ModContent.ItemType<Plaguenade>(), ModContent.ItemType<PlasmaGrenade>(), ModContent.ItemType<PulseGrenade>(), ModContent.ItemType<Pumpkaboom>(), ModContent.ItemType<SeafoamBomb>(), ModContent.ItemType<SealedSingularity>(), ModContent.ItemType<SkyfinBombers>(), ModContent.ItemType<SpentFuelContainer>(), ModContent.ItemType<StarofDestruction>(), ModContent.ItemType<Supernova>(), ModContent.ItemType<TotalityBreakers>(), ModContent.ItemType<WavePounder>(), ModContent.ItemType<Whitewater>());
		RogueBoomerang = Factory.CreateBoolSet(ModContent.ItemType<AerialTracker>(), ModContent.ItemType<Brimblade>(), ModContent.ItemType<Celestus>(), ModContent.ItemType<DefectiveSphere>(), ModContent.ItemType<DimensionTearingDisk>(), ModContent.ItemType<DynamicPursuer>(), ModContent.ItemType<EnchantedAxe>(), ModContent.ItemType<EpidemicShredder>(), ModContent.ItemType<Equanimity>(), ModContent.ItemType<FishboneBoomerang>(), ModContent.ItemType<FrostcrushValari>(), ModContent.ItemType<GhoulishGouger>(), ModContent.ItemType<Icebreaker>(), ModContent.ItemType<InfestedClawmerang>(), ModContent.ItemType<KelvinCatalyst>(), ModContent.ItemType<Kylie>(), ModContent.ItemType<MangroveChakram>(), ModContent.ItemType<MoltenAmputator>(), ModContent.ItemType<NanoblackReaper>(), ModContent.ItemType<ReboundingRainbow>(), ModContent.ItemType<SamsaraSlicer>(), ModContent.ItemType<SubductionSlicer>(), ModContent.ItemType<ToxicantTwister>(), ModContent.ItemType<Valediction>());
		RogueDagger = Factory.CreateBoolSet(ModContent.ItemType<AshenStalactite>(), ModContent.ItemType<Cinquedea>(), ModContent.ItemType<Crystalline>(), ModContent.ItemType<FeatherKnife>(), ModContent.ItemType<GelDart>(), ModContent.ItemType<GildedDagger>(), ModContent.ItemType<GleamingDagger>(), ModContent.ItemType<InfernalKris>(), ModContent.ItemType<Mycoroot>(), ModContent.ItemType<ShinobiBlade>(), ModContent.ItemType<SporeKnife>(), ModContent.ItemType<WulfrumKnife>(), ModContent.ItemType<CobaltKunai>(), ModContent.ItemType<CorpusAvertor>(), ModContent.ItemType<CursedDagger>(), ModContent.ItemType<LeviathanTeeth>(), ModContent.ItemType<Malachite>(), ModContent.ItemType<MythrilKnife>(), ModContent.ItemType<OrichalcumSpikedGemstone>(), ModContent.ItemType<Prismalline>(), ModContent.ItemType<RadiantStar>(), ModContent.ItemType<StellarKnife>(), ModContent.ItemType<StormfrontRazor>(), ModContent.ItemType<TerrorTalons>(), ModContent.ItemType<CosmicKunai>(), ModContent.ItemType<JawsOfOblivion>(), ModContent.ItemType<LunarKunai>(), ModContent.ItemType<Sacrifice>(), ModContent.ItemType<Seraphim>(), ModContent.ItemType<ShatteredDawn>(), ModContent.ItemType<TarragonThrowingDart>(), ModContent.ItemType<TimeBolt>(), ModContent.ItemType<TwistingThunder>(), ModContent.ItemType<UtensilPoker>());
		RogueJavelin = Factory.CreateBoolSet(ModContent.ItemType<AntlionSkewer>(), ModContent.ItemType<CrystalPiercer>(), ModContent.ItemType<EclipsesFall>(), ModContent.ItemType<IchorSpear>(), ModContent.ItemType<Vega>(), ModContent.ItemType<PalladiumJavelin>(), ModContent.ItemType<PhantasmalRuin>(), ModContent.ItemType<ProfanedPartisan>(), ModContent.ItemType<RealityRupture>(), ModContent.ItemType<ScarletDevil>(), ModContent.ItemType<ScourgeoftheDesert>(), ModContent.ItemType<ScourgeoftheSeas>(), ModContent.ItemType<ShardofAntumbra>(), ModContent.ItemType<SpearofDestiny>(), ModContent.ItemType<SpearofPaleolith>(), ModContent.ItemType<Turbulance>(), ModContent.ItemType<TheAtomSplitter>(), ModContent.ItemType<WaveSkipper>(), ModContent.ItemType<Wrathwing>());
		RogueSpikyBall = Factory.CreateBoolSet(ModContent.ItemType<BurningStrife>(), ModContent.ItemType<GodsParanoia>(), ModContent.ItemType<MetalMonstrosity>(), ModContent.ItemType<NastyCholla>(), ModContent.ItemType<SystemBane>(), ModContent.ItemType<WebBall>());
	}
}
