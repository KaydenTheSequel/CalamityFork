using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "T1000" })]
public class AetherfluxCannon : ModItem, ILocalizedModType, IModType
{
	public const int UseTime = 36;

	public static Color mainColor;

	public static Color accentColor;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 94;
		base.Item.height = 54;
		base.Item.damage = 355;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 4f;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AetherfluxCannonHoldout>();
		base.Item.shootSpeed = 24f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
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
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - spawnPosition, ModContent.ProjectileType<AetherfluxCannonHoldout>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<NanoPurge>().AddIngredient<AuricBar>(5).AddIngredient<UelibloomBar>(12)
			.AddIngredient<DivineGeode>(8)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	static AetherfluxCannon()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.Goldenrod;
		accentColor = Color.LightGreen;
	}
}
