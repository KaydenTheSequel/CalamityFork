using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class GiantTortoiseShell : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 30;
		base.Item.defense = 10;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.noKnockback = true;
		calamityPlayer.tortShell = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GiantShell>().AddIngredient(1328).AddTile(16)
			.Register();
	}
}
