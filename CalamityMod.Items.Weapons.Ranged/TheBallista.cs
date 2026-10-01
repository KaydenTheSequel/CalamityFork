using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TheBallista : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 70;
		base.Item.damage = 88;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 28);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BallistaGreatArrow>();
		base.Item.shootSpeed = 20f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.CheckWoodenAmmo(type, player))
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BallistaGreatArrow>(), damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(682).AddIngredient(3783).AddIngredient(1508, 10)
			.AddTile(134)
			.Register();
	}
}
