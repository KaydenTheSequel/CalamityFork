using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Purge" })]
public class NanoPurge : ModItem, ILocalizedModType, IModType
{
	public static int UseTime = 20;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 34;
		base.Item.damage = 135;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 8;
		base.Item.useAnimation = (base.Item.useTime = UseTime);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<NanoPurgeHoldout>();
		base.Item.shootSpeed = 16f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - spawnPosition, ModContent.ProjectileType<NanoPurgeHoldout>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2795).AddIngredient<UelibloomBar>(7).AddIngredient(1346, 100)
			.AddTile(134)
			.Register();
	}
}
