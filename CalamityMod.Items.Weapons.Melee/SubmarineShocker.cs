using CalamityMod.Projectiles.Melee.Shortswords;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SubmarineShocker : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 13;
		base.Item.damage = 90;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 16);
		base.Item.shoot = ModContent.ProjectileType<SubmarineShockerProj>();
		base.Item.shootSpeed = 2f;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool MeleePrefix()
	{
		return true;
	}
}
