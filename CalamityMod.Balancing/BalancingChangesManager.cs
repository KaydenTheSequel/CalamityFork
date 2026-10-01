using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Balancing;

public sealed class BalancingChangesManager : ModSystem
{
	internal static List<NPCBalancingChange> NPCSpecificBalancingChanges;

	public override void SetStaticDefaults()
	{
		NPCSpecificBalancingChanges = new List<NPCBalancingChange>();
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<Crabulon>(), ResistTrueMelee(0.8f)));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.EaterOfWorlds, Do(new ProjectileResistBalancingRule(0.5f, 45))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.EaterOfWorlds, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<StickyFeather>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.EaterOfWorlds, Do(ResistTrueMelee(0.75f))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(267, Do(new ProjectileResistBalancingRule(0.5f, 45))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(267, Do(ResistTrueMelee(0.75f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Perforators, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(114, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<CrimslimeMinion>(), ModContent.ProjectileType<CorroslimeMinion>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.5f, 466))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<DormantBrimseekerBab>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<MountedScannerLaser>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<CryoBlast>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<MeowFire>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AquaticScourge, Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<Snowflake>(), ModContent.ProjectileType<SnowflakeIceStar>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.5f, 466))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<DormantBrimseekerBab>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.55f, ModContent.ProjectileType<MountedScannerLaser>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<MeowFire>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<IceBombFriendly>(), ModContent.ProjectileType<FrostShardFriendly>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Destroyer, Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<Snowflake>(), ModContent.ProjectileType<SnowflakeIceStar>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<AstrumAureus>(), ResistTrueMelee(0.75f)));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<AstrumAureus>(), Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<BallistaGreatArrow>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(370, Do(new ProjectileSpecificRequirementBalancingRule(1.2f, HiveBeeFilter))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(370, Do(new ProjectileResistBalancingRule(1.25f, ModContent.ProjectileType<SakuraBullet>(), ModContent.ProjectileType<PurpleButterfly>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(636, Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<AbyssBladeProjectile>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(636, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<PlagueTaintedDrone>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<AegisBlast>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<IcicleArrowProj>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(new ProjectileResistBalancingRule(0.65f, 684))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<AuroraFire>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ravager, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<HiveNuke>(), ModContent.ProjectileType<HiveMissile>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(439, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<AegisBlast>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(439, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<SubductionFlameburst>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(439, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<ArtAttackStrike>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(439, Do(new ProjectileResistBalancingRule(0.8f, 931))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.25f, ModContent.ProjectileType<PlaguenadeBee>(), ModContent.ProjectileType<PlaguenadeProj>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.3f, 625, 626, 627, 628))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<AtlantisSpear>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.5f, 461))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.65f, 710))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<IcicleArrowProj>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<AuroraFire>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<FlakKrakenProjectile>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<BallisticPoisonCloud>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.AstrumDeus, Do(new ProjectileResistBalancingRule(0.8f, 779, 783))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(398, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<HiveNuke>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<ProfanedRocks>(), ResistTrueMelee(0.5f)));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<Providence>(), new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<SpatialSpear>(), ModContent.ProjectileType<SpatialSpear2>(), ModContent.ProjectileType<SpatialSpear3>(), ModContent.ProjectileType<SpatialSpear4>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<DarkEnergy>(), ResistTrueMelee(0.5f)));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<DarkEnergy>(), new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<GalacticaComet>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<DarkEnergy>(), new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<PristineSecondary>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<DarkEnergy>(), new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<StellarTorusBeam>())));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(ResistTrueMelee(0.5f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<DazzlingStabber>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.5f, 632))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<CelestialAxeMinion>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<PristineSecondary>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<TacticiansElectricBoom>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<DevilsSunriseProj>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.StormWeaver, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<EventHorizonStar>(), ModContent.ProjectileType<EventHorizonBlackhole>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<OldDuke>(), new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<TimeBoltKnife>())));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.DevourerOfGods, Do(new ProjectileResistBalancingRule(0.35f, ModContent.ProjectileType<WavePounderBoom>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.DevourerOfGods, Do(new ProjectileResistBalancingRule(0.4f, ModContent.ProjectileType<CorinthPrimeAirburst>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.DevourerOfGods, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<SulphuricAcidCannonExplosion>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.DevourerOfGods, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<NuclearFuryProjectile>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.DevourerOfGods, Do(new StealthStrikeBalancingRule(1.15f, ModContent.ProjectileType<TimeBoltKnife>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<Yharon>(), Do(new ProjectileResistBalancingRule(0.85f, ModContent.ProjectileType<RadiationRain>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<Yharon>(), Do(new ProjectileResistBalancingRule(0.85f, ModContent.ProjectileType<TimeBoltKnife>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<DevilsDevastationHoldout>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<DynamicPursuerProjectile>(), ModContent.ProjectileType<DynamicPursuerLaser>(), ModContent.ProjectileType<DynamicPursuerElectricity>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<PhasedGodRay>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileSpecificRequirementBalancingRule(0.8f, AotCThrowCombo))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileSpecificRequirementBalancingRule(0.8f, DragonRageFilter))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<RancorLaserbeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<YharimsCrystalBeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Ares, Do(new ProjectileResistBalancingRule(0.8f, 933))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(ResistTrueMelee(0.35f))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.35f, ModContent.ProjectileType<FinalDawnThrow2>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.35f, ModContent.ProjectileType<StellarTorusBeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.4f, ModContent.ProjectileType<DynamicPursuerProjectile>(), ModContent.ProjectileType<DynamicPursuerLaser>(), ModContent.ProjectileType<DynamicPursuerElectricity>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<ChickenExplosion>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<RancorLaserbeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<VehemenceSkull>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.5f, ModContent.ProjectileType<YharimsCrystalBeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.55f, ModContent.ProjectileType<WrathwingCinder>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<OmicronBeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.65f, ModContent.ProjectileType<PrismaticRay>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<InfernadoFriendly>(), ModContent.ProjectileType<DragonScalesInfernado>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileSpecificRequirementBalancingRule(0.75f, AotCThrowCombo))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileSpecificRequirementBalancingRule(0.75f, DragonRageFilter))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileSpecificRequirementBalancingRule(0.75f, BigGaelsSkullFilter))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.75f, 933))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<LiliesOfFinalityAoE>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<Paradoxica>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.8f, ModContent.ProjectileType<ClimaxBeam>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.85f, ModContent.ProjectileType<FinalDawnFlame>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.85f, ModContent.ProjectileType<SpiritCongregation>()))));
		NPCSpecificBalancingChanges.AddRange(Bundle(CalamityNPCTypeSets.Thanatos, Do(new ProjectileResistBalancingRule(0.85f, ModContent.ProjectileType<UltimaRay>()))));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<BrimstoneHeart>(), new ProjectileResistBalancingRule(0.66f, ModContent.ProjectileType<CelestusMiniScythe>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<BrimstoneHeart>(), new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<SupernovaBoom>(), ModContent.ProjectileType<SupernovaStealthBoom>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<BrimstoneHeart>(), new ProjectileResistBalancingRule(0.7f, ModContent.ProjectileType<PrismComet>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<SoulSeekerSupreme>(), new ProjectileResistBalancingRule(0.2f, ModContent.ProjectileType<ChickenExplosion>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<SoulSeekerSupreme>(), new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<ExoVortex>(), ModContent.ProjectileType<ExoVortex2>(), ModContent.ProjectileType<EnormousConsumingVortex>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<SoulSeekerSupreme>(), new ProjectileResistBalancingRule(0.6f, ModContent.ProjectileType<SupernovaBoom>(), ModContent.ProjectileType<SupernovaStealthBoom>())));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<SoulSeekerSupreme>(), new ProjectileResistBalancingRule(0.7f, 933)));
		NPCSpecificBalancingChanges.Add(new NPCBalancingChange(ModContent.NPCType<SoulSeekerSupreme>(), new ProjectileResistBalancingRule(0.75f, ModContent.ProjectileType<YharimsCrystalBeam>())));
		static bool AotCThrowCombo(Projectile p)
		{
			if (p.type == ModContent.ProjectileType<ArkoftheCosmosSwungBlade>())
			{
				if (p.ai[0] != 2f)
				{
					return p.ai[0] == 3f;
				}
				return true;
			}
			return false;
		}
		static bool BigGaelsSkullFilter(Projectile p)
		{
			if (p.type == ModContent.ProjectileType<GaelSkull>())
			{
				return p.ai[1] == 1f;
			}
			return false;
		}
		static bool DragonRageFilter(Projectile p)
		{
			if (p.type != ModContent.ProjectileType<DragonRageStaff>() && p.type != ModContent.ProjectileType<DragonRageFireball>())
			{
				if (p.type == ModContent.ProjectileType<FuckYou>())
				{
					return p.CountsAsClass<MeleeDamageClass>();
				}
				return false;
			}
			return true;
		}
		static bool HiveBeeFilter(Projectile p)
		{
			if (p.type == ModContent.ProjectileType<BasicPlagueBee>())
			{
				return Main.player[p.owner].HeldItem.type == ModContent.ItemType<TheHive>();
			}
			return false;
		}
		static IBalancingRule ResistTrueMelee(float f)
		{
			return new ClassResistBalancingRule(f, TrueMeleeDamageClass.Instance);
		}
	}

	public override void Unload()
	{
		NPCSpecificBalancingChanges = null;
	}

	public static void ApplyFromProjectile(NPC npc, ref NPC.HitModifiers modifiers, Projectile proj)
	{
		foreach (NPCBalancingChange balanceChange in NPCSpecificBalancingChanges)
		{
			if (npc.type != balanceChange.NPCType)
			{
				continue;
			}
			IBalancingRule[] balancingRules = balanceChange.BalancingRules;
			foreach (IBalancingRule balancingRule in balancingRules)
			{
				if (balancingRule.AppliesTo(npc, modifiers, proj))
				{
					balancingRule.ApplyBalancingChange(npc, ref modifiers);
				}
			}
		}
	}

	internal static IBalancingRule[] Do(params IBalancingRule[] rules)
	{
		return rules;
	}

	internal static NPCBalancingChange[] Bundle(IEnumerable<int> npcIDs, params IBalancingRule[] rules)
	{
		NPCBalancingChange[] changes = new NPCBalancingChange[npcIDs.Count()];
		for (int i = 0; i < changes.Length; i++)
		{
			changes[i] = new NPCBalancingChange(npcIDs.ElementAt(i), rules);
		}
		return changes;
	}
}
