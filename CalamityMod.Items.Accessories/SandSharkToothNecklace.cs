using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SandSharkToothNecklace : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 44;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<GenericDamageClass>() += 0.1f;
		player.GetArmorPenetration<GenericDamageClass>() += 10f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3212).AddIngredient(935).AddIngredient<GrandScale>()
			.AddTile(114)
			.Register();
	}
}
