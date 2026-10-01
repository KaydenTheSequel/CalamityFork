using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RottenBrain : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int NimbusDamage => 8.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().rBrain = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodyWormTooth>().AddTile(114).AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
	}
}
