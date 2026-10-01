using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class HeliumFlash : ModItem, ILocalizedModType, IModType
{
	internal const float ExplosionDamageMultiplier = 1.4f;

	public static readonly SoundStyle Charge = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCharge");

	public static readonly SoundStyle ChargeLoop = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashFullChargeLoop");

	internal static readonly int ChargeLoopSoundFrames = 120;

	public static readonly SoundStyle ChargeFire = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashFire")
	{
		Volume = 1f
	};

	public static int AftershotCooldownFrames = 17;

	public static int FullChargeFrames = 88;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 112;
		base.Item.height = 112;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.damage = 5100;
		base.Item.knockBack = 9.5f;
		base.Item.mana = 80;
		base.Item.useAnimation = (base.Item.useTime = AftershotCooldownFrames);
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 5;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.shoot = ModContent.ProjectileType<HeliumFlashHoldout>();
		base.Item.shootSpeed = 15f;
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
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - spawnPosition, ModContent.ProjectileType<HeliumFlashHoldout>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VenusianTrident>().AddIngredient<ForbiddenSun>().AddIngredient<AuricBar>(5)
			.AddIngredient(3458, 10)
			.AddIngredient(3457, 5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
