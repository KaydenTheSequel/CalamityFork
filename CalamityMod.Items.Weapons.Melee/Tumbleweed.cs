using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.MaceFlails;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Tumbleweed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 36;
		base.Item.damage = 78;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<TumbleweedFlail>();
		base.Item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(220).AddIngredient(3794, 5).AddIngredient<GrandScale>()
			.AddTile(134)
			.Register();
	}
}
