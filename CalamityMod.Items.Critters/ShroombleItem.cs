using CalamityMod.NPCs.NormalNPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Critters;

public class ShroombleItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Misc";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToCapturedCritter(ModContent.NPCType<Shroomble>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}
}
