using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Kylie : RogueWeapon
{
	public static float Speed = 14f;

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 50;
		base.Item.damage = 12;
		base.Item.knockBack = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shootSpeed = Speed;
		base.Item.shoot = ModContent.ProjectileType<KylieBoomerang>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
				Main.projectile[proj].penetrate = 7;
			}
			return false;
		}
		return true;
	}
}
