using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Vehemenc" })]
public class Vehemence : ModItem, ILocalizedModType, IModType
{
	public static float SkullRatio = 0.11f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 6666;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 41;
		base.Item.useAnimation = (base.Item.useTime = 43);
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.75f;
		base.Item.UseSound = SoundID.Item73;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VehemenceHoldout>();
		base.Item.shootSpeed = 16.5f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		Item v = player.HeldItem;
		int chargeTime = 2 * v.useAnimation;
		Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, chargeTime);
		return false;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}
}
