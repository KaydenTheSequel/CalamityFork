using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "ShadecrystalTome" })]
public class ShadecrystalBarrage : ModItem, ILocalizedModType, IModType
{
	internal const float ShootSpeed = 2f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 24;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 7;
		base.Item.useAnimation = 14;
		base.Item.reuseDelay = 49;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item9;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ShadecrystalProjectile>();
		base.Item.shootSpeed = 2f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = 6;
		float maxSpread = 0.5f;
		Vector2 cachedVelocity = velocity;
		Vector2 newPosition = position + velocity.SafeNormalize(Vector2.UnitY) * 20f;
		for (int index = 0; index < projAmt; index++)
		{
			velocity += new Vector2(Main.rand.NextFloat(0f - maxSpread, maxSpread), Main.rand.NextFloat(0f - maxSpread, maxSpread));
			Projectile.NewProjectile(source, newPosition, velocity, type, damage, knockback, player.whoAmI, 0f, MathHelper.Lerp(1.04f, 1.09f, (float)index / (float)(projAmt - 1)));
			velocity = cachedVelocity;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(518).AddIngredient<CryonicBar>(6).AddTile(101)
			.Register();
	}
}
