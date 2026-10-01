using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class RuinousSoul : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 111;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 42;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1, 50);
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}
}
