using CalamityMod.Cooldowns;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class M1Garand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 102;
		base.Item.height = 22;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 40;
		base.Item.useAnimation = 40;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.autoReuse = false;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<M1GarandHoldout>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.noUseGraphic = true;
		base.Item.value = Item.buyPrice(0, 20);
		base.Item.rare = 3;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		if (player.Calamity().cooldowns.TryGetValue(M1GarandShots.ID, out var cooldown))
		{
			cooldown.timeLeft = 8 - player.Calamity().garandShots;
		}
		else
		{
			player.AddCooldown(M1GarandShots.ID, 8);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<M1GarandHoldout>(), damage, knockback, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}
}
