using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "BrimstoneFlamesprayer" })]
public class HavocsBreath : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 26;
		base.Item.damage = 62;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 35;
		base.Item.reuseDelay = 20;
		base.Item.useLimitPerAnimation = 7;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.UseSound = SoundID.Item34;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BrimstoneFireFriendly>();
		base.Item.shootSpeed = 8.5f;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			Projectile.NewProjectile(source, position + velocity * 4f, velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.9f, 1.25f), type, damage, knockback, player.whoAmI);
		}
		return false;
	}
}
