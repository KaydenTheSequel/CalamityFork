using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AquamarineStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 84;
		base.Item.damage = 17;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 3;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item43;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AquamarineBolt>();
		base.Item.shootSpeed = 14f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		for (int index = 0; index < 2; index++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
			int projectile = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[projectile].timeLeft = 180;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(739).AddIngredient<PearlShard>(3).AddIngredient<SeaPrism>(5)
			.AddIngredient<Navystone>(25)
			.AddTile(16)
			.Register();
		CreateRecipe().AddIngredient(740).AddIngredient<PearlShard>(3).AddIngredient<SeaPrism>(5)
			.AddIngredient<Navystone>(25)
			.AddTile(16)
			.Register();
	}
}
