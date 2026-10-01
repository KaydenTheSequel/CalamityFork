using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DefectiveSphere : RogueWeapon
{
	public static float Speed = 15f;

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 44;
		base.Item.damage = 85;
		base.Item.knockBack = 5f;
		base.Item.useAnimation = 13;
		base.Item.useTime = 13;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item15;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.shoot = ModContent.ProjectileType<SphereSpiked>();
		base.Item.shootSpeed = Speed;
	}

	public override bool CanUseItem(Player player)
	{
		if (!player.Calamity().StealthStrikeAvailable())
		{
			return player.ownedProjectileCounts[base.Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<SphereBladed>()] + player.ownedProjectileCounts[ModContent.ProjectileType<SphereYellow>()] + player.ownedProjectileCounts[ModContent.ProjectileType<SphereBlue>()] <= 4;
		}
		return true;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		type = Utils.SelectRandom<int>(Main.rand, type, ModContent.ProjectileType<SphereBladed>(), ModContent.ProjectileType<SphereYellow>(), ModContent.ProjectileType<SphereBlue>());
		if (player.Calamity().StealthStrikeAvailable())
		{
			velocity += Main.rand.NextVector2Square(-1.5f, 1.5f);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int[] array = new int[4]
			{
				ModContent.ProjectileType<SphereSpiked>(),
				ModContent.ProjectileType<SphereBladed>(),
				ModContent.ProjectileType<SphereYellow>(),
				ModContent.ProjectileType<SphereBlue>()
			};
			foreach (int projectileType in array)
			{
				int stealth = Projectile.NewProjectile(source, position, velocity, projectileType, damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		return true;
	}
}
