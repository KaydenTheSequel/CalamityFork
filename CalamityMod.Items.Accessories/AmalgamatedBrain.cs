using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AmalgamatedBrain : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int NimbusDamage => 18.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.rBrain = true;
		calamityPlayer.aBrain = true;
		player.brainOfConfusionItem = base.Item;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RottenBrain>().AddIngredient(3223).AddIngredient(521, 3)
			.AddTile(16)
			.Register();
	}
}
