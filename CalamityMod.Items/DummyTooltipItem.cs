using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items;

public abstract class DummyTooltipItem : ModItem, ILocalizedModType, IModType
{
	public override LocalizedText DisplayName => LocalizedText.Empty;

	public override LocalizedText Tooltip => LocalizedText.Empty;

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 0;
		ItemID.Sets.ItemsThatShouldNotBeInInventory[base.Type] = true;
	}
}
