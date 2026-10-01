using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Condemnation : ModItem, ILocalizedModType, IModType
{
	public static int MaxLoadedArrows = 9;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 130;
		base.Item.height = 42;
		base.Item.damage = 2130;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.useStyle = 5;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<CondemnationArrow>();
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-50f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		CalamityPlayer calPlayer = player.Calamity();
		if (Main.myPlayer == player.whoAmI)
		{
			calPlayer.rightClickListener = true;
			calPlayer.mouseRotationListener = true;
		}
		if (calPlayer.mouseRight && player.ownedProjectileCounts[ModContent.ProjectileType<CondemnationHoldout>()] <= 0)
		{
			base.Item.noUseGraphic = false;
		}
		else
		{
			base.Item.noUseGraphic = true;
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<CondemnationHoldout>()] <= 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if ((float)player.altFunctionUse != 2f)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<CondemnationHoldout>()] > 0;
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
		if (player.Calamity().mouseRight)
		{
			Vector2 tipPosition = position + shootDirection * 110f;
			Projectile.NewProjectile(source, tipPosition, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, shootDirection, ModContent.ProjectileType<CondemnationHoldout>(), 0, 0f, player.whoAmI);
		}
		return false;
	}
}
