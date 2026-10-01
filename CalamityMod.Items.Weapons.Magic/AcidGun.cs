using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AcidGun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 28;
		base.Item.damage = 28;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 9;
		base.Item.useAnimation = (base.Item.useTime = 45);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item13;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = ModContent.ProjectileType<AcidGunStream>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		int acid1 = Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(-8f)), type, damage, knockback, player.whoAmI);
		int acid2 = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		int acid3 = Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(8f)), type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(acid1))
		{
			Main.projectile[acid1].ai[0] = acid2;
			Main.projectile[acid1].ai[1] = acid3;
		}
		if (Main.projectile.IndexInRange(acid2))
		{
			Main.projectile[acid2].ai[0] = acid1;
			Main.projectile[acid2].ai[1] = acid3;
		}
		if (Main.projectile.IndexInRange(acid3))
		{
			Main.projectile[acid3].ai[0] = acid1;
			Main.projectile[acid3].ai[1] = acid2;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient<SulphuricScale>(12).AddTile(16)
			.Register();
	}
}
