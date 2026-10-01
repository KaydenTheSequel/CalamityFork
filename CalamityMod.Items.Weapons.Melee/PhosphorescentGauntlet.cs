using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class PhosphorescentGauntlet : ModItem, ILocalizedModType, IModType
{
	public const int OnHitIFrames = 15;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 40);
		base.Item.damage = 2705;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<PhosphorescentGauntletPunches>();
		base.Item.shootSpeed = 1f;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}
}
