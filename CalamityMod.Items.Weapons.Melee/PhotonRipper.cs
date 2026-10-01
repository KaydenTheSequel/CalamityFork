using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class PhotonRipper : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 134;
		base.Item.height = 54;
		base.Item.damage = 3725;
		base.Item.knockBack = 12f;
		base.Item.useTime = 5;
		base.Item.useAnimation = 25;
		base.Item.axe = 600;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<PhotonRipperProjectile>();
		base.Item.shootSpeed = 1f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 18f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
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
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		float breakBlocks = 1f;
		if (player.Calamity().mouseRight && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse)
		{
			breakBlocks = 0f;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f, breakBlocks);
		return false;
	}
}
