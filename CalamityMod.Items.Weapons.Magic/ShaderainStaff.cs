using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ShaderainStaff : ModItem, ILocalizedModType, IModType
{
	public const int RainAmount = 2;

	public const float LesserRainVELMultiplier = 0.9f;

	public const float HigherRainVELMultiplier = 1.2f;

	public const float GravityStrenght = 0.15f;

	public const int FadeoutSpeed = 2;

	public const float CloudDMGMultiplier = 1f;

	public const float CloudVELMultiplier = 1.25f;

	public const float DeaccelerationStrenght = 0.95f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 42);
		base.Item.damage = 19;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 34);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<Shaderain>();
		base.Item.shootSpeed = 11f;
		base.Item.UseSound = SoundID.Item66;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		for (int shadeRainIndex = 0; shadeRainIndex < 2; shadeRainIndex++)
		{
			Projectile.NewProjectile(source, player.Center, velocity * Main.rand.NextFloat(0.9f, 1.2f), type, damage, knockback, player.whoAmI);
		}
		Projectile.NewProjectile(source, player.Center, velocity * 1.25f, ModContent.ProjectileType<ShadeNimbusCloud>(), (int)((float)damage * 1f), knockback, player.whoAmI);
		return false;
	}
}
