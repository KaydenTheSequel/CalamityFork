using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DevilsSunrise : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 420;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = 25;
		base.Item.useTime = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 4f;
		base.Item.autoReuse = false;
		base.Item.useStyle = 5;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<DevilsSunriseProj>();
		base.Item.shootSpeed = 24f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<DevilsSunriseCyclone>()] <= 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
		player.Calamity().rightClickListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4144).AddIngredient<BloodstoneCore>(25).AddIngredient<ScorchedBone>(10)
			.AddTile(134)
			.Register();
	}
}
