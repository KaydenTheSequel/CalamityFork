using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Melee;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AbyssBlade : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SpinSound = new SoundStyle("CalamityMod/Sounds/Item/SpinningWoosh")
	{
		Volume = 0.65f
	};

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 74;
		base.Item.damage = 123;
		base.Item.knockBack = 7.5f;
		base.Item.useTime = 65;
		base.Item.useAnimation = 65;
		base.Item.shoot = ModContent.ProjectileType<AbyssBladeProjectile>();
		base.Item.shootSpeed = 3f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DepthCrusher>().AddIngredient<Voidstone>(20).AddIngredient<DepthCells>(20)
			.AddTile(134)
			.Register();
	}
}
