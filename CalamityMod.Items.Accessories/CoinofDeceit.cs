using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class CoinofDeceit : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 1;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().stealthStrike90Cost = true;
		player.GetCritChance<ThrowingDamageClass>() += 6f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyCopperBar", 12).AddRecipeGroup("AnyEvilBar", 8).AddTile(16)
			.Register();
	}
}
