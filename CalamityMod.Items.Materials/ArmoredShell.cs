using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class ArmoredShell : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 107;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 34;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1, 40);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}
}
