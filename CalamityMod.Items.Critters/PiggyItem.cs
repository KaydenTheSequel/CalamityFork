using CalamityMod.NPCs.NormalNPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Critters;

public class PiggyItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Misc";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToCapturedCritter(ModContent.NPCType<Piggy>());
		base.Item.value = Item.sellPrice(0, 10);
		base.Item.rare = 1;
		base.Item.Calamity().donorItem = true;
	}
}
