using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class IcicleArrow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 50;
		base.Item.damage = 6;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.consumable = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = Item.buyPrice(0, 0, 0, 80);
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<IcicleArrowProj>();
		base.Item.shootSpeed = 0.2f;
		base.Item.ammo = AmmoID.Arrow;
		base.Item.maxStack = Item.CommonMaxStack;
	}
}
