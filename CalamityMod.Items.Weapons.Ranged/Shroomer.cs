using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Shroomer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 90;
		base.Item.height = 28;
		base.Item.damage = 150;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 35;
		base.Item.useAnimation = 35;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 9.75f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item40;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 10f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 35f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-25f, 0f);
	}

	public override void HoldItem(Player player)
	{
		player.scope = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Shroom>(), (int)((double)damage * 0.5), knockback, player.whoAmI);
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1254).AddIngredient<Fungicide>().AddIngredient(3459, 6)
			.AddTile(412)
			.Register();
	}
}
