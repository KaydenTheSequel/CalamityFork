using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SquirrelSquireStaff : ModItem, ILocalizedModType, IModType
{
	public static float ProjectileVelocity = 20f;

	public static int TimeBeforeFalling = 30;

	public static float DistanceToMortarShoot = 240f;

	public static int ProjectileTimeAlive = 180;

	public static float ProjectileGravity = 0.5f;

	public static int ProjectileAoERadiusSize = 24;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.shoot = ModContent.ProjectileType<SquirrelSquireMinion>();
		base.Item.knockBack = 0.5f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.mana = 10;
		base.Item.width = 46;
		base.Item.height = 52;
		base.Item.noMelee = true;
		base.Item.sentry = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item44;
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 initialSpawn = player.GetFarthestSpawnPositionOnLine(position, velocity.X, velocity.Y);
		int dontLoopForever = 0;
		while ((!Collision.CanHit(initialSpawn, 0, 0, initialSpawn + Vector2.UnitX * 20f, 0, 0) || !Collision.CanHit(initialSpawn, 0, 0, initialSpawn - Vector2.UnitX * 20f, 0, 0) || !Collision.CanHit(initialSpawn, 0, 0, initialSpawn + Vector2.UnitY * 24f, 0, 0) || !Collision.CanHit(initialSpawn, 0, 0, initialSpawn - Vector2.UnitY * 24f, 0, 0)) && dontLoopForever < 200)
		{
			initialSpawn = Vector2.Lerp(player.Center, initialSpawn, 0.99f);
			dontLoopForever++;
		}
		Projectile.NewProjectile(source, initialSpawn, Vector2.Zero, type, damage, knockback, player.whoAmI);
		player.UpdateMaxTurrets();
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("Wood", 10).AddIngredient(27).AddTile(18)
			.Register();
	}
}
