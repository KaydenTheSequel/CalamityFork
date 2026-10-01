using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FlakKraken : ModItem, ILocalizedModType, IModType
{
	public static int OriginalUseTime = 63;

	public static float TimeBetweenShots = 21f;

	public static float ProjectilesPerBurst = 2f;

	public static float OwnerKnockbackStrength = 2.5f;

	public static float ProjectileGravityStrength = 0.22f;

	public static float ProjectileShootSpeed = 25f;

	public static float InitialShotDamageMultiplier = 1f;

	public static float InitialShotHitShrapnelDamageMultiplier = 0.3f;

	public static int ShrapnelAmount = 5;

	public static float ShrapnelAngleOffset = (float)Math.PI / 4f;

	public static int ClusterShrapnelAmount = 8;

	public static float ClusterShrapnelAngleOffset = (float)Math.PI / 2f + MathHelper.ToRadians(50f);

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 152;
		base.Item.height = 58;
		base.Item.damage = 78;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = OriginalUseTime);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<FlakKrakenHoldout>();
		base.Item.shootSpeed = 15f;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
		{
			Volume = 0.4f,
			Pitch = -0.95f,
			PitchVariance = 0.1f
		};
		base.Item.useStyle = 5;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
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
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<FlakKrakenHoldout>(), 0, 0f, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FlakToxicannon>().AddIngredient<Voidstone>(20).AddIngredient<DepthCells>(20)
			.AddTile(134)
			.Register();
	}
}
