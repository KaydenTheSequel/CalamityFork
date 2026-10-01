using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class Starcore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 18;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 40;
		base.Item.rare = 9;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StarblightSoot>(25).AddIngredient<AureusCell>(8).AddIngredient<AstralBar>(4)
			.AddTile(412)
			.Register();
	}
}
