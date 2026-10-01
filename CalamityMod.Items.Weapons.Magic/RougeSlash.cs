using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class RougeSlash : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.mana = 30;
		base.Item.width = 28;
		base.Item.damage = 119;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.height = 32;
		base.Item.useTime = 19;
		base.Item.useAnimation = 19;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.UseSound = SoundID.Item91;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<RougeSlashLarge>();
		base.Item.shootSpeed = 24f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<RougeSlashLarge>(), damage, knockback, player.whoAmI);
		Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<RougeSlashMedium>(), damage, knockback, player.whoAmI);
		Projectile.NewProjectile(source, position, velocity * 0.6f, ModContent.ProjectileType<RougeSlashSmall>(), damage / 2, knockback, player.whoAmI);
		return false;
	}
}
