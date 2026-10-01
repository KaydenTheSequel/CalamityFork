using CalamityMod.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class GreatbayPickaxe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 9;
		base.Item.knockBack = 2f;
		base.Item.useTime = 8;
		base.Item.useAnimation = 16;
		base.Item.pick = 55;
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
		CreateRecipe().AddIngredient<SeaRemains>(3).AddTile(16).Register();
	}
}
