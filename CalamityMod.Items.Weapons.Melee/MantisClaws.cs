using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class MantisClaws : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 20;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<MantisClawHoldout>();
		base.Item.useStyle = 10;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool MeleePrefix()
	{
		return true;
	}
}
