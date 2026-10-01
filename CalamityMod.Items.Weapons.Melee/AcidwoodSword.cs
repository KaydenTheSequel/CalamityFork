using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AcidwoodSword : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.damage = 11;
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 18);
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(7).AddTile(18).Register();
	}
}
