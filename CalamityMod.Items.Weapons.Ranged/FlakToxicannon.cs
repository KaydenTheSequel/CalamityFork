using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FlakToxicannon : ModItem, ILocalizedModType, IModType
{
	public static float OwnerKnockbackStrength = 1.1f;

	public static float ProjectileGravityStrength = 0.17f;

	public static float ProjectileShootSpeed = 25f;

	public static float InitialShotDamageMultiplier = 1f;

	public static float InitialShotHitShrapnelDamageMultiplier = 0.2f;

	public static int ShrapnelAmount = 4;

	public static float ShrapnelAngleOffset = 0.11f;

	public static int ClusterShrapnelAmount = 7;

	public static float ClusterShrapnelAngleOffset = 0.53f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 28;
		base.Item.damage = 73;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 44);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<FlakToxicannonHoldout>();
		base.Item.shootSpeed = 15f;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
		{
			Volume = 0.4f,
			Pitch = -0.7f,
			PitchVariance = 0.1f
		};
		base.Item.useStyle = 5;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] != 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<FlakToxicannonHoldout>(), 0, 0f, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}
}
