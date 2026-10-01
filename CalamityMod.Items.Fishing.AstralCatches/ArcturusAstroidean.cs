using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.AstralCatches;

public class ArcturusAstroidean : ModItem, ILocalizedModType, IModType
{
	public static float FishingPowerBiomeMult = 1.1f;

	public new string LocalizationCategory => "Items.Fishing";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FishingPowerBiomeMult.ToString());

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 3;
		base.Item.bait = 40;
	}
}
