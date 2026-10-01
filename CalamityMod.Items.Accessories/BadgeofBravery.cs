using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BadgeofBravery : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().badgeOfBravery = true;
		player.GetArmorPenetration<MeleeDamageClass>() += 5f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(490).AddIngredient<UelibloomBar>(2).AddTile(134)
			.Register();
	}
}
