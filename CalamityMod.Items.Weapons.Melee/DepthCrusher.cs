using CalamityMod.Projectiles.Melee;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "DepthBlade" })]
public class DepthCrusher : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 50;
		base.Item.damage = 48;
		base.Item.knockBack = 7.25f;
		base.Item.useTime = 65;
		base.Item.useAnimation = 65;
		base.Item.shoot = ModContent.ProjectileType<DepthCrusherProjectile>();
		base.Item.shootSpeed = 6f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}
}
