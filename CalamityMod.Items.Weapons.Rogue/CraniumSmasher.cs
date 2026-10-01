using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CraniumSmasher : RogueWeapon
{
	private bool throwExplosive;

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 140;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<CraniumSmasherProj>();
		base.Item.shootSpeed = 20f;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.Calamity().StealthStrikeAvailable())
		{
			type = ModContent.ProjectileType<CraniumSmasherStealth>();
			return;
		}
		type = (throwExplosive ? ModContent.ProjectileType<CraniumSmasherExplosive>() : ModContent.ProjectileType<CraniumSmasherProj>());
		throwExplosive = !throwExplosive;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}
}
