using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class VenusianTrident : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 68;
		base.Item.damage = 2800;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 40;
		base.Item.useTime = 65;
		base.Item.useAnimation = 65;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 9f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.UseSound = SoundID.Item45;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VenusianBolt>();
		base.Item.shootSpeed = 13f;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override Vector2? HoldoutOrigin()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(15f, 15f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1445).AddIngredient<RuinousSoul>(2).AddIngredient<TwistingNether>()
			.AddTile(134)
			.Register();
	}
}
