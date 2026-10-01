using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SeasSearing : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 44;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 10;
		base.Item.reuseDelay = 16;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SeasSearingBubble>();
		base.Item.shootSpeed = 13f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, -5f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.useTime = 30;
			base.Item.useAnimation = 30;
			base.Item.reuseDelay = 0;
		}
		else
		{
			base.Item.useTime = 5;
			base.Item.useAnimation = 10;
			base.Item.reuseDelay = 16;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SeasSearingSecondary>(), (int)((float)damage * 1.22f), knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SeasSearingBubble>(), damage, knockback, player.whoAmI, 1f);
		}
		return false;
	}
}
