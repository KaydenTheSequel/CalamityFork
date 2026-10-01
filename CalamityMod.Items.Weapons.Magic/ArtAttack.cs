using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ArtAttack : ModItem, ILocalizedModType, IModType
{
	public const int MaxDamageBoostTime = 180;

	public const float MaxDamageBoostFactor = 18f;

	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/ArtAttackCast");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 70;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<ArtAttackHoldout>();
		base.Item.channel = true;
		base.Item.shootSpeed = 12f;
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(495).AddIngredient(1526).AddIngredient(502)
			.AddIngredient<AshesofCalamity>(5)
			.AddTile(134)
			.Register();
	}
}
