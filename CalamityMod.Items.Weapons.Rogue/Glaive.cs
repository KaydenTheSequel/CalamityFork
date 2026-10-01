using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Glaive : RogueWeapon
{
	public override float StealthVelocityMultiplier => 1.3f;

	public override float StealthDamageMultiplier => 0.6f;

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 32;
		base.Item.damage = 22;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.value = Item.buyPrice(0, 10);
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shootSpeed = 13f;
		base.Item.shoot = ModContent.ProjectileType<GlaiveProj>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 6f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, -1f);
		if (player.Calamity().StealthStrikeAvailable())
		{
			p.Calamity().stealthStrike = true;
			p.netUpdate = true;
			int goType = ModContent.ProjectileType<GlaiveOrbital>();
			if (player.ownedProjectileCounts[goType] <= 0)
			{
				p = Projectile.NewProjectileDirect(source, position, Vector2.Zero, ModContent.ProjectileType<GlaiveOrbital>(), damage, 10f, player.whoAmI, 0f, -1f);
				p.Calamity().stealthStrike = true;
			}
			else
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile proj = enumerator.Current;
					if (proj.active && proj.type == goType && proj.owner == player.whoAmI)
					{
						proj.timeLeft += 300;
						proj.netUpdate = true;
						break;
					}
				}
			}
		}
		return false;
	}
}
