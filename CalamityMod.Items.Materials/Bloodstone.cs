using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class Bloodstone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 112;
	}

	public override void SetDefaults()
	{
		base.Item.width = 13;
		base.Item.height = 10;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 60);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}
}
