using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class NeptunesBounty : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SpinSound = new SoundStyle("CalamityMod/Sounds/Item/SpinningWoosh")
	{
		Volume = 0.65f
	};

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 122;
		base.Item.height = 122;
		base.Item.damage = 444;
		base.Item.knockBack = 9f;
		base.Item.useTime = 65;
		base.Item.useAnimation = 65;
		base.Item.shoot = ModContent.ProjectileType<NeptunesBountyProjectile>();
		base.Item.shootSpeed = 3f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssBlade>().AddIngredient<ReaperTooth>(6).AddIngredient<RuinousSoul>(5)
			.AddTile(134)
			.Register();
	}
}
