using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AstrumAureus;

public class AddAureusGFBDrop : ModSystem
{
	public override void PostSetupRecipes()
	{
		List<int> luminiteStuff = new List<int>();
		for (int i = 0; i < Main.recipe.Length; i++)
		{
			if (Main.recipe[i].ContainsIngredient(3467) && !luminiteStuff.Contains(Main.recipe[i].createItem.type))
			{
				luminiteStuff.Add(Main.recipe[i].createItem.type);
			}
		}
		LeadingConditionRule GFBOnly = new LeadingConditionRule(DropHelper.GFB);
		GFBOnly.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, luminiteStuff.ToArray()));
		Main.ItemDropsDB.RegisterToNPCNetId(ModContent.NPCType<AstrumAureus>(), GFBOnly);
	}
}
