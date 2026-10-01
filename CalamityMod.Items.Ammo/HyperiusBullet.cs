using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class HyperiusBullet : ModItem, ILocalizedModType, IModType
{
	public static float overflowEfficency = 1.2f;

	public static readonly SoundStyle hit = new SoundStyle("CalamityMod/Sounds/Item/HyperiusOverflow");

	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 30;
		base.Item.damage = 12;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 16);
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<HyperiusBulletProj>();
		base.Item.shootSpeed = 5f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(333).AddIngredient(97, 333).AddIngredient<LifeAlloy>().AddTile(134)
			.Register();
	}
}
