using System;
using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.Dyes.HairDye;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Placeables.Furniture.Fountains;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.SummonItems.Invasion;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Systems.Collections;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public class CalamityGlobalTownNPC : GlobalNPC
{
	public bool setNewName = true;

	public int shopAlertAnimTimer;

	public int shopAlertAnimFrame;

	private static readonly string[] AnglerNames = new string[4] { "Dazren", "Johnny Test", "Bling Bling Boy", "RICE" };

	private static readonly string[] ArmsDealerNames = new string[9] { "Finchi", "Heniek", "Fire", "Barney Calhoun", "XiaoEn0426", "Jeffred", "The Cooler Arthur", "Shark", "Sagi" };

	private static readonly string[] ClothierNames = new string[5] { "Joeseph Jostar", "Storm Havik", "Magorfis Splunt the Greater Finklejim", "Perrin", "Dorkyy" };

	private static readonly string[] CyborgNames = new string[3] { "Sylux", "Nemesis", "Univerze" };

	private static readonly string[] DemolitionistNames = new string[3] { "Tavish DeGroot", "Fimmy", "John Helldiver" };

	private static readonly string[] DryadNames = new string[7] { "Rythmi", "Izuna", "Jasmine", "Cybil", "Ruth", "Kanna", "Elliada" };

	private static readonly string[] DyeTraderNames = null;

	private static readonly string[] GoblinTinkererNames = new string[13]
	{
		"Verth", "Gormer", "TingFlarg", "Driser", "Eddie Spaghetti", "G'tok", "Katto", "Him", "Tooshiboots", "Neesh",
		"Bars Boldia", "Basel Raiden John Clive Fantasy 16", "Gobby, Destroyer of Wallets"
	};

	private static readonly string[] GolferNames = null;

	private static readonly string[] GuideNames = new string[19]
	{
		"Lapp", "Ben Shapiro", "Streakist", "Necroplasmic", "Devin", "Woffle", "Cameron", "Wilbur", "Good Game Design", "Danmaku",
		"Grylken", "Outlaw", "Alfred Rend", "Leeman", "Mihai", "Dinkleberg", "Wamy", "Baggute", "Jacob Bryson"
	};

	private static readonly string[] MechanicNames = new string[7] { "Lilly", "Daawn", "Robin", "Curly", "Cobalt", "Dizzetriya", "Vodka" };

	private static readonly string[] MerchantNames = new string[2] { "Morshu", "Spamton G. Spamton" };

	private static readonly string[] NurseNames = new string[4] { "Farsni", "Fanny", "Mausi", "Fiona" };

	private static readonly string[] PainterNames = new string[2] { "Picasso", "Bew" };

	private static readonly string[] PartyGirlNames = new string[3] { "Arin", "Typhäne", "Charlotte Linlin" };

	private static readonly string[] PirateNames = new string[9] { "Tyler Van Hook", "Cap'n Deek", "Captain Billy Bones", "Captain J. Crackers", "Gol D. Roger", "Yarrim", "Hector Barbossa", "Blunderbeard", "Vergil Cyrus" };

	private static readonly string[] PrincessNames = new string[9] { "Nyapano", "Jade", "Nyavi Aceso", "everquartz", "Gwynevere", "Hael", "Yumesaki Mirrin", "Vela", "Misako Drevis" };

	private static readonly string[] SantaClausNames = new string[2] { "Jank", "Aoi Kurashiki" };

	private static readonly string[] SkeletonMerchantNames = new string[4] { "Sans Undertale", "Papyrus Undertale", "Mr. Bones", "Freakbob" };

	private static readonly string[] SteampunkerNames = new string[8] { "Vorbis", "Angel", "Mòrag Ladair", "Linn", "Eira", "Kreutz", "Cathlyn", "Eunice" };

	private static readonly string[] StylistNames = new string[7] { "Amber", "Faith", "Xsiana", "Lain", "Hamis", "Brio Scarlet", "Vanessa" };

	private static readonly string[] TavernkeepNames = new string[4] { "Tim Lockwood", "Sir Samuel Winchester Jenkins Kester II", "Brutus", "Sloth" };

	private static readonly string[] TaxCollectorNames = new string[3] { "Emmett", "Bagman", "Old Man Scrooge" };

	private static readonly string[] TravelingMerchantNames = new string[6] { "Stan Pines", "Intergaze", "Borgus", "Postman Hiss", "Cosmoec", "Junorism" };

	private static readonly string[] TruffleNames = new string[4] { "Aldrimil", "Wonton", "Mad Lad", "Nokko" };

	private static readonly string[] WitchDoctorNames = new string[6] { "Sok'ar", "Aeroni", "Mixcoatl", "Amnesia Wapers", "Tequila", "Bee Movie Script" };

	private static readonly string[] WizardNames = new string[11]
	{
		"Inorim, son of Ivukey", "Jensen", "Merasmus", "Habolo", "Ortho", "Chris Tallballs", "Syethas", "Nextdoor Psycho", "Mike Cyclops", "Derin",
		"Umbara"
	};

	private static readonly string[] ZoologistNames = new string[7] { "Kiriku", "Lacuna", "Mae Borowski", "Fera", "Gwenhwyvar", "Daxie", "Zora" };

	private static readonly string[] ClumsySlimeNames = null;

	private static readonly string[] CoolSlimeNames = null;

	private static readonly string[] DivaSlimeNames = null;

	private static readonly string[] ElderSlimeNames = null;

	private static readonly string[] MysticSlimeNames = null;

	private static readonly string[] NerdySlimeNames = new string[2] { "Big Blungus", "Rimuru Tempest" };

	private static readonly string[] SquireSlimeNames = null;

	private static readonly string[] SurlySlimeNames = null;

	private const int TownDogLabradorVanillaNames = 17;

	private const int TownDogPitBullVanillaNames = 14;

	private const int TownDogBeagleVanillaNames = 12;

	private const int TownDogCorgiVanillaNames = 14;

	private const int TownDogDalmatianVanillaNames = 13;

	private const int TownDogHuskyVanillaNames = 16;

	private static readonly string[] TownDogNames = new string[3] { "Ozymandias", "Miss Throws a Lot", "Brikwilla" };

	private static readonly string[] TownDogLabradorNames = new string[3] { "Riley", "Silvie", "Madison" };

	private static readonly string[] TownDogPitBullNames = new string[1] { "Splinter" };

	private static readonly string[] TownDogBeagleNames = new string[4] { "Kendra", "Libby", "Myles", "Luna" };

	private static readonly string[] TownDogCorgiNames = null;

	private static readonly string[] TownDogDalmatianNames = null;

	private static readonly string[] TownDogHuskyNames = new string[2] { "Yoshi", "Franklin" };

	private const int TownCatSiameseVanillaNames = 12;

	private const int TownCatBlackVanillaNames = 23;

	private const int TownCatOrangeTabbyVanillaNames = 18;

	private const int TownCatRussianBlueVanillaNames = 16;

	private const int TownCatSilverVanillaNames = 17;

	private const int TownCatWhiteVanillaNames = 15;

	private static readonly string[] TownCatNames = new string[6] { "Smoogle", "The Meowurer of Gods", "Katsafaros", "Lucerne", "Milo", "Octo" };

	private static readonly string[] TownCatSiameseNames = new string[5] { "Conductor", "Vivian", "Pudum", "Snickers", "Mr. Kitten" };

	private static readonly string[] TownCatBlackNames = new string[6] { "Bear", "Storm", "Hognar", "Saffie", "Willow", "Maine" };

	private static readonly string[] TownCatOrangeTabbyNames = new string[6] { "Felix", "Tardo", "Dali", "Kiba", "Monkey", "Percy" };

	private static readonly string[] TownCatRussianBlueNames = null;

	private static readonly string[] TownCatSilverNames = new string[1] { "Archie" };

	private static readonly string[] TownCatWhiteNames = null;

	private const int TownBunnyWhiteVanillaNames = 14;

	private const int TownBunnyAngoraVanillaNames = 10;

	private const int TownBunnyDutchVanillaNames = 11;

	private const int TownBunnyFlemishVanillaNames = 12;

	private const int TownBunnyLopVanillaNames = 13;

	private const int TownBunnySilverVanillaNames = 13;

	private static readonly string[] TownBunnyNames = new string[2] { "Poco", "Puffer" };

	private static readonly string[] TownBunnyWhiteNames = null;

	private static readonly string[] TownBunnyAngoraNames = null;

	private static readonly string[] TownBunnyDutchNames = null;

	private static readonly string[] TownBunnyFlemishNames = null;

	private static readonly string[] TownBunnyLopNames = null;

	private static readonly string[] TownBunnySilverNames = null;

	public static List<(int, Predicate<Player>, Action<Player, bool>)> npcAlertList = new List<(int, Predicate<Player>, Action<Player, bool>)>
	{
		(17, (Player player) => player.Calamity().newMerchantInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newMerchantInventory = enabled;
		}),
		(227, (Player player) => player.Calamity().newPainterInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newPainterInventory = enabled;
		}),
		(588, (Player player) => player.Calamity().newGolferInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newGolferInventory = enabled;
		}),
		(633, (Player player) => player.Calamity().newZoologistInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newZoologistInventory = enabled;
		}),
		(207, (Player player) => player.Calamity().newDyeTraderInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newDyeTraderInventory = enabled;
		}),
		(208, (Player player) => player.Calamity().newPartyGirlInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newPartyGirlInventory = enabled;
		}),
		(353, (Player player) => player.Calamity().newStylistInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newStylistInventory = enabled;
		}),
		(38, (Player player) => player.Calamity().newDemolitionistInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newDemolitionistInventory = enabled;
		}),
		(20, (Player player) => player.Calamity().newDryadInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newDryadInventory = enabled;
		}),
		(550, (Player player) => player.Calamity().newTavernkeepInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newTavernkeepInventory = enabled;
		}),
		(19, (Player player) => player.Calamity().newArmsDealerInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newArmsDealerInventory = enabled;
		}),
		(107, (Player player) => player.Calamity().newGoblinTinkererInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newGoblinTinkererInventory = enabled;
		}),
		(228, (Player player) => player.Calamity().newWitchDoctorInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newWitchDoctorInventory = enabled;
		}),
		(54, (Player player) => player.Calamity().newClothierInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newClothierInventory = enabled;
		}),
		(124, (Player player) => player.Calamity().newMechanicInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newMechanicInventory = enabled;
		}),
		(229, (Player player) => player.Calamity().newPirateInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newPirateInventory = enabled;
		}),
		(160, (Player player) => player.Calamity().newTruffleInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newTruffleInventory = enabled;
		}),
		(108, (Player player) => player.Calamity().newWizardInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newWizardInventory = enabled;
		}),
		(178, (Player player) => player.Calamity().newSteampunkerInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newSteampunkerInventory = enabled;
		}),
		(209, (Player player) => player.Calamity().newCyborgInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newCyborgInventory = enabled;
		}),
		(663, (Player player) => player.Calamity().newPrincessInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newPrincessInventory = enabled;
		}),
		(453, (Player player) => player.Calamity().newSkeletonMerchantInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newSkeletonMerchantInventory = enabled;
		}),
		(ModContent.NPCType<SeaKing>(), (Player player) => player.Calamity().newAmidiasInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newAmidiasInventory = enabled;
		}),
		(ModContent.NPCType<Bandit>(), (Player player) => player.Calamity().newBanditInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newBanditInventory = enabled;
		}),
		(ModContent.NPCType<Archmage>(), (Player player) => player.Calamity().newPermafrostInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newPermafrostInventory = enabled;
		}),
		(ModContent.NPCType<BrimstoneWitch>(), (Player player) => player.Calamity().newCalamitasInventory, delegate(Player player, bool enabled)
		{
			player.Calamity().newCalamitasInventory = enabled;
		})
	};

	public static float TaxYieldFactor
	{
		get
		{
			if (DownedBossSystem.downedDoG)
			{
				return 10f;
			}
			if (NPC.downedMoonlord)
			{
				return 8f;
			}
			if (NPC.downedPlantBoss)
			{
				return 2f;
			}
			return 1f;
		}
	}

	public static int TotalTaxesPerNPC => (int)((float)Item.buyPrice(0, 0, 1) * TaxYieldFactor);

	public static int TaxesToCollectLimit => (int)((float)Item.buyPrice(0, 50) * TaxYieldFactor);

	public override bool InstancePerEntity => true;

	public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
	{
		return entity.isLikeATownNPC;
	}

	public override GlobalNPC Clone(NPC npc, NPC npcClone)
	{
		CalamityGlobalTownNPC obj = (CalamityGlobalTownNPC)base.Clone(npc, npcClone);
		obj.setNewName = setNewName;
		obj.shopAlertAnimTimer = shopAlertAnimTimer;
		obj.shopAlertAnimFrame = shopAlertAnimFrame;
		return obj;
	}

	public static void ResetTownNPCNameBools()
	{
		ResetName(637, ref CalamityWorld.catName);
		ResetName(638, ref CalamityWorld.dogName);
		ResetName(656, ref CalamityWorld.bunnyName);
		static void ResetName(int npcID, ref bool nameBool)
		{
			if (NPC.FindFirstNPC(npcID) == -1)
			{
				nameBool = false;
			}
		}
	}

	private string ChooseName(ref bool alreadySet, string currentName, int numVanillaNames, string[] patreonNames, string[] globalNames)
	{
		if (alreadySet)
		{
			alreadySet = true;
			return currentName;
		}
		alreadySet = true;
		int combinedLength = ((patreonNames != null) ? patreonNames.Length : 0) + ((globalNames != null) ? globalNames.Length : 0);
		int index = Main.rand.Next(numVanillaNames + combinedLength);
		if (index >= combinedLength)
		{
			return currentName;
		}
		if (index >= globalNames.Length)
		{
			return patreonNames[index - globalNames.Length];
		}
		return globalNames[index];
	}

	public void SetPatreonTownNPCName(NPC npc, Mod mod)
	{
		if (!setNewName)
		{
			return;
		}
		setNewName = false;
		switch (npc.type)
		{
		case 637:
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 12, TownCatSiameseNames, TownCatNames);
				break;
			case 1:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 23, TownCatBlackNames, TownCatNames);
				break;
			case 2:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 18, TownCatOrangeTabbyNames, TownCatNames);
				break;
			case 3:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 16, TownCatRussianBlueNames, TownCatNames);
				break;
			case 4:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 17, TownCatSilverNames, TownCatNames);
				break;
			case 5:
				npc.GivenName = ChooseName(ref CalamityWorld.catName, npc.GivenName, 15, TownCatWhiteNames, TownCatNames);
				break;
			}
			break;
		case 638:
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 17, TownDogLabradorNames, TownDogNames);
				break;
			case 1:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 14, TownDogPitBullNames, TownDogNames);
				break;
			case 2:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 12, TownDogBeagleNames, TownDogNames);
				break;
			case 3:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 14, TownDogCorgiNames, TownDogNames);
				break;
			case 4:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 13, TownDogDalmatianNames, TownDogNames);
				break;
			case 5:
				npc.GivenName = ChooseName(ref CalamityWorld.dogName, npc.GivenName, 16, TownDogHuskyNames, TownDogNames);
				break;
			}
			break;
		case 656:
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 14, TownBunnyWhiteNames, TownBunnyNames);
				break;
			case 1:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 10, TownBunnyAngoraNames, TownBunnyNames);
				break;
			case 2:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 11, TownBunnyDutchNames, TownBunnyNames);
				break;
			case 3:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 12, TownBunnyFlemishNames, TownBunnyNames);
				break;
			case 4:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 13, TownBunnyLopNames, TownBunnyNames);
				break;
			case 5:
				npc.GivenName = ChooseName(ref CalamityWorld.bunnyName, npc.GivenName, 13, TownBunnySilverNames, TownBunnyNames);
				break;
			}
			break;
		}
	}

	private void AddNewNames(List<string> nameList, string[] patreonNames)
	{
		if (patreonNames != null && patreonNames.Length != 0)
		{
			for (int i = 0; i < patreonNames.Length; i++)
			{
				nameList.Add(patreonNames[i]);
			}
		}
	}

	public override void ModifyNPCNameList(NPC npc, List<string> nameList)
	{
		switch (npc.type)
		{
		case 369:
			AddNewNames(nameList, AnglerNames);
			break;
		case 19:
			AddNewNames(nameList, ArmsDealerNames);
			break;
		case 54:
			AddNewNames(nameList, ClothierNames);
			break;
		case 209:
			AddNewNames(nameList, CyborgNames);
			break;
		case 38:
			AddNewNames(nameList, DemolitionistNames);
			break;
		case 20:
			AddNewNames(nameList, DryadNames);
			break;
		case 207:
			AddNewNames(nameList, DyeTraderNames);
			break;
		case 107:
			AddNewNames(nameList, GoblinTinkererNames);
			break;
		case 588:
			AddNewNames(nameList, GolferNames);
			break;
		case 22:
			AddNewNames(nameList, GuideNames);
			break;
		case 124:
			AddNewNames(nameList, MechanicNames);
			break;
		case 17:
			AddNewNames(nameList, MerchantNames);
			break;
		case 18:
			AddNewNames(nameList, NurseNames);
			break;
		case 227:
			AddNewNames(nameList, PainterNames);
			break;
		case 208:
			AddNewNames(nameList, PartyGirlNames);
			break;
		case 229:
			AddNewNames(nameList, PirateNames);
			break;
		case 663:
			AddNewNames(nameList, PrincessNames);
			break;
		case 142:
			AddNewNames(nameList, SantaClausNames);
			break;
		case 453:
			AddNewNames(nameList, SkeletonMerchantNames);
			break;
		case 178:
			AddNewNames(nameList, SteampunkerNames);
			break;
		case 353:
			AddNewNames(nameList, StylistNames);
			break;
		case 550:
			AddNewNames(nameList, TavernkeepNames);
			break;
		case 441:
			AddNewNames(nameList, TaxCollectorNames);
			break;
		case 368:
			AddNewNames(nameList, TravelingMerchantNames);
			break;
		case 160:
			AddNewNames(nameList, TruffleNames);
			break;
		case 228:
			AddNewNames(nameList, WitchDoctorNames);
			break;
		case 108:
			AddNewNames(nameList, WizardNames);
			break;
		case 633:
			AddNewNames(nameList, ZoologistNames);
			break;
		case 680:
			AddNewNames(nameList, ClumsySlimeNames);
			break;
		case 678:
			AddNewNames(nameList, CoolSlimeNames);
			break;
		case 681:
			AddNewNames(nameList, DivaSlimeNames);
			break;
		case 679:
			AddNewNames(nameList, ElderSlimeNames);
			break;
		case 683:
			AddNewNames(nameList, MysticSlimeNames);
			break;
		case 670:
			AddNewNames(nameList, NerdySlimeNames);
			break;
		case 684:
			AddNewNames(nameList, SquireSlimeNames);
			break;
		case 682:
			AddNewNames(nameList, SurlySlimeNames);
			break;
		case 637:
			AddNewNames(nameList, TownCatNames);
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				AddNewNames(nameList, TownCatSiameseNames);
				break;
			case 1:
				AddNewNames(nameList, TownCatBlackNames);
				break;
			case 2:
				AddNewNames(nameList, TownCatOrangeTabbyNames);
				break;
			case 3:
				AddNewNames(nameList, TownCatRussianBlueNames);
				break;
			case 4:
				AddNewNames(nameList, TownCatSilverNames);
				break;
			case 5:
				AddNewNames(nameList, TownCatWhiteNames);
				break;
			}
			break;
		case 638:
			AddNewNames(nameList, TownDogNames);
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				AddNewNames(nameList, TownDogLabradorNames);
				break;
			case 1:
				AddNewNames(nameList, TownDogPitBullNames);
				break;
			case 2:
				AddNewNames(nameList, TownDogBeagleNames);
				break;
			case 3:
				AddNewNames(nameList, TownDogCorgiNames);
				break;
			case 4:
				AddNewNames(nameList, TownDogDalmatianNames);
				break;
			case 5:
				AddNewNames(nameList, TownDogHuskyNames);
				break;
			}
			break;
		case 656:
			AddNewNames(nameList, TownBunnyNames);
			switch (npc.townNpcVariationIndex)
			{
			case 0:
				AddNewNames(nameList, TownBunnyWhiteNames);
				break;
			case 1:
				AddNewNames(nameList, TownBunnyAngoraNames);
				break;
			case 2:
				AddNewNames(nameList, TownBunnyDutchNames);
				break;
			case 3:
				AddNewNames(nameList, TownBunnyFlemishNames);
				break;
			case 4:
				AddNewNames(nameList, TownBunnyLopNames);
				break;
			case 5:
				AddNewNames(nameList, TownBunnySilverNames);
				break;
			}
			break;
		}
	}

	public override void SetDefaults(NPC npc)
	{
		BoundNPCSafety(base.Mod, npc);
	}

	public override bool PreAI(NPC npc)
	{
		SetPatreonTownNPCName(npc, base.Mod);
		return true;
	}

	public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		TownNPCAlertSystem(npc, base.Mod, spriteBatch);
		return true;
	}

	public void TownNPCAlertSystem(NPC npc, Mod mod, SpriteBatch spriteBatch)
	{
		if (!CalamityClientConfig.Instance.ShopNewAlert || !npc.townNPC)
		{
			return;
		}
		for (int i = 0; i < npcAlertList.Count; i++)
		{
			if (npc.type == npcAlertList[i].Item1 && npcAlertList[i].Item2(Main.LocalPlayer))
			{
				DrawNewInventoryAlert(npc);
			}
		}
		void DrawNewInventoryAlert(NPC nPC)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			Vector2 drawPos = nPC.Center - Main.screenPosition;
			float drawPosY = (float)(TextureAssets.Npc[nPC.type].Value.Height / Main.npcFrameCount[nPC.type] / 2) * nPC.scale + npc.gfxOffY + 36f;
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/NPCAlertDisplay", (AssetRequestMode)2).Value;
			shopAlertAnimTimer++;
			if (shopAlertAnimTimer >= 6)
			{
				shopAlertAnimTimer = 0;
				shopAlertAnimFrame++;
				if (shopAlertAnimFrame > 4)
				{
					shopAlertAnimFrame = 0;
				}
			}
			int frameHeight = texture.Height / 5;
			Rectangle animRect = default(Rectangle);
			((Rectangle)(ref animRect))._002Ector(0, frameHeight * shopAlertAnimFrame, texture.Width, frameHeight);
			spriteBatch.Draw(texture, drawPos - new Vector2(5f, drawPosY), (Rectangle?)animRect, Color.White, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}

	public override void OnChatButtonClicked(NPC npc, bool firstButton)
	{
		for (int i = 0; i < npcAlertList.Count; i++)
		{
			if (npc.type == npcAlertList[i].Item1)
			{
				npcAlertList[i].Item3(Main.LocalPlayer, arg2: false);
			}
		}
	}

	public static void SetNewShopVariable(int[] types, bool alreadySet)
	{
		_ = ContentSamples.NpcsByNetId[types[0]].FullName;
		if (alreadySet)
		{
			return;
		}
		for (int i = 0; i < types.Length; i++)
		{
			for (int n = 0; n < npcAlertList.Count; n++)
			{
				if (types[i] == npcAlertList[n].Item1)
				{
					npcAlertList[n].Item3(Main.LocalPlayer, arg2: true);
				}
			}
		}
	}

	public override void GetChat(NPC npc, ref string chat)
	{
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		int permafrost = NPC.FindFirstNPC(ModContent.NPCType<Archmage>());
		int seahorse = NPC.FindFirstNPC(ModContent.NPCType<SeaKing>());
		int thief = NPC.FindFirstNPC(ModContent.NPCType<Bandit>());
		NPC.FindFirstNPC(17);
		switch (npc.type)
		{
		case 369:
			if (Main.rand.NextBool(5) && seahorse != -1)
			{
				chat = CalamityUtils.GetText("Vanilla.AnglerChat.SeaKing").Format(Main.npc[seahorse].GivenName);
			}
			break;
		case 19:
			if (Main.rand.NextBool(Main.hardMode ? 20 : 4) && NPC.downedBoss3 && !Main.LocalPlayer.InventoryHas(4703) && !Main.LocalPlayer.ZoneGraveyard)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ArmsDealerChat.MentionQuadBarrel");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.InventoryHas(4703))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ArmsDealerChat.HasQuadBarrel");
			}
			else if (Main.rand.NextBool(10) && DownedBossSystem.downedDoG)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ArmsDealerChat.DoGDefeated");
			}
			else if (Main.rand.NextBool(5) && Main.eclipse)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ArmsDealerChat.Eclipse");
			}
			break;
		case 54:
			if (Main.rand.NextBool(10) && DownedBossSystem.downedPolterghast)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ClothierChat.PolterghastDefeated");
			}
			if (Main.rand.NextBool(5) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ClothierChat.MoonLordDefeated" + Main.rand.Next(1, 4));
			}
			if (Main.rand.NextBool(5) && NPC.AnyNPCs(398))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.ClothierChat.MoonLordPresent");
			}
			break;
		case 209:
			if (Main.rand.NextBool(5) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.CyborgChat.MoonLordDefeated" + Main.rand.Next(1, 3));
			}
			else if (Main.rand.NextBool(10) && !DownedBossSystem.downedPlaguebringer && NPC.downedGolemBoss)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.CyborgChat.MentionPlague");
			}
			else if (Main.rand.NextBool(10) && Main.raining)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.CyborgChat.Rain");
			}
			break;
		case 38:
			if (Main.rand.NextBool(5) && DownedBossSystem.downedDoG)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DemolitionistChat.DoGDefeated");
			}
			else if (Main.rand.NextBool(10))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DemolitionistChat.MentionSkynamite");
			}
			break;
		case 20:
			if (Main.rand.NextBool(5) && DownedBossSystem.downedDoG && Main.eclipse)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DryadChat.DarksunEclipse");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.ZoneGlowshroom)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DryadChat.Mushroom");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneSulphur)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DryadChat.SulphurSea");
			}
			else if (Main.rand.NextBool(5) && Main.hardMode)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DryadChat.Hardmode");
			}
			break;
		case 207:
			if (Main.rand.NextBool(5) && permafrost != -1)
			{
				chat = CalamityUtils.GetText("Vanilla.DyeTraderChat.Archmage").Format(Main.npc[permafrost].GivenName);
			}
			else if (Main.rand.NextBool(5))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.DyeTraderChat.Normal");
			}
			break;
		case 107:
			if (Main.rand.NextBool(10) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.GoblinTinkererChat.MoonLordDefeated");
			}
			else if (Main.rand.NextBool(3) && thief != -1 && CalamityWorld.Reforges >= 1)
			{
				chat = CalamityUtils.GetText("Vanilla.GoblinTinkererChat.Bandit").Format(Main.npc[thief].GivenName);
			}
			break;
		case 22:
			if (Main.rand.NextBool(10) && DownedBossSystem.downedProvidence)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.GuideChat.ProvidenceDefeated" + Main.rand.Next(1, 3));
			}
			else if (Main.rand.NextBool(20) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.GuideChat.MoonLordDefeated");
			}
			else if (Main.rand.NextBool(10) && Main.hardMode)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.GuideChat.Hardmode" + Main.rand.Next(1, 3));
			}
			break;
		case 124:
			if (Main.rand.NextBool(5) && Main.LocalPlayer.InventoryHas(3384))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MechanicChat.HasPortalGun");
			}
			else if (Main.rand.NextBool(5) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MechanicChat.MoonLordDefeated");
			}
			else if (Main.rand.NextBool(5) && Main.eclipse)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MechanicChat.Eclipse");
			}
			else if (Main.rand.NextBool(5) && AcidRainEvent.AcidRainEventIsOngoing)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MechanicChat.AcidRain");
			}
			break;
		case 17:
			if (Main.rand.NextBool(5) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MerchantChat.MoonLordDefeated");
			}
			else if (Main.rand.NextBool(5) && Main.eclipse)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MerchantChat.Eclipse");
			}
			else if (Main.rand.NextBool(5) && AcidRainEvent.AcidRainEventIsOngoing)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MerchantChat.AcidRain");
			}
			else if (Main.rand.NextBool(7) && thief != -1)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.MerchantChat.Bandit");
			}
			break;
		case 227:
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneCorrupt)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Corruption");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneCrimson)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Crimson");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneSnow)
			{
				if (Main.rand.NextBool() && permafrost != -1)
				{
					chat = CalamityUtils.GetText("Vanilla.PainterChat.Archmage").Format(Main.npc[permafrost].GivenName);
				}
				else
				{
					chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Tundra");
				}
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneDesert)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Desert");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneHallow)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Hallow");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneSkyHeight)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Space");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneJungle)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Jungle");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneAstral)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Astral");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.ZoneUnderworldHeight)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Underworld" + Main.rand.Next(1, 3));
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneCalamity)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Crags");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneSulphur)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.SulphurSea");
			}
			if (Main.rand.NextBool(4) && Main.LocalPlayer.Calamity().ZoneAbyss)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PainterChat.Abyss");
			}
			break;
		case 208:
			if (Main.rand.NextBool(4) && Main.eclipse)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PartyGirlChat.Eclipse" + Main.rand.Next(1, 3));
			}
			break;
		case 229:
			if (Main.rand.NextBool(5) && !DownedBossSystem.downedLeviathan)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PirateChat.PreLeviathan");
			}
			else if (Main.rand.NextBool(5) && DownedBossSystem.downedAquaticScourge)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PirateChat.WetScourgeDefeated");
			}
			else if (Main.rand.NextBool(5) && seahorse != -1)
			{
				chat = CalamityUtils.GetText("Vanilla.PirateChat.SeaKing").Format(Main.npc[seahorse].GivenName);
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.Center.ToTileCoordinates().X < 380 && !Main.LocalPlayer.Calamity().ZoneSulphur)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PirateChat.Ocean");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneSulphur)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.PirateChat.SulphurSea" + Main.rand.Next(1, 3));
			}
			break;
		case 453:
			if (Main.rand.NextBool(5))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.SkeletonMerchantChat.Normal");
			}
			break;
		case 178:
			if (Main.rand.NextBool(5) && NPC.downedMoonlord)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.SteampunkerChat.MoonLordDefeated");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.Calamity().ZoneAstral)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.SteampunkerChat.Astral");
			}
			else if (Main.rand.NextBool(5) && Main.LocalPlayer.ZoneHallow)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.SteampunkerChat.Hallow");
			}
			break;
		case 353:
		{
			string worldEvil = Language.GetTextValue("LegacyMisc." + (WorldGen.crimson ? 102 : 101));
			if (Main.rand.NextBool(15) && Main.hardMode)
			{
				chat = CalamityUtils.GetText("Vanilla.StylistChat.Hardmode").Format(worldEvil);
			}
			if (Main.rand.NextBool((npc.GivenName == "Amber") ? 10 : 15) && Main.LocalPlayer.Calamity().pSoulArtifact)
			{
				if (Main.LocalPlayer.Calamity().profanedCrystalBuffs)
				{
					chat = CalamityUtils.GetTextValue("Vanilla.StylistChat.ProfanedSoulCrystal" + Main.rand.Next(1, 3));
				}
				else if (Main.LocalPlayer.Calamity().pSoulGuardians)
				{
					chat = CalamityUtils.GetTextValue("Vanilla.StylistChat.ProfanedDonuts");
				}
			}
			break;
		}
		case 550:
			if (Main.rand.NextBool(5) && !Main.dayTime && Main.moonPhase == 0)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TavernkeepChat.FullMoon");
			}
			break;
		case 441:
		{
			int platinumCoins = 0;
			Player player = Main.LocalPlayer;
			if (player.active)
			{
				for (int j = 0; j < player.inventory.Length; j++)
				{
					if (player.inventory[j].type == 74)
					{
						platinumCoins += player.inventory[j].stack;
					}
				}
			}
			if (Main.rand.NextBool(10) && DownedBossSystem.downedDoG)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TaxCollectorChat.DoGDefeated");
			}
			else if (Main.rand.NextBool(5) && !DownedBossSystem.downedBrimstoneElemental)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TaxCollectorChat.PreBrimmy");
			}
			else if (Main.rand.NextBool(10) && Main.LocalPlayer.InventoryHas(ModContent.ItemType<SlickCane>()))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TaxCollectorChat.HasSlickCane");
			}
			else if (Main.rand.NextBool(5) && platinumCoins >= 500)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TaxCollectorChat.Has500Plat");
			}
			else if (Main.rand.NextBool(5) && platinumCoins >= 100)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TaxCollectorChat.Has100Plat");
			}
			break;
		}
		case 160:
			if (Main.rand.NextBool(8))
			{
				chat = CalamityUtils.GetTextValue("Vanilla.TruffleChat.Normal");
			}
			break;
		case 228:
			if (Main.rand.NextBool(8) && Main.bloodMoon)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.WitchDoctorChat.BloodMoon");
			}
			else if (Main.rand.NextBool(8) && Main.hardMode && !NPC.downedPlantBoss)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.WitchDoctorChat.PrePlantera");
			}
			else if (Main.rand.NextBool(8) && Main.LocalPlayer.ZoneJungle)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.WitchDoctorChat.Jungle");
			}
			break;
		case 108:
			if (Main.rand.NextBool(10) && Main.hardMode)
			{
				chat = CalamityUtils.GetTextValue("Vanilla.WizardChat.Hardmode");
			}
			break;
		}
	}

	public void BoundNPCSafety(Mod mod, NPC npc)
	{
		if (CalamityNPCTypeSets.BoundTownNPC[npc.type])
		{
			npc.dontTakeDamageFromHostiles = true;
		}
	}

	public override void BuffTownNPC(ref float damageMult, ref int defense)
	{
		if (NPC.downedMoonlord)
		{
			damageMult += 0.6f;
			defense += 20;
		}
		if (DownedBossSystem.downedProvidence)
		{
			damageMult += 0.2f;
			defense += 12;
		}
		if (DownedBossSystem.downedPolterghast)
		{
			damageMult += 0.2f;
			defense += 12;
		}
		if (DownedBossSystem.downedDoG)
		{
			damageMult += 0.2f;
			defense += 12;
		}
		if (DownedBossSystem.downedYharon)
		{
			damageMult += 0.2f;
			defense += 12;
		}
		if (DownedBossSystem.downedExoMechs)
		{
			damageMult += 0.6f;
			defense += 20;
		}
		if (DownedBossSystem.downedCalamitas)
		{
			damageMult += 0.6f;
			defense += 20;
		}
	}

	public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
	{
		if (npc.type == 441 && projectile.type == ModContent.ProjectileType<SlickCaneProjectile>())
		{
			return true;
		}
		return base.CanBeHitByProjectile(npc, projectile);
	}

	public override void ModifyShop(NPCShop shop)
	{
		int type = shop.NpcType;
		Condition spelunkerGlowCondition = new Condition(Language.GetText("Conditions.NightDayFullMoon"), () => !Main.dayTime || Main.GetMoonPhase() == MoonPhase.Full);
		Condition hasFlareGunUpgrade = new Condition(CalamityUtils.GetText("Condition.HasFlareGun"), () => (Main.LocalPlayer.HasItem(ModContent.ItemType<FirestormCannon>()) || Main.LocalPlayer.HasItem(ModContent.ItemType<SpectralstormCannon>())) && !Main.LocalPlayer.HasItem(930));
		Condition bestiaryProgressLacewing = new Condition(CalamityUtils.GetText("Condition.LacewingBestiary"), () => Main.GetBestiaryProgressReport().CompletionPercent >= 0.4f);
		if (type == 17)
		{
			shop.InsertBefore(28, 31).InsertAfter(189, 2997, Condition.HappyEnoughToSellPylons).InsertAfter(346, 576)
				.InsertAfter(931, 931, hasFlareGunUpgrade)
				.InsertAfter(1614, 1614, hasFlareGunUpgrade)
				.AddWithCustomValue(52, Item.buyPrice(0, 5), Condition.NpcIsPresent(ModContent.NPCType<Bandit>()));
		}
		if (type == 207)
		{
			shop.Add<DefiledFlameDye>(new Condition[1] { Condition.Hardmode }).AddWithCustomValue(3349, Item.buyPrice(0, 15));
		}
		if (type == 38)
		{
			shop.Add<DeepcoreGK2>(new Condition[1] { Condition.DownedMechBossAny });
		}
		if (type == 19)
		{
			shop.Add<M1Garand>(new Condition[1] { Condition.DownedSkeletron }).Add<P90>(new Condition[1] { Condition.Hardmode }).AddWithCustomValue(964, Item.buyPrice(0, 25), Condition.DownedQueenBee)
				.AddWithCustomValue(1265, Item.buyPrice(0, 50), Condition.DownedPlantera);
		}
		if (type == 353)
		{
			shop.Add<StealthHairDye>(new Condition[1] { CalamityConditions.PlayerHasRogueArmor }).Add<WingTimeHairDye>(new Condition[1] { CalamityConditions.PlayerHasWings }).Add<AdrenalineHairDye>(new Condition[1] { CalamityConditions.InRevengeanceMode })
				.Add<RageHairDye>(new Condition[1] { CalamityConditions.InRevengeanceMode })
				.AddWithCustomValue(3352, Item.buyPrice(0, 15));
		}
		if (type == 209)
		{
			shop.Add<MartianDistressRemote>(new Condition[1] { Condition.DownedGolem }).Add<LionHeart>(new Condition[1] { CalamityConditions.DownedPolterghast });
		}
		if (type == 20)
		{
			shop.InsertAfter(5214, ModContent.ItemType<CinderBlossomSeeds>(), Condition.DownedSkeletron).InsertAfter(745, 59, Condition.CrimsonWorld, Condition.InGraveyard, Condition.PreHardmode).InsertAfter(745, 2171, Condition.CorruptWorld, Condition.InGraveyard, Condition.PreHardmode)
				.InsertAfter(4505, ModContent.ItemType<AstralGrassSeeds>(), Condition.NotBloodMoon, Condition.Hardmode)
				.AddWithCustomValue(208, Item.buyPrice(0, 3))
				.AddWithCustomValue(223, Item.buyPrice(0, 15))
				.Add<RomajedaOrchid>(Array.Empty<Condition>());
		}
		if (type == 107)
		{
			shop.Add<StatMeter>(Array.Empty<Condition>()).Add(1923, Condition.NpcIsPresent(124));
		}
		if (type == 124)
		{
			shop.AddWithCustomValue(2325, Item.buyPrice(0, 2), Condition.HappyEnoughToSellPylons).AddWithCustomValue(4818, Item.buyPrice(0, 15));
		}
		if (type == 54)
		{
			shop.Add<CounterScarf>(Array.Empty<Condition>()).AddWithCustomValue(327, Item.buyPrice(0, 15), Condition.Hardmode).Add<GodSlayerHornedHelm>(new Condition[1] { CalamityConditions.DownedDevourerOfGods })
				.Add<GodSlayerVisage>(new Condition[1] { CalamityConditions.DownedDevourerOfGods })
				.Add<SilvaHelm>(new Condition[1] { CalamityConditions.DownedDevourerOfGods })
				.Add<SilvaHornedHelm>(new Condition[1] { CalamityConditions.DownedDevourerOfGods })
				.Add<SilvaMask>(new Condition[1] { CalamityConditions.DownedDevourerOfGods });
		}
		if (type == 227)
		{
			shop.AddWithCustomValue(3350, Item.buyPrice(0, 15)).Add(ModContent.ItemType<CalamityCanvas2023>()).Add(ModContent.ItemType<CalamityCanvas2024>());
		}
		if (type == 178)
		{
			shop.InsertAfter(781, ModContent.ItemType<AstralSolution>(), Condition.NotRemixWorld).InsertAfter(782, 782, Condition.InGraveyard, Condition.CrimsonWorld, Condition.NotRemixWorld).InsertAfter(784, 784, Condition.InGraveyard, Condition.CorruptWorld, Condition.NotRemixWorld)
				.Add<LucisHairstyle>(Array.Empty<Condition>())
				.Add<LucisMilitaryUniform>(Array.Empty<Condition>())
				.Add<LucisBoots>(Array.Empty<Condition>())
				.Add<LucisSight>(Array.Empty<Condition>());
		}
		if (type == 108)
		{
			shop.Add<HowlsHeart>(Array.Empty<Condition>()).AddWithCustomValue(113, Item.buyPrice(0, 25)).Add<ResilientCandle>(Array.Empty<Condition>())
				.Add<SpitefulCandle>(Array.Empty<Condition>())
				.Add<VigorousCandle>(Array.Empty<Condition>())
				.Add<WeightlessCandle>(Array.Empty<Condition>());
		}
		if (type == 228)
		{
			shop.InsertAfter(4417, ModContent.ItemType<SunkenSeaFountain>()).InsertAfter(4417, ModContent.ItemType<SulphurousFountainItem>()).InsertAfter(4417, ModContent.ItemType<AbyssFountainItem>())
				.InsertAfter(4417, ModContent.ItemType<AstralFountainItem>())
				.InsertAfter(4417, ModContent.ItemType<BrimstoneLavaFountainItem>());
		}
		if (type == 208)
		{
			shop.Add(2756, Condition.HappyEnoughToSellPylons);
		}
		if (type == 663)
		{
			Mod musicMod = ExternalMods.musicMod;
			musicMod.TryFind<ModItem>("Interlude1MusicBox", out var interlude1Box);
			musicMod.TryFind<ModItem>("Interlude2MusicBox", out var interlude2Box);
			musicMod.TryFind<ModItem>("Interlude3MusicBox", out var interlude3Box);
			musicMod.TryFind<ModItem>("DevourerofGodsEulogyMusicBox", out var eulogyBox);
			shop.InsertAfter(5044, interlude1Box.Type, CalamityConditions.DownedCalamitasClone).InsertAfter(5044, interlude2Box.Type, Condition.DownedMoonLord).InsertAfter(5044, interlude3Box.Type, CalamityConditions.DownedYharon)
				.InsertAfter(5044, eulogyBox.Type, CalamityConditions.DownedDevourerOfGods)
				.AddWithCustomValue(5065, Item.buyPrice(1), Condition.Hardmode)
				.AddWithCustomValue(ModContent.ItemType<ForgivenessPainting>(), Item.buyPrice(0, 15), Condition.NpcIsPresent(ModContent.NPCType<BrimstoneWitch>()))
				.Add<LanternCenter>(Array.Empty<Condition>());
		}
		if (type == 453)
		{
			shop.InsertAfter(188, ModContent.ItemType<CalciumPotion>()).InsertAfter(188, 5041).InsertAfter(5377, 5377, spelunkerGlowCondition, hasFlareGunUpgrade)
				.AddWithCustomValue(682, Item.buyPrice(0, 25), Condition.Hardmode)
				.AddWithCustomValue<GiantShell>(Item.buyPrice(0, 15), Array.Empty<Condition>())
				.AddWithCustomValue<CrawCarapace>(Item.buyPrice(0, 15), Array.Empty<Condition>());
		}
		if (type == 633)
		{
			shop.Add(4961, bestiaryProgressLacewing);
		}
		if (type == 160)
		{
			shop.Add<OddMushroom>(Array.Empty<Condition>());
		}
	}
}
