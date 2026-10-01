using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class HolyFireBullet : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Explosion = new SoundStyle("CalamityMod/Sounds/Item/HolyFireBulletExplosion")
	{
		PitchVariance = 0.2f,
		Volume = 0.6f
	};

	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 22;
		base.Item.damage = 19;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 2f;
		base.Item.value = Item.sellPrice(0, 0, 0, 24);
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<HolyFireBulletProj>();
		base.Item.shootSpeed = 1f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(333).AddIngredient(1351, 333).AddIngredient<UnholyEssence>().AddTile(134)
			.Register();
	}
}
