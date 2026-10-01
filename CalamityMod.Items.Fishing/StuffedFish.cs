using System.Collections.Generic;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Items.Placeables.Crags;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public class StuffedFish : ModItem, ILocalizedModType, IModType
{
	public static List<int> HerbDisplay = new List<int> { 313, 315, 317, 2358, 314, 316, 318 };

	public static List<int> SeedDisplay = new List<int>
	{
		307,
		309,
		311,
		2357,
		308,
		310,
		312,
		62,
		195,
		194,
		5214,
		ModContent.ItemType<CinderBlossomSeeds>(),
		1828
	};

	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 10;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 30;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.GoodieBags;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		int herbMin = 1;
		int herbMax = 3;
		int seedMin = 2;
		int seedMax = 5;
		itemLoot.Add(313, 4, herbMin, herbMax);
		itemLoot.Add(315, 4, herbMin, herbMax);
		itemLoot.Add(317, 4, herbMin, herbMax);
		itemLoot.Add(2358, 4, herbMin, herbMax);
		itemLoot.Add(314, 4, herbMin, herbMax);
		itemLoot.Add(316, 4, herbMin, herbMax);
		itemLoot.Add(318, 4, herbMin, herbMax);
		itemLoot.Add(307, 5, seedMin, seedMax);
		itemLoot.Add(309, 5, seedMin, seedMax);
		itemLoot.Add(311, 5, seedMin, seedMax);
		itemLoot.Add(2357, 5, seedMin, seedMax);
		itemLoot.Add(308, 5, seedMin, seedMax);
		itemLoot.Add(310, 5, herbMin, herbMax);
		itemLoot.Add(312, 5, seedMin, seedMax);
		itemLoot.Add(62, 10, seedMin, seedMax);
		itemLoot.Add(195, 10, seedMin, seedMax);
		itemLoot.Add(194, 10, seedMin, seedMax);
		itemLoot.Add(5214, 20, seedMin, seedMax);
		itemLoot.Add(ModContent.ItemType<CinderBlossomSeeds>(), 20, seedMin, seedMax);
		itemLoot.Add(1828, 20, seedMin, seedMax);
		itemLoot.AddIf(() => !WorldGen.crimson, 59, 20, seedMin, seedMax);
		itemLoot.AddIf(() => WorldGen.crimson, 2171, 20, seedMin, seedMax);
		itemLoot.AddIf(() => Main.hardMode, 369, 20, seedMin, seedMax);
		itemLoot.AddIf(() => Main.hardMode, ModContent.ItemType<AstralGrassSeeds>(), 20, seedMin, seedMax);
		Mod thorium = ExternalMods.thorium;
		if (thorium != null)
		{
			ModItem marineKelp = thorium.Find<ModItem>("MarineKelp");
			ModItem marineKelpSeeds = thorium.Find<ModItem>("MarineKelpSeeds");
			if (marineKelp != null && marineKelpSeeds != null)
			{
				itemLoot.Add(marineKelp.Type, 4, herbMin, herbMax);
				itemLoot.Add(marineKelpSeeds.Type, 10, seedMin, seedMax);
			}
			else
			{
				CalamityMod.Log.Warn((object)"Could not find either Marine Kelp or Marine Kelp Seeds from Thorium. These items will not be added to Stuffed Fish.");
			}
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		int currentHerb = (int)(Main.GlobalTimeWrappedHourly * 1.5f) % HerbDisplay.Count;
		list.FindAndReplace("[HERBS]", $"[i:{HerbDisplay[currentHerb]}]");
		int currentSeed = (int)(Main.GlobalTimeWrappedHourly * 1.5f) % SeedDisplay.Count;
		list.FindAndReplace("[SEEDS]", $"[i:{SeedDisplay[currentSeed]}]");
	}
}
