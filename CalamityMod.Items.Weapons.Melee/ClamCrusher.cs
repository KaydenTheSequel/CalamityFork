using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class ClamCrusher : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 60;
		base.Item.damage = 150;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = 50;
		base.Item.useAnimation = 50;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<ClamCrusherFlail>();
		base.Item.shootSpeed = 18f;
	}
}
