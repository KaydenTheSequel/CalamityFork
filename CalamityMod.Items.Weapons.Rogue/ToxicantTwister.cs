using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ToxicantTwister : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.4f;

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 46;
		base.Item.damage = 272;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ToxicantTwisterProj>();
		base.Item.shootSpeed = 20f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		float rotationAngle = MathHelper.ToRadians(20f);
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (int i = 0; i < 2; i++)
			{
				int proj = Projectile.NewProjectile(source, position, velocity.RotatedBy(rotationAngle), type, damage, knockback, player.whoAmI, 0f, 0f, i);
				int proj2 = Projectile.NewProjectile(source, position, velocity.RotatedBy(rotationAngle * 0.5f) * 0.9f, type, damage, knockback, player.whoAmI, 0f, 0f, 2 + i);
				rotationAngle *= -1f;
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
				if (proj2.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj2].Calamity().stealthStrike = true;
				}
			}
		}
		else
		{
			for (int j = 0; j < 2; j++)
			{
				Projectile.NewProjectile(source, position, velocity.RotatedBy(rotationAngle), type, damage, knockback, player.whoAmI, 0f, 0f, j);
				Projectile.NewProjectile(source, position, velocity.RotatedBy(rotationAngle * 0.5f) * 0.9f, type, damage, knockback, player.whoAmI, 0f, 0f, 2 + j);
				rotationAngle *= -1f;
			}
		}
		return false;
	}
}
