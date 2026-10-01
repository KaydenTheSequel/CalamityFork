using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BulletFilledShotgun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 24;
		base.Item.damage = 1;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 75);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<BouncingShotgunPellet>();
		base.Item.shootSpeed = 18f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.UseSound = SoundID.Item38;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.Calamity().donorItem = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-7f, 0f);
	}

	public override bool CanUseItem(Player player)
	{
		return CalamityGlobalItem.HasEnoughAmmo(player, base.Item, 5);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		type = base.Item.shoot;
		position += player.itemRotation.ToRotationVector2() * (float)player.direction * 48f;
		int bulletAmt = 40;
		for (int i = 0; i < bulletAmt; i++)
		{
			float newSpeedX = velocity.X + Main.rand.NextFloat(-15f, 15f);
			float newSpeedY = velocity.Y + Main.rand.NextFloat(-15f, 15f);
			Projectile.NewProjectile(source, position.X, position.Y, newSpeedX, newSpeedY, type, damage, knockback, player.whoAmI);
		}
		CalamityGlobalItem.ConsumeAdditionalAmmo(player, base.Item, 5);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(97, 100).AddRecipeGroup("IronBar", 7).AddIngredient<AerialiteBar>(3)
			.AddTile(16)
			.Register();
	}
}
