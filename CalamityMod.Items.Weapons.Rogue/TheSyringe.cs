using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class TheSyringe : RogueWeapon
{
	public static float Knockback = 5f;

	public static float Speed = 15f;

	public override float StealthDamageMultiplier => 1.2f;

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 50;
		base.Item.damage = 60;
		base.Item.knockBack = Knockback;
		base.Item.autoReuse = true;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item106;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<TheSyringeProj>();
		base.Item.shootSpeed = Speed;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
