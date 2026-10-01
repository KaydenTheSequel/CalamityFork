using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Bonebreaker : ModItem, ILocalizedModType, IModType
{
	public const int BaseDamage = 55;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.useStyle = 1;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.damage = 55;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<BonebreakerProjectile>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3378, 150).AddIngredient<CorrodedFossil>(15).AddTile(16)
			.Register();
	}
}
