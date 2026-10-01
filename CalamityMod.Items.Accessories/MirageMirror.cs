using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class MirageMirror : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.stealthGenStandstill += 0.25f;
		calamityPlayer.stealthGenMoving += 0.12f;
		player.aggro -= 200;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(50).AddIngredient(236).AddIngredient(154, 50)
			.AddTile(114)
			.Register();
		CreateRecipe().AddIngredient(3199).AddIngredient(236).AddIngredient(154, 50)
			.AddTile(114)
			.Register();
	}
}
