using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BlossomFlux : LegendaryItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override Color? TooltipExtensionColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(109, 161, 84);
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 68;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 4;
		base.Item.useAnimation = 16;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.15f;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<LeafArrow>();
		base.Item.shootSpeed = 10f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.consumeAmmoOnFirstShotOnly = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.useTime = 25;
			base.Item.useAnimation = 25;
			base.Item.useLimitPerAnimation = 1;
			base.Item.UseSound = SoundID.Item77;
		}
		else
		{
			base.Item.useTime = 3;
			base.Item.useAnimation = 15;
			base.Item.useLimitPerAnimation = 5;
			base.Item.UseSound = SoundID.Item5;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SporeBomb>(), (int)((float)damage * 3.15f), knockback * 60f, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LeafArrow>(), damage, knockback, player.whoAmI);
		}
		return false;
	}
}
