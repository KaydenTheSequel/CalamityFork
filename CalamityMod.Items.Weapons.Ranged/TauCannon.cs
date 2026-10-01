using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TauCannon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.damage = 620;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 180);
		base.Item.shoot = ModContent.ProjectileType<TauCannonHoldout>();
		base.Item.shootSpeed = 15f;
		base.Item.knockBack = 4f;
		base.Item.width = 146;
		base.Item.height = 52;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.useStyle = 5;
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArcNovaDiffuser>().AddIngredient<MysteriousCircuitry>(10).AddIngredient<DubiousPlating>(15)
			.AddIngredient<AstralBar>(10)
			.AddIngredient<RuinousSoul>(2)
			.AddTile(134)
			.Register();
	}
}
