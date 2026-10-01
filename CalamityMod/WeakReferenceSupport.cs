using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.SummonItems.Invasion;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Other;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Summon.Umbrella;
using CalamityMod.Systems;
using CalamityMod.Systems.Graphic.LiquidSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod;

internal class WeakReferenceSupport : ModSystem
{
	public const string CalamityWikiURLOld = "calamitymod.wiki.gg";

	public const string CalamityWikiURL = "https://calamitymod.wiki.gg/wiki/{}";

	private const string loreItemPage = "Lore#Lore_Items";

	private static readonly Dictionary<string, float> BossChecklistProgressionValues;

	private static Color DefaultTooltipColor;

	private static Color DefaultDamageColor;

	private static Color DefaultCritColor;

	private static Color MeleeTooltipColor;

	private static Color MeleeDamageColor;

	private static Color MeleeCritColor;

	private static Color MeleeRangedTooltipColor;

	private static Color MeleeRangedDamageColor;

	private static Color MeleeRangedCritColor;

	private static Color RogueTooltipColor;

	private static Color RogueDamageColor;

	private static Color RogueCritColor;

	private static Color StealthTooltipColor;

	private static Color StealthDamageColor;

	private static Color StealthCritColor;

	public override void Load()
	{
		LavaStyleToBiomeLava();
	}

	public override void PostSetupContent()
	{
		BossChecklistSupport();
		FargosSupport();
		DialogueTweakSupport();
		SummonersAssociationSupport();
		ColoredDamageTypesSupport();
		LuminanceSupport();
		if (!Main.dedServ)
		{
			WikiThisSupport();
		}
	}

	private static void LavaStyleToBiomeLava()
	{
		CalamityMod calamity = ModContent.GetInstance<CalamityMod>();
		Mod biomelava = ExternalMods.biomeLava;
		if (biomelava == null)
		{
			return;
		}
		foreach (ModLavaStyle allStyle in ModLavaStyleLoader.AllStyles)
		{
			ModLavaStyle lavaStyle = allStyle;
			_ = lavaStyle.Slot;
			if (lavaStyle != null)
			{
				Func<int> GetSplashDust = lavaStyle.GetSplashDust;
				Func<int> GetDropletGore = lavaStyle.GetDropletGore;
				Func<int, int, float, float, float, Vector3> ModifyLightFunc = ModifyLight;
				Func<bool> IsLavaActive = lavaStyle.IsLavaActive;
				Func<bool> lavafallGlowmask = lavaStyle.LavafallGlowmask;
				Action<Player, NPC, int> InflictDebuffFunc = InflictDebuff;
				Func<bool> yes = InflictsOnFire;
				biomelava.Call("ModLavaStyle", calamity, lavaStyle.Name, lavaStyle.Texture, lavaStyle.BlockTexture, lavaStyle.SlopeTexture, lavaStyle.WaterfallTexture, GetSplashDust, GetDropletGore, ModifyLightFunc, IsLavaActive, lavafallGlowmask, InflictDebuffFunc, yes);
			}
			void InflictDebuff(Player player, NPC npc, int onfireDuration)
			{
				if (player != null && npc == null)
				{
					lavaStyle.InflictDebuff(player, onfireDuration);
				}
			}
			Vector3 ModifyLight(int x, int y, float r, float g, float b)
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				lavaStyle.ModifyLight(x, y, ref r, ref g, ref b);
				return new Vector3(r, g, b);
			}
		}
		static bool InflictsOnFire()
		{
			return true;
		}
	}

	private static void WikiThisSupport()
	{
		Mod wiki;
		if (!Main.dedServ)
		{
			CalamityMod calamity = ModContent.GetInstance<CalamityMod>();
			wiki = ExternalMods.wikithis;
			if (wiki != null)
			{
				bool oldVersion = wiki.Version < new Version(2, 4, 7, 5);
				wiki.Call("AddModURL", calamity, oldVersion ? "calamitymod.wiki.gg" : "https://calamitymod.wiki.gg/wiki/{}");
				wiki.Call(0, calamity, oldVersion ? "calamitymod.wiki.gg" : "https://calamitymod.wiki.gg/wiki/{}");
				wiki.Call("AddWikiTexture", calamity, ModContent.Request<Texture2D>("CalamityMod/ModSupport/WikiThisIcon", (AssetRequestMode)2));
				wiki.Call(3, calamity, ModContent.Request<Texture2D>("CalamityMod/ModSupport/WikiThisIcon", (AssetRequestMode)2));
				ItemRedirect(ModContent.ItemType<PineapplePet>(), "Pineapple (calamity)");
				ItemRedirect(ModContent.ItemType<TrashmanTrashcan>(), "Trash Can (pet)");
				ItemRedirect(ModContent.ItemType<LoreAstralInfection>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAbyss>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAquaticScourge>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreArchmage>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAstrumAureus>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAstrumDeus>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAwakening>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreAzafure>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreBloodMoon>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreBrainofCthulhu>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreBrimstoneElemental>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCalamitas>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCalamitasClone>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCeaselessVoid>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCorruption>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCrabulon>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCrimson>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreCynosure>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreDesertScourge>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreDestroyer>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreDevourerofGods>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreDragonfolly>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreDukeFishron>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreEaterofWorlds>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreEmpressofLight>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreExoMechs>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreEyeofCthulhu>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreGolem>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreHiveMind>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreKingSlime>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreLeviathanAnahita>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreMechs>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreOldDuke>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LorePerforators>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LorePlaguebringerGoliath>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LorePlantera>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LorePolterghast>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LorePrelude>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreProfanedGuardians>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreProvidence>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreQueenBee>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreQueenSlime>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreRavager>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreRequiem>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreSignus>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreSkeletron>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreSkeletronPrime>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreSlimeGod>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreStormWeaver>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreSulphurSea>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreTwins>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreUnderworld>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreWallofFlesh>(), "Lore#Lore_Items");
				ItemRedirect(ModContent.ItemType<LoreYharon>(), "Lore#Lore_Items");
				EnemyRedirect(ModContent.NPCType<KingSlimeJewelRuby>(), "Crown Jewels");
				EnemyRedirect(ModContent.NPCType<OldDukeToothBall>(), "Tooth Ball (Old Duke)");
				EnemyRedirect(ModContent.NPCType<CalamitasEnchantDemon>(), "Enchantment");
				EnemyRedirect(ModContent.NPCType<LeviathanStart>(), "%3F%3F%3F");
			}
		}
		void EnemyRedirect(int item, string pageName)
		{
			wiki.Call(2, item, pageName);
		}
		void ItemRedirect(int item, string pageName)
		{
			wiki.Call(1, item, pageName);
		}
	}

	internal static bool InAnySubworld()
	{
		if (ExternalMods.subworldLibrary == null)
		{
			return false;
		}
		Mod[] mods = ModLoader.Mods;
		foreach (Mod mod in mods)
		{
			if (!mod.Name.Equals(ExternalMods.subworldLibrary.Name) && ExternalMods.subworldLibrary.Call("AnyActive", mod) as bool? == true)
			{
				return true;
			}
		}
		return false;
	}

	private static void AddBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, Func<bool> downed, object npcTypes, Dictionary<string, object> extraInfo)
	{
		bossChecklist.Call("LogBoss", hostMod, name, difficulty, downed, npcTypes, extraInfo);
	}

	private static void AddMiniBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, Func<bool> downed, int npcType, Dictionary<string, object> extraInfo)
	{
		bossChecklist.Call("LogMiniBoss", hostMod, name, difficulty, downed, npcType, extraInfo);
	}

	private static void AddEvent(Mod bossChecklist, Mod hostMod, string name, float difficulty, Func<bool> downed, List<int> npcTypes, Dictionary<string, object> extraInfo)
	{
		bossChecklist.Call("LogEvent", hostMod, name, difficulty, downed, npcTypes, extraInfo);
	}

	private static LocalizedText GetDisplayName(string entryName)
	{
		return CalamityUtils.GetText("BossChecklistIntegration." + entryName + ".EntryName");
	}

	private static LocalizedText GetSpawnInfo(string entryName)
	{
		return CalamityUtils.GetText("BossChecklistIntegration." + entryName + ".SpawnInfo");
	}

	private static LocalizedText GetDespawnMessage(string entryName)
	{
		return CalamityUtils.GetText("BossChecklistIntegration." + entryName + ".DespawnMessage");
	}

	private static void BossChecklistSupport()
	{
		CalamityMod calamity = CalamityMod.Instance;
		Mod bossChecklist = ExternalMods.bossChecklist;
		if (bossChecklist != null)
		{
			AddCalamityBosses(bossChecklist, calamity);
			AddCalamityEvents(bossChecklist, calamity);
			RegisterCalamityExtraInfo(bossChecklist, calamity);
		}
	}

	private static void AddCalamityBosses(Mod bossChecklist, Mod calamity)
	{
		string entryName = "DesertScourge";
		BossChecklistProgressionValues.TryGetValue(entryName, out var order);
		List<int> segments = new List<int>
		{
			ModContent.NPCType<DesertScourgeHead>(),
			ModContent.NPCType<DesertScourgeBody>(),
			ModContent.NPCType<DesertScourgeTail>()
		};
		List<int> collection = new List<int>
		{
			ModContent.ItemType<DesertScourgeRelic>(),
			ModContent.ItemType<DesertScourgeTrophy>(),
			ModContent.ItemType<DesertScourgeMask>(),
			ModContent.ItemType<LoreDesertScourge>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/DesertScourge/DesertScourge_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName, order, Downed.DownedDesertScourge, segments, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName),
			["spawnInfo"] = GetSpawnInfo(entryName),
			["despawnMessage"] = GetDespawnMessage(entryName),
			["spawnItems"] = ModContent.ItemType<DesertMedallion>(),
			["collectibles"] = collection,
			["customPortrait"] = portrait
		});
		string entryName2 = "GiantClam";
		BossChecklistProgressionValues.TryGetValue(entryName2, out var order2);
		int type = ModContent.NPCType<GiantClam>();
		List<int> collection2 = new List<int>
		{
			ModContent.ItemType<GiantClamRelic>(),
			ModContent.ItemType<GiantClamTrophy>()
		};
		AddMiniBoss(bossChecklist, calamity, entryName2, order2, Downed.DownedGiantClam, type, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName2),
			["despawnMessage"] = GetDespawnMessage(entryName2),
			["collectibles"] = collection2
		});
		string entryName3 = "Crabulon";
		BossChecklistProgressionValues.TryGetValue(entryName3, out var order3);
		int type2 = ModContent.NPCType<Crabulon>();
		List<int> collection3 = new List<int>
		{
			ModContent.ItemType<CrabulonRelic>(),
			ModContent.ItemType<CrabulonTrophy>(),
			ModContent.ItemType<CrabulonMask>(),
			ModContent.ItemType<LoreCrabulon>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName3, order3, Downed.DownedCrabulon, type2, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName3),
			["despawnMessage"] = GetDespawnMessage(entryName3),
			["spawnItems"] = ModContent.ItemType<DecapoditaSprout>(),
			["collectibles"] = collection3
		});
		string entryName4 = "HiveMind";
		BossChecklistProgressionValues.TryGetValue(entryName4, out var order4);
		int type3 = ModContent.NPCType<HiveMind>();
		Func<bool> IsCorruption = () => !WorldGen.crimson || Main.drunkWorld;
		List<int> collection4 = new List<int>
		{
			ModContent.ItemType<HiveMindRelic>(),
			ModContent.ItemType<HiveMindTrophy>(),
			ModContent.ItemType<HiveMindMask>(),
			ModContent.ItemType<LoreHiveMind>(),
			ModContent.ItemType<RottingEyeball>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName4, order4, Downed.DownedHiveMind, type3, new Dictionary<string, object>
		{
			["availability"] = IsCorruption,
			["spawnInfo"] = GetSpawnInfo(entryName4),
			["despawnMessage"] = GetDespawnMessage(entryName4),
			["spawnItems"] = ModContent.ItemType<Teratoma>(),
			["collectibles"] = collection4,
			["overrideHeadTextures"] = "CalamityMod/NPCs/HiveMind/HiveMindP2_Head_Boss"
		});
		string entryName5 = "Perforators";
		BossChecklistProgressionValues.TryGetValue(entryName5, out var order5);
		int type4 = ModContent.NPCType<PerforatorHive>();
		Func<bool> IsCrimson = () => WorldGen.crimson || Main.drunkWorld;
		List<int> collection5 = new List<int>
		{
			ModContent.ItemType<PerforatorsRelic>(),
			ModContent.ItemType<PerforatorTrophy>(),
			ModContent.ItemType<PerforatorMask>(),
			ModContent.ItemType<LorePerforators>(),
			ModContent.ItemType<BloodyVein>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		float difficulty = order5;
		Func<bool> downedPerforators = Downed.DownedPerforators;
		object npcTypes = type4;
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["availability"] = IsCrimson;
		dictionary["displayName"] = GetDisplayName(entryName5);
		dictionary["spawnInfo"] = GetSpawnInfo(entryName5);
		dictionary["despawnMessage"] = GetDespawnMessage(entryName5);
		dictionary["spawnItems"] = ModContent.ItemType<BloodyWormFood>();
		dictionary["collectibles"] = collection5;
		dictionary["displayName"] = GetDisplayName(entryName5);
		AddBoss(bossChecklist, calamity, entryName5, difficulty, downedPerforators, npcTypes, dictionary);
		string entryName6 = "SlimeGod";
		BossChecklistProgressionValues.TryGetValue(entryName6, out var order6);
		List<int> bosses = new List<int>
		{
			ModContent.NPCType<SlimeGodCore>(),
			ModContent.NPCType<EbonianPaladin>(),
			ModContent.NPCType<CrimulanPaladin>()
		};
		List<int> collection6 = new List<int>
		{
			ModContent.ItemType<SlimeGodRelic>(),
			ModContent.ItemType<SlimeGodTrophy>(),
			ModContent.ItemType<SlimeGodMask>(),
			ModContent.ItemType<SlimeGodMask2>(),
			ModContent.ItemType<LoreSlimeGod>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName6, order6, Downed.DownedSlimeGod, bosses, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName6),
			["spawnInfo"] = GetSpawnInfo(entryName6),
			["despawnMessage"] = GetDespawnMessage(entryName6),
			["spawnItems"] = ModContent.ItemType<OverloadedSludge>(),
			["collectibles"] = collection6
		});
		string entryName7 = "Cryogen";
		BossChecklistProgressionValues.TryGetValue(entryName7, out var order7);
		int type5 = ModContent.NPCType<Cryogen>();
		List<int> collection7 = new List<int>
		{
			ModContent.ItemType<CryogenRelic>(),
			ModContent.ItemType<CryogenTrophy>(),
			ModContent.ItemType<CryogenMask>(),
			ModContent.ItemType<LoreArchmage>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName7, order7, Downed.DownedCryogen, type5, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName7),
			["despawnMessage"] = GetDespawnMessage(entryName7),
			["spawnItems"] = ModContent.ItemType<CryoKey>(),
			["collectibles"] = collection7,
			["overrideHeadTextures"] = "CalamityMod/NPCs/Cryogen/Cryogen_Phase1_Head_Boss"
		});
		string entryName8 = "AquaticScourge";
		BossChecklistProgressionValues.TryGetValue(entryName8, out var order8);
		List<int> segments2 = new List<int>
		{
			ModContent.NPCType<AquaticScourgeHead>(),
			ModContent.NPCType<AquaticScourgeBody>(),
			ModContent.NPCType<AquaticScourgeBodyAlt>(),
			ModContent.NPCType<AquaticScourgeTail>()
		};
		List<int> collection8 = new List<int>
		{
			ModContent.ItemType<AquaticScourgeRelic>(),
			ModContent.ItemType<AquaticScourgeTrophy>(),
			ModContent.ItemType<AquaticScourgeMask>(),
			ModContent.ItemType<LoreAquaticScourge>(),
			ModContent.ItemType<LoreSulphurSea>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait2 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/AquaticScourge/AquaticScourge_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName8, order8, Downed.DownedAquaticScourge, segments2, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName8),
			["spawnInfo"] = GetSpawnInfo(entryName8),
			["despawnMessage"] = GetDespawnMessage(entryName8),
			["spawnItems"] = ModContent.ItemType<Seafood>(),
			["collectibles"] = collection8,
			["customPortrait"] = portrait2
		});
		string entryName9 = "CragmawMire";
		BossChecklistProgressionValues.TryGetValue(entryName9, out var order9);
		int type6 = ModContent.NPCType<CragmawMire>();
		List<int> collection9 = new List<int>
		{
			ModContent.ItemType<CragmawMireRelic>(),
			ModContent.ItemType<CragmawMireTrophy>()
		};
		AddMiniBoss(bossChecklist, calamity, entryName9, order9, Downed.DownedCragmawMire, type6, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName9),
			["despawnMessage"] = GetDespawnMessage(entryName9),
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = collection9,
			["availability"] = Downed.DownedAcidRainInitial
		});
		string entryName10 = "BrimstoneElemental";
		BossChecklistProgressionValues.TryGetValue(entryName10, out var order10);
		int type7 = ModContent.NPCType<global::CalamityMod.NPCs.BrimstoneElemental.BrimstoneElemental>();
		List<int> collection10 = new List<int>
		{
			ModContent.ItemType<BrimstoneElementalRelic>(),
			ModContent.ItemType<BrimstoneElementalTrophy>(),
			ModContent.ItemType<BrimstoneElementalMask>(),
			ModContent.ItemType<LoreAzafure>(),
			ModContent.ItemType<LoreBrimstoneElemental>(),
			ModContent.ItemType<CharredRelic>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName10, order10, Downed.DownedBrimstoneElemental, type7, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName10),
			["despawnMessage"] = GetDespawnMessage(entryName10),
			["spawnItems"] = ModContent.ItemType<CharredIdol>(),
			["collectibles"] = collection10
		});
		string entryName11 = "CalamitasClone";
		BossChecklistProgressionValues.TryGetValue(entryName11, out var order11);
		int type8 = ModContent.NPCType<CalamitasClone>();
		List<int> collection11 = new List<int>
		{
			ModContent.ItemType<CalamitasCloneRelic>(),
			ModContent.ItemType<CalamitasCloneTrophy>(),
			ModContent.ItemType<CataclysmTrophy>(),
			ModContent.ItemType<CatastropheTrophy>(),
			ModContent.ItemType<CalamitasCloneMask>(),
			ModContent.ItemType<HoodOfCalamity>(),
			ModContent.ItemType<RobesOfCalamity>(),
			ModContent.ItemType<LoreCalamitasClone>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName11, order11, Downed.DownedCalClone, type8, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName11),
			["despawnMessage"] = GetDespawnMessage(entryName11),
			["spawnItems"] = ModContent.ItemType<EyeofDesolation>(),
			["collectibles"] = collection11
		});
		string entryName12 = "GreatSandShark";
		BossChecklistProgressionValues.TryGetValue(entryName12, out var order12);
		int type9 = ModContent.NPCType<GreatSandShark>();
		List<int> collection12 = new List<int>
		{
			ModContent.ItemType<GreatSandSharkRelic>(),
			ModContent.ItemType<GreatSandSharkTrophy>(),
			3796
		};
		AddMiniBoss(bossChecklist, calamity, entryName12, order12, Downed.DownedGSS, type9, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName12),
			["despawnMessage"] = GetDespawnMessage(entryName12),
			["spawnItems"] = ModContent.ItemType<SandstormsCore>(),
			["collectibles"] = collection12
		});
		string entryName13 = "Leviathan";
		BossChecklistProgressionValues.TryGetValue(entryName13, out var order13);
		List<int> bosses2 = new List<int>
		{
			ModContent.NPCType<Leviathan>(),
			ModContent.NPCType<Anahita>()
		};
		List<int> collection13 = new List<int>
		{
			ModContent.ItemType<LeviathanAnahitaRelic>(),
			ModContent.ItemType<LeviathanTrophy>(),
			ModContent.ItemType<AnahitaTrophy>(),
			ModContent.ItemType<LeviathanMask>(),
			ModContent.ItemType<AnahitaMask>(),
			ModContent.ItemType<LoreAbyss>(),
			ModContent.ItemType<LoreLeviathanAnahita>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait3 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/Leviathan/AnahitaLevi_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName13, order13, Downed.DownedLeviathan, bosses2, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName13),
			["spawnInfo"] = GetSpawnInfo(entryName13),
			["despawnMessage"] = GetDespawnMessage(entryName13),
			["spawnItems"] = ModContent.ItemType<NaiadsWarhorn>(),
			["collectibles"] = collection13,
			["customPortrait"] = portrait3
		});
		string entryName14 = "AstrumAureus";
		BossChecklistProgressionValues.TryGetValue(entryName14, out var order14);
		int type10 = ModContent.NPCType<AstrumAureus>();
		List<int> collection14 = new List<int>
		{
			ModContent.ItemType<AstrumAureusRelic>(),
			ModContent.ItemType<AstrumAureusTrophy>(),
			ModContent.ItemType<AstrumAureusMask>(),
			ModContent.ItemType<LoreAstrumAureus>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName14, order14, Downed.DownedAureus, type10, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName14),
			["despawnMessage"] = GetDespawnMessage(entryName14),
			["spawnItems"] = ModContent.ItemType<AstralChunk>(),
			["collectibles"] = collection14
		});
		string entryName15 = "PlaguebringerGoliath";
		BossChecklistProgressionValues.TryGetValue(entryName15, out var order15);
		int type11 = ModContent.NPCType<PlaguebringerGoliath>();
		List<int> collection15 = new List<int>
		{
			ModContent.ItemType<PlaguebringerGoliathRelic>(),
			ModContent.ItemType<PlaguebringerGoliathTrophy>(),
			ModContent.ItemType<PlaguebringerGoliathMask>(),
			ModContent.ItemType<LorePlaguebringerGoliath>(),
			ModContent.ItemType<PlagueCaller>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait4 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliath_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName15, order15, Downed.DownedPBG, type11, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName15),
			["despawnMessage"] = GetDespawnMessage(entryName15),
			["spawnItems"] = ModContent.ItemType<Abombination>(),
			["collectibles"] = collection15,
			["customPortrait"] = portrait4
		});
		string entryName16 = "Ravager";
		BossChecklistProgressionValues.TryGetValue(entryName16, out var order16);
		List<int> segments3 = new List<int>
		{
			ModContent.NPCType<RavagerBody>(),
			ModContent.NPCType<RavagerClawLeft>(),
			ModContent.NPCType<RavagerClawRight>(),
			ModContent.NPCType<RavagerHead>(),
			ModContent.NPCType<RavagerLegLeft>(),
			ModContent.NPCType<RavagerLegRight>()
		};
		List<int> collection16 = new List<int>
		{
			ModContent.ItemType<RavagerRelic>(),
			ModContent.ItemType<RavagerTrophy>(),
			ModContent.ItemType<RavagerMask>(),
			ModContent.ItemType<LoreRavager>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait5 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/Ravager/Ravager_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName16, order16, Downed.DownedRavager, segments3, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName16),
			["spawnInfo"] = GetSpawnInfo(entryName16),
			["despawnMessage"] = GetDespawnMessage(entryName16),
			["spawnItems"] = ModContent.ItemType<DeathWhistle>(),
			["collectibles"] = collection16,
			["customPortrait"] = portrait5
		});
		string entryName17 = "AstrumDeus";
		BossChecklistProgressionValues.TryGetValue(entryName17, out var order17);
		List<int> segments4 = new List<int>
		{
			ModContent.NPCType<AstrumDeusHead>(),
			ModContent.NPCType<AstrumDeusBody>(),
			ModContent.NPCType<AstrumDeusTail>()
		};
		List<int> summons = new List<int>
		{
			ModContent.ItemType<TitanHeart>(),
			ModContent.ItemType<Starcore>()
		};
		List<int> collection17 = new List<int>
		{
			ModContent.ItemType<AstrumDeusRelic>(),
			ModContent.ItemType<AstrumDeusTrophy>(),
			ModContent.ItemType<AstrumDeusMask>(),
			ModContent.ItemType<LoreAstrumDeus>(),
			ModContent.ItemType<LoreAstralInfection>(),
			ModContent.ItemType<ChromaticOrb>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait6 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/AstrumDeus/AstrumDeus_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName17, order17, Downed.DownedDeus, segments4, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName17),
			["spawnInfo"] = GetSpawnInfo(entryName17),
			["despawnMessage"] = GetDespawnMessage(entryName17),
			["spawnItems"] = summons,
			["collectibles"] = collection17,
			["customPortrait"] = portrait6
		});
		string entryName18 = "ProfanedGuardians";
		BossChecklistProgressionValues.TryGetValue(entryName18, out var order18);
		int type12 = ModContent.NPCType<ProfanedGuardianCommander>();
		List<int> collection18 = new List<int>
		{
			ModContent.ItemType<ProfanedGuardiansRelic>(),
			ModContent.ItemType<ProfanedGuardianTrophy>(),
			ModContent.ItemType<ProfanedGuardianMask>(),
			ModContent.ItemType<LoreProfanedGuardians>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait7 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/ProfanedGuardians/ProfanedGuardians_BossChecklist", (AssetRequestMode)2).Value;
			float num = 0.7f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddBoss(bossChecklist, calamity, entryName18, order18, Downed.DownedGuardians, type12, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName18),
			["spawnInfo"] = GetSpawnInfo(entryName18),
			["despawnMessage"] = GetDespawnMessage(entryName18),
			["spawnItems"] = ModContent.ItemType<ProfanedShard>(),
			["collectibles"] = collection18,
			["customPortrait"] = portrait7,
			["overrideHeadTextures"] = "CalamityMod/NPCs/ProfanedGuardians/ProfanedGuardianCommander_Head_Boss"
		});
		string entryName19 = "Dragonfolly";
		BossChecklistProgressionValues.TryGetValue(entryName19, out var order19);
		int type13 = ModContent.NPCType<Dragonfolly>();
		List<int> collection19 = new List<int>
		{
			ModContent.ItemType<DragonfollyRelic>(),
			ModContent.ItemType<DragonfollyTrophy>(),
			ModContent.ItemType<BumblefuckMask>(),
			ModContent.ItemType<LoreDragonfolly>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName19, order19, Downed.DownedDragonfolly, type13, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName19),
			["despawnMessage"] = GetDespawnMessage(entryName19),
			["spawnItems"] = ModContent.ItemType<ExoticPheromones>(),
			["collectibles"] = collection19
		});
		string entryName20 = "Providence";
		BossChecklistProgressionValues.TryGetValue(entryName20, out var order20);
		int type14 = ModContent.NPCType<Providence>();
		List<int> collection20 = new List<int>
		{
			ModContent.ItemType<ProvidenceRelic>(),
			ModContent.ItemType<ProvidenceTrophy>(),
			ModContent.ItemType<ProvidenceMask>(),
			ModContent.ItemType<LoreProvidence>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait8 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName20, order20, Downed.DownedProvidence, type14, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName20),
			["despawnMessage"] = GetDespawnMessage(entryName20),
			["spawnItems"] = ModContent.ItemType<ProfanedCore>(),
			["collectibles"] = collection20,
			["customPortrait"] = portrait8
		});
		string entryName21 = "CeaselessVoid";
		BossChecklistProgressionValues.TryGetValue(entryName21, out var order21);
		List<int> bosses3 = new List<int>
		{
			ModContent.NPCType<CeaselessVoid>(),
			ModContent.NPCType<DarkEnergy>()
		};
		List<int> collection21 = new List<int>
		{
			ModContent.ItemType<CeaselessVoidRelic>(),
			ModContent.ItemType<CeaselessVoidTrophy>(),
			ModContent.ItemType<CeaselessVoidMask>(),
			ModContent.ItemType<AncientGodSlayerHelm>(),
			ModContent.ItemType<AncientGodSlayerChestplate>(),
			ModContent.ItemType<AncientGodSlayerLeggings>(),
			ModContent.ItemType<LoreCeaselessVoid>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName21, order21, Downed.DownedCeaselessVoid, bosses3, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName21),
			["spawnInfo"] = GetSpawnInfo(entryName21),
			["despawnMessage"] = GetDespawnMessage(entryName21),
			["spawnItems"] = ModContent.ItemType<MarkofProvidence>(),
			["collectibles"] = collection21
		});
		string entryName22 = "StormWeaver";
		BossChecklistProgressionValues.TryGetValue(entryName22, out var order22);
		List<int> segments5 = new List<int>
		{
			ModContent.NPCType<StormWeaverHead>(),
			ModContent.NPCType<StormWeaverBody>(),
			ModContent.NPCType<StormWeaverTail>()
		};
		List<int> collection22 = new List<int>
		{
			ModContent.ItemType<WeaverTrophy>(),
			ModContent.ItemType<StormWeaverMask>(),
			ModContent.ItemType<AncientGodSlayerHelm>(),
			ModContent.ItemType<AncientGodSlayerChestplate>(),
			ModContent.ItemType<AncientGodSlayerLeggings>(),
			ModContent.ItemType<LoreStormWeaver>(),
			ModContent.ItemType<LittleLight>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait9 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/StormWeaver/StormWeaver_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName22, order22, Downed.DownedStormWeaver, segments5, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName22),
			["spawnInfo"] = GetSpawnInfo(entryName22),
			["despawnMessage"] = GetDespawnMessage(entryName22),
			["spawnItems"] = ModContent.ItemType<MarkofProvidence>(),
			["collectibles"] = collection22,
			["customPortrait"] = portrait9,
			["overrideHeadTextures"] = "CalamityMod/NPCs/StormWeaver/StormWeaverHead_Head_Boss"
		});
		string entryName23 = "Signus";
		BossChecklistProgressionValues.TryGetValue(entryName23, out var order23);
		int type15 = ModContent.NPCType<Signus>();
		List<int> collection23 = new List<int>
		{
			ModContent.ItemType<SignusRelic>(),
			ModContent.ItemType<SignusTrophy>(),
			ModContent.ItemType<SignusMask>(),
			ModContent.ItemType<AncientGodSlayerHelm>(),
			ModContent.ItemType<AncientGodSlayerChestplate>(),
			ModContent.ItemType<AncientGodSlayerLeggings>(),
			ModContent.ItemType<LoreSignus>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName23, order23, Downed.DownedSignus, type15, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName23),
			["spawnInfo"] = GetSpawnInfo(entryName23),
			["despawnMessage"] = GetDespawnMessage(entryName23),
			["spawnItems"] = ModContent.ItemType<MarkofProvidence>(),
			["collectibles"] = collection23
		});
		string entryName24 = "Polterghast";
		BossChecklistProgressionValues.TryGetValue(entryName24, out var order24);
		List<int> bosses4 = new List<int>
		{
			ModContent.NPCType<Polterghast>(),
			ModContent.NPCType<PolterPhantom>()
		};
		List<int> collection24 = new List<int>
		{
			ModContent.ItemType<PolterghastRelic>(),
			ModContent.ItemType<PolterghastTrophy>(),
			ModContent.ItemType<PolterghastMask>(),
			ModContent.ItemType<LorePolterghast>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName24, order24, Downed.DownedPolterghast, bosses4, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName24),
			["spawnInfo"] = GetSpawnInfo(entryName24),
			["despawnMessage"] = GetDespawnMessage(entryName24),
			["spawnItems"] = ModContent.ItemType<NecroplasmicBeacon>(),
			["collectibles"] = collection24
		});
		string entryName25 = "Mauler";
		BossChecklistProgressionValues.TryGetValue(entryName25, out var order25);
		int type16 = ModContent.NPCType<Mauler>();
		List<int> collection25 = new List<int>
		{
			ModContent.ItemType<MaulerRelic>(),
			ModContent.ItemType<MaulerTrophy>()
		};
		AddMiniBoss(bossChecklist, calamity, entryName25, order25, Downed.DownedMauler, type16, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName25),
			["despawnMessage"] = GetDespawnMessage(entryName25),
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = collection25,
			["availability"] = Downed.DownedAcidRainHardmode
		});
		string entryName26 = "NuclearTerror";
		BossChecklistProgressionValues.TryGetValue(entryName26, out var order26);
		int type17 = ModContent.NPCType<NuclearTerror>();
		List<int> collection26 = new List<int>
		{
			ModContent.ItemType<NuclearTerrorRelic>(),
			ModContent.ItemType<NuclearTerrorTrophy>()
		};
		AddMiniBoss(bossChecklist, calamity, entryName26, order26, Downed.DownedNuclearTerror, type17, new Dictionary<string, object>
		{
			["spawnInfo"] = GetSpawnInfo(entryName26),
			["despawnMessage"] = GetDespawnMessage(entryName26),
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = collection26,
			["availability"] = Downed.DownedAcidRainHardmode
		});
		string entryName27 = "OldDuke";
		BossChecklistProgressionValues.TryGetValue(entryName27, out var order27);
		int type18 = ModContent.NPCType<OldDuke>();
		List<int> collection27 = new List<int>
		{
			ModContent.ItemType<OldDukeRelic>(),
			ModContent.ItemType<OldDukeTrophy>(),
			ModContent.ItemType<OldDukeMask>(),
			ModContent.ItemType<LoreOldDuke>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName27, order27, Downed.DownedOldDuke, type18, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName27),
			["spawnInfo"] = GetSpawnInfo(entryName27),
			["despawnMessage"] = GetDespawnMessage(entryName27),
			["spawnItems"] = ModContent.ItemType<BloodwormItem>(),
			["collectibles"] = collection27
		});
		string entryName28 = "DevourerofGods";
		BossChecklistProgressionValues.TryGetValue(entryName28, out var order28);
		int type19 = ModContent.NPCType<DevourerofGodsHead>();
		List<int> collection28 = new List<int>
		{
			ModContent.ItemType<DevourerOfGodsRelic>(),
			ModContent.ItemType<DevourerofGodsTrophy>(),
			ModContent.ItemType<DevourerofGodsMask>(),
			ModContent.ItemType<LoreDevourerofGods>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait10 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/DevourerofGods_BossChecklist", (AssetRequestMode)2).Value;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)(((Rectangle)(ref rect)).Center.X - value.Width / 2), (float)(((Rectangle)(ref rect)).Center.Y - value.Height / 2));
			sb.Draw(value, val, color);
		};
		AddBoss(bossChecklist, calamity, entryName28, order28, Downed.DownedDoG, type19, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName28),
			["spawnInfo"] = GetSpawnInfo(entryName28),
			["despawnMessage"] = GetDespawnMessage(entryName28),
			["spawnItems"] = ModContent.ItemType<CosmicWorm>(),
			["collectibles"] = collection28,
			["customPortrait"] = portrait10,
			["overrideHeadTextures"] = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsHead_Head_Boss"
		});
		string entryName29 = "Yharon";
		BossChecklistProgressionValues.TryGetValue(entryName29, out var order29);
		int type20 = ModContent.NPCType<Yharon>();
		List<int> collection29 = new List<int>
		{
			ModContent.ItemType<YharonRelic>(),
			ModContent.ItemType<YharonTrophy>(),
			ModContent.ItemType<YharonMask>(),
			ModContent.ItemType<LoreYharon>(),
			ModContent.ItemType<ForgottenDragonEgg>(),
			ModContent.ItemType<McNuggets>(),
			ModContent.ItemType<FoxDrive>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName29, order29, Downed.DownedYharon, type20, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName29),
			["spawnInfo"] = GetSpawnInfo(entryName29),
			["despawnMessage"] = GetDespawnMessage(entryName29),
			["spawnItems"] = ModContent.ItemType<YharonEgg>(),
			["collectibles"] = collection29
		});
		string entryName30 = "ExoMechs";
		BossChecklistProgressionValues.TryGetValue(entryName30, out var order30);
		List<int> bosses5 = new List<int>
		{
			ModContent.NPCType<Apollo>(),
			ModContent.NPCType<AresBody>(),
			ModContent.NPCType<Artemis>(),
			ModContent.NPCType<ThanatosHead>()
		};
		List<int> collection30 = new List<int>
		{
			ModContent.ItemType<DraedonRelic>(),
			ModContent.ItemType<AresTrophy>(),
			ModContent.ItemType<ThanatosTrophy>(),
			ModContent.ItemType<ArtemisTrophy>(),
			ModContent.ItemType<ApolloTrophy>(),
			ModContent.ItemType<DraedonMask>(),
			ModContent.ItemType<AresMask>(),
			ModContent.ItemType<ThanatosMask>(),
			ModContent.ItemType<ArtemisMask>(),
			ModContent.ItemType<ApolloMask>(),
			ModContent.ItemType<LoreExoMechs>(),
			ModContent.ItemType<LoreCynosure>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait11 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/ExoMechs_BossChecklist", (AssetRequestMode)2).Value;
			float num = 0.7f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddBoss(bossChecklist, calamity, entryName30, order30, Downed.DownedExoMechs, bosses5, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName30),
			["spawnInfo"] = GetSpawnInfo(entryName30),
			["despawnMessage"] = GetDespawnMessage(entryName30),
			["collectibles"] = collection30,
			["customPortrait"] = portrait11
		});
		string entryName31 = "Calamitas";
		BossChecklistProgressionValues.TryGetValue(entryName31, out var order31);
		int type21 = ModContent.NPCType<SupremeCalamitas>();
		List<int> summons2 = new List<int>
		{
			ModContent.ItemType<AshesofCalamity>(),
			ModContent.ItemType<CeremonialUrn>()
		};
		List<int> collection31 = new List<int>
		{
			ModContent.ItemType<CalamitasRelic>(),
			ModContent.ItemType<SupremeCalamitasTrophy>(),
			ModContent.ItemType<SupremeCataclysmTrophy>(),
			ModContent.ItemType<SupremeCatastropheTrophy>(),
			ModContent.ItemType<AshenHorns>(),
			ModContent.ItemType<SCalMask>(),
			ModContent.ItemType<SCalRobes>(),
			ModContent.ItemType<SCalBoots>(),
			ModContent.ItemType<LoreCalamitas>(),
			ModContent.ItemType<LoreCynosure>(),
			ModContent.ItemType<BrimstoneJewel>(),
			ModContent.ItemType<Levi>(),
			ModContent.ItemType<ThankYouPainting>()
		};
		AddBoss(bossChecklist, calamity, entryName31, order31, Downed.DownedCalamitas, type21, new Dictionary<string, object>
		{
			["displayName"] = GetDisplayName(entryName31),
			["spawnInfo"] = GetSpawnInfo(entryName31),
			["despawnMessage"] = GetDespawnMessage(entryName31),
			["spawnItems"] = summons2,
			["collectibles"] = collection31,
			["overrideHeadTextures"] = "CalamityMod/NPCs/SupremeCalamitas/HoodedHeadIcon"
		});
	}

	private static void AddCalamityEvents(Mod bossChecklist, Mod calamity)
	{
		string entryName = "AcidRainT1";
		BossChecklistProgressionValues.TryGetValue(entryName, out var order);
		List<int> enemies = AcidRainEvent.PossibleEnemiesPreHM.Select((KeyValuePair<int, AcidRainSpawnData> enemy) => enemy.Key).ToList();
		Action<SpriteBatch, Rectangle, Color> portrait = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Events/AcidRainT1_BossChecklist", (AssetRequestMode)2).Value;
			float num = 1f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddEvent(bossChecklist, calamity, entryName, order, Downed.DownedAcidRainInitial, enemies, new Dictionary<string, object>
		{
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = ModContent.ItemType<RadiatingCrystal>(),
			["customPortrait"] = portrait,
			["overrideHeadTextures"] = "CalamityMod/UI/MiscTextures/AcidRainIcon"
		});
		string entryName2 = "AcidRainT2";
		BossChecklistProgressionValues.TryGetValue(entryName2, out var order2);
		List<int> enemies2 = AcidRainEvent.PossibleEnemiesAS.Select((KeyValuePair<int, AcidRainSpawnData> enemy) => enemy.Key).ToList();
		enemies2.Add(ModContent.NPCType<IrradiatedSlime>());
		enemies2.AddRange(AcidRainEvent.PossibleMinibossesAS.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key));
		List<int> collection = new List<int>
		{
			ModContent.ItemType<CragmawMireRelic>(),
			ModContent.ItemType<CragmawMireTrophy>(),
			ModContent.ItemType<RadiatingCrystal>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait2 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Events/AcidRainT2_BossChecklist", (AssetRequestMode)2).Value;
			float num = 0.9f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddEvent(bossChecklist, calamity, entryName2, order2, Downed.DownedAcidRainHardmode, enemies2, new Dictionary<string, object>
		{
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = collection,
			["customPortrait"] = portrait2,
			["overrideHeadTextures"] = "CalamityMod/UI/MiscTextures/AcidRainIcon",
			["availability"] = Downed.DownedAcidRainInitial
		});
		string entryName3 = "AcidRainT3";
		BossChecklistProgressionValues.TryGetValue(entryName3, out var order3);
		List<int> enemies3 = AcidRainEvent.PossibleEnemiesPolter.Select((KeyValuePair<int, AcidRainSpawnData> enemy) => enemy.Key).ToList();
		enemies3.AddRange(AcidRainEvent.PossibleMinibossesPolter.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key));
		List<int> collection2 = new List<int>
		{
			ModContent.ItemType<CragmawMireRelic>(),
			ModContent.ItemType<CragmawMireTrophy>(),
			ModContent.ItemType<MaulerRelic>(),
			ModContent.ItemType<MaulerTrophy>(),
			ModContent.ItemType<NuclearTerrorRelic>(),
			ModContent.ItemType<NuclearTerrorTrophy>(),
			ModContent.ItemType<RadiatingCrystal>()
		};
		Action<SpriteBatch, Rectangle, Color> portrait3 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Events/AcidRainT3_BossChecklist", (AssetRequestMode)2).Value;
			float num = 0.9f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddEvent(bossChecklist, calamity, entryName3, order3, Downed.DownedOldDuke, enemies3, new Dictionary<string, object>
		{
			["spawnItems"] = ModContent.ItemType<CausticTear>(),
			["collectibles"] = collection2,
			["customPortrait"] = portrait3,
			["overrideHeadTextures"] = "CalamityMod/UI/MiscTextures/AcidRainIcon",
			["availability"] = Downed.DownedAcidRainHardmode
		});
		string entryName4 = "BossRush";
		BossChecklistProgressionValues.TryGetValue(entryName4, out var order4);
		List<int> enemies4 = new List<int> { 0 };
		Action<SpriteBatch, Rectangle, Color> portrait4 = delegate(SpriteBatch sb, Rectangle rect, Color color)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Skies/XerocEye", (AssetRequestMode)2).Value;
			float num = 0.5f;
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector((float)((Rectangle)(ref rect)).Center.X - (float)value.Width * num / 2f, (float)((Rectangle)(ref rect)).Center.Y - (float)value.Height * num / 2f);
			sb.Draw(value, val, (Rectangle?)null, color, 0f, Vector2.Zero, num, (SpriteEffects)0, 0f);
		};
		AddEvent(bossChecklist, calamity, entryName4, order4, Downed.DownedBossRush, enemies4, new Dictionary<string, object>
		{
			["spawnItems"] = ModContent.ItemType<Terminus>(),
			["collectibles"] = ModContent.ItemType<Rock>(),
			["customPortrait"] = portrait4,
			["overrideHeadTextures"] = "CalamityMod/UI/MiscTextures/BossRushIcon"
		});
	}

	private static void RegisterCalamityExtraInfo(Mod bossChecklist, Mod calamity)
	{
		bossChecklist.Call("SubmitEntrySpawnItems", calamity, new Dictionary<string, object>
		{
			{
				"Terraria Plantera",
				ModContent.ItemType<Portabulb>()
			},
			{
				"Terraria CultistBoss",
				ModContent.ItemType<EidolonTablet>()
			}
		});
		bossChecklist.Call("SubmitEntryCollectibles", calamity, new Dictionary<string, object>
		{
			{
				"Terraria KingSlime",
				new List<int>
				{
					ModContent.ItemType<LoreKingSlime>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria EyeofCthulhu",
				new List<int>
				{
					ModContent.ItemType<LoreEyeofCthulhu>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria EaterofWorlds",
				new List<int>
				{
					ModContent.ItemType<LoreEaterofWorlds>(),
					ModContent.ItemType<LoreCorruption>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria BrainofCthulhu",
				new List<int>
				{
					ModContent.ItemType<LoreBrainofCthulhu>(),
					ModContent.ItemType<LoreCrimson>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria QueenBee",
				new List<int>
				{
					ModContent.ItemType<LoreQueenBee>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria Skeletron",
				new List<int>
				{
					ModContent.ItemType<LoreSkeletron>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria WallofFlesh",
				new List<int>
				{
					ModContent.ItemType<LoreWallofFlesh>(),
					ModContent.ItemType<LoreUnderworld>(),
					ModContent.ItemType<HermitsBoxofOneHundredMedicines>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria QueenSlimeBoss",
				new List<int>
				{
					ModContent.ItemType<LoreQueenSlime>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria TheTwins",
				new List<int>
				{
					ModContent.ItemType<LoreTwins>(),
					ModContent.ItemType<LoreMechs>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria TheDestroyer",
				new List<int>
				{
					ModContent.ItemType<LoreDestroyer>(),
					ModContent.ItemType<LoreMechs>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria SkeletronPrime",
				new List<int>
				{
					ModContent.ItemType<LoreSkeletronPrime>(),
					ModContent.ItemType<LoreMechs>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria Plantera",
				new List<int>
				{
					ModContent.ItemType<LorePlantera>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria Golem",
				new List<int>
				{
					ModContent.ItemType<LoreGolem>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria HallowBoss",
				new List<int>
				{
					ModContent.ItemType<LoreEmpressofLight>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria DukeFishron",
				new List<int>
				{
					ModContent.ItemType<LoreDukeFishron>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria CultistBoss",
				new List<int>
				{
					ModContent.ItemType<LorePrelude>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			},
			{
				"Terraria MoonLord",
				new List<int>
				{
					ModContent.ItemType<LoreRequiem>(),
					ModContent.ItemType<ThankYouPainting>()
				}
			}
		});
	}

	private static void FargosSupport()
	{
		Mod fargos = ExternalMods.fargos;
		if (fargos != null)
		{
			int rogueItem = ModContent.ItemType<WulfrumKnife>();
			DamageClass rogueDamageClass = ModContent.GetInstance<RogueDamageClass>();
			Func<string> rogueDamage = () => $"Rogue Damage: {Damage(rogueDamageClass)}%";
			Func<string> rogueCrit = () => $"Rogue Critical: {Crit(rogueDamageClass)}%";
			fargos.Call("AddStat", rogueItem, rogueDamage);
			fargos.Call("AddStat", rogueItem, rogueCrit);
			fargos.Call("AbominationnClearEvents", "CalamityMod", AcidRainEvent.AcidRainEventIsOngoing, true);
			AddToMutantShop("OldDuke", "BloodwormItem", Downed.DownedOldDuke, Item.buyPrice(2));
		}
		void AddToMutantShop(string bossName, string summonItemName, Func<bool> downed, int price)
		{
			BossChecklistProgressionValues.TryGetValue(bossName, out var order);
			fargos.Call("AddSummon", order, "CalamityMod", summonItemName, downed, price);
		}
		static int Crit(DamageClass damageClass)
		{
			return (int)Main.LocalPlayer.GetTotalCritChance(damageClass);
		}
		static double Damage(DamageClass damageClass)
		{
			return Math.Round(Main.LocalPlayer.GetTotalDamage(damageClass).Additive * Main.LocalPlayer.GetTotalDamage(damageClass).Multiplicative * 100f - 100f);
		}
	}

	private static void DialogueTweakSupport()
	{
		ExternalMods.dialogueTweak?.Call("ReplaceShopButtonIcon", ModContent.NPCType<BrimstoneWitch>(), "Head");
	}

	private static void SummonersAssociationSupport()
	{
		Mod sAssociation = ExternalMods.summonersAssociation;
		if (sAssociation != null)
		{
			RegisterSummon(ModContent.ItemType<WulfrumController>(), ModContent.BuffType<WulfrumDroidBuff>(), ModContent.ProjectileType<WulfrumDroid>());
			RegisterSummon(ModContent.ItemType<SunSpiritStaff>(), ModContent.BuffType<SolarSpirit>(), ModContent.ProjectileType<SunSpiritMinion>());
			RegisterSummon(ModContent.ItemType<FrostBlossomStaff>(), ModContent.BuffType<FrostBlossomBuff>(), ModContent.ProjectileType<FrostBlossom>());
			RegisterSummon(ModContent.ItemType<BelladonnaSpiritStaff>(), ModContent.BuffType<BelladonnaSpiritBuff>(), ModContent.ProjectileType<BelladonnaSpirit>());
			RegisterSummon(ModContent.ItemType<StormjawStaff>(), ModContent.BuffType<BabyStormlionBuff>(), ModContent.ProjectileType<StormjawBaby>());
			RegisterSummon(ModContent.ItemType<BrittleStarStaff>(), ModContent.BuffType<BrittleStar>(), ModContent.ProjectileType<BrittleStarMinion>());
			RegisterSummon(ModContent.ItemType<EnchantedConch>(), ModContent.BuffType<HermitCrab>(), ModContent.ProjectileType<HermitCrabMinion>());
			RegisterSummon(ModContent.ItemType<DeathstareRod>(), ModContent.BuffType<MiniatureEyeofCthulhu>(), ModContent.ProjectileType<DeathstareEyeball>());
			RegisterSummon(ModContent.ItemType<PuffShroom>(), ModContent.BuffType<PuffWarriorBuff>(), ModContent.ProjectileType<PuffWarrior>());
			RegisterSummon(ModContent.ItemType<VileFeeder>(), ModContent.BuffType<VileFeederBuff>(), ModContent.ProjectileType<VileFeederSummon>());
			RegisterSummon(ModContent.ItemType<ScabRipper>(), ModContent.BuffType<BabyBloodCrawlerBuff>(), ModContent.ProjectileType<BabyBloodCrawler>());
			RegisterSummon(ModContent.ItemType<CinderBlossomStaff>(), ModContent.BuffType<CinderBlossomBuff>(), ModContent.ProjectileType<CinderBlossom>());
			RegisterSummon(ModContent.ItemType<DankStaff>(), ModContent.BuffType<DankCreeperBuff>(), ModContent.ProjectileType<DankCreeperMinion>());
			RegisterSummon(ModContent.ItemType<AqueousHunterDrone>(), ModContent.BuffType<AqueousHunterDroneBuff>(), ModContent.ProjectileType<AqueousHunterDroneSummon>());
			RegisterSummon(ModContent.ItemType<HerringStaff>(), ModContent.BuffType<Herring>(), ModContent.ProjectileType<HerringMinion>());
			RegisterSummon(ModContent.ItemType<EyeOfNight>(), ModContent.BuffType<EyeOfNightBuff>(), ModContent.ProjectileType<EyeOfNightSummon>());
			RegisterSummon(ModContent.ItemType<FleshOfInfidelity>(), ModContent.BuffType<FleshBallBuff>(), ModContent.ProjectileType<FleshBallMinion>());
			RegisterSummon(ModContent.ItemType<CorroslimeStaff>(), ModContent.BuffType<Corroslime>(), ModContent.ProjectileType<CorroslimeMinion>());
			RegisterSummon(ModContent.ItemType<CrimslimeStaff>(), ModContent.BuffType<Crimslime>(), ModContent.ProjectileType<CrimslimeMinion>());
			RegisterSummon(ModContent.ItemType<BlackHawkRemote>(), ModContent.BuffType<BlackHawkBuff>(), ModContent.ProjectileType<BlackHawkSummon>());
			RegisterSummon(ModContent.ItemType<CausticStaff>(), ModContent.BuffType<CausticStaffBuff>(), ModContent.ProjectileType<CausticStaffSummon>());
			RegisterSummon(ModContent.ItemType<AncientIceChunk>(), ModContent.BuffType<IceClasperBuff>(), ModContent.ProjectileType<IceClasperMinion>());
			RegisterSummon(ModContent.ItemType<ShellfishStaff>(), ModContent.BuffType<ShellfishBuff>(), ModContent.ProjectileType<Shellfish>());
			RegisterSummon(ModContent.ItemType<HauntedScroll>(), ModContent.BuffType<HauntedDishesBuff>(), ModContent.ProjectileType<HauntedDishes>());
			RegisterSummon(ModContent.ItemType<ForgottenApexWand>(), ModContent.BuffType<AncientMineralSharkBuff>(), ModContent.ProjectileType<ApexShark>());
			RegisterSummon(ModContent.ItemType<DaedalusGolemStaff>(), ModContent.BuffType<DaedalusGolemBuff>(), ModContent.ProjectileType<DaedalusGolem>());
			RegisterSummon(ModContent.ItemType<GlacialEmbrace>(), ModContent.BuffType<GlacialEmbraceBuff>(), ModContent.ProjectileType<GlacialEmbracePointyThing>());
			RegisterSummon(ModContent.ItemType<MountedScanner>(), ModContent.BuffType<MountedScannerBuff>(), ModContent.ProjectileType<MountedScannerSummon>());
			RegisterSummon(ModContent.ItemType<DeepseaStaff>(), ModContent.BuffType<AquaticStar>(), ModContent.ProjectileType<AquaticStarMinion>());
			RegisterSummon(ModContent.ItemType<VengefulSunStaff>(), ModContent.BuffType<SolarGodSpiritBuff>(), ModContent.ProjectileType<VengefulSunSpiritMinion>());
			RegisterSummon(ModContent.ItemType<TundraFlameBlossomsStaff>(), ModContent.BuffType<TundraFlameBlossomsBuff>(), ModContent.ProjectileType<TundraFlameBlossom>());
			RegisterSummon(ModContent.ItemType<DormantBrimseeker>(), ModContent.BuffType<BrimseekerBuff>(), ModContent.ProjectileType<DormantBrimseekerBab>());
			RegisterSummon(ModContent.ItemType<IgneousExaltation>(), ModContent.BuffType<IgneousExaltationBuff>(), ModContent.ProjectileType<IgneousBlade>());
			RegisterSummon(ModContent.ItemType<PlantationStaff>(), ModContent.BuffType<PlantationStaffBuff>(), ModContent.ProjectileType<PlantationStaffSummon>());
			RegisterSummon(ModContent.ItemType<ViralSprout>(), ModContent.BuffType<SageSpiritBuff>(), ModContent.ProjectileType<SageSpirit>());
			RegisterSummon(ModContent.ItemType<SandSharknadoStaff>(), ModContent.BuffType<Sandnado>(), ModContent.ProjectileType<SandnadoMinion>());
			RegisterSummon(ModContent.ItemType<GastricBelcherStaff>(), ModContent.BuffType<GastricAberrationBuff>(), ModContent.ProjectileType<GastricBelcher>());
			RegisterSummon(ModContent.ItemType<FuelCellBundle>(), ModContent.BuffType<MiniPlaguebringerBuff>(), ModContent.ProjectileType<PlaguebringerMK2>());
			RegisterSummon(ModContent.ItemType<WitherBlossomsStaff>(), ModContent.BuffType<WitherBlossomsBuff>(), ModContent.ProjectileType<WitherBlossom>());
			RegisterSummon(ModContent.ItemType<StarspawnHelixStaff>(), ModContent.BuffType<AstralProbeBuff>(), ModContent.ProjectileType<AstralProbeSummon>());
			RegisterSummon(ModContent.ItemType<TacticalPlagueEngine>(), ModContent.BuffType<TacticalPlagueEngineBuff>(), ModContent.ProjectileType<TacticalPlagueJet>());
			RegisterSummon(ModContent.ItemType<LegionofCelestia>(), ModContent.BuffType<LegionofCelestiaBuff>(), ModContent.ProjectileType<CelestialAxeMinion>());
			RegisterSummon(ModContent.ItemType<FlowersOfMortality>(), ModContent.BuffType<FlowersOfMortalityBuff>(), ModContent.ProjectileType<FlowersOfMortalityPetal>());
			RegisterSummon(ModContent.ItemType<SnakeEyes>(), ModContent.BuffType<SnakeEyesBuff>(), ModContent.ProjectileType<SnakeEyesSummon>());
			RegisterSummon(ModContent.ItemType<DazzlingStabberStaff>(), ModContent.BuffType<DazzlingStabberBuff>(), ModContent.ProjectileType<DazzlingStabber>());
			RegisterSummon(ModContent.ItemType<ViridVanguard>(), ModContent.BuffType<ViridVanguardBuff>(), ModContent.ProjectileType<ViridVanguardBlade>());
			RegisterSummon(ModContent.ItemType<DragonbloodDisgorger>(), ModContent.BuffType<SkeletalDragonsBuff>(), ModContent.ProjectileType<SkeletalDragonMother>());
			RegisterSummon(ModContent.ItemType<Cosmilamp>(), ModContent.BuffType<CosmilampBuff>(), ModContent.ProjectileType<CosmilampMinion>());
			RegisterSummon(ModContent.ItemType<VoidConcentrationStaff>(), ModContent.BuffType<VoidConcentrationBuff>(), ModContent.ProjectileType<VoidConcentrationAura>());
			RegisterSummon(ModContent.ItemType<EtherealSubjugator>(), ModContent.BuffType<Phantom>(), ModContent.ProjectileType<PhantomGuy>());
			RegisterSummon(ModContent.ItemType<CalamarisLament>(), ModContent.BuffType<CalamarisLamentBuff>(), ModContent.ProjectileType<CalamarisLamentMinion>());
			RegisterSummon(ModContent.ItemType<GammaHeart>(), ModContent.BuffType<GammaHydraBuff>(), ModContent.ProjectileType<GammaHead>());
			RegisterSummon(ModContent.ItemType<WarloksMoonFist>(), ModContent.BuffType<MoonFistBuff>(), ModContent.ProjectileType<MoonFist>());
			RegisterSummon(ModContent.ItemType<VoidEaterMarionette>(), ModContent.BuffType<VoidEaterMarionetteBuff>(), ModContent.ProjectileType<VoidEaterMarionetteProjectile>());
			RegisterSummon(ModContent.ItemType<CorvidHarbringerStaff>(), ModContent.BuffType<CorvidHarbringerBuff>(), ModContent.ProjectileType<PowerfulRaven>());
			RegisterSummon(ModContent.ItemType<EndoHydraStaff>(), ModContent.BuffType<EndoHydraBuff>(), ModContent.ProjectileType<EndoHydraHead>());
			RegisterSummon(ModContent.ItemType<CosmicViperEngine>(), ModContent.BuffType<CosmicViperEngineBuff>(), ModContent.ProjectileType<CosmicViperSummon>());
			RegisterSummon(ModContent.ItemType<YharonsKindleStaff>(), ModContent.BuffType<FieryDraconidBuff>(), ModContent.ProjectileType<FieryDraconid>());
			RegisterSummon(ModContent.ItemType<MidnightSunBeacon>(), ModContent.BuffType<MidnightSunBuff>(), ModContent.ProjectileType<MidnightSunUFO>());
			RegisterSummon(ModContent.ItemType<PoleWarper>(), ModContent.BuffType<PoleWarperBuff>(), ModContent.ProjectileType<PoleWarperSummon>());
			RegisterSummon(ModContent.ItemType<Vigilance>(), ModContent.BuffType<SoulSeekerBuff>(), ModContent.ProjectileType<SeekerSummonProj>());
			RegisterSummon(ModContent.ItemType<Metastasis>(), ModContent.BuffType<SepulcherMinionBuff>(), ModContent.ProjectileType<SepulcherMinion>());
			RegisterSummon(ModContent.ItemType<CosmicImmaterializer>(), ModContent.BuffType<CosmicEnergy>(), ModContent.ProjectileType<CosmicEnergySpiral>());
			RegisterSummon(ModContent.ItemType<TemporalUmbrella>(), ModContent.BuffType<MagicHatBuff>(), ModContent.ProjectileType<MagicHat>());
			RegisterSummon(ModContent.ItemType<Endogenesis>(), ModContent.BuffType<EndoCooperBuff>(), ModContent.ProjectileType<EndoCooperBody>());
			sAssociation.Call("AddMinionInfo", ModContent.ItemType<EntropysVigil>(), ModContent.BuffType<EntropysVigilBuff>(), new List<Dictionary<string, object>>
			{
				new Dictionary<string, object>
				{
					["ProjID"] = ModContent.ProjectileType<Calamitamini>(),
					["Slot"] = 0.6666666f
				},
				new Dictionary<string, object>
				{
					["ProjID"] = ModContent.ProjectileType<Cataclymini>(),
					["Slot"] = 2f / 3f
				},
				new Dictionary<string, object>
				{
					["ProjID"] = ModContent.ProjectileType<Catastromini>(),
					["Slot"] = 2f / 3f
				}
			});
			sAssociation.Call("AddMinionInfo", ModContent.ItemType<ResurrectionButterfly>(), ModContent.BuffType<ResurrectionButterflyBuff>(), new List<Dictionary<string, object>>
			{
				new Dictionary<string, object> { ["ProjID"] = ModContent.ProjectileType<PurpleButterfly>() },
				new Dictionary<string, object> { ["ProjID"] = ModContent.ProjectileType<PinkButterfly>() }
			});
			sAssociation.Call("AddMinionInfo", ModContent.ItemType<KingofConstellationsTenryu>(), ModContent.BuffType<KingofConstellationsBuff>(), new List<Dictionary<string, object>>
			{
				new Dictionary<string, object> { ["ProjID"] = ModContent.ProjectileType<BlackDragonHead>() },
				new Dictionary<string, object> { ["ProjID"] = ModContent.ProjectileType<WhiteDragonHead>() }
			});
		}
		void RegisterSummon(int summonItem, int summonBuff, int summonProjectile)
		{
			sAssociation.Call("AddMinionInfo", summonItem, summonBuff, summonProjectile);
		}
	}

	private static void ColoredDamageTypesSupport()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		Mod coloredDamageTypes = ExternalMods.coloredDamageTypes;
		if (coloredDamageTypes != null)
		{
			coloredDamageTypes.Call("AddDamageType", AverageDamageClass.Instance, DefaultTooltipColor, DefaultDamageColor, DefaultCritColor);
			coloredDamageTypes.Call("AddDamageType", TrueMeleeDamageClass.Instance, MeleeTooltipColor, MeleeDamageColor, MeleeCritColor);
			coloredDamageTypes.Call("AddDamageType", TrueMeleeNoSpeedDamageClass.Instance, MeleeTooltipColor, MeleeDamageColor, MeleeCritColor);
			coloredDamageTypes.Call("AddDamageType", MeleeRangedHybridDamageClass.Instance, MeleeRangedTooltipColor, MeleeRangedDamageColor, MeleeRangedCritColor);
			coloredDamageTypes.Call("AddDamageType", RogueDamageClass.Instance, RogueTooltipColor, RogueDamageColor, RogueCritColor);
			coloredDamageTypes.Call("AddDamageType", StealthDamageClass.Instance, StealthTooltipColor, StealthDamageColor, StealthCritColor);
		}
	}

	private static void RegisterWorldInfoIcon(Mod luminance, string texturePath, string hoverTextKey, Func<WorldFileData, bool> shouldAppear, byte priority)
	{
		luminance.Call("RegisterWorldInfoIcon", texturePath, hoverTextKey, shouldAppear, priority);
	}

	private static void LuminanceSupport()
	{
		Mod luminance = ExternalMods.luminance;
		if (luminance == null)
		{
			return;
		}
		Func<WorldFileData, bool> deathEnabled = delegate(WorldFileData data)
		{
			if (!data.TryGetHeaderData<WorldSelectionDifficultySystem>(out var data2))
			{
				return false;
			}
			return data2.ContainsKey("DeathMode") && data2.GetBool("DeathMode");
		};
		Func<WorldFileData, bool> revengeanceEnabled = delegate(WorldFileData data)
		{
			if (!data.TryGetHeaderData<WorldSelectionDifficultySystem>(out var data2))
			{
				return false;
			}
			return data2.ContainsKey("RevengeanceMode") && data2.GetBool("RevengeanceMode") && (!data2.ContainsKey("DeathMode") || !data2.GetBool("DeathMode"));
		};
		RegisterWorldInfoIcon(luminance, "CalamityMod/UI/ModeIndicator/ModeIndicator_Death", "Mods.CalamityMod.UI.Death", deathEnabled, 50);
		RegisterWorldInfoIcon(luminance, "CalamityMod/UI/ModeIndicator/ModeIndicator_Rev", "Mods.CalamityMod.UI.Revengeance", revengeanceEnabled, 50);
	}

	static WeakReferenceSupport()
	{
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		BossChecklistProgressionValues = new Dictionary<string, float>
		{
			{ "DesertScourge", 1.6f },
			{ "GiantClam", 1.61f },
			{ "AcidRainT1", 2.67f },
			{ "Crabulon", 2.7f },
			{ "HiveMind", 3.98f },
			{ "Perforators", 3.99f },
			{ "SlimeGod", 6.7f },
			{ "Cryogen", 8.5f },
			{ "AquaticScourge", 9.5f },
			{ "AcidRainT2", 9.51f },
			{ "CragmawMire", 9.52f },
			{ "BrimstoneElemental", 10.5f },
			{ "CalamitasClone", 11.7f },
			{ "GreatSandShark", 12.09f },
			{ "Leviathan", 12.8f },
			{ "AstrumAureus", 12.81f },
			{ "PlaguebringerGoliath", 14.5f },
			{ "Ravager", 16.5f },
			{ "AstrumDeus", 17.5f },
			{ "ProfanedGuardians", 18.5f },
			{ "Dragonfolly", 18.6f },
			{ "Providence", 19f },
			{ "CeaselessVoid", 19.6f },
			{ "StormWeaver", 19.61f },
			{ "Signus", 19.62f },
			{ "Polterghast", 20f },
			{ "AcidRainT3", 20.49f },
			{ "Mauler", 20.491f },
			{ "NuclearTerror", 20.492f },
			{ "OldDuke", 20.5f },
			{ "DevourerofGods", 21f },
			{ "Yharon", 22f },
			{ "ExoMechs", 22.99f },
			{ "Calamitas", 23f },
			{ "BossRush", 25.99f }
		};
		DefaultTooltipColor = Color.White;
		DefaultDamageColor = new Color(255, 160, 80);
		DefaultCritColor = new Color(255, 100, 30);
		MeleeTooltipColor = new Color(254, 121, 2);
		MeleeDamageColor = new Color(254, 121, 2);
		MeleeCritColor = new Color(253, 62, 3);
		MeleeRangedTooltipColor = new Color(144, 171, 76);
		MeleeRangedDamageColor = new Color(144, 171, 76);
		MeleeRangedCritColor = new Color(86, 102, 46);
		RogueTooltipColor = new Color(206, 132, 227);
		RogueDamageColor = new Color(206, 132, 227);
		RogueCritColor = new Color(194, 38, 212);
		StealthTooltipColor = RogueTooltipColor;
		StealthDamageColor = new Color(185, 105, 250);
		StealthCritColor = new Color(144, 33, 235);
	}
}
