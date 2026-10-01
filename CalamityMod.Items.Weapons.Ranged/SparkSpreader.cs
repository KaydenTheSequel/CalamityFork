using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SparkSpreader : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 26;
		base.Item.damage = 16;
		base.Item.knockBack = 1f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.autoReuse = true;
		base.Item.useTime = 12;
		base.Item.useAnimation = 48;
		base.Item.reuseDelay = 12;
		base.Item.useLimitPerAnimation = 4;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
		base.Item.shootSpeed = 6f;
		base.Item.shoot = ModContent.ProjectileType<SparkSpreaderFire>();
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item34;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-4f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 10).AddIngredient(178).AddIngredient(23, 12)
			.AddTile(16)
			.Register();
	}
}
