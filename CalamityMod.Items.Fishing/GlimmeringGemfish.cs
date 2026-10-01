using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public class GlimmeringGemfish : ModItem, ILocalizedModType, IModType
{
	public static List<int> LootDisplay = new List<int> { 181, 180, 177, 179, 178, 182, 999 };

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
		int gemMin = 1;
		int gemMax = 3;
		itemLoot.Add(181, 2, gemMin, gemMax);
		itemLoot.Add(180, 2, gemMin, gemMax);
		itemLoot.Add(177, 4, gemMin, gemMax);
		itemLoot.Add(179, 4, gemMin, gemMax);
		itemLoot.Add(178, 8, gemMin, gemMax);
		itemLoot.Add(182, 10, gemMin, gemMax);
		itemLoot.Add(999, 8, gemMin, gemMax);
		Mod thorium = ExternalMods.thorium;
		if (thorium != null)
		{
			ModItem aquamarine = thorium.Find<ModItem>("Aquamarine");
			if (aquamarine != null)
			{
				itemLoot.Add(aquamarine.Type, 4, gemMin, gemMax);
			}
			else
			{
				CalamityMod.Log.Warn((object)"Could not find Thorium Aquamarine gem. This item will not be added to Glimmering Gemfish.");
			}
			ModItem opal = thorium.Find<ModItem>("Opal");
			if (opal != null)
			{
				itemLoot.Add(opal.Type, 4, gemMin, gemMax);
			}
			else
			{
				CalamityMod.Log.Warn((object)"Could not find Thorium Opal gem. This item will not be added to Glimmering Gemfish.");
			}
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		int currentItem = (int)(Main.GlobalTimeWrappedHourly * 1.5f) % LootDisplay.Count;
		list.FindAndReplace("[ITEMS]", $"[i:{LootDisplay[currentItem]}]");
	}
}
