using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SurgeDriver : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 164;
		base.Item.height = 58;
		base.Item.damage = 140;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PrismEnergyBullet>();
		base.Item.shootSpeed = 11f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-50f, -8f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
		base.Item.noUseGraphic = !player.Calamity().mouseRight || player.ownedProjectileCounts[ModContent.ProjectileType<SurgeDriverHoldout>()] > 0;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<SurgeDriverHoldout>()] <= 0;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if ((float)player.altFunctionUse != 2f)
		{
			return 1f;
		}
		return 2.5f;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if ((float)player.altFunctionUse != 2f)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<SurgeDriverHoldout>()] > 0;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
		Vector2 gunTip = position + shootDirection * base.Item.scale * 126f;
		gunTip.Y -= 6f;
		if (player.Calamity().mouseRight)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 newShootVelocity = velocity * Main.rand.NextFloat(1f, 1.45f);
				newShootVelocity = newShootVelocity.RotatedByRandom(0.15000000596046448);
				Projectile.NewProjectile(source, gunTip, newShootVelocity, base.Item.shoot, damage, knockback, player.whoAmI);
			}
		}
		else
		{
			Projectile.NewProjectile(source, gunTip, velocity, ModContent.ProjectileType<SurgeDriverHoldout>(), 0, knockback, player.whoAmI);
		}
		return false;
	}
}
