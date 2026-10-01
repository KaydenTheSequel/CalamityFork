using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class AshenStalactite : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.15f;

	public override float StealthVelocityMultiplier => 0.6f;

	public override float StealthKnockbackMultiplier => 2.5f;

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 36;
		base.Item.damage = 37;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.maxStack = 1;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice / 4;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<AshenStalactiteProj>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.Calamity().StealthStrikeAvailable())
		{
			type = ModContent.ProjectileType<AshenStalagmiteProj>();
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
