using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ReaperToothNecklace : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 50;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<GenericDamageClass>() += 0.2f;
		player.GetArmorPenetration<GenericDamageClass>() += 15f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SandSharkToothNecklace>().AddIngredient<ReaperTooth>(6).AddIngredient<DepthCells>(15)
			.AddTile(114)
			.Register();
	}
}
