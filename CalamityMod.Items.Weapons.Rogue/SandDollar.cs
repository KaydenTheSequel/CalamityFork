using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SandDollar : RogueWeapon
{
	public override float StealthDamageMultiplier => 1f;

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 28;
		base.Item.damage = 26;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.knockBack = 3.5f;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<SandDollarProj>();
		base.Item.shootSpeed = 14f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] >= 2 && !player.Calamity().StealthStrikeAvailable())
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 2;
			for (int i = 0; i < 2; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-2, 3), velocity.Y + (float)Main.rand.Next(-2, 3)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int stealth = Projectile.NewProjectile(source, position.X, position.Y, perturbedspeed.X * 1.5f, perturbedspeed.Y * 1.5f, ModContent.ProjectileType<SandDollarStealth>(), damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(1, 3);
			}
			return false;
		}
		return true;
	}
}
