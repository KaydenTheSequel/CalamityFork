using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class StellarCannon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 30;
		base.Item.damage = 180;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 40;
		base.Item.useAnimation = 40;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item92;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AstralCannonProjectile>();
		base.Item.shootSpeed = 3f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 16f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}
}
