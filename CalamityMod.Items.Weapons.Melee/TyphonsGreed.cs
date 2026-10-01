using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TyphonsGreed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<TyphonsGreedStaff>();
		base.Item.shootSpeed = 24f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Voidstone>(30).AddIngredient<DepthCells>(30).AddTile(134)
			.Register();
	}
}
