using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class MagnaCannon : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ChargeFull = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonChargeFull")
	{
		Volume = 0.5f
	};

	internal static readonly int ChargeFullSoundFrames = 42;

	public static readonly SoundStyle ChargeLoop = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonChargeLoop")
	{
		Volume = 0.5f
	};

	internal static readonly int ChargeLoopSoundFrames = 153;

	public static readonly SoundStyle ChargeStart = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonChargeStart")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle Fire = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot")
	{
		PitchVariance = 0.3f,
		Volume = 0.4f
	};

	public static int AftershotCooldownFrames = 30;

	public static int FullChargeFrames = 138;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 34;
		base.Item.damage = 25;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = AftershotCooldownFrames);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = null;
		base.Item.autoReuse = false;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<MagnaCannonHoldout>();
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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = player.RotatedRelativePoint(player.MountedCenter);
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - player.RotatedRelativePoint(player.MountedCenter), ModContent.ProjectileType<MagnaCannonHoldout>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3086, 25).AddIngredient(117, 12).AddIngredient(182, 3)
			.AddIngredient(177, 5)
			.AddTile(16)
			.Register();
	}
}
