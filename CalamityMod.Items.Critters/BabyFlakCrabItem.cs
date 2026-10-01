using CalamityMod.NPCs.AcidRain;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Critters;

public class BabyFlakCrabItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Misc";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToCapturedCritter(ModContent.NPCType<BabyFlakCrab>());
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 6;
	}
}
