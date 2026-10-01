using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "IcyBullet" })]
public class HailstormBullet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 20;
		base.Item.damage = 12;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.consumable = true;
		base.Item.knockBack = 2f;
		base.Item.value = Item.buyPrice(0, 0, 0, 80);
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<HailstormBulletProj>();
		base.Item.shootSpeed = 0.3f;
		base.Item.ammo = AmmoID.Bullet;
		base.Item.maxStack = Item.CommonMaxStack;
	}
}
