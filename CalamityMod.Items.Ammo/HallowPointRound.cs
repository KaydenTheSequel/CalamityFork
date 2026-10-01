using System.Collections.Generic;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class HallowPointRound : ModItem, ILocalizedModType, IModType
{
	public static int BaseDamage = 12;

	public static int BonusDamageOnHit = 6;

	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 8;
		base.Item.height = 18;
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 3f;
		base.Item.value = Item.sellPrice(0, 0, 0, 12);
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<HallowPointRoundProj>();
		base.Item.shootSpeed = 6f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[DAMAGE]", ((int)Main.LocalPlayer.GetTotalDamage<RangedDamageClass>().ApplyTo(BaseDamage + BonusDamageOnHit)).ToString());
	}

	public override void AddRecipes()
	{
		CreateRecipe(100).AddIngredient(1432, 100).AddIngredient(1225).AddTile(18)
			.Register();
	}
}
