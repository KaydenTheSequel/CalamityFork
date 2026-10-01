using CalamityMod.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class ReefclawHamaxe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 15;
		base.Item.knockBack = 6f;
		base.Item.useTime = 11;
		base.Item.useAnimation = 29;
		base.Item.hammer = 60;
		base.Item.axe = 11;
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
		CreateRecipe().AddIngredient<SeaRemains>(2).AddTile(16).Register();
	}
}
