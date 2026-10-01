using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class FellerofEvergreens : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 18;
		base.Item.knockBack = 5f;
		base.Item.useTime = 17;
		base.Item.useAnimation = 25;
		base.Item.axe = 20;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnySilverBar", 18).AddIngredient(620, 18).AddTile(16)
			.Register();
	}
}
