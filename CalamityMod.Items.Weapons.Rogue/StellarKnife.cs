using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class StellarKnife : RogueWeapon
{
	private int knifeCount = 10;

	private int knifeLimit = 20;

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
		base.Item.damage = 69;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 9);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<StellarKnifeProj>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 4f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable() && player.ownedProjectileCounts[base.Item.shoot] < knifeLimit)
		{
			int knifeAmt = knifeCount;
			if (player.ownedProjectileCounts[base.Item.shoot] + knifeCount >= knifeLimit)
			{
				knifeAmt = knifeLimit - player.ownedProjectileCounts[base.Item.shoot];
			}
			if (knifeAmt <= 0)
			{
				int knife = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
				if (knife.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[knife].Calamity().stealthStrike = true;
				}
			}
			int spread = 20;
			for (int i = 0; i < knifeCount; i++)
			{
				velocity.X *= 0.9f;
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X, velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int knife2 = Projectile.NewProjectile(source, position, perturbedspeed, type, damage, knockback, player.whoAmI, 1f, (i % 5 == 0) ? 1f : 0f);
				if (knife2.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[knife2].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(1, 3);
			}
			return false;
		}
		return true;
	}
}
