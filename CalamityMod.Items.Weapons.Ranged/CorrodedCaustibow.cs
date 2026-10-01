using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class CorrodedCaustibow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 45;
		base.Item.useAnimation = 45;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<Shell>();
		base.Item.shootSpeed = 5f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.CheckWoodenAmmo(type, player))
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CorrodedShell>(), damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Shellshooter>().AddIngredient<Toxibow>().AddIngredient<CorrodedFossil>(10)
			.AddTile(16)
			.Register();
	}
}
