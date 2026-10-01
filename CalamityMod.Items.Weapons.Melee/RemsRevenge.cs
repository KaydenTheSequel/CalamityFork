using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.MaceFlails;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class RemsRevenge : ModItem, ILocalizedModType, IModType
{
	public static int WitherDefenseReduction = 20;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(WitherDefenseReduction);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 34;
		base.Item.damage = 188;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<RemsRevengeProj>();
		base.Item.shootSpeed = 12f;
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(163).AddIngredient(3467, 5).AddIngredient<Lumenyl>(10)
			.AddTile(134)
			.Register();
	}
}
