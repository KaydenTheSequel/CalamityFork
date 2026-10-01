using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class OpalStriker : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Charge = new SoundStyle("CalamityMod/Sounds/Item/OpalCharge")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle ChargeLoop = new SoundStyle("CalamityMod/Sounds/Item/OpalChargeLoop")
	{
		Volume = 0.5f
	};

	internal static readonly int ChargeLoopSoundFrames = 120;

	public static readonly SoundStyle Fire = new SoundStyle("CalamityMod/Sounds/Item/OpalFire")
	{
		PitchVariance = 0.4f,
		Volume = 0.3f
	};

	public static readonly SoundStyle ChargedFire = new SoundStyle("CalamityMod/Sounds/Item/OpalChargedFire")
	{
		PitchVariance = 0.3f,
		Volume = 0.6f
	};

	public static int AftershotCooldownFrames = 17;

	public static int FullChargeFrames = 88;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 24;
		base.Item.damage = 30;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = AftershotCooldownFrames);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = null;
		base.Item.autoReuse = false;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<OpalStrikerHoldout>();
		base.Item.shootSpeed = 12f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - spawnPosition, ModContent.ProjectileType<OpalStrikerHoldout>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3081, 25).AddIngredient(117, 10).AddIngredient(182, 3)
			.AddIngredient(999, 3)
			.AddTile(16)
			.Register();
	}
}
