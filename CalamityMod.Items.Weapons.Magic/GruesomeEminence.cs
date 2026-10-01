using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class GruesomeEminence : ExhumedItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 74;
		base.Item.damage = 666;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.mana = 400;
		base.Item.useTime = 27;
		base.Item.useAnimation = 27;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.shootSpeed = 9f;
		base.Item.shoot = ModContent.ProjectileType<GruesomeEminenceHoldout>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<GhastlyVisage>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		return false;
	}
}
