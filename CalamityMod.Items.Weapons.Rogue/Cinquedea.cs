using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Cinquedea : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.5f;

	public override float StealthVelocityMultiplier => 1.25f;

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.damage = 35;
		base.Item.rare = 3;
		base.Item.knockBack = 5f;
		base.Item.autoReuse = true;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<CinquedeaProj>();
		base.Item.shootSpeed = 10f;
		base.Item.value = Item.buyPrice(0, 10);
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 8f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CinquedeaProj>(), damage, knockback, player.whoAmI);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
