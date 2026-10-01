using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class GacruxianMollusk : RogueWeapon
{
	public static float Knockback = 5f;

	public static float Speed = 15f;

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 22;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.damage = 36;
		base.Item.knockBack = Knockback;
		base.Item.autoReuse = true;
		base.Item.useTime = 26;
		base.Item.useAnimation = 26;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<GacruxianProj>();
		base.Item.shootSpeed = Speed;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<GacruxianProj>(), damage, knockback, player.whoAmI, 0f, 1f);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
