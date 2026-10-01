using System.Collections.Generic;
using System.Linq;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class BloodwormItem : ModItem, ILocalizedModType, IModType
{
	public const int SpoofBaitNumber = 4444;

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.bait = 50;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.useAnimation = 15;
		base.Item.useTime = 10;
		base.Item.consumable = true;
		base.Item.noUseGraphic = true;
		base.Item.makeNPC = (short)ModContent.NPCType<BloodwormNormal>();
		base.Item.value = Item.sellPrice(0, 20);
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		TooltipLine? tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "BaitPower");
		if (tooltipLine != null)
		{
			tooltipLine.Text = Language.GetTextValue("GameUI.BaitPower", 4444);
		}
	}
}
