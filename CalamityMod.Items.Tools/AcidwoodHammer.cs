using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class AcidwoodHammer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.damage = 10;
		base.Item.knockBack = 3.5f;
		base.Item.useTime = 9;
		base.Item.useAnimation = 20;
		base.Item.hammer = 25;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.width = 40;
		base.Item.height = 40;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(8).AddTile(18).Register();
	}
}
