using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AethersWhisper : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 134;
		base.Item.height = 44;
		base.Item.damage = 504;
		base.Item.knockBack = 5.5f;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<AetherBeam>();
		base.Item.mana = 30;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = CommonCalamitySounds.LaserCannonSound;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlasmaRod>().AddIngredient<TwistingNether>(3).AddTile(134)
			.Register();
	}
}
