using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags.MiscGrabBags;

public class StarterBag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.TreasureBags";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 0;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 24;
		base.Item.consumable = true;
		base.Item.rare = 1;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		LeadingConditionRule leadingConditionRule = itemLoot.DefineConditionalDropSet(() => WorldGen.SavedOreTiers.Copper == 166);
		leadingConditionRule.Add(3502);
		leadingConditionRule.Add(3498);
		leadingConditionRule.Add(740);
		leadingConditionRule.OnFailedConditions(new CommonDrop(3508, 1));
		leadingConditionRule.OnFailedConditions(new CommonDrop(3504, 1));
		leadingConditionRule.OnFailedConditions(new CommonDrop(739, 1));
		itemLoot.Add(40, 1, 100, 100);
		itemLoot.Add(ModContent.ItemType<SquirrelSquireStaff>());
		itemLoot.Add(ModContent.ItemType<ThrowingBrick>(), 1, 150, 150);
		itemLoot.Add(109);
		leadingConditionRule.Add(3499);
		leadingConditionRule.OnFailedConditions(new CommonDrop(3505, 1));
		itemLoot.Add(965, 1, 50, 50);
		LeadingConditionRule mainRule = itemLoot.DefineConditionalDropSet(() => Main.netMode == 1);
		itemLoot.Add(2350, 1, 3, 3);
		mainRule.Add(2997, 1, 3, 3);
		itemLoot.Add(8, 1, 25, 25);
		Mod musicMod = ExternalMods.musicMod;
		if (musicMod != null)
		{
			itemLoot.Add(musicMod.Find<ModItem>("CalamityMusicbox").Type);
		}
		itemLoot.Add(ModContent.ItemType<LoreAwakening>());
		itemLoot.AddIf(getsLadPet, ModContent.ItemType<JoyfulHeart>());
		itemLoot.AddIf(getsHapuFruit, ModContent.ItemType<HapuFruit>());
		itemLoot.AddIf(getsSharkyPlush, ModContent.ItemType<SharkyPlush>());
		itemLoot.AddIf(getsGhostBracelet, ModContent.ItemType<GhostBracelet>());
		itemLoot.AddIf(getsXyksBlessing, ModContent.ItemType<XyksBlessingBlue>());
		itemLoot.AddIf(getsOracleHeadphones, ModContent.ItemType<OracleHeadphones>());
		itemLoot.AddIf(getsLittleE, ModContent.ItemType<LittleE>());
		itemLoot.AddIf(getsShimmeringRibbon, ModContent.ItemType<GlimmeringRibbon>());
		static bool getsGhostBracelet(DropAttemptInfo info)
		{
			return info.player.name == "Dandy";
		}
		static bool getsHapuFruit(DropAttemptInfo info)
		{
			return info.player.name == "Heart Plus Up";
		}
		static bool getsLadPet(DropAttemptInfo info)
		{
			string playerName = info.player.name;
			if (!(playerName == "Aleksh"))
			{
				return playerName == "Shark Lad";
			}
			return true;
		}
		static bool getsLittleE(DropAttemptInfo info)
		{
			string playerName = info.player.name.ToLower();
			if (playerName == "big e" || playerName == "bige")
			{
				return true;
			}
			return false;
		}
		static bool getsOracleHeadphones(DropAttemptInfo info)
		{
			string playerName = info.player.name;
			if (playerName == "Amber" || playerName == "Mishiro")
			{
				return true;
			}
			return false;
		}
		static bool getsSharkyPlush(DropAttemptInfo info)
		{
			string playerName = info.player.name;
			if (!(playerName == "CongratsIsTrash"))
			{
				return playerName == "CIT";
			}
			return true;
		}
		static bool getsShimmeringRibbon(DropAttemptInfo info)
		{
			string playerName = info.player.name.ToLower();
			if (playerName == "sagi" || playerName == "sagittariod")
			{
				return true;
			}
			return false;
		}
		static bool getsXyksBlessing(DropAttemptInfo info)
		{
			return info.player.name.Contains("Xyk");
		}
	}
}
