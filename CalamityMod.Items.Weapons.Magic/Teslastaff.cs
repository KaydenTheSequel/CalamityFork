using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Teslastaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 38;
		base.Item.damage = 166;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.reuseDelay = 60;
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<Teslabeam>();
		base.Item.shootSpeed = 30f;
		base.Item.UseSound = CommonCalamitySounds.LightningSound with
		{
			Pitch = 1.1f
		};
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4062).AddRecipeGroup("AnyCopperBar", 20).AddIngredient<EssenceofSunlight>(6)
			.AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
