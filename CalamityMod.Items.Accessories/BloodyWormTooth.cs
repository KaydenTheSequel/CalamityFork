using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BloodyWormTooth : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 15;
		base.Item.defense = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().bloodyWormTooth = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RottenBrain>().AddTile(114).AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
	}
}
