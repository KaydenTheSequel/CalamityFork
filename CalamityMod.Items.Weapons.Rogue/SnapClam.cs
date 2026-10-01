using System;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SnapClam : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 16;
		base.Item.damage = 14;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<SnapClamProj>();
		base.Item.shootSpeed = 12f;
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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 3;
			for (int i = 0; i < 5; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-3, 4), velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int proj = Projectile.NewProjectile(source, position, perturbedspeed, ModContent.ProjectileType<SnapClamStealth>(), Math.Max(damage / 5, 1), knockback / 5f, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(1, 3);
			}
			return false;
		}
		return true;
	}
}
