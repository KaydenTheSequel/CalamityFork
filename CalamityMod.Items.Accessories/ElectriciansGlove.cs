using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class ElectriciansGlove : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 5;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.electricianGlove = true;
		calamityPlayer.bloodyGlove = true;
		calamityPlayer.filthyGlove = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FilthyGlove>().AddIngredient(530, 100).AddRecipeGroup("AnyMythrilBar", 5)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient<BloodstainedGlove>().AddIngredient(530, 100).AddRecipeGroup("AnyMythrilBar", 5)
			.AddTile(134)
			.Register();
	}
}
